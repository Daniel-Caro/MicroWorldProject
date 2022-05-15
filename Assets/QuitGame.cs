using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGame : MonoBehaviour
{
    public void doExit()
    {
        SaveManager.SaveGameData();
        Application.Quit();
        Debug.Log("Game is exiting");
    }
}
