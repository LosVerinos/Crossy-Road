using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    [SerializeField] private int minDistanceFromPlayer;
    [SerializeField] private int maxTerrainCount = 20;
    [SerializeField] private List<TerrainData> terrainsNormal = new();
    [SerializeField] private List<TerrainData> terrainsStarWars = new();
    [SerializeField] private List<TerrainData> terrainsHarryPotter = new();
    [SerializeField] private List<TerrainData> terrainsLOTR = new();
    [SerializeField] private Transform terrainHolder;
    [SerializeField] private List<GameObject> startTerrains = new();
    [SerializeField] private bool isStart;

    private List<TerrainData> terrainData = new();
    public List<GameObject> _currentTerrains = new();
    private Vector3 currentPosition = new(0, 0, 0);
    private GameObject startTerrain;
    private GameObject lastTerrain;
    private int wasLilipadsTwoRowsAgo = 2;
    public float lastTerrainX;

    private void Start()
    {
        if (isStart)
        {
            ThemeDetermination();
            SpawnInitialTerrain();

            for (var i = 0; i < maxTerrainCount; i++)
            {
                SpawnTerrain(true, Vector3.zero);
            }
        }
    }

    private void Update()
    {
        if (isStart && Input.GetKeyDown(KeyCode.W))
        {
            SpawnTerrain(false, Vector3.zero);
        }
    }

    private void ThemeDetermination()
    {
        switch (GlobalVariables.theme)
        {
            case "StarWars":
                terrainData = terrainsStarWars;
                startTerrain = startTerrains[1];
                Debug.Log("Terrain theme: StarWars");
                break;
            case "HarryPotter":
                terrainData = terrainsHarryPotter;
                startTerrain = startTerrains[0];
                FindObjectOfType<LightController>()?.ChangeLightIntensity(0f);
                Debug.Log("Terrain theme: HarryPotter");
                break;
            case "LOTR":
                terrainData = terrainsLOTR;
                startTerrain = startTerrains[2];
                Debug.Log("Terrain theme: LOTR");
                break;
            default:
                terrainData = terrainsNormal;
                startTerrain = startTerrains[0];
                Debug.Log("Terrain theme: Natural");
                break;
        }
    }

    private void SpawnInitialTerrain()
    {
        if (startTerrain != null)
        {
            var newTerrain = Instantiate(startTerrain, new Vector3(-1, 0, 0), Quaternion.identity, terrainHolder);
            _currentTerrains.Add(newTerrain);
            currentPosition.x = 6;
        }
    }

    public void SpawnTerrain(bool isStart, Vector3 playerPos)
    {
        if (currentPosition.x - playerPos.x >= minDistanceFromPlayer && !isStart) return;

        int wichTerrain;
        do
        {
            wichTerrain = Random.Range(0, terrainData.Count);
        } while (terrainData[wichTerrain].probabilityOfSpawning < Random.Range(0f, 1.0f));

        int successive = Random.Range(1, terrainData[wichTerrain].maxSuccessive);
        for (var i = 0; i < successive; i++)
        {
            int whichOne = SelectTerrain(wichTerrain);
            InstantiateTerrain(wichTerrain, whichOne);
            if (!isStart && _currentTerrains.Count > maxTerrainCount)
            {
                lastTerrainX = _currentTerrains[0].transform.position.x;
                Destroy(_currentTerrains[0]);
                _currentTerrains.RemoveAt(0);
            }
            currentPosition.x++;
        }
    }

    private int SelectTerrain(int wichTerrain)
    {
        int whichOne;
        if (terrainData[wichTerrain].name.StartsWith("Water"))
        {
            do
            {
                whichOne = Random.Range(0, terrainData[wichTerrain].PossibleTerrain.Count);
            } while (whichOne == wasLilipadsTwoRowsAgo ||
                     (terrainData[wichTerrain].PossibleTerrain[whichOne].name.StartsWith("Lilipads") &&
                      wasLilipadsTwoRowsAgo < 2));

            wasLilipadsTwoRowsAgo = terrainData[wichTerrain].PossibleTerrain[whichOne].name.StartsWith("Lilipads") ? 0 : wasLilipadsTwoRowsAgo + 1;
        }
        else
        {
            whichOne = Random.Range(0, terrainData[wichTerrain].PossibleTerrain.Count);
            wasLilipadsTwoRowsAgo++;
        }
        return whichOne;
    }

    private void InstantiateTerrain(int wichTerrain, int whichOne)
    {
        var newTerrain = Instantiate(terrainData[wichTerrain].PossibleTerrain[whichOne], currentPosition,
            Quaternion.identity, terrainHolder);
        _currentTerrains.Add(newTerrain);
        if (GlobalVariables.theme == "StarWars" && currentPosition.x > 6)
        {
            SetCliffs(newTerrain);
        }
        lastTerrain = terrainData[wichTerrain].PossibleTerrain[whichOne];
    }

    private void SetCliffs(GameObject newTerrain)
    {
        var cliffSouth = newTerrain.transform.Find("cliff-South.vox");
        if (cliffSouth != null)
        {
            cliffSouth.gameObject.SetActive(!(lastTerrain.transform.name.StartsWith("Water") ||
                                              lastTerrain.transform.name.StartsWith("Lilipads")));
        }

        var cliffNorth = newTerrain.transform.Find("Cliff");
        if (cliffNorth != null)
        {
            cliffNorth.gameObject.SetActive(lastTerrain.transform.name.StartsWith("Water") ||
                                            lastTerrain.transform.name.StartsWith("Lilipads"));
        }
    }

    public void destroyAll()
    {
        GlobalVariables.reload = true;

        foreach (var terrain in _currentTerrains) Destroy(terrain);

        _currentTerrains.Clear();

        Invoke(nameof(NoReload), 0.02f);
        Invoke(nameof(ReloadTerrain), 0.03f);
    }

    private void NoReload()
    {
        GlobalVariables.reload = false;
    }

    private void ReloadTerrain()
    {
        ThemeDetermination();
        currentPosition = Vector3.zero;
        SpawnInitialTerrain();

        for (var i = 0; i < maxTerrainCount; i++) SpawnTerrain(true, Vector3.zero);
    }

    public List<GameObject> GetCurrentTerrains()
    {
        return _currentTerrains;
    }
}
