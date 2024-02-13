using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI winnerText;
    public static string winner = "Player 1";
    private Color[] colors = { Color.red, Color.blue, Color.yellow, Color.green, Color.magenta };
    private int i = 0;

    private void Awake()
    {
        winnerText.text = winner + "\nWINS!";
    }

    private void Start()
    {
        StartCoroutine(ChangeColor());
    }

    private IEnumerator ChangeColor()
    {
        while (true)
        {
            i = i + 1 >= colors.Length ? 0 : i + 1;
            winnerText.color = colors[i];
            yield return new WaitForSeconds(0.5f);
        }
    }

    public void ToMainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenuScene");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
