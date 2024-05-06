using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class TerrainGenerator : MonoBehaviour
{
    [SerializeField] private int minDistanceFromPlayer;
    private int maxTerrainCount = 20;
    private List<TerrainData> terrainData = new();
    [SerializeField] private List<TerrainData> terrainsNormal = new();
    [SerializeField] private List<TerrainData> terrainsStarWars = new();
    [SerializeField] private List<TerrainData> terrainsHarryPotter = new();
    [SerializeField] private List<TerrainData> terrainsLOTR = new();
    [SerializeField] private Transform terrainHolder;
    [HideInInspector] public Vector3 currentPosition = new(0, 0, 0);
    private List<GameObject> _currentTerrains = new();
    [SerializeField] private List<GameObject> startTerrains = new();
    private GameObject startTerrain;
    private GameObject lastTerrain;
    public float lastTerrainX;
    private int wasLilipadsTwoRowsAgo=2;
    public bool isStart;

    private void Start()
    {

        //PlayerPrefs.SetInt("Coins", 1000);
        //PlayerPrefs.Save();

        if (isStart)
        {
            ThemeDetermination();
            SpawnInitialTerrain();

            for (var i = 0; i < maxTerrainCount; i++)
            {
                SpawnTerrain(true, new Vector3(0, 0, 0));
            }
        }
    }


    private void Update()
    {
        if (isStart)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                SpawnTerrain(false, new Vector3(0, 0, 0));
            }
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

            for (int i=0; i < successive; i++)
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

                    if(GlobalVariables.theme == "StarWars" && currentPosition.x > 6){
                            Transform southCliff = terrainData[wichTerrain].PossibleTerrain[whichOne].transform.Find("cliff-South.vox");
                            southCliff.gameObject.SetActive(true);
                            
                            if(lastTerrain.transform.name.StartsWith("Water") || lastTerrain.transform.name.StartsWith("Lilipads"))
                            {
                                southCliff.gameObject.SetActive(false); 
                            }
                        }
                }
                else{
                    whichOne = Random.Range(0, terrainData[wichTerrain].PossibleTerrain.Count);
                    if(GlobalVariables.theme == "StarWars" && currentPosition.x > 6){
                        Transform northCliff = terrainData[wichTerrain].PossibleTerrain[whichOne].transform.Find("Cliff");
                            if(northCliff != null){
                                northCliff.gameObject.SetActive(false);
                                if(lastTerrain.transform.name.StartsWith("Water") || lastTerrain.transform.name.StartsWith("Lilipads")){
                                    if (northCliff != null)
                                    {
                                        northCliff.gameObject.SetActive(true);
                                    }
                                }
                            }
                    }
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
                            Debug.Log("real delete : " + _currentTerrains[0]);
                            
                            Destroy(_currentTerrains[0]);
                            _currentTerrains.RemoveAt(0);
                        }
                    }
                currentPosition.x++;
                lastTerrain = terrainData[wichTerrain].PossibleTerrain[whichOne];
            }
            

        }
        
    }


    private void SpawnInitialTerrain(){
        if (startTerrain != null)
        {
            GameObject newTerrain = Instantiate(startTerrain, new Vector3(-1, 0, 0), Quaternion.identity, terrainHolder);
            _currentTerrains.Add(newTerrain);
            currentPosition.x = 6;
        }
    }

    private void ThemeDetermination(){
        if(GlobalVariables.theme == "StarWars"){
            Debug.Log("terrain determined is StarWars");
            terrainData = terrainsStarWars;
            startTerrain = startTerrains[1];

        }
        else if(GlobalVariables.theme == "HarryPotter"){
            Debug.Log("terrain determined is HarryPotter");
            terrainData = terrainsHarryPotter;
            startTerrain = startTerrains[0];
            LightController lightController = FindObjectOfType<LightController>();

            if (lightController != null)
            {
                // Appelle ChangeLightIntensity avec une intensité de 2.0
                lightController.ChangeLightIntensity(0f);
            }
            else
            {
                Debug.LogError("Script LightController non trouvé.");
            }
            }
        else if(GlobalVariables.theme == "LOTR"){
            Debug.Log("terrain determined is LOTR");
            terrainData = terrainsLOTR;
            startTerrain = startTerrains[2];
        }
        else{
            Debug.Log("terrain determined is natural");
            terrainData = terrainsNormal;
            startTerrain = startTerrains[0];

        }
    }


    public void destroyAll()
    {
        GlobalVariables.reload = true;

        foreach(GameObject terrain in _currentTerrains)
        {
            Destroy(terrain);
        }

        _currentTerrains.Clear();

        Invoke("NoReload",0.02f);

        Invoke("Reloadterrain",0.03f);

    }

    void NoReload()
    {
        GlobalVariables.reload = false;
    }

    private void Reloadterrain()
    {
        ThemeDetermination();

        currentPosition = Vector3.zero;

        SpawnInitialTerrain();

        for (int i = 0; i < maxTerrainCount; i++)
        {
            SpawnTerrain(true, Vector3.zero);
        }

    }
}



