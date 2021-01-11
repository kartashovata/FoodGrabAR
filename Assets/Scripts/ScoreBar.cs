using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBar : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private Player _player;
    [SerializeField] private LevelManager _levelManager;
    [SerializeField] private float _colorChangingSpeed = 1f;
    [SerializeField] private Color _positiveColor;
    [SerializeField] private Color _negativeColor;

    private Image _sliderImage;
    private Color _sliderOrigColor;

    private void OnEnable()
    {
        _player.ScoreChanged += OnScoreChanged;
        _sliderImage = _slider.fillRect.GetComponent<Image>();
        _sliderOrigColor = _sliderImage.color;
    }

    private void OnDisable()
    {
        _player.ScoreChanged -= OnScoreChanged;
    }

    public void OnScoreChanged(int value)
    {
        float oldValue = _slider.value;
        _slider.value = (float)value / (float)_levelManager.CurrentLevel.WinScore;

        if (_slider.value > oldValue)
        {
            StartCoroutine(BlinkColor(_positiveColor));
        }
        else if (_slider.value < oldValue)
        {
            StartCoroutine(BlinkColor(_negativeColor));
        }
    }

    IEnumerator BlinkColor(Color goalColor)
    {
        float startTime = Time.time;

        for (int i = 0; i < 5; i++)
        {
            float t = (Time.time - startTime) + _colorChangingSpeed;
            _sliderImage.color = Color.Lerp(_sliderOrigColor, goalColor, t);
            yield return new WaitForSeconds(0.05f);
        }

        for (int i = 0; i < 5; i++)
        {
            float t = (Time.time - startTime) + _colorChangingSpeed;
            _sliderImage.color = Color.Lerp(goalColor, _sliderOrigColor, t);
            yield return new WaitForSeconds(0.05f);
        }
    }
}
