using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;


public class AiScript : Agent
{
    [SerializeField] private TerrainGenerator TerrainGenerator;
    [SerializeField] private int goalScore = 30;
    private int _scoreBuffer = 0;
    private int score = 0;
    private bool isOnLog = false;

    public override void OnEpisodeBegin()
    {
        transform.localPosition = new Vector3(0, 1.03f, 0);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        if (actions.DiscreteActions[0] == 1)
        {
            if (isOnLog) AddReward(+1f);
            AddReward(+0.3f);
            MovePlayer(Vector3.forward);
            _scoreBuffer++;
            if (_scoreBuffer > 0)
            {
                score++;
                _scoreBuffer = 0;
                AddReward(0.4f);
                EndEpisode();
            }

            if (score == goalScore)
            {
                AddReward(0.9f);
                goalScore += 5;
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
        if (_isHopping) return;
        var actions = actionsOut.DiscreteActions;
        actions[0] = 0;
        if (Input.GetKey(KeyCode.W))
        {
            actions[0] = 1;
            AddReward(+0.5f);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            actions[0] = 2;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            actions[0] = 3;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            actions[0] = 4;
        }
    }

    private void MovePlayer(Vector3 diff)
    {
        var newPosition = transform.localPosition + diff;

        var colliders = Physics.OverlapBox(newPosition, Vector3.one * 0.2f);
        foreach (var collider in colliders)
            if (collider.CompareTag("Obstacle"))
            {
                AddReward(-0.2f);
                return;
            }

        var position = transform.localPosition;
        transform.localPosition = Vector3.Lerp(position, newPosition, 1f);
        TerrainGenerator.SpawnTerrain(false, position);
        if (!(transform.localPosition.x is < -5 or > 5 || transform.localPosition.z < -5)) return;
        AddReward(-0.5f);
        EndEpisode();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Goal")) return;
        SetReward(+1f);
        EndEpisode();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.GetComponent<MovingObjectScript>() != null)
        {
            if (collision.collider.GetComponent<MovingObjectScript>().islog)
            {
                transform.parent = collision.collider.transform;
                AddReward(+0.2f);
                isOnLog = true;
            }
        }
        else
        {
            transform.parent = null;
        }
    }

    public bool _isHopping { get; set; }
}