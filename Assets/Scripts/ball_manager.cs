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
    private bool isReseting;
    public bool left = false;

    public void ResetWithDelay()
    {
        isReseting = true;
        speed = minSpeed;
        transform.position = original_pos;
        rb.velocity = Vector2.zero;
        FunctionTimer.Create(ResetBall, 2f);
    }

    private void ResetBall()
    {
        Vector2[] _dirs = {new Vector2(1, 0.5f), new Vector2(1, -0.5f), new Vector2(-1, 0.5f), new Vector2(-1, -0.5f)};
        //Vector2 _rand_dir = new Vector2(1,Random.value);
        //if (left)
        //    _rand_dir = new Vector2(-1, Random.value);
        Vector2 _rand_dir = new Vector2(1, Mathf.Sin(Random.Range(-Mathf.PI / 3.0f, Mathf.PI / 3.0f)));
        if (Random.value > 0.5f)
            _rand_dir.x = -1;
;       rb.velocity = _rand_dir * speed;
        prev_speed = rb.velocity;
        isReseting = false;
    }
    private void Awake()
    {
        left = Random.value > 0.5f;
        isReseting = true;
    }
    private void FixedUpdate()
    {
        if (!isReseting && rb.velocity.magnitude < minSpeed)
        {
            ResetWithDelay();
        }
    }
    void Start()
    {
        original_pos = transform.position;
        rb = gameObject.GetComponent<Rigidbody2D>();
        ResetWithDelay();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (!isReseting)
        {
            if (other.gameObject.CompareTag("Player"))
                speed = Mathf.Clamp(speed + inc_speed_step, minSpeed, maxSpeed);
            Vector2 _nrm = other.contacts[0].normal;
            Vector2 _ref_dir = Vector2.Reflect(prev_speed, _nrm).normalized;
            rb.velocity = _ref_dir * speed;
            prev_speed = rb.velocity;
        }
    }

    public Vector2 getVelocity()
    {
        return rb.velocity;
    }

    public Vector2 getPos()
    {
        return (Vector2)transform.position;
    }
}
