using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Ball : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed, increasedSpeed;
    [SerializeField] private float timeWindow; // Tiempo de reaccion para pulsar Espacio
    private Vector2 startPos;
    private float minSpeed = 0.5f;
    private float deltaSpeed = 0.5f;    
    private bool canIncreaseSpeed = false;
    private float collisionTime;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        Launch();
    }

    // Update is called once per frame
    void Update()
    {
        if (canIncreaseSpeed && Input.GetKeyDown(KeyCode.Space))
        {
            if (Time.time <= collisionTime + timeWindow)
            {
                IncreaseSpeed();
            }
            canIncreaseSpeed = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Adjust();

            canIncreaseSpeed = true;

            collisionTime = Time.time;
        }
    }

    private void Launch()
    {
        float x = Random.Range(0, 2) == 0 ? -1 : 1; // Randomly choose -1 or 1
        float y = Random.Range(0, 2) == 0 ? -1 : 1; // Randomly choose -1 or 1
        rb.velocity = new Vector2(speed * x, speed * y);
    }

    private void Adjust()
    {
        if (Mathf.Abs(rb.velocity.x) < minSpeed)
        {
            deltaSpeed = Random.value < 0.5f ? deltaSpeed : -deltaSpeed;
            rb.velocity = new Vector2(rb.velocity.x + deltaSpeed, rb.velocity.y);
        }
        if (Mathf.Abs(rb.velocity.y) < minSpeed)
        {
            deltaSpeed = Random.value < 0.5f ? deltaSpeed : -deltaSpeed;
            rb.velocity += new Vector2(0f, deltaSpeed);
        }
    }

    public void Reset()
    {
        rb.position = startPos;
        Launch();
    }

    private void IncreaseSpeed()
    {
        Vector2 direction = rb.velocity.normalized;
        rb.velocity = direction * (rb.velocity.magnitude + increasedSpeed);
    }
}
