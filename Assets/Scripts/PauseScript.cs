using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript : MonoBehaviour
{
    //Variáveis
    public GameObject hiddenmenu;
    bool condicao = false;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) //Detetar tecla ESCAPE
        {
            if(condicao == false) //Abrir menu de pausa
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                hiddenmenu.SetActive(true);
                condicao = true;
                Time.timeScale = 0;
            }
            else //Fechar menu de pausa
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;          
                hiddenmenu.SetActive(false);
                condicao = false;
                Time.timeScale = 1;
            }
        }
    }
}
