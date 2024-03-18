using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
public class VehicleSpawn : MonoBehaviour
{
    [SerializeField] private List<GameObject> vehicles; // Liste des véhicules possibles
    [SerializeField] private Transform spawnPos;
    [SerializeField] private float minSeparationTime;
    [SerializeField] private float maxSeparationTime;

    // Start is called before the first frame update
    private void Start()
    {
        StartCoroutine(SpawnVehicle());
    }

    private IEnumerator SpawnVehicle()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minSeparationTime, maxSeparationTime));
            // Choix aléatoire d'un véhicule dans la liste
            int randomIndex = Random.Range(0, vehicles.Count);
            GameObject selectedVehicle = vehicles[randomIndex];
            // Instanciation du véhicule choisi à la position de spawn
            Instantiate(selectedVehicle, spawnPos.position, Quaternion.identity);
        }
    }
}*/


public class VehicleSpawn : MonoBehaviour
{
    [SerializeField] private List<GameObject> vehicles;
    [SerializeField] private Transform SpawnPos;
    [SerializeField] private float minSeparationTime;
    [SerializeField] private float maxSeparationTime;

    // Start is called before the first frame update
    private void Start()
    {
        // Choix aléatoire d'un véhicule dans la liste
        int randomIndex = Random.Range(0, vehicles.Count);
        GameObject selectedVehicle = vehicles[randomIndex];
        StartCoroutine(SpawnVehicle(selectedVehicle));
        
    }

    private IEnumerator SpawnVehicle(GameObject selectedVehicle)
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minSeparationTime, maxSeparationTime));
            Instantiate(selectedVehicle, SpawnPos.position, Quaternion.identity);
        }
        
    }

}
