using UnityEngine;
using Cinemachine;

/// <summary>
/// Dynamically tunes the CinemachineVirtualCamera's follow damping, forward lookahead offset,
/// and orthographic zoom based on the tracked player's current speed. This keeps the camera glued
/// to fast-moving targets (tighter damping at high speed), biases the framing ahead of the
/// direction of travel, and gently zooms out as speed increases, returning to normal as the
/// player slows down.
/// </summary>
[RequireComponent(typeof(CinemachineVirtualCamera))]
public class DynamicFollowCamera : MonoBehaviour
{
    [Header("Target")]
    [Tooltip("Rigidbody2D of the currently active player. Resolved automatically from the vcam's Follow target if left empty.")]
    public Rigidbody2D targetRigidbody;

    [Header("Speed Range")]
    [Tooltip("Speed (units/sec) below which all dynamic effects are at their resting value.")]
    public float minSpeedForEffects = 2f;
    [Tooltip("Speed (units/sec) at or above which all dynamic effects reach their maximum value.")]
    public float maxSpeedForEffects = 20f;
    [Tooltip("How quickly the tracked speed itself eases toward the player's real speed, in seconds. Raise this to prevent a jarring camera snap when the player stops or collides suddenly.")]
    public float speedSmoothTime = 0.35f;

    [Header("Damping - Keep Up With Fast Movement")]
    [Tooltip("Follow damping used while the player is slow/stationary, for smooth camera motion.")]
    public float minDamping = 0.4f;
    [Tooltip("Follow damping used while the player is at or above maxSpeedForEffects, for a tight, lag-free follow.")]
    public float maxDamping = 0.05f;

    [Header("Lookahead - Lead Into Direction Of Travel")]
    [Tooltip("How far (world units) the framing target is pushed ahead of the player at top speed.")]
    public float maxLeadDistance = 4f;
    [Tooltip("How quickly the lead offset itself is smoothed, in seconds.")]
    public float leadSmoothTime = 0.25f;

    [Header("Zoom - Pull Back With Speed")]
    [Tooltip("Orthographic size used at low/no speed. Captured automatically at Start if left at 0.")]
    public float baseOrthoSize = 0f;
    [Tooltip("Additional orthographic size added on top of baseOrthoSize at maxSpeedForEffects.")]
    public float maxZoomOut = 4f;
    [Tooltip("How quickly the orthographic size itself is smoothed, in seconds.")]
    public float zoomSmoothTime = 0.35f;

    private CinemachineVirtualCamera virtualCamera;
    private CinemachineFramingTransposer framingTransposer;
    private Vector2 currentLeadOffset;
    private Vector2 leadOffsetVelocity;
    private float zoomVelocity;
    private float smoothedSpeed;
    private float smoothedSpeedVelocity;

    // The vcam's originally-assigned Follow target (e.g. a spawn-point transform that gets
    // re-parented under the spinning player). Used only to read position and to locate the
    // player's Rigidbody2D up the hierarchy - never used for rotation.
    private Transform sourceFollowTarget;

    // A runtime-only, rotation-locked proxy that mirrors sourceFollowTarget's position every
    // frame. The vcam actually follows this instead of the real target so that a physics-based
    // spin never gets baked into FramingTransposer's target-local TrackedObjectOffset, which
    // would otherwise make the lead offset (and the camera) orbit around the player as it spins.
    private Transform followAnchor;

    private void Awake()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        framingTransposer = virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();

        sourceFollowTarget = virtualCamera.Follow;

        GameObject anchorObject = new GameObject("DynamicFollowCamera Anchor (Position Only)");
        anchorObject.hideFlags = HideFlags.DontSave;
        followAnchor = anchorObject.transform;
        followAnchor.rotation = Quaternion.identity;

        if (sourceFollowTarget != null)
        {
            followAnchor.position = sourceFollowTarget.position;
            virtualCamera.Follow = followAnchor;
        }
    }

    private void OnDestroy()
    {
        if (followAnchor != null)
        {
            Destroy(followAnchor.gameObject);
        }
    }

    private void Start()
    {
        if (baseOrthoSize <= 0f)
        {
            baseOrthoSize = virtualCamera.m_Lens.OrthographicSize;
        }
    }

    private void Update()
    {
        ResolveTargetRigidbody();

        if (targetRigidbody == null || framingTransposer == null)
        {
            return;
        }

        if (sourceFollowTarget != null && followAnchor != null)
        {
            // Copy position only - rotation stays locked to identity so the framing offset
            // below is never distorted by the player spinning.
            followAnchor.position = sourceFollowTarget.position;
        }

        float rawSpeed = targetRigidbody.velocity.magnitude;
        smoothedSpeed = Mathf.SmoothDamp(smoothedSpeed, rawSpeed, ref smoothedSpeedVelocity, speedSmoothTime);

        float speedFactor = Mathf.InverseLerp(minSpeedForEffects, maxSpeedForEffects, smoothedSpeed);
        speedFactor = Mathf.Clamp01(speedFactor);

        ApplyDamping(speedFactor);
        ApplyLeadOffset(speedFactor);
        ApplyZoom(speedFactor);
    }

    /// <summary>
    /// Finds the active player's Rigidbody2D by walking up from the original Follow target
    /// (before it was swapped for the rotation-locked anchor). That target (a spawn-point
    /// transform) is re-parented under the spawned player prefab at runtime, so its ancestor
    /// chain leads to the Rigidbody2D that actually moves.
    /// </summary>
    private void ResolveTargetRigidbody()
    {
        if (targetRigidbody != null)
        {
            return;
        }

        if (sourceFollowTarget != null)
        {
            targetRigidbody = sourceFollowTarget.GetComponentInParent<Rigidbody2D>();
        }
    }

    private void ApplyDamping(float speedFactor)
    {
        float damping = Mathf.Lerp(minDamping, maxDamping, speedFactor);
        framingTransposer.m_XDamping = damping;
        framingTransposer.m_YDamping = damping;
    }

    private void ApplyLeadOffset(float speedFactor)
    {
        Vector2 velocityDirection = targetRigidbody.velocity.normalized;
        Vector2 desiredLeadOffset = velocityDirection * maxLeadDistance * speedFactor;
        currentLeadOffset = Vector2.SmoothDamp(currentLeadOffset, desiredLeadOffset, ref leadOffsetVelocity, leadSmoothTime);
        framingTransposer.m_TrackedObjectOffset = new Vector3(currentLeadOffset.x, currentLeadOffset.y, 0f);
    }

    private void ApplyZoom(float speedFactor)
    {
        float desiredOrthoSize = baseOrthoSize + maxZoomOut * speedFactor;
        LensSettings lens = virtualCamera.m_Lens;
        lens.OrthographicSize = Mathf.SmoothDamp(lens.OrthographicSize, desiredOrthoSize, ref zoomVelocity, zoomSmoothTime);
        virtualCamera.m_Lens = lens;
    }
}
