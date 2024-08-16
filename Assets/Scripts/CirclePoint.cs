using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CirclePoint : MonoBehaviour
{
    public bool Boss;
    public Transform TargetPoint;
    public float Distance;

    private void Update()
    {
        if (!Boss)
        {
            if (Vector3.Distance(transform.position, TargetPoint.position) > Distance)
            {
                transform.position += (TargetPoint.position - transform.position).normalized *
                                      (Vector3.Distance(transform.position, TargetPoint.position) - Distance);
            }
        }
    }
}
