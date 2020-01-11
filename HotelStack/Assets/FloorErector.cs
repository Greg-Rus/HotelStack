using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FloorErector : MonoBehaviour
{
    public SideErector SidePrefab;
    public GameObject GroundPrefab;

    public float Width;
    public float Depth;

    public SideErector South;
    public SideErector North;
    public SideErector East;
    public SideErector West;
    public GameObject Ground;

    public List<MeshFilter> Meshes;


    public void BuildFloor(float width, float depth)
    {
        Meshes = new List<MeshFilter>();
        Width = width;
        Depth = depth;

        South = BuildSide(Vector3.zero, false);
        North = BuildSide(Vector3.forward * depth, false);
        East = BuildSide(Vector3.right * width, true);
        West = BuildSide(Vector3.zero, true);

        Meshes.AddRange(South.GetComponentsInChildren<MeshFilter>());
        Meshes.AddRange(North.GetComponentsInChildren<MeshFilter>());
        Meshes.AddRange(East.GetComponentsInChildren<MeshFilter>());
        Meshes.AddRange(West.GetComponentsInChildren<MeshFilter>());

        BuildGround();
    }

    private void BuildGround()
    {
        Ground = Instantiate(GroundPrefab, new Vector3(Width * 0.5f, 0f, Depth * 0.5f), Quaternion.Euler(90f,0f,0f));
        Ground.transform.localScale = new Vector3(Width,Depth);
        Ground.transform.parent = transform;

        Meshes.Add(Ground.GetComponent<MeshFilter>());
    }

    private SideErector BuildSide(Vector3 position, bool rotated)
    {
        var side = Instantiate(SidePrefab, position, Quaternion.identity);
        side.MakeSide(rotated ? Depth : Width);
        if(rotated) side.transform.rotation = Quaternion.Euler(0f, -90f, 0f);
        side.transform.parent = transform;
        return side;
    }
}

[CustomEditor(typeof(FloorErector))]
public class FloorErectorEditor : Editor
{
    [SerializeField] public float Width;
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        var t = (FloorErector)target;
        if (GUILayout.Button("Build Floor"))
        {

            for (int i = t.transform.childCount; i > 0; --i)
            {
                DestroyImmediate(t.transform.GetChild(0).gameObject);
            }

            t.BuildFloor(t.Width, t.Depth);
        }
    }
}
