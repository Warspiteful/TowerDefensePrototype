using System;
using System.Collections.Generic;
using UnityEngine;

namespace DeploymentMenu
{
    public class HealingSystem : MonoBehaviour
    {

                
        
        [SerializeField] private float timeToIncrement;
        [SerializeField] private float currentTime;
        [SerializeField] private FloatVariable percent;

        private List<DeployableUnit> _units;

        [SerializeField]  private IntVariable healAmount;

        private void Start()
        {
            _units = new List<DeployableUnit>();
     
                currentTime = timeToIncrement;

        }
        

        public void AddUnit(DeployableUnit unit)
        {
            _units.Add(unit);
        }

        private void OnHeal()
        {
            foreach (DeployableUnit unit in _units)
            {
                if(unit.GetState() == DeploymentUnitState.HELD)
                {
                    Mathf.Clamp(
                        unit.getOperatorData().currentHealth,
                        unit.getOperatorData().currentHealth + healAmount.Value,
                        unit.getOperatorData().health
                        );
                }
            }
        }
        
        private void Update()
        {
            currentTime -= Time.deltaTime;
            percent.Value = 1 - (currentTime / timeToIncrement);
            if (currentTime < 0)
            {
                OnHeal();
                currentTime = timeToIncrement;
            }
        }
    }
}