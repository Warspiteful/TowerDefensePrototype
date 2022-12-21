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

    [SerializeField] private GameObject unitPanel; 
    


    [SerializeField] private Image archetypeImage;
    [SerializeField] private Image shadowDisplay;

    [SerializeField] private TextMeshProUGUI cost;

    [SerializeField] private OperatorData _operatorData;

    private DeployableUnitCallback _operatorCallback;

    private LayoutElement _layoutElement;
    private Animator _animator;

    private VoidCallback _EndDragVoidCallback;


    // Start is called before the first frame update

    private float currentCost;
    [SerializeField] private bool canPurchase;


    public void Initialize(OperatorData operatorData)
    {
        _animator = GetComponent<Animator>();
        _layoutElement = GetComponent<LayoutElement>();
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

    public OperatorData getOperatorData()
    {
        return _operatorData;
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

    public void RegisterOperatorCallback(DeployableUnitCallback _callback)
    {
        _operatorCallback += _callback;
    }
    
    public void RegisterEndDragHandler(VoidCallback _callback)
    {
        _EndDragVoidCallback += _callback;
    }
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        _animator.Play("Selected");
        Debug.Log("Start Drag");
        _operatorCallback?.Invoke(this);
    }

    public void OnDrag(PointerEventData eventData)
    {
        return;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _animator.Play("Default");
        Debug.Log("ENDDRAG");
        _EndDragVoidCallback?.Invoke();
    }

    public void ShowMenu()
    {
        _layoutElement.ignoreLayout = false;
        unitPanel.SetActive(true);
    }



    
    public void HideMenu()
    {
        _layoutElement.ignoreLayout = true;
        unitPanel.SetActive(false);
    }
}