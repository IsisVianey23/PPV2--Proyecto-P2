using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    [Header("Level Data")]
    public SubjectContainer subject;

    //Esta variables almacenan 
    [Header("User Interface")]
    public TMP_Text QuestionTxt;//Muestra en texto las preguntas
    public TMP_Text livesTXt;//Esta variable sirve para mostrar el contador de vidas
    public List<OptionBtm> Options;//Almacena la lista de opciones
    public GameObject CheckButton;//Almacena el botón de comprobar
    public GameObject AnswerContainer;//Almacena la contenedor donde se mostrará si la respuesta es correcta
    public Color red;
    public Color green;

    //Estas variables almacenan la configuracion de las preguntas
    [Header("Game Configuration")]
    public int questionAmount = 0;//Almacena el número total de preguntas iniciando en el 0
    public int currentQuestion = 0;//Almacena el orden de las preguntas iniciando en 0
    public string question;//Almacena las preguntas
    public string correctAnswer; //Almacena la respuesta correcta.
    public int answerFromPlayer = 9; //esta variable da la respuesta del jugador
    public int lives = 5;//cantidad de vidas que tiene el jugador iniciando en 5

    [Header("Current Lesson")]
    public Leccion currentLesson;//Establece la lección actual

    //PATRON SINGLETO ES UN PATRON DE DISEÑO, ENCARGADO DE CREAR UNA INSTANCIA DE LA CLASE 
    //PARA SER REFERENCIA DA EN OTRA CLASE SIN LA NECESIDAD DE DECLARAR LAS VARIABLES

    private void Awake()
    {
        if (Instance != null)
        {
            return;
        }
        else
        { 
            Instance = this;
        }
    }

    //Este método configura la cantidad de preguntas y las inicializa.
    void Start()
    {
        subject = SaveSystem.Instance.subject;
        //Establecemos la cantidad de preguntas en la leccion para 
        //seleccionarlas y actualizarlas
        questionAmount = subject.leccionList.Count;
        //se llama a la funcion para comprobar si se ha
        //seleccionado una pocion
        LoadQuestion();
        CheckPlayerState();
    }

    private void LoadQuestion()
    {
        //Aseguramos que la pregunta actual esta dentro de los limites
        if (currentQuestion < questionAmount)
        {
            //Establecemos la leccíon actual
            currentLesson = subject.leccionList[currentQuestion];
            //Establecemos la pregunta
            question = currentLesson.lessons;
            //Establecemos la respuesta correcta
            correctAnswer = currentLesson.options[currentLesson.correctAnswer];
            //Establecemos la pregunta en UI
            QuestionTxt.text = question;
            //Establecemos las Opciones
            for (int i = 0; i < currentLesson.options.Count; i++)
            { 
                //Recorre todas las opciones de la leccion actual
                Options[i].GetComponent<OptionBtm>().OptionName = currentLesson.options[i];
                //Para cada opción, configura el nombre de la opción y su identificador 
                Options[i].GetComponent<OptionBtm>().OptionID = i;
                //Actualiza el texto del botón
                Options[i].GetComponent<OptionBtm>().UpdateText();
            }
        }
        else
        {
            //Si llegamos al final de las preguntas se mostrará en la consola este mensaje
            Debug.Log("Fin de las preguntas");

        }
    }

    //En este metodo verificamos si las respuestas son correctas o incorrectas
    //dependiendo de la respuesta el AnswerContainer se desglosara con los colores verde o rojo,
    //y el contador de vida disminuirá o se mantendrá cuando pasemos a la siguiente pregunta.
    public void NextQuestion()
    {
        if (CheckPlayerState())
        {

            bool isCorrect = currentLesson.options[answerFromPlayer] == correctAnswer;

            AnswerContainer.SetActive(true);

            if (isCorrect)
            {
                //El contenedor es verde si la respuesta es correcta
                //y //y se desglosara el texto que indique que es correcto
                AnswerContainer.GetComponent<Image>().color = Color.green;
                Debug.Log("Respuesta Correcta" + ": " + correctAnswer);
            }
            else
            {
                //El contenedor sera rojo si la respuesta es incorrecta
                //y se desglosara el texto que lo indique que es incorrecto
                AnswerContainer.GetComponent<Image>().color = Color.red;
                Debug.Log("Respuesta Incorrecta.  " + question + ": " + correctAnswer);
                lives--;
            }

            //actializar el contador de vida
            livesTXt.text = lives.ToString();
            //Incrementamos el indice de la pregunta actual
            currentQuestion++;
            StartCoroutine(ShowResultAndLoadQuestion(isCorrect));
            answerFromPlayer = 9;
        }
        else
        {
            //cambio de escena
        }
        
    }

    //Este metodo sirve para mostrar y ocultar el AnswerContainer en un periodo establecido
    //para luego cargar la siguiente pregunta
    private IEnumerator ShowResultAndLoadQuestion(bool isCorrect)
    {
        //Ajesta el tiempo en el que se mostrará el AnswerContainer
        yield return new WaitForSeconds(2.5f);
        //Oculta el AnswerContainer
        AnswerContainer.SetActive(false);
        //Carga la nueva pregunta
        LoadQuestion();

        //Activa el CheckButtom (El botón de comprobar respuestas)
        CheckPlayerState();
    }

    public void SetPlayerAnswer(int _answer)
    {
        answerFromPlayer = _answer;
    }

    //Este método verifica si el jugador ha presionado una opción o no
    //cambiando la apariencia del CheckButtom.
    public bool CheckPlayerState()
    {

        if (answerFromPlayer != 9)
        {
            //Si ha habido interacción con el CheckButtom (Comprobar)
            //el botón va a resaltar.
            CheckButton.GetComponent<Button>().interactable = true;
            CheckButton.GetComponent<Image>().color = Color.white;
            return true;
        }
        else
        {
            //Si no ha habido interacción el CheckButtom (Comprobar)
            //se mantendrá gris.
            CheckButton.GetComponent<Button>().interactable = false;
            CheckButton.GetComponent<Image>().color = Color.grey;
            return false;
        }
    }
    
}
