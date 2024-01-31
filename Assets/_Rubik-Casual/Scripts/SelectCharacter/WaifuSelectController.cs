using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Rubik.Select;
using Rubik_Casual;
using RubikCasual.Data;
using RubikCasual.Data.Player;
using RubikCasual.Tool;
using RubikCasual.Waifu;
using Spine.Unity;
using Spine.Unity.Editor;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RubikCasual.Lobby
{
    public class WaifuSelectController : MonoBehaviour
    {
        public List<SlotWaifuSelectUI> lsSlotWaifuSelectUI;
        public GameObject bgMainscreen;
        UserData userData;
        Vector3 scaleCharacter = new Vector3(1.75f, 1.75f, 1.75f);
        public List<PlayerOwnsWaifu> Waifus;
        public Transform WaifuInitLocation;
        public SlotWaifuAva waifuAva;
        public static WaifuSelectController instance;
        void Start()
        {
            instance = this;
            userData = UserData.instance;
            CreatListSeclecWaifu();
            CreateListAvaWaifu();
            Waifus = DataController.instance.playerData.lsPlayerOwnsWaifu;
            SortPower();

            

        }
        void Update()
        {
            // if (userData.data.isChange)
            // {
            //     SetUpSlotStart();
            //     userData.data.isChange = false;
            // }
        }
        void CreatListSeclecWaifu()
        {
            foreach (SlotWaifuSelectUI item in lsSlotWaifuSelectUI)
            {
                item.avaBox_Obj.SetActive(false);
            }
            // for (int i = 0; i < lsSlotWaifuSelectUI.Count; i++)
            // {
            //     if(DataController.instance.userData.CurentTeam[i] == 0)
            //     {
            //         lsSlotWaifuSelectUI[i].avaBox_Obj.SetActive(false);
            //     }
            //     else
            //     {
            //         PlayerOwnsWaifu ownsWaifu = DataController.instance.GetPlayerOwnsWaifuByID(DataController.instance.userData.CurentTeam[i]);
            //         lsSlotWaifuSelectUI[i].SetUp(ownsWaifu);
            //     }
            // }
        }
        void CreateListAvaWaifu()
        {
            foreach (Transform child in WaifuInitLocation)
            {
                Destroy(child.gameObject);
            }
            for (int i = 0; i < DataController.instance.playerData.lsPlayerOwnsWaifu.Count; i++)
            {
                SlotWaifuAva slotWaifuAva = Instantiate(waifuAva, WaifuInitLocation);
                //Debug.Log("Id của waifu " + listWaifu.playerData.lsPlayerOwnsWaifu[i].Index.ToString());
                
                slotWaifuAva.SetUpItemAvaWaifu(DataController.instance.playerData.lsPlayerOwnsWaifu[i]);
            }

        }
        private void SortPower()
        {
            Waifus.Sort((charA, charB) =>
            {
                InfoWaifuAsset infoWaifuA = DataController.instance.GetInfoWaifuAssetsByIndex(charA.ID);
                InfoWaifuAsset infoWaifuB = DataController.instance.GetInfoWaifuAssetsByIndex(charB.ID);
                int result = (charA.Pow + charA.ATK).CompareTo(charB.Pow + charB.ATK);
                if (result == 0)
                {
                    result = charA.level.CompareTo(charB.level);
                    if (result == 0)
                    {
                        return infoWaifuA.Rare.CompareTo(infoWaifuB.Rare);
                    }
                }
                return result;
            });
            RefreshWaifuUI();
        }
        public void RefreshWaifuUI()
        {

            foreach (Transform child in WaifuInitLocation)
            {
                Destroy(child.gameObject);
            }

            for (int i = Waifus.Count - 1; i > -1; i--)
            {
                SlotWaifuAva slotWaifuAva = Instantiate(waifuAva, WaifuInitLocation);
                slotWaifuAva.SetUpItemAvaWaifu(Waifus[i]);
            }
        }
        void SetUpSlotStart()
        {
            for (int i = 0; i < lsSlotWaifuSelectUI.Count; i++)
            {
                lsSlotWaifuSelectUI[i].gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
                lsSlotWaifuSelectUI[i].gameObject.GetComponent<Button>().onClick.AddListener(() =>
                {
                    userData.itemData.lsIdSlotSetupCharacter[i] = 0;
                });
                if (userData.data.lsIdSlotCharacter[i] == 0)
                {
                    lsSlotWaifuSelectUI[i].waittingSlot.GetComponent<Button>().onClick.RemoveAllListeners();
                    lsSlotWaifuSelectUI[i].waittingSlot.SetActive(true);
                    int index = i;
                    lsSlotWaifuSelectUI[index].waittingSlot.GetComponent<Button>().onClick.AddListener(() =>
                    {
                        ChoseCharacter(index);
                    });

                }
                else
                {
                    lsSlotWaifuSelectUI[i].waittingSlot.SetActive(false);

                    SkeletonGraphic slotClone = lsSlotWaifuSelectUI[i].slotCharacter.AddComponent<SkeletonGraphic>();
                    slotClone.skeletonDataAsset = AssetLoader.instance.GetAvaById(userData.characterInfo.Characters.FirstOrDefault(f => f.ID == userData.data.lsIdSlotCharacter[i]).Nameid);
                    slotClone.initialSkinName = slotClone.skeletonDataAsset.GetSkeletonData(true).Skins.Items[1].Name;
                    slotClone.startingLoop = true;
                    slotClone.startingAnimation = "Idle";
                    slotClone.transform.localScale = scaleCharacter;
                    SpineEditorUtilities.ReinitializeComponent(slotClone);
                }
            }
        }
        void ChoseCharacter(int i)
        {
            int index = UnityEngine.Random.Range(1, 8);
            Debug.Log(i);
            userData.itemData.lsIdSlotSetupCharacter[i] = userData.characterInfo.Characters[index].ID;
        }
        public void loadScene(string sceneName)
        {
            if (sceneName == "CombatScene")
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
            }
        }
        public void BackPopupCharacter()
        {
            MovePopup.TransPopupHorizontal(this.gameObject, bgMainscreen);
        }
        public void OpenPopupSelectCharacter()
        {
            MovePopup.TransPopupHorizontal(bgMainscreen, this.gameObject);
        }

        public int CheckIndexOfWaifu(PlayerOwnsWaifu Waifu)
        {

            return Waifus.IndexOf(Waifu);

        }
        public PlayerOwnsWaifu GetWaifu(int index)
        {
            if (index >= DataController.instance.playerData.lsPlayerOwnsWaifu.Count)
            {
                index = 0;
            }
            else if (index < 0)
            {
                index = DataController.instance.playerData.lsPlayerOwnsWaifu.Count - 1;
            }

            return Waifus[index];
        }
        
        
    }
}