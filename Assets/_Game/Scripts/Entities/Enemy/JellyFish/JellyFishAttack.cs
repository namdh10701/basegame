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
    public GridPicker gridPicker;
    public AttackPatternProfile AttackPatternProfile;
    public AttackPatternProfile meeleAttackProfile;

    public Transform leftMeleeHit;
    public Transform rightMeleeHit;
    private void Awake()
    {
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
        enemyAttackData.TargetCells = gridPicker.PickCells(leftMeleeHit, meeleAttackProfile, out Cell centerCell);
        enemyAttackData.CenterCell = centerCell;

        DecreaseHealthEffect decreaseHp = new GameObject("", typeof(DecreaseHealthEffect)).GetComponent<DecreaseHealthEffect>();
        decreaseHp.Amount = 3;// Take from boss stats;

        enemyAttackData.Effects = new List<Effect> { decreaseHp };
    }

    public void DoRightMelleAttack()
    {
        enemyAttackData = new EnemyAttackData();
        enemyAttackData.TargetCells = gridPicker.PickCells(leftMeleeHit, meeleAttackProfile, out Cell centerCell);
        enemyAttackData.CenterCell = centerCell;
        DecreaseHealthEffect decreaseHp = new GameObject("", typeof(DecreaseHealthEffect)).GetComponent<DecreaseHealthEffect>();
        decreaseHp.Amount = 3;// Take from boss stats;

        enemyAttackData.Effects = new List<Effect> { decreaseHp };
    }
}
