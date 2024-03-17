using MBT;
using UnityEngine;
using UnityEngine.Events;
public class Enemy : MonoBehaviour
{
    public EnemyData EnemyData;
    [SerializeField] Blackboard blackboard;
    Transform target;
    public bool IsPlayerInRange;
    public CooldownBrain Cooldown;
    public bool IsAbleToAttack => !Cooldown.IsInCooldown && IsPlayerInRange;
    private void Start()
    {
        Cooldown.SetCooldownTime(1 / EnemyData.AttackSpeed);

        target = GameObject.Find("Ship").transform;
        if (blackboard != null)
        {
            //blackboard.GetVariable<Vector3Variable>("targetPos").Value = target.transform.position;
            blackboard.GetVariable<TransformVariable>("target").Value = target;
        }
    }


    public void DoAttack()
    {
        Debug.Log("Attack");
    }


    private void Update()
    {
       /* if (blackboard != null)
            blackboard.GetVariable<Vector3Variable>("targetPos").Value = target.transform.position;
*/

    }
}