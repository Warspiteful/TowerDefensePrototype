using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private DeployLocationType locationType;

    [SerializeField] private ActiveDrag _activeDrag;
    
    [SerializeField] private SpriteRenderer deployableIndicator;
    
    
    [SerializeField] private SpriteRenderer attackIndicator;

    [SerializeField] private bool enemyWalkable;
    private bool displayAttackTiles = false;


    private TileCallback onAttackPreview;
    
    private VoidCallback onAttackReset;


    private DeployedUnit _deployedUnit;
    
    private Tuple<int,int> _coordinate;

    private bool isDisplayingAttackIndicators;
    



    // Start is called before the first frame update

    private void OnEnable()
    {
        _activeDrag.onValueChanged += ShowDeployable;
        ShowDeployable();
    }

    private void OnDisable()
    {
        _activeDrag.onValueChanged -= ShowDeployable;
        
    }

    public Tuple<int,int> GetCoordinates()
    {
        return _coordinate;
    }

    public bool isWalkable()
    {
        return enemyWalkable;
    }
    
    public void SetCoordinates(Tuple<int,int> coordinate)
    {
        _coordinate = coordinate;
    }

    public void RenderAttackDisplay()
    {
        attackIndicator.enabled = true;
    }

    public void HideAttackDisplay()
    {
        
            attackIndicator.enabled = false;
        
    }
    
    public void DeployOperator(DeployedUnit _unit, VoidCallback onDestroy)
    {
        _deployedUnit = _unit;
        _deployedUnit.RegisterOnDestroyCallback(onDestroy);
        _deployedUnit.RegisterOnDestroyCallback(() => onAttackReset?.Invoke());
            _deployedUnit.gameObject.transform.parent = this.transform.parent;
        _deployedUnit.gameObject.transform.localPosition = new Vector3(0.5f, 1, 0.5f);
        _deployedUnit.RegisterOnClickCallback(DisplayPreview);
        _deployedUnit.RegisterOnClickElsewhereCallback(HideDisplayPreview);

    }


    private void DisplayPreview()
    {
        if (_deployedUnit != null)
        {

            onAttackPreview?.Invoke(this);
        }
    }

    private void HideDisplayPreview()
    {
        onAttackReset?.Invoke();
    }

    public Vector2[] GetAttackRange()
    {
        if (_deployedUnit != null)
        {
            return _deployedUnit.GetRange();
        }
        return new []
            {
                new Vector2(0, 0)
            };

        
    }

    public void RegisterAttckCallback(TileCallback callback)
    {
        onAttackPreview += callback;
    }
    
    public void RegisterAttackResetCallback(VoidCallback callback)
    {
        onAttackReset += callback;
    }

    private void ShowDeployable()
    {
        if (_activeDrag.Value != null && _activeDrag.Value == locationType && _activeDrag.Value != DeployLocationType.NONE)
        {
            
            deployableIndicator.enabled = true;
        }
        else
        {
            deployableIndicator.enabled = false;
        }
    }

    public bool CanPlace(DeployLocationType currentTerrain)
    {
        return currentTerrain == locationType;
    }
}
