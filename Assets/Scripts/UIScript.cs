using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIScript : MonoBehaviour
{
    public int NextScene;
    public GameObject showmenu;
    public GameObject hidemenu;

    public void Changescreen()
    {
        SceneManager.LoadScene(NextScene);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ClickOpen()
    {
        showmenu.SetActive(true);
        hidemenu.SetActive(false);
    }

    public void ClickClose()
    {
        showmenu.SetActive(true);
    }
}