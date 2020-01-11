using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorSpawner : MonoBehaviour
{
    public FloorErector FloorPrefab;
    public float StartingSideLength;
    public Vector2 FloorDimensions;
    public float MovementSpeed;
    public FloorErector CurrentFloor;
    public int HotelHeight = 0;
    public float FloorSpawnOffset;
    public Vector3 FloorOrigin = Vector3.zero;
    public float SnapZoneSize;
    public Vector3 Destination;

    public MeshFilter CombinedMesh;

    public Vector3 CurrentDirection = Vector3.left;

    void Start()
    {
        FloorDimensions = new Vector2(StartingSideLength, StartingSideLength);
        CombinedMesh.mesh = new Mesh();
        SpawnFloor();
    }

    private void SpawnFloor()
    {
        CurrentFloor = Instantiate(FloorPrefab, Vector3.zero, Quaternion.identity);
        CurrentFloor.BuildFloor(FloorDimensions.x, FloorDimensions.y);
        CurrentFloor.transform.position = FloorOrigin + (CurrentDirection * FloorSpawnOffset * -1);
        Destination = FloorOrigin + CurrentDirection * FloorSpawnOffset;

        CombineMeshes();
    }

    private void CombineMeshes()
    {
        CombineInstance[] combine = new CombineInstance[CurrentFloor.Meshes.Count];

        int i = 0;
        while (i < CurrentFloor.Meshes.Count)
        {
            combine[i].mesh = CurrentFloor.Meshes[i].sharedMesh;
            combine[i].transform = CurrentFloor.Meshes[i].transform.localToWorldMatrix;
            CurrentFloor.Meshes[i].gameObject.SetActive(false);

            i++;
        }

        CombinedMesh.mesh = new Mesh();
        transform.GetComponent<MeshFilter>().mesh.CombineMeshes(combine);
    }

    void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    OnFloorDrop();
        //}
        //else
        //{
        //    MoveCurrentFloor();
        //}
    }

    private void MoveCurrentFloor()
    {
        CurrentFloor.transform.Translate(CurrentDirection * Time.smoothDeltaTime * MovementSpeed);
        var distanceFromOrigin = (FloorOrigin - CurrentFloor.transform.position).magnitude;
        if (distanceFromOrigin >= FloorSpawnOffset)
        {
            CurrentDirection *= -1;
        }
    }

    private void OnFloorDrop()
    {
        var restingPos = CurrentFloor.transform.position;
        var overhang = restingPos - FloorOrigin;

        if (overhang.magnitude < SnapZoneSize) SnapToOrigin();
        else SnipFloor(overhang);

        HotelHeight++;
        FloorOrigin += Vector3.up * 2; 

        SpawnNewFloor();
    }

    private void SnipFloor(Vector3 overhang)
    {
        if(overhang.x > 0) FloorOrigin = CurrentFloor.transform.position;
        FloorDimensions.x -= Math.Abs(overhang.x);
    }

    private void SpawnNewFloor()
    {
        CurrentDirection = CurrentDirection == Vector3.left ? Vector3.back : Vector3.left;
        SpawnFloor();
    }

    private void SnapToOrigin()
    {
        CurrentFloor.transform.position = FloorOrigin;
    }
}
