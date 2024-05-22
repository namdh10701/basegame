using _Base.Scripts.RPG.Effects;
using _Game.Scripts;
using _Game.Scripts.Battle;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellAttacker : EnemyAttackBehaviour, ICellAttacker
{
    [SerializeField] protected AttackPatternProfile AttackPatternProfile;
    protected GridPicker gridPicker;
    protected GridAttackHandler attackHandler;
    protected EnemyAttackData enemyAttackData;
    protected SpineAnimationEnemyHandler SpineAnimationEnemyHandler;
    private void Awake()
    {
        gridPicker = FindAnyObjectByType<GridPicker>();
        attackHandler = FindAnyObjectByType<GridAttackHandler>();
    }

    public void SelectCells()
    {
        enemyAttackData = new EnemyAttackData();
        enemyAttackData.TargetCells = gridPicker.PickCells(transform, AttackPatternProfile, out Cell centerCell);
        enemyAttackData.CenterCell = centerCell;
        enemyAttackData.Effect = new DecreaseHealthEffect(2);
        attackHandler.PlayTargetingFx(enemyAttackData.TargetCells);
    }

    public override IEnumerator AttackSequence()
    {
        SelectCells();
        yield return new WaitForSeconds(2);
        SpineAnimationEnemyHandler.PlayAnim(Anim.Attack, false, () =>
        {
            //cooldownBehaviour.StartCooldown();
        });
    }

    public override void DoAttack()
    {
    }

}

public interface ICellAttacker
{
    public void SelectCells();

}
