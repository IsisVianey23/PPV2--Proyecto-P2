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
        if (lessonContainer != null)
        {
            OnUpdateUI();
        }
        else
        {
            Debug.LogWarning("Revisa las variables TMP_Text");
        }
    }

    public void OnUpdateUI()
    {
        if (StageTitle != null || LessonStage != null)
        {
            StageTitle.text = "Leccion " + Lection;
            LessonStage.text = "Leccion " + CurrentLession + "de" + TotalLessions;

        }
        else
        {
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
