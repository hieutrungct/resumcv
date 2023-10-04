using System.Collections;
using System.Collections.Generic;
using RubikCasual.StageLevel;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace RubikCasual.StageLevel.UI
{
    public class LevelUI : MonoBehaviour
    {
        public List<GameObject> Path, star;
        public GameObject lvlUITop, lvlUICentrel, lvlUIBottom, focus, iconBoss;
        public Image imageLvl;
        public TextMeshProUGUI textLevel;
        public Sprite focusSprite, normalSprite, notCompleteSprite;


        void Update()
        {
            if (!StageLevelController.instance.infoLevelClone)
            {
                focus.SetActive(false);
            }
        }
    }

}