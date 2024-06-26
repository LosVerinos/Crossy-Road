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
        var randomIndex = Random.Range(0, vehicles.Count);
        var selectedVehicle = vehicles[randomIndex];
        SelectSpeed(selectedVehicle);
        MakePatern();

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

    private void MakePatern()
    {
        var midInterval = (minSeparationTime + maxSeparationTime) / 2;
        firstPart = Random.Range(minSeparationTime, maxSeparationTime);
        secondPart = Random.Range(minSeparationTime, maxSeparationTime);
        if (firstPart + secondPart >= midInterval * 1.5)
            thirdPart = Random.Range(minSeparationTime, midInterval);
        else if (firstPart + secondPart <= midInterval / 2f)
            thirdPart = Random.Range(midInterval, maxSeparationTime);
        else
            thirdPart = Random.Range(minSeparationTime, maxSeparationTime);
    }

    private void InstantiateVehicle(GameObject selectedVehicle)
    {
        var newVehicule = Instantiate(selectedVehicle, SpawnPos.position, Quaternion.identity);
        var vehicle = newVehicule.GetComponent<MovingObjectScript>();
        if (direction < 0) newVehicule.transform.Rotate(new Vector3(0, 180, 0));
        vehicle.SetDirection(direction);
        vehicle.SetSpeed(speed);
    }

    private void SelectSpeed(GameObject selectedVehicle)
    {
        //METTRE DES TAGS 
        if (selectedVehicle.CompareTag("LittleCar")) speed = Random.Range(2f, 3.5f);

        if (selectedVehicle.CompareTag("FastCar")) speed = Random.Range(5f, 6f);
        if (selectedVehicle.CompareTag("MidCar")) speed = Random.Range(2.5f, 4f);
        if (selectedVehicle.CompareTag("Truck"))
        {
            speed = Random.Range(1.5f, 2f);
            maxSeparationTime *= 1.5f;
            minSeparationTime += 1.0f;
        }

        if (selectedVehicle.CompareTag("BigCar")) speed = Random.Range(2f, 3f);
    }

    private void FirstVehicles(GameObject selectedVehicle)
    {
        float interval1;
        float interval2;
        float fullPattern = 0;

        var occurence = 0;
        do
        {
            occurence++;
            interval1 = fullPattern + speed * firstPart;
            interval2 = speed * secondPart + interval1;
            fullPattern = speed * thirdPart + interval2;
            var firstOne = Instantiate(selectedVehicle,
                new Vector3(SpawnPos.position.x, SpawnPos.position.y, interval1 * -direction), Quaternion.identity);
            var first = firstOne.GetComponent<MovingObjectScript>();

            if (direction < 0) firstOne.transform.Rotate(new Vector3(0, 180, 0));
            first.SetDirection(direction);
            first.SetSpeed(speed);
            var secondOne = Instantiate(selectedVehicle,
                new Vector3(SpawnPos.position.x, SpawnPos.position.y, interval2 * -direction), Quaternion.identity);
            var second = secondOne.GetComponent<MovingObjectScript>();

            if (direction < 0) secondOne.transform.Rotate(new Vector3(0, 180, 0));
            second.SetDirection(direction);
            second.SetSpeed(speed);
            var thirdOne = Instantiate(selectedVehicle,
                new Vector3(SpawnPos.position.x, SpawnPos.position.y, fullPattern * -direction), Quaternion.identity);
            var third = thirdOne.GetComponent<MovingObjectScript>();
            if (direction < 0) thirdOne.transform.Rotate(new Vector3(0, 180, 0));
            third.SetDirection(direction);
            third.SetSpeed(speed);
        } while (fullPattern < SpawnPos.position.z * -direction);

        fullPattern *= -direction;
        var exedent = fullPattern / occurence - (SpawnPos.position.z - fullPattern / occurence * (occurence - 1));
        timeToWait = exedent / speed;
        if (timeToWait < 0) timeToWait *= -1;
    }
}