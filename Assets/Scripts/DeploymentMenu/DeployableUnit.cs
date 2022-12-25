
using System;
using UnityEngine;




//TODO: Combine DisplayPreview and DirectionUnit
namespace  DeploymentMenu
{
    [RequireComponent(typeof(DragController), typeof(DeploymentMenuPanel), typeof(RecallSystem))]
    public class DeployableUnit : MonoBehaviour
    {

        private DeploymentUnitState _state;
        private OperatorData _data;

        private DragController _controller;
        private DeploymentMenuPanel _panel;

        private DeployedUnit _deployedUnit;

        private RecallSystem _recallSystem;

        
        private DeployableUnitCallback _operatorCallback;


        public void Initialize(OperatorData data)
        {
            _controller = GetComponent<DragController>();
            _panel = GetComponent<DeploymentMenuPanel>();
            _recallSystem = GetComponent<RecallSystem>();
  
            _data = data;

            _panel.Initialize(data.sprite, data.archtetype.image, data.deployCost.ToString());
            _controller.Toggle(true);
            ChangeState(DeploymentUnitState.HELD);
            
            _controller.RegisterStateChange(()=>ChangeState(DeploymentUnitState.SELECTED));
            _recallSystem.RegisterOnRecallCallback(()=>ChangeState(DeploymentUnitState.HELD));

            
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
            if (balance >= _data.deployCost && _state != DeploymentUnitState.DEAD)
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

        public DeploymentUnitState GetState()
        {
            return _state;
        }

        public void SetDeployedUnit(DeployedUnit unit)
        {
            if (_deployedUnit != null)
            {
                throw new Exception("Multiple Deployments of the Same Unit");
            }
            _deployedUnit = unit;
            _recallSystem.Initialize(_deployedUnit);
        }

        public void ChangeState(DeploymentUnitState state)
        {
            switch (state)
            {
                case DeploymentUnitState.HELD:
                    _controller.Toggle(true);

                    _recallSystem.ToggleRecall(false);
                    _panel.OnHeld();
                    break; 
                case DeploymentUnitState.SELECTED:
                    _panel.OnSelect();
                    _operatorCallback?.Invoke(this);
                    break;
                case DeploymentUnitState.DEPLOYED:
                    _controller.Toggle(false);
                    _panel.OnDeploy();
                    _recallSystem.ToggleRecall(true);
                    break;
                case DeploymentUnitState.DEAD:
                    _recallSystem.ToggleRecall(false);
                
                    _controller.Toggle(false);
                    _panel.OnDeath();
                    break;
            }

            _state = state;
        }

        public void ChangeStateTo()
        {
            ChangeState(DeploymentUnitState.DEAD);
        }

       
    }
}