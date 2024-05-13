using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartTerrainScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private List<GameObject> prefabsToPlace;
    private int[] angles = { 0, 90, -90, 180 };

    private void Start()
    {
        PlacePrefabs();
    }

    private void PlacePrefabs()
    {
        for (var x = -11; x <= 5; x++)
            if (x == -6 || x == -7 || x == -8 || x == -9 || x == -10 || x == -11)
            {
                for (var z = -15; z <= 15; z++) Instantiate(x, z);
            }
            else if (x == 5)
            {
                for (var z = -15; z <= -4; z++) Instantiate(x, z);
                for (var z = 4; z <= 15; z++) Instantiate(x, z);
            }
            else
            {
                for (var z = -15; z <= -5; z++) Instantiate(x, z);
                for (var z = 5; z <= 15; z++) Instantiate(x, z);
            }

        Instantiate(6, 10);
        Instantiate(6, -10);
    }

    private void Instantiate(int x, int z)
    {
        var spawnPosition = new Vector3(x - 1, 0.5f, z);
        var randomPrefabIndex = Random.Range(0, prefabsToPlace.Count);
        var prefabToPlace = Instantiate(prefabsToPlace[randomPrefabIndex], spawnPosition, Quaternion.identity);
        var randomAngleIndex = Random.Range(0, angles.Length);
        prefabToPlace.transform.Rotate(new Vector3(0, angles[randomAngleIndex], 0));
    }
}