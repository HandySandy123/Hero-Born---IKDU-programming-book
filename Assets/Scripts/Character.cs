using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character
{
    public string Name;
    public int Exp = 0;
    public Character()
    {
        this.Name = "Not assigned";
        this.Exp = 0;
    }

    public Character(string name)
    {
        Name = name;
    }

    public Character(string name, int exp)
    {
        Name = name;
        Exp = exp;
    }

    public virtual void PrintStatsInfo()
    {
        Debug.LogFormat("Hero: {0} - {1} XP", this.Name, this.Exp);
    }
}

public class Paladin : Character
{
    public Weapon PrimaryWeapon;

    public Paladin(string name, Weapon weapon) : base(name)
    {
        {
            PrimaryWeapon = weapon;
        }

    }
    public override void PrintStatsInfo()
    {
        Debug.LogFormat("Hail {0}, pick up thy {1}", this.Name, this.PrimaryWeapon.Name);
    }
}
   