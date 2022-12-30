using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitHealthIndicator : MonoBehaviour
{
    [SerializeField] private Image HealthIndicator;
    [SerializeField] private GameObject healthbar;
    

    public void UpdateDisplay(int currentHealth, int maxHealth)
    {

        Vector3 currScale = HealthIndicator.transform.localScale;
        if (!healthbar.activeSelf || currentHealth == 0)
        {
            healthbar.SetActive(false);
            return;
        }
        else
        {
            healthbar.SetActive(true);
        }

    
        HealthIndicator.transform.localScale =
            new Vector3(
                currScale.x,
                (float)currentHealth / maxHealth,
                currScale.z
            );
    }
}
