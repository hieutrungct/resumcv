using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Minimalist.Bar.Utility;
using Spine;
using System.Collections.Generic;
using Spine.Unity;

namespace Rubik_Casual
{
    public class AssetLoader : Singleton<AssetLoader>
    {
        // Start is called before the first frame update
        //public List<CardInfo> UnitCards;
        //public List<CardInfo> HeroCards;
        public List<Sprite> Avatars;
        public List<SkeletonDataAsset> Hero; 
        public List<Sprite> RarrityBox;
        public List<Sprite> AttackSprite;
        public List<Sprite> EffectSprite;
        public List<Sprite> SkillSprite;
        public GameObject cardPrefab;
        public static AssetLoader instance; 
        private Dictionary<string, Sprite> AvatarDic = new Dictionary<string, Sprite>();
        private Dictionary<string, SkeletonData> HeroDic = new Dictionary<string, SkeletonData>();
    
        public override void Awake()
        {
            instance = this;
            Avatars = Resources.LoadAll<Sprite>("Character").ToList();
            Hero = Resources.LoadAll<SkeletonDataAsset>("Character").ToList();
            
            foreach (Sprite sprite in Avatars)
            {
                try
                {
                    AvatarDic.TryAdd(sprite.name, sprite);
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
            
        }
    
        
    
        public Sprite GetAvatarById(string id)
        {
            return AvatarDic[id];
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
    
        
    }
}
