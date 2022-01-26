using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    public void gotoLone()
    {
        SceneManager.LoadScene("Level-1");
    }
    public void gotoLtwo()
    {
        SceneManager.LoadScene("Level-2");
    }
}
