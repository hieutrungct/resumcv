using System.Collections;
using System.Collections.Generic;
using NTPackage;
using NTPackage.Functions;
using RubikCasual.Waifu;
using SimpleJSON;
using Sirenix.OdinInspector;
using Spine.Unity;
using UnityEditor;
using UnityEngine;

namespace Rubik.Waifu
{
    [System.Serializable]
    public class WaifuAssetData
    {
        public int Index;
        public string PathSkeleton;
        public Vector3 OriginScale;
        public string Skin;
        public string Anim_Idle;
        public string Anim_Atk;
        public string Anim_Die;
        public string Anim_Atked;
        public string Anim_Skill;
    }

    public class WaifuAssets : NTBehaviour
    {
        public const string Path_Assets_SO = "Assets/_Data/Resources/Waifu/SO";
        public const string Path_Resources_SO = "Waifu/SO";


        public NTDictionary<string, Transform> CacheHolder;
        public Transform Holder;

        public NTDictionary<string, WaifuSO> WaifuSODic;

        public TextAsset AssetData;
        public List<WaifuAssetData> WaifuAssetDatas;
        public InfoWaifuAssets infoWaifuAssets;
        public static WaifuAssets instance;

        [Button]
        public override void LoadComponents()
        {
            base.LoadComponents();
            this.LoadHolder();
        }

        protected void LoadHolder()
        {
            if (Holder != null) return;
            this.Holder = transform.Find("Holder").GetComponent<Transform>();
        }

        protected override void Start()
        {
            instance = this;
            this.WaifuAssetDatas = new List<WaifuAssetData>();
            this.CacheHolder = new NTDictionary<string, Transform>();
            foreach (JSONNode item in JSON.Parse(this.AssetData.text))
            {
                WaifuAssetData waifuAssetData = JsonUtility.FromJson<WaifuAssetData>(item.ToString());
                this.WaifuAssetDatas.Add(waifuAssetData);
            }
            infoWaifuAssets = JsonUtility.FromJson<InfoWaifuAssets>(Resources.Load<TextAsset>("Character").text);
        }

        public WaifuSO WaifuSO;
        [Button]
        public void TestGetSO()
        {
            this.WaifuSO = this.GetWaifuSOByIndex(Random.Range(0, 65).ToString());
        }
        public Transform Holder2D;
        [Button]
        public void TestGet2D()
        {
            this.Get2D(Random.Range(0, 65).ToString()).transform.SetParent(this.Holder2D);
        }
        public Transform HolderUI;
        [Button]
        public void TestGetUI()
        {
            this.GetUI(Random.Range(0, 65).ToString());
        }

        public WaifuSO GetWaifuSOByIndex(string index)
        {
            try
            {
                WaifuSO waifuSO = this.WaifuSODic.Get(index);
                if (waifuSO == null)
                {
                    waifuSO = Resources.Load<WaifuSO>(Path_Resources_SO + "/" + index);
                    if (waifuSO == null) throw null;
                    this.WaifuSODic.Add(index, waifuSO);
                    return waifuSO;
                }
                else
                {
                    return waifuSO;
                }
            }
            catch (System.Exception e)
            {
                Debug.LogWarning(e);
                return null;
            }
        }

        public SkeletonAnimation Get2D(string index)
        {
            WaifuSO waifuSO = this.GetWaifuSOByIndex(index);
            if (waifuSO == null) return null;
            SkeletonAnimation skeletonAnimation;
            try
            {
                skeletonAnimation = this.CacheHolder.Get(index + "2D").GetComponent<SkeletonAnimation>();
                if (skeletonAnimation == null) throw null;
            }
            catch (System.Exception)
            {
                skeletonAnimation = SkeletonAnimation.NewSkeletonAnimationGameObject(waifuSO.SkeletonDataAsset);
                skeletonAnimation.transform.SetParent(this.Holder);
                skeletonAnimation.transform.name = index + "2D";
                this.CacheHolder.Add(skeletonAnimation.name, skeletonAnimation.transform);
            }
            skeletonAnimation.initialSkinName = waifuSO.Skin;
            skeletonAnimation.Skeleton.SetSkin(waifuSO.Skin);
            return skeletonAnimation;
        }

