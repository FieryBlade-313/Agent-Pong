using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using UnityEngine;
using UnityEngine.UI;

public class Bar_controller : Agent
{
    [SerializeField]
    private float moveSpeed = 5f;

    private float _vertDir = 0f;
    private float reward = 0f;
    public Text score;
    public int max_ral = 1;
    private int curr = 0;

    [SerializeField]
    private Bar_controller enemy_controller;
    [SerializeField]
    private Ball_manager bm;
    [SerializeField]
    private Transform[] edges;

    private Vector2 orig_pos;

    private Rigidbody2D rb;
    
    public override void Initialize()
    {
        orig_pos = transform.position;
        rb = GetComponent<Rigidbody2D>();
    }

    public override void OnEpisodeBegin()
    {
        curr = 0;
        transform.position = orig_pos;
        reward = 0f;
    }

    public override void OnActionReceived(float[] vectorAction)
    {
        switch(Mathf.FloorToInt(vectorAction[0]))
        {
            case 0: { _vertDir = -1f; addReward(-0.01f); }break;
            case 2: { _vertDir = 1f; addReward(-0.01f); } break;
            default: _vertDir = 0;break;
        }
        Move();
    }

    private void FixedUpdate()
    {
        if (bm.getVelocity().x < 0 && getPos().x < enemy_controller.getPos().x)
            RequestDecision();
        if (bm.getVelocity().x > 0 && getPos().x > enemy_controller.getPos().x)
            RequestDecision();
    }

    public override void Heuristic(float[] actionsOut)
    {
        actionsOut[0] = Input.GetAxis("Vertical")+1;
    }
    private void Move()
    {
        rb.MovePosition((Vector2)transform.position + new Vector2(0, _vertDir * moveSpeed * Time.deltaTime));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ball"))
            addReward(0.25f);
    }

    public void addReward(float reward_value)
    {
        reward += reward_value;
        //print(reward);
        AddReward(reward_value);
        score.text = "Score : " + string.Format("{0:#0.0}", reward);
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        Vector2 ball_pos = bm.getPos();
        Vector2 enm_pos = enemy_controller.getPos();
        Vector2 self_pos = getPos();

        sensor.AddObservation(enemy_controller.getVelocity());
        sensor.AddObservation(getVelocity());
        sensor.AddObservation(bm.getVelocity());
        //sensor.AddObservation(ball_pos);
        sensor.AddObservation(enm_pos-self_pos);
        sensor.AddObservation(edges[0].position.y-self_pos.y);
        sensor.AddObservation(self_pos.y- edges[1].position.y);
        sensor.AddObservation(ball_pos - self_pos);
        sensor.AddObservation(edges[0].position.y - ball_pos.y);
        sensor.AddObservation(ball_pos.y - edges[1].position.y);
    }

    public Vector2 getPos()
    {
        return (Vector2)transform.position;
    }

    public float getVelocity()
    {
        return rb.velocity.y;
    }

    public void Increase_curr()
    {
        curr += 1;
        //print(curr+" "+max_ral);
        if (curr >= max_ral)
        {
            //print("Episode End");
            EndEpisode();
        }
    }
}
