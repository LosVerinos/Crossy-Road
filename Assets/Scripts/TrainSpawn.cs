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

            if(gameObject.name.EndsWith("SW")){
                //Fonction for StarWars
                InstantiateVehicle();
            }
            else{
                
                if (alarmController != null)
                {
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
                    InstantiateVehicle();
                    InvokeRepeating("TriggerAlarmOn", 0f, 0.2f);
                    InvokeRepeating("TurnOffAlarm", 0.1f, 0.2f);
                    yield return new WaitForSeconds(0.9f);
                    CancelInvoke("TriggerAlarmOn");
                    CancelInvoke("TurnOffAlarm");
                }
                else
                {
                    Debug.LogError("Alarm controller not assigned!");
                }   
            }
            
        }
    }

    private void InstantiateVehicle(){
        GameObject newTrain = Instantiate(vehicle, SpawnPos.position, Quaternion.identity);
        MovingObjectScript train = newTrain.GetComponent<MovingObjectScript>();
        if (direction < 0)
        {
            newTrain.transform.Rotate(new Vector3(0,180,0));
        }
        train.SetDirection(direction);
        train.SetSpeed(40f);
    }

    private void TriggerAlarmOn()
    {
        Debug.Log("CACA");
        alarmController.TriggerAlarmOn();
    }

    private void TurnOffAlarm()
    {
        alarmController.TriggerAlarmOff();
    }
}
