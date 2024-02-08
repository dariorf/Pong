using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void Exit()
    {
        print("Salir");
        Application.Quit();
    }

    public void SinglePlayer()
    {
        Options.pvp = false;
    }

    public void MultiPlayer()
    {
        Options.pvp = true;
    }
}
