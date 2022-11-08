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
    
    [SerializeField] private DeployedUnit _deployedUnitPrefab;

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

            
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            // Save the info
            
            RaycastHit hit;
            Vector3 dir;
            if (Physics.Raycast(ray, out hit))
            {
                if(hit.collider.gameObject.GetComponent<Tile>() != null && hit.collider.gameObject.GetComponent<Tile>().CanPlace(_operatorData.locationType) ){

                deployPreview.SetActive(true);
                mousePos = hit.point;
                mousePos.y += deployPreview.transform.position.y / 2;
                deployPreview.transform.position = new Vector3
                (
                    mousePos.x,
                    mousePos.y,
                    mousePos.z
                );
                }
            
            }
            else
            {
                deployPreview.SetActive(false);
            }

       
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if(canPurchase){
            deployPreview = Instantiate(draggableObject);
            deployPreview.GetComponent<SpriteRenderer>().sprite = _operatorData.sprite;
            deployPreview.gameObject.SetActive(false);
            _active.Value = _operatorData;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    { 
        if(canPurchase && deployPreview != null){ 
            
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {

                if (hit.collider.gameObject.GetComponent<Tile>() != null && hit.collider.gameObject
                        .GetComponent<Tile>().CanPlace(_operatorData.locationType))
                {
                    DeployedUnit unit = Instantiate(_deployedUnitPrefab,
                        hit.collider.gameObject.transform);
                        unit.gameObject.transform.localPosition = new Vector3(0.5f, 1, 1);
                    unit.Initialize(_operatorData);
                    _balance.Value -= _operatorData.deployCost;

                }
            }

            Destroy(deployPreview);
        }
    }
}
