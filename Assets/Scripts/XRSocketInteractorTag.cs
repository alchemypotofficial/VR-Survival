using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XRSocketInteractorTag : XRSocketInteractor
{
    public Tags.TagType[] targetTags;

    public override bool CanSelect(XRBaseInteractable interactable)
    {
        if(interactable.gameObject.GetComponent<Tags>())
        {
            Tags tags = interactable.gameObject.GetComponent<Tags>();

            foreach(Tags.TagType tag in targetTags)
            {
                if(!tags.hasTag(tag))
                {
                    return false;
                }
            }

            return base.CanSelect(interactable);
        }

        return false;
    }
}
