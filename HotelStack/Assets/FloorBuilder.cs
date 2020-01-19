using System.Collections.Generic;
using UnityEngine;

public class FloorBuilder : MonoBehaviour
{
    public float xWidth;
    public float zWidth;
    public FloorSideBuilder SidePrefab;

    private List<FloorSideBuilder> _sides;

    public void BuildFloorSides()
    {
        _sides = new List<FloorSideBuilder>();
        var wallOffset = SidePrefab.WallThickness * 0.5f;

        _sides.Add(SpawnFloorSide(Vector3.zero, Vector3.zero, xWidth));
        _sides.Add(SpawnFloorSide(Vector3.forward * zWidth , Vector3.zero, xWidth));

        _sides.Add(SpawnFloorSide(Vector3.forward * wallOffset + Vector3.right * wallOffset, Vector3.up * -90, zWidth - SidePrefab.WallThickness));
        _sides.Add(SpawnFloorSide(Vector3.right * (xWidth - wallOffset)  + Vector3.forward * wallOffset, Vector3.up * -90, zWidth - SidePrefab.WallThickness));

    }

    private FloorSideBuilder SpawnFloorSide(Vector3 position, Vector3 rotation, float width)
    {
        var side = Instantiate(SidePrefab, position, Quaternion.Euler(rotation));
        side.BuildFloorSide(width);
        side.transform.SetParent(transform);

        return side;
    }
}
