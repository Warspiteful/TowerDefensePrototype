using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace  DeploymentMenu
{
    public class DeployedUnitMenu : MonoBehaviour
    {
        [SerializeField] private UnitSquadData squadData;
        [SerializeField] private DeployableUnit deployPanelPrefab;


        [SerializeField] private DeployableUnit currentUnitPanel;

         private VoidIntCallback _balanceCallback;

        private PlayerControls _controls;
        
        
        private void Start()
        {
            foreach(OperatorData _operator in squadData.squadList)
            {
                DeployableUnit _unit = Instantiate(deployPanelPrefab, this.transform);
                _unit.Initialize(_operator);
                _balanceCallback += _unit.CanAfford;
                _unit.RegisterOperatorCallback(UpdatePreview);
                _unit.RegisterOnEndDeployCallback(ReleaseDrag);
            }
            
            

            _controls = new PlayerControls();
            _controls.Gameplay.Enable();
            _controls.Gameplay.MousePosition.performed += ClickHoldRelease;

            _balance.onValueChanged += UpdateValue;
            UpdateValue();

        }

        private void UpdateValue()
        {
            _balanceCallback?.Invoke(_balance.Value);
        }

        [SerializeField] private bool isDragging;
        private Tile selectedTile;

        private PlacementUnit directionalUnit;
        
        [SerializeField] private IntVariable _balance;


        [SerializeField] private GameObject draggableObject;
        
        
        [SerializeField] private ActiveDrag _active;

        
        [SerializeField] LayerMask relevantLayer;
        
        
        private GameObject deployPreview;
        
        private OperatorCallback _operatorCallback;
        
        [SerializeField] private OperatorData _operatorData;


        private void UpdatePreview(DeployableUnit value)
        {
            if (deployPreview == null)
            {
                deployPreview = Instantiate(draggableObject);
            }

            currentUnitPanel = value;
            deployPreview.SetActive(true);
            deployPreview.GetComponent<SpriteRenderer>().sprite = value.getOperatorData().sprite;
            _active.Value = value.getOperatorData().locationType;
            this._operatorData = value.getOperatorData();
            
            isDragging = true;
        }

        public void ClickHoldRelease(InputAction.CallbackContext ctx)
        {
            if (isDragging)
            {
                Vector3 mousePos = ctx.ReadValue<Vector2>();

                Ray ray = Camera.main.ScreenPointToRay(mousePos);
                // Save the info

                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, Mathf.Infinity, relevantLayer))
                {
                    selectedTile = hit.collider.gameObject.GetComponentInParent<Tile>();
                    if (selectedTile != null && selectedTile.CanPlace(_active.Value))
                    {

                        mousePos = hit.point;
                        mousePos.y += deployPreview.transform.position.y / 2;
                        deployPreview.transform.position =
                            hit.collider.transform.position + new Vector3(0.5f, 1, 0.5f);

                        return;

                    }
                }

                mousePos = Camera.main.ScreenToWorldPoint(mousePos);


                deployPreview.transform.position = new Vector3
                (
                    mousePos.x,
                    0,
                    mousePos.z + mousePos.y *
                    Mathf.Cos(Mathf.Deg2Rad * Camera.main.transform.rotation.eulerAngles.x)
                );
            }
        }


        public void ReleaseDrag()
        { 
            
            if( deployPreview != null && selectedTile != null && selectedTile.CanPlace(_active.Value))
            {
                directionalUnit = deployPreview.GetComponent<PlacementUnit>();
                directionalUnit.Initialize(_operatorData.sprite);
                directionalUnit.RegisterOnCancelCallback(() => currentUnitPanel.ChangeState(DeploymentUnitState.HELD));
                directionalUnit.RegisterDirectionCallback(DeployOperator);
                isDragging = false;
            }
            else if (deployPreview != null)
            {
                deployPreview.SetActive(false);
                isDragging = false;
                currentUnitPanel.ChangeState(DeploymentUnitState.HELD);

                currentUnitPanel = null;
            }
            
            _active.Value = DeployLocationType.NONE;
        }



        private void DeployOperator(Direction dir)
        {
            currentUnitPanel.ChangeState(DeploymentUnitState.DEPLOYED);
            selectedTile.DeployOperator(_operatorData, dir, () => currentUnitPanel.ChangeState(DeploymentUnitState.DEAD));
            _balance.Value -= _operatorData.deployCost;
            directionalUnit = null;
            currentUnitPanel = null;
            selectedTile = null;
        }
    }
}
