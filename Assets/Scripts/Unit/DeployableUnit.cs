using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
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
    
    
    [SerializeField] LayerMask relevantLayer;

    // Start is called before the first frame update

    private float currentCost;
    [SerializeField] private bool canPurchase;


    private GameObject deployPreview;
    
    private float startXPos;
    private float startYPos;

    private Tile selectedTile;


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
    
    //TODO: Split Drag Behavior and Renderer Behavior into Two Scripts
    public void OnDrag(PointerEventData eventData)
    {
        if(canPurchase){ 
            Vector3 mousePos = Input.mousePosition;

            
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            // Save the info
            
            RaycastHit hit;
            Vector3 dir;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, relevantLayer)){
            selectedTile = hit.collider.gameObject.GetComponentInParent<Tile>();
             if(selectedTile != null && selectedTile.CanPlace(_operatorData.locationType))
            {
                Debug.DrawLine(Camera.main.transform.position, hit.collider.gameObject.transform.position,Color.red);
                Debug.Log(hit.collider.gameObject.name);
          
                mousePos = hit.point;
                Vector3 hitPoint = hit.collider.transform.position;
                mousePos.y += deployPreview.transform.position.y / 2;
                deployPreview.transform.position = hit.collider.transform.position + new Vector3(0.5f,1,1);
                float sharedScale = 5.25f / Mathf.Max(hitPoint.z + 5, 1);
                deployPreview.transform.localScale = new Vector3(sharedScale,sharedScale,sharedScale);

            }
            }
            else
            {
                
                mousePos = Camera.main.ScreenToWorldPoint(mousePos);

                Debug.Log(mousePos);
         
                deployPreview.transform.position = new Vector3
                (
                    mousePos.x,
                    0,
                    mousePos.z +mousePos.y
                );
                float scale = 5.25f/Mathf.Max(mousePos.z + mousePos.y+5,1);
                deployPreview.transform.localScale = new Vector3(scale, scale, scale);

            }
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
        if(canPurchase && deployPreview != null && selectedTile != null){ 
            
 
            selectedTile.DeployOperator(_operatorData);
                    _balance.Value -= _operatorData.deployCost;
        }
        selectedTile = null;

            _active.Value = null;
            Destroy(deployPreview);
        
    }
}
