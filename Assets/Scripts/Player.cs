using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    private int _score = 0;

    public event UnityAction<int> ScoreChanged;

    public void AddScore(int score)
    {
        _score+=score;

        if (_score < 0)
        {
            _score = 0;
        }

        ScoreChanged?.Invoke(_score);
    }

    public void ZeroScore()
    {
        _score = 0;
        ScoreChanged?.Invoke(_score);
    }
}
