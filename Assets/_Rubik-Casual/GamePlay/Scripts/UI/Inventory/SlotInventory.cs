using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using RubikCasual.RewardInGame;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace RubikCasual.Battle.Inventory
{
    public class SlotInventory : MonoBehaviour
    {
        public int IdSlot, idItem;
        public GameObject Icon;
        public List<int> lsIdItem;
        public bool isChess;
        public Vector3 OriginPos, valuePos;
        public float JumbPower, NumberJumb, Duration, dividePowerJumb = 1f;
        public int valueCoins = 0, ValueGems = 0;
        Vector3 targetRotation = new Vector3(0, 0, 360f);
        public Ease ease;
        void Start()
        {
            OriginPos = gameObject.transform.position;
        }
        [Button]
        public void BtnTest()
        {
            int j = (int)JumbPower;
            PerformJump(j, j);
        }

        void PerformJump(int remainingJumps, int oldRemainingJumps)
        {
            if (remainingJumps > 0)
            {
                gameObject.transform.DOJump(gameObject.transform.position - valuePos, remainingJumps / dividePowerJumb, (int)NumberJumb, Duration)
                    .SetEase(ease)
                    .OnComplete(() =>
                    {
                        if (remainingJumps <= 1)
                        {
                            // Debug.Log(remainingJumps);
                            // gameObject.transform.DORotate(targetRotation, Duration).Loops();
                            // Destroy(this.gameObject);
                            if (valueCoins != 0)
                            {
                                gameObject.transform.SetParent(RewardInGamePanel.instance.txtCoins.transform.parent);
                                gameObject.transform.DOMove(RewardInGamePanel.instance.txtCoins.transform.parent.Find("Energe").position, Duration * 4)
                                .OnComplete(() =>
                                {
                                    SetAnimCoins();
                                    Destroy(gameObject);
                                });
                            }
                            else
                            {
                                gameObject.transform.SetParent(RewardInGamePanel.instance.txtGems.transform.parent);
                                gameObject.transform.DOMove(RewardInGamePanel.instance.txtGems.transform.parent.Find("Energe").position, Duration * 4)
                                .OnComplete(() =>
                                {
                                    SetAnimCoins();
                                    Destroy(gameObject);
                                });
                            }

                        }
                        PerformJump(remainingJumps - 1, oldRemainingJumps);
                    });
            }

        }
        public void SetAnimCoins()
        {
            if (valueCoins != 0)
            {
                RewardInGamePanel.instance.txtCoins.text = (valueCoins + float.Parse(RewardInGamePanel.instance.txtCoins.text)).ToString();
            }
            else
            {
                RewardInGamePanel.instance.txtGems.text = (ValueGems + float.Parse(RewardInGamePanel.instance.txtGems.text)).ToString();
            }
        }

        [Button]
        void ResetBtn()
        {
            gameObject.transform.position = OriginPos;
        }
    }

}