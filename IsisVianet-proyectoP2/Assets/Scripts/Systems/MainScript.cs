using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.VR;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

//Almacena las lecciones y cambia la escena
public class MainScript : MonoBehaviour
{
    public static MainScript instance;
    public string SelectedLesson = "dummy";


    [Header("External GameObject Configuration")]
    public GameObject creditos;

    private void Awake()
    {
        if (instance != null)
        {
            return;

        }
        else
        {
            instance = this;
        }
    }
    // identifica la leccion
    public void SetSelectedLesson(string lesson)
    {
        SelectedLesson = lesson;
        PlayerPrefs.SetString("SelectedLesson", SelectedLesson); //alamacena la leccion
    }

    //Este metodo sirve para pasar a la escena que contiene la leccion
    public void BeginGame()
    {
        SceneManager.LoadScene("Lesson");
    }

    //Activa la venta de los creditos.
    public void EnableWindow()
    {
        if (creditos.activeSelf)
        {
            //Desactiva el objeto si esta activo
            creditos.SetActive(false);
        }
        else
        {
            // Activa el objeto si está desactivado
           creditos.SetActive(true);
        }
    }
}
