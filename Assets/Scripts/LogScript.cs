using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogScript : MonoBehaviour
{
    private float speed;
    public bool islog;
    [SerializeField] private float direction;

    void Start(){
        speed = Random.Range(1.5f, 3f);
    }
    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    // Start is called before the first frame update
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime * direction);
    }
}
