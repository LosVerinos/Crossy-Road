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
            LogScript logScript = selectedLog.GetComponent<LogScript>();

            // Vérifiez si le script LogScript a été trouvé
            if (logScript != null)
            {
                // Changez la vitesse de la bûche en lui appelant la méthode SetSpeed avec la nouvelle vitesse
                logScript.SetSpeed(speed);
            }
            else
            {
                Debug.LogError("Log script not found on the log object!");
            }
            Instantiate(selectedLog, SpawnPos.position, Quaternion.identity);
            
        }
        
    }
}
