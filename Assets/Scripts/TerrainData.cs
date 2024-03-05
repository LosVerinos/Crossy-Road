using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TerrainData", menuName = "TerrainData")]
public class TerrainData : ScriptableObject
{
    public GameObject terrain;
    public int maxSuccessive;
    // Start is called before the first frame update
}
