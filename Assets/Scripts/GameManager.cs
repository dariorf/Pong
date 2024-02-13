using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

// TODO: Una vez se marca gol, dejar de comprobar Triggers

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject ball, playerLeft, playerRight; // These are set in the inspector
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Canvas goalCanvas, pauseCanvas;
    [SerializeField] private AudioSource whistle, celebration;
    public int playerLeftScore, playerRightScore; // These are set in the inspector
    [SerializeField] private TMPro.TextMeshProUGUI playerLeftScoreText, playerRightScoreText; // These are set in the inspector
    private int maxGoals;
    public bool pvp, scored;

    private void Awake()
    {
        ball.GetComponent<SpriteRenderer>().sprite = Options.ballType;
        maxGoals = Options.maxGoals;
        pvp = Options.pvp;
        scored = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0;
            pauseCanvas.enabled = true;
        }
    }

    public void GoalScored(int playerNumber, int goals)
    {
        if (!scored)
        {
            scored = true;
            StartCoroutine(ChangeCamera(playerNumber, goals));
            whistle.Play();
            celebration.Play();
        }
    }

    private IEnumerator ChangeCamera(int playerNumber, int goals)
    {
        ball.GetComponent<Ball>().ReduceSpeed();
        mainCamera.GetComponent<GameCamera>().goalScored = true;
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

        goalCanvas.GetComponentInChildren<TextMeshProUGUI>().text = "PLAYER " + playerNumber + "\nGOAL!";
        goalCanvas.enabled = true;

        yield return new WaitForSeconds(1.5f);

        if (playerLeftScore >= maxGoals)
        {
            GameOver.winner = "Player 1";
            UnityEngine.SceneManagement.SceneManager.LoadScene("GameOverScene");
        }

        if (playerRightScore >= maxGoals)
        {
            GameOver.winner = "Player 2";
            UnityEngine.SceneManagement.SceneManager.LoadScene("GameOverScene");
        }

        ResetPositions();
    }

    private void ResetPositions()
    {
        goalCanvas.enabled = false;
        ball.GetComponent<Ball>().Reset();
        playerLeft.GetComponent<Players>().Reset();
        if (pvp)
        {
            playerRight.GetComponent<Players>().Reset();
        }
        else
        {
            playerRight.GetComponent<AI>().Reset();
        }
        mainCamera.GetComponent<GameCamera>().Reset();
        scored = false;
    }
}
