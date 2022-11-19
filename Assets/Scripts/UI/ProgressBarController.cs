using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressBarController : MonoBehaviour
{
   [SerializeField] private FloatVariable progress;

   [SerializeField] private GameObject progressBar;

   private void OnEnable()
   {
      progress.onValueChanged += UpdateDisplay;
   }

   private void OnDisable()
   {
      progress.onValueChanged -= UpdateDisplay;

   }

   private void UpdateDisplay()
   {
      progressBar.transform.localScale = new Vector3(progress.Value, 1, 1);
   }
}
