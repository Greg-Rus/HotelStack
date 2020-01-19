using System.Linq;
using UnityEngine;

public class FloorSideBuilder : MonoBehaviour
{
    public AbstractResizer[] WallSegments;
    public AbstractResizer[] WindowSegments;

    public float Length;



    public void BuildFloorSide(float totalWidth)
    {
        var midPoint = totalWidth * 0.5f;
        var windowBudget = midPoint;
        
        if (windowBudget < SmallestWindowPrefab.MinimalWidth())
        {
            SpawnSegment(WallPrefab, Vector3.right * midPoint, totalWidth);
            return;
        }
        else if (windowBudget <= SmallestWindowPrefab.PreferredWidth())
        {
            SpawnSegment(WindowPrefab, Vector3.right * midPoint, totalWidth);
            return;
        }
        else
        {
            var windowSegment = WindowPrefab;
            var wallSegment = WallPrefab;
            var windowCount = (int)(windowBudget / windowSegment.PreferredWidth());
            var wallBudget = totalWidth - windowCount * windowSegment.PreferredWidth();
            var wallCount = windowCount + 1;
            var wallWidth = wallBudget / wallCount;

            var progress = 0f;
            for (int i = 0; i < windowCount + wallCount; i++)
            {
                if (i % 2 == 0)
                {
                    var position = Vector3.right * (progress + wallWidth * 0.5f);
                    SpawnSegment(wallSegment, position, wallWidth);
                    progress += wallWidth;
                }
                else
                {
                    var position = Vector3.right * (progress + windowSegment.PreferredWidth() * 0.5f);
                    SpawnSegment(windowSegment, position, windowSegment.PreferredWidth());
                    progress += windowSegment.PreferredWidth();
                }   
            }
        }
    }

    private AbstractResizer SmallestWindowPrefab => WindowSegments.First();
    private AbstractResizer WindowPrefab => WindowSegments.First();
    private AbstractResizer WallPrefab => WallSegments.First();


    private void SpawnSegment(AbstractResizer segmentPrefab, Vector3 position, float width)
    {
        var segment = Instantiate(segmentPrefab, position, Quaternion.identity);
        segment.SetSegmentWidth(width);
        segment.transform.SetParent(transform,false);
    }

    public float WallThickness => WallPrefab.BaseThickness();
}
