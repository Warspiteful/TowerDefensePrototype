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
        remainingOperators.Shuffle();
        
        Debug.Log("Starting List");
        foreach (OperatorData op in remainingOperators)
        {
            Debug.Log(op.name);
        }
        
        
        
        //TODO: Add Empty Operator ?
        OperatorData[] returnValue = new OperatorData[numOfItems];
        if (remainingOperators.Count < numOfItems)
        {
            int i;
            for (i = 0; i < remainingOperators.Count; i++)
            {
                returnValue[i] = remainingOperators[i];
            }
        } 
        else{
            for (int i = 0; i < numOfItems; i++)
            {
                returnValue[i] = remainingOperators[i];
            }
        }

        return returnValue;
    }

    public void RemoveOperator(OperatorData data)
    {
        remainingOperators.Remove(data);
    }

    public int GetNumberOfAvailableOperators()
    {
        return remainingOperators.Count;
    }
    

}
