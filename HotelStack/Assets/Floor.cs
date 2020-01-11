using MeshSplitting.Splitables;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Splitable))]
public class Floor : MonoBehaviour
{
    public MeshFilter MeshFilter;
    public Rigidbody Rigidbody;
    public Splitable Splitable;

    public void Split(Transform splitTransform)
    {
        Splitable.Split(splitTransform);
    }

    public void SetMesh(Mesh mesh)
    {
        MeshFilter.mesh = mesh;
    }
}