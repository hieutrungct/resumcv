using System.Collections;
using System.Collections.Generic;
using RubikCasual.Data;
using UnityEngine;

public class TestLoadData : MonoBehaviour
{
    public DataController dataController;
    void Awake()
    {
        dataController.LoadPlayerDataToJson();
    }
}
