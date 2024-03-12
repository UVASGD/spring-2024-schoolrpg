using UnityEngine;

//launches the damage color change.
public void TakeDamage()
{
    // Tints the sprite red and fades back to the origin color after a delay of 1 second
    StartCoroutine(DamageEffectSequence(sr, Color.red, 2, 1));
}

IEnumerator DamageEffectSequence(SpriteRenderer sr, Color dmgColor, float duration, float delay)
{
    // save origin color
    Color originColor = sr.color;

    // tint the sprite with damage color
    sr.color = dmgColor;

    // you can delay the animation
    yield return new WaitForSeconds(delay);

    // lerp animation with given duration in seconds
    for (float t = 0; t < 1.0f; t += Time.deltaTime / duration)
    {
        sr.color = Color.Lerp(dmgColor, originColor, t);

        yield return null;
    }

    // restore origin color
    sr.color = originColor;
}