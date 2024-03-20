using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainSpawn : MonoBehaviour
{
    [SerializeField] private GameObject vehicle;
    [SerializeField] private Transform SpawnPos;
    [SerializeField] private float minSeparationTime;
    [SerializeField] private float maxSeparationTime;
    private float timeBeforeComing;
    private bool alarm;

    // Start is called before the first frame update
    private void Start()
    {
        StartCoroutine(SpawnVehicle());
        
    }

    private IEnumerator SpawnVehicle()
    {
        while (true)
        {
            timeBeforeComing = Random.Range(minSeparationTime, maxSeparationTime);
            yield return new WaitForSeconds(timeBeforeComing - 3f);
            alarm = true;
            yield return new WaitForSeconds(3f);
            Instantiate(vehicle, SpawnPos.position, Quaternion.identity);
            yield return new WaitForSeconds(2f);
            alarm = false;
        }
        
    }

}

