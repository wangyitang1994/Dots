using Unity.Entities;
using Unity.Jobs;
using Unity.Transforms;

public class JobSystem : JobComponentSystem
{
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        return Entities.ForEach((ref RotationEulerXYZ data) => {
            data.Value = new Unity.Mathematics.float3(0,45,0);
        }).WithBurst().Schedule(inputDeps);
    }
}