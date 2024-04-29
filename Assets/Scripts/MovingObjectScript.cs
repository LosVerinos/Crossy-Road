using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents.Sensors.Reflection;
using Unity.VisualScripting;
using UnityEngine;

public class MovingObjectScript : MonoBehaviour
{
    [SerializeField] private Transform lenghtStart;
    [SerializeField] private Transform lenghtEnd;
    [Observable(numStackedObservations: 1)]
    public float speed;
    [Observable(numStackedObservations: 1)]
    public bool islog;
    [DoNotSerialize] public float logLenght;
    
    [Observable(numStackedObservations: 1)]
    private float direction;
    private TerrainGenerator terrainGenerator;

    [Observable(numStackedObservations: 3)]
    public Vector3 Position => transform.position;

    
    void Start(){
        terrainGenerator = FindObjectOfType<TerrainGenerator>();
        if(islog)
        {
            logLenght = lenghtEnd.position.z - lenghtStart.position.z;
        }
    }

    void Update()
    {
        transform.Translate(Vector3.forward * (speed * Time.deltaTime));
        if((transform.position.z * direction >= 45f) || terrainGenerator.lastTerrainX >= transform.position.x){
            Destroy(gameObject);
        }
        if(islog && transform.position.z * direction >= 50f-10f*3f){
            SetSpeed(10f);
        }
    }

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }
    public void SetDirection(float way)
    {
        direction = way;
    }
}
