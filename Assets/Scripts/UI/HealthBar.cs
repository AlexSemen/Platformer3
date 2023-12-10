using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Slider _slider;
    [SerializeField] private Slider _sliderSmooth;

    private Coroutine _fadeInJob;
    private float _stepChangingVolume;
    private float _timeOneStepChangingVolume;
    private WaitForSeconds _waitForDelay;

    private void Awake()
    {
        _stepChangingVolume = 0.01f;
        _timeOneStepChangingVolume = 0.03f;
        _waitForDelay = new WaitForSeconds(_timeOneStepChangingVolume);
    }

    private void OnEnable()
    {
        _player.HealthChanged += HealthChanged;
    }

    private void OnDisable()
    {
        _player.HealthChanged -= HealthChanged;
    }

    private void HealthChanged(int value, int maxValue)
    {
        ChangedText(value, maxValue);
        ChangedSlider(value, maxValue);
        ChangedSliderSmooth(value, maxValue);
    }

    private void ChangedText(int value, int maxValue)
    {
        _text.text = value.ToString() + "/" + maxValue.ToString();
    }

    private void ChangedSlider(int value, int maxValue)
    {
        _slider.value = value;
        _slider.maxValue = maxValue;
    }

    private void ChangedSliderSmooth(int value, int maxValue)
    {
        if (_fadeInJob != null)
        {
            StopCoroutine(_fadeInJob);
        }

        _fadeInJob = StartCoroutine(ChangingVolume(value, maxValue));
    }

    private IEnumerator ChangingVolume(int value, int maxValue)
    {
        _sliderSmooth.maxValue = maxValue;

        while (_sliderSmooth.value != value)
        {
            if(_sliderSmooth.value > value)
            {
                _sliderSmooth.value -= _stepChangingVolume;
            }
            else
            {
                _sliderSmooth.value += _stepChangingVolume;
            }

            yield return _waitForDelay;
        }
    }
}

     
