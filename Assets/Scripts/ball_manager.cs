using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ball_manager : MonoBehaviour
{
    public float speed = 50f;
    private Rigidbody2D rb;
    private Vector2 prev_speed;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        //Vector2 _rand_dir = Random.insideUnitCircle.normalized;
        Vector2 _rand_dir = Random.value > 0.5 ?  new Vector2(1,0.5f): new Vector2(1,-0.5f);
        rb.velocity = _rand_dir * speed;
        prev_speed = rb.velocity;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Vector2 _nrm = other.contacts[0].normal;
        Vector2 _ref_dir = Vector2.Reflect(prev_speed, _nrm).normalized;
        rb.velocity = _ref_dir * speed;
        prev_speed = rb.velocity;
    }
}
