using Unity.Entities;
using UnityEngine;
using Unity.Transforms;
using Unity.Mathematics;
using Unity.Collections;

public class LifeCycle : SystemBase
{
    EntityQuery tempQuery;

    protected override void OnCreate()
    {
        Debug.Log("OnCreate");
    }

    protected override void OnStartRunning()
    {
        Debug.Log("OnStartRunning");
        // NativeArray<Entity> queryToArray = tempQuery.ToEntityArray(Allocator.TempJob);
        // foreach (var item in queryToArray)
        // {
        //     Debug.Log($"{EntityManager.GetName(item)}");
        // }
        // queryToArray.Dispose();


        // NativeArray //数组
        // NativeHashMap //字典
        // NativeMultiHashMap
        //NativeQueue //队列

        //Temp JobTemp persistent
//生命周期 1帧   4帧     永久
//效率      快         慢
    }

    protected override void OnUpdate()
    {
        // Test1();
        // Test2();
        JobWithCode();
        Debug.Log("OnUpdate");
    }

    void Test1(){
        NativeArray<int> tempArray = new NativeArray<int>(5,Allocator.TempJob);
        // OnUpdate不满足条件则不会运行
        //ref : 可读可写
        //in  : 只读
        Entities.ForEach((ref Translation trans,in TestEntity test) => {
            trans.Value = new float3(1,2,3);
            Debug.Log($"{test.num}");
            tempArray[0] = 5;
        })
        // .WithAll<Scale,Rotation>()//查找包含组件 (&&)
        // .WithAny<Scale,Rotation>()//查找包含组件,任意一个即可 (||)
        // .WithNone<Scale,Rotation>()//查找不包含所有组件
        // .WithChangeFilter<TestEntity>()//xx组件变化时执行
        // .WithSharedComponentFilter(new TestShare() {data = 5})//筛选data = 5 的共享组件
        .WithStoreEntityQueryInField(ref tempQuery)//把查询到的结果 放到EntityQuery里
        // .Run()//主线程上运行
        .WithName("QQQ")//
        // .WithNativeDisableContainerSafetyRestriction(tempArray)//关闭线程安全规则
        // .Schedule()//开一个Thread执行
        .WithDeallocateOnJobCompletion(tempArray)//线程执行完后自动释放变量
        .ScheduleParallel()//多Thread执行
        ;


        //线程执行完后再运行
        // CompleteDependency();
        // tempArray.Dispose();
    }

    void Test2(){
        //添加组件1
        if(false)
            Entities.ForEach((Entity entity,ref Translation trans) => {
                EntityManager.AddComponentData(entity,new TestEntity(){num = 20});
            })
            .WithStructuralChanges()
            .WithoutBurst()
            .Run()
            ;
        //添加组件2(这种效率更好)
        if(true){
            EntityCommandBuffer buffer = new EntityCommandBuffer(Allocator.TempJob);
            Entities.ForEach((Entity entity,ref Translation trans) => {
                buffer.AddComponent(entity,new TestEntity(){num = 52});
            })
            .WithStructuralChanges()
            .WithoutBurst()
            .Run()
            ;
            buffer.Playback(EntityManager);
            buffer.Dispose();
        }
            
    }

    //针对一段逻辑 多线程运行
    void JobWithCode(){
        NativeArray<float> tempArray = new NativeArray<float>(1000,Allocator.TempJob);
        Job.WithCode(() => {
            for (int i = 0; i < tempArray.Length; i++)
            {
                tempArray[i] = i;
            }
        })
        .Schedule();
        CompleteDependency();
        // foreach (var item in tempArray)
        // {
        //     Debug.Log(item);
        // }
        tempArray.Dispose();
    }

    protected override void OnStopRunning()
    {
        Debug.Log("OnStopRunning");
    }

    protected override void OnDestroy()
    {
        Debug.Log("OnDestroy");
    }
}