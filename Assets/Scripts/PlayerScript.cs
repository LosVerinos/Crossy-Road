using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] private TerrainGenerator TerrainGenerator;
    [SerializeField] private Text scoreText;
    [SerializeField] private AudioClip sound;

    private Animator _animator;
    private AudioSource _audioSource;
    private readonly static int Hop = Animator.StringToHash("hop");
    private bool _isHopping;
    private int _scoreBuffer = 0;
    [SerializeField] private List<SkinData> skinData = new();
    [SerializeField] private Transform parentPos;
    [SerializeField] private Transform parentObject;
    public string playerName;
    private byte _backwardsCount = 0;
    private char lastInput = 'W';
    private bool soundIsPlayed = false;
    public List<SkinData> skinDataList = new List<SkinData>();


    void Start()
    {
        if (skinDataList.Count == 0)
        {
            Debug.Log("La liste skinDataList est vide.");
        }
        else
        {
            foreach (SkinData skin in skinDataList)
            {
                if (skin.selected)
                {
                    GameObject player = Instantiate(skin.Model, parentPos);
                    GlobalVariables.theme = skin.theme;
                }
            }
        }

        _animator = parentObject.GetComponent<Animator>();
        GlobalVariables.Player = GameObject.Find("PlayerObject").GetComponent<PlayerScript>();
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
    }

    public void reloadSkin()
    {
        
        GameObject playerObject = GameObject.FindWithTag("Skin");
        if (playerObject != null)
        {
            Destroy(playerObject);
        }
        

        if (skinDataList.Count == 0)
        {
            Debug.Log("La liste skinDataList est vide.");
        }
        else
        {
            foreach (SkinData skin in skinDataList)
            {
                if (skin.selected)
                {
                    GameObject player = Instantiate(skin.Model, parentPos);
                    GlobalVariables.theme = skin.theme;
                }
            }
        }

        _animator = parentObject.GetComponent<Animator>();
        GlobalVariables.Player = GameObject.Find("PlayerObject").GetComponent<PlayerScript>();
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();

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
        Destroy(GlobalVariables.Player.GameObject());
    }
    
    public void SetPlayerName()
    {
        playerName = GameObject.Find("InputPlayerName").GetComponent<InputField>().textComponent.text;
    }

    void Update()
    {
        if (_isHopping || !GlobalVariables.run)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space))
        {

            switch (lastInput)
            {
                case 'S':
                    transform.Rotate(Vector3.up, 180f);
                    break;
                case 'A':
                    transform.Rotate(Vector3.up, 90f);
                    break;
                case 'D':
                    transform.Rotate(Vector3.up, -90f);
                    break;
                default:
                    break;
            }

            float zDiff = 0;
            if (transform.position.z % 1 != 0)
            {
                zDiff = Mathf.Round(transform.position.z) - transform.position.z;
            }

            MovePlayer(new Vector3(1,0, zDiff));
            _scoreBuffer++;
            soundIsPlayed = false;
            if (_scoreBuffer > 0)
            {
                ScoreScript.Instance.UpdateScore();
                _scoreBuffer = 0;
            }

            _backwardsCount = 0;

            lastInput = 'W';
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {

            switch (lastInput)
            {
                case 'W':
                    transform.Rotate(Vector3.up, 180f);
                    break;
                case 'A':
                    transform.Rotate(Vector3.up, -90f);
                    break;
                case 'D':
                    transform.Rotate(Vector3.up, 90f);
                    break;
                default:
                    break;
            }

            float zDiff = 0;
            if (transform.position.z % 1 != 0)
            {
                zDiff = Mathf.Round(transform.position.z) - transform.position.z;
            }
            MovePlayer(new Vector3(-1,0, zDiff));
            _scoreBuffer--;
            lastInput = 'S';
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            switch (lastInput)
            {
                case 'S':
                    transform.Rotate(Vector3.up, 90f);
                    break;
                case 'W':
                    transform.Rotate(Vector3.up, -90f);
                    break;
                case 'D':
                    transform.Rotate(Vector3.up, 180f);
                    break;
                default:
                    break;
            }

            float xDiff = 0;
            if (transform.position.x % 1 != 0)
            {
                xDiff = Mathf.Round(transform.position.x) - transform.position.x;
            }
            MovePlayer(new Vector3(xDiff,0,1));
            lastInput = 'A';
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            switch (lastInput)
            {
                case 'S':
                    transform.Rotate(Vector3.up, -90f);
                    break;
                case 'A':
                    transform.Rotate(Vector3.up, 180f);
                    break;
                case 'W':
                    transform.Rotate(Vector3.up, 90f);
                    break;
                default:
                    break;
            }

            float xDiff = 0;
            if (transform.position.x % 1 != 0)
            {
                xDiff = Mathf.Round(transform.position.x) - transform.position.x;
            }
            MovePlayer(new Vector3(xDiff,0,-1));
            lastInput = 'D';
        }
        if (ScoreScript.Instance.GetScore() % 50 == 0 && ScoreScript.Instance.GetScore()!=0 && !soundIsPlayed)
        {
            soundIsPlayed = true;

            if (sound != null)
            {
                _audioSource.clip = sound;
                _audioSource.Play();
            }

        }
        scoreText.text = "Score: " + ScoreScript.Instance.GetScore();
        if (_backwardsCount >= 3)
        {
            KillPlayer();
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
        _isHopping = true;
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
