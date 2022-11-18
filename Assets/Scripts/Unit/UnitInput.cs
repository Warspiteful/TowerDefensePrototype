using System;
using UnityEngine;
using UnityEngine.InputSystem;


public enum CallbackType{
    BEGINDRAG, ENDDRAG, DRAG
}

public class UnitInput : MonoBehaviour
{
    private PlayerControls _controls;
    
    
    
    [SerializeField] private LayerMask inputLayerMask;

    [SerializeField] private bool isDragging;
    
    private void Start()
    {
        Camera.main.eventMask = inputLayerMask;
        _controls = new PlayerControls();
        _controls.Gameplay.Enable();
        _controls.Gameplay.OnClick.started += ctx => HandleClick(ctx);
        _controls.Gameplay.MousePosition.performed += ctx => MouseDrag(ctx);
        _controls.Gameplay.OnClick.canceled += ctx => HandleClick(ctx);

    }

    private Vector3Callback OnDragVector3Callback;
    private Vector3Callback OnDragBeginVector3Callback;
    private Vector3Callback OnDragEndVector3Callback;
    private VoidCallback OnClickVoidCallback;
    private VoidCallback OnClickElsewhereVoidCallback;
    private VoidCallback OnReleaseVoidCallback;



    public void RegisterMousePositionCallback(CallbackType _callbackType, params Vector3Callback[] callback)
    {
        Vector3Callback _callback;
        switch (_callbackType)
        {
            case CallbackType.DRAG:
                foreach (Vector3Callback vector3Callback in callback)
                {
                    OnDragVector3Callback += vector3Callback;
                }
                break;
            case CallbackType.BEGINDRAG:
                foreach (Vector3Callback vector3Callback in callback)
                {
                    OnDragBeginVector3Callback += vector3Callback;
                }
                break;
            case CallbackType.ENDDRAG:
                foreach (Vector3Callback vector3Callback in callback)
                {
                    OnDragEndVector3Callback += vector3Callback;
                }                break;
            default:
                throw new ArgumentException("Invalid callback Type");
        }
    }
    
 

    public void RegisterOnReleaseVoidCallback(params VoidCallback[] callback)
    {
        foreach(VoidCallback _callback in callback)
        {
            OnReleaseVoidCallback += _callback;
        }
    }

    public void RegisterOnClickCallback(params VoidCallback[] callback)
    {
        foreach(VoidCallback _callback in callback)
        {
            OnClickVoidCallback += _callback;
        }
    }
    
    public void RegisterOnElsewhereClickCallback(params VoidCallback[] callback)
    {
        foreach(VoidCallback _callback in callback)
        {
            OnClickElsewhereVoidCallback += _callback;
        }
    }


    private void OnDisable()
    {   
     OnDragVector3Callback = null;
    OnDragBeginVector3Callback = null;
    OnDragEndVector3Callback = null;
    OnClickVoidCallback = null;
    OnClickElsewhereVoidCallback = null;
        
    }

    public void HandleClick(InputAction.CallbackContext ctx)
    {
        
        RaycastHit rcHit;

        if (ctx.started)
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(
                    Mouse.current.position.ReadValue()), out rcHit, Mathf.Infinity, inputLayerMask))
            {
                Debug.Log("ONCLICK");
                OnClickVoidCallback?.Invoke();
                isDragging = true;
                
            }
            else
            {
                
                Debug.Log("ONCLICKELSEWHERE");
                OnClickElsewhereVoidCallback?.Invoke();
            }
        }
        else if (ctx.canceled)
        {
            isDragging = false;
            OnReleaseVoidCallback?.Invoke();
        }

    }

    public void MouseDrag(InputAction.CallbackContext ctx)
    {
        Camera c = Camera.main;
        RaycastHit rcHit;


        Vector3 position = ctx.ReadValue<Vector2>();
        
        Ray ray = Camera.main.ScreenPointToRay(position);
        Plane plane = new Plane(new Vector3(0,Mathf.Cos(Mathf.Deg2Rad*45), Mathf.Sin(-Mathf.Deg2Rad*45)), Vector3.zero);
        float distance;
        
        if (plane.Raycast(ray, out distance))
        {
            Vector3 target = ray.GetPoint(distance);
            position = target - transform.position;
            position.Normalize();
            position.y += position.z;
        }
   
 
        if (isDragging)
        {
            OnDragVector3Callback?.Invoke(position);
            Debug.Log("Dragging");
        }
       
       

    }
}
