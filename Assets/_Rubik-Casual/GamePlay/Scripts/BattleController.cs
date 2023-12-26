using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using DG.Tweening;
using Rubik.Axie;
using RubikCasual.Battle.Calculate;
using RubikCasual.Battle.Inventory;
using RubikCasual.Battle.UI;
using RubikCasual.Data;
using RubikCasual.Data.Player;
using RubikCasual.Data.Waifu;
using RubikCasual.Lobby;
using RubikCasual.Tool;
using Sirenix.OdinInspector;
using Spine;
using Spine.Unity;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RubikCasual.Battle
{
    public class BattleController : MonoBehaviour
    {
        public MapBattleController mapBattleController, dameSlotTxtController;
        public CharacterInBattle HeroInBattle, EnemyInBattle;
        public WaifuAssets waifuAssets;
        public EnemyAssets enemyAssets;
        public GamePlayUI gamePlayUI;
        public PlayerAssetsLoader playerAssetsLoader;
        public List<SlotInArea> HeroInArea;
        public List<GameObject> lsSlotGbEnemy, lsSlotGbHero;
        const string Anim_Character_Attack = "Attack",
        Anim_Character_Attacked = "Attacked",
        Anim_Character_Idle = "Idle",
        Anim_Character_Die = "Die",
        Anim_Character_Skill = "SkillCast";
        const string Layer_Attack = "Character_Attack",
        Layer_Attacked = "Character_Attacked",
        Layer_Character = "Character";
        bool isEndBattle = false, isRangeRemoved = false;
        public bool isUpdateDmgEnemy = false;
        int CountState = 1, numberSlot = 5;
        public static BattleController instance;
        public GameState gameState;
        [Button]
        void UpSpeed()
        {
            Time.timeScale = 2f;
        }
        [Button]
        void DownSpeed()
        {
            Time.timeScale = 1f;
        }
        void Start()
        {
            instance = this;
            gameState = GameState.WAIT_BATTLE;
            StartCoroutine(CreateBattlefield());
            // Cooldown();
        }
        void Update()
        {

            CheckHeroInBattle();
            StartCoroutine(BaseState(gameState));
        }

        public void StartGame()
        {
            if (gameState == GameState.WAIT_BATTLE)
            {
                gameState = GameState.START;
            }
        }
        public void ResetGame()
        {
            CountState = 1;
            CreateAreaHeroStart(HeroInArea);
            CreateAreaEnemyStart();

        }

        IEnumerator BaseState(GameState state)
        {
            UpdateHpBarEnemyForState(false);
            switch (state)
            {
                case GameState.START:
                    gameState = GameState.BATTLE;
                    break;
                case GameState.WAIT_BATTLE:

                    gamePlayUI.txtTime.text = CountState.ToString();
                    break;
                case GameState.BATTLE:
                    Atack();
                    break;
                case GameState.END_BATTLE:
                    UnityEngine.Debug.Log("End Battle");
                    EndBattleMoveCharacter();
                    yield return new WaitForSeconds(durations * 2);
                    UpdateHpBarEnemyForState(true);
                    break;
                case GameState.END:

                    break;
            }
        }
        public List<float> lsIdTeam = new List<float>();
        IEnumerator CreateBattlefield()
        {
            yield return new WaitForSeconds(0.25f);
            HeroInArea.Clear();
            lsIdTeam = DataController.instance.playerData.userData.CurentTeam;

            for (int i = 0; i < numberSlot; i++)
            {
                SlotInArea slotInArea = new SlotInArea();
                if (lsIdTeam.Count > i)
                {
                    slotInArea.idCharacter = lsIdTeam[i];
                }
                else
                {
                    slotInArea.idCharacter = 0;
                }
                slotInArea.slotCharacter = i + 1;
                HeroInArea.Add(slotInArea);
            }
            CreateAreaHeroStart(HeroInArea);
            CreateAreaEnemyStart();
        }
        void CheckHeroInBattle()
        {
            int count = 0;
            foreach (var item in lsSlotGbHero)
            {
                if (item == null)
                {
                    count++;
                }
            }
            if (count == 5)
            {
                gameState = GameState.END;
                // UnityEngine.Debug.Log("Hết Hero");
            }
        }
        SkeletonAnimation SpawnCharacter(Transform poscharacterInBattle, SkeletonAnimation WaifuCharacter)
        {
            SkeletonAnimation character = Instantiate(WaifuCharacter);
            character.transform.localScale = waifuAssets.transform.localScale * 2f / 3f;
            character.gameObject.transform.SetParent(poscharacterInBattle);
            character.gameObject.transform.position = poscharacterInBattle.position;
            character.loop = true;
            character.AnimationName = Anim_Character_Idle;

            character.gameObject.GetComponent<MeshRenderer>().sortingLayerName = Layer_Character;
            return character;
        }

        void CreateAreaHeroStart(List<SlotInArea> lsHeroInArea)
        {
            foreach (var item in lsSlotGbHero)
            {
                Destroy(item);
            }
            lsSlotGbHero.Clear();

            for (int i = 0; i < mapBattleController.lsPosHeroSlot.lsPosCharacterSlot.Count; i++)
            {
                int index = i;
                PositionCharacterSlot posSlot = mapBattleController.lsPosHeroSlot.lsPosCharacterSlot[i];

                if (lsHeroInArea[index].idCharacter != 0)
                {
                    CharacterInBattle heroInBattle = Instantiate(HeroInBattle, posSlot.gameObject.transform);
                    heroInBattle.gameObject.transform.position = posSlot.gameObject.transform.position;
                    heroInBattle.indexOfSlot = index;

                    SkeletonAnimation Hero = SpawnCharacter(heroInBattle.PosCharacter, waifuAssets.Get2D(lsHeroInArea[index].idCharacter.ToString()));

                    CharacterDragPosition CharacterHero = Hero.gameObject.AddComponent<CharacterDragPosition>();
                    CharacterHero.CharacterSke = waifuAssets.Get2D(lsHeroInArea[index].idCharacter.ToString());
                    CharacterHero.posCharacter = posSlot.gameObject.transform;
                    CharacterHero.oriIndex = index;
                    heroInBattle.skeletonCharacterAnimation = Hero;
                    heroInBattle.infoWaifuAsset = waifuAssets.infoWaifuAssets.lsInfoWaifuAssets.Find(f => f.ID == lsHeroInArea[index].idCharacter);

                    heroInBattle.cooldownAttackBar.value = 0;
                    heroInBattle.cooldownSkillBar.value = 0;
                    heroInBattle.healthBar.value = 1;
                    heroInBattle.Rage = 0;
                    heroInBattle.HpNow = heroInBattle.infoWaifuAsset.HP;

                    lsSlotGbHero.Add(heroInBattle.gameObject);
                }
                else
                {
                    lsSlotGbHero.Add(null);
                }

            }
        }

        [Button]
        void CreateAreaEnemyStart()
        {

            foreach (var item in lsSlotGbEnemy)
            {
                if (item != null && item.GetComponent<CharacterInBattle>() != null)
                {
                    item.GetComponent<CharacterInBattle>().healthBar.gameObject.transform.SetParent(item.GetComponent<CharacterInBattle>().cooldownSkillBar.gameObject.transform.parent);
                }
                Destroy(item);
            }
            lsSlotGbEnemy.Clear();
            int Count = 0;
            for (int i = 0; i < mapBattleController.lsPosEnemySlot.Count; i++)
            {
                int index = i;

                ListSlotPos lsPosSlot = mapBattleController.lsPosEnemySlot[index];

                for (int j = 0; j < lsPosSlot.lsPosCharacterSlot.Count; j++)
                {
                    int indexRand = Random.Range(0, enemyAssets.lsIdEnemy.Count);
                    PositionCharacterSlot posSlot = lsPosSlot.lsPosCharacterSlot[j];

                    if (enemyAssets.WaifuEnemyAssetDatas.FirstOrDefault(f => f.Index == enemyAssets.lsIdEnemy[indexRand]).Is_Boss != true)
                    {
                        // UnityEngine.Debug.Log(enemyAssets.WaifuEnemyAssetDatas.FirstOrDefault(f => f.Index == enemyAssets.lsIdEnemy[indexRand]).Is_Boss);
                        mapBattleController.lsPosEnemySlot[index].lsPosCharacterSlot[j].id = enemyAssets.lsIdEnemy[indexRand];


                        CharacterInBattle enemyInBattle = Instantiate(EnemyInBattle, posSlot.gameObject.transform);
                        enemyInBattle.indexOfSlot = Count;
                        enemyInBattle.isEnemy = true;
                        enemyInBattle.gameObject.transform.position = posSlot.gameObject.transform.position;
                        enemyInBattle.cooldownSkillBar.gameObject.SetActive(false);
                        if (i > 0)
                        {
                            enemyInBattle.cooldownAttackBar.gameObject.SetActive(false);
                        }
                        SkeletonAnimation Enemy = SpawnCharacter(enemyInBattle.PosCharacter, enemyAssets.Get2D(enemyAssets.lsIdEnemy[indexRand].ToString()));
                        // Enemy.GetComponent<MeshRenderer>().sortingLayerName = "Enemy";

                        enemyInBattle.gameObject.transform.localScale = new Vector3(-enemyInBattle.gameObject.transform.localScale.x, enemyInBattle.gameObject.transform.localScale.y, enemyInBattle.gameObject.transform.localScale.z);
                        enemyInBattle.skeletonCharacterAnimation = Enemy;
                        enemyInBattle.infoWaifuAsset = enemyAssets.infoEnemyAssets.lsInfoWaifuAssets.Find(f => f.Code == enemyAssets.lsIdEnemy[indexRand]);


                        enemyInBattle.cooldownAttackBar.value = 0;
                        enemyInBattle.cooldownSkillBar.value = 0;
                        enemyInBattle.healthBar.value = 1;
                        enemyInBattle.Rage = 0;
                        enemyInBattle.HpNow = enemyInBattle.infoWaifuAsset.HP;
                        lsSlotGbEnemy.Add(enemyInBattle.gameObject);
                    }
                    else
                    {
                        int rand = UnityEngine.Random.Range(0, 5);
                        if (rand == 0)
                        {
                            lsSlotGbEnemy.Add(null);
                        }
                        else
                        {
                            int randIdItem = UnityEngine.Random.Range(0, 5);
                            GameObject ItemClone = Instantiate(InventorryUIPanel.instance.itemInventory, posSlot.gameObject.transform);
                            ItemClone.transform.position = new Vector3(ItemClone.transform.position.x, ItemClone.transform.position.y + durations, ItemClone.transform.position.z);
                            SlotInventory Item = ItemClone.GetComponent<SlotInventory>();
                            Item.idItem = Item.lsIdItem[randIdItem];
                            Item.Icon.GetComponent<Image>().sprite = DataController.instance.itemData.InfoItems.FirstOrDefault(f => f.id == Item.lsIdItem[randIdItem]).imageItem;

                            Destroy(ItemClone.GetComponent<ItemDragPosition>());
                            lsSlotGbEnemy.Add(ItemClone);
                        }
                    }
                    Count++;
                }
            }
        }




        IEnumerator StartCooldown(Slider slider, float cooldownTime)
        {
            float timer = 0f;
            while (timer < cooldownTime)
            {
                timer += Time.deltaTime;
                slider.value = timer / cooldownTime;
                yield return null;
            }

            slider.value = 1f;
        }

        void Atack()
        {

            CheckEndBattle();
            if (!isEndBattle)
            {
                if (lsSlotGbEnemy[2] != null && lsSlotGbEnemy[2].GetComponent<CharacterInBattle>() != null && lsSlotGbEnemy[2].GetComponent<CharacterInBattle>().isBoss)
                {

                }
                else
                {
                    for (int i = 0; i < mapBattleController.lsPosHeroSlot.lsPosCharacterSlot.Count; i++)
                    {
                        if (lsSlotGbHero[i] != null && lsSlotGbEnemy[i] != null && lsSlotGbEnemy[i].GetComponent<CharacterInBattle>() != null)
                        {
                            CharacterInBattle Hero = lsSlotGbHero[i].GetComponent<CharacterInBattle>();
                            CharacterInBattle Enemy = lsSlotGbEnemy[i].GetComponent<CharacterInBattle>();
                            float randomCooldownTimeHero = (float)Random.Range(1, 10);
                            float randomCooldownTimeEnemy = (float)Random.Range(1, 10);
                            if (!Hero.isAttack && !Enemy.isAttack && !Hero.isUseSkill)
                            {
                                if (Hero.cooldownSkillBar.value == 1)
                                {
                                    CharacterUseSkill(Hero);
                                    Hero.cooldownSkillBar.value = 0;
                                }
                                else
                                {
                                    if (Hero.cooldownAttackBar.value == 1)
                                    {
                                        CharacterAtackAnimation(Enemy.gameObject, Hero.gameObject);
                                        // Debug.Log((i + 1) + " hero atack");
                                        Hero.cooldownAttackBar.value = 0;
                                    }
                                    if (Hero.cooldownAttackBar.value == 0)
                                    {
                                        StartCoroutine(StartCooldown(Hero.cooldownAttackBar, randomCooldownTimeHero));
                                    }
                                }

                                if (Enemy.cooldownSkillBar.value == 1)
                                {
                                    // CharacterUseSkill(Enemy);
                                    Enemy.cooldownSkillBar.value = 0;
                                }
                                else
                                {
                                    if (Enemy.cooldownAttackBar.value == 1)
                                    {
                                        CharacterAtackAnimation(Hero.gameObject, Enemy.gameObject);
                                        // Debug.Log((i + 1) + " enemy atack");
                                        Enemy.cooldownAttackBar.value = 0;
                                    }
                                    if (Enemy.cooldownAttackBar.value == 0)
                                    {
                                        StartCoroutine(StartCooldown(Enemy.cooldownAttackBar, randomCooldownTimeEnemy));
                                    }
                                }
                            }
                        }
                    }

                }


            }
            if (isEndBattle)
            {
                gameState = GameState.END_BATTLE;
            }

        }
        void UpdateHpBarEnemyForState(bool resultCheck)
        {
            if (resultCheck)
            {
                int count = 0;
                for (int i = 0; i < mapBattleController.lsPosEnemySlot.Count; i++)
                {
                    for (int j = 0; j < mapBattleController.lsPosEnemySlot[i].lsPosCharacterSlot.Count; j++)
                    {
                        if (lsSlotGbEnemy[count] != null && lsSlotGbEnemy[count].GetComponent<CharacterInBattle>() != null)
                        {
                            CharacterInBattle enemyInBattle = lsSlotGbEnemy[count].GetComponent<CharacterInBattle>();

                            enemyInBattle.healthBar.gameObject.transform.SetParent(dameSlotTxtController.lsPosEnemySlot[i].lsPosCharacterSlot[j].transform);

                            SkeletonAnimation Enemy = enemyInBattle.skeletonCharacterAnimation;
                            if (Enemy.AnimationName == Anim_Character_Idle)
                            {

                                Enemy.GetComponent<MeshRenderer>().sortingLayerName = "Map";
                                Enemy.GetComponent<MeshRenderer>().sortingOrder = 1;
                            }
                        }
                        count++;
                    }
                }
            }
            else
            {
                if (lsSlotGbEnemy.Count != 25)
                {
                    return;
                }
                int count = 0;
                for (int i = 0; i < mapBattleController.lsPosEnemySlot.Count; i++)
                {
                    for (int j = 0; j < mapBattleController.lsPosEnemySlot[i].lsPosCharacterSlot.Count; j++)
                    {
                        if (lsSlotGbEnemy[count] != null && lsSlotGbEnemy[count].GetComponent<CharacterInBattle>() != null)
                        {
                            CharacterInBattle enemyInBattle = lsSlotGbEnemy[count].GetComponent<CharacterInBattle>();
                            if (count < 5 && enemyInBattle.HpNow == 0)
                            {
                                enemyInBattle.healthBar.gameObject.transform.SetParent(enemyInBattle.cooldownAttackBar.gameObject.transform.parent);
                            }
                            if (gameState == GameState.END_BATTLE)
                            {
                                if (enemyInBattle.healthBar.gameObject.transform.parent == dameSlotTxtController.lsPosEnemySlot[i].lsPosCharacterSlot[j].transform)
                                {
                                    enemyInBattle.healthBar.gameObject.transform.SetParent(enemyInBattle.cooldownAttackBar.gameObject.transform.parent);
                                }
                            }
                            SkeletonAnimation Enemy = enemyInBattle.skeletonCharacterAnimation;
                            if (Enemy.AnimationName == Anim_Character_Idle)
                            {

                                Enemy.GetComponent<MeshRenderer>().sortingLayerName = "Map";
                                Enemy.GetComponent<MeshRenderer>().sortingOrder = 1;
                            }
                        }
                        count++;

                    }
                }
            }

        }
        void CheckEndBattle()
        {

            int Count = 0;
            for (int i = 0; i < mapBattleController.lsPosHeroSlot.lsPosCharacterSlot.Count; i++)
            {
                if (lsSlotGbHero[i] == null || lsSlotGbEnemy[i] == null || lsSlotGbEnemy[i].GetComponent<CharacterInBattle>() == null)
                {
                    Count++;
                }
            }
            if (mapBattleController.lsPosHeroSlot.lsPosCharacterSlot.Count == Count)
            {

                isEndBattle = true;
            }
        }
        float durations = 0.5f;


        void CharacterUseSkill(CharacterInBattle CharacterAttack)
        {
            CharacterAttack.skeletonCharacterAnimation.AnimationName = Anim_Character_Skill;
            CharacterAttack.skeletonCharacterAnimation.GetComponent<MeshRenderer>().sortingLayerName = Layer_Attack;
            CharacterAttack.skeletonCharacterAnimation.AnimationState.SetAnimation(0, Anim_Character_Skill, false);
            CharacterAttack.isUseSkill = true;
            CharacterAttack.skeletonCharacterAnimation.AnimationState.Complete += delegate
            {
                CharacterAttack.isUseSkill = false;
                CharacterAttack.skeletonCharacterAnimation.AnimationName = Anim_Character_Idle;
            };

            int count = 0;
            for (int i = 0; i < mapBattleController.lsPosEnemySlot.Count; i++)
            {
                for (int j = 0; j < mapBattleController.lsPosEnemySlot[0].lsPosCharacterSlot.Count; j++)
                {
                    if ((count - CharacterAttack.indexOfSlot) % 5 == 0)
                    {
                        // UnityEngine.Debug.Log(count);
                        if (lsSlotGbEnemy[count] != null && lsSlotGbEnemy[count].GetComponent<CharacterInBattle>() != null)
                        {

                            SkeletonAnimation AnimEnemy = lsSlotGbEnemy[count].GetComponent<CharacterInBattle>().skeletonCharacterAnimation;
                            AnimEnemy.GetComponent<MeshRenderer>().sortingLayerName = Layer_Attacked;
                            AnimEnemy.AnimationName = Anim_Character_Attacked;
                            AnimEnemy.AnimationState.SetAnimation(0, Anim_Character_Attacked, false);
                            AnimEnemy.AnimationState.Complete += delegate
                            {
                                // AnimEnemy.GetComponent<MeshRenderer>().sortingLayerName = Layer_Character;
                                AnimEnemy.AnimationName = Anim_Character_Idle;
                            };
                            CharacterInBattle CharacterInBattleAttacked = lsSlotGbEnemy[count].GetComponent<CharacterInBattle>();

                            float OldHp = CharacterInBattleAttacked.HpNow;
                            Calculator.CalculateHealth(CharacterAttack, CharacterInBattleAttacked);

                            GameObject txtDame = Instantiate(UIGamePlay.instance.TxtDame, dameSlotTxtController.lsPosEnemySlot[i].lsPosCharacterSlot[j].transform);

                            txtDame.GetComponent<TextMeshProUGUI>().text = "-" + ((int)(OldHp - lsSlotGbEnemy[count].GetComponent<CharacterInBattle>().HpNow)).ToString();
                            txtDame.transform.DOMoveY(txtDame.transform.position.y + durations, durations);
                            txtDame.GetComponent<TextMeshProUGUI>().color = Color.red;
                            StartCoroutine(DelayUseSkill(txtDame, durations));
                            CheckHpEnemy(lsSlotGbEnemy[count]);
                        }
                    }

                    count++;
                }
            }

        }
        IEnumerator DelayUseSkill(GameObject txtDame, float delay)
        {
            yield return new WaitForSeconds(delay);
            Destroy(txtDame);
        }

        void CharacterAtackAnimation(GameObject CharacterAttacked, GameObject CharacterAttack)
        {

            if (CharacterAttacked == null)
            {
                CharacterAttack.GetComponent<CharacterInBattle>().isAttack = false;
                return;
            }
            CharacterAttack.GetComponent<CharacterInBattle>().isAttack = true;
            float valueSliderBarCharacterAttack = CharacterAttack.GetComponent<CharacterInBattle>().cooldownSkillBar.value;
            CharacterAttack.GetComponent<CharacterInBattle>().cooldownSkillBar.value = valueSliderBarCharacterAttack + 0.05f;

            float valueSliderBarCharacterAttacked = CharacterAttacked.GetComponent<CharacterInBattle>().cooldownSkillBar.value;
            CharacterAttacked.GetComponent<CharacterInBattle>().cooldownSkillBar.value = valueSliderBarCharacterAttacked + 0.1f;

            StartCoroutine(MoveBackDelay(CharacterAttack, CharacterAttacked, durations));
        }
        IEnumerator MoveBackDelay(GameObject CharacterAttack, GameObject CharacterAttacked, float delay)
        {

            SkeletonAnimation CharacterAttackAnim = CharacterAttack.GetComponent<CharacterInBattle>().skeletonCharacterAnimation;
            SkeletonAnimation CharacterAttackedAnim = CharacterAttacked.GetComponent<CharacterInBattle>().skeletonCharacterAnimation;
            CharacterInBattle CharacterInBattleAttack = CharacterAttack.GetComponent<CharacterInBattle>();
            CharacterInBattle CharacterInBattleAttacked = CharacterAttacked.GetComponent<CharacterInBattle>();

            // CharacterAttackAnim.GetComponent<MeshRenderer>().sortingLayerName = Layer_Attack;
            // CharacterAttackedAnim.GetComponent<MeshRenderer>().sortingLayerName = Layer_Attacked;

            CharacterAttackAnim.AnimationName = Anim_Character_Attack;

            CharacterAttackAnim.AnimationState.SetAnimation(0, Anim_Character_Attack, false);
            CharacterAttackAnim.AnimationState.Complete += delegate
            {
                // CharacterAttackAnim.GetComponent<MeshRenderer>().sortingLayerName = Layer_Character;
                CharacterAttackAnim.AnimationName = Anim_Character_Idle;
            };
            yield return new WaitForSeconds(delay / 2);
            CharacterAttackedAnim.AnimationName = Anim_Character_Attacked;
            CharacterAttackedAnim.AnimationState.SetAnimation(0, Anim_Character_Attacked, false);
            CharacterAttackedAnim.AnimationState.Complete += delegate
            {
                // CharacterAttackedAnim.GetComponent<MeshRenderer>().sortingLayerName = Layer_Character;
                CharacterAttackedAnim.AnimationName = Anim_Character_Idle;
            };
            float OldHp = CharacterInBattleAttacked.HpNow;
            Calculator.CalculateHealth(CharacterInBattleAttack, CharacterInBattleAttacked);
            Transform TransTxt;
            if (CharacterInBattleAttacked.isEnemy)
            {
                TransTxt = dameSlotTxtController.lsPosEnemySlot[0].lsPosCharacterSlot[CharacterInBattleAttacked.indexOfSlot].transform;
            }
            else
            {
                TransTxt = dameSlotTxtController.lsPosHeroSlot.lsPosCharacterSlot[CharacterInBattleAttacked.indexOfSlot].transform;
            }
            GameObject txtDame = Instantiate(UIGamePlay.instance.TxtDame, TransTxt);
            txtDame.GetComponent<TextMeshProUGUI>().text = "-" + ((int)(OldHp - CharacterInBattleAttacked.HpNow)).ToString();
            txtDame.GetComponent<TextMeshProUGUI>().color = Color.red;
            txtDame.transform.DOMoveY(txtDame.transform.position.y + delay, delay);

            yield return new WaitForSeconds(delay / 2);


            if (CharacterInBattleAttacked.HpNow == 0)
            {

                CharacterAttackedAnim.AnimationName = Anim_Character_Die;
                CharacterAttackedAnim.AnimationState.SetAnimation(0, Anim_Character_Die, false);
                CharacterAttackedAnim.AnimationState.Complete += delegate
                {
                    CharacterAttack.GetComponent<CharacterInBattle>().isAttack = true;
                    Destroy(CharacterAttacked);
                };

            }
            else
            {
                if (CharacterAttack != null)
                {
                    CharacterAttack.GetComponent<CharacterInBattle>().isAttack = false;
                }

            }

            yield return new WaitForSeconds(delay / 2);
            Destroy(txtDame);

        }
        void CheckHpEnemy(GameObject gbEnemy)
        {
            CharacterInBattle enemy = gbEnemy.GetComponent<CharacterInBattle>();
            SkeletonAnimation enemyAnim = enemy.skeletonCharacterAnimation;
            if (enemy.HpNow == 0)
            {

                enemyAnim.AnimationName = Anim_Character_Die;
                enemyAnim.AnimationState.SetAnimation(0, Anim_Character_Die, false);
                enemyAnim.AnimationState.Complete += delegate
                {
                    // UnityEngine.Debug.Log(enemyAnim.skeletonDataAsset.name);
                    Destroy(gbEnemy);
                };
            }
        }

        void EndBattleMoveCharacter()
        {

            if (!isRangeRemoved)
            {

                for (int i = 0; i < mapBattleController.lsPosHeroSlot.lsPosCharacterSlot.Count; i++)
                {
                    int index = i;
                    if (lsSlotGbEnemy[i] != null)
                    {
                        lsSlotGbEnemy[i].transform.SetParent(mapBattleController.lsPosHeroSlot.lsPosCharacterSlot[i].transform);
                        lsSlotGbEnemy[i].transform.DOMoveX(mapBattleController.lsPosHeroSlot.lsPosCharacterSlot[i].transform.position.x, durations * 2);

                        StartCoroutine(MoveEnemy(lsSlotGbEnemy[i], i));
                    }
                }
                lsSlotGbEnemy.RemoveRange(0, 5); // Xóa 5 phần tử đầu tiên một lần duy nhất
                isRangeRemoved = true; // Đánh dấu rằng đã xóa
            }
            else
            {
                int Count = 0;
                for (int i = 0; i < mapBattleController.lsPosEnemySlot.Count; i++)
                {
                    if (lsSlotGbHero[i] != null)
                    {
                        lsSlotGbHero[i].GetComponent<CharacterInBattle>().isAttack = false;
                        // lsSlotGbHero[i].GetComponent<CharacterInBattle>().skeletonCharacterAnimation.AnimationName = Anim_Character_Idle;
                    }
                    for (int j = 0; j < mapBattleController.lsPosEnemySlot[i].lsPosCharacterSlot.Count; j++)
                    {
                        if (i != (lsSlotGbEnemy.Count / 5))
                        {
                            // UnityEngine.Debug.Log("Count = " + Count);
                            if (lsSlotGbEnemy[Count] != null)
                            {
                                lsSlotGbEnemy[Count].transform.SetParent(mapBattleController.lsPosEnemySlot[i].lsPosCharacterSlot[j].transform);
                                Tween TMoveEnemy = lsSlotGbEnemy[Count].transform.DOMoveX(mapBattleController.lsPosEnemySlot[i].lsPosCharacterSlot[j].transform.position.x, durations * 2);

                                if (i == 0 && lsSlotGbEnemy[Count].GetComponent<CharacterInBattle>() != null)
                                {

                                    CharacterInBattle EnemyClone = lsSlotGbEnemy[Count].GetComponent<CharacterInBattle>();
                                    EnemyClone.isCompleteMove = true;
                                    TMoveEnemy.OnComplete(() =>
                                    {
                                        EnemyClone.isCompleteMove = false;
                                    });
                                    EnemyClone.indexOfSlot = Count;
                                    EnemyClone.cooldownAttackBar.gameObject.SetActive(true);
                                    // EnemyClone.cooldownSkillBar.gameObject.SetActive(true);

                                }
                            }

                            Count++;
                        }
                    }
                }
                StartCoroutine(WaitBattleDelay());
                CountState++;
                gameState = GameState.WAIT_BATTLE;
            }
        }
        IEnumerator MoveEnemy(GameObject gb, int i)
        {
            yield return new WaitForSeconds(durations * 2);
            if (gb.GetComponent<SlotInventory>() != null && lsSlotGbHero[i] != null)
            {
                int idItem = gb.GetComponent<SlotInventory>().idItem;
                Calculator.CheckItemCalculate(idItem, lsSlotGbHero[i].GetComponent<CharacterInBattle>());
                StartCoroutine(MovePopup.ShowTxtDame(gb, UIGamePlay.instance.TxtDame, mapBattleController.lsPosHeroSlot.lsPosCharacterSlot[i].transform.position, DataController.instance.itemData.InfoItems.FirstOrDefault(f => f.id == idItem).Dame, DataController.instance.itemData.InfoItems.FirstOrDefault(f => f.id == idItem).type.ToString()));
                if (lsSlotGbHero[i].GetComponent<CharacterInBattle>().HpNow <= 0)
                {
                    SkeletonAnimation heroAnim = lsSlotGbHero[i].GetComponent<CharacterInBattle>().skeletonCharacterAnimation;
                    heroAnim.AnimationName = Anim_Character_Die;
                    heroAnim.AnimationState.SetAnimation(0, Anim_Character_Die, false);
                    heroAnim.AnimationState.Complete += delegate
                    {
                        Destroy(lsSlotGbHero[i]);
                    };
                }
            }
            else
            {
                Destroy(gb);
            }

        }
        public float OldATKUpdate = 5f;
        void SpawnEnemyEndBattle()
        {
            if (lsSlotGbEnemy.Count < 25)
            {
                ListSlotPos lsPosSlot = mapBattleController.lsPosEnemySlot[4];
                if ((CountState + mapBattleController.lsPosEnemySlot.Count) % 10 != 0)
                {
                    int Count = 20;
                    for (int i = 0; i < 5; i++)
                    {
                        int index = i;
                        int indexRand = Random.Range(0, enemyAssets.lsIdEnemy.Count);

                        PositionCharacterSlot posSlot = lsPosSlot.lsPosCharacterSlot[index];

                        if (enemyAssets.WaifuEnemyAssetDatas.FirstOrDefault(f => f.Index == enemyAssets.lsIdEnemy[indexRand]).Is_Boss != true)
                        {
                            lsPosSlot.lsPosCharacterSlot[index].id = enemyAssets.lsIdEnemy[indexRand];


                            CharacterInBattle enemyInBattle = Instantiate(EnemyInBattle, posSlot.gameObject.transform);
                            enemyInBattle.indexOfSlot = Count;
                            enemyInBattle.isEnemy = true;
                            enemyInBattle.gameObject.transform.position = posSlot.gameObject.transform.position;

                            enemyInBattle.cooldownAttackBar.gameObject.SetActive(false);
                            enemyInBattle.cooldownSkillBar.gameObject.SetActive(false);

                            SkeletonAnimation Enemy = SpawnCharacter(enemyInBattle.PosCharacter, enemyAssets.Get2D(enemyAssets.lsIdEnemy[indexRand].ToString()));
                            // Enemy.GetComponent<MeshRenderer>().sortingLayerName = "Enemy";

                            enemyInBattle.gameObject.transform.localScale = new Vector3(-enemyInBattle.gameObject.transform.localScale.x, enemyInBattle.gameObject.transform.localScale.y, enemyInBattle.gameObject.transform.localScale.z);
                            enemyInBattle.skeletonCharacterAnimation = Enemy;
                            enemyInBattle.infoWaifuAsset = enemyAssets.infoEnemyAssets.lsInfoWaifuAssets.Find(f => f.Code == enemyAssets.lsIdEnemy[indexRand]);


                            enemyInBattle.cooldownAttackBar.value = 0;
                            enemyInBattle.cooldownSkillBar.value = 0;
                            enemyInBattle.healthBar.value = 1;
                            enemyInBattle.Rage = 0;
                            enemyInBattle.HpNow = enemyInBattle.infoWaifuAsset.HP;
                            enemyInBattle.infoWaifuAsset.ATK = OldATKUpdate;
                            // for (int j = 0; j < 5; j++)
                            // {
                            //     enemyInBattle.infoWaifuAsset.ATK = lsSlotGbEnemy[19].GetComponent<CharacterInBattle>().infoWaifuAsset.ATK;
                            // }

                            if (CountState % 2 == 0)
                            {

                                enemyInBattle.infoWaifuAsset.ATK = OldATKUpdate * 1.1f;

                                // UnityEngine.Debug.Log(enemyInBattle.infoWaifuAsset.ATK);
                            }
                            lsSlotGbEnemy.Add(enemyInBattle.gameObject);
                        }
                        else
                        {
                            int rand = UnityEngine.Random.Range(0, 5);
                            if (rand == 0)
                            {
                                lsSlotGbEnemy.Add(null);
                            }
                            else
                            {
                                int randIdItem = UnityEngine.Random.Range(0, 5);
                                GameObject ItemClone = Instantiate(InventorryUIPanel.instance.itemInventory, posSlot.gameObject.transform);
                                ItemClone.transform.position = new Vector3(ItemClone.transform.position.x, ItemClone.transform.position.y + durations, ItemClone.transform.position.z);
                                SlotInventory Item = ItemClone.GetComponent<SlotInventory>();
                                Item.idItem = Item.lsIdItem[randIdItem];
                                Item.Icon.GetComponent<Image>().sprite = DataController.instance.itemData.InfoItems.FirstOrDefault(f => f.id == Item.lsIdItem[randIdItem]).imageItem;

                                Destroy(ItemClone.GetComponent<ItemDragPosition>());
                                lsSlotGbEnemy.Add(ItemClone);
                            }

                        }
                        Count++;
                    }
                    if (CountState % 2 == 0)
                    {
                        OldATKUpdate = OldATKUpdate * 1.1f;
                    }
                }
                else
                {
                    int Count = 20;
                    for (int i = 0; i < 5; i++)
                    {
                        if (i == 2)
                        {
                            int indexRand = Random.Range(0, 12);
                            PositionCharacterSlot posSlot = lsPosSlot.lsPosCharacterSlot[2];
                            CharacterInBattle enemyBossInBattle = Instantiate(EnemyInBattle, posSlot.gameObject.transform);
                            enemyBossInBattle.indexOfSlot = Count;
                            enemyBossInBattle.isEnemy = true;
                            enemyBossInBattle.isBoss = true;
                            enemyBossInBattle.gameObject.transform.position = posSlot.gameObject.transform.position;

                            enemyBossInBattle.cooldownAttackBar.gameObject.SetActive(false);
                            enemyBossInBattle.cooldownSkillBar.gameObject.SetActive(false);

                            SkeletonAnimation Enemy = SpawnCharacter(enemyBossInBattle.PosCharacter, enemyAssets.Get2D(enemyAssets.lsIdEnemy[indexRand].ToString()));
                            // Enemy.GetComponent<MeshRenderer>().sortingLayerName = "Enemy";

                            enemyBossInBattle.gameObject.transform.localScale = new Vector3(-enemyBossInBattle.gameObject.transform.localScale.x, enemyBossInBattle.gameObject.transform.localScale.y, enemyBossInBattle.gameObject.transform.localScale.z);
                            enemyBossInBattle.skeletonCharacterAnimation = Enemy;
                            enemyBossInBattle.infoWaifuAsset = enemyAssets.infoEnemyAssets.lsInfoWaifuAssets.Find(f => f.Code == enemyAssets.lsIdEnemy[indexRand]);


                            enemyBossInBattle.cooldownAttackBar.value = 0;
                            enemyBossInBattle.cooldownSkillBar.value = 0;
                            enemyBossInBattle.healthBar.value = 1;
                            enemyBossInBattle.Rage = 0;
                            enemyBossInBattle.HpNow = enemyBossInBattle.infoWaifuAsset.HP;
                            enemyBossInBattle.infoWaifuAsset.ATK = OldATKUpdate;

                            if ((CountState + mapBattleController.lsPosEnemySlot.Count) % 10 == 0)
                            {

                                enemyBossInBattle.infoWaifuAsset.ATK = OldATKUpdate * 1.1f;

                            }
                            lsSlotGbEnemy.Add(enemyBossInBattle.gameObject);
                        }
                        else
                        {
                            lsSlotGbEnemy.Add(null);
                        }
                    }
                }
            }
        }
        IEnumerator WaitBattleDelay()
        {

            yield return new WaitForSeconds(durations);
            SpawnEnemyEndBattle();
            isEndBattle = false;
            isRangeRemoved = false;
        }

    }
    [System.Serializable]
    public class SlotInArea
    {
        public float slotCharacter, idCharacter;
    }
}