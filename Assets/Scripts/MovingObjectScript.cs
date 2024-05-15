using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovingObjectScript : MonoBehaviour
{
    [SerializeField] public Transform lenghtStart;
    [SerializeField] public Transform lenghtEnd;
    private float initialSpeed = 10f;
    private float speed;
    public bool isLog; // Assurez-vous que ce membre est bien défini
    [DoNotSerialize] public float logLenght;
    private float direction;
    private TerrainGenerator terrainGenerator;

    private void Start()
    {
        terrainGenerator = FindObjectOfType<TerrainGenerator>();
        if (isLog) logLenght = direction * (lenghtEnd.position.z - lenghtStart.position.z);
    }

    private void Update()
    {
        if (GlobalVariables.reload) Destroy(gameObject);

        if (isLog && transform.position.z * direction <= -10f)
            transform.Translate(Vector3.forward * (initialSpeed * Time.deltaTime));
        else
            transform.Translate(Vector3.forward * (speed * GlobalVariables.difficulty * Time.deltaTime));
        if (transform.position.z * direction >= 45f || terrainGenerator.lastTerrainX >= transform.position.x)
            Destroy(gameObject);
        if (isLog && transform.position.z * direction >= 10f) SetSpeed(initialSpeed);
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
