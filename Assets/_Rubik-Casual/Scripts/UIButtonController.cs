using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace RubikCasual.UIButtonController
{
    public class UIButtonController : MonoBehaviour
    {
        public List<Button> buttons;  // Danh sách các button
        public List<GameObject> gameObjects;  // Danh sách các game object tương ứng
        public static UIButtonController instance;

        void Start()
        {
            instance = this;
            // Đảm bảo số lượng button và game object giống nhau
            if (buttons.Count != gameObjects.Count)
            {
                Debug.LogError("Số lượng button và game object không khớp!");
                return;
            }

            // Thêm listener cho mỗi button
            for (int i = 0; i < buttons.Count; i++)
            {
                int index = i;  // Lưu lại chỉ số của button trong danh sách
                buttons[i].onClick.AddListener(() => OnButtonClick(index));
            }
        }

        public void OnButtonClick(int buttonIndex)
        {
            // Tắt tất cả các game object trước khi bật game object tương ứng với button được nhấn
            for (int i = 0; i < gameObjects.Count; i++)
            {
                gameObjects[i].SetActive(i == buttonIndex);  // Bật game object nếu chỉ số trùng khớp với buttonIndex
            }
        }
    }
}

