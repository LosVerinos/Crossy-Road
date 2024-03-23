using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class VehicleSpawn : MonoBehaviour
{
    [SerializeField] private List<GameObject> vehicles;
    [SerializeField] private Transform SpawnPos;
    [SerializeField] private float minSeparationTime;
    [SerializeField] private float maxSeparationTime;
    [SerializeField] private float direction;
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
        firstVehicles(selectedVehicle);

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
        newVehicule.transform.rotation = (Quaternion.Euler(0,0, 0));
        if (direction < 0)
        {
            newVehicule.transform.Rotate(new Vector3(0,180,0));
        }
        vehicle.SetDirection(direction);
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

    private void firstVehicles(GameObject selectedVehicle){
        float interval1 = speed * firstPart;
        float interval2 = speed * secondPart + interval1;
        float interval3 = speed * thirdPart + interval2;

        GameObject firstOne = Instantiate(selectedVehicle, new Vector3(SpawnPos.position.x, SpawnPos.position.y, interval1*-direction), Quaternion.identity);
        MovingObjectScript first = firstOne.GetComponent<MovingObjectScript>();
        firstOne.transform.rotation = (Quaternion.Euler(0,0, 0));
        if (direction < 0)
        {
            firstOne.transform.Rotate(new Vector3(0,180,0));
        }
        first.SetDirection(direction);
        first.SetSpeed(speed);

        GameObject secondOne = Instantiate(selectedVehicle, new Vector3(SpawnPos.position.x, SpawnPos.position.y, interval2*-direction), Quaternion.identity);
        MovingObjectScript second = secondOne.GetComponent<MovingObjectScript>();
        secondOne.transform.rotation = (Quaternion.Euler(0,0, 0));
        if (direction < 0)
        {
            secondOne.transform.Rotate(new Vector3(0,180,0));
        }
        second.SetDirection(direction);
        second.SetSpeed(speed);

        GameObject thirdOne = Instantiate(selectedVehicle, new Vector3(SpawnPos.position.x, SpawnPos.position.y, interval3*-direction), Quaternion.identity);
        MovingObjectScript third = thirdOne.GetComponent<MovingObjectScript>();
        thirdOne.transform.rotation = (Quaternion.Euler(0,0, 0));
        if (direction < 0)
        {
            thirdOne.transform.Rotate(new Vector3(0,180,0));

        }
        third.SetDirection(direction);    
        third.SetSpeed(speed);
        
    }
}
