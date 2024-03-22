using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleScript : MonoBehaviour
{

    [SerializeField] private float speed;
    [SerializeField] private float direction;

    // Start is called before the first frame update
    void Update(){
        transform.Translate(Vector3.forward * speed * Time.deltaTime * direction);
        if(transform.position.z * direction >= 45f){
            Destroy(gameObject);
        }
    }

}
