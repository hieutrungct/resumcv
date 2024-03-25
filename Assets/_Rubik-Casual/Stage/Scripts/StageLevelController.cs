using System.Collections;
using System.Collections.Generic;
using RubikCasual.Data;
using RubikCasual.StageLevel.UI;
using UnityEngine;

namespace RubikCasual.StageLevel
{
    public class StageLevelController : MonoBehaviour
    {
        public LevelUI levelUI;
        public GameObject gbNull;
        public List<GameObject> lsLevelUI, lsGbNull;
        public Transform TransTop, TransCentrel, TransBottom;
        void Awake()
        {

        }
        public void CreateLevel(int numberLevel)
        {
            if (lsLevelUI != null)
            {
                foreach (var item in lsGbNull)
                {
                    Destroy(item);
                }
                lsGbNull.Clear();
                foreach (var item in lsLevelUI)
                {
                    Destroy(item);
                }
                lsLevelUI.Clear();
            }

            for (int i = 0; i < numberLevel; i++)
            {
                int rand = UnityEngine.Random.Range(0, 3);
                switch (rand)
                {
                    case 0:
                        lsLevelUI.Add(Instantiate(levelUI, TransTop).gameObject);
                        lsGbNull.Add(Instantiate(gbNull, TransCentrel));
                        lsGbNull.Add(Instantiate(gbNull, TransBottom));
                        break;
                    case 1:
                        lsGbNull.Add(Instantiate(gbNull, TransTop));
                        lsLevelUI.Add(Instantiate(levelUI, TransCentrel).gameObject);
                        lsGbNull.Add(Instantiate(gbNull, TransBottom));
                        break;
                    case 2:
                        lsGbNull.Add(Instantiate(gbNull, TransTop));
                        lsGbNull.Add(Instantiate(gbNull, TransCentrel));
                        lsLevelUI.Add(Instantiate(levelUI, TransBottom).gameObject);
                        break;
                }
            }
        }
        void CreateLevelTest(int countWay, int way, int slot, int rowMax)
        {
            if (way < countWay)
            {
                switch (slot)
                {
                    case 0:
                        int randSlotNext = UnityEngine.Random.Range(0, 2);
                        lsLevelUI.Add(Instantiate(levelUI, TransTop).gameObject);
                        lsLevelUI.Add(Instantiate(gbNull, TransCentrel));
                        lsLevelUI.Add(Instantiate(gbNull, TransBottom));

                        CreateLevelTest(countWay, way + 1, randSlotNext, rowMax);
                        break;
                }
            }
        }
    }
}
