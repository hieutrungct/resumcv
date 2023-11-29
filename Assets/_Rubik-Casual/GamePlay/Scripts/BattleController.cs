using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Rubik.Waifu;
using Spine.Unity;
using UnityEngine;

namespace RubikCasual.Battle
{
    public class BattleController : MonoBehaviour
    {
        public MapBattleController mapBattleController;
        public WaifuAssets waifuAssets;
        public List<HeroInArea> lsHeroInArea;
        void Start()
        {
            CreateBattlefield();
        }

        void CreateBattlefield()
        {
            CreateAreaHeroStart(lsHeroInArea);
        }
        void CreateAreaHeroStart(List<HeroInArea> lsHeroInArea)
        {
            for (int i = 0; i < mapBattleController.lsPosHeroSlot.Count; i++)
            {
                int index = i;
                PositionHeroSlot posSlot = mapBattleController.lsPosHeroSlot[i];

                if (lsHeroInArea[index].idHero != 0)
                {
                    SkeletonAnimation WaifuHero = waifuAssets.Get2D(lsHeroInArea[index].idHero.ToString());
                    waifuAssets.transform.localScale = waifuAssets.transform.localScale * 2f / 3f;
                    WaifuHero.gameObject.transform.SetParent(posSlot.gameObject.transform);
                    WaifuHero.gameObject.transform.position = posSlot.gameObject.transform.position;
                    WaifuHero.loop = true;
                    WaifuHero.AnimationName = "Idle";
                }
            }
        }
        [System.Serializable]
        public class HeroInArea
        {
            public float slotHero, idHero;
        }
    }

}