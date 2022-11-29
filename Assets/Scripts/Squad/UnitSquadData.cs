using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class UnitSquadData : ScriptableObject
{
    public List<OperatorData> squadList;

    public void AddToSquad(OperatorData operatorData)
    {
        squadList.Add(operatorData);
    }
}
