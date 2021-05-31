using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

/*
1.共享组件最好当作只读组件来用，因为修改共享组件的值会导致实体被移动
到新的块(Chunk)中,这种移动操作比较耗费时间(普通组件不会改变实体的块)
共享实体可以节省内存,特别是要创建大量相同物体时,但是不要滥用可能会降低效率

2.对于拥有相同类型组件的实体 原型(Archetype)相同,他们会保存在一个
块(Chunk)中,不管是否包含共享组件
3.一旦修改实体的共享组件的值,该实体会被存放到一个新的块中,因为他的
共享组件发生了变化,相当于使用了新的共享组件
4.一旦实体新增了其他任意组件,则该实体会被放到一个新的块中,因为他的
原型发生了变化
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
        // dstManager.RemoveComponent<TestShare>(entity);
    }
}
