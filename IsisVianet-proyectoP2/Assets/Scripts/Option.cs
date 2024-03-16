using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OptionBtm : MonoBehaviour
{
    public int OptionID;//Esta variable almacena la respuesta correcta
    public string OptionName; //Esta variable asigna el texto en el boton

   
    //Este metodo sirve para actualizar el la pregunta en el ScriptableObject

    void Start()
    {
        transform.GetChild(0).GetComponent<TMP_Text>().text = OptionName;
    }

    public void UpdateText()
    {
        transform.GetChild(0).GetComponent<TMP_Text>().text = OptionName;
    }
    //Comprueba que se haya seleccionado una opción y llama a la función de SetPlayerAnswer y CheckPlayerState.
    public void SelectOptions()
    {
        //Se asigna la respuesta correcta al ID que se encuentra en el Levelmanager 
        LevelManager.Instance.SetPlayerAnswer(OptionID);
        // comprueba si en el levelmanager se selecciono una respuesta.
        LevelManager.Instance.CheckPlayerState();
    }
}
