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
    private float timeToWait;

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
        FirstVehicles(selectedVehicle);

        yield return new WaitForSeconds(timeToWait);
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
        float midInterval = (minSeparationTime + maxSeparationTime)/2;
        firstPart = Random.Range(minSeparationTime, maxSeparationTime);
        secondPart = Random.Range(minSeparationTime, maxSeparationTime);
        if(firstPart+secondPart >= midInterval*1.5){
            thirdPart = Random.Range(minSeparationTime, midInterval);
        }
        else if(firstPart+secondPart <= midInterval/2f){
            thirdPart = Random.Range(midInterval, maxSeparationTime);
        }
        else{
             thirdPart = Random.Range(minSeparationTime, maxSeparationTime);
        }
    }

    private void InstantiateVehicle(GameObject selectedVehicle){
        GameObject newVehicule = Instantiate(selectedVehicle, SpawnPos.position, Quaternion.identity);
        MovingObjectScript vehicle = newVehicule.GetComponent<MovingObjectScript>();
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

    private void FirstVehicles(GameObject selectedVehicle){
        float interval1;
        float interval2;
        float fullPattern = 0;
 
        int occurence = 0;
        do{
            occurence++;
            interval1 = fullPattern + speed * firstPart;
            interval2 = speed * secondPart + interval1;
            fullPattern = speed * thirdPart + interval2 ;
            GameObject firstOne = Instantiate(selectedVehicle, new Vector3(SpawnPos.position.x, SpawnPos.position.y, interval1 * -direction), Quaternion.identity);
            MovingObjectScript first = firstOne.GetComponent<MovingObjectScript>();

            if (direction < 0)
            {
                firstOne.transform.Rotate(new Vector3(0,180,0));
            }
            first.SetDirection(direction);
            first.SetSpeed(speed);
            GameObject secondOne = Instantiate(selectedVehicle, new Vector3(SpawnPos.position.x, SpawnPos.position.y, interval2 * -direction), Quaternion.identity);
            MovingObjectScript second = secondOne.GetComponent<MovingObjectScript>();

            if (direction < 0)
            {
                secondOne.transform.Rotate(new Vector3(0,180,0));
            }
            second.SetDirection(direction);
            second.SetSpeed(speed);
            GameObject thirdOne = Instantiate(selectedVehicle, new Vector3(SpawnPos.position.x, SpawnPos.position.y, fullPattern * -direction), Quaternion.identity);
            MovingObjectScript third = thirdOne.GetComponent<MovingObjectScript>();
            if (direction < 0)
            {
                thirdOne.transform.Rotate(new Vector3(0,180,0));

            }
            third.SetDirection(direction);    
            third.SetSpeed(speed);
            
        }while(fullPattern < SpawnPos.position.z * -direction);

        fullPattern *= -direction;
        float exedent = (fullPattern / occurence) - (SpawnPos.position.z - (fullPattern / occurence * (occurence - 1)));
        timeToWait = exedent/speed;
        if(timeToWait < 0){
            timeToWait *= -1;
        }

    }
}