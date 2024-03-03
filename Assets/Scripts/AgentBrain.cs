// AgentBrain.cs
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

/// <summary>
/// The brain of the agent. This is the ML-Agents file.
/// </summary>
/// 
public class AgentBrain : Agent
{
    private AgentController agentController;

    public override void OnEpisodeBegin()
    {
        if (agentController == null)
        {
            agentController = GetComponent<AgentController>();
            agentController.agentBrain = this;
        }
        agentController.pipeSpawner.ResetPipes();
        agentController.ResetAgent();
        Score.Instance.ResetScore();        
    }

    /// <summary>
    /// Collects observations from the environment and adds them to the sensor.
    /// </summary>
    /// <param name="sensor">The sensor to add the observations to.</param>
    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(agentController.getVelocity());
        sensor.AddObservation(agentController.transform.position.y);

        GameObject nextPipe = agentController.pipeSpawner.GetNextPipe();


        if(nextPipe != null){
            Transform scoreCollider = nextPipe.transform.Find("score_collider");
            Transform top_identifier = nextPipe.transform.Find("top identifier");
            Transform bottom_identifier = nextPipe.transform.Find("bot identifier");

            sensor.AddObservation(scoreCollider.position.y);
            sensor.AddObservation(scoreCollider.position.x - agentController.transform.position.x - 3.1625f/2f);
            sensor.AddObservation(Physics2D.Raycast(scoreCollider.position, Vector2.up, 100).distance / 100);
            sensor.AddObservation(Physics2D.Raycast(scoreCollider.position, Vector2.down, 100).distance / 100);

            sensor.AddObservation(top_identifier.position.y);
            sensor.AddObservation(bottom_identifier.position.y);
        }else{
            sensor.AddObservation(0);
            sensor.AddObservation(10);
            sensor.AddObservation(10 / 100);
            sensor.AddObservation(10 / 100);

            sensor.AddObservation(1.32f);
            sensor.AddObservation(-1.4f);
        }

        

        //rays der går op og ned
        int canCollideLayerMask = 1 << LayerMask.NameToLayer("CanCollide");
        sensor.AddObservation(Physics2D.Raycast(agentController.transform.position, Vector2.up, 100,canCollideLayerMask).distance);
        sensor.AddObservation(Physics2D.Raycast(agentController.transform.position, Vector2.down, 100, canCollideLayerMask).distance);

    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        var discreteActions = actions.DiscreteActions;
        
        if (discreteActions[0] == 1)
        {
            agentController.Jump();
        }
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var discreteActionsOut = actionsOut.DiscreteActions;
        discreteActionsOut[0] = Input.GetKey(KeyCode.Space) ? 1 : 0;
    }
}