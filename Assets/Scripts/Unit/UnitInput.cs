using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;


public enum CallbackType{
    BEGINDRAG, ENDDRAG, DRAG
}


public class UnitInput : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private PlayerControls _controls;
    
    [SerializeField] private LayerMask inputLayerMask;
    
    private void Start()
    {
        Camera.main.eventMask = inputLayerMask;
        _controls = new PlayerControls();
        _controls.Gameplay.Enable();
        _controls.Gameplay.OnClick.performed += ctx => Onclick(ctx);
    }

    private Vector3Callback OnDragVector3Callback;
    private Vector3Callback OnDragBeginVector3Callback;
    private Vector3Callback OnDragEndVector3Callback;
    private VoidCallback OnClickVoidCallback;

    public void RegisterMousePositionCallback(CallbackType _callbackType, params Vector3Callback[] callback)
    {
        Vector3Callback _callback;
        switch (_callbackType)
        {
            case CallbackType.DRAG:
                _callback = OnDragVector3Callback;
                break;
            case CallbackType.BEGINDRAG:
                _callback = OnDragBeginVector3Callback;
                break;
            case CallbackType.ENDDRAG:
                _callback = OnDragEndVector3Callback;
                break;
            default:
                throw new ArgumentException("Invalid callback Type");
        }

        foreach (Vector3Callback vector3Callback in callback)
        {
            _callback += vector3Callback;
        }
    }

    public void RegisterOnClickCallback(params VoidCallback[] callback)
    {
        foreach(VoidCallback _callback in callback)
        {
            OnClickVoidCallback += _callback;
        }
    }

    public void Onclick(InputAction.CallbackContext ctx)
    {
        Camera c = Camera.main;
        RaycastHit rcHit;

        if (Physics.Raycast (Camera.main.ScreenPointToRay(
                    Mouse.current.position.ReadValue()), out rcHit, Mathf.Infinity, inputLayerMask))
        {
            Debug.Log(rcHit.collider.gameObject.name);
            OnClickVoidCallback?.Invoke();

        }
        

    
    }

    public void OnDrag(PointerEventData eventData)
    {
        OnDragVector3Callback?.Invoke(Input.mousePosition);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        OnDragBeginVector3Callback?.Invoke(Input.mousePosition);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        OnDragEndVector3Callback?.Invoke(Input.mousePosition);
    }
}
