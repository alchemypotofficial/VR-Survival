using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [Header("Item Properties")]
    public string displayName = "Item";
    
    public int count = 1;
    public int maxStackSize = 1;
    public bool inventoriable = true;

    public int IncreaseStack(int amount)
    {
        if(count + amount > maxStackSize)
        {
            count = maxStackSize;
            return (count + amount) - maxStackSize;
        }
        else
        {
            count += amount;
            return 0;
        }
    }

    public bool DecreaseStack(int amount)
    {
        count -= amount;
        if(count < 0) { count = 0; }

        if(count == 0)
        {
            Destroy(gameObject);
            return true;
        }

        return false;
    }
}
