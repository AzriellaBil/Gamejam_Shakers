using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(menuName ="Game Event")]
public class GameEvent : ScriptableObject
{
    private List<GameEventListener> listeners = new List<GameEventListener>();

    public void TriggerEvent()
    {
        for (int i = listeners.Count -1; i >= 0; i--)
        {
            listeners[i].OnEventTriggered();
        }
    }

    public void AddListener(GameEventListener listener)
    {
        listeners.Add(listener);
    }

    public void RemoveListener(GameEventListener listener)
    {
        listeners.Remove(listener);
    }
}

[CreateAssetMenu(menuName = "Events/Int Event Channel")]
public class IntEventChannel : ScriptableObject
{
    public event Action<int> OnEventRaised;

    public void Raise(int value)
    {
        OnEventRaised?.Invoke(value);
    }
}


[CreateAssetMenu(menuName = "Events/Float Event Channel")]
public class FloatEventChannel : ScriptableObject
{
    public event Action<float> OnEventRaised;
    public void Raise(float value) => OnEventRaised?.Invoke(value);
}

[CreateAssetMenu(menuName = "Events/String Event Channel")]
public class StringEventChannel : ScriptableObject
{
    public event Action<string> OnEventRaised;
    public void Raise(string value) => OnEventRaised?.Invoke(value);
}

[CreateAssetMenu(menuName = "Events/Vector3 Event Channel")]
public class Vector3EventChannel : ScriptableObject
{
    public event Action<Vector3> OnEventRaised;
    public void Raise(Vector3 value) => OnEventRaised?.Invoke(value);
}