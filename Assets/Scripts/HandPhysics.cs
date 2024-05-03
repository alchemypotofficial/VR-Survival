using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandPhysics : MonoBehaviour
{
    public float followSpeed = 1800f;
    public float rotateSpeed = 6000f;

    public Transform target = null;

    private Rigidbody rigidBody = null;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        rigidBody.collisionDetectionMode = CollisionDetectionMode.Continuous;
        rigidBody.interpolation = RigidbodyInterpolation.Interpolate;
        rigidBody.mass = 1f;

        rigidBody.position = target.position;
        rigidBody.rotation = target.rotation;
    }

    private void Update()
    {
        PhysicsMove();   
    }

    private void PhysicsMove()
    {
        float distance = Vector3.Distance(target.position, transform.position);
        rigidBody.velocity = (target.position - transform.position).normalized * (followSpeed * distance) * Time.deltaTime;

        Quaternion rotation = target.rotation * Quaternion.Inverse(rigidBody.rotation);
        rotation.ToAngleAxis(out float angle, out Vector3 axis);
        rigidBody.angularVelocity = angle * (axis * Mathf.Deg2Rad * rotateSpeed) * Time.deltaTime;
    }
}
