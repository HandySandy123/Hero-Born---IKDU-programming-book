using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyable<T> : MonoBehaviour where T : MonoBehaviour
{
    public int onScreenDelay;
    
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, onScreenDelay);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
