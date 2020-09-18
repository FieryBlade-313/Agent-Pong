using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bar_controller : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 5f;

    private float _vertDir = 0f;
    private Rigidbody2D rb;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        _vertDir = Input.GetAxis("Vertical");
    }
    private void FixedUpdate()
    {
        rb.MovePosition((Vector2)transform.position + new Vector2(0, _vertDir * moveSpeed * Time.deltaTime));
    }
}
