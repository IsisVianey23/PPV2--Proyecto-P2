using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LessonContainer : MonoBehaviour
{

    [Header("GameObject Configuration")]
    public int Lection = 0; //Almacena el número de la lección atual
    public int CurrentLession = 0; //Indica la lección actual
    public int TotalLessions = 0;//esta variable sirve para ver el total de lecciones
    public bool AreAllLessonsComplete = false; //Almacena si todas las lecciones han sido completadas

    [Header("UI Configuration")]
    public TMP_Text StageTitle;//Alamcena el texto del titulo
    public TMP_Text LessonStage; //Almacena el texto de la leccion

    [Header("External GameObject Configuration")]
    public GameObject lessonContainer;

    [Header("Lesson Data")]
    public ScriptableObject lesson; //es un objeto que va a servir para almacenar las lecciones (las preguntas)
    public string LessonName;
    void Start()
    {
        //verifica si no es nula
        if (lessonContainer != null)
        {
            //llama al metodo para actualizar la interfaz de la leccion
            OnUpdateUI();
        }
        else
        {
            //si es nula manda este mensaje 
            Debug.LogWarning("Revisa las variables TMP_Text");
        }
    }

    //Actualiza los textos de las lecciones y su numero
    public void OnUpdateUI()
    {
        if (StageTitle != null || LessonStage != null) //verifica si no son nulas
        {
            StageTitle.text = "Leccion " + Lection; //Actualiza el texto
            LessonStage.text = "Leccion " + CurrentLession + "de" + TotalLessions; //actualiza el numero de la leccion

        }
        else
        {
            //Si las variables son nulas emite este mensaje.
            Debug.LogWarning("GameObject Nulo, revisa las variables de tipo TMP_Text");
        }
    }
    //Este metodo activa y desactiva la ventana de lessonContainer
    public void EnableWindow()
    {
        OnUpdateUI();

        if (lessonContainer.activeSelf)
        {
            //Desactiva el objeto si esta activo
            lessonContainer.SetActive(false);
        }
        else
        {
            // Activa el objeto si está desactivado
            lessonContainer.SetActive(true);
            MainScript.instance.SetSelectedLesson(LessonName);
        }
    }
}
