using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainmenu : MonoBehaviour
{
    public void PlayGame()
    {
        Debug.Log("Play");
        SceneManager.LoadScene("Level-1");
    }
    public void QuitGame()
    {
    Debug.Log("QUIT");
    Application.Quit();
    }
    public void Levels()
    {
        SceneManager.LoadScene("Levels");
    }
}
