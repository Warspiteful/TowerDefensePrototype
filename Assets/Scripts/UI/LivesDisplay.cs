using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LivesDisplay : MonoBehaviour
{
       [SerializeField] private IntVariable Lives;
       [SerializeField] private TextMeshProUGUI textDisplay;

    // Start is called before the first frame update
    void Start()
    {
        Lives.onValueChanged += UpdateDisplay;
        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        textDisplay.text = Lives.Value.ToString();
    }
}
