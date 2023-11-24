using System.Collections;
using System.Collections.Generic;
using RubikCasual.Tool;
using UnityEngine;

namespace RubikCasual.Adventure
{
    public class AdventurePanel : MonoBehaviour
    {

        public GameObject gbTaget;
        public List<DotAdventureUI> lsDotAdventureUI;
        public void ClosePopup()
        {
            MovePopup.TransPopupHorizontal(this.gameObject, gbTaget);
        }
        public void OpenPopup()
        {
            MovePopup.TransPopupHorizontal(gbTaget, this.gameObject);
        }
    }
}
