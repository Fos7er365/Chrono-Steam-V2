using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBossEnemyAnimations : EnemyAnimations
{
    public void SummonAttackAnimation()
    {
            enemyAnimator.SetTrigger("onSummonAttack");
    }
    public void DesperateAttackTrigger()
    {
        enemyAnimator.SetTrigger("onDesperateAttackTrigger");
    }
    public void DesperateAttackAnimation1()
    {
        enemyAnimator.SetTrigger("onDesperateAttackAnimation1");
    }
    public void DesperateAttackAnimation2()
    {
        enemyAnimator.SetTrigger("onDesperateAttackAnimation2");
    }
    public void DesperateAttackAnimation3()
    {
        enemyAnimator.SetTrigger("onDesperateAttackAnimation3");
    }
    public void BlockAttacksAnimation(bool value)
    {
        enemyAnimator.SetBool("isBlocking", value);
    }
}
