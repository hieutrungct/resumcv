using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Rubik_Casual.StageLevel.UI
{
    public class LevelUI : MonoBehaviour
    {
        public List<GameObject> Path, star;
        public GameObject lvlUITop, lvlUICentrel, lvlUIBottom, focus, iconBoss;
        public TextMeshProUGUI textLevel;
        public Sprite focusSprite, normalSprite, notCompleteSprite;
    }

}