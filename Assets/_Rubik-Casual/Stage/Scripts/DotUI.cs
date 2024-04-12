using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using NTPackage;
using NTPackage.Functions;
using RubikCasual.Data;
using UnityEngine;
using UnityEngine.UI;

namespace RubikCasual.StageLevel.UI
{
    public class DotUI : MonoBehaviour
    {
        public int id;
        public int index;

        public Image imageLvl, imageIcon;
        public IdentifyLevelStage identifyLevelStage;
        public List<GameObject> Path, star;
        public PosLevelUI posLevelUI;
        public List<DotUI> dotUIArounds;
        public bool isShowPath, isPathTarget;
        IdentifyDot identifyDot;
        LevelUIController levelUIController;
        Sprite focusSprite, normalSprite, notCompleteSprite;
        DataController dataController;
        void Awake()
        {
            dataController = DataController.instance;
            imageLvl.gameObject.AddComponent<Button>().onClick.AddListener(() =>
            {
                if (imageLvl.sprite == focusSprite && id < levelUIController.indexLength)
                {
                    if (!isShowPath)
                    {
                        imageLvl.sprite = normalSprite;
                        levelUIController.isClickWay = true;
                        levelUIController.TestSetImage(this.posLevelUI);
                        levelUIController.GetStageLevelController().AddIdentifyTurn(index, posLevelUI);

                        if (identifyDot.typeDot == TypeDot.Attack)
                        {
                            dataController.stageAssets.LoadLevelStageAssets(identifyDot.Id);
                        }

                        SetPathAround();
                        levelUIController.GetStageLevelController().SaveLevelStage();
                        StartCoroutine(SwapGamePlay());
                    }
                    isShowPath = !isShowPath;
                }
            });
        }
        IEnumerator SwapGamePlay()
        {
            yield return new WaitForSeconds(0.25f);
            bl_SceneLoaderManager.LoadScene(NameScene.GAMEPLAY_SCENE);
        }
        public void SetUpIdentifyDot(IdentifyDot identifyDot)
        {
            this.identifyDot = identifyDot;
        }
        public void SetlevelController(LevelUIController levelUIController)
        {
            this.levelUIController = levelUIController;
        }
        public void SetSpriteDot(Sprite focusSprite, Sprite normalSprite, Sprite notCompleteSprite)
        {
            this.focusSprite = focusSprite;
            this.normalSprite = normalSprite;
            this.notCompleteSprite = notCompleteSprite;
        }
        public void AddListDotUIAround(DotUI dotUI)
        {
            if (dotUI != null && dotUIArounds.Find(f => f.index == dotUI.index && f.posLevelUI == dotUI.posLevelUI) == null)
            {
                dotUIArounds.Add(dotUI);
            }
        }

