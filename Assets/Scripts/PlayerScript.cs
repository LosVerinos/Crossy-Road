using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private Animator _animator;
    private readonly static int Hop = Animator.StringToHash("hop");
    private bool _isHopping;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
       if (Input.GetKeyDown(KeyCode.W) && !_isHopping)
       {
           _animator.SetTrigger(Hop);
           _isHopping = true;
           float zDiff = 0;
           if (transform.position.z % 1 != 0)
           {
               zDiff = Mathf.Round(transform.position.z) - transform.position.z;
           }
           transform.position = (transform.position + new Vector3(1, 0, zDiff));
       }
       else if (Input.GetKeyDown(KeyCode.S) && !_isHopping)
       {
           _animator.SetTrigger(Hop);
           _isHopping = true;
           float zDiff = 0;
           if (transform.position.z % 1 != 0)
           {
               zDiff = Mathf.Round(transform.position.z) - transform.position.z;
           }
           transform.position = (transform.position + new Vector3(-1, 0, zDiff));
       }
       else if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) && !_isHopping)
       {
           _animator.SetTrigger(Hop);
           _isHopping = true;
           float xDiff = 0;
           if (transform.position.x % 1 != 0)
           {
               xDiff = Mathf.Round(transform.position.x) - transform.position.x;
           }
           transform.position = (transform.position + new Vector3(xDiff, 0, 1));
       }
       else if (Input.GetKeyDown(KeyCode.D) && !_isHopping)
       {
           _animator.SetTrigger(Hop);
           _isHopping = true;
           float xDiff = 0;
           if (transform.position.x % 1 != 0)
           {
               xDiff = Mathf.Round(transform.position.x) - transform.position.x;
           }
           transform.position = (transform.position + new Vector3(xDiff, 0, -1));
       }

    }

    public void HopAnimationEnd()
    {
        _isHopping = false;
    }

}
