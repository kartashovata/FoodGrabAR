using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuUI : MonoBehaviour
{
    [SerializeField] private GameObject _mainMenuPanel;
    [SerializeField] private GameObject _nextLevelPanel;
    [SerializeField] private GameObject _finishedPanel;
    [SerializeField] private TMP_Text _levelNumber;
    [SerializeField] private TMP_Text _levelName;
    [SerializeField] private TMP_Text _levelDescription;
    [SerializeField] private AudioSource _menuMusic;
    [SerializeField] private AudioSource _gameMusic;

    public void SetLevelDetails(int levelNumber, string levelName, string levelDescription)
    {
        _levelName.text = levelName;
        _levelDescription.text = levelDescription;
        _levelNumber.text = "Level "+(levelNumber + 1).ToString();
    }

    public void OpenNextLevelPanel()
    {
        OpenPanel(_nextLevelPanel);
    }

    public void OpenFinishedPanel()
    {
        OpenPanel(_finishedPanel);
    }

    public void OpenPanel(GameObject panel)
    {
        panel.SetActive(true);
        Time.timeScale = 0;
    }

    public void ClosePanel(GameObject panel)
    {
        panel.SetActive(false);
        Time.timeScale = 1;
    }

    public void StopMusic(AudioSource musicToStop)
    {
        musicToStop.Stop();
    }

    public void PlayMusic(AudioSource musicToPlay)
    {
        musicToPlay.Play();
    }

    public void Exit()
    {
        Application.Quit();
    }
}
