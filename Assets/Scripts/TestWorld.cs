using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public class TestWorld : MonoBehaviour
{
    void Start()
    {
        //查找或创建一个System
        TestSystem test = World.DefaultGameObjectInjectionWorld.GetOrCreateSystem<TestSystem>();
        //增：创建一个世界 添加一个系统
        World temp = new World("new World");
        TestSystem test2 = World.DefaultGameObjectInjectionWorld.GetOrCreateSystem<TestSystem>();
        temp.AddSystem(test);
        //删：删除系统
        //默认系统组
        //1. InitiailzationSystemGroup
        //2. SimulationSystemGroup
        //3. PresentationSystemGroup
        InitializationSystemGroup tempGroup = World.DefaultGameObjectInjectionWorld.GetExistingSystem<InitializationSystemGroup>();
        tempGroup.RemoveSystemFromUpdateList(test2);//将系统移除组
        temp.DestroySystem(test2);
    }
}
