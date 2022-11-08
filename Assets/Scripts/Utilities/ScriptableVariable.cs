using UnityEngine;

public class ScriptableVariable<T> : ScriptableObject
{
    public OnValueChanged onValueChanged;
    private T _value;

    public T Value
    {
        set
        {
            _value = value;
            onValueChanged?.Invoke();
        }
        get
        {
            return _value;
        }
    }
}