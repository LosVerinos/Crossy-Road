using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class TerrainGenerator : MonoBehaviour
{
    // TODO: Create a buffer of terrains, at Instantiation, fill the buffer with 5, 3, 4 terrains. If the buffer contains terrains, push the terrain to the end of the list, and remove the first terrain from the list.
    // else, create a new batch of terrain and add it to the buffer.
    [FormerlySerializedAs("_maxTerrainCount")] [SerializeField] private int maxTerrainCount;
    [SerializeField] private List<TerrainData> terrainData = new();
    [SerializeField] private Transform terrainHolder;
    private Vector3 _currentPosition = new(0, 0, 0);

    private List<GameObject> _currentTerrains = new();


    private void Start()
    {
        for (var i=0; i< maxTerrainCount; i++){
            SpawnTerrain(true);
        }
        maxTerrainCount = _currentTerrains.Count;
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            SpawnTerrain(false);
        }
    }

    private void SpawnTerrain(bool isStart)
    {
        int wichTerrain = Random.Range(0, terrainData.Count);
        int successive = Random.Range(1, terrainData[wichTerrain].maxSuccessive);
        for (int i=0; i< successive; i++)
        {
            GameObject newTerrain = Instantiate(terrainData[wichTerrain].terrain, _currentPosition, Quaternion.identity, terrainHolder);
            _currentTerrains.Add(newTerrain);
            if (!isStart)
            {
                if (_currentTerrains.Count > maxTerrainCount)
                {
                    Destroy(_currentTerrains[0]);
                    _currentTerrains.RemoveAt(0);
                }
            }
            _currentPosition.x++;
        }
    }
}
