using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeployableUnit : MonoBehaviour
{

    [SerializeField] private Image thumbnailImage;

    [SerializeField] private Image archetypeImage;
    [SerializeField] private TextMeshProUGUI cost;

    [SerializeField] private OperatorData _operatorData;
    // Start is called before the first frame update

    private float currentCost;


    public void Initialize(OperatorData operatorData)
    {
        thumbnailImage.sprite = operatorData.sprite;
        archetypeImage.sprite = operatorData.archtetype.image;
        cost.text = operatorData.deployTime.ToString();
        _operatorData = operatorData;
    }

}
