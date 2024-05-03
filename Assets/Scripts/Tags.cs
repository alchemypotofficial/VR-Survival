using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tags : MonoBehaviour
{
    public enum TagType
    {
        Backpack,
        SmallWeapon,
        LargeWeapon
    }

    public TagType[] tags;

    public bool hasTag(TagType tag)
    {
        foreach(TagType tagType in tags)
        {
            if(tagType == tag)
            {
                return true;
            }
        }

        return false;
    }
}
