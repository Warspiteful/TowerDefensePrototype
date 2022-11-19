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
    public void Initialize(Vector3[] path)
    {
        _rigidbody = GetComponent<Rigidbody>();
        pathFinding = path;
        Debug.Log(path.Length);
        pathIndex = 0;
        currTarget = new Vector3(pathFinding[pathIndex].x+0.5f, 0, pathFinding[pathIndex].z+1);

        initialized = true;

    }

    private void Update()
    {
        if (initialized && pathIndex < pathFinding.Length )
        {
            Debug.Log("MOVING");
            transform.position = Vector3.MoveTowards(transform.position, currTarget, 0.5f*Time.deltaTime);
            if(currTarget.x == transform.position.x && currTarget.z == transform.position.z)
            {
                
                pathIndex += 1;
                if(pathIndex < pathFinding.Length){
                    currTarget = new Vector3(pathFinding[pathIndex].x+0.5f, 0, pathFinding[pathIndex].z+1);
                }
            }
            
        }
    }


}
