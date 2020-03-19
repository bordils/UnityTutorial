using UnityEngine;

[ExecuteInEditMode]
public class CubeEditor : MonoBehaviour
{
    Waypoint waypoint;

    private void Awake()
    {
        waypoint = GetComponent<Waypoint>();
    }

    void Update()
    {
        SnapToGrid();
        UpdateLabel();
    }

    private void SnapToGrid()
    {
        transform.position = new Vector3 (  waypoint.GetGridPos().x * waypoint.GetGridSize(),
                                            0f, 
                                            waypoint.GetGridPos().y * waypoint.GetGridSize()
                                            );
    }

    private void UpdateLabel()
    {
        TextMesh textMesh = GetComponentInChildren<TextMesh>();
        string labelText = waypoint.GetGridPos().x + "." + waypoint.GetGridPos().y;
        textMesh.text = labelText;
        gameObject.name = "cube " + labelText;
    }
}
