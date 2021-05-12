using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;

public class TestAuthoring : MonoBehaviour,IConvertGameObjectToEntity
{
    public float num;

    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem converstionSystem)
    {
        dstManager.AddComponentData(entity,new TestEntity(){num = num});
        dstManager.AddComponentData(entity,new RotationEulerXYZ(){Value = new Unity.Mathematics.float3(12,45,45)});
    }
}
