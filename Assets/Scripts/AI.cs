using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private GameObject ball, rival;
    [SerializeField] private AudioSource normalHit, heavyHit;
    [SerializeField] private float limit = 4.67f;
    private Vector2 ballPosition;
    private Vector2 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        ballPosition = ball.transform.position; // Posición de la pelota
        Vector2 aiPosition = transform.position;

        if (transform.position.y > ballPosition.y) // Si la posición de la IA es mayor que la de la pelota, se mueve hacia abajo
        {
            aiPosition.y = Mathf.Clamp(aiPosition.y - 1 * speed * Time.deltaTime, -limit, limit);
            transform.position = aiPosition;
        }
        else if (transform.position.y < ballPosition.y) // Si la posición de la IA es menor que la de la pelota, se mueve hacia arriba
        {
            aiPosition.y = Mathf.Clamp(aiPosition.y + 1 * speed * Time.deltaTime, -limit, limit);
            transform.position = aiPosition;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            bool hit = Random.Range(0, 2) == 0 ? true : false;

            if (hit)
            {
                ball.GetComponent<Ball>().IncreaseSpeed();
                PlaySound(heavyHit);
            }
            else
            {
                PlaySound(normalHit);
            }

            rival.GetComponent<Players>().canHit = true;
            rival.GetComponent<Players>().hasHit = false;
        }
    }

    private void PlaySound(AudioSource soundEffect)
    {
        soundEffect.Play();
    }

    public void Reset()
    {
        transform.position = startPos;
    }
}
