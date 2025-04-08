using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using System.Text;
using UnityEngine;
using Random = UnityEngine.Random;

public class XMLJsonassignment : MonoBehaviour
{
    private string _dataPath;
    private string classData;
    private string _filePath;
    private string _xmlDataPath;
    private string _jsonDataPath;
    private List<classMate> classmates = new List<classMate>();
    private List<classMate> classmatesFromXml = new List<classMate>();
    
    // Start is called before the first frame update
    void Start()
    {
        /*
         * Classmates are chosen arbitrarily, with arbitrary names, dates of birth and pseudorandom generations of colors
         */
        classmates.Add(new classMate("Kim", "16/5/98", new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f))));
        classmates.Add(new classMate("Julie", "16/02/2010", new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f))));
        classmates.Add(new classMate("Patrice", "01/05/2002", new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f))));
        classmates.Add(new classMate("Malene", "03/03/2003", new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f))));
        classmates.Add(new classMate("Ellis", "26/08/1995", new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f))));
        classmates.Add(new classMate("Kombutcha", "10/09/1990", new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f))));
        
        _dataPath = Application.persistentDataPath + "/Assignment_data/";
        Debug.Log(_dataPath);
        NewDirectory();
        
        foreach (var mates in classmates)
        {
            string mateDesc = mates.Name + " " + mates.DOB + " " + mates.favColor;
            classData = classData + mateDesc + "\n";
        }
        Debug.Log("ClassData: " + classData); 
        _filePath = _dataPath + "classData.txt";
        _xmlDataPath = _dataPath + "classData.xml";
        _jsonDataPath = _dataPath + "classData.json";
        NewTextFile(); 
        WriteToXML(_xmlDataPath); 
        DeserializeXML(_xmlDataPath);
        SerializeJson(_jsonDataPath);
    }

    
    /*
     * 1. Create a directory somewhere on your computer
     */
    public void NewDirectory()
    {
        if (Directory.Exists(_dataPath)) 
        {
            Debug.Log("Directory already exists");
            return;
        }
        Directory.CreateDirectory(_dataPath);
        Debug.Log("Directory created");
    }
    /*
     * Create a text file with the information about classmates
     * Not technically necessary for the assignment or for the code, since the data is all stored internally.
     * But it helps me keep track.
     */
    public void NewTextFile()
    {
        if (File.Exists(_filePath))
        {
            Debug.Log("File already exists");
            return;
        }
        File.WriteAllText(_filePath, "<SAVE DATA> \n \n" + classData);

        Debug.Log("File created at" + _filePath);
    }
    
    /*
     * Writes a new file in xml, using a stream and returns the data
     */
    public void WriteToXML(string filename)
    {
        var serializer = new XmlSerializer(typeof(List<classMate>));
        using (var stream = new FileStream(filename, FileMode.Create))
        {
            serializer.Serialize(stream, classmates); 
        }
    }

    public void DeserializeXML(string filename)
    {
        if (File.Exists(filename)) 
        {
            var xmlSerializer = new XmlSerializer(typeof(List<classMate>));
            using (FileStream stream = File.OpenRead(filename))
            {
                classmatesFromXml = (List<classMate>)xmlSerializer.Deserialize(stream);
            }
            Debug.Log(classmatesFromXml.Count == classmates.Count);
        }
        
    }

    public void SerializeJson(string filename)
    {
        classMate.Class classJson = new classMate.Class();
        classJson.classMates = classmates;
        Debug.Log(classJson.classMates.Count);
        string jsonString = JsonUtility.ToJson(classJson, true);
        Debug.Log(jsonString);
        using (StreamWriter sw = File.CreateText(filename))
        {
            sw.WriteLine(jsonString);
        }
    }
}
