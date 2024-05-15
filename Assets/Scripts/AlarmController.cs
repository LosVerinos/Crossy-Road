using System.Collections.Generic;
using UnityEngine;

public class AlarmController : MonoBehaviour
{
    public List<GameObject> Activated; // Référence à l'objet à déplacer lors du déclenchement de l'alarme
    [SerializeField] private Transform lasersSpawnPos;
    private float alarmDirection;

    private void MoveObject(Vector3 direction)
    {
        if (Activated != null && Activated.Count > 0 && Activated[0] != null)
        {
            Activated[0].transform.Translate(direction);
        }
        else
        {
            Debug.LogError("Object to move not assigned or list is empty!");
        }
    }

    // Méthode pour déclencher l'alarme
    public void TriggerAlarmOn()
    {
        MoveObject(Vector3.up * 1f);
    }

    public void TriggerAlarmOff()
    {
        MoveObject(Vector3.down * 1f);
    }

    public void TriggerLasersOn(float direction)
    {
        alarmDirection = direction;
        InvokeRepeating(nameof(InstantiateLasers), 0f, 0.1f);
    }

    public void TriggerLasersOff()
    {
        CancelInvoke(nameof(InstantiateLasers));
    }

    private void InstantiateLasers()
    {
        var y = Random.Range(0.41f, 1.5f);
        var x = Random.Range(-0.5f, 0.5f);
        var redOrGreen = Random.Range(0, Activated.Count);

        if (redOrGreen >= Activated.Count || Activated[redOrGreen] == null)
        {
            Debug.LogError("Invalid laser GameObject in Activated list!");
            return;
        }

        var newLaser = Instantiate(Activated[redOrGreen], new Vector3(lasersSpawnPos.position.x + x, y, lasersSpawnPos.position.z), Quaternion.identity);
        var laser = newLaser.GetComponent<MovingObjectScript>();

        if (laser == null)
        {
            Debug.LogError("The instantiated laser does not have a MovingObjectScript component!");
            return;
        }

        if (alarmDirection < 0)
        {
            newLaser.transform.Rotate(new Vector3(0, 180, 0));
        }

        laser.SetDirection(alarmDirection);
        laser.SetSpeed(60f);
    }

    public void TriggerVibrationsOn()
    {
        MoveObject(Vector3.up * 0.05f);
    }

    public void TriggerVibrationsOff()
    {
        MoveObject(Vector3.down * 0.05f);
    }
}
