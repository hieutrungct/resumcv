using Spine.Unity;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


namespace Rubik_Casual
{
    public class CharacterInfoPopup : MonoBehaviour
    {
        [SerializeField] private SkeletonGraphic avaCharacter;

        [SerializeField]
        private TextMeshProUGUI lvTxt, lvProcessTxt, damageTxt, defenseTxt, critTxt, healthTxt, moveSpeedTxt;

        public Button btn_Arrow_r, btn_Arrow_l;
        
        

        public Image role, avatar;
        
        
        private Character thisCharacter;

        public void Start()
        {
            
        }

        public void SetUp(Character character)
        {
            thisCharacter = character;
            avaCharacter.skeletonDataAsset = AssetLoader.instance.GetAvaById(character.Nameid);
            avaCharacter.initialSkinName = avaCharacter.skeletonDataAsset.GetSkeletonData(true).Skins.Items[1].Name;
            avaCharacter.startingAnimation = avaCharacter.skeletonDataAsset.GetSkeletonData(true).Animations.Items[3].Name;
            avaCharacter.Initialize(true);
            
            avatar.sprite = AssetLoader.Instance.GetAvatarById(character.Nameid);
            
            role.sprite = AssetLoader.Instance.AttackSprite[character.Role];
            lvTxt.text = character.Level.ToString();
            lvProcessTxt.text = character.Exp + "/" + character.Exp;
            damageTxt.text = character.AttackDamage.ToString();
            defenseTxt.text = character.Depense.ToString();
            critTxt.text = character.Critical.ToString();
            healthTxt.text = character.Health.ToString();
            moveSpeedTxt.text = character.MoveSpeed.ToString();
        }

        public void Next()
        {
            int temp = CharacterUIController.instance.CheckIndexOfCharacter(thisCharacter);
            thisCharacter = CharacterUIController.instance.GetCharacter(temp - 1);
            SetUp(thisCharacter);
            
        }
        public void Back()
        {
            int temp = CharacterUIController.instance.CheckIndexOfCharacter(thisCharacter);
            thisCharacter = CharacterUIController.instance.GetCharacter(temp + 1);
            SetUp(thisCharacter);
        }

        public void Close()
        {
            gameObject.SetActive(false);
            HUDController.instanse.UpdateTopPanel(Energe:false,Gold:false,Gem:false,Ticket: false);
        }
        

        public void ShowCharaterfoPopup(Character character)
        {
            gameObject.SetActive(true);
            SetUp(character);
        }
    }
}

