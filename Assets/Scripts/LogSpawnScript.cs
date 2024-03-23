using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class LogSpawnScript : MonoBehaviour
{
    [SerializeField] private List<GameObject> logs;
    [SerializeField] private Transform SpawnPos;
    [SerializeField] private float minSeparationTime;
    [SerializeField] private float maxSeparationTime;
    [SerializeField] private float direction;
    private float speed;
    int randomIndex;
    GameObject selectedLog;
    private void Start()
    {
        // Choix aléatoire d'un véhicule dans la liste
        speed = Random.Range(0.5f, 3f);
        
        
        StartCoroutine(SpawnLog());
        
    }

    private IEnumerator SpawnLog()
    {
        while (true)
        {
            
            yield return new WaitForSeconds(Random.Range(minSeparationTime, maxSeparationTime));
            
            int randomIndex = Random.Range(0, logs.Count);
            GameObject selectedLog = logs[randomIndex];
        
            GameObject newLog = Instantiate(selectedLog, SpawnPos.position, Quaternion.identity);
            MovingObjectScript log = newLog.GetComponent<MovingObjectScript>();
            if (direction < 0)
            {
                newLog.transform.Rotate(new Vector3(0,180,0));
            }
            log.SetDirection(direction);
            log.SetSpeed(10f);

            yield return new WaitForSeconds(2f);
            
            log.SetSpeed(speed);
            if(speed <= 1.2f){
                yield return new WaitForSeconds(Random.Range(3f, 6f));
            }
        }
        
    }
}
