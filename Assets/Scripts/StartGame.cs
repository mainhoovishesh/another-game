
using UnityEngine;
using UnityEngine.SceneManagement;
public class StartGame : MonoBehaviour
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
    public void Mainmenu()
    {
        SceneManager.LoadScene("Start");
    }
}
