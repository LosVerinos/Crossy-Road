using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogSpawnScript : MonoBehaviour
{
    [SerializeField] private List<GameObject> logs;
    [SerializeField] private Transform spawnPos;
    private float minSeparationTime = 0.25f;
    private float maxSeparationTime = 2.25f;
    [SerializeField] private float direction;
    private float speed;
    private float logLength;
    private float lastLogLength;
    private float initialSpeed = 10f;
    private float timeWithInitialSpeed = 4f;
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
        int logNumber = 0;
        while (true)
        {
            randomIndex = Random.Range(0, logs.Count);
            selectedLog = logs[randomIndex];
            if (logNumber != 0) yield return new WaitForSeconds(DetermineTimeToWait());
            SpawnLogInstance();
            logNumber++;
        }
    }

    private void FirstLogs()
    {
        float spawnLimit = Mathf.Abs(spawnPos.position.z + direction * (initialSpeed * timeWithInitialSpeed));
        float logsZ = spawnLimit;
        while (logsZ > -Mathf.Abs(spawnPos.position.z + direction * (initialSpeed * timeWithInitialSpeed)))
        {
            SpawnInitialLog(ref logsZ);
            if (logsZ < -spawnLimit) break;
        }

        minSeparationTime = (0.2f + logLength) / speed;
        maxSeparationTime = minSeparationTime + 2f;
        lastLogLength = logLength;
    }

    private void SpawnInitialLog(ref float logsZ)
    {
        randomIndex = Random.Range(0, logs.Count);
        selectedLog = logs[randomIndex];
        logLength = GetLogLength(selectedLog);
        logsZ -= lastLogLength / 2 + 0.2f + logLength;

        if (logsZ - logLength / 2 < -Mathf.Abs(spawnPos.position.z + direction * (initialSpeed * timeWithInitialSpeed))) return;

        GameObject newLog = Instantiate(selectedLog, new Vector3(spawnPos.position.x, spawnPos.position.y, logsZ), Quaternion.identity);
        MovingObjectScript log = newLog.GetComponent<MovingObjectScript>();
        if (direction < 0) newLog.transform.Rotate(new Vector3(0, 180, 0));
        log.SetDirection(direction);
        log.SetSpeed(speed);
        lastLogLength = logLength;
    }

    private void SpawnLogInstance()
    {
        GameObject newLog = Instantiate(selectedLog, spawnPos.position, Quaternion.identity);
        MovingObjectScript log = newLog.GetComponent<MovingObjectScript>();
        if (direction < 0) newLog.transform.Rotate(new Vector3(0, 180, 0));
        log.SetDirection(direction);
        log.SetSpeed(speed);

        minSeparationTime = (0.2f + logLength) / speed;
        maxSeparationTime = minSeparationTime + 2f;
    }

    private float DetermineTimeToWait()
    {
        logLength = GetLogLength(selectedLog);
        return Random.Range(minSeparationTime, maxSeparationTime) + logLength / 2 / speed;
    }

    private float GetLogLength(GameObject log)
    {
        // Assuming the length of the log is determined by the Z scale of the log's transform
        return log.GetComponent<Renderer>().bounds.size.z;
    }
}
