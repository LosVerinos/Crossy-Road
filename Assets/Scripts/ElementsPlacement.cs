using System.Collections;
using System.Collections.Generic;
using System.IO.Compression;
using UnityEngine;

public class ElementsPlacement : MonoBehaviour
{
    [SerializeField] private List<GameObject> prefabsToPlace;
    [SerializeField] private float numberMax;
    [SerializeField] private Transform spawnPos;
    private int numberToPlace;
    private List<int> takenPlaces = new();
    private int[] angles = { 0, 90, -90, 180 };
    [SerializeField] private bool isRoadOrTrain;
    [SerializeField] private bool isLilypads;
    private float probaOfCoinSpawn = 1.5f;
    private Vector3 spawnPosition;
    private int positionZ;
    private int randomPrefabIndex;
    private GameObject prefabToPlace;
    private int randomAngleIndex;
    private bool coinAlreadySpawned = false;

    private void Start()
    {
        if (!isRoadOrTrain && !isLilypads) PlaceLimitsElements();
        numberToPlace = Mathf.RoundToInt(Random.Range(0, numberMax));
        //Debug.Log(gameObject.name + numberToPlace);
        if (isRoadOrTrain) probaOfCoinSpawn = 0.4f;

        //Debug.Log(probaOfCoinSpawn);
        PlacePrefabs();
    }

    private void PlacePrefabs()
    {
        for (var i = 0; i < numberToPlace; i++)
        {
            do
            {
                positionZ = Mathf.RoundToInt(Random.Range(-9f, 9f));
            } while (positionZ == 0 || IsPositionOccupied(positionZ));

            takenPlaces.Add(positionZ);
            spawnPosition = new Vector3(spawnPos.position.x, spawnPos.position.y, positionZ);

            randomPrefabIndex = Random.Range(0, prefabsToPlace.Count);

            randomAngleIndex = Random.Range(0, angles.Length);

            //Debug.Log(prefabToPlace.CompareTag("Coins"));
            if (prefabsToPlace[randomPrefabIndex].CompareTag("Coins") && Random.Range(0f, 1f) < probaOfCoinSpawn &&
                !coinAlreadySpawned)
            {
                Instantiate(prefabsToPlace[randomPrefabIndex], spawnPosition, Quaternion.identity);
                coinAlreadySpawned = true;
            }
            else if ((prefabsToPlace[randomPrefabIndex].CompareTag("Coins") && isRoadOrTrain) || coinAlreadySpawned)
            {
                break;
            }
            else
            {
                prefabToPlace = Instantiate(prefabsToPlace[randomPrefabIndex], spawnPosition, Quaternion.identity);
                prefabToPlace.transform.Rotate(new Vector3(0, angles[randomAngleIndex], 0));
            }
        }
    }

    private bool IsPositionOccupied(int positionZ)
    {
        return takenPlaces.Contains(positionZ);
    }

    private void PlaceLimitsElements()
    {
        for (var z = 10; z < 20; z++)
        {
            spawnPosition = new Vector3(spawnPos.position.x, spawnPos.position.y, z);
            randomPrefabIndex = Random.Range(0, prefabsToPlace.Count - 1);
            randomAngleIndex = Random.Range(0, angles.Length);
            prefabToPlace = Instantiate(prefabsToPlace[randomPrefabIndex], spawnPosition, Quaternion.identity);
            prefabToPlace.transform.Rotate(new Vector3(0, angles[randomAngleIndex], 0));
            spawnPosition.z = -z;
            prefabToPlace = Instantiate(prefabsToPlace[randomPrefabIndex], spawnPosition, Quaternion.identity);
        }
    }

    public void reloadComponents()
    {
        numberToPlace = Mathf.RoundToInt(Random.Range(0, numberMax));
        //Debug.Log(gameObject.name + numberToPlace);
        if (isRoadOrTrain) probaOfCoinSpawn = 0.4f;
        //Debug.Log(probaOfCoinSpawn);
        PlacePrefabs();
    }
}