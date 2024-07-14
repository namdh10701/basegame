using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ConsumedManaDisplay : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI damageText;

    public void Init(float mana, Vector3 position)
    {
        transform.position = position;
        damageText.text = "-" + mana.ToString();
        gameObject.SetActive(true);
    }

    public void OnEnable()
    {
        transform.DOMoveY(transform.position.y + 1f, 1).OnComplete(() => Destroy(gameObject));
    }
}
