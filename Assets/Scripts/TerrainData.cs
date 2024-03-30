using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TerrainData", menuName = "TerrainData")]
public class TerrainData : ScriptableObject
{
    public List<GameObject> PossibleTerrain;
    public int maxSuccessive;

    public float probabilityOfSpawning;
    // Start is called before the first frame update

}