        public SkeletonGraphic GetUI(string index, Transform parent = null)
        {
            WaifuSO waifuSO = this.GetWaifuSOByIndex(index);
            if (waifuSO == null) return null;
            SkeletonGraphic skeletonGraphic;
            try
            {
                skeletonGraphic = this.CacheHolder.Get(index + "UI").GetComponent<SkeletonGraphic>();
                if (skeletonGraphic == null) throw null;
            }
            catch (System.Exception)
            {
                skeletonGraphic = SkeletonGraphic.NewSkeletonGraphicGameObject(waifuSO.SkeletonDataAsset, parent, null);
                skeletonGraphic.transform.SetParent(this.Holder);
                skeletonGraphic.transform.name = index + "2D";
                this.CacheHolder.Add(skeletonGraphic.name, skeletonGraphic.transform);
            }
            skeletonGraphic.initialSkinName = waifuSO.Skin;
            skeletonGraphic.Skeleton.SetSkin(waifuSO.Skin);
            return skeletonGraphic;
        }

        public Vector3 GetOriginalScall(string index)
        {
            WaifuSO waifuSO = this.GetWaifuSOByIndex(index);
            if (waifuSO == null) return Vector3.one;
            return waifuSO.OriginScale;
        }

        public string GetAnimIdle(string index)
        {
            WaifuSO waifuSO = this.GetWaifuSOByIndex(index);
            if (waifuSO == null) return "Idle";
            return waifuSO.Anim_Idle;
        }
        public string GetAnimRun(string index)
        {
            WaifuSO waifuSO = this.GetWaifuSOByIndex(index);
            if (waifuSO == null) return "Idle";
            return waifuSO.Anim_Idle;
        }
        public string GetAnimAttack(string index)
        {
            WaifuSO waifuSO = this.GetWaifuSOByIndex(index);
            if (waifuSO == null) return "Idle";
            return waifuSO.Anim_Atk;
        }
        public string GetAnimAttacked(string index)
        {
            WaifuSO waifuSO = this.GetWaifuSOByIndex(index);
            if (waifuSO == null) return "Idle";
            return waifuSO.Anim_Atked;
        }
        public string GetAnimDie(string index)
        {
            WaifuSO waifuSO = this.GetWaifuSOByIndex(index);
            if (waifuSO == null) return "Idle";
            return waifuSO.Anim_Die;
        }
        public string GetAnimSkill(string index)
        {
            WaifuSO waifuSO = this.GetWaifuSOByIndex(index);
            if (waifuSO == null) return "Idle";
            return waifuSO.Anim_Skill;
        }

#if UNITY_EDITOR
        [Button]
        public void LoadSO()
        {
            foreach (WaifuAssetData item in this.WaifuAssetDatas)
            {
                string path = Path_Assets_SO + "/" + item.Index + ".asset";
                WaifuSO waifuSO = AssetDatabase.LoadAssetAtPath<WaifuSO>(Path_Assets_SO);
                waifuSO = ScriptableObject.CreateInstance<WaifuSO>();
                waifuSO.Index = item.Index;
                waifuSO.SkeletonDataAsset = AssetDatabase.LoadAssetAtPath<SkeletonDataAsset>(item.PathSkeleton);
                waifuSO.OriginScale = item.OriginScale;
                waifuSO.Skin = item.Skin;
                waifuSO.Anim_Idle = item.Anim_Idle;
                waifuSO.Anim_Atk = item.Anim_Atk;
                waifuSO.Anim_Die = item.Anim_Die;
                waifuSO.Anim_Atked = item.Anim_Atked;
                waifuSO.Anim_Skill = item.Anim_Skill;
                AssetDatabase.CreateAsset(waifuSO, path);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
                EditorUtility.FocusProjectWindow();
                Selection.activeObject = waifuSO;
            }
        }
#endif
    }
}

