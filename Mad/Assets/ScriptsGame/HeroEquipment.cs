using Spine.Unity;
using UnityEngine;

public class HeroEquipment : MonoBehaviour
{
    [SerializeField]
    private SkeletonAnimation _skeletonAnimation;

    [SerializeField]
    private Skining skining;

    private WeaponData _leftArm, _rightArm;

    public WeaponData RightArm()
    {
        return _rightArm;
    }

    public WeaponData LeftArm()
    {
        return _leftArm;
    }

    private void Start()
    {
        EventBus.SetHeroEquipment.Invoke(this);
    }

    private void OnEnable()
    {
        EventBus.GetGunLeftArm.Subscribe(GetGunLeftArm);
        EventBus.GetGunRightArm.Subscribe(GetGunRightArm);
    }

    private void OnDestroy()
    {
        EventBus.GetGunLeftArm.Unsubscribe(GetGunLeftArm);
        EventBus.GetGunRightArm.Unsubscribe(GetGunRightArm);
    }

    private void GetGunLeftArm(string nameGun)
    {
        skining.AddWeapon(_skeletonAnimation.skeleton, nameGun, false);
        _leftArm = WeaponDatas.Instance.GetGunByName(nameGun);
    }

    private void GetGunRightArm(string nameGun)
    {
        skining.AddWeapon(_skeletonAnimation.skeleton, nameGun, true);
        _rightArm = WeaponDatas.Instance.GetGunByName(nameGun);
    }

    /*
    private void Start()
    {
        //_skeletonRenderer = GetComponent<SkeletonRenderer>();

        //var skeleton = _skeletonAnimation.Skeleton;
        //var _skeletonData = skeleton.Data;
        //Debug.Log(skeleton.FindSlot("GG_Arm_L2").SequenceIndex);
        //Debug.Log(skeleton.GetAttachment(skeleton.FindSlot("GG_Arm_L2").SequenceIndex, "GG_Arm_L2").Name);

        //Skining.SetSkinWithIndividualAttachments(_skeletonAnimation, "GG_Arm_L2", "GG_Arm_L2");
        
        var mixAndMatchSkin = new Skin("custom-girl");
        mixAndMatchSkin.AddSkin(_skeletonData.FindSkin("GUN/Revolver/Gun_L_Revolver"));
        mixAndMatchSkin.AddSkin(_skeletonData.FindSkin("GUN/Uzi/Gun_R_Uzi"));
        skeleton.SetSkin(mixAndMatchSkin);
        skeleton.SetSlotsToSetupPose();
    }/**/
}
