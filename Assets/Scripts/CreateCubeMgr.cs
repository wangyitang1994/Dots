using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

public class CreateCubeMgr : MonoBehaviour
{
    public GameObject cube;
    public float interval;

    private void Start() {
        GameObjectConversionSettings setting = GameObjectConversionSettings.FromWorld(World.DefaultGameObjectInjectionWorld,null);
        Entity entity = GameObjectConversionUtility.ConvertGameObjectHierarchy(cube,setting);
        EntityManager mgr = World.DefaultGameObjectInjectionWorld.EntityManager;
        Translation translation = new Translation();
        for (int i = 0; i < 100; i++)
        {
            for (int j = 0; j < 100; j++)
            {
                Entity tmp = mgr.Instantiate(entity);
                mgr.SetComponentData(tmp,translation);
                translation.Value.x += interval;
            }
            translation.Value.x = 0;
            translation.Value.y += interval;
        }
    }
}
