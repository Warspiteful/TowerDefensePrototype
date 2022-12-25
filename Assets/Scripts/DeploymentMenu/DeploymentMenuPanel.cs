
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

using UnityEngine.UI;

namespace DeploymentMenu
{
    public class DeploymentMenuPanel: MonoBehaviour
    { 
        [SerializeField] private Image thumbnailImage;

    [SerializeField] private GameObject unitPanel;



    [SerializeField] private Image archetypeImage;
    [SerializeField] private Image shadowDisplay;
    [SerializeField] private Image DeathIndicator;

    [SerializeField] private TextMeshProUGUI cost;



    private LayoutElement _layoutElement;
    private Animator _animator;



    // Start is called before the first frame update

    private float currentCost;


    public void Initialize(Sprite operatorSprite, Sprite archetypeSprite, string deployCost)
    {
        _animator = GetComponent<Animator>();
        _layoutElement = GetComponent<LayoutElement>();
        thumbnailImage.sprite = operatorSprite;
        archetypeImage.sprite = archetypeSprite;
        cost.text = deployCost;
    }

  



    public void DisplayPurchaseable(bool canPurchase)
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
 

 


    public void OnSelect()
    {
        
        _animator.Play("Selected");
    }

    public void OnDeselect()
    {
        _animator.Play("Default");
    }
    public void OnHeld()
    {
        _layoutElement.ignoreLayout = false;
        unitPanel.SetActive(true);
        OnDeselect();

    }
    
    public void OnDeath()
    {
        DeathIndicator.enabled = true;
        shadowDisplay.enabled = true;
        _layoutElement.ignoreLayout = false;
        unitPanel.SetActive(true);
    }


        public void OnDeploy()
        {
            _layoutElement.ignoreLayout = true;
            OnDeselect();
            unitPanel.SetActive(false);
        }
    }
}