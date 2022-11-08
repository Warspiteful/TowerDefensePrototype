using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private DeployLocationType locationType;

    [SerializeField] private ActiveDrag _activeDrag;
    
    [SerializeField] private SpriteRenderer deployableIndicator;

    // Start is called before the first frame update

    private void OnEnable()
    {
        _activeDrag.onValueChanged += ShowDeployable;
    }

    private void OnDisable()
    {
        _activeDrag.onValueChanged -= ShowDeployable;
    }

    private void ShowDeployable()
    {
        if (_activeDrag.Data.locationType == locationType)
        {
            deployableIndicator.enabled = true;
        }
        else
        {
            deployableIndicator.enabled = false;
        }
    }


}
