using Unity.Collections;
using Unity.Entities;
using UnityEngine;

public class TestBuffer : MonoBehaviour {
    private void Start() {
            Debug.Log($"start");
        
        EntityQuery entityQuery = World.DefaultGameObjectInjectionWorld.EntityManager.CreateEntityQuery(typeof(BufferComponent));
        NativeArray<Entity> queryToArray = entityQuery.ToEntityArray(Allocator.TempJob);
        //buffer相当于list
        DynamicBuffer<BufferComponent> tempBuffer = World.DefaultGameObjectInjectionWorld.EntityManager.GetBuffer<BufferComponent>(queryToArray[0]);
        
        tempBuffer.Add(new BufferComponent{data0 = 101,data1 = 201});
        tempBuffer.Insert(0,new BufferComponent{data0 = 301,data1 = 401});
        tempBuffer.RemoveAt(1);
        
        foreach (var item in tempBuffer)
        {
            Debug.Log($"{item.data0} {item.data1}");
        }
        queryToArray.Dispose();
    }
}