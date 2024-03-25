using System.Collections;
using System.Collections.Generic;
using NTPackage;
using RubikCasual.StageLevel;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace RubikCasual.StageLevel.UI
{
    public class LevelUI : MonoBehaviour
    {
        public List<GameObject> Path, star;
        public GameObject lvlUITop, lvlUICentrel, lvlUIBottom, focusTop, focusCentrel, focusBotton, iconBoss;
        public Image imageLvl;
        public TextMeshProUGUI textLevel;
        public Sprite focusSprite, normalSprite, notCompleteSprite;

        NTDictionary<NamePath, GameObject> dicPath;

        void Awake()
        {
            LoadPathSetDic();
            setPath();
        }
        void setPath()
        {
            foreach (var item in Path)
            {
                item.SetActive(true);
            }
        }
        void LoadPathSetDic()
        {
            foreach (GameObject path in Path)
            {
                if (System.Enum.TryParse<NamePath>(name, out NamePath namePath))
                {
                    // Kiểm tra xem đối tượng đã có trong Dictionary chưa trước khi thêm vào
                    if (dicPath.Get(namePath) == null)
                    {
                        // Thêm đối tượng vào Dictionary, với NamePath làm khóa
                        dicPath.Add(namePath, path);
                    }
                }

            }
        }
        public void SetUpLevelTop(PosLevelUI posLevelUIPrev)
        {

        }
        public void SetUpLevelBottom()
        {

        }
        public void SetUpLevelCenter()
        {

        }
        GameObject GetPathLevelUI(NamePath namePath)
        {
            return dicPath.Get(namePath);
        }
    }
    public enum PosLevelUI
    {
        LvlTop = 0,
        LvlCentrel = 1,
        LvlBottom = 2,
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