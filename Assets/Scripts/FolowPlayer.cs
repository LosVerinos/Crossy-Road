using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float smoothSpeed = 0.125f;
    [SerializeField] private Vector3 offset = new Vector3(0, 1, 0);
    private Vector3 historicPosition;

    private void Start()
    {
        historicPosition = new Vector3(0, 3.1f, 0);
        transform.position = Vector3.zero;
    }

    private void Update()
    {
        if (player != null)
        {
            Vector3 playerPosition = player.transform.position;

            if (GlobalVariables.difficulty == 1.0f || !CompareTag("MainCamera"))
            {
                FollowPlayerNormally(playerPosition);
            }
            else if (GlobalVariables.run && CompareTag("MainCamera"))
            {
                if (GlobalVariables.eagleCatch)
                {
                    FollowPlayerDirectly(playerPosition);
                }
                else
                {
                    FollowPlayerWithOffset(playerPosition, GlobalVariables.difficulty == 1.2f ? 0.03f : 0.04f);
                }
            }
        }
    }

    private void FollowPlayerNormally(Vector3 playerPosition)
    {
        Vector3 desiredPosition = playerPosition + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }

    private void FollowPlayerDirectly(Vector3 playerPosition)
    {
        Vector3 desiredPosition = playerPosition;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }

    private void FollowPlayerWithOffset(Vector3 playerPosition, float multiplier)
    {
        if (playerPosition.x - transform.position.x > 2)
        {
            offset = new Vector3(multiplier * (playerPosition.x - transform.position.x), 0, playerPosition.z);
        }
        else
        {
            offset = new Vector3(multiplier, 0, playerPosition.z);
        }

        Vector3 desiredPosition = historicPosition + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
        historicPosition = new Vector3(desiredPosition.x, desiredPosition.y, 0);
    }
}
