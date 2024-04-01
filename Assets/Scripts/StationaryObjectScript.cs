using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{

    private TerrainGenerator terrainGenerator;

    void Start(){
        terrainGenerator = FindObjectOfType<TerrainGenerator>();
    }

    void Update()
    {
        if(terrainGenerator.lastTerrainX >= transform.position.x){
            Destroy(gameObject);
        }
    }
}