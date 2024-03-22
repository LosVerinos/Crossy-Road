using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LogSpawnScript : MonoBehaviour
{
    [SerializeField] private List<GameObject> logs;
    [SerializeField] private Transform SpawnPos;
    [SerializeField] private float minSeparationTime;
    [SerializeField] private float maxSeparationTime;
    private float speed;
    int randomIndex;
    GameObject selectedLog;
    private void Start()
    {
        // Choix aléatoire d'un véhicule dans la liste

        speed = Random.Range(1.5f, 3f);
        
        StartCoroutine(SpawnLog());
        
    }

    private IEnumerator SpawnLog()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minSeparationTime, maxSeparationTime));
            randomIndex = Random.Range(0, logs.Count);
            selectedLog = logs[randomIndex];
            selectedLog.GetComponent<LogScript>().SetSpeed(speed);
            Instantiate(selectedLog, SpawnPos.position, Quaternion.identity);
            
        }
        
    }
}
