using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ActiveDrag : ScriptableObject
{

    public OnValueChanged onValueChanged;
    private OperatorData _data;

    public OperatorData Data
    {
        set
        {
            _data = value;
            onValueChanged?.Invoke();
        }
        get
        {
            return _data;
        }
    }
}
