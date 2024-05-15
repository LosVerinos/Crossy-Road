using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleSpawn : MonoBehaviour
{
    [SerializeField] private List<GameObject> vehicles;
    [SerializeField] private Transform spawnPos;
    [SerializeField] private float minSeparationTime;
    [SerializeField] private float maxSeparationTime;
    [SerializeField] private float direction;

    private float firstPart;
    private float secondPart;
    private float thirdPart;
    private float speed;
    private float timeToWait;

    private void Start()
    {
        var randomIndex = Random.Range(0, vehicles.Count);
        var selectedVehicle = vehicles[randomIndex];
        SelectSpeed(selectedVehicle);
        MakePattern();

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

    private void MakePattern()
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
        var newVehicle = Instantiate(selectedVehicle, spawnPos.position, Quaternion.identity);
        var vehicleScript = newVehicle.GetComponent<MovingObjectScript>();
        if (direction < 0) newVehicle.transform.Rotate(new Vector3(0, 180, 0));
        vehicleScript.SetDirection(direction);
        vehicleScript.SetSpeed(speed);
    }

    private void SelectSpeed(GameObject selectedVehicle)
    {
        // Set the speed based on vehicle type
        switch (selectedVehicle.tag)
        {
            case "LittleCar":
                speed = Random.Range(2f, 3.5f);
                break;
            case "FastCar":
                speed = Random.Range(5f, 6f);
                break;
            case "MidCar":
                speed = Random.Range(2.5f, 4f);
                break;
            case "Truck":
                speed = Random.Range(1.5f, 2f);
                maxSeparationTime *= 1.5f;
                minSeparationTime += 1.0f;
                break;
            case "BigCar":
                speed = Random.Range(2f, 3f);
                break;
        }
    }

    private void FirstVehicles(GameObject selectedVehicle)
    {
        float interval1 = 0, interval2 = 0, fullPattern = 0;
        int occurrence = 0;

        while (fullPattern < spawnPos.position.z * -direction)
        {
            occurrence++;
            interval1 = fullPattern + speed * firstPart;
            interval2 = speed * secondPart + interval1;
            fullPattern = speed * thirdPart + interval2;

            InstantiateVehicleAtPosition(selectedVehicle, interval1);
            InstantiateVehicleAtPosition(selectedVehicle, interval2);
            InstantiateVehicleAtPosition(selectedVehicle, fullPattern);
        }

        fullPattern *= -direction;
        var excess = fullPattern / occurrence - (spawnPos.position.z - fullPattern / occurrence * (occurrence - 1));
        timeToWait = Mathf.Abs(excess / speed);
    }

    private void InstantiateVehicleAtPosition(GameObject selectedVehicle, float positionOffset)
    {
        var newVehicle = Instantiate(selectedVehicle, new Vector3(spawnPos.position.x, spawnPos.position.y, positionOffset * -direction), Quaternion.identity);
        var vehicleScript = newVehicle.GetComponent<MovingObjectScript>();

        if (direction < 0) newVehicle.transform.Rotate(new Vector3(0, 180, 0));
        vehicleScript.SetDirection(direction);
        vehicleScript.SetSpeed(speed);
    }
}