        public void SetPathAround()
        {
            if (id < levelUIController.indexLength)
            {
                foreach (DotUI dotUIAround in dotUIArounds)
                {
                    if (dotUIAround.imageLvl.sprite == notCompleteSprite)
                    {
                        // if (dotUIAround.index == this.index && (int)dotUIAround.posLevelUI == (int)posLevelUI - 1)
                        // {
                        //     this.GetPathActiveLevelUI(NamePath.Path_Top);
                        //     dotUIAround.SetImageDot(this.focusSprite);
                        //     dotUIAround.GetRevertPathLevelUI(NamePath.Path_Top);
                        //     dotUIAround.isPathTarget = true;
                        //     dotUIAround.id = id + 1;
                        // }
                        // else if (dotUIAround.index == this.index && (int)dotUIAround.posLevelUI == (int)posLevelUI + 1)
                        // {
                        //     this.GetPathActiveLevelUI(NamePath.Path_Bottom);
                        //     dotUIAround.SetImageDot(this.focusSprite);
                        //     dotUIAround.GetRevertPathLevelUI(NamePath.Path_Bottom);
                        //     dotUIAround.isPathTarget = true;
                        //     dotUIAround.id = id + 1;
                        // }
                        // else 
                        if (dotUIAround.index == this.index + 1)
                        {
                            IdentifyDot identifyDot = new IdentifyDot();
                            dotUIAround.gameObject.SetActive(true);
                            if ((int)dotUIAround.posLevelUI == (int)posLevelUI - 1)
                            {
                                this.GetPathActiveLevelUI(NamePath.Path_Top_Right);
                                dotUIAround.SetImageDot(this.focusSprite);
                                dotUIAround.GetRevertPathLevelUI(NamePath.Path_Top_Right);

                                identifyDot.typeDot = TypeDot.Shop;
                                dotUIAround.SetUpIdentifyDot(identifyDot);

                                dotUIAround.isPathTarget = true;
                                dotUIAround.id = id + 1;

                            }
                            else if ((int)dotUIAround.posLevelUI == (int)posLevelUI)
                            {
                                this.GetPathActiveLevelUI(NamePath.Path_Right);
                                dotUIAround.SetImageDot(this.focusSprite);
                                dotUIAround.GetRevertPathLevelUI(NamePath.Path_Right);


                                identifyDot.Id = dotUIAround.index;
                                identifyDot.typeDot = TypeDot.Attack;
                                dotUIAround.SetUpIdentifyDot(identifyDot);

                                dotUIAround.isPathTarget = true;
                                dotUIAround.id = id + 1;
                            }
                            else if ((int)dotUIAround.posLevelUI == (int)posLevelUI + 1)
                            {
                                this.GetPathActiveLevelUI(NamePath.Path_Bottom_Right);
                                dotUIAround.SetImageDot(this.focusSprite);
                                dotUIAround.GetRevertPathLevelUI(NamePath.Path_Bottom_Right);

                                identifyDot.typeDot = TypeDot.Special;
                                dotUIAround.SetUpIdentifyDot(identifyDot);

                                dotUIAround.isPathTarget = true;
                                dotUIAround.id = id + 1;
                            }
                        }
                    }
                }

                if (dotUIArounds.Find(f => f.index == index + 1) != null)
                {
                    dotUIArounds.Find(f => f.index == index + 1).levelUIController.HideDotAfterClickWay();
                }
            }
        }
        // void SetHidePathAround()
        // {
        //     foreach (DotUI dotUIAround in dotUIArounds)
        //     {
        //         if (GetPathLevelUI(NamePath.Path_Top).activeSelf && dotUIAround.index == this.index && (int)dotUIAround.posLevelUI == (int)posLevelUI - 1)
        //         {
        //             this.GetHidePathLevelUI(NamePath.Path_Top);
        //             dotUIAround.SetImageDot(this.notCompleteSprite);
        //             dotUIAround.GetHideRevertPathLevelUI(NamePath.Path_Top);
        //         }
        //         else if (GetPathLevelUI(NamePath.Path_Bottom).activeSelf && dotUIAround.index == this.index && (int)dotUIAround.posLevelUI == (int)posLevelUI + 1)
        //         {
        //             this.GetHidePathLevelUI(NamePath.Path_Bottom);
        //             dotUIAround.SetImageDot(this.notCompleteSprite);
        //             dotUIAround.GetHideRevertPathLevelUI(NamePath.Path_Bottom);

        //         }
        //         else if (GetPathLevelUI(NamePath.Path_Top_Right).activeSelf && dotUIAround.index == this.index + 1 && (int)dotUIAround.posLevelUI == (int)posLevelUI - 1)
        //         {
        //             this.GetHidePathLevelUI(NamePath.Path_Top_Right);
        //             dotUIAround.SetImageDot(this.notCompleteSprite);
        //             dotUIAround.GetHideRevertPathLevelUI(NamePath.Path_Top_Right);
        //         }
        //         else if (GetPathLevelUI(NamePath.Path_Right).activeSelf && dotUIAround.index == this.index + 1 && (int)dotUIAround.posLevelUI == (int)posLevelUI)
        //         {
        //             this.GetHidePathLevelUI(NamePath.Path_Right);
        //             dotUIAround.SetImageDot(this.notCompleteSprite);
        //             dotUIAround.GetHideRevertPathLevelUI(NamePath.Path_Right);
        //         }
        //         else if (GetPathLevelUI(NamePath.Path_Bottom_Right).activeSelf && dotUIAround.index == this.index + 1 && (int)dotUIAround.posLevelUI == (int)posLevelUI + 1)
        //         {
        //             this.GetHidePathLevelUI(NamePath.Path_Bottom_Right);
        //             dotUIAround.SetImageDot(this.notCompleteSprite);
        //             dotUIAround.GetHideRevertPathLevelUI(NamePath.Path_Bottom_Right);
        //         }
        //     }
        // }

        public void SetImageDot(Sprite spriteDot)
        {
            this.imageLvl.sprite = spriteDot;
        }

