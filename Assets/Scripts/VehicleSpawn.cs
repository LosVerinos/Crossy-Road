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
    private float speed;

    // Start is called before the first frame update
    private void Start()
    {
        // Choix aléatoire d'un véhicule dans la liste
        int randomIndex = Random.Range(0, vehicles.Count);
        GameObject selectedVehicle = vehicles[randomIndex];
        MakePatern();
        SelectSpeed(selectedVehicle);
        StartCoroutine(SpawnVehicle(selectedVehicle));

    }


    private IEnumerator SpawnVehicle(GameObject selectedVehicle)
    {
        while (true)
        {
            yield return new WaitForSeconds(firstPart);
            InstantiateVehicle(selectedVehicle);
            yield return new WaitForSeconds(secondPart);
            InstantiateVehicle(selectedVehicle);
            yield return new WaitForSeconds(thirdPart);
            InstantiateVehicle(selectedVehicle);
        }
        
    }

    private void MakePatern(){
        firstPart = Random.Range(minSeparationTime, maxSeparationTime);
        secondPart = Random.Range(minSeparationTime, maxSeparationTime);
        thirdPart = Random.Range(minSeparationTime, maxSeparationTime);
    }

    private void InstantiateVehicle(GameObject selectedVehicle){
        GameObject newVehicule = Instantiate(selectedVehicle, SpawnPos.position, Quaternion.identity);
        MovingObjectScript vehicle = newVehicule.GetComponent<MovingObjectScript>();
        vehicle.SetSpeed(speed);
    }

    private void SelectSpeed(GameObject selectedVehicle){
        if (selectedVehicle.name.StartsWith("Cabriolet")){
            speed = 3.5f;  
        }
        
        if (selectedVehicle.name.StartsWith("F40")){
            speed = 7f;
        }
        if (selectedVehicle.name.StartsWith("RS6")){
            speed = 4f;
        }
        if (selectedVehicle.name.StartsWith("Truck")){
            speed = 2f;
        }
        if (selectedVehicle.name.StartsWith("Hummer")){
            speed = 3f;
        }
    }
}
