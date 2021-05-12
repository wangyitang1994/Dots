using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class CreateCubeMgr : MonoBehaviour
{
    public GameObject cube;

    private void Start() {
        GameObjectConversionSettings setting = GameObjectConversionSettings.FromWorld(World.DefaultGameObjectInjectionWorld,null);
        Entity entity = GameObjectConversionUtility.ConvertGameObjectHierarchy(cube,setting);
        World.DefaultGameObjectInjectionWorld.EntityManager.Instantiate(entity);
    }
}
