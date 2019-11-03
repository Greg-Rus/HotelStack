using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[CustomEditor(typeof(FloorBuilder))]
public class FloorBuilderEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        var t = (FloorBuilder)target;
        if (GUILayout.Button("Build Floor Sides"))
        {
            for (int i = t.transform.childCount; i > 0; --i)
            {
                DestroyImmediate(t.transform.GetChild(0).gameObject);
            }

            t.BuildFloorSides();
        }
    }
}

public class FloorBuilder : MonoBehaviour
{
    public float xWidth;
    public float zWidth;
    public FloorSideBuilder SidePrefab;

    public void BuildFloorSides()
    {
        var wallOffset = SidePrefab.WallThickness * 0.5f;

        SpawnFloorSide(Vector3.zero, Vector3.zero, xWidth);
        SpawnFloorSide(Vector3.forward * zWidth , Vector3.zero, xWidth);

        SpawnFloorSide(Vector3.forward * wallOffset + Vector3.right * wallOffset, Vector3.up * -90, zWidth - SidePrefab.WallThickness);
        SpawnFloorSide(Vector3.right * (xWidth - wallOffset)  + Vector3.forward * wallOffset, Vector3.up * -90, zWidth - SidePrefab.WallThickness);

    }

    private void SpawnFloorSide(Vector3 position, Vector3 rotation, float width)
    {
        var side = Instantiate(SidePrefab, position, Quaternion.Euler(rotation));
        side.BuildFloorSide(width);
        side.transform.SetParent(transform);
    }
}
