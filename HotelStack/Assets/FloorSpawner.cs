using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorSpawner : MonoBehaviour
{
    public FloorErector FloorPrefab;
    public float StartingSideLength;
    //public float CurrentWidth;
    //public float CurrentDepth;
    public Vector2 FloorDimensions;
    public float MovementSpeed;
    public FloorErector CurrentFloor;
    public int HotelHeight = 0;
    public float FloorSpawnOffset;
    public Vector3 CurrentAxis = Vector3.left;
    public Vector3 FloorOrigin = Vector3.zero;
    public float SnapZoneSize;

    public Vector3 CurrentDirection = Vector3.left;
    // Start is called before the first frame update
    void Start()
    {
        FloorDimensions = new Vector2(StartingSideLength, StartingSideLength);
        SpawnFloor();
    }

    private void SpawnFloor()
    {
        CurrentFloor = Instantiate(FloorPrefab, Vector3.zero, Quaternion.identity);
        CurrentFloor.BuildFloor(FloorDimensions.x, FloorDimensions.y);
        CurrentFloor.transform.position = FloorOrigin + (Vector3.right * FloorSpawnOffset);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnFloorDrop();
        }
        else
        {
            CurrentFloor.transform.Translate(CurrentDirection * Time.smoothDeltaTime * MovementSpeed);
            if (Mathf.Abs(CurrentFloor.transform.position.x) >= FloorOrigin.x + FloorSpawnOffset)
            {
                CurrentDirection *= -1;
            }
        }
    }

    private void OnFloorDrop()
    {
        var restingPos = CurrentFloor.transform.position;
        var overhang = FloorOrigin.x - restingPos.x;
        if (Mathf.Abs(overhang) < SnapZoneSize) SnapToOrigin();
        else if(overhang < 0) SnipRight(overhang);
        else SnipLeft(overhang);

        HotelHeight++;
        FloorOrigin += Vector3.up * 2; 

        SpawnNewFloor();
    }

    private void SpawnNewFloor()
    {
        CurrentDirection = Vector3.left;
        SpawnFloor();
    }

    private void SnapToOrigin()
    {
        CurrentFloor.transform.position = FloorOrigin;
    }

    private void SnipLeft(float overhang)
    {
        FloorOrigin = CurrentFloor.transform.position;
        FloorDimensions.x -= Mathf.Abs(overhang);
    }

    private void SnipRight(float overhang)
    {
        FloorDimensions.x -= Mathf.Abs(overhang);
        //Just cut the floor. Origin remains.
    }
}
