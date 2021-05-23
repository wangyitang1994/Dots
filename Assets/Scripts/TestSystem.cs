using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

// [DisableAutoCreation] //禁用自动创建
[UpdateInGroup(typeof(InitializationSystemGroup))] //将系统放入组
public class TestSystem : ComponentSystem
{
    protected override void OnUpdate()
    {
        // Entities.ForEach((ref TestEntity item) => {
        //     Debug.Log(item.num);
        // });
        // Entities.ForEach((ref Translation trans) => {
        //     trans.Value = new float3(5,5,5);
        // });
    }
}