
using System;
using UnityEngine;




//TODO: Combine DisplayPreview and DirectionUnit
namespace  DeploymentMenu
{
    [RequireComponent(typeof(DragController), typeof(DeploymentMenuPanel))]
    public class DeployableUnit : MonoBehaviour
    {

        private DeploymentUnitState _state;
        private OperatorData _data;

        private DragController _controller;
        private DeploymentMenuPanel _panel;

        
        private DeployableUnitCallback _operatorCallback;


        public void Initialize(OperatorData data)
        {
            _controller = GetComponent<DragController>();
            _panel = GetComponent<DeploymentMenuPanel>();
  
            _state = DeploymentUnitState.HELD;
            _data = data;

            _panel.Initialize(data.sprite, data.archtetype.image, data.deployCost.ToString());
            
            _controller.RegisterStartDragHandler(()=>ChangeState(DeploymentUnitState.SELECTED));
            
      
            
        }

        public OperatorData getOperatorData()
        {
            return _data;
        }
        
        public void RegisterOperatorCallback(DeployableUnitCallback _callback)
        {
            _operatorCallback += _callback;
        }


        public void CanAfford(int balance)
        {
            if (balance >= _data.deployCost)
            {
                _panel.DisplayPurchaseable(true);
            }
            else
            {
                _panel.DisplayPurchaseable(false);

            }
        }

        public void RegisterOnEndDeployCallback(VoidCallback _callback)
        {
            _controller.RegisterEndDragHandler(_callback);
        }
        

        public void ChangeState(DeploymentUnitState state)
        {
            switch (state)
            {
                case DeploymentUnitState.HELD:
                    _panel.OnHeld();
                    break; 
                case DeploymentUnitState.SELECTED:
                    _panel.OnSelect();
                    Debug.Log("DEPLOYYYYYING");
                    _operatorCallback?.Invoke(this);
                    break;
                case DeploymentUnitState.DEPLOYED:
                    _panel.OnDeploy();
                    break;
                case DeploymentUnitState.DEAD:
                    _panel.OnDeath();
                    break;
            }
        }

       
    }
}