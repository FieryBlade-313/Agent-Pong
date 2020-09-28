using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bar_controller : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 5f;

    private float _vertDir = 0f;
    public float reward = 0f;
    public Text score;

    private Rigidbody2D rb;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _vertDir = Input.GetAxis("Vertical");
    }
    private void FixedUpdate()
    {
        rb.MovePosition((Vector2)transform.position + new Vector2(0, _vertDir * moveSpeed * Time.deltaTime));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ball"))
            addReward(0.05f);
    }

    public void addReward(float reward_value)
    {
        reward += reward_value;
        //print(reward);
        score.text = "Score : " + string.Format("{0:#0.0}", reward);
    }
}
