
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Vector3Variable : ScriptableVariable<Vector3>
{

    public VoidBoolCallback EnableCallback;

    public void ToggleEnable(bool value)
    {
        EnableCallback?.Invoke(value);
    }

   
}
