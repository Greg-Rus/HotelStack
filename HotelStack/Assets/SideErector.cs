using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SideErector : MonoBehaviour
{
    public MeshFilter Wall;
    public MeshFilter Window;
    public float Width;

    private List<MeshFilter> _meshFilters;

    public List<MeshFilter> MeshFilters
    {
        get { return _meshFilters; }
    }

    void Awake()
    {
        _meshFilters = new List<MeshFilter>();
    }

    public void MakeSide(float width)
    {
        MeshFilters.Clear();

        if (width <= 1)
        {
            var obj = Instantiate(Wall);
            obj.transform.localScale = new Vector3(width, 1f, 1f);
            obj.transform.parent = this.transform;
            obj.transform.localPosition = Vector3.zero;
            MeshFilters.Add(obj);
        }

        else if(width > 1 && width <= 3)
        {
            var objectScale = width / 3f;

            MeshFilters.Add(SpawnObject(Wall, 0, objectScale));
            MeshFilters.Add(SpawnObject(Window, objectScale, objectScale));
            MeshFilters.Add(SpawnObject(Wall, objectScale * 2, objectScale));
        }
        else
        {
            var windowCount = Mathf.Ceil(width * 0.5f);
            var wallScale = (width - windowCount) / (windowCount + 1);

            var elapsedWidth = 0f;
            for (int i = 0; i < windowCount; i++)
            {
                MeshFilters.Add(SpawnObject(Wall, elapsedWidth, wallScale));
                MeshFilters.Add(SpawnObject(Window,elapsedWidth + wallScale,1));
                elapsedWidth += 1 + wallScale;
            }
            MeshFilters.Add(SpawnObject(Wall, elapsedWidth, wallScale));
        }
    }

    private MeshFilter SpawnObject(MeshFilter go, float position, float scale)
    {
        var obj = Instantiate(go);
        obj.transform.parent = this.transform;
        obj.transform.localPosition = Vector3.right * position;
        obj.transform.localScale = new Vector3(scale, 1f, 1f);

        return obj;
    }
}

[CustomEditor(typeof(SideErector))]
public class SideErectorEditor : Editor
{
    [SerializeField] public float Width;
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        var t = (SideErector)target;
        if (GUILayout.Button("Build Object"))
        {

            for (int i = t.transform.childCount; i > 0; --i)
            {
                DestroyImmediate(t.transform.GetChild(0).gameObject);
            }

            t.MakeSide(t.Width);
        }
    }
}
