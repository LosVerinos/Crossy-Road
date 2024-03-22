using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObjectScript : MonoBehaviour
{

    private float speed;
    public bool islog;
    [SerializeField] private float direction;

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime * direction);
        if(transform.position.z * direction >= 45f){
            Destroy(gameObject);
        }
    }

    // Méthode pour définir la vitesse de la bûche
    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

}
