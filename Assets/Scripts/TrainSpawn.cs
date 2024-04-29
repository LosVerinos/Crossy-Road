using System.Collections;
using UnityEngine;

public class TrainSpawn : MonoBehaviour
{
    [SerializeField] private GameObject vehicle;
    [SerializeField] private Transform SpawnPos;
    [SerializeField] private float minSeparationTime;
    [SerializeField] private float maxSeparationTime;
    [SerializeField] private float direction;
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

            if(gameObject.name.EndsWith("SW(Clone)")){
                if (alarmController != null){
                    alarmController.TriggerLasersOn(direction);
                    yield return new WaitForSeconds(3.1f);
                    InstantiateVehicle(40f);
                    yield return new WaitForSeconds(0.5f);
                    alarmController.TriggerLasersOff();
                }
                else{
                    Debug.LogError("Alarm controller not assigned!");
                } 
            }
            else if(gameObject.name.EndsWith("LOTR(Clone)")){
                if (alarmController != null){
                    InvokeRepeating("TriggerVibrationsOn", 0f, 0.01f);
                    InvokeRepeating("TriggerVibrationsOff", 0.005f, 0.01f);
                    yield return new WaitForSeconds(3f);
                    InstantiateVehicle(25f);
                    yield return new WaitForSeconds(3.005f);
                    CancelInvoke("TriggerVibrationsOn");
                    CancelInvoke("TriggerVibrationsOff");

                }
                else{
                    Debug.LogError("Alarm controller not assigned!");
                } 
            }
            else{
                if (alarmController != null){
                    /*
                    alarmController.TriggerAlarmOn();
                    yield return new WaitForSeconds(0.1f);
                    alarmController.TriggerAlarmOff();
                    yield return new WaitForSeconds(0.1f);
                    */
                    InvokeRepeating("TriggerAlarmOn", 0f, 0.2f);
                    InvokeRepeating("TurnOffAlarm", 0.1f, 0.2f);
                    yield return new WaitForSeconds(3.1f);
                    CancelInvoke("TriggerAlarmOn");
                    CancelInvoke("TurnOffAlarm");
                    InstantiateVehicle(40f);
                    /*InvokeRepeating("TriggerAlarmOn", 0f, 0.2f);
                    InvokeRepeating("TurnOffAlarm", 0.1f, 0.2f);
                    yield return new WaitForSeconds(0.9f);
                    CancelInvoke("TriggerAlarmOn");
                    CancelInvoke("TurnOffAlarm");*/
                }
                else{
                    Debug.LogError("Alarm controller not assigned!");
                }   
            }
        }
    }

    private void InstantiateVehicle(float speed){
        GameObject newTrain = Instantiate(vehicle, SpawnPos.position, Quaternion.identity);
        MovingObjectScript train = newTrain.GetComponent<MovingObjectScript>();
        if (direction < 0)
        {
            newTrain.transform.Rotate(new Vector3(0,180,0));
        }
        train.SetDirection(direction);
            train.SetSpeed(speed);
        
    }

    private void TriggerAlarmOn()
    {
        alarmController.TriggerAlarmOn();
    }

    private void TurnOffAlarm()
    {
        alarmController.TriggerAlarmOff();
    }

    private void TriggerVibrationsOn(){
        alarmController.TriggerVibrationsOn();
    }
    private void TriggerVibrationsOff(){
        alarmController.TriggerVibrationsOff();
    }

}
