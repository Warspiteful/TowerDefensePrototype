using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnitSelector : MonoBehaviour
{

    [SerializeField] private Image _operatorDisplay;
    [SerializeField] private Image _archetypeDisplay;
    [SerializeField] private TextMeshProUGUI cost;

    private VoidOperatorCallback _onSelectCallback;
    [SerializeField] private OperatorData storedData;
    
    public void Initialize(OperatorData data)
    {
        _operatorDisplay.sprite = data.sprite;
        _archetypeDisplay.sprite = data.archtetype.image;
        storedData = data;
        cost.text = data.deployCost.ToString();
    }

    public void RegisterOnClick(VoidOperatorCallback callback)
    {
        _onSelectCallback += callback;
    }

    public void OnClick()
    {
        _onSelectCallback?.Invoke(storedData);
    }
}
