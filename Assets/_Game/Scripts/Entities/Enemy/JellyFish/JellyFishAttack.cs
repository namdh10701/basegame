using _Base.Scripts.RPG.Effects;
using _Game.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyFishAttack : MonoBehaviour
{
    public bool IsLeftHandAttack;
    public Transform leftShootPos;
    public Transform rightShootPos;
    public JellyFishProjectile projectilePrefab;
    public EnemyAttackData enemyAttackData;

    public AttackPatternProfile AttackPatternProfile;
    public AttackPatternProfile meeleAttackProfile;

    GridAttackHandler gridAttackHandler;
    GridPicker gridPicker;
    private void Start()
    {
        gridAttackHandler = FindAnyObjectByType<GridAttackHandler>();
        gridPicker = FindAnyObjectByType<GridPicker>();

    }
    public void DoLeftAttack()
    {
        IsLeftHandAttack = false;
        SelectCells();
        JellyFishProjectile projectile = Instantiate(projectilePrefab);
        projectile.SetData(enemyAttackData, leftShootPos.transform.position, 30);
        projectile.Launch();
    }

    public void DoRightAttack()
    {
        IsLeftHandAttack = true;
        SelectCells();
        JellyFishProjectile projectile = Instantiate(projectilePrefab);
        projectile.SetData(enemyAttackData, rightShootPos.transform.position, -30);
        projectile.Launch();
    }

    void SelectCells()
    {
        enemyAttackData = new EnemyAttackData();
        enemyAttackData.TargetCells = gridPicker.PickCells(transform, AttackPatternProfile, out Cell centerCell);
        enemyAttackData.CenterCell = centerCell;
        DecreaseHealthEffect decreaseHp = new GameObject("", typeof(DecreaseHealthEffect)).GetComponent<DecreaseHealthEffect>();
        decreaseHp.Amount = 3;// Take from boss stats;
        enemyAttackData.Effects = new List<Effect> { decreaseHp };
    }


    public void DoLeftMeleeAttack()
    {
        enemyAttackData = new EnemyAttackData();
        enemyAttackData.TargetCells = gridPicker.PickCells(leftShootPos, meeleAttackProfile, out Cell centerCell);
        enemyAttackData.CenterCell = centerCell;

        DecreaseHealthEffect decreaseHp = new GameObject("", typeof(DecreaseHealthEffect)).GetComponent<DecreaseHealthEffect>();
        decreaseHp.Amount = 3;// Take from boss stats;
        enemyAttackData.Effects = new List<Effect> { decreaseHp };

        gridAttackHandler.ProcessAttack(enemyAttackData);
    }

    public void DoRightMelleAttack()
    {
        enemyAttackData = new EnemyAttackData();
        enemyAttackData.TargetCells = gridPicker.PickCells(rightShootPos, meeleAttackProfile, out Cell centerCell);
        enemyAttackData.CenterCell = centerCell;
        DecreaseHealthEffect decreaseHp = new GameObject("", typeof(DecreaseHealthEffect)).GetComponent<DecreaseHealthEffect>();
        decreaseHp.Amount = 3;// Take from boss stats;

        enemyAttackData.Effects = new List<Effect> { decreaseHp };

        gridAttackHandler.ProcessAttack(enemyAttackData);
    }
}
