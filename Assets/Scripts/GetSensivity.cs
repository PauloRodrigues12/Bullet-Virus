using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetSensivity : MonoBehaviour
{
    [SerializeField] string sensParameter = "Sensivity";
    [SerializeField] Slider slider;
    public FPSController fpsController;
    public Text sensText;

    public void textUpdate(float value)
    {
        sensText.text = Mathf.Round(slider.value) + "";
    }
    private void OnDisable() //Detetar parametro e mudar valor
    {
        PlayerPrefs.SetFloat(sensParameter, slider.value);
    }

    void Start()
    {
        slider.value = PlayerPrefs.GetFloat(sensParameter, slider.value);
    }
    void Update()
    {
        fpsController.mouseSensitivity = slider.value;
    }
}