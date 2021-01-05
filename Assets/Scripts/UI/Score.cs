using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private TMP_Text _scoreDisplay;
    [SerializeField] private LevelManager _levelManager;

    private void OnEnable()
    {
        _player.ScoreChanged += OnScoreChanged;
    }

    private void OnDisable()
    {
        _player.ScoreChanged -= OnScoreChanged;
    }

    private void OnScoreChanged(int score)
    {
        _scoreDisplay.text = score.ToString();
    }
}
