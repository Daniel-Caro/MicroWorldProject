using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeSceneScript : MonoBehaviour
{
    public GameObject buttonsPanel;
    public GameObject payPanel;
    public AudioSource noSound;

    private void Start() {
        if (!Globals.minigameAccess["Pipes"])
        {
            buttonsPanel.transform.Find("PipeButton").gameObject.GetComponent<Image>().color = Color.grey;
        }
        if (!Globals.minigameAccess["Frogger"])
        {
            buttonsPanel.transform.Find("FroggerButton").gameObject.GetComponent<Image>().color = Color.grey;
        }
        if (!Globals.minigameAccess["Association"])
        {
            buttonsPanel.transform.Find("AssociationButton").gameObject.GetComponent<Image>().color = Color.grey;
        }
        if (!Globals.minigameAccess["Memory"])
        {
            buttonsPanel.transform.Find("MemoryButton").gameObject.GetComponent<Image>().color = Color.grey;
        }
        if (!Globals.minigameAccess["Jumps"])
        {
            buttonsPanel.transform.Find("JumpButton").gameObject.GetComponent<Image>().color = Color.grey;
        }
        if (!Globals.minigameAccess["Flappy"])
        {
            buttonsPanel.transform.Find("FlappyButton").gameObject.GetComponent<Image>().color = Color.grey;
        }
    }

    public void PipeScene()
    {
        if (Globals.minigameAccess["Pipes"])
        {
            GameObject.Find("SampleSceneObject").SetActive(false);
            SceneManager.LoadScene("Tuberias", LoadSceneMode.Additive);
        }
        else
        {
            payMinigame("Pipes");
        }
    }

    public void FlappyScene()
    {
        if (Globals.minigameAccess["Flappy"])
        {
            GameObject.Find("SampleSceneObject").SetActive(false);
            SceneManager.LoadScene("minijuegovuelo", LoadSceneMode.Additive);
        }
        else
        {
            payMinigame("Flappy");
        }
    }

    public void SaltosScene()
    {
        if (Globals.minigameAccess["Jumps"])
        {
            GameObject.Find("SampleSceneObject").SetActive(false);
            SceneManager.LoadScene("saltosVerticalesScene", LoadSceneMode.Additive);
        }
        else
        {
            payMinigame("Jumps");
        }
    }

    public void FroggerScene()
    {
        if (Globals.minigameAccess["Frogger"])
        {
            GameObject.Find("SampleSceneObject").SetActive(false);
            SceneManager.LoadScene("froggerScene", LoadSceneMode.Additive);
        }
        else
        {
            payMinigame("Frogger");
        }
    }

    public void memoriaScene()
    {
        if (Globals.minigameAccess["Memory"])
        {
            GameObject.Find("SampleSceneObject").SetActive(false);
            SceneManager.LoadScene("memoriaScene", LoadSceneMode.Additive);
        }
        else
        {
            payMinigame("Memory");
        }
    }

    public void AssociationScene()
    {
        if (Globals.minigameAccess["Association"])
        {
            GameObject.Find("SampleSceneObject").SetActive(false);
            SceneManager.LoadScene("minijuego-asociacion", LoadSceneMode.Additive);
        }
        else
        {
            payMinigame("Association");
        }
    }

    private void payMinigame(string game)
    {
        string buttonName = null;
        switch (game)
        {
            case "Pipes":
                buttonName = "PipeButton";
                break;
            case "Flappy":
                buttonName = "FlappyButton";
                break;
            case "Jumps":
                buttonName = "JumpButton";
                break;
            case "Frogger":
                buttonName = "FroggerButton";
                break;
            case "Memory":
                buttonName = "MemoryButton";
                break;
            case "Association":
                buttonName = "AssociationButton";
                break;

        }
        payPanel.SetActive(true);
        Button button = payPanel.transform.Find("UnlockButton").gameObject.GetComponent<Button>();
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(delegate {
            if (Globals.gameResources["Coins"].currentR >= 1500)
            {
                Globals.gameResources["Coins"].DedactResources(1500);
                Globals.minigameAccess[game] = true;
                payPanel.SetActive(false);
                buttonsPanel.transform.Find(buttonName).gameObject.GetComponent<Image>().color = Color.white;
            }
            else
            {
                noSound.Play();
            }
        });
    }
}
