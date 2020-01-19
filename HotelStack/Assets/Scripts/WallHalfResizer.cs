using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallHalfResizer : MonoBehaviour
{
    [SerializeField] private MeshFilter _wallMeshFilter;
    [SerializeField] private float _baseWallWidth;
    [SerializeField] private float _baseThickness;

    void OnValidate()
    {
        _baseWallWidth = _wallMeshFilter.sharedMesh.bounds.size.x;
        _baseThickness = _wallMeshFilter.sharedMesh.bounds.size.z;
    }

    public void SetWidth(float desiredWidth)
    {
        var newWallScale = desiredWidth / _baseWallWidth;
        _wallMeshFilter.transform.localScale = new Vector3(newWallScale, 1f, 1f);
    }

    public float BaseThickness => _baseThickness;
}
