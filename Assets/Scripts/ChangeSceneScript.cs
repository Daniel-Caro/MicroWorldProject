using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneScript : MonoBehaviour
{
    public void PipeScene()
    {
        SceneManager.LoadScene("Tuberias", LoadSceneMode.Single);
    }

    public void FlappyScene()
    {
        SceneManager.LoadScene("minijuegovuelo", LoadSceneMode.Single);
    }

    public void SaltosScene()
    {
        SceneManager.LoadScene("saltosVerticalesScene", LoadSceneMode.Single);
    }

    public void FroggerScene()
    {
        SceneManager.LoadScene("froggerScene", LoadSceneMode.Single);
    }

    public void memoriaScene()
    {
        SceneManager.LoadScene("memoriaScene", LoadSceneMode.Single);
    }

    public void AssociationScene()
    {
        SceneManager.LoadScene("minijuego-asociacion", LoadSceneMode.Single);
    }
}
