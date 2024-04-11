using System.Collections;
using System.Collections.Generic;
using Sirenix.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace RubikCasual.UIButtonController
{
    public class UIButtonController : MonoBehaviour
    {
        public List<Button> buttons;  // Danh sách các button
        public List<TextMeshProUGUI> lsText;
        public List<GameObject> gameObjects1,gameObjects2;  // Danh sách các game object tương ứng
        public static UIButtonController instance;

        void Start()
        {
            instance = this;
            // Đảm bảo số lượng button và game object giống nhau
            if (buttons.Count != gameObjects1.Count)
            {
                Debug.LogError("Số lượng button và game object không khớp!");
                return;
            }
            // if (buttons.Count != gameObjects2.Count)
            // {
            //     Debug.LogError("Số lượng button và game object không khớp!");
            //     return;
            // }

            // Thêm listener cho mỗi button
            for (int i = 0; i < buttons.Count; i++)
            {
                int index = i;  // Lưu lại chỉ số của button trong danh sách
                buttons[i].onClick.AddListener(() => 
                {
                    if(gameObjects2.Count != 0 && lsText.Count != 0)
                    {
                        OnButtonClick(index);
                        OnButtonClick2(index);
                        OnButtonClick_text(index);
                    }
                    else if(gameObjects2.Count != 0)
                    {
                        OnButtonClick(index);
                        OnButtonClick2(index);
                    }
                    else if(lsText.Count != 0)
                    {
                        OnButtonClick_text(index);
                    }
                    else
                    {
                        OnButtonClick(index);
                    }
                    
                });
            }
        }

        public void OnButtonClick(int buttonIndex)
        {
            // Tắt tất cả các game object trước khi bật game object tương ứng với button được nhấn
            for (int i = 0; i < gameObjects1.Count; i++)
            {
                gameObjects1[i].SetActive(i == buttonIndex);  // Bật game object nếu chỉ số trùng khớp với buttonIndex
            }
        }
        public void OnButtonClick2(int buttonIndex)
        {
            for (int i = 0; i < gameObjects2.Count; i++)
            {
                gameObjects2[i].SetActive(i == buttonIndex);  // Bật game object nếu chỉ số trùng khớp với buttonIndex
            }
        }
        public void OnButtonClick_text(int buttonIndex)
        {
            // Debug.Log("Đã vào hàm đổi màu!");
            for (int i = 0; i < lsText.Count; i++)
            {
                Config.SetTextColorWithHex(lsText[i], Config.color_Blue);
                
            }
            Config.SetTextColorWithHex(lsText[buttonIndex], Config.color_White);
            
        }
    }
}

