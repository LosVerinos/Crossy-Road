using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class TerrainGenerator : MonoBehaviour
{
    [SerializeField] private int minDistanceFromPlayer;
    private int maxTerrainCount = 18;
    [SerializeField] private List<TerrainData> terrainData = new();
    [SerializeField] private Transform terrainHolder;
    [HideInInspector] public Vector3 currentPosition = new(0, 0, 0);
    private List<GameObject> _currentTerrains = new();
    private List<GameObject> _bufferTerrains = new();
    [SerializeField] private GameObject startTerrain;
    public float lastTerrainX;
    private int wasLilipadsTwoRowsAgo=2;
    private void Start()
    {

            SpawnInitialTerrain();
        
        for (var i=0; i< maxTerrainCount; i++){
            SpawnTerrain(true, new Vector3(0,0,0));
        }
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            SpawnTerrain(false, new Vector3(0,0,0));
        }
    }

    public void SpawnTerrain(bool isStart, Vector3 playerPos)
    {
        if(currentPosition.x - playerPos.x < minDistanceFromPlayer || isStart)
        {
            int wichTerrain;
            int successive;
            int lastOne = -1;
            do{
                wichTerrain = Random.Range(0, terrainData.Count);
            }while(terrainData[wichTerrain].probabilityOfSpawning < Random.Range(0f,1.0f));

            successive = Random.Range(1, terrainData[wichTerrain].maxSuccessive);
            
            for (int i=0; i< successive; i++)
            {
                int whichOne;
                if(terrainData[wichTerrain].name.StartsWith("Water")){
                    
                    do{
                        whichOne = Random.Range(0, terrainData[wichTerrain].PossibleTerrain.Count);
                    }while(whichOne == lastOne || (terrainData[wichTerrain].PossibleTerrain[whichOne].name.StartsWith("Lilipads") && wasLilipadsTwoRowsAgo < 2));
                    if(terrainData[wichTerrain].PossibleTerrain[whichOne].name.StartsWith("Lilipads")){
                            wasLilipadsTwoRowsAgo = 0;
                        }
                        else{
                            wasLilipadsTwoRowsAgo++;
                        }
                }
                else{
                    whichOne = Random.Range(0, terrainData[wichTerrain].PossibleTerrain.Count);
                    wasLilipadsTwoRowsAgo++;
                }
                lastOne = whichOne;
                GameObject newTerrain = Instantiate(terrainData[wichTerrain].PossibleTerrain[whichOne], currentPosition, Quaternion.identity, terrainHolder);
                _currentTerrains.Add(newTerrain);
                    if (!isStart)
                    {
                        if (_currentTerrains.Count > maxTerrainCount)
                        {
                            lastTerrainX = _currentTerrains[0].transform.position.x;
                            Destroy(_currentTerrains[0]);
                            _currentTerrains.RemoveAt(0);
                        }
                    }
                    currentPosition.x++;
            }
        }
        
    }


    private void SpawnInitialTerrain()
    {
        if (startTerrain != null)
        {
            GameObject newTerrain = Instantiate(startTerrain, new Vector3(-1, 0, 0), Quaternion.identity, terrainHolder);
            _currentTerrains.Add(newTerrain);
            currentPosition.x = 5;
        }
    }
}
