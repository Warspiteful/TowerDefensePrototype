using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyDefeatedTracker : MonoBehaviour
{

    [SerializeField] private IntVariable totalEnemies;

    [SerializeField] private IntVariable enemiesKilled;

    [SerializeField] private TextMeshProUGUI display;
    // Start is called before the first frame update
    void Start()
    {
        totalEnemies.onValueChanged += UpdateDisplay;
        enemiesKilled.onValueChanged += UpdateDisplay;
        UpdateDisplay();
    }

    // Update is called once per frame
    private void UpdateDisplay()
    {
        display.text = enemiesKilled.Value + "/" + totalEnemies.Value;
    }
}
