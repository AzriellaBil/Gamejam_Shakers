using UnityEngine;
using System;

[CreateAssetMenu(menuName = "Events/String Event Channel")]
public class StringEventChannel : ScriptableObject
{
    public event Action<string> OnEventRaised;
    public void Raise(string value) => OnEventRaised?.Invoke(value);
}