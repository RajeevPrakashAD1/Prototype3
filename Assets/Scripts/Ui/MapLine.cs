using UnityEngine;

public class MapLine : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public Transform target1;
    public Transform target2;

    void Update()
    {
        if (lineRenderer == null)
        {
            lineRenderer = GetComponent<LineRenderer>();
        }

        // Set the positions of the line renderer's points
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, target1.position+new Vector3(0,20f,0));
        lineRenderer.SetPosition(1, target2.position);
    }
}
