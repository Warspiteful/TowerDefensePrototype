using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class BalanceDisplay : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private IntVariable balance;

    [SerializeField] private TextMeshProUGUI _textMeshProUGUI;

    private void OnEnable()
    {
        balance.onValueChanged += UpdateDisplay;
        UpdateDisplay();
    }

    private void OnDisable()
    {
        balance.onValueChanged -= UpdateDisplay;
    }

    private void UpdateDisplay()
    {
        _textMeshProUGUI.text = balance.Value.ToString();
    }
}
