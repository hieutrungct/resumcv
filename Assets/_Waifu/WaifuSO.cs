using Spine.Unity;
using UnityEngine;

namespace RubikCasual.Waifu
{
    [CreateAssetMenu(fileName = "NewWaifuSO", menuName = "ScriptableObject/WaifuSO/New WaifuSO", order = 0)]
    public class WaifuSO : ScriptableObject
    {
        public bool FunnyCheck;
        public int ID;
        public SkeletonDataAsset SkeletonDataAsset;
        public SkeletonDataAsset SkeletonDataAsset_Skin;
        public Vector3 OriginScale;
        public string Skin;
        public string Anim_Idle;
        public string Anim_Atk;
        public string Anim_Die;
        public string Anim_Atked;
        public string Anim_Skill;
        public bool Is_Boss;
        public string Code;
        public string Skin_Evol;

    }
}