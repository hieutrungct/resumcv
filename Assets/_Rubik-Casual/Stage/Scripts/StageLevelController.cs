using System.Collections;
using System.Collections.Generic;
using RubikCasual.Data;
using RubikCasual.StageLevel.UI;
using Sirenix.OdinInspector;
using UnityEngine;

namespace RubikCasual.StageLevel
{
    public class StageLevelController : MonoBehaviour
    {
        public LevelUIController levelUIController;
        public List<GameObject> lsLevelUI;
        public Transform TransCentrel;
        void Awake()
        {

        }

        // [Button]
        void AddDotAround()
        {
            for (int i = 0; i < lsLevelUI.Count; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    DotUI dotUI = lsLevelUI[i].GetComponent<LevelUIController>().GetDotUI((PosLevelUI)j);
                    if (dotUI != null)
                    {
                        AddDotActiveAroundWithIndex(i, dotUI.posLevelUI);
                    }
                }
            }
        }
        void AddDotActiveAroundWithIndex(int index, PosLevelUI posLevelUI)
        {
            for (int i = 0; i < lsLevelUI.Count; i++)
            {
                LevelUIController levelUIControllerClone = lsLevelUI[index].GetComponent<LevelUIController>();
                DotUI dotUITarget = levelUIControllerClone.GetDotUI(posLevelUI);
                int posLevelUINumber = (int)posLevelUI;
                if (index - 1 == i || index == i || index + 1 == i)
                {
                    for (int j = 0; j < 5; j++)
                    {

                        DotUI dotUI = lsLevelUI[i].GetComponent<LevelUIController>().GetDotUI((PosLevelUI)j);
                        if ((posLevelUINumber - 1 == j || posLevelUINumber == j || posLevelUINumber + 1 == j) && dotUI != null && dotUI != dotUITarget)
                        {
                            dotUITarget.AddListDotUIAround(dotUI);
                        }
                    }
                }

            }
            // Debug.Log(lsLevelUI[index].GetComponent<LevelUIController>().CheckActiveDot(posLevelUI));
        }

        [Button]
        void Test(int CountWay, int lengthWay, int SlotStart)
        {
            CreateMapLevel(CountWay, lengthWay, SlotStart);
            AddDotAround();
        }
        void CreateMapLevel(int CountWay, int lengthWay, int SlotStart)
        {
            if (CountWay > lengthWay)
            {
                LevelUIController levelUIControllerClone = Instantiate(levelUIController, TransCentrel);
                levelUIControllerClone.SetIndexDot(lengthWay);
                levelUIControllerClone.indexLength = CountWay;
                for (int i = 0; i < 5; i++)
                {
                    if (lengthWay == 0)
                    {
                        levelUIControllerClone.SetActiveDotUI((PosLevelUI)SlotStart);
                        levelUIControllerClone.SetImageDotFocusSprite((PosLevelUI)SlotStart);
                    }
                    else
                    {
                        levelUIControllerClone.SetActiveDotUI((PosLevelUI)i);
                        levelUIControllerClone.SetImageDotNotCompleteSprite((PosLevelUI)i);
                    }
                }

                lsLevelUI.Add(levelUIControllerClone.gameObject);
                CreateMapLevel(CountWay, lengthWay + 1, SlotStart);
            }
        }
        [Button]
        void ResetLevel()
        {
            foreach (var item in lsLevelUI)
            {
                Destroy(item.gameObject);
            }
            lsLevelUI.Clear();
        }
    }
}
