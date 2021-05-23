using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public class CreateWorld : MonoBehaviour
{
    void Start()
    {
        World temp = new World("new World");
        foreach (var item in World.All)
        {
            Debug.Log(item.Name);
        }
        temp.Dispose();
    }
}
