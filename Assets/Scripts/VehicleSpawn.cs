using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class VehicleSpawn : MonoBehaviour
{
    [SerializeField] private List<GameObject> vehicles;
    [SerializeField] private Transform SpawnPos;
    [SerializeField] private float minSeparationTime;
    [SerializeField] private float maxSeparationTime;
    private float firstPart;
    private float secondPart;
    private float thirdPart;

    // Start is called before the first frame update
    private void Start()
    {
        // Choix aléatoire d'un véhicule dans la liste
        int randomIndex = Random.Range(0, vehicles.Count);
        GameObject selectedVehicle = vehicles[randomIndex];
        MakePatern();
        StartCoroutine(SpawnVehicle(selectedVehicle));
        
    }

    private IEnumerator SpawnVehicle(GameObject selectedVehicle)
    {
        while (true)
        {
            yield return new WaitForSeconds(firstPart);
            Instantiate(selectedVehicle, SpawnPos.position, Quaternion.identity);
            yield return new WaitForSeconds(secondPart);
            Instantiate(selectedVehicle, SpawnPos.position, Quaternion.identity);
            yield return new WaitForSeconds(thirdPart);
            Instantiate(selectedVehicle, SpawnPos.position, Quaternion.identity);
        }
        
    }

    private void MakePatern(){
        firstPart = Random.Range(minSeparationTime, maxSeparationTime);
        secondPart = Random.Range(minSeparationTime, maxSeparationTime);
        thirdPart = Random.Range(minSeparationTime, maxSeparationTime);
    }

}
