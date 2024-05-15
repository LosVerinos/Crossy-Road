using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EagleScript : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject eaglePrefab;
    private Animator _animator;
    private static readonly int Catch = Animator.StringToHash("catch");

    // Start is called before the first frame update
    private void Start()
    {
        _animator = GetComponent<Animator>();
        if (_animator == null)
        {
            Debug.LogWarning("Animator component missing from EagleScript");
        }
    }

    public void CatchPlayer()
    {
        if (_animator != null)
        {
            _animator.SetTrigger(Catch);
        }
    }

    public void GrabPlayer()
    {
        if (player != null)
        {
            DisablePlayerComponents();
            PositionPlayerOnEagle();
        }
    }

    private void DisablePlayerComponents()
    {
        var followPlayerScript = transform.parent.GetComponent<FollowPlayer>();
        if (followPlayerScript != null)
        {
            followPlayerScript.enabled = false;
        }

        var playerCollider = player.GetComponent<BoxCollider>();
        if (playerCollider != null)
        {
            playerCollider.enabled = false;
        }

        var playerRigidbody = player.GetComponent<Rigidbody>();
        if (playerRigidbody != null)
        {
            playerRigidbody.useGravity = false;
        }
    }

    private void PositionPlayerOnEagle()
    {
        player.transform.position = eaglePrefab.transform.position;
        player.transform.parent = eaglePrefab.transform;
    }

    public void EndCatch()
    {
        if (player != null)
        {
            var playerScript = player.GetComponent<PlayerScript>();
            if (playerScript != null)
            {
                playerScript.KillPlayer();
            }
        }
    }
}
