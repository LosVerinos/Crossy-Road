using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Policies;

public class PlayerScript : Agent
{
    [SerializeField] private TerrainGenerator TerrainGenerator;
    [SerializeField] private AudioClip sound;

    private Animator _animator;
    private AudioSource _audioSource;
    private static readonly int Hop = Animator.StringToHash("hop");
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
    public List<SkinData> skinDataList = new();
    private float timeWithoutScoreIncrease = 0f;
    private float maxTimeWithoutScore = 8f;
    [SerializeField] private bool _isAi = false;

    private bool isOnLog;
    private int bestScore = 0;


    public override void OnEpisodeBegin()
    {
        _scoreBuffer = 0;
        bestScore = ScoreScript.Instance.GetScore() > bestScore ? ScoreScript.Instance.GetScore() : bestScore;
        ScoreScript.Instance.ResetScore();
        transform.localPosition = new Vector3(0, 1.03f, 0);
    }

    public override void OnActionReceived(ActionBuffers actions)
{
    if (_isHopping) return;

    // Add some exploration
    var randomValue = Random.value;
    if (randomValue < 0.1f)  // 10% of the time, take a random action
    {
        var randomAction = Random.Range(1, 5);
        MovePlayerBasedOnAction(randomAction);
    }
    else  // 90% of the time, take the action that the agent thinks is best
    {
        MovePlayerBasedOnAction(actions.DiscreteActions[0]);
    }
}

private void MovePlayerBasedOnAction(int action)
{
    switch (action)
    {
        case 1:
            MovePlayer(Vector3.forward);
            break;
        case 2:
            MovePlayer(Vector3.back);
            break;
        case 3:
            MovePlayer(Vector3.left);
            break;
        case 4:
            MovePlayer(Vector3.right);
            break;
    }
}

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        if (_isHopping) return;
        var actions = actionsOut.DiscreteActions;
        actions[0] = 0;
    }

    public void SetAi(bool isAi)
    {
        _isAi = isAi;
        GetComponent<BehaviorParameters>().BehaviorType =
            isAi ? BehaviorType.InferenceOnly : BehaviorType.HeuristicOnly;
    }


    private void Start()
    {
        skinDataList[3].selected = true;
        if (skinDataList.Count == 0)
            Debug.Log("La liste skinDataList est vide.");
        else
            foreach (var skin in skinDataList)
                if (skin.selected)
                {
                    if (skin.theme == "Random")
                    {
                        Debug.Log("random");
                        var randomIndex = Random.Range(0, skinDataList.Count);
                        var randomSkin = skinDataList[randomIndex];

                        do
                        {
                            randomIndex = Random.Range(0, skinDataList.Count);
                            randomSkin = skinDataList[randomIndex];
                        } while (!randomSkin.unlocked);

                        var player = Instantiate(randomSkin.Model, parentPos);
                        GlobalVariables.theme = randomSkin.theme;
                        break;
                    }
                    else
                    {
                        var player = Instantiate(skin.Model, parentPos);
                        GlobalVariables.theme = skin.theme;
                        break;
                    }
                }

        _animator = parentObject.GetComponent<Animator>();
        GlobalVariables.Player = GameObject.Find("PlayerObject").GetComponent<PlayerScript>();
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
        GetComponent<BehaviorParameters>().BehaviorType = BehaviorType.HeuristicOnly;
    }

    public void reloadSkin()
    {
        var playerObject = GameObject.FindWithTag("Skin");
        if (playerObject != null) Destroy(playerObject);


        if (skinDataList.Count == 0)
            Debug.Log("La liste skinDataList est vide.");
        else
            foreach (var skin in skinDataList)
                if (skin.selected)
                {
                    if (skin.theme == "Random")
                    {
                        var randomIndex = Random.Range(0, skinDataList.Count);
                        var randomSkin = skinDataList[randomIndex];

                        do
                        {
                            randomIndex = Random.Range(0, skinDataList.Count);
                            randomSkin = skinDataList[randomIndex];
                        } while (!randomSkin.unlocked);

                        var player = Instantiate(randomSkin.Model, parentPos);
                        GlobalVariables.theme = randomSkin.theme;
                    }
                    else
                    {
                        var player = Instantiate(skin.Model, parentPos);
                        GlobalVariables.theme = skin.theme;
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
        SetReward(-1f);
        EndEpisode();
        Destroy(GlobalVariables.Player.GameObject());
    }

    public void SetPlayerName()
    {
        playerName = GameObject.Find("InputPlayerName").GetComponent<InputField>().textComponent.text;
    }

    private void Update()
    {
        if (_isHopping || !GlobalVariables.run) return;
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
            if (transform.position.z % 1 != 0) zDiff = Mathf.Round(transform.position.z) - transform.position.z;
    
            MovePlayer(new Vector3(1, 0, zDiff));
            soundIsPlayed = false;
            // timeWithoutScoreIncrease = 0f;
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
            if (transform.position.z % 1 != 0) zDiff = Mathf.Round(transform.position.z) - transform.position.z;
            MovePlayer(new Vector3(-1, 0, zDiff));
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
            if (transform.position.x % 1 != 0) xDiff = Mathf.Round(transform.position.x) - transform.position.x;
            MovePlayer(new Vector3(xDiff, 0, 1));
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
            if (transform.position.x % 1 != 0) xDiff = Mathf.Round(transform.position.x) - transform.position.x;
            MovePlayer(new Vector3(xDiff, 0, -1));
            lastInput = 'D';
        }
    
        if (ScoreScript.Instance.GetScore() % 50 == 0 && ScoreScript.Instance.GetScore() != 0 && !soundIsPlayed)
        {
            soundIsPlayed = true;
    
            if (sound != null)
            {
                _audioSource.clip = sound;
                _audioSource.Play();
            }
        }
    
        
    
        if (transform.position.z < -15f || transform.position.z > 15f) KillPlayer();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.GetComponent<MovingObjectScript>() != null)
        {
            if (collision.collider.GetComponent<MovingObjectScript>().islog)
            {
                isOnLog = true;
                transform.parent = collision.collider.transform;
                AddReward(+1f);
            }
        }
        else
        {
            transform.parent = null;
            isOnLog = false;
        }
    }

    private void MovePlayer(Vector3 diff)
    {
        var newPosition = transform.position + diff;
        // normalize the position

        var results = new Collider[1];
        var size = Physics.OverlapBoxNonAlloc(newPosition, Vector3.one * 0.2f, results);
        if (size > 0)
        {
            if (results.Any(collider1 => collider1.CompareTag("Obstacle")))
            {
                AddReward(-1f);
                EndEpisode();
                return;
            }
            string[] tags = {"Truck", "Car", "Train", "FastCar", "MidCar", "BigCar", "LittleCar"};
            if (results.Any(collider1 => tags.Contains(collider1.tag)))
            {
                AddReward(-1f);
                EndEpisode();
            }
            else if (results.Any(collider1 => collider1.CompareTag("LillyPad")))
            {
                AddReward(+1f);
            }
            else if (results.Any(collider1 => collider1.CompareTag("Log")))
            {
                AddReward(+1f);
            }
        }
        
        // check if the player is going on the water
        
        foreach (var terrain in TerrainGenerator._currentTerrains
            .Where(terrain => Mathf.Approximately(terrain.transform.position.x, newPosition.x))
            .Where(terrain => terrain.CompareTag("Water")))
        {
            // check if the player is on a log or a lilypad
            if (isOnLog)
            {
                AddReward(+1f);
            }
            else
            {
                AddReward(-1f);
                EndEpisode();
            }
        }
        
        
        _animator.SetTrigger(Hop);
        _isHopping = true;
        var position = transform.position;
        transform.position = Vector3.Lerp(transform.position, newPosition, 1f);
        TerrainGenerator.SpawnTerrain(false, position);
        
        // get the prefab the player is on
        if (Mathf.Approximately(diff.x, 1))
        {
            timeWithoutScoreIncrease = 0f;
            _backwardsCount = 0;
            _scoreBuffer++;
            AddReward(+0.2f);
            if (_scoreBuffer > 0)
            {
                ScoreScript.Instance.UpdateScore();
                _scoreBuffer = 0;
                AddReward(+0.3f + ScoreScript.Instance.GetScore() / 200f);
                if (ScoreScript.Instance.GetScore() == bestScore)
                {
                    // Debug.Log("Score increased !: " + ScoreScript.Instance.GetScore());
                    bestScore++;
                    AddReward(+1f);
                    EndEpisode();
                }
            }
            foreach (var terrain in TerrainGenerator._currentTerrains
                         .Where(terrain1 => Mathf.Approximately(terrain1.transform.position.x, newPosition.x -1))
                         .Where(terrain => terrain.CompareTag("Water")
                                            || terrain.CompareTag("Railway")
                                            || terrain.CompareTag("Road")))
            {
                AddReward(+1f);
            }
            
        }
        else if (Mathf.Approximately(diff.x, -1))
        {
            _scoreBuffer--;
            _backwardsCount++;
            AddReward(-0.1f);
            if (_scoreBuffer < 0) AddReward(-0.33f);
        }
        else
        {
            AddReward(0.02f);
        }
        
        if (_backwardsCount >= 3){
            EagleScript eagleScript = Eagle.GetComponentInChildren<EagleScript>();
            eagleScript.CatchPlayer();
            SetReward(-1f);
            EndEpisode();
        }
        
        if (ScoreScript.Instance.isCounting){
            timeWithoutScoreIncrease += Time.deltaTime; 
            if (timeWithoutScoreIncrease >= maxTimeWithoutScore){
                EagleScript eagleScript = Eagle.GetComponentInChildren<EagleScript>();
                eagleScript.CatchPlayer();
                SetReward(-1f);
                EndEpisode();
            }
        }
        
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