using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
using TMPro;
using UnityEngine;

namespace RubikCasual.FlipCard2
{
    public class InfoCard : MonoBehaviour
    {
        public TextMeshProUGUI txtNameWaifu, txtRare, txtValueFrag;
        public List<GameObject> lsGbStar;
        public Transform posWaifu;
        public SkeletonAnimation SkeWaifu;
    }

}