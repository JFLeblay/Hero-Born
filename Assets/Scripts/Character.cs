using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Weapon
{
    public string name;
    public int damage;

    public Weapon(string name, int damage)
    {
        this.name = name;
        this.damage = damage;
    }
    public void PrintStatsInfo()
    {
        Debug.LogFormat("Weapon: {0} - {1} DMB", this.name, this.damage);
    }
}
public class Character
{

    // Public properties
    public string name;
    public int exp = 0;

    // Constructors
    public Character()
    {
        this.name = "Not assigned";
    }
    public Character(string name)
    {
        this.name = name;
    }

    public void PrintStatsInfo()
    {
        Debug.LogFormat("Hero: {0} - {1} EXP", this.name, this.exp);
    }
}
