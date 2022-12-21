using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//TODO: Make this a pooled object
[RequireComponent(typeof(UnitInput))]
public class PlacementUnit : MonoBehaviour
{
    private UnitInput _input;

    [SerializeField] private Direction dir;

    private DirectionCallback _directionCallback;

    private SpriteRenderer _renderer;

    private VoidCallback onCancel;


    public void RegisterOnCancelCallback(VoidCallback callback)
    {
        onCancel += callback;
    }

    public void RegisterDirectionCallback(DirectionCallback callback)
    {
        _directionCallback += callback;
    }

    private void OnDisable()
    {
        Debug.Log("Disabled!!!!");
        _directionCallback = delegate(Direction direction) {  };
        onCancel  = delegate {  };
        _input.enabled = false;

    }

    public void Initialize(Sprite operatorSprite)
    {
        _renderer = GetComponent<SpriteRenderer>();
        _input = GetComponent<UnitInput>();
        _input.enabled = true;
        _input.RegisterMousePositionCallback(CallbackType.DRAG, ChooseDirection);
        _input.RegisterOnElsewhereClickCallback(Cancel);
        _input.RegisterOnReleaseVoidCallback(Complete);
        
        
        gameObject.SetActive(true);
        _renderer.sprite = operatorSprite;
    }
    
    public void Cancel()
    {
        Debug.Log("Cancel");
        _directionCallback = null;
        
     
            onCancel?.Invoke();

        gameObject.SetActive(false);
        
    }

    public void ChooseDirection(Vector3 direction)
    {
        float x = direction.x;
        float y = direction.y + direction.z;
        if (x > 0 && Mathf.Abs(x) > Mathf.Abs(y))
        {
            Debug.Log("Direction Right");
            dir = Direction.RIGHT;
        
        }
        else if (x < 0 && Mathf.Abs(x) > Mathf.Abs(y))
        {
            Debug.Log("Direction Left");
            dir = Direction.LEFT;
            
        }
        
        else if (y > 0 && Mathf.Abs(y) > Mathf.Abs(x))
        {
            Debug.Log("Direction Up");
            dir = Direction.UP;
     

        }
        else if (y < 0 && Mathf.Abs(y) > Mathf.Abs(x))
        {
            Debug.Log("Direction Down");
             dir = Direction.DOWN;
        
        }
        else
        {
            Debug.Log("NO DIRECTION");
        }
    }
    
    public void Complete()
    {
        Debug.Log("Complete");
      _directionCallback?.Invoke(dir);
        _directionCallback = null;
        gameObject.SetActive(false);

    }

}
