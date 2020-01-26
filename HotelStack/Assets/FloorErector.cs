using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using MeshSplitting.Splitables;
using UnityEngine;

[ExecuteInEditMode]
public class FloorErector : MonoBehaviour
{
    public float Width;
    public float Depth;
    public float GroundThickness;

    public SideErector South;
    public SideErector North;
    public SideErector East;
    public SideErector West;
    public GameObject Ground;

    private SideErector[] _sideErectors;

    public Transform SplitTransform;

    public List<MeshFilter> Meshes;
    public Floor FloorPrefab;

    void Awake()
    {
        Meshes = new List<MeshFilter>();
        _sideErectors = new []{South, North, East, West};
    }


    public Floor BuildFloor(float width, float depth)
    {
        Meshes.Clear();

        Width = width;
        Depth = depth;

        South.transform.localPosition = Vector3.zero;
        South.MakeSide(width);

        North.transform.localPosition = Vector3.forward * depth;
        North.MakeSide(width);

        East.transform.localPosition = Vector3.right * width;
        East.MakeSide(depth);

        West.transform.localPosition = Vector3.zero;
        West.MakeSide(depth);

        Meshes.AddRange(South.MeshFilters);
        Meshes.AddRange(North.MeshFilters);
        Meshes.AddRange(East.MeshFilters);
        Meshes.AddRange(West.MeshFilters);

        Meshes.Add(BuildGround());

        return SpawnFloor();
    }

    private Floor SpawnFloor()
    {
        var floor = Instantiate(FloorPrefab, transform.position, transform.rotation);
        var combinedMesh = new Mesh();
        combinedMesh.CombineMeshes(CombineMeshes(Meshes));
        floor.MeshFilter.mesh = combinedMesh;

        return floor;
    }

    private CombineInstance[] CombineMeshes(List<MeshFilter> meshes)
    {
        CombineInstance[] combine = new CombineInstance[meshes.Count];

        int i = 0;
        while (i < meshes.Count)
        {
            combine[i].mesh = meshes[i].sharedMesh;
            combine[i].transform = meshes[i].transform.localToWorldMatrix;
            meshes[i].gameObject.SetActive(false);

            i++;
        }

        return combine;
    }

    private MeshFilter BuildGround()
    {
        Ground.transform.position = new Vector3(Width * 0.5f, GroundThickness * 0.5f, Depth * 0.5f);
        Ground.transform.localScale = new Vector3(Width, GroundThickness, Depth);
        Ground.transform.parent = transform;

        return Ground.GetComponent<MeshFilter>();
    }

    public void SplitFloor(Vector3 splitPlaneOrigin, Vector3 splitPlaneRotation)
    {
        SplitTransform.position = splitPlaneOrigin;
        SplitTransform.rotation = Quaternion.Euler(splitPlaneRotation);
    }
}