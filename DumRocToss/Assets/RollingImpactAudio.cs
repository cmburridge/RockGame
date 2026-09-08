using UnityEngine;
using Cinemachine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class RollingImpactAudio : MonoBehaviour
{
    [System.Serializable]
    public class SurfaceProfile
    {
        public string surfaceName;
        public LayerMask surfaceLayer;

        [Header("Light / Medium Impacts")]
        public AudioClip[] mediumImpactSounds;

        [Header("Heavy Impacts")]
        public AudioClip heavyImpactSound;

        [Header("Rolling")]
        public AudioClip rollingSound;

        [Header("Particles")]
        public GameObject mediumImpactParticles;
        public GameObject heavyImpactParticles;
    }

    [Header("Surface Profiles")]
    public SurfaceProfile[] surfaces;

    [Header("Impact Settings")]
    public float mediumImpactThreshold = 6f;
    public float heavyImpactThreshold = 12f;
    public float impactCooldown = 0.15f;

    [Header("Rolling Settings")]
    public float rollingVelocityThreshold = 2f;
    public float rollingVolume = 0.35f;

    [Header("Audio Variation")]
    public float pitchVariation = 0.08f;
    public float volumeVariation = 0.1f;

    [Header("Heavy Impact Feel")]
    public CinemachineVirtualCamera virtualCamera;
    public float zoomAmount = 0.4f;
    public float zoomReturnSpeed = 6f;
    public float hitStopDuration = 0.04f;

    private Rigidbody2D rb;

    private AudioSource rollingSource;
    private AudioSource impactSource;

    private float lastImpactTime;
    private float originalOrthoSize;
    private bool zoomActive;

    private SurfaceProfile currentSurface;
    private bool isGrounded;

    public bool is3D;

    void Awake()
    {
        virtualCamera = Object.FindObjectOfType<CinemachineVirtualCamera>();

        rb = GetComponent<Rigidbody2D>();

        // Create rolling source
        rollingSource = gameObject.AddComponent<AudioSource>();
        rollingSource.loop = true;
        rollingSource.playOnAwake = false;

        // Create impact source
        impactSource = gameObject.AddComponent<AudioSource>();
        impactSource.loop = false;
        impactSource.playOnAwake = false;

        if (is3D == true)
        {
            impactSource.spatialBlend = 1;
            rollingSource.spatialBlend = 1;
            impactSource.maxDistance = 2;
            rollingSource.maxDistance = 2;
        }

        if (virtualCamera != null)
            originalOrthoSize = virtualCamera.m_Lens.OrthographicSize;
    }

    void Update()
    {      

        HandleRolling();
        HandleZoomReturn();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        SurfaceProfile surface = GetSurfaceProfile(collision.gameObject.layer);
        if (surface == null) return;

        float impactForce = collision.relativeVelocity.magnitude;

        if (Time.time - lastImpactTime < impactCooldown)
            return;

        if (impactForce >= heavyImpactThreshold)
        {
            PlayHeavyImpact(surface, collision, impactForce);
            lastImpactTime = Time.time;
        }
        else if (impactForce >= mediumImpactThreshold)
        {
            PlayMediumImpact(surface, collision);
            lastImpactTime = Time.time;
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        currentSurface = GetSurfaceProfile(collision.gameObject.layer);
        isGrounded = currentSurface != null;
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
        currentSurface = null;
        rollingSource.Stop();
    }

    SurfaceProfile GetSurfaceProfile(int layer)
    {
        foreach (var surface in surfaces)
        {
            if (((1 << layer) & surface.surfaceLayer) != 0)
                return surface;
        }
        return null;
    }

    void PlayMediumImpact(SurfaceProfile surface, Collision2D collision)
    {
        if (surface.mediumImpactSounds.Length > 0)
        {
            AudioClip clip = surface.mediumImpactSounds[
                Random.Range(0, surface.mediumImpactSounds.Length)
            ];

            PlayImpactClip(clip);
        }

        if (surface.mediumImpactParticles != null)
            SpawnParticles(surface.mediumImpactParticles, collision);
    }

    void PlayHeavyImpact(SurfaceProfile surface, Collision2D collision, float force)
    {
        if (surface.heavyImpactSound != null)
            PlayImpactClip(surface.heavyImpactSound);

        if (surface.heavyImpactParticles != null)
            SpawnParticles(surface.heavyImpactParticles, collision);

        TriggerZoom(force);
        StartCoroutine(HitStop());
    }

    void HandleRolling()
    {
        if (!isGrounded || currentSurface == null)
        {
            rollingSource.Stop();
            return;
        }

        float speed = rb.velocity.magnitude;

        if (speed > rollingVelocityThreshold && currentSurface.rollingSound != null)
        {
            if (!rollingSource.isPlaying)
            {
                rollingSource.clip = currentSurface.rollingSound;
                rollingSource.volume = rollingVolume;
                rollingSource.pitch = 1f + Random.Range(-pitchVariation, pitchVariation) - .25f;
                rollingSource.Play();
            }
        }
        else
        {
            if (rollingSource.isPlaying)
                rollingSource.Stop();
        }
    }

    void PlayImpactClip(AudioClip clip)
    {
        impactSource.pitch = 1f + Random.Range(-pitchVariation, pitchVariation);
        impactSource.volume = 1f + Random.Range(-volumeVariation, volumeVariation);
        impactSource.PlayOneShot(clip);
    }

    void SpawnParticles(GameObject prefab, Collision2D collision)
    {
        ContactPoint2D contact = collision.contacts[0];
        Instantiate(prefab, contact.point, Quaternion.identity);
    }

    void TriggerZoom(float force)
    {
        if (virtualCamera == null) return;

        float intensity = Mathf.InverseLerp(
            heavyImpactThreshold,
            heavyImpactThreshold * 2f,
            force
        );

        float zoom = zoomAmount * (1f + intensity * 0.5f);

        virtualCamera.m_Lens.OrthographicSize = originalOrthoSize - zoom;
        zoomActive = true;
    }

    void HandleZoomReturn()
    {
        if (!zoomActive || virtualCamera == null)
            return;

        virtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(
            virtualCamera.m_Lens.OrthographicSize,
            originalOrthoSize,
            Time.unscaledDeltaTime * zoomReturnSpeed
        );

        if (Mathf.Abs(virtualCamera.m_Lens.OrthographicSize - originalOrthoSize) < 0.01f)
        {
            virtualCamera.m_Lens.OrthographicSize = originalOrthoSize;
            zoomActive = false;
        }
    }

    IEnumerator HitStop()
    {
        Time.timeScale = 0.05f;
        yield return new WaitForSecondsRealtime(hitStopDuration);
        Time.timeScale = 1f;
    }
}