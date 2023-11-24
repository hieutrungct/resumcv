using System.Collections;
using System.Collections.Generic;
using System.IO;
using Rubik.Axie;
using Sirenix.OdinInspector;
using Spine.Unity;
using UnityEngine;
using UnityEngine.UI;

namespace Rubik.Axie
{
    public class AxieSelection : MonoBehaviour
    {
        public AxieData AxieData;
        public SkeletonAnimation runtimeSkeletonAnimation;

        public int back_Index = 0;
        public int body_Index = 0;
        public int ears_Index = 0;
        public int ear_Index = 0;
        public int eyes_Index = 0;
        public int horn_Index = 0;
        public int mouth_Index = 0;
        public int tail_Index = 0;
        public int body_class_Index = 0;
        public int colorVariant_Index = 0;
        public int accssory_slot_Index = 0;
        public int accessoryIdx_Index = 0;

        public List<Transform> Buttons;
        public Dictionary<string, Text> Text;

        public void Inc(string varible)
        {
            if (varible.Equals("back_Index"))
            {
                this.back_Index++;
            }
            if (varible.Equals("body_Index"))
            {
                this.body_Index++;
            }
            if (varible.Equals("ears_Index"))
            {
                this.ears_Index++;
            }
            if (varible.Equals("ear_Index"))
            {
                this.ear_Index++;
            }
            if (varible.Equals("eyes_Index"))
            {
                this.eyes_Index++;
            }
            if (varible.Equals("horn_Index"))
            {
                this.horn_Index++;
            }
            if (varible.Equals("mouth_Index"))
            {
                this.mouth_Index++;
            }
            if (varible.Equals("tail_Index"))
            {
                this.tail_Index++;
            }
            if (varible.Equals("body_class_Index"))
            {
                this.body_class_Index++;
            }
            if (varible.Equals("colorVariant_Index"))
            {
                this.colorVariant_Index++;
            }
            if (varible.Equals("accssory_slot_Index"))
            {
                this.accssory_slot_Index++;
            }
            if (varible.Equals("accessoryIdx_Index"))
            {
                this.accessoryIdx_Index++;
            }
            this.SetData();
            this.UpdateText();
        }
        public void Dec(string varible)
        {
            if (varible.Equals("back_Index"))
            {
                this.back_Index--;
                if (this.back_Index <= 0)
                {
                    this.back_Index = 0;
                }
            }
            if (varible.Equals("body_Index"))
            {
                this.body_Index--;
                if (this.body_Index <= 0)
                {
                    this.body_Index = 0;
                }
            }
            if (varible.Equals("ears_Index"))
            {
                this.ears_Index--;
                if (this.ears_Index <= 0)
                {
                    this.ears_Index = 0;
                }
            }
            if (varible.Equals("ear_Index"))
            {
                this.ear_Index--;
                if (this.ear_Index <= 0)
                {
                    this.ear_Index = 0;
                }
            }
            if (varible.Equals("eyes_Index"))
            {
                this.eyes_Index--;
                if (this.eyes_Index <= 0)
                {
                    this.eyes_Index = 0;
                }
            }
            if (varible.Equals("horn_Index"))
            {
                this.horn_Index--;
                if (this.horn_Index <= 0)
                {
                    this.horn_Index = 0;
                }
            }
            if (varible.Equals("mouth_Index"))
            {
                this.mouth_Index--;
                if (this.mouth_Index <= 0)
                {
                    this.mouth_Index = 0;
                }
            }
            if (varible.Equals("tail_Index"))
            {
                this.tail_Index--;
                if (this.tail_Index <= 0)
                {
                    this.tail_Index = 0;
                }
            }
            if (varible.Equals("body_class_Index"))
            {
                this.body_class_Index--;
                if (this.body_class_Index <= 0)
                {
                    this.body_class_Index = 0;
                }
            }
            if (varible.Equals("colorVariant_Index"))
            {
                this.colorVariant_Index--;
                if (this.colorVariant_Index <= 0)
                {
                    this.colorVariant_Index = 0;
                }
            }
            if (varible.Equals("accssory_slot_Index"))
            {
                this.accssory_slot_Index--;
                if (this.accssory_slot_Index <= 0)
                {
                    this.accssory_slot_Index = 0;
                }
            }
            if (varible.Equals("accessoryIdx_Index"))
            {
                this.accessoryIdx_Index--;
                if (this.accessoryIdx_Index <= 0)
                {
                    this.accessoryIdx_Index = 0;
                }
            }
            this.SetData();
            this.UpdateText();
        }
        void Start()
        {
            this.Text = new Dictionary<string, Text>();
            foreach (Transform item in Buttons)
            {
                Button inc = item.transform.Find("Inc").GetComponent<Button>();
                inc.onClick.AddListener(() => Inc(item.name));
                Button dec = item.transform.Find("Dec").GetComponent<Button>();
                dec.onClick.AddListener(() => Dec(item.name));
                this.Text.Add(item.name, item.Find("Text").GetComponent<Text>());
                Text text = item.Find("Text_Name").GetComponent<Text>();
                text.text = item.name.Replace("_Index", "");
            }
            StartCoroutine(this.Waite());
        }

