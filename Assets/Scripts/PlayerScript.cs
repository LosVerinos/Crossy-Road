using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] private TerrainGenerator TerrainGenerator;
    [SerializeField] private Text scoreText;
    private Animator _animator;
    private readonly static int Hop = Animator.StringToHash("hop");
    private bool _isHopping;
    private int _scoreBuffer = 0;
    public string playerName;
    private byte _backwardsCount = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void KillPlayer()
    {
        if (GlobalVariables.isPlayerKilled)
        {
            return;
        }
        GlobalVariables.isPlayerKilled = true;
        GlobalVariables.run = false;
        ScoreScript.Instance.WriteScore();
        ScoreScript.Instance.ResetScore();
        Destroy(GameObject.Find("Player").GetComponent<PlayerScript>());
    }
    
    public void SetPlayerName()
    {
        Debug.Log(GameObject.Find("InputPlayerName").GetComponent<InputField>().textComponent.text);
        playerName = GameObject.Find("InputPlayerName").GetComponent<InputField>().textComponent.text;
        Debug.Log(playerName);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) && !_isHopping && GlobalVariables.run)
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
                ScoreScript.Instance.UpdateScore();
                _scoreBuffer = 0;
            }

            _backwardsCount = 0;
        }
        if (Input.GetKeyDown(KeyCode.S) && !_isHopping && GlobalVariables.run)
        {
            float zDiff = 0;
            if (transform.position.z % 1 != 0)
            {
                zDiff = Mathf.Round(transform.position.z) - transform.position.z;
            }
            MovePlayer(new Vector3(-1,0, zDiff));
            _scoreBuffer--;
        }
        if (Input.GetKeyDown(KeyCode.A) && !_isHopping && GlobalVariables.run)
        {
            MovePlayer(new Vector3(0,0,1));
        }
        if (Input.GetKeyDown(KeyCode.D) && !_isHopping && GlobalVariables.run)
        {
            MovePlayer(new Vector3(0,0,-1));
        }
        scoreText.text = "Score: " + ScoreScript.Instance.GetScore();
        if (_backwardsCount >= 3)
        {
            Destroy(this.gameObject);
            //TODO: display the game ended message @Reaub1
        }
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
        var position = transform.position;
        transform.position = Vector3.Lerp(transform.position, newPosition, 1f);
        TerrainGenerator.SpawnTerrain(false, position);
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
