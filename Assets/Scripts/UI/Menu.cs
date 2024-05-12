using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] private string _nameSceneGame;
    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private GameObject _settingsMenu;

    private void Awake()
    {
        _mainMenu.SetActive(true);
        _settingsMenu.SetActive(false);
    }

    public void Game()
    {
        SceneManager.LoadScene(_nameSceneGame);
    }
    
    public void Settings()
    {
        _mainMenu.SetActive(false);
        _settingsMenu.SetActive(true);
    }
    
    public void Back()
    {
        _mainMenu.SetActive(true);
        _settingsMenu.SetActive(false);
    }
    
    public void Exit()
    {
        UnityEditor.EditorApplication.isPlaying = false;
    }
}
