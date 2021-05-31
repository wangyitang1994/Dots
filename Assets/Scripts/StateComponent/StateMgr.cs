
using Unity.Entities;
using UnityEngine;
using Unity.Transforms;
using Unity.Collections;

public class StateMgr : MonoBehaviour {
    private void Start() {
        Entity tempEntity = World.DefaultGameObjectInjectionWorld.EntityManager.CreateEntity(typeof(StateComponent1));
        EntityQuery entityQuery = World.DefaultGameObjectInjectionWorld.EntityManager.CreateEntityQuery(typeof(StateComponent1));
        NativeArray<Entity> queryToArray = entityQuery.ToEntityArray(Allocator.TempJob);
        World.DefaultGameObjectInjectionWorld.EntityManager.DestroyEntity(queryToArray[0]);
        World.DefaultGameObjectInjectionWorld.EntityManager.SetComponentData(queryToArray[0],new StateComponent1{data = 10});
        queryToArray.Dispose();
    }
}