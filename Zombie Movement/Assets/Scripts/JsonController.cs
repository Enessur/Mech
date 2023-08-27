using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class JsonController : MonoBehaviour
{
   public SwordStats sword = new SwordStats("Long Sword",40,30);

   private void Start()
   {
      JsonSave();
     // JsonLoad();
   }

   public void JsonSave()
   {
      string jsonString = JsonUtility.ToJson(sword);
      File.WriteAllText(Application.dataPath+"/Save/SwordJson.Json",jsonString);
   }

   public void JsonLoad()
   {
      string path = Application.dataPath + "/Save/SwordJson.Json";

      if (File.Exists(path))
      {
         string jsonRead = File.ReadAllText(path);
         sword = JsonUtility.FromJson<SwordStats>(jsonRead);
      }
      else
      {
         Debug.Log("File doesn't exist");
      }
   }
}
