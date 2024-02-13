using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Choices : MonoBehaviour
{
    [SerializeField] private GameObject ballType, maxGoals, mainMenu;
    [SerializeField] private Sprite[] ballTypes;
    [SerializeField] private int[] maxGoalsOptions;
    private int currentType = 0;
    private int currentGoals = 0;

    public void MoveBallTypeRight()
    {
        currentType = currentType + 1 >= ballTypes.Length ? 0 : currentType + 1;
        ballType.GetComponent<Image>().sprite = ballTypes[currentType];
    }

    public void MoveBallTypeLeft()
    {
        currentType = currentType - 1 < 0 ? ballTypes.Length -1 : currentType - 1;
        ballType.GetComponent<Image>().sprite = ballTypes[currentType];
    }

    public void IncreaseMaxGoals()
    {
        currentGoals = currentGoals + 1 >= maxGoalsOptions.Length ? 0 : currentGoals + 1;
        maxGoals.GetComponent<TextMeshProUGUI>().text = maxGoalsOptions[currentGoals].ToString();
    }

    public void DecreaseMaxGoals()
    {
        currentGoals = currentGoals - 1 < 0 ? maxGoalsOptions.Length - 1 : currentGoals - 1;
        maxGoals.GetComponent<TextMeshProUGUI>().text = maxGoalsOptions[currentGoals].ToString();
    }

    public void BackToMainMenu()
    {
        GetComponent<Canvas>().enabled = false;
        currentGoals = 0;
        currentType = 0;
        mainMenu.GetComponent<Canvas>().enabled = true;
        maxGoals.GetComponent<TextMeshProUGUI>().text = maxGoalsOptions[currentGoals].ToString();
        ballType.GetComponent<Image>().sprite = ballTypes[currentType];
    }

    public void Play()
    {
        Options.ballType = ballTypes[currentType];
        Options.maxGoals = maxGoalsOptions[currentGoals];
        Invoke("ToPlay", 0.2f);        
    }

    private void ToPlay()
    {
        if (Options.pvp)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("PVPScene");
        } else
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("PVEScene");
        }
        
    }

}
