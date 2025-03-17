using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour, IManager
{
    private string _state;
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
        Debug.Log(_state);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
