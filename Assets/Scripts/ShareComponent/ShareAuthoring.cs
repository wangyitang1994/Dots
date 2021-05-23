using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

/*
共享组件最好当作只读组件来用，因为修改共享组件的值会导致实体被移动
到新的块(Chunk)中,这种移动操作比较耗费时间(普通组件不会改变实体的块)
共享实体可以节省内存,特别是要创建大量相同物体时,但是不要滥用可能会降低效率
*/

// [DisallowMultipleComponent]
// [RequiresEntityConversion]
public class ShareAuthoring : MonoBehaviour, IConvertGameObjectToEntity
{
    public int data;

    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        //增
        dstManager.AddSharedComponentData<TestShare>(entity,new TestShare{data = data});
        //改
        dstManager.SetSharedComponentData<TestShare>(entity,new TestShare{data = 100});
        //查
        EntityQuery entityQuery = World.DefaultGameObjectInjectionWorld.EntityManager.CreateEntityQuery(typeof(TestShare));
        NativeArray<Entity> queryToArray = entityQuery.ToEntityArray(Allocator.TempJob);
        foreach (var item in queryToArray)
        {
            Debug.Log(item.Index);
        }
        queryToArray.Dispose();
        //删
        dstManager.RemoveComponent<TestShare>(entity);
    }
}
