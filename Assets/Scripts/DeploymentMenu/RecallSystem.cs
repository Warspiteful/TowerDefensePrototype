using UnityEngine;
using UnityEngine.UI;

namespace DeploymentMenu
{
    public class RecallSystem : MonoBehaviour
    {

        [SerializeField] private Button recallButton;

        private DeployedUnit _unit;

        private VoidCallback onRecall;
        public void Initialize(DeployedUnit unit)
        {
            _unit = unit;
        }


        public void ToggleRecall(bool value)
        {
            recallButton.enabled = value;
        }
        public void Recall()
        {
            onRecall?.Invoke();
            _unit.gameObject.SetActive(false);
        }

        public void RegisterOnRecallCallback(VoidCallback _callback)
        {
            onRecall += _callback;
        }
        
        
        
    }
}