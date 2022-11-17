using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//TODO: Make this a pooled object
[RequireComponent(typeof(UnitInput))]
public class PlacementUnit : MonoBehaviour
{
    private UnitInput _input;

    [SerializeField] private Direction dir;
    
    // Start is called before the first frame update
    void Start()
    {
        _input = GetComponent<UnitInput>();
        _input.RegisterMousePositionCallback(CallbackType.DRAG, ChooseDirection);
        _input.RegisterOnElsewhereClickCallback(Cancel);
    }

    public void Cancel()
    {
        Debug.Log("Cancel");
    }

    public void ChooseDirection(Vector3 direction)
    {
        Debug.Log(direction.x + ", " + direction.y + ", " + direction.z) ;
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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
