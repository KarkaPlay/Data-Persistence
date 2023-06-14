using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    public Button newButton, loadButton;
    public void Start()
    {
        if (SceneManager.GetActiveScene().name == "Menu")
        {
            DataManager.Instance.inputField = GameObject.Find("InputField").GetComponent<InputField>();
            DataManager.Instance.bestScoreText = GameObject.Find("BestScoreText").GetComponent<Text>();
            
            newButton.onClick.AddListener(NewButtonClicked);
            loadButton.onClick.AddListener(LoadButtonClicked);
        }
    }

    public void NewButtonClicked()
    {
        DataManager.Instance.NewPlayer();
    }
    
    public void LoadButtonClicked()
    {
        DataManager.Instance.LoadData();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("main");
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Quit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
