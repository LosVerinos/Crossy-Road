using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleScript : MonoBehaviour
{

    [SerializeField] private float speed;

    // Start is called before the first frame update
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.GetComponent<PlayerScript>() != null)
        {
            Destroy(collision.gameObject);
        }
    }

}
