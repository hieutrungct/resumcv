using System.Collections;
using System.Collections.Generic;
using RubikCasual.Combat;
using RubikCasual.Combat.Character;
using Spine.Unity.Editor;
using UnityEngine;

public class GamePlay : MonoBehaviour
{
    public bool isHeroTurn = true, isEndTurn = true;
    public List<CharacterCombatUI> slotHeroClone, slotEnemyClone;
    public static GamePlay instance;
    void Awake()
    {
        instance = this;
    }
    void Start()
    {

        slotEnemyClone = CombatController.instance.slotEnemyClone;
        slotHeroClone = CombatController.instance.slotHeroClone;

    }
    void Update()
    {
        if (isEndTurn)
        {
            isEndTurn = false;
            GamePlayStart();
        }

    }
    Vector3 backTurnPos, posMove = new Vector3(2f, 0, 1f);
    float timeScale;
    public void GamePlayStart()
    {
        var turnIndex = Random.Range(0, 4);
        var attackIndex = Random.Range(0, 2);
        if (isHeroTurn)
        {
            turnHero(slotHeroClone[turnIndex], slotEnemyClone[attackIndex]);
        }
        else
        {
            turnHero(slotEnemyClone[attackIndex], slotHeroClone[turnIndex]);
        }
    }
    public void turnHero(CharacterCombatUI turn, CharacterCombatUI attack)
    {

        if (turn.characterInCombat.startingAnimation == "Idle")
        {
            turn.doneTurn = false;
            timeScale = turn.characterInCombat.timeScale;
            backTurnPos = turn.transform.position;


            StartCoroutine(PerformTurn(turn, attack));
        }
        else
        {
            return;
        }
    }

    IEnumerator PerformTurn(CharacterCombatUI turn, CharacterCombatUI attack)
    {
        yield return new WaitForSeconds(1f);

        if (isHeroTurn)
        {
            turn.transform.position = attack.transform.position - posMove;
        }
        else
        {
            turn.transform.position = attack.transform.position + posMove;
        }

        var nhanPham = Random.Range(0, 4);
        turn.characterInCombat.timeScale = 1;
        turn.characterInCombat.startingLoop = false;
        if (nhanPham == 1)
        {
            turn.characterInCombat.startingAnimation = "SkillCast";
            SpineEditorUtilities.ReinitializeComponent(turn.characterInCombat);
        }
        else
        {
            turn.characterInCombat.startingAnimation = "Attack";
            SpineEditorUtilities.ReinitializeComponent(turn.characterInCombat);
        }
        attack.characterInCombat.startingLoop = false;
        attack.characterInCombat.startingAnimation = "Attacked";
        SpineEditorUtilities.ReinitializeComponent(attack.characterInCombat);
        yield return new WaitForSeconds(0.7f);

        attack.characterInCombat.startingLoop = true;
        attack.characterInCombat.startingAnimation = "Idle";
        SpineEditorUtilities.ReinitializeComponent(attack.characterInCombat);

        yield return new WaitForSeconds(0.6f);

        turn.characterInCombat.startingAnimation = "Idle";
        turn.characterInCombat.startingLoop = true;
        SpineEditorUtilities.ReinitializeComponent(turn.characterInCombat);



        yield return new WaitForSeconds(1f);

        EndTurn(turn);
        turn.doneTurn = true;
    }

    void EndTurn(CharacterCombatUI turn)
    {
        turn.characterInCombat.timeScale = timeScale;
        turn.transform.position = backTurnPos;
        isHeroTurn = !isHeroTurn;
        isEndTurn = true;
    }
}
