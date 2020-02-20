using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FurnitureConfig", menuName = "ScriptableObjects/FurnitureConfig")]
public class FurnitureConfig : ScriptableObject
{
    public FurnitureEntity[] FurnitureEntities;
}

[Serializable]
public class FurnitureEntity
{
    public GameObject Prefab;
    public int xSize;
    public int ySize;
}
