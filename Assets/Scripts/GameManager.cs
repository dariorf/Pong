using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject ball, playerLeft, playerRight; // These are set in the inspector
    public int playerLeftScore, playerRightScore; // These are set in the inspector
    [SerializeField] private TMPro.TextMeshProUGUI playerLeftScoreText, playerRightScoreText; // These are set in the inspector

    public bool pvp;

    // This method is called when the ball enters the goal area (the trigger collider)
    public void GoalScored(int playerNumber, int goals)
    {
        if (playerNumber == 1)
        {
            playerLeftScore += goals;
            playerLeftScoreText.text = playerLeftScore.ToString();
        }
        else if (playerNumber == 2)
        {
            playerRightScore += goals;
            playerRightScoreText.text = playerRightScore.ToString();
        }
        
        ResetPositions();
    }

    // This method is called to reset the ball and players to their starting positions
    private void ResetPositions()
    {
        ball.GetComponent<Ball>().Reset();
        playerLeft.GetComponent<Players>().Reset();
        if (pvp)
        {
            playerRight.GetComponent<Players>().Reset();
        }
    }
}
