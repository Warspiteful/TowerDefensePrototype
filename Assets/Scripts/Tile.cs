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
    
    [SerializeField] private DeployedUnit deployedUnitPrefab;

    private TileCallback onAttackPreview;

    private DeployedUnit _deployedUnit;
    
    private Vector2 _coordinate;


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

    public Vector2 GetCoordinates()
    {
        return _coordinate;
    }

    public void SetCoordinates(Vector2 coordinate)
    {
        _coordinate = coordinate;
    }

    public Direction GetDirection()
    {
        if (_deployedUnit != null)
        {
            return _deployedUnit.GetDirection();
        }

        return Direction.NONE;
    }

    public void DeployOperator(OperatorData _operatorData)
    {
        _deployedUnit = Instantiate(deployedUnitPrefab,
            this.transform);
        _deployedUnit.gameObject.transform.localPosition = new Vector3(0.5f, 1, 1);
        _deployedUnit .Initialize(_operatorData);
    }


    private void OnMouseDown()
    {
        if(_deployedUnit != null){
            onAttackPreview?.Invoke(this);
        }
    }

    public Vector2 GetAttackRange()
    {
        if (_deployedUnit != null)
        {
            return _deployedUnit.GetRange();
        }
        else
        {
            return new Vector2(0,0);
        }
    }

    public void RegisterAttckCallback(TileCallback callback)
    {
        onAttackPreview += callback;
    }
    
    private void ShowDeployable()
    {
        if (_activeDrag.Value != null && _activeDrag.Value.locationType == locationType)
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

    public string Test()
    {
        return "Testing";
    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }
}
