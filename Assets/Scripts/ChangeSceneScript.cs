using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeSceneScript : MonoBehaviour
{
    public GameObject buttonsPanel;
    public GameObject chooseMinionsPanel;
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
        if (Globals.minigameAccess["Pipes"]) chooseMinion("Tuberias");
        else payMinigame("Pipes");
    }

    public void FlappyScene()
    {
        if (Globals.minigameAccess["Flappy"]) chooseMinion("minijuegovuelo");
        else payMinigame("Flappy");
    }

    public void SaltosScene()
    {
        if (Globals.minigameAccess["Jumps"]) chooseMinion("saltosVerticalesScene");
        else payMinigame("Jumps");
    }

    public void FroggerScene()
    {
        if (Globals.minigameAccess["Frogger"]) chooseMinion("froggerScene");
        else payMinigame("Frogger");
    }

    public void memoriaScene()
    {
        if (Globals.minigameAccess["Memory"]) chooseMinion("memoriaScene");
        else payMinigame("Memory");
    }

    public void AssociationScene()
    {
        if (Globals.minigameAccess["Association"]) chooseMinion("minijuego-asociacion");
        else payMinigame("Association");
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

    private void chooseMinion(string scene)
    {
        chooseMinionsPanel.SetActive(true);
        
        chooseMinionsPanel.transform.Find("miniontier1").gameObject.transform.Find("Quantity").gameObject.GetComponent<Text>().text = Globals.minionsQuantity[1].ToString();
        if (Globals.minionsQuantity[1] != 0)
        {
            chooseMinionsPanel.transform.Find("miniontier1").gameObject.GetComponent<Button>().interactable = true;
            chooseMinionsPanel.transform.Find("miniontier1").gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
            chooseMinionsPanel.transform.Find("miniontier1").gameObject.GetComponent<Button>().onClick.AddListener(() => loadScene(scene, 1));
        }
        else chooseMinionsPanel.transform.Find("miniontier1").gameObject.GetComponent<Button>().interactable = false;
        
        chooseMinionsPanel.transform.Find("miniontier2").gameObject.transform.Find("Quantity").gameObject.GetComponent<Text>().text = Globals.minionsQuantity[2].ToString();
        if (Globals.minionsQuantity[2] != 0)
        {
            chooseMinionsPanel.transform.Find("miniontier2").gameObject.GetComponent<Button>().interactable = true;
            chooseMinionsPanel.transform.Find("miniontier2").gameObject.GetComponent<Button>().onClick.AddListener(() => loadScene(scene, 2));
        }
        else chooseMinionsPanel.transform.Find("miniontier2").gameObject.GetComponent<Button>().interactable = false;

        chooseMinionsPanel.transform.Find("miniontier3").gameObject.transform.Find("Quantity").gameObject.GetComponent<Text>().text = Globals.minionsQuantity[3].ToString();
        if (Globals.minionsQuantity[3] != 0)
        {
            chooseMinionsPanel.transform.Find("miniontier3").gameObject.GetComponent<Button>().interactable = true;
            chooseMinionsPanel.transform.Find("miniontier3").gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
            chooseMinionsPanel.transform.Find("miniontier3").gameObject.GetComponent<Button>().onClick.AddListener(() => loadScene(scene, 3));
        }
        else chooseMinionsPanel.transform.Find("miniontier3").gameObject.GetComponent<Button>().interactable = false;

        chooseMinionsPanel.transform.Find("miniontier4").gameObject.transform.Find("Quantity").gameObject.GetComponent<Text>().text = Globals.minionsQuantity[4].ToString();
        if (Globals.minionsQuantity[4] != 0)
        {
            chooseMinionsPanel.transform.Find("miniontier4").gameObject.GetComponent<Button>().interactable = true;
            chooseMinionsPanel.transform.Find("miniontier4").gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
            chooseMinionsPanel.transform.Find("miniontier4").gameObject.GetComponent<Button>().onClick.AddListener(() => loadScene(scene, 4));
        }
        else chooseMinionsPanel.transform.Find("miniontier4").gameObject.GetComponent<Button>().interactable = false;


    }

    private void loadScene(string scene, int minion)
    {
        Globals.minionsQuantity[minion] = Globals.minionsQuantity[minion] - 1;
        Globals.gameResources["Minions"].DedactResources(1);
        chooseMinionsPanel.SetActive(false);
        if (minion == 3 || minion == 4) Globals.doubleCoinsBoost = true;
        if (minion == 2 || minion == 4) Globals.restartGameBoost = true;
        GameObject.Find("SampleSceneObject").SetActive(false);
        SceneManager.LoadScene(scene, LoadSceneMode.Additive);
    }
}
