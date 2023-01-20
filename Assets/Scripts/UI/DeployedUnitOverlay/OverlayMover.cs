using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlayMover : MonoBehaviour
{

    [SerializeField] private Vector3Variable location;

    [SerializeField] private Transform imageMask;
    [SerializeField] private Transform shadowImage;


    [SerializeField] private Camera mainCamera;

    private Vector3 basePosition;
    // Start is called before the first frame update
    void Start()
    {
        basePosition = transform.position;
        location.onValueChanged += UpdateLoction;
        location.EnableCallback += ToggleVisibility;
    }
    
    private void ToggleVisibility(bool value)
    {

            imageMask.gameObject.SetActive(value);
   
    }
    

    private void UpdateLoction()
    {
        Vector3 pointer = mainCamera.WorldToScreenPoint(location.Value);
        imageMask.position = pointer;
        shadowImage.position = basePosition;
    }
}
