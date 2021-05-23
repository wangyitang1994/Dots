
using Unity.Entities;
using UnityEngine;
using Unity.Transforms;
using Unity.Collections;

public class Scene01Test : MonoBehaviour {

    private void Start() {
        //1 手动创建一个实体
        Entity tempEntity = World.DefaultGameObjectInjectionWorld.EntityManager.CreateEntity(typeof(Scale));
        //2 复制了一个实体
        World.DefaultGameObjectInjectionWorld.EntityManager.Instantiate(tempEntity);
        
        //通过ArcheType创建实体 这种方式更适合大量实体的创建
        EntityArchetype tempArcheType = World.DefaultGameObjectInjectionWorld.EntityManager.CreateArchetype(typeof(TestEntity),typeof(RotationEulerXYZ));
        NativeArray<Entity> tempArray = new NativeArray<Entity>(4,Allocator.Temp);

        //3 批量创建实体 填满NativeArray(创建多个实体 并得到引用)
        World.DefaultGameObjectInjectionWorld.EntityManager.CreateEntity(tempArcheType,tempArray);
        //4 批量复制实体 填满NativeArray
        World.DefaultGameObjectInjectionWorld.EntityManager.Instantiate(tempEntity,tempArray);

        //5 查找实体
        //获取所有实体
        NativeArray<Entity> allEntity = World.DefaultGameObjectInjectionWorld.EntityManager.GetAllEntities();
        //创建查找
        EntityQuery entityQuery = World.DefaultGameObjectInjectionWorld.EntityManager.CreateEntityQuery(typeof(Scale));
        NativeArray<Entity> queryToArray = entityQuery.ToEntityArray(Allocator.TempJob);
        foreach (var item in queryToArray)
        {
            Debug.Log(item.Index);
        }
        queryToArray.Dispose();

        //删除实体
        // World.DefaultGameObjectInjectionWorld.EntityManager.DestroyEntity(tempEntity);
        // World.DefaultGameObjectInjectionWorld.EntityManager.DestroyEntity(tempArray);
        // World.DefaultGameObjectInjectionWorld.EntityManager.DestroyEntity(entityQuery);

        //添加组件
        World.DefaultGameObjectInjectionWorld.EntityManager.AddComponent(tempEntity,typeof(TestEntity));
        World.DefaultGameObjectInjectionWorld.EntityManager.AddComponent<TestEntity>(tempEntity);
        //批量添加组件
        World.DefaultGameObjectInjectionWorld.EntityManager.AddComponent(tempArray,typeof(TestEntity));
        World.DefaultGameObjectInjectionWorld.EntityManager.AddComponent<TestEntity>(tempArray);
        World.DefaultGameObjectInjectionWorld.EntityManager.AddComponent<TestEntity>(entityQuery);
        //一次添加多个组件
        World.DefaultGameObjectInjectionWorld.EntityManager.AddComponents(tempEntity,new ComponentTypes(typeof(TestEntity),typeof(RotationEulerXYZ)));
        //通过代码初始化组件 赋值
        World.DefaultGameObjectInjectionWorld.EntityManager.AddComponentData(tempEntity,new TestEntity{num = 1});

        //修改组件
        // Scale scale = World.DefaultGameObjectInjectionWorld.EntityManager.GetComponentData<Scale>(tempEntity);
        World.DefaultGameObjectInjectionWorld.EntityManager.SetComponentData(tempEntity,new Scale{Value = 3});

        //删除组件
        World.DefaultGameObjectInjectionWorld.EntityManager.RemoveComponent<Scale>(tempEntity);
        //批量删除组件
        World.DefaultGameObjectInjectionWorld.EntityManager.RemoveComponent<Scale>(tempArray);
        World.DefaultGameObjectInjectionWorld.EntityManager.RemoveComponent<Scale>(entityQuery);
    }
}