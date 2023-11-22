using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RubikCasual.Lobby
{
    public class CharacterSelectController : MonoBehaviour
    {
        public List<SlotCharacterUI> lsSlotCharacterUI;
        public void loadScene(string sceneName)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
        }
        public void BackPopupCharacter()
        {
            this.gameObject.SetActive(false);
        }
    }
}