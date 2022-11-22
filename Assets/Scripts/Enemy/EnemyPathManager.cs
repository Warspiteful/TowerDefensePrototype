using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyPathManager : MonoBehaviour
{
    private Vector3[] pathFinding;
    [SerializeField] private int pathIndex;
    [SerializeField] private Vector3 currTarget;
    private Rigidbody _rigidbody;
    private bool initialized = false;

    private bool _isMoving;
    public void Initialize(Vector3[] path)
    {
        _rigidbody = GetComponent<Rigidbody>();
        pathFinding = path;

        transform.position = SetOffset(pathFinding[0]);
        pathIndex = 1;
        currTarget = SetOffset(pathFinding[pathIndex]);
        
        initialized = true;
        _isMoving = true;
    }

    private void Update()
    {
        if (initialized && pathIndex < pathFinding.Length && _isMoving )
        {
            transform.position = Vector3.MoveTowards(transform.position, currTarget, 0.5f*Time.deltaTime);
            if(currTarget.x == transform.position.x && currTarget.z == transform.position.z)
            {
                
                pathIndex += 1;
                if(pathIndex < pathFinding.Length-1){
                    currTarget = SetOffset(pathFinding[pathIndex]);
                }
                else if (pathIndex == pathFinding.Length - 1)
                {
                    currTarget = SetOffset(pathFinding[pathIndex])+new Vector3(0.5f,0,0);
                }    
            }
        }
    }

    public void ControlMoving(bool isBlocked)
    {
        _isMoving = !isBlocked;
    }

    private Vector3 SetOffset(Vector3 position)
    {
        return new Vector3(position.x+0.5f, transform.position.y, position.z+0.5f);
    }


}
