using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DeployableUnit : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{

    [SerializeField] private Image thumbnailImage;

    [SerializeField] private Image archetypeImage;
    [SerializeField] private Image shadowDisplay;

    [SerializeField] private TextMeshProUGUI cost;

    [SerializeField] private OperatorData _operatorData;
    [SerializeField] private ActiveDrag _active;
    [SerializeField] private IntVariable _balance;


    [SerializeField] private GameObject draggableObject;
    // Start is called before the first frame update

    private float currentCost;
    [SerializeField] private bool canPurchase;


    private GameObject deployPreview;
    
    private float startXPos;
    private float startYPos;


    public void Initialize(OperatorData operatorData)
    {
        thumbnailImage.sprite = operatorData.sprite;
        archetypeImage.sprite = operatorData.archtetype.image;
        cost.text = operatorData.deployCost.ToString();
        _operatorData = operatorData;
        UpdateValue();

    }

    private void OnEnable()
    {
        _balance.onValueChanged += UpdateValue;
    }
    
    private void OnDisable()
    {
        _balance.onValueChanged -= UpdateValue;
    }
    private void UpdateValue()
    {
        canPurchase = _balance.Value >= _operatorData.deployCost;


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
    public void OnDrag(PointerEventData eventData)
    {
        if(canPurchase){ 
            Vector3 mousePos = Input.mousePosition;
            
            if(!Camera.main.orthographic)
            {
                mousePos.z = 10;
            }
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);
            
            deployPreview.transform.position = new Vector3
            (
                mousePos.x,
                mousePos.y,
                deployPreview.transform.localPosition.z
                );
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if(canPurchase){
            deployPreview = Instantiate(draggableObject);
            deployPreview.GetComponent<SpriteRenderer>().sprite = _operatorData.sprite;
            _active.Value = _operatorData;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    { 
        if(deployPreview != null){
            Destroy(deployPreview);
        }
    }
}
