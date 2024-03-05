using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] private TerrainGenerator TerrainGenerator;
    private Animator _animator;
    private readonly static int Hop = Animator.StringToHash("hop");
    private bool _isHopping;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
    }
    
    private bool IsMovingForward()
    {
        return Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow);
    }
    
    private bool IsMovingBackward()
    {
        return Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow);
    }
    
    private bool IsMovingLeft()
    {
        return Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow);
    }
    
    private bool IsMovingRight()
    {
        return Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow);
    }
    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) && !_isHopping)
        {
            float zDiff = 0;
            if (transform.position.z % 1 != 0)
            {
                zDiff = Mathf.Round(transform.position.z) - transform.position.z;
            }
            MovePlayer(new Vector3(1,0, zDiff));
        }
        if (Input.GetKeyDown(KeyCode.S) && !_isHopping)
        {
            float zDiff = 0;
            if (transform.position.z % 1 != 0)
            {
                zDiff = Mathf.Round(transform.position.z) - transform.position.z;
            }
            MovePlayer(new Vector3(-1,0, zDiff));
        }
        if (Input.GetKeyDown(KeyCode.A) && !_isHopping)
        {
            MovePlayer(new Vector3(0,0,1));
        }
        if (Input.GetKeyDown(KeyCode.D) && !_isHopping)
        {
            MovePlayer(new Vector3(0,0,-1));
        }

    }
    
    private void MovePlayer(Vector3 diff)
    {
        _animator.SetTrigger(Hop);
        _isHopping = true;
        transform.position = transform.position + diff;
        TerrainGenerator.SpawnTerrain(false, transform.position);
    }

    public void HopAnimationEnd()
    {
        _isHopping = false;
    }

}
