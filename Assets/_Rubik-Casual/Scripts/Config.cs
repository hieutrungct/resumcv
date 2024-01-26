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
    public static string color_Rare_R = "#2B74CE";
    public static string color_BackGlow_Rare_R = "#7AF7FF";
    public static string color_Rare_SR = "#53D804";
    public static string color_BackGlow_Rare_SR = "#92F358";
    public static string color_Rare_SSR ="#FF4F00";
    public static string color_BackGlow_Rare_SSR =  "#E2772C";
    public static string color_Rare_UR = "#BD44C6";
    public static string color_BackGlow_Rare_UR = "#ED74FF";
    public static string color_White = "#FFFFFF";
    public static string color_Blue = "#4AAAF8";

    
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

