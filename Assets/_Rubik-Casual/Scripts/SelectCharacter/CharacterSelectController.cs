using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Rubik_Casual;
using RubikCasual.Tool;
using Spine.Unity;
using Spine.Unity.Editor;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RubikCasual.Lobby
{
    public class CharacterSelectController : MonoBehaviour
    {
        public List<SlotCharacterUI> lsSlotCharacterUI;
        public GameObject TopPannel, bgMainscreen;
        UserData userData;
        float duration = 0.5f;
        public Vector3 scaleCharacter = new Vector3(1.75f, 1.75f, 1.75f);
        public float posOrigin;
        void Start()
        {
            userData = UserData.instance;
            posOrigin = this.transform.position.x;

        }
        void Update()
        {
            if (userData.data.isChange)
            {
                SetUpSlotStart();
                userData.data.isChange = false;
            }
        }
        void SetUpSlotStart()
        {
            for (int i = 0; i < lsSlotCharacterUI.Count; i++)
            {
                lsSlotCharacterUI[i].gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
                lsSlotCharacterUI[i].gameObject.GetComponent<Button>().onClick.AddListener(() =>
                {
                    userData.itemData.lsIdSlotSetupCharacter[i] = 0;
                });
                if (userData.data.lsIdSlotCharacter[i] == 0)
                {
                    lsSlotCharacterUI[i].waittingSlot.GetComponent<Button>().onClick.RemoveAllListeners();
                    lsSlotCharacterUI[i].waittingSlot.SetActive(true);
                    int index = i;
                    lsSlotCharacterUI[index].waittingSlot.GetComponent<Button>().onClick.AddListener(() =>
                    {
                        ChoseCharacter(index);
                    });

                }
                else
                {
                    lsSlotCharacterUI[i].waittingSlot.SetActive(false);

                    var slotClone = lsSlotCharacterUI[i].slotCharacter.AddComponent<SkeletonGraphic>();
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

            MovePopup.transPopupHorizontal(this.gameObject, bgMainscreen);
            if (this.gameObject.transform.position.x == posOrigin)
            {
                this.gameObject.SetActive(false);

            }
            TopPannel.SetActive(true);
        }
        public void OpenPopupSelectCharacter()
        {
            this.gameObject.SetActive(true);
            TopPannel.SetActive(false);
            MovePopup.transPopupHorizontal(bgMainscreen, this.gameObject);
        }
    }
}