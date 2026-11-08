using System;

/// <summary>
/// Serializable key-value structure used to simulate dictionary entries in the Unity Inspector.
/// </summary>

[Serializable]
public struct StructForDictionary<T1,T2>
{
    public T1 Key;
    public T2 Value;
}
