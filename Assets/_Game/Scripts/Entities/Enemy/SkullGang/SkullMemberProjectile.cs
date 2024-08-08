using _Base.Scripts.RPG.Effects;
using _Game.Features.Gameplay;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullMemberProjectile : MonoBehaviour
{
    [SerializeField] Rigidbody2D body;
    public float speed;
    [SerializeField] ParticleSystem particle;
    public bool isLaunched;
    public DecreaseHealthEffect dmgEffect;
    public Transform upper;


    public GridPicker gridPicker;
    public GridAttackHandler gridAttack;
    private void Start()
    {
        gridPicker = FindAnyObjectByType<GridPicker>();
        gridAttack = FindAnyObjectByType<GridAttackHandler>();
    }
    public void SetData(Vector2 startPos, Vector2 targetPos, float dmg)
    {
        transform.up = (targetPos - startPos);
        transform.position = startPos;
        dmgEffect.Amount = dmg;
    }

    private void FixedUpdate()
    {
        if (isLaunched)
        {
            body.velocity = speed * transform.up;
        }
    }

    public void Launch()
    {
        isLaunched = true;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out EffectTakerCollider taker))
        {
            Debug.Log(taker.Taker);
            if (taker.Taker is Ship ship)
            {
                isLaunched = false;
                OnImpact(ship);
            }
        }
    }
    bool isImpacted;
    void OnImpact(Ship ship)
    {
        if (isImpacted)
        {
            return;
        }
        isImpacted = true;
        isLaunched = false;
        
        ParticleSystem ps = Instantiate(particle, upper.transform.position, Quaternion.identity, ship.transform.GetChild(0));
        ps.gameObject.SetActive(true);
        DoDmg();
        Destroy(gameObject);
    }

    void DoDmg()
    {
        Cell cell = gridPicker.PickRandomCell();
        EnemyAttackData enemyAttackData = new EnemyAttackData();
        enemyAttackData.TargetCells = new List<Cell>() { cell };
        enemyAttackData.CenterCell = cell;

        dmgEffect.transform.position = cell.transform.position;
        enemyAttackData.Effects = new List<Effect> { dmgEffect };

        gridAttack.ProcessAttack(enemyAttackData);
    }
}
