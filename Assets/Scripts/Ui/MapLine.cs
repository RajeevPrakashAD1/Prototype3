using UnityEngine;

public class MapLine : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public Transform target1;
    public Transform target2;
    public Transform patentTarget;

    void Update()
    {
        if (lineRenderer == null)
        {
            lineRenderer = GetComponent<LineRenderer>();
        }
        //Debug.Log("target2" + patentTarget.gameObject.activeSelf.ToString());
        // Set the positions of the line renderer's points
        if (!target1.gameObject.activeSelf || !patentTarget.gameObject.activeSelf)
        {
            lineRenderer.enabled = false; // Hide the line renderer if any target is inactive
            
        }
        else
        {
            lineRenderer.enabled = true;
            //Debug.Log("setting line rendree");
            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, target1.position + new Vector3(0, 20f, 0));
            lineRenderer.SetPosition(1, target2.position);
        }
    }
}