        public void SetIdentifyLevelStage(IdentifyLevelStage identifyLevelStage)
        {
            this.identifyLevelStage = identifyLevelStage;
        }
        public GameObject GetPathLevelUI(NamePath namePath)
        {
            foreach (var gbPath in Path)
            {
                if (gbPath.name == namePath.ToString())
                {
                    return gbPath;
                }
            }
            return null;
        }
        public GameObject GetPathActiveLevelUI(NamePath namePath)
        {
            foreach (var gbPath in Path)
            {
                if (gbPath.name == namePath.ToString())
                {
                    gbPath.SetActive(true);
                    return gbPath;
                }
            }
            return null;
        }
        public GameObject GetHidePathLevelUI(NamePath namePath)
        {
            foreach (var gbPath in Path)
            {
                if (gbPath.name == namePath.ToString())
                {
                    gbPath.SetActive(false);
                    return gbPath;
                }
            }
            return null;
        }
        public GameObject GetRevertPathLevelUI(NamePath namePath)
        {
            NamePath nameRevertPath = namePath;
            switch (namePath)
            {
                case NamePath.Path_Top:
                    nameRevertPath = NamePath.Path_Bottom;
                    break;
                case NamePath.Path_Bottom:
                    nameRevertPath = NamePath.Path_Top;
                    break;
                case NamePath.Path_Left:
                    nameRevertPath = NamePath.Path_Right;
                    break;
                case NamePath.Path_Right:
                    nameRevertPath = NamePath.Path_Left;
                    break;
                case NamePath.Path_Top_Left:
                    nameRevertPath = NamePath.Path_Bottom_Right;
                    break;
                case NamePath.Path_Bottom_Right:
                    nameRevertPath = NamePath.Path_Top_Left;
                    break;
                case NamePath.Path_Top_Right:
                    nameRevertPath = NamePath.Path_Bottom_Left;
                    break;
                case NamePath.Path_Bottom_Left:
                    nameRevertPath = NamePath.Path_Top_Right;
                    break;
            }
            foreach (var gbPath in Path)
            {
                if (gbPath.name == nameRevertPath.ToString())
                {
                    gbPath.SetActive(true);
                    return gbPath;
                }
            }
            return null;
        }
        public GameObject GetHideRevertPathLevelUI(NamePath namePath)
        {
            NamePath nameRevertPath = namePath;
            switch (namePath)
            {
                case NamePath.Path_Top:
                    nameRevertPath = NamePath.Path_Bottom;
                    break;
                case NamePath.Path_Bottom:
                    nameRevertPath = NamePath.Path_Top;
                    break;
                case NamePath.Path_Left:
                    nameRevertPath = NamePath.Path_Right;
                    break;
                case NamePath.Path_Right:
                    nameRevertPath = NamePath.Path_Left;
                    break;
                case NamePath.Path_Top_Left:
                    nameRevertPath = NamePath.Path_Bottom_Right;
                    break;
                case NamePath.Path_Bottom_Right:
                    nameRevertPath = NamePath.Path_Top_Left;
                    break;
                case NamePath.Path_Top_Right:
                    nameRevertPath = NamePath.Path_Bottom_Left;
                    break;
                case NamePath.Path_Bottom_Left:
                    nameRevertPath = NamePath.Path_Top_Right;
                    break;
            }
            foreach (var gbPath in Path)
            {
                if (gbPath.name == nameRevertPath.ToString())
                {
                    gbPath.SetActive(false);
                    return gbPath;
                }
            }
            return null;
        }
    }
    public class IdentifyDot
    {
        public int Id;
        public TypeDot typeDot;
    }
    public enum TypeDot
    {
        Shop = 0,
        Attack = 1,
        Special = 2,
    }
    [Serializable]
    public class IdentifyLevelStage
    {
        public int id;
        public NameLevelStage nameLevelStage;
    }
    public enum NameLevelStage
    {
        Shop = 0,
        LevelAttack = 1,
    }
    public enum PosLevelUI
    {
        LvlTop = 0,
        LvlTop_Centrel = 1,
        LvlCentrel = 2,
        LvlBottom_Centrel = 3,
        LvlBottom = 4,
    }

    public enum NamePath
    {
        Path_Left = 0,
        Path_Right = 1,
        Path_Top = 2,
        Path_Bottom = 3,
        Path_Top_Left = 4,
        Path_Top_Right = 5,
        Path_Bottom_Left = 6,
        Path_Bottom_Right = 7,
    }
}
