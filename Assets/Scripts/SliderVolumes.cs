using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using System;

public class SliderVolumes : MonoBehaviour
{
    [SerializeField] string volumeParameter = "";
    [SerializeField] AudioMixer mixer;
    [SerializeField] Slider slider;
    [SerializeField] float multiplier = 30f;

    private void Awake() //Quando valor muda
    {
        slider.onValueChanged.AddListener(HandleSliderValueChanged);
    }

    private void OnDisable() //Detetar parametro e mudar valor
    {
        PlayerPrefs.SetFloat(volumeParameter, slider.value);
    }

    private void HandleSliderValueChanged(float value) //Multiplicador de volume
    {
        mixer.SetFloat(volumeParameter, Mathf.Log10(value) * multiplier);
    }

    void Start() //Atualizar de volume
    {
        slider.value = PlayerPrefs.GetFloat(volumeParameter, slider.value);
    }
}
