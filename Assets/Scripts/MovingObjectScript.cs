using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObjectScript : MonoBehaviour
{

    private float speed;
    public bool islog;
    private float direction;
    private TerrainGenerator terrainGenerator;

    void Start(){
        terrainGenerator = FindObjectOfType<TerrainGenerator>();
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
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
