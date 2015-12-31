using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class ObjectPool : MonoBehaviour{

    Stack<GameObject> pool;

    [SerializeField]
    GameObject prototype;

    [SerializeField]
    int maxPool;

    int currentPool = 0;

    void Start()
    {
        pool = new Stack<GameObject>();
    }
    public GameObject RequestObject()
    {
        if (prototype == null)
        {
            Debug.Log("Please set the prototype for the Object Pool");
            return null;
        }
        // if number of object in pool is empty
        if (pool.Count <= 0)
        {
            // try to create a new object then return
            // check the max pool first before create a new
            if (currentPool < maxPool)
            {
                currentPool++;
                return (GameObject)Instantiate(prototype);
            }
            else
            {
                return null;
            }
        }
        else
        {
            return pool.Pop();
        }
    }
    public void ReturnObject(GameObject newObject)
    {
        newObject.SetActive(false);
        pool.Push(newObject);
    }
}
