using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tool : MonoBehaviour
{
    [Header("Tool Properties")]
    public int toolDamage = 10;
    public Collider toolCollider = null;
    public Rigidbody rigidBody = null;

    protected virtual void Start()
    {
        rigidBody.maxAngularVelocity = 25;
    }
}
