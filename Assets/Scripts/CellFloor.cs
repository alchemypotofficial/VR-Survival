using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellFloor : MonoBehaviour
{
    private Transform transform;
    private int hitCount;

    void Start()
    {
        transform = gameObject.GetComponent<Transform>();
    }

    void Update()
    {
        if (transform.position.y < -100f)
        {
            if (hitCount > 3)
            {
                Destroy(gameObject);
            }

            hitCount++;
            transform.position = new Vector3(transform.position.x, 30f, transform.position.z);
        }
    }
}
