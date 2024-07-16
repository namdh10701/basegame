using BehaviorDesigner.Runtime.Tasks.Unity.UnityGameObject;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InflictedDamageDisplay : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI damageText;

    public void Init(float dmg, bool isCrit, Vector3 position)
    {
        damageText.text = Mathf.RoundToInt(dmg).ToString();
        if (isCrit)
        {
            damageText.color = Color.red;
        }
        else
        {
            damageText.color = Color.white;
        }
        transform.position = position;
        gameObject.SetActive(true);
    }

    public void OnEnable()
    {
        transform.DOPunchScale(Vector3.one, .5f, 2).OnComplete(() => Destroy(gameObject));
    }
}
