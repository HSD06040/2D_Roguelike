using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMonsterFSM : MonoBehaviour
{
    public MonsterStat stat;
    public Transform Player;
    public Monster Owner;   
    public bool isPatternPlaying;
    public bool animFinish;

    #region AnimHash
    protected static readonly int idleHash = Animator.StringToHash("Idle");
    protected static readonly int attackHash = Animator.StringToHash("Attack");
    protected static readonly int runHash = Animator.StringToHash("Run");
    protected static readonly int dieHash = Animator.StringToHash("Dead");
    #endregion   

    private void Awake()
    {
        Owner.SetStats(stat.health, stat.attackPower);
    }

    public void AnimFinish() => animFinish = true;

    public IEnumerator FadeOutCoroutine(float _fadeDuration)
    {        
        float elapsed = 0f;
        Color originalColor = Owner.spriteRenderer.color;

        while (elapsed < _fadeDuration)
        {
            float alpha = Mathf.Lerp(1f, 0f, elapsed / _fadeDuration);
            Owner.spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            elapsed += Time.deltaTime;
            yield return null;
        }

        Owner.spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);
    }
}
