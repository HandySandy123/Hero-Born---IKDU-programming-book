using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct Weapon
{
    public string Name;
    public int Damage;

    public Weapon(string name, int damage)
    {
        this.Name = name;
        this .Damage = damage;
    }

    public void PrintWeaponStats()
    {
        Debug.LogFormat("Name: {0}, damage {1}", this.Name, this.Damage);
    }
}

[Serializable]
public class WeaponShop
{
    public List<Weapon> inventory;
}