using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball_manager : MonoBehaviour
{
    public float minSpeed = 5f;
    public float maxSpeed = 25f;
    [Range(0.1f,5f)]
    public float inc_speed_step;
    private float speed;

    private Rigidbody2D rb;
    private Vector2 prev_speed;
    private Vector2 original_pos;

    public void ResetWithDelay()
    {
        speed = minSpeed;
        transform.position = original_pos;
        rb.velocity = Vector2.zero;
        FunctionTimer.Create(ResetBall, 2f);
    }

    private void ResetBall()
    {
        Vector2[] _dirs = {new Vector2(1, 0.5f), new Vector2(1, -0.5f), new Vector2(-1, 0.5f), new Vector2(-1, -0.5f)};
        Vector2 _rand_dir = _dirs[Random.Range(0,4)];
        rb.velocity = _rand_dir * speed;
        prev_speed = rb.velocity;
    }

    void Start()
    {
        original_pos = transform.position;
        rb = gameObject.GetComponent<Rigidbody2D>();
        ResetWithDelay();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Player"))
            speed = Mathf.Clamp(speed + inc_speed_step, minSpeed, maxSpeed);
        Vector2 _nrm = other.contacts[0].normal;
        Vector2 _ref_dir = Vector2.Reflect(prev_speed, _nrm).normalized;
        rb.velocity = _ref_dir * speed;
        prev_speed = rb.velocity;
    }
}
