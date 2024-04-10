using System.Collections;
using System.Collections.Generic;
using NTPackage;
using RubikCasual.StageLevel;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace RubikCasual.StageLevel.UI
{
    public class LevelUIController : MonoBehaviour
    {

        public List<DotUI> lsDotUI;
        public Sprite focusSprite, normalSprite, notCompleteSprite;
        public int indexLength;
        public bool isClickWay;
        void Awake()
        {
            SetImageDot();
            SetLevelController();
        }
        void SetImageDot()
        {
            foreach (DotUI dotUI in lsDotUI)
            {
                dotUI.SetSpriteDot(focusSprite, normalSprite, notCompleteSprite);
            }
        }
        void SetLevelController()
        {
            foreach (DotUI dotUI in lsDotUI)
            {
                dotUI.SetlevelController(this);
            }
        }
        public void TestSetImage(PosLevelUI posLevelUI)
        {
            foreach (DotUI dotUI in lsDotUI)
            {
                if (posLevelUI != dotUI.posLevelUI)
                {
                    dotUI.SetImageDot(notCompleteSprite);
                }
            }
        }
        public DotUI GetDotUI(PosLevelUI posLevelUI)
        {
            DotUI dotUIResult = null;
            foreach (DotUI dotUI in lsDotUI)
            {
                if (posLevelUI == dotUI.posLevelUI)
                {
                    dotUIResult = dotUI;
                }
            }
            return dotUIResult;
        }

        public void SetIndexDot(int index)
        {
            foreach (DotUI dotUI in lsDotUI)
            {
                dotUI.index = index;
            }
        }
        public void SetImageDotNotCompleteSprite(PosLevelUI posLevelUI)
        {
            foreach (DotUI dotUI in lsDotUI)
            {
                if (posLevelUI == dotUI.posLevelUI)
                {
                    dotUI.SetImageDot(notCompleteSprite);
                }
            }
        }
        public void SetImageDotNormalSprite(PosLevelUI posLevelUI)
        {
            foreach (DotUI dotUI in lsDotUI)
            {
                if (posLevelUI == dotUI.posLevelUI)
                {
                    dotUI.SetImageDot(normalSprite);
                }
            }
        }
        public void SetImageDotFocusSprite(PosLevelUI posLevelUI)
        {
            foreach (DotUI dotUI in lsDotUI)
            {
                if (posLevelUI == dotUI.posLevelUI)
                {
                    dotUI.SetImageDot(focusSprite);
                }
            }
        }
        public void SetIdentifyLevelStage(PosLevelUI posLevelUI, IdentifyLevelStage identifyLevelStage)
        {
            foreach (DotUI dotUI in lsDotUI)
            {
                if (posLevelUI == dotUI.posLevelUI)
                {
                    dotUI.SetIdentifyLevelStage(identifyLevelStage);
                }
            }

        }
        public void SetActiveDotUI(PosLevelUI posLevelUI)
        {
            for (int i = 0; i < lsDotUI.Count; i++)
            {
                lsDotUI[i].posLevelUI = (PosLevelUI)i;
            }

            foreach (DotUI dotUI in lsDotUI)
            {
                if (posLevelUI == dotUI.posLevelUI)
                {
                    dotUI.gameObject.SetActive(true);
                }
            }
        }
        public void GetPathDotUI(PosLevelUI posLevelUI, NamePath namePath)
        {
            foreach (DotUI dotUI in lsDotUI)
            {
                if (posLevelUI == dotUI.posLevelUI)
                {
                    dotUI.GetPathActiveLevelUI(namePath);
                }
            }
        }
        public void GetRevertPathDotUI(PosLevelUI posLevelUI, NamePath namePath)
        {
            foreach (DotUI dotUI in lsDotUI)
            {
                if (posLevelUI == dotUI.posLevelUI)
                {
                    dotUI.GetRevertPathLevelUI(namePath);
                }
            }
        }
    }
}