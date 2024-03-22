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
    public AlarmController alarmController;

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

        if (alarmController != null)
        {

            alarmController.TriggerAlarmOn();
            yield return new WaitForSeconds(0.1f);
            alarmController.TriggerAlarmOff();
            yield return new WaitForSeconds(0.1f);
            alarmController.TriggerAlarmOn();
            yield return new WaitForSeconds(0.1f);
            alarmController.TriggerAlarmOff();
            yield return new WaitForSeconds(0.1f);
            alarmController.TriggerAlarmOn();
            yield return new WaitForSeconds(0.1f);
            alarmController.TriggerAlarmOff();
            yield return new WaitForSeconds(0.1f);
            alarmController.TriggerAlarmOn();
            yield return new WaitForSeconds(0.1f);
            alarmController.TriggerAlarmOff();
            yield return new WaitForSeconds(0.1f);
            alarmController.TriggerAlarmOn();
            yield return new WaitForSeconds(0.1f);
            alarmController.TriggerAlarmOff();
            yield return new WaitForSeconds(0.1f);
            alarmController.TriggerAlarmOn();
            yield return new WaitForSeconds(0.1f);
            alarmController.TriggerAlarmOff();
            yield return new WaitForSeconds(0.1f);
            alarmController.TriggerAlarmOn();
            yield return new WaitForSeconds(0.1f);
            alarmController.TriggerAlarmOff();
            yield return new WaitForSeconds(0.1f);
            alarmController.TriggerAlarmOn();
            yield return new WaitForSeconds(0.1f);
            alarmController.TriggerAlarmOff();
            yield return new WaitForSeconds(0.1f);
            alarmController.TriggerAlarmOn();
            yield return new WaitForSeconds(0.1f);
            alarmController.TriggerAlarmOff();
            yield return new WaitForSeconds(0.1f);
            alarmController.TriggerAlarmOn();
            yield return new WaitForSeconds(0.1f);
            alarmController.TriggerAlarmOff();
            yield return new WaitForSeconds(0.1f);
            alarmController.TriggerAlarmOn();
            yield return new WaitForSeconds(0.1f);
            alarmController.TriggerAlarmOff();
            yield return new WaitForSeconds(0.1f);
            alarmController.TriggerAlarmOn();
            yield return new WaitForSeconds(0.1f);
            alarmController.TriggerAlarmOff();
            yield return new WaitForSeconds(0.1f);
            alarmController.TriggerAlarmOn();
            yield return new WaitForSeconds(0.1f);
            alarmController.TriggerAlarmOff();
            yield return new WaitForSeconds(0.1f);
            alarmController.TriggerAlarmOn();
            yield return new WaitForSeconds(0.1f);
            alarmController.TriggerAlarmOff();
            yield return new WaitForSeconds(0.1f);
            GameObject newTrain = Instantiate(vehicle, SpawnPos.position, Quaternion.identity);
            MovingObjectScript train = newTrain.GetComponent<MovingObjectScript>();
            train.SetSpeed(30f);
            alarmController.TriggerAlarmOn();
            yield return new WaitForSeconds(0.1f);
            alarmController.TriggerAlarmOff();
            yield return new WaitForSeconds(0.1f);
            alarmController.TriggerAlarmOn();
            yield return new WaitForSeconds(0.1f);
            alarmController.TriggerAlarmOff();
            yield return new WaitForSeconds(0.1f);
        }
        else
        {
            Debug.LogError("Alarm controller not assigned!");
        }   
            
            
        }
    }
}
