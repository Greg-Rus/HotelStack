using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorDecorator : MonoBehaviour
{
    public GameObject RoofPrefab;

    private static FloorDecorator _instance;

    public static FloorDecorator Instance => _instance;

    // Start is called before the first frame update
    void Awake()
    {
        _instance = this;
    }

    public void AddRoof(GameObject floor, Vector3 splitNormal, Bounds bounds)
    {
        var center = bounds.center;
        var roofGo = Instantiate(RoofPrefab, center, floor.transform.rotation);
        roofGo.transform.SetParent(floor.transform, false);
        roofGo.transform.Translate(Vector3.up * (1f - 0.005f));
        roofGo.transform.localScale = new Vector3(FloorSpawner.Instance.FloorDimensions.x, 0.01f, FloorSpawner.Instance.FloorDimensions.y);
    }
}
