using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementsPlacement : MonoBehaviour
{
    [SerializeField] private List<GameObject> prefabsToPlace;
    [SerializeField] private float numberMax;
    [SerializeField] private Transform spawnPos;
    [SerializeField] bool isRoadOrTrain;
    private int numberToPlace;
    private List<int> takenPlaces = new();
    private float probaOfCoinSpawn = 1.5f;

    private void Start()
    {
        numberToPlace = Mathf.RoundToInt(Random.Range(0, numberMax));
        Debug.Log(gameObject.name + numberToPlace);
        if(isRoadOrTrain){
            probaOfCoinSpawn = 0.4f;
        }
        Debug.Log(probaOfCoinSpawn);
        PlacePrefabs();
    }

    private void PlacePrefabs()
    {
        for (int i = 0; i < numberToPlace; i++)
        {
            int positionZ;
            int randomPrefabIndex;
            GameObject prefabToPlace;

            do
            {
                positionZ = Mathf.RoundToInt(Random.Range(-5f, 5f));
            } while (positionZ == 0 || IsPositionOccupied(positionZ));
            takenPlaces.Add(positionZ);
            Vector3 spawnPosition = new Vector3(spawnPos.position.x, spawnPos.position.y, positionZ);
            randomPrefabIndex = Random.Range(0, prefabsToPlace.Count);
            prefabToPlace = prefabsToPlace[randomPrefabIndex];
            Debug.Log(prefabToPlace.CompareTag("Coins"));
            if(prefabToPlace.CompareTag("Coins") && isRoadOrTrain && Random.Range(0f, 1f) < probaOfCoinSpawn){
                Instantiate(prefabToPlace, spawnPosition, Quaternion.identity);
            }
            else if(prefabToPlace.CompareTag("Coins") && isRoadOrTrain){
                break;
            }
            Instantiate(prefabToPlace, spawnPosition, Quaternion.identity);
        }
    }

    private bool IsPositionOccupied(int positionZ)
    {
        return takenPlaces.Contains(positionZ);
    }
}
