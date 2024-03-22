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

        speed = Random.Range(1f, 3f);
        
        StartCoroutine(SpawnLog());
        
    }

    private IEnumerator SpawnLog()
    {
        while (true)
        {
            // Attendre un temps aléatoire avant de spawn une nouvelle bûche
            yield return new WaitForSeconds(Random.Range(minSeparationTime, maxSeparationTime));

            
            // Choisir aléatoirement une bûche dans la liste
            int randomIndex = Random.Range(0, logs.Count);
            GameObject selectedLog = logs[randomIndex];
            
            // Instancier la bûche
            GameObject newLog = Instantiate(selectedLog, SpawnPos.position, Quaternion.identity);
            
            // Récupérer le script LogScript attaché à la bûche et lui transmettre la vitesse initiale de 1
            MovingObjectScript logScript = newLog.GetComponent<MovingObjectScript>();
            if (logScript != null)
            {
                logScript.SetSpeed(10f); // Définir la vitesse initiale de la bûche à 1
            }
            else
            {
                Debug.LogError("Log script not found on the spawned log!");
            }
            yield return new WaitForSeconds(2f);
            logScript.SetSpeed(speed);
        }
    }
}
