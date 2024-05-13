using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Policies;

public class PlayerScript : Agent
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
    [SerializeField] private Transform Eagle;

    public string playerName;
    private byte _backwardsCount = 0;
    private char lastInput = 'W';
    private bool soundIsPlayed = false;
    public List<SkinData> skinDataList = new List<SkinData>();
private float timeWithoutScoreIncrease = 0f;
    private float maxTimeWithoutScore = 8f;
    [SerializeField]
    private bool _isAi = false;
    
    
    
    public override void OnEpisodeBegin()
    {
        transform.localPosition = new Vector3(0, 1.03f, 0);
    }
    
    public override void OnActionReceived(ActionBuffers actions)
    {
        if (_isHopping)
        {
            return;
        }
        if (actions.DiscreteActions[0] == 1)
        {
            AddReward(+0.3f);
            MovePlayer(Vector3.forward);
            _scoreBuffer++;
            if (_scoreBuffer > 0)
            {
                ScoreScript.Instance.UpdateScore();
                timeWithoutScoreIncrease = 0f;
                _scoreBuffer = 0;
            }
            if (_isAi)
            {
                AddReward(0.4f);
            }
        }
        else if (actions.DiscreteActions[0] == 2)
        {
            Debug.Log("Move Backward");
            MovePlayer(Vector3.back);
            _scoreBuffer--;
            AddReward(-0.2f);
        }
        else if (actions.DiscreteActions[0] == 3)
        {
            Debug.Log("Move Left");
            MovePlayer(Vector3.left);
        }
        else if (actions.DiscreteActions[0] == 4)
        {
            Debug.Log("Move Right");
            MovePlayer(Vector3.right);
        }
    }
    
    public override void Heuristic(in ActionBuffers actionsOut)
    {
        if (_isHopping)
        {
            return;
        }
        var actions = actionsOut.DiscreteActions;
        actions[0] = 0;
    }
    
    public void SetAi(bool isAi)
    {
        _isAi = isAi;
        this.GetComponent<BehaviorParameters>().BehaviorType = isAi ? BehaviorType.InferenceOnly : BehaviorType.HeuristicOnly;
    }
    
    

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
                    if (skin.theme == "Random")
                    {
                        int randomIndex = Random.Range(0, skinDataList.Count);
                        SkinData randomSkin = skinDataList[randomIndex];

                        do
                        {
                            randomIndex = Random.Range(0, skinDataList.Count);
                            randomSkin = skinDataList[randomIndex];
                        } while (!randomSkin.unlocked);

                        GameObject player = Instantiate(randomSkin.Model, parentPos);
                        GlobalVariables.theme = randomSkin.theme;
                        break;
                    }
                    else
                    {
                        GameObject player = Instantiate(skin.Model, parentPos);
                        GlobalVariables.theme = skin.theme;
                        break;
                    }
                }
            }
        }

        _animator = parentObject.GetComponent<Animator>();
        GlobalVariables.Player = GameObject.Find("PlayerObject").GetComponent<PlayerScript>();
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
        this.GetComponent<BehaviorParameters>().BehaviorType = BehaviorType.Default;
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
                    if (skin.theme == "Random")
                    {
                        int randomIndex = Random.Range(0, skinDataList.Count);
                        SkinData randomSkin = skinDataList[randomIndex];

                        do
                        {
                            randomIndex = Random.Range(0, skinDataList.Count);
                            randomSkin = skinDataList[randomIndex];
                        } while (!randomSkin.unlocked);

                        GameObject player = Instantiate(randomSkin.Model, parentPos);
                        GlobalVariables.theme = randomSkin.theme;
                    }
                    else
                    {
                        GameObject player = Instantiate(skin.Model, parentPos);
                        GlobalVariables.theme = skin.theme;
                    }
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

        string str_difficulty = "";

        switch (GlobalVariables.difficulty)
        {
            case 1.0f:
                str_difficulty = "easy";
                break;
            case 1.2f:
                str_difficulty = "medium";
                break;
            case 1.5f:
                str_difficulty = "hard";
                break;
            default:
                Debug.LogError("Invalid difficulty level: " + GlobalVariables.difficulty);
                break;
        }

        ScoreScript.Instance.WriteScore(str_difficulty);
        ScoreScript.Instance.ResetScore();
        AddReward(-1f);
        EndEpisode();
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
            AddReward(+0.2f);
            soundIsPlayed = false;
            if (_scoreBuffer > 0)
            {
                ScoreScript.Instance.UpdateScore();
                AddReward(+0.5f);
                timeWithoutScoreIncrease = 0f;
                _scoreBuffer = 0;
            }

            _backwardsCount = 0;

            lastInput = 'W';
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            AddReward(-0.3f);
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
            _backwardsCount++;
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
        if (_backwardsCount >= 3){
            EagleScript eagleScript = Eagle.GetComponentInChildren<EagleScript>();
            eagleScript.CatchPlayer();
            //TODO: display the game ended message @Reaub1
        }

        if (ScoreScript.Instance.isCounting){
            timeWithoutScoreIncrease += Time.deltaTime; 
            if (timeWithoutScoreIncrease >= maxTimeWithoutScore){
                EagleScript eagleScript = Eagle.GetComponentInChildren<EagleScript>();
                eagleScript.CatchPlayer();
            }
        }

        if(transform.position.z < -15f || transform.position.z > 15f){
            KillPlayer();
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
                AddReward(-0.2f);
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

    public void setDifficulty()
    {
        maxTimeWithoutScore = maxTimeWithoutScore / GlobalVariables.difficulty;
    }


}