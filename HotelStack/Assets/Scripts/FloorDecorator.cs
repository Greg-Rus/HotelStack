using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(FloorDecorator))]
public class FloorDecoratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        var t = (FloorDecorator)target;
        if (GUILayout.Button("Decorate Floor"))
        {
            t.DecorateAlongWall(t.TestFloor);
        }
    }
}

public class FloorDecorator : MonoBehaviour
{
    public GameObject RoofPrefab;
    public FurnitureConfig Config;
    public GameObject TestFloor;

    private static FloorDecorator _instance;

    public static FloorDecorator Instance => _instance;

    // Start is called before the first frame update
    void Awake()
    {
        _instance = this;
    }

    public void AddRoof(GameObject floor, Vector3 splitNormal, Bounds bounds)
    {
        var center = FloorSpawner.Instance.FloorOrigin +
                     new Vector3(
                         FloorSpawner.Instance.FloorDimensions.x * 0.5f,
                         -0.005f,
                         FloorSpawner.Instance.FloorDimensions.y * 0.5f);

        var roofGo = Instantiate(RoofPrefab, center, floor.transform.rotation);
        roofGo.transform.SetParent(floor.transform, true);
        roofGo.transform.localScale = new Vector3(
            FloorSpawner.Instance.FloorDimensions.x,
            0.01f,
            FloorSpawner.Instance.FloorDimensions.y);
    }

    public void DecorateAlongWall(GameObject floor)
    {

        var lengthX = FloorSpawner.Instance.FloorDimensions.x;
        if (lengthX < 1) return;

        var usedBudget = 0;

        while (usedBudget < lengthX - 1)
        {
            var legalFurniture = Config.FurnitureEntities.Where(entity => entity.xSize <= lengthX - usedBudget).ToList().Random();

            var furniture = Instantiate(legalFurniture.Prefab);

            furniture.transform.SetParent(floor.transform, false);
            furniture.transform.Translate(new Vector3(legalFurniture.xSize * 0.5f + usedBudget, 0f, 0f));
            furniture.transform.Rotate(Vector3.up, 180);

            usedBudget += legalFurniture.xSize;
        }
    }
}