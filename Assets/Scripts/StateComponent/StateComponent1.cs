using Unity.Entities;

//状态组件 在实体被销毁后依然存在
//ISystemStateSharedComponentData 共享版本
[GenerateAuthoringComponent]
public struct StateComponent1 : ISystemStateComponentData{
    public int data;
}