using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSelectorManager : MonoBehaviour
{
    [SerializeField] private AvailableUnits unitManager;

    [SerializeField] private UnitSquadData squadData;

    [SerializeField] private UnitSelector selectorPrefab;
    
    [SerializeField] private int unitsToDisplay;
    [SerializeField] private IntVariable totalChoiceNums;
    [SerializeField] private IntVariable currentChoiceNum;

    private List<UnitSelector> displayedPanels;
    [Header("PLACEHOLDER")]
    [SerializeField] private int totalChoices;


    //PLACEHOLDER
    private void Start()
    {
        //TODO: REMOVE PLACEHOLDER
        totalChoiceNums.Value = totalChoices;
        unitManager.ResetChoices();
        
        currentChoiceNum.Value = 0;
        displayedPanels = new List<UnitSelector>();
        for (int i = 0; i < unitsToDisplay; i++)
        {
            displayedPanels.Add(Instantiate(selectorPrefab, transform));
        }
        
        if (totalChoiceNums.Value > 0)
        {
            GenerateChoices();
        }


    }

    private void IterateChoice()
    {
        currentChoiceNum.Value += 1;
        if (currentChoiceNum.Value < totalChoiceNums.Value)
        {
            ResetUI();
            GenerateChoices();
        }
    }

    private void ResetUI()
    {
        for (int i = 0; i < unitsToDisplay; i++)
        {
            displayedPanels[i].gameObject.SetActive(false);
        }
    }


    private void GenerateChoices()
    {
        OperatorData[] pickedUnits = unitManager.ReturnRandomOperators(unitsToDisplay);
        
        for(int i = 0; i < pickedUnits.Length; i++)
        {
            displayedPanels[i].Initialize(pickedUnits[i]);
            displayedPanels[i].RegisterOnClick(AddToSquad);
            displayedPanels[i].gameObject.SetActive(true);
        }
    }

    private void AddToSquad(OperatorData selectedOperator)
    {
        squadData.AddToSquad(selectedOperator);
        unitManager.RemoveOperator(selectedOperator);
        IterateChoice();
    }
}
