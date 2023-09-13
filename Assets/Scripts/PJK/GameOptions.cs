using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameOptions : MonoBehaviour
{
    public GameObject optionBtn;
    public GameObject optionMenu;


    public void closeMenu()
    {
        optionMenu.SetActive(false);
        optionBtn.SetActive(true);
    }

    public void openMenu()
    {
        optionMenu.SetActive(true);
        optionBtn.SetActive(false);
    }

    public void ToStage()
    {
        MySceneManager.Instance.ChangeScene("StageScene");

        //SceneManager.LoadScene("StageScene");
        closeMenu();
    }

    public void ToMain()
    {
        MySceneManager.Instance.ChangeScene("StartScene");

        //SceneManager.LoadScene("StartScene");
        closeMenu();
    }
}
