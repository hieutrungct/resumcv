using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CharacterItem : MonoBehaviour
{
    public Image avaCard,attackType,avaBox, BackGlow,Glow;
    public SkeletonDataAsset hero;
    public TextMeshProUGUI strTxt, levelTxt;
    [SerializeField] GameObject[] stars;
    
    public void SetUp(Character character, UnityEngine.Events.UnityAction callbackClick = null)
    {
       
        //avaCard.sprite = Common.GetAvatar(character.Name);
        avaCard.sprite = AssetLoader.Instance.GetAvatarById(character.Name);
        hero = AssetLoader.Instance.GetAvaById(character.Name);
        // avaCard.SetNativeSize();
        if (avaBox != null)
        {
            avaBox.sprite = AssetLoader.Instance.RarrityBox[character.Rarity];
        }
        
        
        if (attackType != null)
        {
            switch (character.CharacterType)
            {
                case CharacterType.Melee:
                    attackType.sprite = AssetLoader.Instance.AttackSprite[0];
                    break;
                case CharacterType.Ranged:
                    attackType.sprite = AssetLoader.Instance.AttackSprite[1];
                    break;
            }
        }
       
        strTxt.text = character.Name.ToString();
        levelTxt.text = "" + character.Level;
        for(int i = 0; i < character.Star; i++)
        {
            stars[i].SetActive(true);
            if (i < character.Ascend)
            {
                stars[i].GetComponent<Image>().color = Color.red;
            }
        }
        
        if (callbackClick != null)
        {
            //btnClick.onClick.AddListener(() => {
            //    callbackClick();
            //});
        }
        //inDeck.SetActive(character.isInDeck);

    }
    
}
