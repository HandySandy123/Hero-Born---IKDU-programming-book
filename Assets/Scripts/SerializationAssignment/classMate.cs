using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class classMate
{
    public string Name;
    public string DOB;
    public Color favColor;

    public classMate(string name, string dob, Color favColor)
    {
        Name = name;
        DOB = dob;
        this.favColor = favColor;
    }

    public classMate()
    {
             
    }
    
    [System.Serializable]
    public struct Class
    {
        public List<classMate> classMates;
    }
}
