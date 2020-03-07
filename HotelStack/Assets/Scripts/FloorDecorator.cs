using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(FloorDecorator))]
public class FloorDecoratorEditor : Editor
{

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

    public void DecorateAlongWall(FurnishedFloor floor)
    {
        var motionAxis = FloorSpawner.Instance.CurrentDirection;
        //FurnishAlongX(floor, motionAxis.z > 0);
        //FurnishAlongY(floor, motionAxis.x > 0);
        FurnishAlongWall(floor, motionAxis.z > 0, FloorSpawner.Instance.FloorDimensions.x - 1, Vector3.right, 180, Vector3.zero);
        FurnishAlongWall(floor, motionAxis.z > 0, FloorSpawner.Instance.FloorDimensions.x - 1, Vector3.right, 0, Vector3.forward * FloorSpawner.Instance.FloorDimensions.y);
        FurnishAlongWall(floor, motionAxis.x > 0, FloorSpawner.Instance.FloorDimensions.y - 1, Vector3.forward, -90, Vector3.zero);
        FurnishAlongWall(floor, motionAxis.x > 0, FloorSpawner.Instance.FloorDimensions.y - 1, Vector3.forward, 90, Vector3.right * FloorSpawner.Instance.FloorDimensions.x);
    }

    //private void FurnishAlongX(FurnishedFloor floor, bool isParallelToMotion)
    //{
    //    var lengthX = FloorSpawner.Instance.FloorDimensions.x - 1;
    //    if (lengthX < 1) return;

    //    var usedBudget = 0;

    //    while (usedBudget < lengthX)
    //    {
    //        var legalFurniture = Config.FurnitureEntities.Where(entity => entity.xSize <= lengthX - usedBudget).ToList()
    //            .Random();

    //        var furniture = Instantiate(legalFurniture.Prefab);

    //        furniture.transform.SetParent(floor.DecorationsParent, false);
    //        furniture.transform.Translate(new Vector3(legalFurniture.xSize * 0.5f + usedBudget, 0f, 0f));
    //        furniture.transform.Rotate(Vector3.up, 180);

    //        usedBudget += legalFurniture.xSize;
    //    }
    //}

    //private void FurnishAlongY(FurnishedFloor floor, bool isParallelToMotion)
    //{
    //    var lengthY = FloorSpawner.Instance.FloorDimensions.y - 1;
    //    if (lengthY < 1) return;

    //    var usedBudget = 0;

    //    while (usedBudget < lengthY)
    //    {
    //        var legalFurniture = Config.FurnitureEntities.Where(entity => entity.xSize <= lengthY - usedBudget).ToList()
    //            .Random();

    //        var furniture = Instantiate(legalFurniture.Prefab);

    //        furniture.transform.SetParent(floor.DecorationsParent, false);
    //        furniture.transform.Translate(new Vector3(0f, 0f, legalFurniture.xSize * 0.5f + usedBudget));
    //        furniture.transform.Rotate(Vector3.up, -90);

    //        usedBudget += legalFurniture.xSize;
    //    }
    //}

    private void FurnishAlongWall(FurnishedFloor floor, bool isParallelToMotion, float wallLength, Vector3 translationDirection, 
        float roation, Vector3 translationOffset)
    {
        if (wallLength <= 1) return;

        var usedBudget = 0;

        while (usedBudget < wallLength)
        {
            var legalFurniture = Config.FurnitureEntities.Where(entity => entity.xSize <= wallLength - usedBudget).ToList()
                .Random();

            var furniture = Instantiate(legalFurniture.Prefab);

            furniture.transform.SetParent(floor.DecorationsParent, false);
            furniture.transform.Translate((translationDirection * (legalFurniture.xSize * 0.5f + usedBudget)) + translationOffset);
            furniture.transform.Rotate(Vector3.up, roation);

            usedBudget += legalFurniture.xSize;
        }
    }
}