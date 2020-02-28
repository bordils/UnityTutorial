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
        transform.position = new Vector3 (  waypoint.GetGridPos().x,
                                            0f, 
                                            waypoint.GetGridPos().y);
    }

    private void UpdateLabel()
    {
        TextMesh textMesh = GetComponentInChildren<TextMesh>();
        string labelText = waypoint.GetGridPos().x / waypoint.GetGridSize() + "." + waypoint.GetGridPos().y / waypoint.GetGridSize();
        textMesh.text = labelText;
        gameObject.name = "cube " + labelText;
    }
}
