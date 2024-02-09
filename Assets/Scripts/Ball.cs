using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Ball : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GameObject fire;
    [SerializeField] private float speed, increasedSpeed;
    [SerializeField] private float rotationSpeed = 45f;
    private Vector2 startPos;
    private float minSpeed = 0.5f;
    private float deltaSpeed = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        Launch();
    }

    // Update is called once per frame
    void Update()
    {
        fire.transform.rotation = Quaternion.Euler(0f, 0f, Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg + 90);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Adjust();

        Vector2 direccionColision = collision.contacts[0].normal;

        // Calcular el vector perpendicular a la dirección de la colisión y la direccion de la bola
        float angulo = Vector2.SignedAngle(direccionColision, rb.velocity);

        // Aplicar la rotación
        rb.angularVelocity = rotationSpeed * Mathf.Sign(angulo);
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
        rb.rotation = 0f;
        rb.angularVelocity = 0f;
        rotationSpeed = 45f;
        fire.GetComponent<SpriteRenderer>().enabled = false;
        Launch();
    }

    public void IncreaseSpeed()
    {
        Vector2 direction = rb.velocity.normalized;
        rb.velocity = direction * (rb.velocity.magnitude + increasedSpeed);
        rotationSpeed += 40f;
        fire.GetComponent<SpriteRenderer>().enabled = true;
    }
}
