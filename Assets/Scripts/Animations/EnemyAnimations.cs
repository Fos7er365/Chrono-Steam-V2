﻿using UnityEngine;

public class EnemyAnimations : MonoBehaviour, IEntityAnimations
{
    [SerializeField] protected Animator enemyAnimator;

    void Awake()
    {
        enemyAnimator = GetComponent<Animator>();
    }

    public void IdleAnimation()
    {
        enemyAnimator.SetTrigger(EntityAnimationTags.IDLE_ANIMATION_TAG);
    }

    public void MovingAnimation(bool isMoving)
    {

        enemyAnimator.SetBool(EntityAnimationTags.MOVING_ANIMATION_TAG, isMoving);

    }
    public void Moving_X_YAnimation(float Moving_X, float Moving_Y)
    {
        enemyAnimator.SetFloat(EntityAnimationTags.MOVING_X_ANIMATION_TAG, Moving_X);
        enemyAnimator.SetFloat(EntityAnimationTags.MOVING_Y_ANIMATION_TAG, Moving_Y);
    }
    public void RunAnimation()
    {
        enemyAnimator.SetTrigger(EntityAnimationTags.RUN_ANIMATION_TAG);
    }

    public void JumpAnimation()
    {
        enemyAnimator.SetTrigger(EntityAnimationTags.JUMP_ANIMATION_TAG);
    }

    public void AttackAnimation()
    {
        Debug.Log("Enemy attack 1 animation");
        enemyAnimator.SetTrigger(EntityAnimationTags.ATTACK_ANIMATION_TAG);
    }
    public void Attack2Animation()
    {
        Debug.Log("Enemy attack 2 animation");
        enemyAnimator.SetTrigger(EntityAnimationTags.ATTACK2_ANIMATION_TAG);
    }
    public void Attack3Animation()
    {
        Debug.Log("Enemy attack 3 animation");
        enemyAnimator.SetTrigger(EntityAnimationTags.ATTACK3_ANIMATION_TAG);
    }

    public void DamagedAnimation()
    {
        enemyAnimator.SetTrigger(EntityAnimationTags.DAMAGE_ANIMATION_TAG);
    }

    public void DeathAnimation()
    {
        enemyAnimator.SetTrigger(EntityAnimationTags.DEATH_ANIMATION_TAG);
    }

}
