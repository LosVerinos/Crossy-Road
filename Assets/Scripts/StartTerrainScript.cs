using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartTerrainScript : MonoBehaviour
{
    [SerializeField] private List<GameObject> prefabsToPlace;
    private static readonly int[] Angles = { 0, 90, -90, 180 };

    private const int MinX = -11;
    private const int MaxX = 5;
    private const int MinZ = -15;
    private const int MaxZ = 15;

    private const float SpawnY = 0.5f;
    private const int SkipZoneStartX = -11;
    private const int SkipZoneEndX = -6;
    private const int SkipZoneEdgeX = 5;
    private const int SkipZoneStartZ = -4;
    private const int SkipZoneEndZ = 4;
    private const int ExtraObjectX = 6;
    private const int ExtraObjectZ = 10;

    private void Start()
    {
        PlacePrefabs();
    }

    private void PlacePrefabs()
    {
        for (var x = MinX; x <= MaxX; x++)
        {
            if (x <= SkipZoneEndX && x >= SkipZoneStartX)
            {
                PlaceInZRange(x, MinZ, MaxZ);
            }
            else if (x == SkipZoneEdgeX)
            {
                PlaceInZRange(x, MinZ, SkipZoneStartZ);
                PlaceInZRange(x, SkipZoneEndZ, MaxZ);
            }
            else
            {
                PlaceInZRange(x, MinZ, SkipZoneStartZ - 1);
                PlaceInZRange(x, SkipZoneEndZ + 1, MaxZ);
            }
        }

        InstantiateAtPosition(ExtraObjectX, ExtraObjectZ);
        InstantiateAtPosition(ExtraObjectX, -ExtraObjectZ);
    }

    private void PlaceInZRange(int x, int zStart, int zEnd)
    {
        for (var z = zStart; z <= zEnd; z++)
        {
            InstantiateAtPosition(x, z);
        }
    }

    private void InstantiateAtPosition(int x, int z)
    {
        var spawnPosition = new Vector3(x - 1, SpawnY, z);
        var randomPrefabIndex = Random.Range(0, prefabsToPlace.Count);
        var prefabToPlace = Instantiate(prefabsToPlace[randomPrefabIndex], spawnPosition, Quaternion.identity);
        var randomAngleIndex = Random.Range(0, Angles.Length);
        prefabToPlace.transform.Rotate(new Vector3(0, Angles[randomAngleIndex], 0));
    }
}
