using MeshSplitting.Splitables;
using UnityEditor;
using UnityEngine;

namespace MeshSplitting.Splitters
{
    public class SimpleSplitter : MonoBehaviour
    {
        public Splitable Floor;

        public void SplitObject()
        {
            Floor.Split(transform);
        }
    }

    [CustomEditor(typeof(SimpleSplitter))]
    public class SimpleSplitterEditor : Editor
    {
        [SerializeField] public float Width;
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            var t = (SimpleSplitter)target;
            if (GUILayout.Button("Split Floor"))
            {

                //for (int i = t.transform.childCount; i > 0; --i)
                //{
                //    DestroyImmediate(t.transform.GetChild(0).gameObject);
                //}

                t.SplitObject();
            }
        }
    }
}