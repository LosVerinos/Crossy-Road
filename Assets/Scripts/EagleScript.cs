using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EagleScript : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject eaglePrebaf;
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
        player.transform.position = eaglePrebaf.transform.position;
        player.transform.parent = eaglePrebaf.transform;
    }
    
    public void EndCatch(){
        player.GetComponent<PlayerScript>().KillPlayer();
        isCatching = false;
    }
}
