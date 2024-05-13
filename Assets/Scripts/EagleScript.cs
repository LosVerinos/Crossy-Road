using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class EagleScript : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject eaglePrebaf;
    private Animator _animator;
    private static readonly int Catch = Animator.StringToHash("catch");

    // Start is called before the first frame update
    void Start()
    {
         _animator = GetComponent<Animator>();
    }

    public void CatchPlayer(){

        _animator.SetTrigger(Catch);
    }
    
    public void GrabPlayer(){
        transform.parent.GetComponent<FolowPlayer>().enabled = false;
        player.GetComponent<BoxCollider>().enabled = false;
        player.GetComponent<Rigidbody>().useGravity = false;
        player.transform.position = eaglePrebaf.transform.position;
        player.transform.parent = eaglePrebaf.transform;
    }
    
    public void EndCatch(){
        player.GetComponent<PlayerScript>().KillPlayer(); 
    }
}
