using MeshSplitting.Splitables;
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
}