using UnityEngine;
using System;

[CreateAssetMenu(menuName = "Events/String Event Channel")]
public class StringEventChannel : ScriptableObject
{
    public event Action<string> OnEventRaised;
    public void Raise(string value) => OnEventRaised?.Invoke(value);
}

[CreateAssetMenu(menuName = "Events/Float Event Channel")]
public class FloatEventChannel : ScriptableObject
{
    public event Action<float> OnEventRaised;
    public void Raise(float value) => OnEventRaised?.Invoke(value);
}