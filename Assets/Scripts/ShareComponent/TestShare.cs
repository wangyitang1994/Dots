using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using System;

//共享组件 必须继承IEquatable
public struct TestShare : ISharedComponentData,IEquatable<TestShare>
{
    public int data;

    public bool Equals(TestShare other)
    {
        return data == other.data;
    }

    public override int GetHashCode()
    {
        int temp = 0;
        temp ^= data.GetHashCode();
        return temp;
    }
}
