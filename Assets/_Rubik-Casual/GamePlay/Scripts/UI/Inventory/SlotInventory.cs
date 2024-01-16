using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
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
        Vector3 targetRotation = new Vector3(0, 0, 360f);
        public Ease ease;
        void Start()
        {
            OriginPos = gameObject.transform.position;
        }
        [Button]
        void BtnTest()
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
                        if (oldRemainingJumps == remainingJumps)
                        {
                            // gameObject.transform.DORotate(targetRotation, Duration).Loops();
                        }
                        PerformJump(remainingJumps - 1, oldRemainingJumps);
                    });
            }
            
        }

        [Button]
        void ResetBtn()
        {
            gameObject.transform.position = OriginPos;
        }
    }

}