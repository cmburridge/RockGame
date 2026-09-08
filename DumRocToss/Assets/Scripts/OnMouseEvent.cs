using UnityEngine;

public class OnMouseEvent : MonoBehaviour
{
    public bool switchOn = false;
    public Rigidbody2D rb;
    public float reverseGravity = -1f;

    void OnApplicationQuit()
    {
        PlayerPrefs.Save();
    }

    private void Update()
    {
        // 1. Check for Touch or Mouse Click
        bool isInputDetected = Input.touchCount > 0 || Input.GetMouseButton(0);

        if (isInputDetected)
        {
            // 2. Get the position of the touch/click
            Vector3 pos = Input.touchCount > 0 ? (Vector3)Input.GetTouch(0).position : Input.mousePosition;
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(pos);

            // 3. Find ALL colliders at that point
            RaycastHit2D[] hits = Physics2D.RaycastAll(worldPoint, Vector2.zero);

            bool hitTarget = false;
            foreach (RaycastHit2D hit in hits)
            {
                // SKIP Polygon Colliders
                if (hit.collider is PolygonCollider2D) continue;

                // CHECK if we hit this object
                if (hit.collider.gameObject == gameObject)
                {
                    hitTarget = true;
                    break;
                }
            }

            switchOn = hitTarget;
        }
        else
        {
            switchOn = false;
        }

        // 4. Apply Gravity Logic
        if (switchOn)
        {
            rb.gravityScale = reverseGravity;
        }
        else
        {
            rb.gravityScale = 1;
        }
    }
}