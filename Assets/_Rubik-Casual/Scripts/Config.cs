using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NameKey
{
    public static string Data = "Data";
    public static string  USER_DATA_KEY = "USER_DATA_KEY";
    public static string  USER_OWN_WAIFU_KEY = "USER_OWN_WAIFU_KEY";
    public static string  USER_ITEM_KEY = "USER_ITEM_KEY";
}
public class NameScene
{
    public static string HOME_SCENE = "MainScene";
    public static string GAMEPLAY_SCENE = "GamePlay";
}
public class Config : MonoBehaviour
{
    public static string color_Rare_R = "#83D801";
    public static string color_BackGlow_Rare_R = "#92F358";
    public static string color_Rare_SR = "#2B74CE";
    public static string color_BackGlow_Rare_SR = "#7AF7FF";
    public static string color_Rare_SSR ="#FF4F00";
    public static string color_BackGlow_Rare_SSR =  "#E2772C";
    public static string color_Rare_UR = "#BD44C6";
    public static string color_BackGlow_Rare_UR = "#ED74FF";
    public static string color_White = "#FFFFFF";
    public static string color_Blue = "#4AAAF8";

    // Animations Hero
    public static string Attack = "Attack";
    public static string Attacked = "Attacked";
    public static string Die = "Die";
    public static string Idle = "Idle";
    public static string SkillCast = "SkillCast";
    public static string Hit = "Hit";

    
    public static void SetTextColorWithHex(TextMeshProUGUI  text, string hexColor)
    {
        if (ColorUtility.TryParseHtmlString(hexColor, out Color color))
        {
            text.color = color;
        }
        else
        {
            Debug.LogError("Invalid Hex Color: " + hexColor);
        }
    }
    public static void SetColorFromHex(Image image, string hexColor)
    {
        if (ColorUtility.TryParseHtmlString(hexColor, out Color color))
        {
            image.color = color;
        }
        else
        {
            Debug.LogError("Invalid Hex Color: " + hexColor);
        }
    }


}
public enum SummonKey
{
    idOnSlot_0 = 15, //có màu chủ đạo đang là #FFD700
    idOnSlot_1 = 37, //có màu chủ đạo đang là #FF0000
    idOnSlot_2 = 44, //có màu chủ đạo đang là #BD44C6
    idOnSlot_3 = 46, //có màu chủ đạo đang là #FF4F00
    idOnSlot_4 = 118, //có màu chủ đạo đang là #1E90FF
    idOnSlot_5 = 68, //có màu chủ đạo đang là #696969
    idOnSlot_6 = 77 //có màu chủ đạo đang là #00FF00
}

