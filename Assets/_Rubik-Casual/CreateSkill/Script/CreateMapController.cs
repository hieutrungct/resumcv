using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using RubikCasual.DailyItem;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RubikCasual.CreateSkill.Panel
{
    public enum TypeMap
    {
        Default_Map = 0,
        Infinity_Map = 1,
        Challenge_Map = 2,
    }
    public class CreateMapController : MonoBehaviour
    {
        public GameObject gbRowSlot, gbInfoSlot, gbCreateItem, gbCreateEnemy;
        public Transform TransParent;
        public List<RowSlot> lsRowSlot = new List<RowSlot>();
        public Vector3 originPosInfoSlot;
        public GameObject itemWithSlot;
        public Toggle toggleChoicePanel;
        public static CreateMapController instance;
        void Awake()
        {
            instance = this;
            originPosInfoSlot = gbInfoSlot.transform.position;
            gbRowSlot.transform.Find("Row").gameObject.AddComponent<RowSlot>();
            ChoseChoiceCreate();
        }
        public void ChoseChoiceCreate()
        {
            if (toggleChoicePanel.isOn)
            {
                gbCreateItem.SetActive(true);
                gbCreateEnemy.SetActive(false);
            }
            else
            {
                gbCreateItem.SetActive(false);
                gbCreateEnemy.SetActive(true);
            }
        }
        public void CreateMap()
        {
            int row = int.Parse(ChoseSlotMapPanel.instance.inputFieldRow.text);
            if (lsRowSlot != null)
            {
                foreach (var item in lsRowSlot)
                {
                    Destroy(item.transform.parent.gameObject);
                }
                lsRowSlot.Clear();
            }

            for (int i = 0; i < row; i++)
            {
                GameObject gbRowSlotClone = Instantiate(gbRowSlot, TransParent);
                gbRowSlotClone.SetActive(true);
                gbRowSlotClone.name = "GroupRow" + (i + 1);
                gbRowSlotClone.transform.Find("Row").gameObject.GetComponent<RowSlot>().idSlotRow = i + 1;
                gbRowSlotClone.transform.Find("Row").gameObject.GetComponent<RowSlot>().attribute = 1f;
                lsRowSlot.Add(gbRowSlotClone.transform.Find("Row").GetComponent<RowSlot>());
            }
            InfoStagePanel.instance.txtNumberTurn.text = "Number turn: " + row.ToString();
            InfoStagePanel.instance.txtTotalTurn.text = "Total Slot: " + (row * 5).ToString();
        }
        public Transform GetDraPosSlot(Vector3 PosCheck)
        {

            for (int i = 0; i < lsRowSlot.Count; i++)
            {
                for (int j = 0; j < lsRowSlot[i].GetComponent<RowSlot>().lsSlotInTurn.Count; j++)
                {
                    Vector3 PosSlot = lsRowSlot[i].GetComponent<RowSlot>().lsSlotInTurn[j].transform.position;
                    if (PosCheck.x > PosSlot.x - 1f &&
                        PosCheck.x < PosSlot.x + 1f &&
                        PosCheck.y > PosSlot.y - 1f &&
                        PosCheck.y < PosSlot.y + 1f
                        )
                    {
                        // Debug.Log("Slot" + (i + 1).ToString() + (j + 1).ToString());
                        return lsRowSlot[i].GetComponent<RowSlot>().lsSlotInTurn[j].transform;
                    }
                }
            }
            return null;
        }

        public void SaveStage()
        {
            List<Data.InfoStageAssetsData> lsInfoStageAssetsData = new List<Data.InfoStageAssetsData>();
            Data.StageAssetsData stageAssetsData = new Data.StageAssetsData();
            Data.RewardWinStage rewardWinStage = new Data.RewardWinStage();

            switch ((TypeMap)ChoseSlotMapPanel.instance.dropdownTypeMap.value)
            {
                case TypeMap.Challenge_Map:

                    foreach (RowSlot TurnStage in lsRowSlot)
                    {
                        Data.InfoStageAssetsData infoStageAssetsData = new Data.InfoStageAssetsData();
                        infoStageAssetsData.TurnStage = TurnStage.idSlotRow;
                        infoStageAssetsData.Slot0 = TurnStage.lsSlotInTurn[0].GetComponent<DragPosSlot>().stateSlot;
                        infoStageAssetsData.Slot1 = TurnStage.lsSlotInTurn[1].GetComponent<DragPosSlot>().stateSlot;
                        infoStageAssetsData.Slot2 = TurnStage.lsSlotInTurn[2].GetComponent<DragPosSlot>().stateSlot;
                        infoStageAssetsData.Slot3 = TurnStage.lsSlotInTurn[3].GetComponent<DragPosSlot>().stateSlot;
                        infoStageAssetsData.Slot4 = TurnStage.lsSlotInTurn[4].GetComponent<DragPosSlot>().stateSlot;
                        infoStageAssetsData.Attribute = TurnStage.attribute;

                        lsInfoStageAssetsData.Add(infoStageAssetsData);
                    }
                    break;

                case TypeMap.Infinity_Map:
                    foreach (RowSlot TurnStage in lsRowSlot)
                    {
                        Data.InfoStageAssetsData infoStageAssetsData = new Data.InfoStageAssetsData();
                        infoStageAssetsData.TurnStage = TurnStage.idSlotRow;
                        infoStageAssetsData.Slot0 = TurnStage.lsSlotInTurn[0].GetComponent<DragPosSlot>().stateSlot;
                        infoStageAssetsData.Slot1 = TurnStage.lsSlotInTurn[1].GetComponent<DragPosSlot>().stateSlot;
                        infoStageAssetsData.Slot2 = TurnStage.lsSlotInTurn[2].GetComponent<DragPosSlot>().stateSlot;
                        infoStageAssetsData.Slot3 = TurnStage.lsSlotInTurn[3].GetComponent<DragPosSlot>().stateSlot;
                        infoStageAssetsData.Slot4 = TurnStage.lsSlotInTurn[4].GetComponent<DragPosSlot>().stateSlot;
                        infoStageAssetsData.Attribute = TurnStage.attribute;

                        bool isBoss = false;
                        foreach (Transform slotInTurn in TurnStage.lsSlotInTurn)
                        {
                            if (slotInTurn.GetComponent<DragPosSlot>().isBoss)
                            {
                                isBoss = true;
                            }
                        }
                        if (isBoss)
                        {
                            infoStageAssetsData.Attribute = float.Parse(InfoStagePanel.instance.AttributeBeforeAttackBoss.text);
                        }

                        lsInfoStageAssetsData.Add(infoStageAssetsData);
                    }

                    break;
                case TypeMap.Default_Map:
                    foreach (RowSlot TurnStage in lsRowSlot)
                    {
                        Data.InfoStageAssetsData infoStageAssetsData = new Data.InfoStageAssetsData();
                        infoStageAssetsData.TurnStage = TurnStage.idSlotRow;
                        infoStageAssetsData.Slot0 = TurnStage.lsSlotInTurn[0].GetComponent<DragPosSlot>().stateSlot;
                        infoStageAssetsData.Slot1 = TurnStage.lsSlotInTurn[1].GetComponent<DragPosSlot>().stateSlot;
                        infoStageAssetsData.Slot2 = TurnStage.lsSlotInTurn[2].GetComponent<DragPosSlot>().stateSlot;
                        infoStageAssetsData.Slot3 = TurnStage.lsSlotInTurn[3].GetComponent<DragPosSlot>().stateSlot;
                        infoStageAssetsData.Slot4 = TurnStage.lsSlotInTurn[4].GetComponent<DragPosSlot>().stateSlot;
                        infoStageAssetsData.Attribute = float.Parse(InfoStagePanel.instance.AttributeBeforeAttackBoss.text);

                        lsInfoStageAssetsData.Add(infoStageAssetsData);
                    }

                    break;
            }

            stageAssetsData.typeMap = (TypeMap)ChoseSlotMapPanel.instance.dropdownTypeMap.value;
            stageAssetsData.lsStageAssetsDatas = lsInfoStageAssetsData;
            stageAssetsData.lsRewardWinStage = InfoRewardWinStagePanel.instance.lsRewardWinStage;

            if (stageAssetsData == null)
            {
                Debug.Log("Lỗi stage null");
            }
            else
            {
                // Ghi dữ liệu vào file JSON
                string json = JsonConvert.SerializeObject(stageAssetsData);
                string path = System.IO.Path.Combine("Assets/_Data/Resources/Stage/", InfoStagePanel.instance.NameStage.text + ".json");
                System.IO.File.WriteAllText(path, json);

                Debug.Log(json);

                Debug.Log("Đã lưu dữ liệu vào tệp JSON: " + path);
            }
        }
    }
    public class RowSlot : MonoBehaviour
    {
        public int idSlotRow;
        public float attribute;
        public List<Transform> lsSlotInTurn = new List<Transform>();
        TMP_InputField inputAttribute;
        private void Awake()
        {
            inputAttribute = this.transform.parent.Find("InputAttribute").GetComponent<TMP_InputField>();
        }
        void Start()
        {
            for (int i = 1; i < 6; i++)
            {
                // Debug.Log(this.idSlotRow);
                Transform tranSlotClone = transform.Find("Slot" + i);

                BoxCollider2D colliderSlotClone = tranSlotClone.gameObject.AddComponent<BoxCollider2D>();
                colliderSlotClone.size = new Vector2(150f, 150f);

                DragPosSlot dragPosSlot = tranSlotClone.gameObject.AddComponent<DragPosSlot>();
                dragPosSlot.idSlot = this.idSlotRow.ToString() + i;
                dragPosSlot.stateSlot = "null";
                lsSlotInTurn.Add(tranSlotClone);
            }
            switch ((TypeMap)ChoseSlotMapPanel.instance.dropdownTypeMap.value)
            {
                case TypeMap.Challenge_Map:
                    inputAttribute.onEndEdit.AddListener((string newValue) => { AddAttribute(newValue); });
                    break;
                default:
                    inputAttribute.gameObject.SetActive(false);
                    break;
            }
        }
        void AddAttribute(string newValue)
        {
            attribute = float.Parse(newValue);
        }
    }
    public class DragPosSlot : MonoBehaviour
    {
        public string idSlot;
        public string stateSlot;
        public bool isBoss;
        void OnMouseEnter()
        {
            GameObject gbInfoSlot = CreateMapController.instance.gbInfoSlot;
            gbInfoSlot.SetActive(true);
            gbInfoSlot.transform.Find("ValueSlot").GetComponent<TextMeshProUGUI>().text = idSlot;
            CreateMapController.instance.gbInfoSlot.transform.position = new Vector3(this.transform.position.x + 0.75f, this.transform.position.y, this.transform.position.z);

            gbInfoSlot.transform.Find("StateSlot").GetComponent<TextMeshProUGUI>().text = stateSlot;
            // Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // DragPosSlot dragPosSlotCheck = CreateMapController.instance.GetDraPosSlot(new Vector2(mousePosition.x, mousePosition.y));
            // if (dragPosSlotCheck == null)
            // {
            //     Debug.Log("Check bị null");
            // }
        }
        void OnMouseExit()
        {
            CreateMapController.instance.gbInfoSlot.SetActive(false);
            CreateMapController.instance.gbInfoSlot.transform.position = CreateMapController.instance.originPosInfoSlot;
        }
        void OnMouseDown()
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // Transform transParent = CreateMapController.instance.GetDraPosSlot(new Vector2(mousePosition.x, mousePosition.y));

            this.GetComponent<UnityEngine.UI.Image>().sprite = CreateMapController.instance.itemWithSlot.GetComponent<UnityEngine.UI.Image>().sprite;

            if (CreateMapController.instance.itemWithSlot.GetComponent<DragEnemyForSlot>() != null)
            {
                stateSlot = CreateMapController.instance.itemWithSlot.GetComponent<DragEnemyForSlot>().stateInfoWithSlot;
                isBoss = CreateMapController.instance.itemWithSlot.GetComponent<DragEnemyForSlot>().isBoss;
            }
            else
            {
                stateSlot = CreateMapController.instance.itemWithSlot.GetComponent<DragItemForSlot>().stateInfoWithSlot;
            }
        }
        private bool isDragging = false;
        private float dragStartTime = 0f;
        private float requiredHoldTime = 2f; // Thời gian giữ chuột cần thiết (tính bằng giây)

        void Update()
        {
            if (isDragging)
            {
                // Nếu người dùng đang giữ chuột, tính thời gian đã trôi qua

                // Debug.Log(dragStartTime);
                // Kiểm tra xem thời gian giữ chuột có đạt đủ yêu cầu không
                if (dragStartTime >= requiredHoldTime)
                {
                    // Nếu thời gian giữ chuột đạt yêu cầu, thực hiện thao tác 
                    stateSlot = "null";
                    this.GetComponent<UnityEngine.UI.Image>().sprite = null;
                }
            }
        }

        void OnMouseDrag()
        {
            isDragging = true;
            dragStartTime += Time.deltaTime;
        }

        void OnMouseUp()
        {
            isDragging = false;
            dragStartTime = 0f;
        }
    }
}