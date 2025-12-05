using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    public float movementSpeed = 10f;


    private Rigidbody2D rb;
    private Vector2 movement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        movement = new Vector2(
            Input.GetAxisRaw("Horizontal"), 
            Input.GetAxisRaw("Vertical")
        ).normalized;
    }

    void FixedUpdate()
    {
        rb.linearVelocity = movement * movementSpeed;
    }

    void OnTriggerEnter2D(Collider2D other)
{
    Harvestable harvestable = other.GetComponent<Harvestable>();
    if (harvestable != null)
    {
        harvestable.Collect();
    }
}
}
