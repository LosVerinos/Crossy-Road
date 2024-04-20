using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementsPlacement : MonoBehaviour
{
    [SerializeField] private List<GameObject> prefabsToPlace;
    [SerializeField] private int numberMax;
    [SerializeField] private Transform spawnPos;
    private int numberToPlace;
    private List<int> takenPlaces = new();
    private int[] angles = { 0, 90, -90, 180 };

    private void Start()
    {
        numberToPlace = Mathf.RoundToInt(Random.Range(0, numberMax));
        PlacePrefabs();
    }

    private void PlacePrefabs()
    {
        for (int i = 0; i < numberToPlace; i++)
        {
            int positionZ;
            do
            {
                positionZ = Mathf.RoundToInt(Random.Range(-5f, 5f));
            } while (positionZ == 0 || IsPositionOccupied(positionZ));
            takenPlaces.Add(positionZ);
            Vector3 spawnPosition = new Vector3(spawnPos.position.x, spawnPos.position.y, positionZ);
            int randomPrefabIndex = Random.Range(0, prefabsToPlace.Count);
            
            GameObject prefabToPlace = Instantiate(prefabsToPlace[randomPrefabIndex], spawnPosition, Quaternion.identity);

            int randomAngleIndex = Random.Range(0, angles.Length);
            prefabToPlace.transform.Rotate(new Vector3(0,angles[randomAngleIndex],0));
        }
    }

    private bool IsPositionOccupied(int positionZ)
    {
        return takenPlaces.Contains(positionZ);
    }
}
