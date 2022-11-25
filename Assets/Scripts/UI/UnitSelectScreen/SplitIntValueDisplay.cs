using TMPro;
using UnityEngine;

public class SplitIntValueDisplay : MonoBehaviour
{

    [SerializeField] private IntVariable MovingIndex;

    [SerializeField] private IntVariable TotalIndex;

    [SerializeField] private TextMeshProUGUI display;
    // Start is called before the first frame update
    void Start()
    {
        MovingIndex.onValueChanged += UpdateDisplay;
        TotalIndex.onValueChanged += UpdateDisplay;
        UpdateDisplay();
    }

    // Update is called once per frame
    private void UpdateDisplay()
    {
        display.text = MovingIndex.Value + "/" + TotalIndex.Value;
    }
}

