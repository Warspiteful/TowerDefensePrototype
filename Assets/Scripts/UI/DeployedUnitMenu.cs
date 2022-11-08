using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeployedUnitMenu : MonoBehaviour
{
    [SerializeField] private UnitSquadData squadData;
    [SerializeField] private DeployableUnit deployPanelPrefab;
    private void Start()
    {
        foreach(OperatorData _operator in squadData.squadList)
        {
            Instantiate(deployPanelPrefab, this.transform).Initialize(_operator);
        }
    }
}
