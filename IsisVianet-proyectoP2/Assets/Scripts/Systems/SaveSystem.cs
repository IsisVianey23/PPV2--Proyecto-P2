using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

//El script más importante.
//Es un sistema de guardado
public class SaveSystem : MonoBehaviour
{
    public static SaveSystem Instance;

    //public Leccion data;
   // public SubjectContainer subject;
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

    private void Start()
    {
        CreateFile("Posicion", ".data");

        Debug.Log(ReadFile("Posicion", ".data"));
       
    }
    public void CreateFile(string _name, string _extension)
    {
        //Definir el path del archivo
        string path = Application.dataPath + "/" + _name + _extension;
        //Revisar si el archivo en el path no existe
        if (!File.Exists(path))
        {
            //creamos el contenido
            string content = "Login Date:" + System.DateTime.Now + "\n";
            string position = "x: " + transform .position.x + ",y: " + transform.position.y;
            //Almcenamos la informacion
            File.AppendAllText(path, position);
        }
        else
        {
            Debug.LogWarning("Atención: Estás tratando de crear un archivo con el mismo nombre[" + _name + _extension + "], verifica tu información");
        }
    }

    string ReadFile(string _fileName, string _extension)
    { 
        //Acceder al path del archivo
        string path = Application.dataPath + "/Resources/" + _fileName + _extension;
        //Si el archivo exite manda su informacion
        string data = "";
        if (File.Exists(path))
        {
            data = File.ReadAllText(path);
        }
        return data;
    }

    public void SaveToJSON(string _fileName, object _data)
    {
        if (_data != null)
        {
            string JSONData = JsonUtility.ToJson(_data, true);

            if (JSONData.Length != 0)
            {
                Debug.Log("JSON STRING: " + JSONData);
                string fileName = _fileName + ".json";
                string filePath = Path.Combine(Application.dataPath + "/Resources/JSONS/", fileName);
                File.WriteAllText(filePath, JSONData);
                Debug.Log("JSON almacenado en la direccion:" + filePath);
            }
            else
            {
                Debug.LogWarning("ERROR - FileSystem : JSONData is empty, check for local variable [string JSONData]");

            }
        }
        else 
        {
            Debug.LogWarning("ERROR - FileSystem : _data is null, check for param [object _data]");
        }
    }
}
