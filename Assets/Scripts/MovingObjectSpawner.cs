using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObjectSpawner : MonoBehaviour
{
    [SerializeField] private GameObject spawnObject;
    [SerializeField] private Transform SpawnPos;
    [SerializeField] private float minSeparationTime;
    [SerializeField] private float maxSeparationTime;
    [SerializeField] private bool isRightSide;

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
            GameObject go = Instantiate(spawnObject, SpawnPos.position, Quaternion.identity);
            go.transform.rotation = (Quaternion.Euler(0,SpawnPos.rotation.y, 0));
            if (!isRightSide)
            {
                go.transform.Rotate(new Vector3(0,180,0));
            }
        }
        
    }

}
