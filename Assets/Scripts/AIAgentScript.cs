using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using Unity.VisualScripting;

public class AIAgentScript : Agent
{
  [SerializeField] private Transform targetTransform;

  [SerializeField] private TerrainGenerator TerrainGenerator;
  private Animator _animator;
  private bool _isHopping;
  private readonly static int Hop = Animator.StringToHash("hop");

  public override void OnEpisodeBegin()
  {
    _animator = GetComponent<Animator>();
    transform.position = new Vector3(1,1f,0);
  }

  public override void CollectObservations(VectorSensor sensor)
  {
    sensor.AddObservation(transform.position);
    sensor.AddObservation(targetTransform.position);
  }
  public override void OnActionReceived(ActionBuffers actions)
  {
    int move = actions.DiscreteActions[0];
    Debug.Log(move);
    if (_isHopping) return;
    switch (move)
    {
      case 0:
        break;
      case 1:
        MovePlayer(new Vector3(-1, 0, 0));
        break;
      case 2:
        MovePlayer(new Vector3(1, 0, 0));
        break;
      case 3:
        MovePlayer(new Vector3(0, 0, -1));
        break;
      case 4:
        MovePlayer(new Vector3(0, 0, 1));
        break;
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
        Debug.Log("Obstacle hit");
        SetReward(-1f);
        EndEpisode();
        return;
      }
    }

    _animator.SetTrigger(Hop);
    _isHopping = true;
    var position = transform.position;
    // transform.position = Vector3.Lerp(transform.position, newPosition, 1f);
    transform.position = newPosition;
    TerrainGenerator.SpawnTerrain(false, position);
  }

  public override void Heuristic(in ActionBuffers actionsOut)
  {
    ActionSegment<int> actions = actionsOut.DiscreteActions;
    // set the user inputs zqsd for actions
    
  }

  private void OnTriggerEnter(Collider other)
  {
    if (other.TryGetComponent<Goal>(out Goal sphere))
    {
      Debug.Log("Goal hit");
      // sphere.transform.position = sphere.transform.position + new Vector3(1,0,0);
      AddReward(1f);
      EndEpisode();
    }
    // look at a component with tag Obstacle
    if (other.CompareTag("Obstacle"))
    {
      Debug.Log("Obstacle hit");
      AddReward(-1f);
      EndEpisode();
    }
  }

  public void HopAnimationEnd()
  {
    _isHopping = false;
  }

  private void OnCollisionEnter(Collision collision){
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
}
