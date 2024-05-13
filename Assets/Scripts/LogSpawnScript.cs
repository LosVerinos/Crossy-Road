using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class LogSpawnScript : MonoBehaviour
{
    [SerializeField] private List<GameObject> logs;
    [SerializeField] private Transform SpawnPos;
    private float minSeparationTime = 0.25f;
    private float maxSeparationTime = 2.25f;
    [SerializeField] private float direction;
    private float speed;
    private float logLenght;
    private float lastLogLenght;
    private float initialSpeed = 10f;
    private float timeWhithInitialSpeed = 4f;
    private int randomIndex;
    private GameObject selectedLog;

    private void Start()
    {
        speed = Random.Range(0.5f, 3f);
        FirstLogs();
        randomIndex = Random.Range(0, logs.Count);
        selectedLog = logs[randomIndex];
        StartCoroutine(SpawnLog());
    }

    private IEnumerator SpawnLog()
    {
        var logNumber = 0;
        while (true)
        {
            randomIndex = Random.Range(0, logs.Count);
            selectedLog = logs[randomIndex];
            if (logNumber != 0) yield return new WaitForSeconds(DetermineTimeToWait());
            var newLog = Instantiate(selectedLog, SpawnPos.position, Quaternion.identity);
            var log = newLog.GetComponent<MovingObjectScript>();

            if (direction < 0) newLog.transform.Rotate(new Vector3(0, 180, 0));
            log.SetDirection(direction);

            minSeparationTime = (0.2f + logLenght) / speed;
            maxSeparationTime = minSeparationTime + 2f;
            log.SetSpeed(speed);
            logNumber++;
        }
    }

    private void FirstLogs()
    {
        var spawnLimit = Mathf.Abs(SpawnPos.position.z + direction * (initialSpeed * timeWhithInitialSpeed));
        var LogsZ = spawnLimit;
        while (LogsZ > -Mathf.Abs(SpawnPos.position.z + direction * (initialSpeed * timeWhithInitialSpeed)))
        {
            var randomIndex = Random.Range(0, logs.Count);
            var selectedLog = logs[randomIndex];
            var selected = selectedLog.GetComponent<MovingObjectScript>();
            logLenght = selected.lenghtEnd.position.z - selected.lenghtStart.position.z;
            LogsZ -= lastLogLenght / 2 + 0.2f + logLenght;
            if (LogsZ - logLenght / 2 < -spawnLimit) break;

            var newLog = Instantiate(selectedLog, new Vector3(SpawnPos.position.x, SpawnPos.position.y, LogsZ),
                Quaternion.identity);
            var log = newLog.GetComponent<MovingObjectScript>();

            if (direction < 0) newLog.transform.Rotate(new Vector3(0, 180, 0));
            log.SetDirection(direction);
            log.SetSpeed(speed);
            lastLogLenght = logLenght;
        }

        minSeparationTime = (0.2f + logLenght) / speed;
        maxSeparationTime = minSeparationTime + 2f;
        lastLogLenght = logLenght;
    }

    private float DetermineTimeToWait()
    {
        var selected = selectedLog.GetComponent<MovingObjectScript>();
        lastLogLenght = logLenght;
        logLenght = selected.lenghtEnd.position.z - selected.lenghtStart.position.z;
        if (logLenght > lastLogLenght)
            return Random.Range(minSeparationTime, maxSeparationTime) + logLenght / 2 / speed;
        else
            return Random.Range(minSeparationTime, maxSeparationTime);
    }
}