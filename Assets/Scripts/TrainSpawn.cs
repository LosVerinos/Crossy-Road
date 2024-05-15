using System.Collections;
using UnityEngine;

public class TrainSpawn : MonoBehaviour
{
    [SerializeField] private GameObject vehicle;
    [SerializeField] private Transform spawnPos;
    [SerializeField] private float minSeparationTime;
    [SerializeField] private float maxSeparationTime;
    [SerializeField] private float direction;
    [SerializeField] private AlarmController alarmController;

    private void Start()
    {
        StartCoroutine(SpawnVehicle());
    }

    private IEnumerator SpawnVehicle()
    {
        while (true)
        {
            float timeBeforeComing = Random.Range(minSeparationTime, maxSeparationTime);
            yield return new WaitForSeconds(timeBeforeComing - 3f);

            switch (gameObject.name)
            {
                case string name when name.EndsWith("SW(Clone)"):
                    HandleStarWarsScenario();
                    break;
                case string name when name.EndsWith("LOTR(Clone)"):
                    HandleLotrScenario();
                    break;
                default:
                    HandleDefaultScenario();
                    break;
            }
        }
    }

    private void HandleStarWarsScenario()
    {
        if (alarmController == null)
        {
            Debug.LogError("Alarm controller not assigned!");
            return;
        }

        alarmController.TriggerLasersOn(direction);
        StartCoroutine(DelayAndInstantiate(3.1f, 40f, alarmController.TriggerLasersOff));
    }

    private void HandleLotrScenario()
    {
        if (alarmController == null)
        {
            Debug.LogError("Alarm controller not assigned!");
            return;
        }

        InvokeRepeating(nameof(TriggerVibrationsOn), 0f, 0.05f);
        InvokeRepeating(nameof(TriggerVibrationsOff), 0.025f, 0.05f);
        StartCoroutine(DelayAndInstantiate(3f, 25f, () =>
        {
            CancelInvoke(nameof(TriggerVibrationsOn));
            CancelInvoke(nameof(TriggerVibrationsOff));
        }));
    }

    private void HandleDefaultScenario()
    {
        if (alarmController == null)
        {
            Debug.LogError("Alarm controller not assigned!");
            return;
        }

        InvokeRepeating(nameof(TriggerAlarmOn), 0f, 0.2f);
        InvokeRepeating(nameof(TriggerAlarmOff), 0.1f, 0.2f);
        StartCoroutine(DelayAndInstantiate(3.1f, 40f, () =>
        {
            CancelInvoke(nameof(TriggerAlarmOn));
            CancelInvoke(nameof(TriggerAlarmOff));
        }));
    }

    private IEnumerator DelayAndInstantiate(float delay, float speed, System.Action postInstantiateAction)
    {
        yield return new WaitForSeconds(delay);
        InstantiateVehicle(speed);
        postInstantiateAction?.Invoke();
    }

    private void InstantiateVehicle(float speed)
    {
        var newTrain = Instantiate(vehicle, spawnPos.position, Quaternion.identity);
        var train = newTrain.GetComponent<MovingObjectScript>();
        if (direction < 0) newTrain.transform.Rotate(new Vector3(0, 180, 0));
        train.SetDirection(direction);
        train.SetSpeed(speed);
    }

    private void TriggerAlarmOn()
    {
        alarmController.TriggerAlarmOn();
    }

    private void TriggerAlarmOff()
    {
        alarmController.TriggerAlarmOff();
    }

    private void TriggerVibrationsOn()
    {
        alarmController.TriggerVibrationsOn();
    }

    private void TriggerVibrationsOff()
    {
        alarmController.TriggerVibrationsOff();
    }
}
