using System.Collections.Generic;
using UnityEngine;

public class ElementsPlacement : MonoBehaviour
{
    [SerializeField] private List<GameObject> prefabsToPlace;
    [SerializeField] private float numberMax;
    [SerializeField] private Transform spawnPos;
    [SerializeField] private bool isRoadOrTrain;
    [SerializeField] private bool isLilypads;

    private int numberToPlace;
    private List<int> takenPlaces = new();
    private readonly int[] angles = { 0, 90, -90, 180 };
    private float probaOfCoinSpawn = 1.5f;
    private bool coinAlreadySpawned = false;

    private void Start()
    {
        InitializeParameters();
        PlaceLimitsElements();
        PlacePrefabs();
    }

    private void InitializeParameters()
    {
        numberToPlace = Mathf.RoundToInt(Random.Range(0, numberMax));
        if (isRoadOrTrain) probaOfCoinSpawn = 0.4f;
    }

    private void PlacePrefabs()
    {
        for (var i = 0; i < numberToPlace; i++)
        {
            int positionZ;
            do
            {
                positionZ = Mathf.RoundToInt(Random.Range(-9f, 9f));
            } while (positionZ == 0 || IsPositionOccupied(positionZ));

            takenPlaces.Add(positionZ);
            Vector3 spawnPosition = new Vector3(spawnPos.position.x, spawnPos.position.y, positionZ);

            GameObject prefabToPlace = GetRandomPrefab();
            if (ShouldSpawnCoin(prefabToPlace))
            {
                Instantiate(prefabToPlace, spawnPosition, Quaternion.identity);
                coinAlreadySpawned = true;
            }
            else if (!prefabToPlace.CompareTag("Coins"))
            {
                InstantiateAndRotatePrefab(prefabToPlace, spawnPosition);
            }
        }
    }

    private GameObject GetRandomPrefab()
    {
        int randomIndex = Random.Range(0, prefabsToPlace.Count);
        return prefabsToPlace[randomIndex];
    }

    private bool ShouldSpawnCoin(GameObject prefab)
    {
        return prefab.CompareTag("Coins") && Random.Range(0f, 1f) < probaOfCoinSpawn && !coinAlreadySpawned;
    }

    private void InstantiateAndRotatePrefab(GameObject prefab, Vector3 position)
    {
        GameObject instance = Instantiate(prefab, position, Quaternion.identity);
        int randomAngleIndex = Random.Range(0, angles.Length);
        instance.transform.Rotate(new Vector3(0, angles[randomAngleIndex], 0));
    }

    private bool IsPositionOccupied(int positionZ)
    {
        return takenPlaces.Contains(positionZ);
    }

    private void PlaceLimitsElements()
    {
        if (isRoadOrTrain || isLilypads) return;

        for (var z = 10; z < 20; z++)
        {
            PlaceLimitElement(z);
            PlaceLimitElement(-z);
        }
    }

    private void PlaceLimitElement(int z)
    {
        Vector3 spawnPosition = new Vector3(spawnPos.position.x, spawnPos.position.y, z);
        GameObject prefabToPlace = GetRandomPrefab();
        InstantiateAndRotatePrefab(prefabToPlace, spawnPosition);
    }

    public void ReloadComponents()
    {
        InitializeParameters();
        PlacePrefabs();
    }
}
