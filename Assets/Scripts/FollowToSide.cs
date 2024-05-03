using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowToSide : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public Vector3 rotation;

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = target.position + Vector3.up * offset.y
            + target.right * offset.x
            + target.forward * offset.z;

        transform.eulerAngles = new Vector3(0 + rotation.x, target.eulerAngles.y + rotation.y, 0 + rotation.z);
    }
}
