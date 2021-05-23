using Unity.Entities;

//生成代理组件 即可挂在到GameObject上
[GenerateAuthoringComponent]
public struct TestEntity : IComponentData{
    public float num;
}