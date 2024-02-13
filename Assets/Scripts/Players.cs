using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Collections;
using UnityEngine;

public class Players : MonoBehaviour
{
    [SerializeField] private GameObject rival, ball;
    [SerializeField] private AudioSource normalHit, heavyHit;
    [SerializeField] private bool isPlayerLeft;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed;
    private Vector2 startPos;
    private float movement;
    public bool canHit = true;
    public bool hasHit = false;
    private bool pvp;

    // Start is called before the first frame update
    void Start()
    {
        pvp = FindObjectOfType<GameManager>().pvp;
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerLeft)
        {
            movement = Input.GetAxisRaw("VerticalLeft");
        }
        else
        {
            movement = Input.GetAxisRaw("VerticalRight");
        }

        rb.velocity = new Vector2(rb.velocity.x, movement * speed);
    }

    public void Reset()
    {
        rb.position = startPos;
        canHit = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            canHit = false;

            if (pvp)
            {
                rival.GetComponent<Players>().canHit = true;
                rival.GetComponent<Players>().hasHit = false;
            }

            if (hasHit)
            {
                ball.GetComponent<Ball>().IncreaseSpeed();
                PlaySound(heavyHit);
            }
            else
            {
                PlaySound(normalHit);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball") && canHit && Input.GetKey(KeyCode.Space))
        {            
            canHit = false;
            hasHit = true;            
        }
    }

    private void PlaySound(AudioSource soundEffect)
    {
        soundEffect.Play();
    }
}
