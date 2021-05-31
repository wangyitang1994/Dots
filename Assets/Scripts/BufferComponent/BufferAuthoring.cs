using UnityEngine;
using Unity.Entities;

public class BufferAuthoring : MonoBehaviour, IConvertGameObjectToEntity
{
    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        DynamicBuffer<BufferComponent> temp = dstManager.AddBuffer<BufferComponent>(entity);
        temp.Add(new BufferComponent{data0 = 1,data1 = 2});
        temp.Add(new BufferComponent{data0 = 3,data1 = 4});
        temp.Add(new BufferComponent{data0 = 5,data1 = 6});
    }
}