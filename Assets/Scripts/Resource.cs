using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
public class Resource : MonoBehaviour
{
    [Header("Resource Properties")]
    public int maxHealth = 100;
    public Transform dropTransform;
    public ResourceDrop[] drops;

    private int health = 0;
    protected bool isDestroyed = false;

    protected Rigidbody rigidBody;
    protected Collider currentToolCollider;

    private void Start()
    {
        health = maxHealth;
        rigidBody = gameObject.GetComponent<Rigidbody>();
        rigidBody.isKinematic = true;
        rigidBody.useGravity = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Collider collider = collision.collider;
        Tool tool = collider.gameObject.GetComponent<Tool>();

        if (tool)
        {
            DamageResource(tool.toolDamage);

            Debug.Log("Resource hit for: " + tool.toolDamage);
        }
    }

    protected virtual void OnDestroyed()
    {
        isDestroyed = true;
    }

    protected void SpawnDrops()
    {
        Vector3 spawnPosition = dropTransform.position;
        spawnPosition.y += 3f;

        Quaternion rotation = new Quaternion(0, 0, 0, 0);

        foreach (ResourceDrop drop in drops)
        {
            float randomValue = Random.Range(0, 100);
            if (randomValue < drop.chance)
            {
                Instantiate(drop.drop, spawnPosition, rotation);
            }
        }
    }

    public void DamageResource(int damage)
    {
        health -= damage;
        if(health < 0) { health = 0; }
    }

    public void DestroyResource()
    {
        health = 0;
    }

    protected virtual void Update()
    {
        if(health <= 0 && isDestroyed == false)
        {
            OnDestroyed();
        }
    }
}

[System.Serializable]
public class ResourceDrop
{
    public string name;
    public GameObject drop;
    public float chance;
}
