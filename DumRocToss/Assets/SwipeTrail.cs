using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class SwipeTrail : MonoBehaviour
{
    public Camera cam;
    public float minDistance = 0.02f;     // Lower = tighter follow
    public float trailLifeTime = 0.25f;
    public float lineWidth = 0.18f;

    private LineRenderer line;
    private List<Vector3> points = new List<Vector3>();
    private List<float> times = new List<float>();

    void Awake()
    {
        if (cam == null)
            cam = Camera.main;

        line = GetComponent<LineRenderer>();
        line.positionCount = 0;
        line.startWidth = lineWidth;
        line.endWidth = lineWidth;
        line.numCapVertices = 10;
        line.numCornerVertices = 10;
        line.useWorldSpace = true;
    }

    void Update()
    {
        HandleInput();
    }

    void LateUpdate()
    {
        UpdateTrail();
    }

    void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            points.Clear();
            times.Clear();
            line.positionCount = 0;
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 pos = cam.ScreenToWorldPoint(Input.mousePosition);
            pos.z = 0f;

            // Always force newest point to current position
            if (points.Count == 0)
            {
                AddPoint(pos);
            }
            else
            {
                float dist = Vector3.Distance(points[points.Count - 1], pos);

                if (dist > minDistance)
                {
                    AddPoint(pos);
                }
                else
                {
                    // Snap last point directly to mouse
                    points[points.Count - 1] = pos;
                }
            }
        }
    }

    void AddPoint(Vector3 pos)
    {
        points.Add(pos);
        times.Add(Time.time);
    }

    void UpdateTrail()
    {
        float currentTime = Time.time;

        // Remove expired points
        for (int i = 0; i < times.Count; i++)
        {
            if (currentTime - times[i] > trailLifeTime)
            {
                points.RemoveAt(i);
                times.RemoveAt(i);
                i--;
            }
        }

        line.positionCount = points.Count;

        if (points.Count > 0)
            line.SetPositions(points.ToArray());
    }
}