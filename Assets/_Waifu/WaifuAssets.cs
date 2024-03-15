using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using JetBrains.Annotations;
using NTPackage;
using NTPackage.Functions;
using RubikCasual.CreateSkill;
using RubikCasual.DailyItem;
using RubikCasual.Waifu;
using SimpleJSON;
using Sirenix.OdinInspector;
using Spine;
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
    [System.Serializable]
    public class WaifuSkill
    {
        public int Index;
        public string Code;
        public float percentDameSkill;
        public int Row;
        public int Column;
        public TypeSkill typeSkill;
        public int NumberTurn;
        public float DurationAttacked;
        public float DurationWave;
    }
    public class WaifuAssets : NTBehaviour
    {
        public const string Path_Assets_SO = "Assets/_Data/Resources/Waifu/SO";
        public const string Path_Resources_SO = "Waifu/SO";
        public NTDictionary<string, Transform> CacheHolder;
        public Transform Holder;
        public NTDictionary<string, WaifuSO> WaifuSODic;

        public TextAsset AssetData, AssetSkillData;
        public List<WaifuAssetData> WaifuAssetDatas;
        public List<WaifuSkill> waifuSkills;
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

            this.waifuSkills = new List<WaifuSkill>();

            foreach (JSONNode item in JSON.Parse(this.AssetSkillData.text))
            {
                WaifuSkill waifuSkillsAssetData = JsonUtility.FromJson<WaifuSkill>(item.ToString());
                this.waifuSkills.Add(waifuSkillsAssetData);
            }
        }

        public void SaveSkillDataToJson()
        {
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(waifuSkills);
            string path = System.IO.Path.Combine("Assets/_Data/Resources/Waifu" + "/", "Waifu_Skills.json");
            System.IO.File.WriteAllText(path, json);

            Debug.Log("Đã lưu dữ liệu vào tệp JSON: " + path);
        }
        [Button]
        public void SetSkillData()
        {
            foreach (var item in this.WaifuAssetDatas)
            {
                WaifuSkill waifuSkill = new WaifuSkill();
                if (waifuSkills.Find(f => f.Index == item.Index) == null)
                {
                    waifuSkill.Index = item.Index;
                    waifuSkill.Code = item.Code;
                    waifuSkills.Add(waifuSkill);
                }
            }
        }

        public Transform Holder2D;
        [Button]
        public void TestGet2D(string ID, bool isSkin)
        {
            this.Get2D(ID, isSkin).transform.SetParent(this.Holder2D);
        }
        public Transform HolderUI;
        [Button]
        public void TestGetUI(string index)
        {
            this.GetUI(index);
        }

        public int GetIndexWaifu(int id, bool isSkin = false)
        {
            InfoWaifuAsset infoWaifuAsset = new InfoWaifuAsset();
            infoWaifuAsset = infoWaifuAssets.lsInfoWaifuAssets.Find(f => f.ID == id);

            if (infoWaifuAsset != null)
            {
                List<WaifuAssetData> lsWaifuAssetData = new List<WaifuAssetData>();
                lsWaifuAssetData = WaifuAssetDatas.FindAll(f => f.Code == infoWaifuAsset.Code);

                if (lsWaifuAssetData.Count != 1)
                {
                    if (isSkin)
                    {
                        return lsWaifuAssetData[1].Index;
                    }
                    else
                    {
                        return lsWaifuAssetData[0].Index;
                    }
                }
                else
                {
                    return lsWaifuAssetData[0].Index;
                }

            }
            else
            {
                Debug.LogError("Không có Character có Id: " + id.ToString() + " hoặc character không có Bool isSkin");
                return 1;
            }

        }
        public InfoWaifuAsset GetInfoWaifuAsset(int id)
        {
            InfoWaifuAsset infoWaifuAsset = new InfoWaifuAsset();
            foreach (InfoWaifuAsset itemInfo in infoWaifuAssets.lsInfoWaifuAssets)
            {
                if (itemInfo.ID == id)
                {
                    infoWaifuAsset = itemInfo;
                }
            }
            return infoWaifuAsset;
        }
        public WaifuSkill GetSkillWaifuSOByIndex(int index)
        {
            WaifuSkill waifuSkill = new WaifuSkill();
            foreach (var item in waifuSkills)
            {
                if (item.Index == index)
                {
                    waifuSkill = item;
                }
            }
            return waifuSkill;
        }
        public WaifuSO GetWaifuSOByID(string ID)
        {
            if (this.WaifuSODic == null)
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


        public SkeletonAnimation Get2D(string ID, bool isSkin = false)
        {
            WaifuSO waifuSO = this.GetWaifuSOByID(ID);
            if (waifuSO == null) return null;
            SkeletonAnimation skeletonAnimation;

            try
            {
                skeletonAnimation = this.CacheHolder.Get(ID + "Default2D").GetComponent<SkeletonAnimation>();
                skeletonAnimation = this.CacheHolder.Get(ID + "Skin2D").GetComponent<SkeletonAnimation>();
                if (skeletonAnimation == null) throw null;
            }
            catch (System.Exception)
            {
                if (!isSkin)
                {
                    skeletonAnimation = SkeletonAnimation.NewSkeletonAnimationGameObject(waifuSO.SkeletonDataAsset);
                    skeletonAnimation.initialSkinName = waifuSO.Skin;
                    skeletonAnimation.Skeleton.SetSkin(waifuSO.Skin);
                    skeletonAnimation.transform.SetParent(this.Holder);
                    skeletonAnimation.transform.name = ID + "Default2D";
                }
                else
                {
                    skeletonAnimation = SkeletonAnimation.NewSkeletonAnimationGameObject(waifuSO.SkeletonDataAsset_Skin);
                    skeletonAnimation.initialSkinName = waifuSO.Skin_Evol;
                    skeletonAnimation.Skeleton.SetSkin(waifuSO.Skin_Evol);
                    skeletonAnimation.transform.SetParent(this.Holder);
                    skeletonAnimation.transform.name = ID + "Skin2D";
                }

                this.CacheHolder.Add(skeletonAnimation.name, skeletonAnimation.transform);
            }

            return skeletonAnimation;
        }

        public SkeletonGraphic GetUI(string index, bool isSkin = false, Transform parent = null)
        {
            WaifuSO waifuSO = this.GetWaifuSOByID(index);
            if (waifuSO == null) return null;
            SkeletonGraphic skeletonGraphic;

            try
            {
                skeletonGraphic = this.CacheHolder.Get(index + "DefaultUI").GetComponent<SkeletonGraphic>();
                skeletonGraphic = this.CacheHolder.Get(index + "SkinUI").GetComponent<SkeletonGraphic>();
                if (skeletonGraphic == null) throw null;
            }
            catch (System.Exception)
            {
                if (!isSkin)
                {
                    skeletonGraphic = SkeletonGraphic.NewSkeletonGraphicGameObject(waifuSO.SkeletonDataAsset, parent, null);
                    skeletonGraphic.transform.SetParent(this.Holder);
                    skeletonGraphic.initialSkinName = waifuSO.Skin;
                    skeletonGraphic.Skeleton.SetSkin(waifuSO.Skin);
                    skeletonGraphic.transform.name = index + "DefaultUI";
                }
                else
                {
                    skeletonGraphic = SkeletonGraphic.NewSkeletonGraphicGameObject(waifuSO.SkeletonDataAsset_Skin, parent, null);
                    skeletonGraphic.transform.SetParent(this.Holder);
                    skeletonGraphic.initialSkinName = waifuSO.Skin_Evol;
                    skeletonGraphic.Skeleton.SetSkin(waifuSO.Skin_Evol);
                    skeletonGraphic.transform.name = index + "SkinUI";
                }

                this.CacheHolder.Add(skeletonGraphic.name, skeletonGraphic.transform);
            }

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
                    Debug.Log("vào" + old_item.Index);
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

