using System.Collections;
using UnityEngine;

public class TrainSpawn : MonoBehaviour
{
    [SerializeField] private GameObject vehicle;
    [SerializeField] private Transform SpawnPos;
    [SerializeField] private float minSeparationTime;
    [SerializeField] private float maxSeparationTime;
    private float timeBeforeComing;
    private bool alarm;
    public AlarmBehaviour alarmBehaviour; // Référence à AlarmBehaviour

    // Start is called before the first frame update
    private void Start()
    {
        // Recherche automatique du GameObject contenant AlarmBehaviour
        alarmBehaviour = FindObjectOfType<AlarmBehaviour>();

        StartCoroutine(SpawnVehicle());
    }

    private IEnumerator SpawnVehicle()
    {
        while (true)
        {
            timeBeforeComing = Random.Range(minSeparationTime, maxSeparationTime);
            yield return new WaitForSeconds(timeBeforeComing - 3f);

            // Vérifie si l'objet AlarmBehaviour a été trouvé
            if (alarmBehaviour != null)
            {
                Debug.Log("Trouvé");
                // Démarrer la coroutine Alarm
                StartCoroutine(alarmBehaviour.Alarm());
            }
            else
            {
                Debug.LogError("AlarmBehaviour not found!");
            }

            yield return new WaitForSeconds(3f);
            Instantiate(vehicle, SpawnPos.position, Quaternion.identity);
        }
    }
}
