using System.Collections;
using System.Collections.Generic;
using AxieCore.SimpleJSON;
using AxieMixer.Unity;
using Spine.Unity;
using UnityEngine;
using UnityEngine.UI;
using Pixelplacement;
namespace Rubik.Axie
{

    public class AxieInit : MonoBehaviour
    {
        public TextAsset Data;

        public Button BtnRand;

        public List<string> back = new List<string>();
        public List<string> body = new List<string>();
        public List<string> ears = new List<string>();
        public List<string> ear = new List<string>();
        public List<string> eyes = new List<string>();
        public List<string> horn = new List<string>();
        public List<string> mouth = new List<string>();
        public List<string> tail = new List<string>();
        public List<string> body_class = new List<string>();
        public List<string> accsssory_slot = new List<string>();

        public string back_Input = "";
        public string body_Input = "";
        public string ears_Input = "";
        public string ear_Input = "";
        public string eyes_Input = "";
        public string horn_Input = "";
        public string mouth_Input = "";
        public string tail_Input = "";
        public string body_class_Input = "";
        public int colorVariant_Input = -1;
        public string accsssory_slot_Input = "";
        public int accessoryIdx_Input = -1;

        public TextAsset DataAxie;
        public TextAsset DataAxieStats;
        public NTPackage.NTDictionary<string, Axie> Axies;

        public Axie2dBuilder builder => Mixer.Builder;
        public Transform Content;

        public Transform Holder;

        public static AxieInit instance;
        private void Awake()
        {
            if (AxieInit.instance != null)
            {
                Debug.LogWarning("Only 1 instance allow");
                return;
            }
            AxieInit.instance = this;
        }

        void Start()
        {
            try
            {
                this.BtnRand.onClick.AddListener(this.GenRandom);
            }
            catch (System.Exception) { }
            Mixer.Init();
            JSONNode jData = JSONNode.Parse(Data.text);
            foreach (var item in jData["back"])
            {
                back.Add(item.ToString().Replace("[", "").Replace("]", "").Replace(",", "").Replace('"', ' ').Replace(" ", ""));
            }
            foreach (var item in jData["body"])
            {
                body.Add(item.ToString().Replace("[", "").Replace("]", "").Replace(",", "").Replace('"', ' ').Replace(" ", ""));
            }
            foreach (var item in jData["ears"])
            {
                ears.Add(item.ToString().Replace("[", "").Replace("]", "").Replace(",", "").Replace('"', ' ').Replace(" ", ""));
            }
            foreach (var item in jData["ear"])
            {
                ear.Add(item.ToString().Replace("[", "").Replace("]", "").Replace(",", "").Replace('"', ' ').Replace(" ", ""));
            }
            foreach (var item in jData["eyes"])
            {
                eyes.Add(item.ToString().Replace("[", "").Replace("]", "").Replace(",", "").Replace('"', ' ').Replace(" ", ""));
            }
            foreach (var item in jData["horn"])
            {
                horn.Add(item.ToString().Replace("[", "").Replace("]", "").Replace(",", "").Replace('"', ' ').Replace(" ", ""));
            }
            foreach (var item in jData["mouth"])
            {
                mouth.Add(item.ToString().Replace("[", "").Replace("]", "").Replace(",", "").Replace('"', ' ').Replace(" ", ""));
            }
            foreach (var item in jData["tail"])
            {
                tail.Add(item.ToString().Replace("[", "").Replace("]", "").Replace(",", "").Replace('"', ' ').Replace(" ", ""));
            }
            foreach (var item in jData["body-class"])
            {
                body_class.Add(item.ToString().Replace("[", "").Replace("]", "").Replace(",", "").Replace('"', ' ').Replace(" ", ""));
            }
            foreach (var item in jData["accsssory_slot"])
            {
                accsssory_slot.Add(item.ToString().Replace("[", "").Replace("]", "").Replace(",", "").Replace('"', ' ').Replace(" ", ""));
            }
            this.InitAxie();

        }
        public int total = 0;

