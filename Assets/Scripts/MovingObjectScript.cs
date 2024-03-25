using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObjectScript : MonoBehaviour
{

    private float speed;
    public bool islog;
    private float direction;
    private PlayerScript playerScript;

    void Start(){
        playerScript = FindObjectOfType<PlayerScript>();
    }

    void Update()
    {
        Vector3 playerPosition = playerScript.GetPlayerPosition();
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        if((transform.position.z * direction >= 45f) || playerPosition.x-8 == transform.position.x){
            Destroy(gameObject);
        }
    }

    // Méthode pour définir la vitesse de la bûche
    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }
    public void SetDirection(float way)
    {
        direction = way;
    }
}
