using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using RubikCasual.Data;
using RubikCasual.StageLevel.UI;
using SimpleJSON;
using Sirenix.OdinInspector;
using UnityEngine;

namespace RubikCasual.StageLevel
{
    public class StageLevelController : MonoBehaviour
    {
        public LevelUIController levelUIController;
        public List<GameObject> lsLevelUI;
        public LevelAssetData levelAssetData;
        public TextAsset levelAsset;

        public Transform TransCentrel;
        void Awake()
        {

        }
        public void AddIdentifyTurn(int index, PosLevelUI posLevelUI)
        {
            IdentifyTurn identifyTurn = new IdentifyTurn();
            identifyTurn.index = index;
            identifyTurn.posLevelUI = posLevelUI;
            if (this.levelAssetData.identifyTurns.Find(f => f.index == index) == null)
            {
                this.levelAssetData.identifyTurns.Add(identifyTurn);
            }
        }
        public void SetUpDot()
        {
            if (levelAsset == null)
            {
                TestCreateMapLevel(levelAssetData.lengthWay);
            }
            else
            {
                TestCreateMapLevelWhenHaveLevelAssets();
            }
        }


        // [Button]
        void TestCreateMapLevel(int CountWay)
        {
            int SlotStart = UnityEngine.Random.Range(0, 5);
            CreateMapLevel(CountWay, 0, SlotStart);
            AddDotAround();
        }
        void CreateMapLevel(int CountWay, int lengthWay, int SlotStart)
        {
            if (CountWay > lengthWay)
            {
                LevelUIController levelUIControllerClone = Instantiate(levelUIController, TransCentrel);
                levelUIControllerClone.SetIndexDot(lengthWay);
                levelUIControllerClone.SetUpStageLevelController(this);
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
        // [Button]
        void TestCreateMapLevelWhenHaveLevelAssets()
        {
            CreateMapLevelWhenHaveLevelAssets(0);
            AddDotAround();
            SetUpDotWhenHaveLevelAssets();
        }
        void CreateMapLevelWhenHaveLevelAssets(int index)
        {
            if (levelAssetData.lengthWay > index)
            {
                LevelUIController levelUIControllerClone = Instantiate(levelUIController, TransCentrel);
                levelUIControllerClone.index = index;
                levelUIControllerClone.SetIndexDot(index);
                levelUIControllerClone.SetUpStageLevelController(this);
                levelUIControllerClone.indexLength = levelAssetData.lengthWay;

                for (int i = 0; i < 5; i++)
                {
                    if (index != 0)
                    {
                        levelUIControllerClone.SetActiveDotUI((PosLevelUI)i);
                        levelUIControllerClone.SetImageDotNotCompleteSprite((PosLevelUI)i);
                    }
                }

                lsLevelUI.Add(levelUIControllerClone.gameObject);

                CreateMapLevelWhenHaveLevelAssets(index + 1);
            }
        }
        void SetUpDotWhenHaveLevelAssets()
        {
            foreach (GameObject gbLevelUI in lsLevelUI)
            {
                LevelUIController levelUIControllerClone = gbLevelUI.GetComponent<LevelUIController>();

                if (levelAssetData.identifyTurns.Count > levelUIControllerClone.index)
                {
                    PosLevelUI posLevelUI = levelAssetData.identifyTurns.Find(f => f.index == levelUIControllerClone.index).posLevelUI;
                    levelUIControllerClone.SetActiveDotUI(posLevelUI);
                    levelUIControllerClone.SetImageDotNormalSprite(posLevelUI);
                    levelUIControllerClone.isClickWay = true;

                    DotUI dotUI = levelUIControllerClone.GetDotUI(posLevelUI);
                    dotUI.SetPathAround();
                    for (int i = 0; i < 5; i++)
                    {
                        if (levelUIControllerClone.GetDotUI((PosLevelUI)i).imageLvl.sprite == levelUIControllerClone.focusSprite)
                        {
                            levelUIControllerClone.SetImageDotNotCompleteSprite((PosLevelUI)i);
                        }
                    }
                }

            }
        }
        [Button]
        public void SetUpLevelAsset(int idStage)
        {
            levelAsset = UnityEditor.AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/_Data/Resources/Stage/" + idStage + "/levelAssetData.json");
            if (levelAsset != null)
            {
                levelAssetData = JsonUtility.FromJson<LevelAssetData>(JSON.Parse(this.levelAsset.text).ToString());
            }
            else
            {
                levelAssetData.idStage = idStage;
                levelAssetData.lengthWay = DataController.instance.stageAssets.lsAssetData.Count;
            }
        }

        [Button]
        public void SaveLevelStage()
        {
            string json = JsonConvert.SerializeObject(levelAssetData);
            string path = System.IO.Path.Combine("Assets/_Data/Resources/Stage/" + levelAssetData.idStage + "/", "levelAssetData.json");
            System.IO.File.WriteAllText(path, json);

            Debug.Log(json);

            Debug.Log("Đã lưu dữ liệu vào tệp JSON: " + path);
        }
        // [Button]
        public void ResetLevel()
        {
            foreach (var item in lsLevelUI)
            {
                Destroy(item.gameObject);
            }
            lsLevelUI.Clear();
            levelAsset = null;
            levelAssetData.identifyTurns.Clear();
            this.gameObject.SetActive(false);
            
        }
    }
    [Serializable]
    public class LevelAssetData
    {
        public int idStage;
        public int lengthWay;
        public List<IdentifyTurn> identifyTurns;
    }
    [Serializable]
    public class IdentifyTurn
    {
        public int index;
        public PosLevelUI posLevelUI;
    }
}
