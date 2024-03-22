using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogScript : MonoBehaviour
{
    private float speed = 1f;
    public bool islog;
    [SerializeField] private float direction;

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
        transform.Translate(Vector3.forward * speed * Time.deltaTime * direction);
        Debug.Log("Vitesse changée");
        Debug.Log(speed);
    }

    // Start is called before the first frame update
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime * direction);
    }
}
