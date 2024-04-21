using System.Collections;
using System.Collections.Generic;
using damageTrigger;
using UnityEngine;
public class health_controller : MonoBehaviour
{
    public int maxHealth = 3;
    public int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void takeDamage(int damage, SpriteRenderer sr)
    {
        if (DamageTrigger.dealingDamage)
        {
            currentHealth -= damage;
            StartCoroutine(DamageEffectSequence(sr, Color.red, 0.25f, 0.25f));
            if (currentHealth <= 0)
                die();
        }
    }

    void Update()
    {

    }

    private void die()
    {

    }

    IEnumerator DamageEffectSequence(SpriteRenderer sr, Color dmgColor, float duration, float delay)
    {

        // save origin color
        Color originColor = sr.color;

        // tint the sprite with damage color
        sr.color = dmgColor;

        Debug.Log("fdsfksd");

        // you can delay the animation
        yield return new WaitForSeconds(delay);

        Debug.Log("390284");

        // lerp animation with given duration in seconds
        for (float t = 0; t < 1.0f; t += Time.deltaTime / duration)
        {
            sr.color = Color.Lerp(dmgColor, originColor, t);

            yield return null;
        }

        // restore origin color
        sr.color = originColor;
        DamageTrigger.dealingDamage = false;
    }
}
