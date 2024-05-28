using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningAttack : MonoBehaviour
{
    /*public Transform target;
    public float reachTargetDuration;
    public float activeDuration;
    public bool isActive;
    public float activeTimer;
    public Vector3 startScale = Vector3.one;
    public GameObject fx;
    public void Active()
    {
        transform.localScale = startScale;
        isActive = true;
        activeTimer = 0;
        fx.gameObject.SetActive(true);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            Active();
        }
        if (isActive)
        {
            activeTimer += Time.deltaTime;
            if (activeTimer > activeDuration)
            {
                activeTimer = 0;

                fx.gameObject.SetActive(false);
                isActive = false;
            }
            Vector3 direction = target.position - transform.position;
            Vector3 targetScale = transform.localScale;
            targetScale.y = direction.magnitude;

            transform.localScale = Vector3.Lerp(transform.localScale, targetScale, activeTimer / reachTargetDuration);
            transform.up = direction;
        }
    }*/

    private void OnParticleCollision(GameObject other)
    {
        Debug.Log(other.name);
    }
}
