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
        ScoreChanged?.Invoke(_score);
    }
}
