using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public struct TestShare : ISharedComponentData
{
    public int data;
}
