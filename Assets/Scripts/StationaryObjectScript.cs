using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    private TerrainGenerator terrainGenerator;

    private void Start()
    {
        terrainGenerator = FindObjectOfType<TerrainGenerator>();
    }

    private void Update()
    {
        if (GlobalVariables.reload) Destroy(gameObject);


        if (terrainGenerator.lastTerrainX > 5 && terrainGenerator.lastTerrainX >= transform.position.x)
            Destroy(gameObject);
    }
}