        public void InitAxie()
        {
            this.Axies = new NTPackage.NTDictionary<string, Axie>();
            try
            {
                JSONNode jdata = JSONNode.Parse(this.DataAxie.text);
                foreach (JSONNode item in jdata["DataAxie"])
                {
                    AxieData axieData = JsonUtility.FromJson<AxieData>(item.ToString());
                    SkeletonAnimation skeletonAnimation = this.GenAxie(axieData);
                    skeletonAnimation.transform.SetParent(this.Holder);
                    skeletonAnimation.transform.name = axieData.Index.ToString();

                    SkeletonGraphic skeletonGraphic = this.GenAxieUI(axieData);
                    skeletonGraphic.transform.SetParent(this.Holder);
                    skeletonGraphic.transform.name = axieData.Index.ToString();

                    Axie axie;
                    try
                    {
                        axie = this.Axies.Get(axieData.Index.ToString());
                        if (axie == null) throw null;
                    }
                    catch (System.Exception)
                    {
                        axie = new Axie();
                        this.Axies.Add(axieData.Index.ToString(), axie);
                    }
                    axie.AxieData = axieData;
                    axie.Axie2D = skeletonAnimation;
                    axie.AxieUI = skeletonGraphic;
                }
            }
            catch (System.Exception e)
            {
                Debug.LogWarning(e);
            }

            try
            {
                JSONNode jdata = JSONNode.Parse(this.DataAxieStats.text);
                foreach (JSONNode item in jdata["AxieStatsData"])
                {
                    AxieStats axieStats = JsonUtility.FromJson<AxieStats>(item.ToString());
                    Axie axie;
                    try
                    {
                        axie = this.Axies.Get(axieStats.Index.ToString());
                        if (axie == null) throw null;
                    }
                    catch (System.Exception)
                    {
                        axie = new Axie();
                        this.Axies.Add(axieStats.Index.ToString(), axie);
                    }
                    axie.AxieStats = axieStats;
                }
            }
            catch (System.Exception e)
            {
                Debug.LogWarning(e);
            }
        }

        public void GenRandom()
        {
            AxieData axieData = new AxieData();
            axieData.back = this.back_Input.Length == 0 ? this.back[Random.Range(0, this.back.Count)] : this.back_Input;
            axieData.body = this.body_Input.Length == 0 ? this.body[Random.Range(0, this.body.Count)] : this.body_Input;
            axieData.ears = this.ears_Input.Length == 0 ? this.ears[Random.Range(0, this.ears.Count)] : this.ears_Input;
            axieData.ear = this.ear_Input.Length == 0 ? this.ear[Random.Range(0, this.ear.Count)] : this.ears_Input;
            axieData.eyes = this.eyes_Input.Length == 0 ? this.eyes[Random.Range(0, this.eyes.Count)] : this.eyes_Input;
            axieData.horn = this.horn_Input.Length == 0 ? this.horn[Random.Range(0, this.horn.Count)] : this.horn_Input;
            axieData.mouth = this.mouth_Input.Length == 0 ? this.mouth[Random.Range(0, this.mouth.Count)] : this.mouth_Input;
            axieData.tail = this.tail_Input.Length == 0 ? this.tail[Random.Range(0, this.tail.Count)] : this.tail_Input;
            axieData.body_class = this.body_class_Input.Length == 0 ? this.body_class[Random.Range(0, this.body_class.Count)] : this.body_class_Input;
            axieData.colorVariant = this.colorVariant_Input < 0 ? Random.Range(0, 60) : this.colorVariant_Input;
            axieData.accssory_slot = this.accsssory_slot_Input.Length == 0 ? this.accsssory_slot[Random.Range(0, this.accsssory_slot.Count)] : this.accsssory_slot_Input;
            axieData.accessoryIdx = this.accessoryIdx_Input < 0 ? Random.Range(1, 4) : this.accessoryIdx_Input;
            this.Gen(axieData);
        }

