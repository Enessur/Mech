using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

  
public class SwordStats
{
    
    
     public string Name;
     public int Value;
     public int Level;
    
     
    public SwordStats()
    {
        
    }

    public SwordStats(string name, int value, int level)
    {
        this.Name = name;
        this.Value = value;
        this.Level = level;
    }

}
