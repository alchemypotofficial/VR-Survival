using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceTree : Resource
{
    protected override void Update()
    {
        base.Update();
    }

    protected override void OnDestroyed()
    {
        Vector3 oldPosition = gameObject.transform.position;
        Vector3 newPosition = new Vector3(oldPosition.x, oldPosition.y, oldPosition.z);
        gameObject.transform.position = newPosition;

        rigidBody.isKinematic = false;
        rigidBody.useGravity = true;
        rigidBody.AddForce(Vector3.forward, ForceMode.Impulse);

        StartCoroutine(DestroyTree());

        base.OnDestroyed();
    }

    private IEnumerator DestroyTree()
    {
        yield return new WaitForSeconds(10);
        SpawnDrops();
        Destroy(gameObject);
    }
}
