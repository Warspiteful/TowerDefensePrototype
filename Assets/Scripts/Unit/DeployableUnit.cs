using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;


//TODO: Combine DisplayPreview and DirectionUnit
public class DeployableUnit : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    [SerializeField] private Image thumbnailImage;
    


    [SerializeField] private Image archetypeImage;
    [SerializeField] private Image shadowDisplay;

    [SerializeField] private TextMeshProUGUI cost;

    [SerializeField] private OperatorData _operatorData;

    private VoidOperatorCallback _operatorCallback;

    private VoidCallback _EndDragVoidCallback;


    // Start is called before the first frame update

    private float currentCost;
    [SerializeField] private bool canPurchase;


    public void Initialize(OperatorData operatorData)
    {
        thumbnailImage.sprite = operatorData.sprite;
        archetypeImage.sprite = operatorData.archtetype.image;
        cost.text = operatorData.deployCost.ToString();
        _operatorData = operatorData;
    }
    
    public void UpdateValue(int bal)
    {
        canPurchase = bal >= _operatorData.deployCost;
        
        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        if (canPurchase)
        {
            shadowDisplay.enabled = false;
        }
        else
        {
            shadowDisplay.enabled = true;
        }
    }

    public void RegisterOperatorCallback(VoidOperatorCallback _callback)
    {
        _operatorCallback += _callback;
    }
    
    public void RegisterEndDragHandler(VoidCallback _callback)
    {
        _EndDragVoidCallback += _callback;
    }
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Start Drag");
        _operatorCallback?.Invoke(_operatorData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        return;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("ENDDRAG");
        _EndDragVoidCallback?.Invoke();
    }
}