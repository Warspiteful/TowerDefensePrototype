using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class AvailableUnits : ScriptableObject
{
    [SerializeField] private List<OperatorData> _availableOperators;
    [SerializeField] private ShuffleableList<OperatorData> remainingOperators;

    public void ResetChoices()
    {
        remainingOperators = new ShuffleableList<OperatorData>(_availableOperators);
    }

    public OperatorData[] ReturnRandomOperators(int numOfItems)
    {
        //TODO: Add Empty Operator ?
        OperatorData[] returnValue = new OperatorData[numOfItems];
        if (remainingOperators.Count < numOfItems)
        {
            int i;
            for (i = 0; i < remainingOperators.Count; i++)
            {
                returnValue[i] = remainingOperators[Random.Range(0, remainingOperators.Count)];
            }
        } 
        else{
            for (int i = 0; i < numOfItems; i++)
            {
                Debug.Log(i);
                OperatorData data = remainingOperators[Random.Range(0, remainingOperators.Count-1)];
                returnValue[i] = data;
            }
        }

        return returnValue;
    }

    public void RemoveOperator(OperatorData data)
    {
        remainingOperators.Remove(data);
    }
    
    
    

}
