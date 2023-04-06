using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public const string MENU = "Menu";
    public const string GAME = "Game";

    public static void LoadMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadSceneAsync(MENU);
    }
    public static void LoadGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadSceneAsync(GAME);
    }
}
