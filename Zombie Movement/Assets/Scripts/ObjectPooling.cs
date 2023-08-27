using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Script;
using UnityEngine;

public class ObjectPooling : Singleton<ObjectPooling>
{
    [Serializable]
    public class ObjectPair
    {
        public GameObject prefab;
        public int amount;
        public List<GameObject> items = new();
    }
    [SerializeField] private GameObject objectPrefab; // TODO: remove after multiple system ready
    [SerializeField] private int amount = 10;
    [SerializeField] private List<GameObject> items = new ();
    [SerializeField] private List<ObjectPair> objectPairs = new ();
    protected override void Awake()
    {
        base.Awake();
        Initialize();
    }

    private void Initialize() // init for multiple game objects
    {
        for (int i = 0; i < amount; i++)
        {
            items.Add(CreateObject(objectPrefab));
        }
    }
    public GameObject GetObject() //game object parameter
    {
        if (items == null || items.Count == 0) //items.any
        {
            return CreateObject(objectPrefab);
        }

        var item = items.FirstOrDefault();
        items.Remove(item);
        item.SetActive(true);
        return item;
    }
    
    
    private GameObject CreateObject(GameObject prefab)
    {
       var item = Instantiate(prefab);
       item.SetActive(false);
        return item;
    }

    public void DestroyItem(GameObject curveGameObject)
    {
        curveGameObject.SetActive(false);
        items.Add(curveGameObject);
    }
}
