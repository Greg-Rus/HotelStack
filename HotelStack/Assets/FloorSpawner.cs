using System;
using MeshSplitting.Splitters;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FloorSpawner : MonoBehaviour
{
    public FloorErector FloorErector;
    public float StartingSideLength;
    public Vector2 FloorDimensions;
    public float MovementSpeed;
    public Floor CurrentFloor;
    public int HotelHeight = 0;
    public float FloorSpawnOffset;
    public Vector3 FloorOrigin = Vector3.zero;
    public float SnapZoneSize;
    public Vector3 Destination;

    public Vector3 CurrentDirection = Vector3.left;

    public SimpleSplitter Splitter;
    public Camera MainCamera;
    public Vector3 CameraOffset;

    public static FloorSpawner Instance { get; private set; }

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        FloorDimensions = new Vector2(StartingSideLength, StartingSideLength);
        SpawnFloor();
    }

    private void SpawnFloor()
    {
        CurrentFloor = FloorErector.BuildFloor(FloorDimensions.x, FloorDimensions.y);
        CurrentFloor.transform.position = FloorOrigin + (CurrentDirection * FloorSpawnOffset * -1);
        Destination = FloorOrigin + CurrentDirection * FloorSpawnOffset;

        UpdateCameraPosition();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnFloorDrop();
        }
        else
        {
            MoveCurrentFloor();
        }
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

        if (FloorDimensions.x <= 0 || FloorDimensions.y <= 0)
        {
            EndGame();
        }
        else
        {
            HotelHeight++;
            FloorOrigin += Vector3.up * 2;
            SpawnNewFloor();
        }
    }

    private void EndGame()
    {
        SceneManager.LoadScene(0);
    }

    private void UpdateCameraPosition()
    {
        MainCamera.transform.position = FloorOrigin + CameraOffset;
    }

    private void SnipFloor(Vector3 overhang)
    {
        if (overhang.x < 0 || overhang.z < 0)
        {
            Splitter.transform.position = FloorOrigin;
        }
        else
        {
            var splitterPosition = FloorOrigin + new Vector3(FloorDimensions.x, 0f, FloorDimensions.y);
            Splitter.transform.position = splitterPosition;
        }

        if (overhang.x > 0) Splitter.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 90f));
        else if (overhang.x < 0) Splitter.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, -90f));
        else if (overhang.z > 0) Splitter.transform.rotation = Quaternion.Euler(new Vector3(-90, 0f, 0f));
        else if (overhang.z < 0) Splitter.transform.rotation = Quaternion.Euler(new Vector3(90, 0f, 0f));

        if (overhang.x > 0 || overhang.z > 0) FloorOrigin = CurrentFloor.transform.position;
        FloorDimensions.x -= Math.Abs(overhang.x);
        FloorDimensions.y -= Math.Abs(overhang.z);

        Splitter.Floor = CurrentFloor.Splitable;
        Splitter.SplitObject();
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
