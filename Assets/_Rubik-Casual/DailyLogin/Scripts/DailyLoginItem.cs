using System.Collections;
using System.Collections.Generic;
using RubikCasual.Lobby;
using RubikCasual.RewardMonth;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RubikCasual.DailyLogin
{
    public class DailyLoginItem : MonoBehaviour
    {
        public int idSlot;
        public int idItem;
        public GameObject Clear, focus, imageClear, Item;
        public TextMeshProUGUI textDayNumber, textValue, textTodayTomorrow;
        public Image BackGlow, itemIcon;
        public Sprite nomalBackGlow, specialBackGlow;
        public bool isCheckClear;
        public Color nomalColor = new Color(100 / 255f, 240 / 255f, 1f, 90 / 255f),
        specialColor = new Color(252 / 255f, 208 / 255f, 1f, 166 / 255f);
    }
}