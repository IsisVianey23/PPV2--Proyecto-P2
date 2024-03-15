using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "New Subject", menuName = "ScriptableObjects/NewLesson", order = 1)]

//Esta clase sirve para organizar las lecciones y las preguntas, pudiendo editarlas desde el Editor de Unity
public class Subject : ScriptableObject
{
    [Header("GameObject Configuration")]
    public int Lesson = 0;//Representa el número de lección iniciando en 0

    [Header("Lesson Quest Configuration")]
    public List<Leccion> leccionList;//almacena las preguntas relacionadas con la leccion
}
