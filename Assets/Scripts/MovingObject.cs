using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{

    [SerializeField] private float speed;
    public bool islog;

    // Start is called before the first frame update
    void Update()
    {
        Debug.Log(Vector3.forward * speed * Time.deltaTime);
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}
