using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EagleScript : MonoBehaviour
{
    [SerializeField] GameObject player;
    
    private Animator _animator;
    private static readonly int Catch = Animator.StringToHash("catch");
    public bool isCatching = false;

    // private static readonly int EagleIdle = Animator.StringToHash("DefaultEagle");

    // Start is called before the first frame update
    void Start()
    {
         _animator = GetComponent<Animator>();
    }

    public void CatchPlayer(){
        isCatching = true;
        _animator.SetTrigger(Catch);
    }
    
    public void GrabPlayer(){
        player.transform.position = transform.position;
        player.transform.parent = transform;
    }
    
    public void EndCatch(){
        isCatching = false;
        player.GetComponent<PlayerScript>().KillPlayer();
    }
}
