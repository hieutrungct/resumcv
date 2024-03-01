using System.Collections;
using System.Collections.Generic;
using NTPackage.Functions;
using Spine.Unity;
using UnityEngine;

namespace RubikCasual.Character_ACC
{
    public enum AssetsName
    {
        Waifu,
        Enemy
    }

    public class CharacterAssets : NTBehaviour
    {

        public AssetsName AssetsName;
        public Data.Waifu.WaifuAssets WaifuAssets;
        public Data.Waifu.EnemyAssets enemyAssets;



        public static CharacterAssets instance;
        protected override void Awake()
        {
            base.Awake();
            if (CharacterAssets.instance != null)
            {
                Debug.LogWarning("Only 1 instance allow");
                return;
            }
            CharacterAssets.instance = this;
        }
        public Waifu.InfoWaifuAsset GetInfoWaifuAsset(string code)
        {
            Waifu.InfoWaifuAsset infoWaifuAsset = new Waifu.InfoWaifuAsset();
            return this.WaifuAssets.GetInfoWaifuAsset(code);
        }
        public SkeletonAnimation Get2D(string index)
        {
            switch (this.AssetsName)
            {
                case AssetsName.Waifu:
                    return this.WaifuAssets.Get2D(index);
                default:
                    return this.WaifuAssets.Get2D(index);
            }

        }

        public SkeletonGraphic GetUI(string index)
        {
            switch (this.AssetsName)
            {
                case AssetsName.Waifu:
                    return this.WaifuAssets.GetUI(index);
                default:
                    return this.WaifuAssets.GetUI(index);
            }
        }

        public Vector3 GetOriginalScale(string index)
        {
            return this.WaifuAssets.GetOriginalScall(index);
        }
        public string GetAnimAttack(string index)
        {
            return this.WaifuAssets.GetAnimAttack(index);
        }
        public string GetAnimIdle(string index)
        {
            return this.WaifuAssets.GetAnimIdle(index);
        }
        public string GetAnimRun(string index)
        {
            return this.WaifuAssets.GetAnimRun(index);
        }
        public string GetAnimAttacked(string index)
        {
            return this.WaifuAssets.GetAnimAttacked(index);
        }
        public string GetAnimDie(string index)
        {
            return this.WaifuAssets.GetAnimDie(index);
        }
        public string GetAnimSkill(string index)
        {
            return this.WaifuAssets.GetAnimSkill(index);
        }
    }

}
