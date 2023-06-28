using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private GameObject menuPanel;
    private GameObject settingsPanel;
    public void Start()
    {
        menuPanel = GameObject.Find("MenuPanel");
        settingsPanel = GameObject.Find("SettingsPanel");
        settingsPanel.SetActive(false);
    }
    public void OnPlayButton()
    {
        SceneManager.LoadScene(1);
    }
    public void OnSettingsButton()
    {
        SwitchMenu();  
    }
    void SwitchMenu()
    {
        menuPanel.SetActive(!menuPanel.activeSelf);
        Debug.Log(settingsPanel);
        settingsPanel.SetActive(!settingsPanel.activeSelf);   
    }
    public void OnExitButton()
    {
        Application.Quit();
    }
}
