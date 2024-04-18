using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartTerrainScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private List<GameObject> prefabsToPlace;
    void Start()
    {
        PlacePrefabs();
    }

    private void PlacePrefabs(){
        
        for(int x = -11; x <= 5; x++){
            Debug.Log("Plaed");
            if(x == -6 || x ==-7 || x == -8 || x == -9 || x ==-10 || x == -11 ){
                for(int z=-10; z<=10; z++){
                    Instantiate(x, z);
                }
            }
            else if(x == 5){
                for(int z=-10; z<=-4; z++){
                    Instantiate(x, z);
                }
                for(int z=4; z<=10; z++){
                    Instantiate(x, z);
                }
            }
            else{
                for(int z=-10; z<=-5; z++){
                    Instantiate(x, z);
                }
                for(int z=5; z<=10; z++){
                    Instantiate(x, z);
                }
            }

            
        }
    }

    private void Instantiate(int x, int z){
        Vector3 spawnPosition = new Vector3(x-1,0.5f,z);
        int randomPrefabIndex = Random.Range(0, prefabsToPlace.Count);
        GameObject prefabToPlace = prefabsToPlace[randomPrefabIndex];
        Instantiate(prefabToPlace, spawnPosition, Quaternion.identity);
    }
}
