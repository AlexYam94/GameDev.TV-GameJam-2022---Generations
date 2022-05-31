using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : Singleton<SceneController>, ISavable
{
    [SerializeField] int _gameOverSceneIndex;

    public void LoadNextScene()
    {
        StartCoroutine(LoadNexSceneCoroutine());
    }

    public void StartGame()
    {
        LoadNextScene();
    }

    public void GameOver()
    {
        SceneManager.LoadScene(_gameOverSceneIndex);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Continue()
    {
        Load();
    }

    public void Load()
    {
        StartCoroutine(LoadCoroutine());
        //int index = PlayerPrefs.GetInt("lastSceneIndex",-1);
        //if(index < 0) return;
        //SceneManager.LoadScene(index);
    }

    public void Save()
    {
        //TODO: Save Scene to PlayerPrefs
        PlayerPrefs.SetInt("lastSceneIndex", SceneManager.GetActiveScene().buildIndex);
    }
    IEnumerator LoadCoroutine()
    {
        int index = PlayerPrefs.GetInt("lastSceneIndex", -1);
        if (index < 0) yield break;
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(index);

        while (!asyncOperation.isDone)
        {
            yield return null;
        }
        //if (SceneManager.GetActiveScene().name.ToLower().CompareTo("gameover") != 0)
        //    SavingController.GetInstance().Save();
    }

    IEnumerator LoadNexSceneCoroutine()
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;


        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(currentIndex + 1);

        while (!asyncOperation.isDone)
        {
            yield return new WaitForEndOfFrame();
        }
        //if (SceneManager.GetActiveScene().name.ToLower().CompareTo("gameover") != 0)
        //{
        //    PlayerPrefs.SetInt("lastSceneIndex", currentIndex + 1);
        //    SavingController.GetInstance().Save();
        //}
    }
}
