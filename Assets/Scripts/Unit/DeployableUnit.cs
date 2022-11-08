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


    public void Initialize(OperatorData operatorData)
    {
        thumbnailImage.sprite = operatorData.sprite;
        archetypeImage.sprite = operatorData.archtetype.image;
        cost.text = operatorData.deployTime.ToString();
        _operatorData = operatorData;
    }

    public void OnDrag(PointerEventData eventData)
    {
        
        //Always set to (0,1)??
        Vector3 convertedPosition = Camera.main.ScreenToWorldPoint(eventData.position);
        Debug.Log(convertedPosition.x + ", " + convertedPosition.y);
        deployPreview.transform.position = new Vector3(convertedPosition.x,convertedPosition.y);
     
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        deployPreview = Instantiate(draggableObject);
        deployPreview.GetComponent<SpriteRenderer>().sprite = _operatorData.sprite;
        _active.Data = _operatorData;

    }

    public void OnEndDrag(PointerEventData eventData)
    {
      Destroy(deployPreview);
    }
}
