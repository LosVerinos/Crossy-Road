using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] private TerrainGenerator TerrainGenerator;
    [SerializeField] private Text scoreText;
    private Animator _animator;
    private readonly static int Hop = Animator.StringToHash("hop");
    private bool _isHopping;
    private int _score = 0;
    private int _scoreBuffer = 0;
    [SerializeField] private List<SkinData> skinData = new();
    [SerializeField] private Transform parentPos;
    [SerializeField] private Transform parentObject;

    // Start is called before the first frame update
    void Start()
    {
        int whichSkin = Random.Range(0, skinData.Count);
        GameObject player = Instantiate(skinData[whichSkin].Model, parentPos);
        Debug.Log(skinData[whichSkin].Model.name);
        _animator = parentObject.GetComponent<Animator>();
        Debug.Log(parentObject.name);
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
            _scoreBuffer++;
            if (_scoreBuffer > 0)
            {
                _score++;
                _scoreBuffer = 0;
            }
        }
        if (Input.GetKeyDown(KeyCode.S) && !_isHopping)
        {
            float zDiff = 0;
            if (transform.position.z % 1 != 0)
            {
                zDiff = Mathf.Round(transform.position.z) - transform.position.z;
            }
            MovePlayer(new Vector3(-1,0, zDiff));
            _scoreBuffer--;
        }
        if (Input.GetKeyDown(KeyCode.A) && !_isHopping)
        {
            MovePlayer(new Vector3(0,0,1));
        }
        if (Input.GetKeyDown(KeyCode.D) && !_isHopping)
        {
            MovePlayer(new Vector3(0,0,-1));
        }
        scoreText.text = "Score: " + _score;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.GetComponent<MovingObjectScript>() != null)
        {
            if (collision.collider.GetComponent<MovingObjectScript>().islog)
            {
                transform.parent = collision.collider.transform;
            }
            
        }
        else
        {
            transform.parent = null;
        }
    }

    private void MovePlayer(Vector3 diff)
    {
        Vector3 newPosition = transform.position + diff;

        Collider[] colliders = Physics.OverlapBox(newPosition, Vector3.one * 0.2f);
        foreach (var collider in colliders)
        {
            if (collider.CompareTag("Obstacle"))
            {
                return;
            }
        }
        
        _animator.SetTrigger(Hop);
        _isHopping = true;
        transform.position = newPosition;
        TerrainGenerator.SpawnTerrain(false, transform.position);
    }

    public void HopAnimationEnd()
    {
        _isHopping = false;
    }

    public Vector3 GetPlayerPosition()
    {
        return transform.position;
    }
}
