using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private List<Level> _levels;
    [SerializeField] private Player _player;
    [SerializeField] private MenuUI _menu;
    [SerializeField] private CollectibleSpawner _spawner;

    public Level CurrentLevel { get; private set; }

    private int _currentLevelNumber;

    private void OnEnable()
    {
        _player.ScoreChanged += OnScoreChanged;
    }

    private void OnDisable()
    {
        _player.ScoreChanged -= OnScoreChanged;
    }

    private void Start()
    {
        SetLevel(0);
        Time.timeScale = 0;
    }

    private void OnScoreChanged(int score)
    {
        if (score >= CurrentLevel.WinScore)
        {
            FinishLevel();
        }
    }

    public void SetLevel(int index)
    {
        _currentLevelNumber = index;
        CurrentLevel = _levels[index];
        _menu.SetLevelDetails(index, CurrentLevel.Name, CurrentLevel.Description);
    }

    public void NextLevel()
    {
        _currentLevelNumber++;
        if (_currentLevelNumber < _levels.Count)
        {
            SetLevel(_currentLevelNumber);
            _menu.OpenNextLevelPanel();
        }
        else
        {
            _menu.OpenFinishedPanel();
        }        
    }

    public void StartLevel()
    {
        _player.ZeroScore();
        _spawner.Clear();
        _spawner.ConfigureAtLevelStart(CurrentLevel);
    }

    public void FinishLevel()
    {
        _spawner.StopSpawning();
        NextLevel();
    }


}

[System.Serializable]
public class Level
{
    public string Name;
    public string Description;
    public List<Types> GoodTypes;
    public int Reward;
    public List<Types> BadTypes;
    public int Fine;
    public int WinScore;
}