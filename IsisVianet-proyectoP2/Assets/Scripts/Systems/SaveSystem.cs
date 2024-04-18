using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UIElements;

//El script más importante.
//Es un sistema de guardado
public class SaveSystem : MonoBehaviour
{
    //Creamos una instancia para 
    public static SaveSystem Instance;

    public Leccion data;
    public SubjectContainer subject;
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

        subject = LoadFromJSON<SubjectContainer>(PlayerPrefs.GetString("SelectedLesson"));
    }

    private void Start()
    {
        //SaveToJSON("LeccionDummy", data);

        ////CreateFile("Posicion", ".data");

        //Debug.Log(ReadFile("Posicion", ".data"));

        //subject = LoadFromJSON<SubjectContainer>("LeccionDummy");
       
    }

    //Proporciona una ruta donde se guardaran los archivos
    public void CreateFile(string _name, string _extension)
    {
        //Definir la ruta del archivo
        string path = Application.dataPath + "/" + _name + _extension;
        //Revisar si el archivo en el path no existe
        if (!File.Exists(path))
        {
            //creamos el contenido
            string content = "Login Date:" + System.DateTime.Now + "\n";
            string position = "x: " + transform .position.x + ",y: " + transform.position.y;
            //Almcenamos la informacion
            File.AppendAllText(path, position); //Si el archivo ya existe, emite una advertencia 
        }
        else
        {
            Debug.LogWarning("Atención: Estás tratando de crear un archivo con el mismo nombre[" + _name + _extension + "], verifica tu información");
        }
    }

    //Este metodo lee el nombre del archivo y especifica el nombre y la extencion
    string ReadFile(string _fileName, string _extension)
    { 
        //Acceder al path del archivo
        string path = Application.dataPath + "/StreamAssets/" + _fileName + _extension;
        //Si el archivo exite manda su informacion
        string data = "";
        if (File.Exists(path)) //verifica si existe
        {
            data = File.ReadAllText(path); //si existe, lee su contenido
        }
        return data; 
    }

    //Convierte los archivos en tipo JSON y los almacena
    public void SaveToJSON(string _fileName, object _data)
    {
        if (_data != null) //Comprueba si data no es nulo
        {
            string JSONData = JsonUtility.ToJson(_data, true);
            //si no es nulo lo convierte en una cadena JSON

            if (JSONData.Length != 0) //Comprueba si la cadena no esta vacia
            {
                Debug.Log("JSON STRING: " + JSONData);
                string fileName = _fileName + ".json"; //Si no esta vacia tendra la extension .json
                //Combina la ruta del archivo con la carpeta que se especifique usando PathCombine.
                string filePath = Path.Combine(Application.dataPath + "/StreamAssets/", fileName);
                File.WriteAllText(filePath, JSONData);
                //Si se ha almacenado correctamente mandara este mensaje
                Debug.Log("JSON almacenado en la direccion:" + filePath);
            }
            else
            {
                Debug.LogWarning("ERROR - FileSystem : JSONData is empty, check for local variable [string JSONData]");
                //Si la cadena JSON está vacía, emite una advertencia

            }
        }
        else 
        {
            Debug.LogWarning("ERROR - FileSystem : _data is null, check for param [object _data]");
        }
    }

    //Carga los datos de los JSONS
    public T LoadFromJSON<T>(string _fileName) where T : new()
    {
        T Dato = new T(); //Crea una nueva instancia de tipo T
        string path = Application.dataPath + "/StreamAssets/" + _fileName + ".json"; //ruta del archivo
        string JSONData = "";
        if (File.Exists(path)) //verifica si existe en la ruta
        {
            JSONData = File.ReadAllText(path);//si el archivo existe lee el contenido
            Debug.Log("JSON STRING: " + JSONData);
        }
        else 
        {
            Debug.LogWarning("ERROR: FileSystem: path doesnt exist, check for local variable [string JSONData];"); 
            //si no existe manda este mensaje
        }
        //Verifica si la información se cargo
        if (JSONData.Length != 0)
        {
           JsonUtility.FromJsonOverwrite(JSONData, Dato);
        }
        else
        {
            //si esta vacia la cadena manda este mensaje
            Debug.LogWarning("ERROR: FileSystem: JSONData is empty, check for local variable [string JSONData];");
        }
        return Dato;//devuelve el contenido deserializado del JSON
    }
}
