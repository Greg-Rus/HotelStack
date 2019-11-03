using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallResizer : AbstractResizer
{
    [SerializeField] private WallHalfResizer _leftHalf;
    [SerializeField] private WallHalfResizer _rightHalf;

    public override void SetSegmentWidth(float width)
    {
        _leftHalf.SetWidth(width * 0.5f);
        _leftHalf.transform.localPosition = Vector3.left * width * 0.5f;
        _rightHalf.SetWidth(width * 0.5f);
        _rightHalf.transform.localPosition = Vector3.right * width * 0.5f;
    }

    public override float MinimalWidth()
    {
        return 0;
    }

    public override float PreferredWidth()
    {
        return 1f;
    }

    public override float BaseThickness()
    {
        return _leftHalf.BaseThickness;
    }
}