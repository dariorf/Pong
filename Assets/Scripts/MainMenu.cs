using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject choicesMenu;
    [SerializeField] private GameObject[] balls;

    private void Start()
    {
        LaunchBalls();
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void SinglePlayer()
    {
        Options.pvp = false;
        ToChoices();
    }

    public void MultiPlayer()
    {
        Options.pvp = true;
        ToChoices();
    }

    public void ToChoices()
    {
        GetComponent<Canvas>().enabled = false;
        choicesMenu.GetComponent<Canvas>().enabled = true;
    }

    public void LaunchBalls()
    {
        float speed = 3f;
        foreach (var ball in balls)
        {
            float x = Random.Range(0, 2);
            float y = Random.Range(0, 2);
            ball.GetComponent<Rigidbody2D>().velocity = new Vector2(speed * x, speed * y);
        }
    }
}
