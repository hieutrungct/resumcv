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

namespace RubikCasual.Data.Waifu
{
    [System.Serializable]
    public class EnemyAssetData
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
        public bool Is_Boss;
    }

    public class EnemyAssets : NTBehaviour
    {
        public const string Path_Assets_SOE = "Assets/_Data/Resources/Waifu/SOE";
        public const string Path_Resources_SOE = "Waifu/SOE";

        public NTDictionary<string, Transform> CacheHolder;
        public Transform Holder;

        public NTDictionary<string, WaifuSO> WaifuSODic;

        public TextAsset AssetEnemyData;
        public List<EnemyAssetData> WaifuEnemyAssetDatas;

        public List<int> lsIdEnemy = new List<int>();
        public InfoWaifuAssets infoEnemyAssets;
        public static EnemyAssets instance;

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
            GetAssets();
            this.CacheHolder = new NTDictionary<string, Transform>();
            infoEnemyAssets = JsonUtility.FromJson<InfoWaifuAssets>(Resources.Load<TextAsset>("InfoEnemyAssets").text);

            lsIdEnemy.Clear();
            for (int i = 9001; i < 9093; i++)
            {
                try
                {
                    SkeletonAnimation WaifuEnemy = this.Get2D(i.ToString());
                    lsIdEnemy.Add(i);
                }
                catch (System.Exception)
                {

                }
            }
        }

        public InfoWaifuAsset GetInfoEnemyByIndex(int index)
        {
            InfoWaifuAsset infoWaifuAssetResult = new InfoWaifuAsset();
            foreach (InfoWaifuAsset infoWaifuAsset in infoEnemyAssets.lsInfoWaifuAssets)
            {
                if (infoWaifuAsset.Code == index.ToString())
                {
                    infoWaifuAssetResult = infoWaifuAsset;
                }
            }
            return infoWaifuAssetResult;
        }
        void GetAssets()
        {
            this.WaifuEnemyAssetDatas = new List<EnemyAssetData>();


            foreach (JSONNode item in JSON.Parse(this.AssetEnemyData.text))
            {
                EnemyAssetData waifuEnemyAssetData = JsonUtility.FromJson<EnemyAssetData>(item.ToString());
                this.WaifuEnemyAssetDatas.Add(waifuEnemyAssetData);
            }
        }
        public WaifuSO WaifuSO;
        [Button]
        public void TestGetSO(string index)
        {
            this.WaifuSO = this.GetWaifuSOEByIndex(index);
        }
        public Transform Holder2D;
        [Button]
        public void TestGet2D(string index)
        {
            this.Get2D(index).transform.SetParent(this.Holder2D);
        }
        public Transform HolderUI;
        [Button]
        public void TestGetUI(string index)
        {
            this.GetUI(index);
        }

        public WaifuSO GetWaifuSOEByIndex(string index)
        {
            try
            {
                WaifuSO waifuSO = this.WaifuSODic.Get(index);
                if (waifuSO == null)
                {
                    waifuSO = Resources.Load<WaifuSO>(Path_Resources_SOE + "/" + index);
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
            WaifuSO waifuSO = this.GetWaifuSOEByIndex(index);
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
            WaifuSO waifuSO = this.GetWaifuSOEByIndex(index);
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
            WaifuSO waifuSO = this.GetWaifuSOEByIndex(index);
            if (waifuSO == null) return Vector3.one;
            return waifuSO.OriginScale;
        }

        public string GetAnimIdle(string index)
        {
            WaifuSO waifuSO = this.GetWaifuSOEByIndex(index);
            if (waifuSO == null) return "Idle";
            return waifuSO.Anim_Idle;
        }
        public string GetAnimRun(string index)
        {
            WaifuSO waifuSO = this.GetWaifuSOEByIndex(index);
            if (waifuSO == null) return "Idle";
            return waifuSO.Anim_Idle;
        }
        public string GetAnimAttack(string index)
        {
            WaifuSO waifuSO = this.GetWaifuSOEByIndex(index);
            if (waifuSO == null) return "Idle";
            return waifuSO.Anim_Atk;
        }
        public string GetAnimAttacked(string index)
        {
            WaifuSO waifuSO = this.GetWaifuSOEByIndex(index);
            if (waifuSO == null) return "Idle";
            return waifuSO.Anim_Atked;
        }
        public string GetAnimDie(string index)
        {
            WaifuSO waifuSO = this.GetWaifuSOEByIndex(index);
            if (waifuSO == null) return "Idle";
            return waifuSO.Anim_Die;
        }
        public string GetAnimSkill(string index)
        {
            WaifuSO waifuSO = this.GetWaifuSOEByIndex(index);
            if (waifuSO == null) return "Idle";
            return waifuSO.Anim_Skill;
        }

#if UNITY_EDITOR
        [Button]
        public void LoadSOE()
        {
            foreach (EnemyAssetData item in this.WaifuEnemyAssetDatas)
            {
                string path = Path_Assets_SOE + "/" + item.Index + ".asset";
                WaifuSO waifuSO = AssetDatabase.LoadAssetAtPath<WaifuSO>(Path_Assets_SOE);
                waifuSO = ScriptableObject.CreateInstance<WaifuSO>();
                waifuSO.ID = item.Index;
                waifuSO.SkeletonDataAsset = AssetDatabase.LoadAssetAtPath<SkeletonDataAsset>(item.PathSkeleton);
                waifuSO.OriginScale = item.OriginScale;
                waifuSO.Skin = item.Skin;
                waifuSO.Anim_Idle = item.Anim_Idle;
                waifuSO.Anim_Atk = item.Anim_Atk;
                waifuSO.Anim_Die = item.Anim_Die;
                waifuSO.Anim_Atked = item.Anim_Atked;
                waifuSO.Anim_Skill = item.Anim_Skill;
                waifuSO.Is_Boss = item.Is_Boss;
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

