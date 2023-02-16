using Spine;
using Spine.Unity;
using UnityEngine;

public class Skining: MonoBehaviour 
{
    private Skin leftHand = null;
    private Skin rightHand = null;

    public void AddWeapon(Skeleton _skeleton, string slotName, bool isLeftHand = true)
    {
        var skeleton = _skeleton;
        var _skeletonData = skeleton.Data;
        var mixAndMatchSkin = new Skin("custom");
        if (isLeftHand)
        {
            if (rightHand != null)
            {
                mixAndMatchSkin.AddSkin(rightHand);
            }
            leftHand = _skeletonData.FindSkin(slotName);
        }
        else
        {
            if (leftHand != null)
            {
                mixAndMatchSkin.AddSkin(leftHand);
            }
            rightHand = _skeletonData.FindSkin(slotName);
        }

        mixAndMatchSkin.AddSkin(_skeletonData.FindSkin(slotName));

        skeleton.SetSkin(mixAndMatchSkin);
        skeleton.SetSlotsToSetupPose();
    }
    /*
    public static void SetSkinWithIndividualAttachments(SkeletonAnimation _skeleton, string slotName, string slotKey, string skinName = "my new skin")
    {
        var skeleton = _skeleton.skeleton;
        var skeletonData = skeleton.Data;
        Skin oldSkin = skeletonData.FindSkin("GUN/Uzi/Gun_L_Uzi");
        Skin newSkin = new Skin(skinName);
        int visorSlotIndex = skeleton.Data.FindSlot(slotName).Index;
        Debug.Log("I: "+visorSlotIndex);
        var attachment = oldSkin.GetAttachment(visorSlotIndex, slotName);
        newSkin.SetAttachment(visorSlotIndex, slotKey, attachment);

        skeleton.SetSkin(newSkin);
        skeleton.SetSlotsToSetupPose();
    }/**/
}
