using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SideErector : MonoBehaviour
{
    public GameObject Wall;
    public GameObject Window;
    public float Width;

    public void MakeSide(float width)
    {
        if (width <= 1)
        {
            var obj = Instantiate(Wall);
            obj.transform.localScale = new Vector3(width, 1f, 1f);
            obj.transform.parent = this.transform;
            obj.transform.localPosition = Vector3.zero;
        }

        else if(width > 1 && width <= 3)
        {
            var objectScale = width / 3f;

            SpawnObject(Wall, 0, objectScale);
            SpawnObject(Window, objectScale, objectScale);
            SpawnObject(Wall, objectScale * 2, objectScale);
        }
        else
        {
            var windowCount = Mathf.Ceil(width * 0.5f);
            var wallScale = (width - windowCount) / (windowCount + 1);

            var elapsedWidth = 0f;
            for (int i = 0; i < windowCount; i++)
            {
                SpawnObject(Wall, elapsedWidth, wallScale);
                SpawnObject(Window,elapsedWidth + wallScale,1);
                elapsedWidth += 1 + wallScale;
            }
            SpawnObject(Wall, elapsedWidth, wallScale);
        }
    }

    private void SpawnObject(GameObject go, float position, float scale)
    {
        var obj = Instantiate(go);
        obj.transform.parent = this.transform;
        obj.transform.localPosition = Vector3.right * position;
        obj.transform.localScale = new Vector3(scale, 1f, 1f);
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
