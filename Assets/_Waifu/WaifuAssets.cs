using System.Collections;
using System.Collections.Generic;
using System.IO;
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
        public string Code;

        // public string[] Skins;
        // public int skinIndex;
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

        protected override void Awake()
        {
            instance = this;
            GetAssets();
            this.CacheHolder = new NTDictionary<string, Transform>();
            infoWaifuAssets = JsonUtility.FromJson<InfoWaifuAssets>(Resources.Load<TextAsset>("InfoWaifuAssets").text);

           
        }
        void GetAssets()
        {
            this.WaifuAssetDatas = new List<WaifuAssetData>();

            foreach (JSONNode item in JSON.Parse(this.AssetData.text))
            {
                WaifuAssetData waifuAssetData = JsonUtility.FromJson<WaifuAssetData>(item.ToString());
                this.WaifuAssetDatas.Add(waifuAssetData);
            }
        }
        public WaifuSO WaifuSO;
        [Button]
        public void TestGetSO(string index)
        {
            this.WaifuSO = this.GetWaifuSOByID(index);
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

        public WaifuSO GetWaifuSOByID(string ID)
        {
            if ( this.WaifuSODic ==null)
            {
                Debug.Log("null Dic");
                
            }
            try
            {
                WaifuSO waifuSO = this.WaifuSODic.Get(ID);
                if (waifuSO == null)
                {
                    waifuSO = Resources.Load<WaifuSO>(Path_Resources_SO + "/" + ID);
                    if (waifuSO == null) throw null;
                    this.WaifuSODic.Add(ID, waifuSO);
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
                Debug.LogError(ID);
                return null;
            }
        }


        public SkeletonAnimation Get2D(string index)
        {
            string ID = "";
            WaifuSO waifuSO = this.GetWaifuSOByID(ID);
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
            WaifuSO waifuSO = this.GetWaifuSOByID(index);
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
            WaifuSO waifuSO = this.GetWaifuSOByID(index);
            if (waifuSO == null) return Vector3.one;
            return waifuSO.OriginScale;
        }

        public string GetAnimIdle(string index)
        {
            WaifuSO waifuSO = this.GetWaifuSOByID(index);
            if (waifuSO == null) return "Idle";
            return waifuSO.Anim_Idle;
        }
        public string GetAnimRun(string index)
        {
            WaifuSO waifuSO = this.GetWaifuSOByID(index);
            if (waifuSO == null) return "Idle";
            return waifuSO.Anim_Idle;
        }
        public string GetAnimAttack(string index)
        {
            WaifuSO waifuSO = this.GetWaifuSOByID(index);
            if (waifuSO == null) return "Idle";
            return waifuSO.Anim_Atk;
        }
        public string GetAnimAttacked(string index)
        {
            WaifuSO waifuSO = this.GetWaifuSOByID(index);
            if (waifuSO == null) return "Idle";
            return waifuSO.Anim_Atked;
        }
        public string GetAnimDie(string index)
        {
            WaifuSO waifuSO = this.GetWaifuSOByID(index);
            if (waifuSO == null) return "Idle";
            return waifuSO.Anim_Die;
        }
        public string GetAnimSkill(string index)
        {
            WaifuSO waifuSO = this.GetWaifuSOByID(index);
            if (waifuSO == null) return "Idle";
            return waifuSO.Anim_Skill;
        }
        private WaifuAssetData old_item;

#if UNITY_EDITOR
        [Button]
        public void LoadSO()
        {
            // foreach (WaifuAssetData item in this.WaifuAssetDatas)
            // {
            //     int i = 1;
            //     if(old_item !=null)
            //     {
            //         if(old_item == item)
            //         {
            //             continue;
            //         }
                    
            //     }
            //     else
            //     {
            //         old_item = item;
            //         string path = Path_Assets_SO + "/" + i + ".asset";
            //         WaifuSO waifuSO = AssetDatabase.LoadAssetAtPath<WaifuSO>(Path_Assets_SO);
            //         waifuSO = ScriptableObject.CreateInstance<WaifuSO>();
            //         waifuSO.Index = item.Index;
            //         waifuSO.SkeletonDataAsset = AssetDatabase.LoadAssetAtPath<SkeletonDataAsset>(item.PathSkeleton);
            //         foreach (WaifuAssetData items in this.WaifuAssetDatas)
            //         {
            //             if(items.Code == item.Code)
            //             {
            //                 old_item = items;
            //                 waifuSO.SkeletonDataAsset_Skin = AssetDatabase.LoadAssetAtPath<SkeletonDataAsset>(items.PathSkeleton);
            //                 break;
            //             }
            //         }
            //         waifuSO.OriginScale = item.OriginScale;
            //         waifuSO.Skin = item.NameSkin;
            //         waifuSO.Anim_Idle = item.Anim_Idle;
            //         waifuSO.Anim_Atk = item.Anim_Atk;
            //         waifuSO.Anim_Die = item.Anim_Die;
            //         waifuSO.Anim_Atked = item.Anim_Atked;
            //         waifuSO.Anim_Skill = item.Anim_Skill;
            //         waifuSO.Code = item.Code;
            //         AssetDatabase.CreateAsset(waifuSO, path);
            //         AssetDatabase.SaveAssets();
            //         AssetDatabase.Refresh();
            //         EditorUtility.FocusProjectWindow();
            //         Selection.activeObject = waifuSO;
            //         i++;
            //     }
            //     i++;

            //     // string path = Path_Assets_SO + "/" + item.Index + ".asset";
            //     // WaifuSO waifuSO = AssetDatabase.LoadAssetAtPath<WaifuSO>(Path_Assets_SO);
            //     // waifuSO = ScriptableObject.CreateInstance<WaifuSO>();
            //     // waifuSO.Index = item.Index;
            //     // waifuSO.SkeletonDataAsset = AssetDatabase.LoadAssetAtPath<SkeletonDataAsset>(item.PathSkeleton);

            //     // //waifuSO.SkeletonDataAsset_Skin = AssetDatabase.LoadAssetAtPath<SkeletonDataAsset>(item.PathSkeleton);
            //     // waifuSO.OriginScale = item.OriginScale;
            //     // waifuSO.Skin = item.NameSkin;
            //     // waifuSO.Anim_Idle = item.Anim_Idle;
            //     // waifuSO.Anim_Atk = item.Anim_Atk;
            //     // waifuSO.Anim_Die = item.Anim_Die;
            //     // waifuSO.Anim_Atked = item.Anim_Atked;
            //     // waifuSO.Anim_Skill = item.Anim_Skill;
            //     // waifuSO.Code = item.Code;
            //     // AssetDatabase.CreateAsset(waifuSO, path);
            //     // AssetDatabase.SaveAssets();
            //     // AssetDatabase.Refresh();
            //     // EditorUtility.FocusProjectWindow();
            //     // Selection.activeObject = waifuSO;
            // }
            int i = 1;

            foreach (WaifuAssetData item in this.WaifuAssetDatas)
            {
                //Debug.Log("vào"+ old_item.Index);
                if (old_item != null)
                {
                    Debug.Log("vào"+ old_item.Index);
                    old_item = null;
                    continue;
                }

                

                string path = Path.Combine(Path_Assets_SO, i + ".asset");
                WaifuSO waifuSO = AssetDatabase.LoadAssetAtPath<WaifuSO>(path);

                if (waifuSO == null)
                {
                    waifuSO = ScriptableObject.CreateInstance<WaifuSO>();
                    waifuSO.ID = item.Index;
                    waifuSO.SkeletonDataAsset = AssetDatabase.LoadAssetAtPath<SkeletonDataAsset>(item.PathSkeleton);

                    // Tìm item có cùng Code và cập nhật SkeletonDataAsset_Skin
                    foreach (WaifuAssetData items in this.WaifuAssetDatas)
                    {
                        if (items.Code == item.Code && items.Index != item.Index)
                        {
                            old_item = item;
                            waifuSO.SkeletonDataAsset_Skin = AssetDatabase.LoadAssetAtPath<SkeletonDataAsset>(items.PathSkeleton);
                            waifuSO.Skin_Evol = items.Skin;
                            break;
                        }
                    }
                    

                    waifuSO.OriginScale = item.OriginScale;
                    waifuSO.Skin = item.Skin;
                    waifuSO.Anim_Idle = item.Anim_Idle;
                    waifuSO.Anim_Atk = item.Anim_Atk;
                    waifuSO.Anim_Die = item.Anim_Die;
                    waifuSO.Anim_Atked = item.Anim_Atked;
                    waifuSO.Anim_Skill = item.Anim_Skill;
                    waifuSO.Code = item.Code;

                    AssetDatabase.CreateAsset(waifuSO, path);
                    AssetDatabase.SaveAssets();
                    AssetDatabase.Refresh();
                    EditorUtility.FocusProjectWindow();
                    Selection.activeObject = waifuSO;

                    i++;
                }
            }

        }

#endif
    }
}

