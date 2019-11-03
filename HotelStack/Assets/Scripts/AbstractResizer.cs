using UnityEngine;

public abstract class AbstractResizer : MonoBehaviour
{
    public abstract void SetSegmentWidth(float width);
    public abstract float MinimalWidth();
    public abstract float PreferredWidth();
    public abstract float BaseThickness();
}