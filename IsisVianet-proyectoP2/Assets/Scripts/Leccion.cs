using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
//Esta clase almacena todos los datos de las preguntas
public class Leccion 
{
    public int ID;//Almacena el identificador de la pregunta
    public string lessons;//Almacena el texto de la pregunta
    public List<string> options;//Alamcena la lista de opcines
    public int correctAnswer;//Almacena la respuesta correcta
}