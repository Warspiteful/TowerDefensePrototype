using UnityEngine;

[CreateAssetMenu]
public class IntVariable : ScriptableVariable<int>
{
    public void Increment()
    {
        Value += 1;
    }
    
    public void Decrement()
    {
        Value -= 1;
    }
}
