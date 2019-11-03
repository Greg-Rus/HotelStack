using UnityEngine;

public class WindowResizer : AbstractResizer
{
    [SerializeField] private WindowHalfResizer _leftHalf;
    [SerializeField] private WindowHalfResizer _rightHalf;

    public override void SetSegmentWidth(float width)
    {
        _leftHalf.SetWidth(width * 0.5f);
        _leftHalf.transform.localPosition = Vector3.left * width * 0.5f;
        _rightHalf.SetWidth(width * 0.5f);
        _rightHalf.transform.localPosition = Vector3.right * width * 0.5f;
    }

    public override float MinimalWidth()
    {
        return _leftHalf.MinimalWidth() + _rightHalf.MinimalWidth();
    }

    public override float PreferredWidth()
    {
        return _leftHalf.PreferredWidth() + _rightHalf.PreferredWidth();
    }

    public override float BaseThickness()
    {
        return _leftHalf.BaseThickness;
    }
}
