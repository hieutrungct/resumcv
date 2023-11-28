using Spine.Unity;
using UnityEngine;

namespace Rubik.Waifu
{
    [CreateAssetMenu(fileName = "NewWaifuSO", menuName = "ScriptableObject/WaifuSO/New WaifuSO", order = 0)]
    public class WaifuSO : ScriptableObject
    {
        public bool FunnyCheck;
        public int Index;
        public SkeletonDataAsset SkeletonDataAsset;
        public Vector3 OriginScale;
        public string Skin;
        public string Anim_Idle;
        public string Anim_Atk;
        public string Anim_Die;
        public string Anim_Atked;
        public string Anim_Skill;
    }
}