        public void Gen(AxieData axieData)
        {
            var summer0 = new Dictionary<string, string> {
                    {"back", axieData.back},
                    {"body", axieData.body},
                    {"ears", axieData.ears},
                    {"ear", axieData.ear},
                    {"eyes", axieData.eyes},
                    {"horn", axieData.horn},
                    {"mouth", axieData.mouth},
                    {"tail", axieData.tail},
                    {"body-class", axieData.body_class},
                    {"body-id", " 2727 " },
                    {axieData.accssory_slot, $"{axieData.accssory_slot}1{System.Char.ConvertFromUtf32((int)('a') + axieData.accessoryIdx - 1)}"}
                };
            Debug.LogError(axieData.accssory_slot + " :|: " + $"{axieData.accssory_slot}1{System.Char.ConvertFromUtf32((int)('a') + axieData.accessoryIdx - 1)}");
            float scale = 0.0032f;
            //var builderResult = builder.BuildSpineAdultCombo(adultCombo, (byte)(colorVariant + total), scale);
            var builderResult = builder.BuildSpineAdultCombo(summer0, (byte)(axieData.colorVariant), scale);

            // GameObject go = new GameObject("DemoAxie");
            // int row = total / 3;
            // int col = total % 3;
            // //go.transform.localPosition = new Vector3(row * 1.6f, col * 1.5f) - new Vector3(7.9f, 4.8f, 0);
            // go.transform.localPosition = new Vector3(row * 2.85f, col * 2.5f) - new Vector3(6.9f, 4.8f, 0);

            // SkeletonAnimation runtimeSkeletonAnimation = SkeletonAnimation.NewSkeletonAnimationGameObject(builderResult.skeletonDataAsset);
            SkeletonGraphic runtimeSkeletonAnimation = SkeletonGraphic.NewSkeletonGraphicGameObject(builderResult.skeletonDataAsset, this.Holder, this.builder.axieMixerMaterials.GetSampleGraphicMaterial(AxieFormType.Normal));
            //runtimeSkeletonAnimation.gameObject.layer = LayerMask.NameToLayer("Player");
            // runtimeSkeletonAnimation.transform.SetParent(go.transform, false);
            runtimeSkeletonAnimation.transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
            runtimeSkeletonAnimation.rectTransform.pivot = new Vector2(0.5f, 0.1f);
            total++;
            AxieModel model = runtimeSkeletonAnimation.gameObject.AddComponent<AxieModel>();
            model.AxieData = axieData;
            runtimeSkeletonAnimation.AnimationState.SetAnimation(0, "action/idle/normal", true);

            runtimeSkeletonAnimation.timeScale = 0.5f;
        }
        public SkeletonGraphic GenAxieUI(AxieData axieData)
        {
            var summer0 = new Dictionary<string, string> {
                    {"back", axieData.back},
                    {"body", axieData.body},
                    {"ears", axieData.ears},
                    {"ear", axieData.ear},
                    {"eyes", axieData.eyes},
                    {"horn", axieData.horn},
                    {"mouth", axieData.mouth},
                    {"tail", axieData.tail},
                    {"body-class", axieData.body_class},
                    {"body-id", " 2727 " },
                    {axieData.accssory_slot, $"{axieData.accssory_slot}1{System.Char.ConvertFromUtf32((int)('a') + axieData.accessoryIdx - 1)}"}
                };
            float scale = 0.0032f;
            var builderResult = builder.BuildSpineAdultCombo(summer0, (byte)(axieData.colorVariant), scale);
            SkeletonGraphic runtimeSkeletonAnimation = SkeletonGraphic.NewSkeletonGraphicGameObject(builderResult.skeletonDataAsset, this.Holder, this.builder.axieMixerMaterials.GetSampleGraphicMaterial(AxieFormType.Normal));
            runtimeSkeletonAnimation.transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
            runtimeSkeletonAnimation.AnimationState.SetAnimation(0, "action/idle/normal", true);
            runtimeSkeletonAnimation.timeScale = 0.5f;
            return runtimeSkeletonAnimation;
        }

        public SkeletonAnimation GenAxie(AxieData axieData)
        {
            var summer0 = new Dictionary<string, string> {
                    {"back", axieData.back},
                    {"body", axieData.body},
                    {"ears", axieData.ears},
                    {"ear", axieData.ear},
                    {"eyes", axieData.eyes},
                    {"horn", axieData.horn},
                    {"mouth", axieData.mouth},
                    {"tail", axieData.tail},
                    {"body-class", axieData.body_class},
                    {"body-id", " 2727 " },
                    {axieData.accssory_slot, $"{axieData.accssory_slot}1{System.Char.ConvertFromUtf32((int)('a') + axieData.accessoryIdx - 1)}"}
                };
            float scale = 0.07f;
            var builderResult = AxieInit.instance.builder.BuildSpineAdultCombo(summer0, (byte)(axieData.colorVariant), scale);
            SkeletonAnimation runtimeSkeletonAnimation = SkeletonAnimation.NewSkeletonAnimationGameObject(builderResult.skeletonDataAsset);
            AxieModel model = runtimeSkeletonAnimation.gameObject.AddComponent<AxieModel>();
            model.AxieData = axieData;
            runtimeSkeletonAnimation.state.SetAnimation(0, "action/idle/normal", true);
            runtimeSkeletonAnimation.state.TimeScale = 0.5f;
            runtimeSkeletonAnimation.transform.localScale = new Vector3(scale, scale, scale);
            runtimeSkeletonAnimation.transform.position = new Vector3(-4, -2.5f, 0);
            return runtimeSkeletonAnimation;
        }

        public SkeletonGraphic GetAxieUI(string index)
        {
            try
            {
                return this.Axies.Get(index).AxieUI;
            }
            catch (System.Exception e)
            {
                Debug.LogWarning(e);
                return null;
            }
        }

        public SkeletonAnimation GetAxie(string index)
        {
            try
            {
                return this.Axies.Get(index).Axie2D;
            }
            catch (System.Exception e)
            {
                Debug.LogWarning(e);
                return null;
            }
        }
 
        public AxieData GetAxieData(string index)
        {
            try
            {
                return this.Axies.Get(index).AxieData;
            }
            catch (System.Exception e)
            {
                Debug.LogWarning(e);
                return null;
            }
        }

        public AxieStats GetAxieStats(string index)
        {
            try
            {
               
                return this.Axies.Get(index).AxieStats;
            }
            catch (System.Exception e)
            {
                Debug.LogWarning(e);
                return null;
            }
        }
    }
}