        IEnumerator Waite()
        {
            yield return new WaitForSeconds(1);
            this.SetData();
            this.UpdateText();
        }

        public void UpdateText()
        {
            this.Text["back_Index"].text = AxieInit.instance.back[back_Index % AxieInit.instance.back.Count];
            this.Text["body_Index"].text = AxieInit.instance.body[body_Index % AxieInit.instance.body.Count];
            this.Text["ears_Index"].text = AxieInit.instance.ears[ears_Index % AxieInit.instance.ears.Count];
            this.Text["ear_Index"].text = AxieInit.instance.ear[ear_Index % AxieInit.instance.ear.Count];
            this.Text["eyes_Index"].text = AxieInit.instance.eyes[eyes_Index % AxieInit.instance.eyes.Count];
            this.Text["horn_Index"].text = AxieInit.instance.horn[horn_Index % AxieInit.instance.horn.Count];
            this.Text["mouth_Index"].text = AxieInit.instance.mouth[mouth_Index % AxieInit.instance.mouth.Count];
            this.Text["tail_Index"].text = AxieInit.instance.tail[tail_Index % AxieInit.instance.tail.Count];
            this.Text["body_class_Index"].text = AxieInit.instance.body_class[body_class_Index % AxieInit.instance.body_class.Count];
            this.Text["colorVariant_Index"].text = this.colorVariant_Index % 60 + "";
            this.Text["accssory_slot_Index"].text = AxieInit.instance.accsssory_slot[accssory_slot_Index % AxieInit.instance.accsssory_slot.Count];
            this.Text["accessoryIdx_Index"].text = this.accessoryIdx_Index + "";
        }

        [ContextMenu("SetData")]

        void SetData()
        {
            this.AxieData.back = AxieInit.instance.back[back_Index % AxieInit.instance.back.Count];
            this.AxieData.body = AxieInit.instance.body[body_Index % AxieInit.instance.body.Count];
            this.AxieData.ears = AxieInit.instance.ears[ears_Index % AxieInit.instance.ears.Count];
            this.AxieData.ear = AxieInit.instance.ear[ear_Index % AxieInit.instance.ear.Count];
            this.AxieData.eyes = AxieInit.instance.eyes[eyes_Index % AxieInit.instance.eyes.Count];
            this.AxieData.horn = AxieInit.instance.horn[horn_Index % AxieInit.instance.horn.Count];
            this.AxieData.mouth = AxieInit.instance.mouth[mouth_Index % AxieInit.instance.mouth.Count];
            this.AxieData.tail = AxieInit.instance.tail[tail_Index % AxieInit.instance.tail.Count];
            this.AxieData.body_class = AxieInit.instance.body_class[body_class_Index % AxieInit.instance.body_class.Count];
            this.AxieData.colorVariant = this.colorVariant_Index % 60;
            this.AxieData.accssory_slot = AxieInit.instance.accsssory_slot[accssory_slot_Index % AxieInit.instance.accsssory_slot.Count];
            this.AxieData.accessoryIdx = this.accessoryIdx_Index % 4;
            var summer0 = new Dictionary<string, string> {
                    {"back", AxieData.back},
                    {"body", AxieData.body},
                    {"ears", AxieData.ears},
                    {"ear", AxieData.ear},
                    {"eyes", AxieData.eyes},
                    {"horn", AxieData.horn},
                    {"mouth", AxieData.mouth},
                    {"tail", AxieData.tail},
                    {"body-class", AxieData.body_class},
                    {"body-id", " 2727 " },
                    {AxieData.accssory_slot, $"{AxieData.accssory_slot}1{System.Char.ConvertFromUtf32((int)('a') + AxieData.accessoryIdx - 1)}"}
                };
            float scale = 0.07f;
            var builderResult = AxieInit.instance.builder.BuildSpineAdultCombo(summer0, (byte)(AxieData.colorVariant), scale);
            if (this.runtimeSkeletonAnimation != null)
            {
                Destroy(this.runtimeSkeletonAnimation.gameObject);
            }
            this.runtimeSkeletonAnimation = SkeletonAnimation.NewSkeletonAnimationGameObject(builderResult.skeletonDataAsset);
            AxieModel model = runtimeSkeletonAnimation.gameObject.AddComponent<AxieModel>();
            model.AxieData = this.AxieData;
            runtimeSkeletonAnimation.state.SetAnimation(0, "action/idle/normal", true);
            runtimeSkeletonAnimation.state.TimeScale = 0.5f;
            runtimeSkeletonAnimation.transform.localScale = new Vector3(scale, scale, scale);
            runtimeSkeletonAnimation.transform.position = new Vector3(-4.85f, -1.45f, 0);

            LogDetail();
        }
        void LogDetail()
        {
            Debug.Log(JsonUtility.ToJson(AxieData));
            
        }
    }
  

}
