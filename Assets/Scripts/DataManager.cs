using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Text;
using UnityEngine;

public class DataManager : MonoBehaviour, IManager
{
    private string _state;
    private string _dataPath;
    private string _textFile;
    private string _streamingTextFile;
    private string _xmlLevelProgress;
    private string _jsonWeapons;

    private string _xmlWeapons;
    private List<Weapon> _weaponsInventory = new List<Weapon>()
    {
        new Weapon("Sword of Doom", 100),
        new Weapon("Butterfly knives", 15),
        new Weapon("Brass Knuckles", 20)
    };

    void Awake()
    {
        _dataPath = Application.persistentDataPath + "/Player_data/";
        Debug.Log(_dataPath);
        _textFile = _dataPath + "Save_Data.txt";
        _streamingTextFile = _dataPath + "Streaming_Save_Data.txt";
        _xmlLevelProgress = _dataPath + "Progress_Data.xml";
        _xmlWeapons = _dataPath + "Weapons_Data.xml";  

        _jsonWeapons = _dataPath + "Weapons_Data.json";
    }
    public string State
    {
        get{return _state;}
        set{_state = value;}
    }
    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    public void Initialize(){
        _state = "Data Manager Initialized..";
        //Debug.Log(_state);

        FileSystemInfo();
        NewDirectory();
        SerializeJSON();
        //DeserializeJSON();
        SerializeXML();
        DeserializeXML();
        
        WriteToStream(_streamingTextFile);
        ReadFromStream(_streamingTextFile);

        WriteToXML(_xmlLevelProgress);

        //DeleteDirectory();
        NewTextFile();
        UpdateTextFile();
        ReadFromFile(_textFile);
    }

    private void DeleteDirectory()
    {
        if(!Directory.Exists(_dataPath))
        {
            Debug.Log("Directory does not exist");
            return;
        }
        Directory.Delete(_dataPath, true);
        Debug.Log("Directory successfully deleted");
    }

    private void FileSystemInfo()
    {
        Debug.LogFormat("Path seperator character: {0}", Path.PathSeparator);

        Debug.LogFormat("Directory seperator character: {0}", Path.DirectorySeparatorChar);

        Debug.LogFormat("Current Directory: {0}", Directory.GetCurrentDirectory());

        Debug.LogFormat("Temporary path: {0}", Path.GetTempPath());
    }

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

    public void NewTextFile()
    {
        if (File.Exists(_textFile))
        {
            Debug.Log("File already exists");
            return;
        }
        File.WriteAllText(_textFile, "<SAVE DATA> \n \n");

        Debug.Log("File created");
    }

    public void UpdateTextFile()
    {
        if (!File.Exists(_textFile))
        {
            Debug.Log("File doesn't exist. Initializing . . .  ");
            NewTextFile();
        }
        File.AppendAllText(_textFile, $"Game Started: {DateTime.Now}\n");

        Debug.Log("File updated successfully");
    }
    public void ReadFromFile(string fileName)
    {
        if(!File.Exists(fileName))
        {
            Debug.Log("File doesn't exist");
            return;
        }

        Debug.Log(File.ReadAllText(fileName));
    }

    public void DeleteFile(string Filename)
    {
        if(!File.Exists(Filename))
        {
            Debug.Log("File doesn't exist or has already been deleted");
            return;
        }
        File.Delete(Filename);
        Debug.Log("File deleted successfully");
    }

    public void WriteToStream(string Filename)
    {
        if(!File.Exists(Filename))
        {
            StreamWriter newStream = File.CreateText(Filename);
            newStream.WriteLine("<Save Data> for HERO BORN \n \n");
            newStream.Close();
            Debug.Log("New file created");
        }

        StreamWriter streamWriter = File.AppendText(Filename);

        streamWriter.WriteLine($"Game Ended: {DateTime.Now}");
        streamWriter.Close();
        Debug.Log("File updated with StreamWriter successfully");
    }

    public void ReadFromStream(string Filename)
    {
        if (!File.Exists(Filename))
        {
            Debug.Log("File doesn't exist");
            return;
        }
        StreamReader streamreader = new StreamReader(Filename);
        Debug.Log(streamreader.ReadToEnd());
    }

    public void WriteToXML(string filename)
    {
        if (!File.Exists(filename))
        {
            using (FileStream xmlStream = File.Create(filename))
            {
                XmlWriter xmlWriter = XmlWriter.Create(xmlStream);
                xmlWriter.WriteStartDocument();
                xmlWriter.WriteStartElement("level_progress");
                for (int i = 1; i < 5; i++)
                {
                    xmlWriter.WriteElementString("level", "Level-" + i);
                }
                xmlWriter.WriteEndElement();
                xmlWriter.Close();
            }
                
        }
    }

    public void SerializeXML()
    {
        var xmlSerializer = new XmlSerializer(typeof(List<Weapon>));
        using(FileStream stream = File.Create(_xmlWeapons))
        {
            xmlSerializer.Serialize(stream, _weaponsInventory);
        }
    }

    public void DeserializeXML()
    {
        if (File.Exists(_xmlWeapons))
        {
            var xmlSerializer = new XmlSerializer(typeof(List<Weapon>));
            using (FileStream stream = File.OpenRead(_xmlWeapons))
            {
                _weaponsInventory = (List<Weapon>)xmlSerializer.Deserialize(stream);
            }

            foreach (var weapon in _weaponsInventory)
            {
                Debug.LogFormat("Weapon: {0} - Damage: {1}", weapon.Name, weapon.Damage);
            }
        }
    }
    public void SerializeJSON()
    {
        //Weapon sword = new Weapon("Sword of Doom", 100);
        
        WeaponShop shop = new WeaponShop();
        shop.inventory = _weaponsInventory;
        string jsonString = JsonUtility.ToJson(shop, true);
        using(StreamWriter stream = File.CreateText(_jsonWeapons))
        {
            stream.WriteLine(jsonString);
        }
    }

    public void DeserializeJSON()
    {
        if (File.Exists(_jsonWeapons))
        {
            using (StreamReader stream = new StreamReader(_jsonWeapons))
            {
                string jsonString = stream.ReadToEnd();
                var weaponsData = JsonUtility.FromJson<WeaponShop>(jsonString);

                foreach (var weapon in weaponsData.inventory)
                {
                    Debug.LogFormat("Weapon: {0} - Damage: {1}", weapon.Name, weapon.Damage);
                }
            }
        }
    }
}
