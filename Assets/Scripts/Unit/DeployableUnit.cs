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
    [SerializeField] private TextMeshProUGUI cost;

    [SerializeField] private OperatorData _operatorData;
    [SerializeField] private ActiveDrag _active;


    [SerializeField] private GameObject draggableObject;
    // Start is called before the first frame update

    private float currentCost;

    private GameObject deployPreview;
    
    private float startXPos;
    private float startYPos;


    public void Initialize(OperatorData operatorData)
    {
        thumbnailImage.sprite = operatorData.sprite;
        archetypeImage.sprite = operatorData.archtetype.image;
        cost.text = operatorData.deployTime.ToString();
        _operatorData = operatorData;
    }

    public void OnDrag(PointerEventData eventData)
    {
        
        Vector3 mousePos = Input.mousePosition;
        //Always set to (0,1)??
            if(!Camera.main.orthographic)
            {
                mousePos.z = 10;
            }
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);
            Debug.Log(mousePos.x + ", " + mousePos.y);


        deployPreview.transform.position = new Vector3(mousePos.x, mousePos.y, deployPreview.transform.localPosition.z);
     
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        deployPreview = Instantiate(draggableObject);
        
        Vector3 mousePos = Input.mousePosition;

        if (!Camera.main.orthographic)
        {
            mousePos.z = 10;
        }

        mousePos = Camera.main.ScreenToWorldPoint(mousePos);

      
        deployPreview.GetComponent<SpriteRenderer>().sprite = _operatorData.sprite;
        _active.Data = _operatorData;

    }

    public void OnEndDrag(PointerEventData eventData)
    {
     // Destroy(deployPreview);
    }
}
