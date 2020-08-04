using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class GameEvents {
    public static UnityEvent startZapping = new UnityEvent();
    public static UnityEvent endZapping = new UnityEvent();
    public static UnityEvent endCooldown = new UnityEvent();
}

public class GameObjectEvent : UnityEvent<GameObject> { }