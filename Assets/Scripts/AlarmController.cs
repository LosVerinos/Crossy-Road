using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;
using UnityEngine.UIElements;

public class AlarmController : MonoBehaviour
{
    public List<GameObject> Activated; // Référence à l'objet à déplacer lors du déclenchement de l'alarme
    [SerializeField] private Transform lasersSpawnPos;
    private bool active;
    private float alarmDirection;

    // Méthode pour déclencher l'alarme
    public void TriggerAlarmOn()
    {
        if (Activated != null)
        {
            Activated[0].transform.Translate(Vector3.up * 1f);
            }
        
        else
        {
            Debug.LogError("Object to move not assigned!");
        }
    }

        
    public void TriggerAlarmOff()
    {
        if (Activated != null)
        {
            Activated[0].transform.Translate(Vector3.down * 1f);
            }
        
        else
        {
            Debug.LogError("Object to move not assigned!");
        }
    }

    public void TriggerLasersOn(float direction){
        alarmDirection = direction;
        InvokeRepeating("InstantiateLasers", 0f, 0.1f);
    }
    public void TriggerLasersOff(){
        CancelInvoke("InstantiateLasers");
    }

    private void InstantiateLasers(){
        float y = Random.Range(0.41f,1.5f);
        float x = Random.Range(-0.5f, 0.5f);
        int redOrGreen = Random.Range(0, 2);
        GameObject newLaser = Instantiate(Activated[redOrGreen], new Vector3(lasersSpawnPos.position.x + x,y,lasersSpawnPos.position.z), Quaternion.identity);
        MovingObjectScript laser = newLaser.GetComponent<MovingObjectScript>();
        if (alarmDirection < 0)
        {
            newLaser.transform.Rotate(new Vector3(0,180,0));
        }
        laser.SetDirection(alarmDirection);
        laser.SetSpeed(60f);
    } 
}

            