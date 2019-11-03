using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class WindowHalfResizer : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private MeshFilter _upperWindowMeshFilter;
    [SerializeField] private MeshFilter _lowerWindowMeshFilter;
    [SerializeField] private MeshFilter _windowFrameMeshFilter;
    [SerializeField] private float _baseUpperWidth;
    [SerializeField] private float _baseFrameWidth;
    [SerializeField] private float _baseTotalWidth;
    [SerializeField] private float _baseThickness;

    void OnValidate()
    {
        _baseUpperWidth = _upperWindowMeshFilter.sharedMesh.bounds.size.x;
        _baseFrameWidth = _windowFrameMeshFilter.sharedMesh.bounds.size.x;
        _baseTotalWidth = _baseFrameWidth + _baseUpperWidth;
        _baseThickness = _windowFrameMeshFilter.sharedMesh.bounds.size.z;
    }

    public void SetWidth(float desiredWidth)
    {
        var desiredWindowSize = desiredWidth - _baseFrameWidth;
        var newWindowScale = desiredWindowSize / _baseUpperWidth;
        ApplyWidth(newWindowScale);
    }

    private void ApplyWidth(float scale)
    {
        _upperWindowMeshFilter.transform.localScale = new Vector3(scale, 1f, 1f);
        _lowerWindowMeshFilter.transform.localScale = new Vector3(scale, 1f, 1f);
    }

    public float MinimalWidth()
    {
        return _baseFrameWidth;
    }

    public float PreferredWidth()
    {
        return _baseTotalWidth;
    }

    public float BaseThickness => _baseThickness;
}
