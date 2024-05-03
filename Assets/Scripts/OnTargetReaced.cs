using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnTargetReaced : MonoBehaviour
{
    public float threshold = 0.02f;
    public Transform target;
    public UnityEvent onReached;

    private bool wasReached = false;

    // Update is called once per frame
    void FixedUpdate()
    {
        float distance = Vector3.Distance(transform.position, target.position);

        if(distance < threshold && !wasReached)
        {
            onReached.Invoke();
            wasReached = true;
        }
        else if(distance >= threshold)
        {
            wasReached = false;
        }
    }
}
