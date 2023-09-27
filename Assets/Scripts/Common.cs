using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Common : MonoBehaviour
{
    
    public static Sprite GetAvatar(string id)
    {
        Debug.Log("ảnh" + id);
        return Resources.Load<Sprite>("Character/" + id);
        
        
    }

    public void loadAll()
    {
        
    }
}
