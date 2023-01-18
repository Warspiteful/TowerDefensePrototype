using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeployedUnitPanel : MonoBehaviour
{

    [SerializeField] private OperatorDataVariable data;

    [SerializeField] private GameObject panel;
    [SerializeField] private Image ArchtypeImage;
    [SerializeField] private TextMeshProUGUI UnitName;
    [SerializeField] private TextMeshProUGUI attackStat;
    [SerializeField] private TextMeshProUGUI defenseStat;
    [SerializeField] private TextMeshProUGUI blockNumber;
    [SerializeField] private TextMeshProUGUI healthBar;

    private OperatorData currentData;
    void Start()
    {
        data.onValueChanged += UpdateDisplay;
    }

    // Update is called once per frame
    void UpdateDisplay()
    {
        if (currentData == data.Value || data.Value == null)
        {
            panel.SetActive(false);
            currentData = null;
            return;
        }
        
        panel.SetActive(true);
            OperatorData operatorData = data.Value;
            ArchtypeImage.sprite = operatorData.archtetype.image;
            UnitName.text = operatorData.name;
            attackStat.text = operatorData.atkPower.ToString();
            defenseStat.text = operatorData.def.ToString();
            blockNumber.text = operatorData.guardedUnitNumber.ToString();
            currentData = data.Value;

        
       
    }
}
