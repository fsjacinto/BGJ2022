using System;
using UnityEngine;

/// <summary>
/// SystemsHolder is a persistent singleton that holds the systems as its children objects. 
/// Since the parents is persistent, the children will also not be destroyed on scene load.
/// </summary>
public class SystemsHolder : SingletonPersistent<SystemsHolder> {
    protected override void Awake() {
        base.Awake();
    }
}
