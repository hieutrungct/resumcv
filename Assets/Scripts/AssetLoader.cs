using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Minimalist.Bar.Utility;
using Spine;
using Spine.Unity;
using NTPackage;

namespace Rubik_Casual
{
    public class AssetLoader : Singleton<AssetLoader>
    {
        // Start is called before the first frame update
        //public List<CardInfo> UnitCards;
        //public List<CardInfo> HeroCards;
        public List<Sprite> Avatars, Button, enemyAvatar;
        public List<SkeletonDataAsset> Hero;
        public List<SkeletonDataAsset> Enemy;
        public List<Sprite> RarrityBox;
        public List<Sprite> RarrityAvaBox, LabelRare;
        public List<Sprite> AttackSprite;
        public List<Sprite> EffectSprite;
        public List<Sprite> SkillSprite;
        public List<Sprite> imageSummon, Icon, imageBtn, imageWaifu, imageWaifuChibi;
        public GameObject cardPrefab;
        public static AssetLoader instance;
        private NTDictionary<string, Sprite> AvatarDic = new NTDictionary<string, Sprite>();
        private NTDictionary<string, Sprite> AvatarEnemyDic = new NTDictionary<string, Sprite>();
        private Dictionary<string, SkeletonData> HeroDic = new Dictionary<string, SkeletonData>();
        private Dictionary<string, SkeletonData> EnemyDic = new Dictionary<string, SkeletonData>();
        public override void Awake()
        {
            instance = this;
            Avatars = Resources.LoadAll<Sprite>("CamCapture/Waifu").ToList();
            enemyAvatar = Resources.LoadAll<Sprite>("CamCapture/Creeps").ToList();

            Hero = Resources.LoadAll<SkeletonDataAsset>("Character").ToList();

            Enemy = Resources.LoadAll<SkeletonDataAsset>("Enemy").ToList();

            SetUpAvaDic();
            foreach (Sprite sprite in enemyAvatar)
            {
                try
                {
                    AvatarEnemyDic.Add(sprite.name, sprite);
                }
                catch (Exception e)
                {
                    Debug.LogException(e);
                }
            }

            foreach (SkeletonDataAsset heroData in Hero)
            {
                try
                {
                    HeroDic.TryAdd(heroData.name, heroData.GetSkeletonData(false));
                }
                catch (Exception e)
                {
                    Debug.LogException(e);
                }
            }
            foreach (SkeletonDataAsset enemyData in Enemy)
            {
                try
                {
                    EnemyDic.TryAdd(enemyData.name, enemyData.GetSkeletonData(false));
                }
                catch (Exception e)
                {
                    Debug.LogException(e);
                }
            }
        }
        void SetUpAvaDic()
        {
            for (int i = 0; i < Avatars.Count; i++)
            {
                int indexWaifu = 0;
                indexWaifu = i + 1;
                string[] nameAvatar = Avatars[i].name.Split("_");
                if (nameAvatar.Length != 1)
                {
                    List<Sprite> CheckCountList = Avatars.FindAll(f => f.name.Split("_")[0] == Avatars[i].name.Split("_")[0]);
                    if (CheckCountList.Count != 2)
                    {
                        switch (nameAvatar[1])
                        {
                            case "A":
                                indexWaifu++;
                                AvatarDic.Add(indexWaifu.ToString(), Avatars[i]);
                                break;
                            case "S1":
                                indexWaifu--;
                                AvatarDic.Add(indexWaifu.ToString(), Avatars[i]);
                                break;
                            case "S01":
                                indexWaifu--;
                                AvatarDic.Add(indexWaifu.ToString(), Avatars[i]);
                                break;
                            default:
                                AvatarDic.Add(indexWaifu.ToString(), Avatars[i]);
                                break;
                        }
                    }
                    else
                    {
                        AvatarDic.Add(indexWaifu.ToString(), Avatars[i]);
                    }
                }
                else
                {
                    AvatarDic.Add(indexWaifu.ToString(), Avatars[i]);
                }

            }

        }
        public Sprite GetAvatarByIndex(int index)
        {
            return AvatarDic.Get(index.ToString());
        }

        public Sprite GetAvatarById(string index)
        {
            return AvatarDic.Get(index);
        }
        public Sprite GetAvatarEnemyByIndex(string index)
        {
            return AvatarEnemyDic.Get(index);
        }
        public SkeletonDataAsset GetAvaById(string id)
        {
            foreach (SkeletonDataAsset heroData in Hero)
            {
                if (heroData.name == id)
                {
                    return heroData;
                }
            }

            // Trường hợp không tìm thấy, bạn có thể trả về giá trị mặc định hoặc xử lý tùy theo nhu cầu.
            return null; // Hoặc trả về một giá trị khác để xác định sự thất bại, ví dụ: throw new Exception("Không tìm thấy")
        }
        public SkeletonDataAsset GetAvaByNameEn(string name)
        {
            foreach (SkeletonDataAsset enemyData in Enemy)
            {
                if (enemyData.name == name)
                {
                    return enemyData;
                }
            }
            return null;
        }
    }
}
