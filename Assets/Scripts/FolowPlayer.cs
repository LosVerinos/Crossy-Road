using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FolowPlayer : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float smoothSpeed;
    [SerializeField] private Vector3 offset = new Vector3(0,1,0);
    private Vector3 historicPosition;

    private void Start()
    {
        historicPosition = new Vector3(0,4,0);
        //historicPosition = player.transform.position;
        transform.position = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
           
            if(GlobalVariables.difficulty == 1.0f)
            {
                Vector3 desiredPosition = player.transform.position + offset;
                Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
                transform.position = smoothedPosition;
            }
            else if(GlobalVariables.difficulty == 1.2f && GlobalVariables.run)
            {
                offset = new Vector3(0.03f, 0, 0);
                Vector3 desiredPosition = historicPosition + offset;
                Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
                transform.position = smoothedPosition;
                historicPosition = desiredPosition;
            }
            else if(GlobalVariables.difficulty == 1.5f && GlobalVariables.run)
            {
                offset = new Vector3(0.05f, 0, 0);
                Vector3 desiredPosition = historicPosition + offset;
                Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
                transform.position = smoothedPosition;
                historicPosition = desiredPosition;
            }
            
        }
    }
}
