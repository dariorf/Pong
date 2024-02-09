using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Players : MonoBehaviour
{
    [SerializeField] private GameObject rival, ball;
    [SerializeField] private bool isPlayerLeft;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed;
    private Vector2 startPos;
    private float movement;
    private bool canHit = true;

    // Start is called before the first frame update
    void Start()
    {
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

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball") && canHit && Input.GetKey(KeyCode.Space)) 
        {            
            canHit = false;
            rival.GetComponent<Players>().canHit = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!canHit)
        {
            ball.GetComponent<Ball>().IncreaseSpeed();
        }
    }
}
