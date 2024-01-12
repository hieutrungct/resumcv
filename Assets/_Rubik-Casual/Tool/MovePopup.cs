using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using RubikCasual.Battle.Inventory;
using TMPro;
using UnityEngine;

namespace RubikCasual.Tool
{
    public class MovePopup : MonoBehaviour
    {
        static float durations = 0.5f, valueMoveTxtDame = 0.5f;
        private static MovePopup instance;
        void Start()
        {
            instance = this;
        }
        public static int RandomIntWithList(List<int> lsIntInput)
        {
            List<int> lsIntRandom = new List<int>();
            foreach (int item in lsIntInput)
            {
                if (item != 0)
                {
                    lsIntRandom.Add(item);
                }
            }
            int NumberRandom = lsIntRandom[UnityEngine.Random.Range(0, lsIntRandom.Count)];
            return NumberRandom;
        }
        public static void TransPopupHorizontal(GameObject gbTaget, GameObject gbPopupOpen)
        {
            gbPopupOpen.SetActive(true);
            float tagetPopupMoveX = -gbPopupOpen.transform.position.x;
            float popupOpenMoveX = gbTaget.transform.position.x;

            gbPopupOpen.transform.DOMoveX(popupOpenMoveX, durations);
            gbTaget.transform.DOMoveX(tagetPopupMoveX, durations);
            instance.StartCoroutine(DeactivateAfterDelay(gbTaget, durations));
        }
        static IEnumerator DeactivateAfterDelay(GameObject gbTagetclone, float delay)
        {
            yield return new WaitForSeconds(delay);
            gbTagetclone.SetActive(false);
        }
        public static IEnumerator ShowTxtDame(GameObject gbParentClone, GameObject gbTxtDame, Vector3 posTxtDame, float DameItem, string typeItem)
        {
            switch (typeItem)
            {
                case "Heal":
                    GameObject txtDame = Instantiate(gbTxtDame, gbParentClone.transform);
                    txtDame.transform.position = new Vector2(posTxtDame.x, posTxtDame.y);
                    txtDame.GetComponent<TextMeshProUGUI>().text = "+" + DameItem.ToString();
                    txtDame.GetComponent<TextMeshProUGUI>().color = Color.green;
                    // Debug.Log(txtDame.transform.position.y);

                    txtDame.transform.DOMoveY(txtDame.transform.position.y + valueMoveTxtDame, durations);

                    gbParentClone.GetComponent<SlotInventory>().Icon.GetComponent<UnityEngine.UI.Image>().enabled = false;
                    yield return new WaitForSeconds(durations);

                    Destroy(txtDame);
                    Destroy(gbParentClone);
                    break;

                case "Poison":
                    GameObject txtDame2 = Instantiate(gbTxtDame, gbParentClone.transform);
                    txtDame2.transform.position = new Vector2(posTxtDame.x, posTxtDame.y);
                    txtDame2.GetComponent<TextMeshProUGUI>().text = "-" + DameItem.ToString();
                    txtDame2.GetComponent<TextMeshProUGUI>().color = Color.red;
                    // Debug.Log(txtDame2.transform.position.y);

                    txtDame2.transform.DOMoveY(txtDame2.transform.position.y + valueMoveTxtDame, durations);

                    gbParentClone.GetComponent<SlotInventory>().Icon.GetComponent<UnityEngine.UI.Image>().enabled = false;
                    yield return new WaitForSeconds(durations);

                    Destroy(txtDame2);
                    Destroy(gbParentClone);
                    break;
                case "Mana":
                    GameObject txtDame3 = Instantiate(gbTxtDame, gbParentClone.transform);
                    txtDame3.transform.position = new Vector2(posTxtDame.x, posTxtDame.y);
                    txtDame3.GetComponent<TextMeshProUGUI>().text = "+" + DameItem.ToString();
                    txtDame3.GetComponent<TextMeshProUGUI>().color = Color.yellow;
                    // Debug.Log(txtDame2.transform.position.y);

                    txtDame3.transform.DOMoveY(txtDame3.transform.position.y + valueMoveTxtDame, durations);

                    gbParentClone.GetComponent<SlotInventory>().Icon.GetComponent<UnityEngine.UI.Image>().enabled = false;
                    yield return new WaitForSeconds(durations);

                    Destroy(txtDame3);
                    Destroy(gbParentClone);
                    break;
            }
        }
        public static IEnumerator StartCooldown(UnityEngine.UI.Slider slider, float cooldownTime)
        {
            float timer = 0f;
            while (timer < cooldownTime)
            {
                timer += Time.deltaTime;
                slider.value = timer / cooldownTime;
                yield return null;
            }

            slider.value = 1f;
        }
        public static string GetNameImageWaifu(Spine.Unity.SkeletonGraphic skeletonGraphic)
        {
            string[] lsName = skeletonGraphic.skeletonDataAsset.name.Split("_");
            string NamePNG;
            if (lsName.Length == 4 && skeletonGraphic.initialSkinName != (lsName[0] + "_" + lsName[1]))
            {
                NamePNG = lsName[0] + "_" + lsName[1].Replace("0", "");
            }
            else
            {
                NamePNG = skeletonGraphic.initialSkinName;
            }
            return NamePNG.Replace("Pet", "");
        }
    }
}
