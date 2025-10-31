using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    public float movementSpeed = 10f;

    private Rigidbody2D _rigidbody2D;
    private Vector2 _movement;

    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        _movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
    }

    void FixedUpdate()
    {
        _rigidbody2D.linearVelocity = _movement * movementSpeed;
    }
}
