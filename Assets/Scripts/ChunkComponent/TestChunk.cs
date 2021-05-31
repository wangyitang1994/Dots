using UnityEngine;
using Unity.Entities;

public class TestChunk : MonoBehaviour {
    private void Start() {
        // chunk改变时会导致所有ChunkComponent一起改变
        Entity tempEntity1 = World.DefaultGameObjectInjectionWorld.EntityManager.CreateEntity();
        Entity tempEntity2 = World.DefaultGameObjectInjectionWorld.EntityManager.CreateEntity();
        World.DefaultGameObjectInjectionWorld.EntityManager.AddChunkComponentData<ChunkComponent>(tempEntity1);
        World.DefaultGameObjectInjectionWorld.EntityManager.AddChunkComponentData<ChunkComponent>(tempEntity2);

        // 这样创建会导致无法设置Chunk的数据？？
        // EntityArchetype tempArcheType = World.DefaultGameObjectInjectionWorld.EntityManager.CreateArchetype(typeof(ChunkComponent));
        // Entity tempEntity1 = World.DefaultGameObjectInjectionWorld.EntityManager.CreateEntity(tempArcheType);
        // Entity tempEntity2 = World.DefaultGameObjectInjectionWorld.EntityManager.CreateEntity(tempArcheType);

        ArchetypeChunk chunk1 = World.DefaultGameObjectInjectionWorld.EntityManager.GetChunk(tempEntity2);

        World.DefaultGameObjectInjectionWorld.EntityManager.SetChunkComponentData<ChunkComponent>(chunk1,new ChunkComponent{data = 10});

        // ChunkComponent com = World.DefaultGameObjectInjectionWorld.EntityManager.GetChunkComponentData<ChunkComponent>(tempEntity1);
        // World.DefaultGameObjectInjectionWorld.EntityManager.RemoveChunkComponent<ChunkComponent>(tempEntity1);
        // World.DefaultGameObjectInjectionWorld.EntityManager.AddChunkComponentData<ChunkComponent>(tempEntity1);
        // World.DefaultGameObjectInjectionWorld.EntityManager.HasChunkComponent<ChunkComponent>(tempEntity1);
    }
}