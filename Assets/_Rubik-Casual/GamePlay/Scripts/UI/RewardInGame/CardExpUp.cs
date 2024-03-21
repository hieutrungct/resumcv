using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace RubikCasual.Battle.UI.Result
{
    public class CardExpUp : MonoBehaviour
    {
        public GameObject SlotWaifu;
        public List<GameObject> lsSlotWaifu = new List<GameObject>();
        public Transform transListcard;
        public bool isShowAnimDone;
        void Awake()
        {
            SetUpListSlotWaifu();
        }
        void SetUpListSlotWaifu()
        {
            foreach (Data.Player.CurentTeam curentTeam in Data.DataController.instance.playerData.userData.curentTeams)
            {
                if (curentTeam.ID != 0)
                {
                    GameObject SlotWaifuClone = Instantiate(SlotWaifu, transListcard);
                    SlotWaifuClone.SetActive(true);
                    Rubik.ListWaifu.WaifuItem waifuItem = SlotWaifuClone.GetComponent<Rubik.ListWaifu.WaifuItem>();
                    waifuItem.SetUp(Data.DataController.instance.GetPlayerOwnsWaifuByID(curentTeam.ID), false);
                    lsSlotWaifu.Add(SlotWaifuClone);
                }
            }
        }
        [Button]
        void ResetExpAndLevel()
        {
            foreach (var item in lsSlotWaifu)
            {
                item.GetComponent<Rubik.ListWaifu.WaifuItem>()._waifu.Exp = 0;
                item.GetComponent<Rubik.ListWaifu.WaifuItem>()._waifu.level = 1;
            }
        }
        float Duration = 1f;
        float DurationText = 0.00001f;
        [Button]
        void TestAnimExp()
        {
            SetAnimExp(30, 0);
        }
        [Button]
        void TestAnimExp2(int value)
        {
            SetAnimExp(value, 0);
        }
        public void SetAnimExp(int ValueExp, int SlotCard)
        {
            if (SlotCard < lsSlotWaifu.Count)
            {
                Rubik.ListWaifu.WaifuItem waifuItemClone = lsSlotWaifu[SlotCard].GetComponent<Rubik.ListWaifu.WaifuItem>();
                int oldExp = 0;
                oldExp = waifuItemClone._waifu.Exp;
                int oldLevel = 0;
                oldLevel = waifuItemClone._waifu.level;

                Data.Waifu.ExpWithLevel expWithLevel = Data.ExpCaculator.GetLevelWithValueExp(oldLevel, oldExp, ValueExp);

                StartCoroutine(AnimSliderExp(expWithLevel, waifuItemClone, oldLevel, oldExp));

                SetAnimExp(ValueExp, SlotCard + 1);
            }
            else
            {
                return;
            }
        }
        IEnumerator AnimSliderExp(Data.Waifu.ExpWithLevel expWithLevel, Rubik.ListWaifu.WaifuItem waifuItemClone, int oldLevel, int oldExp)
        {
            yield return new WaitForSeconds(2f);
            bool isHaveLvlUp = false;
            for (int i = oldLevel; i < expWithLevel.Level; i++)
            {
                waifuItemClone.lvProcessTxt.text = Data.DataController.instance.GetExpWithLevel(i).ToString() + "/" + Data.DataController.instance.GetExpWithLevel(i).ToString();
                Tween tween = waifuItemClone.exp.DOValue(1f, Duration / (expWithLevel.Level - oldLevel));
                waifuItemClone.levelTxt.text = i.ToString();
                yield return tween.WaitForCompletion();
                waifuItemClone.exp.value = 0;
                isHaveLvlUp = true;

            }
            if (!isHaveLvlUp)
            {
                StartCoroutine(UpTextAnim(oldExp, expWithLevel.FinalEXP, Data.DataController.instance.GetExpWithLevel(expWithLevel.Level), waifuItemClone.lvProcessTxt, DurationText));
            }
            else
            {
                StartCoroutine(UpTextAnim(0, expWithLevel.FinalEXP, Data.DataController.instance.GetExpWithLevel(expWithLevel.Level), waifuItemClone.lvProcessTxt, DurationText));
            }
            if (oldExp == 0)
            {
                waifuItemClone.lvProcessTxt.text = 0 + "/" + Data.DataController.instance.GetExpWithLevel(expWithLevel.Level).ToString();
            }
            waifuItemClone.exp.DOValue(expWithLevel.FinalEXP / (float)Data.DataController.instance.GetExpWithLevel(expWithLevel.Level), Duration).OnComplete(() =>
            {
                waifuItemClone._waifu.Exp = expWithLevel.FinalEXP;
                waifuItemClone._waifu.level = expWithLevel.Level;
                isShowAnimDone = true;
            });
            waifuItemClone.levelTxt.text = expWithLevel.Level.ToString();

        }

        IEnumerator UpTextAnim(int oldValue, int newValue, int maxValue, TextMeshProUGUI TextTarget, float duration)
        {
            if (oldValue <= newValue)
            {
                yield return new WaitForSeconds(duration);
                TextTarget.text = oldValue.ToString() + "/" + maxValue;

                switch (newValue - oldValue)
                {
                    case < 500:
                        StartCoroutine(UpTextAnim(oldValue + 5, newValue, maxValue, TextTarget, duration));
                        break;
                    case < 1000:
                        StartCoroutine(UpTextAnim(oldValue + 10, newValue, maxValue, TextTarget, duration));
                        break;
                    case < 2000:
                        StartCoroutine(UpTextAnim(oldValue + 25, newValue, maxValue, TextTarget, duration));
                        break;
                    case < 3000:
                        StartCoroutine(UpTextAnim(oldValue + 50, newValue, maxValue, TextTarget, duration));
                        break;
                    case < 5000:
                        StartCoroutine(UpTextAnim(oldValue + 65, newValue, maxValue, TextTarget, duration));
                        break;
                    default:
                        StartCoroutine(UpTextAnim(oldValue + 100, newValue, maxValue, TextTarget, duration));
                        break;
                }
            }
        }
    }
}