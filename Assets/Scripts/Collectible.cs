using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible
{
    public string name;
    
}

public class Potion : Collectible
{
    public Potion()
    {
        this.name = "Potion";
    }
}

public class Antidote : Collectible
{
    public Antidote()
    {
        this.name = "Antidote";
    }
}
