using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Players : MonoBehaviour
{
    [SerializeField] private bool isPlayerLeft;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed;
    private Vector2 startPos;
    private float movement;

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
    }
}
