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
        NativeArray<Entity> queryToArray = tempQuery.ToEntityArray(Allocator.TempJob);
        foreach (var item in queryToArray)
        {
            Debug.Log($"{EntityManager.GetName(item)}");
        }
        queryToArray.Dispose();


        // NativeArray //数组
        // NativeHashMap //字典
        // NativeMultiHashMap
        //NativeQueue //队列

        //Temp JobTemp persistent
//生命周期 1帧   4帧     永久
//          快         慢
    }

    protected override void OnUpdate()
    {
        // OnUpdate不满足条件则不会运行
        //ref : 可读可写
        //in  : 只读
        Entities.ForEach((ref Translation trans,in TestEntity test) => {
            trans.Value = new float3(1,2,3);
            Debug.Log($"{test.num}");
        })
        // .WithAll<Scale,Rotation>()//查找包含组件 (&&)
        // .WithAny<Scale,Rotation>()//查找包含组件,任意一个即可 (||)
        // .WithNone<Scale,Rotation>()//查找不包含所有组件
        // .WithChangeFilter<TestEntity>()//xx组件变化时执行
        // .WithSharedComponentFilter(new TestShare() {data = 5})//筛选data = 5 的共享组件
        // .WithStoreEntityQueryInField(ref tempQuery)//把查询到的结果 放到EntityQuery里
        // .Run()//主线程上运行
        .WithName("QQQ")//
        // .Schedule()//开一个Thread执行
        .ScheduleParallel()//多Thread执行
        ;
        Debug.Log("OnUpdate");
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