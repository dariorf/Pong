using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    [SerializeField] private bool isRedGoal;
    [SerializeField] private bool isPlayerLeftGoal;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        int goals = 1;

        if (isRedGoal)
        {
            goals = 3;
        }

        if (collision.gameObject.CompareTag("Ball"))
        {
            if (isPlayerLeftGoal)
            {
                // FindObjectOfType<GameManager>() is a reference to the GameManager script in the scene (there should only be one)
                FindObjectOfType<GameManager>().GoalScored(2, goals); // 2 is the player number
            }
            else
            {
                FindObjectOfType<GameManager>().GoalScored(1, goals); // 1 is the player number
            }
        }
    }
}
