using UnityEngine;
using UnityEngine.AI;

public class Navigation : MonoBehaviour
{
    public Transform destination;
    public LineRenderer lineRenderer;
    public float updateInterval = 0.5f;

    private NavMeshPath path;
    private float lastUpdateTime;
    
    void Start()
    {
        path = new NavMeshPath();
        UpdatePath();
    }

    void Update()
    {
        if (Time.time - lastUpdateTime > updateInterval)
        {
            UpdatePath();
            lastUpdateTime = Time.time;
        }
    }

    void UpdatePath()
    {
        NavMesh.CalculatePath(transform.position, destination.position, NavMesh.AllAreas, path);

        DisplayPath(path);
    }

    void DisplayPath(NavMeshPath path)
    {
        lineRenderer.positionCount = path.corners.Length;
        lineRenderer.SetPositions(path.corners);
    }
}
