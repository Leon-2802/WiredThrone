using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public struct CollisionData
{
    public GameObject collidedWith { get; set; }
    public GameObject triggerGameObject { get; set; }
}

[Serializable]
public class CollisionEvent : UnityEvent<CollisionData> { }