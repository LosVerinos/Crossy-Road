using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class LogSpawnScript : MonoBehaviour
{
    [SerializeField] private List<GameObject> logs;
    [SerializeField] private Transform SpawnPos;
    private float minSeparationTime = 0.25f;
    private float maxSeparationTime = 1.5f;
    [SerializeField] private float direction;
    private float speed;
    private float logLenght;
    private float maxLogLenght = 3.64f;
    private void Start()
    {
        speed = Random.Range(0.5f, 3f);
        FirstLogs();
        StartCoroutine(SpawnLog());

    }

    private void Update(){
        minSeparationTime = (0.2f + logLenght)/speed;
        if(minSeparationTime < 0f){
            Debug.Log(minSeparationTime + "     ALERT minSeparationTime");
        }
    }

    private IEnumerator SpawnLog()
    {
        while (true)
        {
            float timeToWait = Random.Range(minSeparationTime, maxSeparationTime);
            if(timeToWait < logLenght/speed){
                Debug.Log(timeToWait + "     ALERT");
            }
            yield return new WaitForSeconds(timeToWait);
            
            int randomIndex = Random.Range(0, logs.Count);
            GameObject selectedLog = logs[randomIndex];
        
            GameObject newLog = Instantiate(selectedLog, SpawnPos.position, Quaternion.identity);
            MovingObjectScript log = newLog.GetComponent<MovingObjectScript>();
            logLenght = log.logLenght;

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

    private void FirstLogs(){
        float LogsZ = Mathf.Abs(SpawnPos.position.z + direction*(10f * 2f));
        while(LogsZ > -Mathf.Abs(SpawnPos.position.z + direction*(10f * 2f))){
            int randomIndex = Random.Range(0, logs.Count);
            GameObject selectedLog = logs[randomIndex];
        
            GameObject newLog = Instantiate(selectedLog, new Vector3(SpawnPos.position.x, SpawnPos.position.y, LogsZ), Quaternion.identity);
            MovingObjectScript log = newLog.GetComponent<MovingObjectScript>();
            if (direction < 0)
            {
                newLog.transform.Rotate(new Vector3(0,180,0));
            }
            log.SetDirection(direction);
            log.SetSpeed(speed);

            LogsZ -= speed*Random.Range(minSeparationTime, maxSeparationTime) + maxLogLenght;
        }
    }
}
