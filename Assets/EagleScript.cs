using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EagleScript : MonoBehaviour
{
    [SerializeField] GameObject player;
    private Animator _animator;
    private readonly static int Catch = Animator.StringToHash("catch");
    // Start is called before the first frame update
    void Start()
    {
         _animator = GetComponent<Animator>();
    }

    public void CatchPlayer(){
        _animator.Play(Catch);
        //yield return new WaitForSeconds(5f);
        player.GetComponent<PlayerScript>().KillPlayer();

    }
}
