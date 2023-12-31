﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyCombat : EnemyCombat
{
    [SerializeField] BossAI bossEnemyAIHandler;

    //Roulette Wheel variables
    Roulette _roulette;
    Dictionary<Node, int> _rouletteNodes = new Dictionary<Node, int>();
    //

    private void Start()
    {
        RouletteWheelSetUp();
    }

    public void HandleAttacksRouletteWheel()
    {
        bossEnemyAIHandler.BossFleeSB.move = false;
        bossEnemyAIHandler.BossSeekSB.move = false;
        bossEnemyAIHandler.BossObstaclAavoidanceSB.move = false;
        //gameObject.GetComponent<BossAI>().BossCharge = false;

        if (bossEnemyAIHandler.CurrentAttackTime >= bossEnemyAIHandler.DefaultAttackTime)
        {
            RouletteAction();
            bossEnemyAIHandler.CurrentAttackTime = 0;
        }
    }

    public override void Attack()
    {
        bossEnemyAIHandler.BossObstaclAavoidanceSB.move = false;
        bossEnemyAIHandler.BossFleeSB.move = false;
        bossEnemyAIHandler.CurrentAttackTime += 1 * Time.deltaTime;

        if (Vector3.Distance(transform.position, bossEnemyAIHandler.BossLineOfSight.Target.position) > bossEnemyAIHandler.EnemyModel.Stats.AttackRange)
        {
            if (gameObject.TryGetComponent<BigEnemyAI>(out var bullCharge))
            {
                /*if (bullCharge.Charge1)
                {
                    seek.move = false;
                }
                else
                {
                    seek.move = true;
                }*/
                enemyAnim.RunAnimation();
            }
            else
            {
                bossEnemyAIHandler.BossSeekSB.move = true;
                //enemyAnim.MovingAnimation(true);
                enemyAnim.RunAnimation();
            }
            //enemyAnim.MovingAnimation(true);
        }
        else
        {
            bossEnemyAIHandler.BossSeekSB.move = false;
            //enemyAnim.MovingAnimation();
        }
    }

    private void Fleeing()
    {
        bossEnemyAIHandler.BossSeekSB.move = false;
        bossEnemyAIHandler.BossObstaclAavoidanceSB.move = false;

        bossEnemyAIHandler.BossFleeSB.move = false;
        if (bossEnemyAIHandler.EnemyModel.IsDead)
        {
            bossEnemyAIHandler.BossFleeSB.move = false;
        }
    }

    //private void Patroling()
    //{
    //    bossEnemyAIHandler.BossFleeSB.move = false;
    //    bossEnemyAIHandler.BossSeekSB.move = false;
    //    bossEnemyAIHandler.BossObstaclAavoidanceSB.move = true;
    //    enemyAnim.MovingAnimation(true);
    //}

    private void TeslaBall()
    {
        bossEnemyAIHandler.BossFleeSB.move = false;
        bossEnemyAIHandler.BossSeekSB.move = false;
        enemyAnim.Attack2Animation();
        Debug.Log("TeslaBall");
    }

    private void Clap()
    {
        bossEnemyAIHandler.BossFleeSB.move = false;
        bossEnemyAIHandler.BossSeekSB.move = false;
        enemyAnim.AttackAnimation();
        Debug.Log("Clap");
    }

    private void SmashGround()
    {
        bossEnemyAIHandler.BossFleeSB.move = false;
        bossEnemyAIHandler.BossSeekSB.move = false;
        enemyAnim.Attack3Animation();
        Debug.Log("SmashGround");
    }
    private void Charge()
    {
        bossEnemyAIHandler.BossFleeSB.move = false;
        bossEnemyAIHandler.BossSeekSB.move = false;
        gameObject.GetComponent<BigEnemyAI>().BossCharge = true;
        Debug.Log("Charge");
    }

    //se llama desde el animator 
    public void Attacking()
    {
        bossEnemyAIHandler.BossSeekSB.move = false;
    }
    public void RouletteWheelSetUp()
    {
        _roulette = new Roulette();

        ActionNode teslaBallAttack = new ActionNode(TeslaBall);
        ActionNode clapAttack = new ActionNode(Clap);
        ActionNode smashGroundAttack = new ActionNode(SmashGround);
        ActionNode chargeAttack = new ActionNode(Charge);


        HandleRouletteWheelNodes(clapAttack, smashGroundAttack, chargeAttack, teslaBallAttack);

        ActionNode rouletteAction = new ActionNode(RouletteAction);
    }

    void HandleRouletteWheelNodes(ActionNode clap, ActionNode sGround, ActionNode charge, ActionNode tBall)
    {

        if (enemyModel.EnemyHealthController.CurrentLife > enemyModel.Stats.MaxHealth * 0.66)
        {
            if (!_rouletteNodes.ContainsKey(clap) && !_rouletteNodes.ContainsKey(sGround))
            {
                if (Vector3.Distance(transform.position, GameManager.Instance.PlayerInstance.transform.position) > enemyModel.Stats.AttackRange)
                {
                    _rouletteNodes.Add(clap, 75);
                    _rouletteNodes.Add(sGround, 25);
                }
                else
                {
                    _rouletteNodes.Add(clap, 25);
                    _rouletteNodes.Add(sGround, 75);
                }
            }
        }
        else if (enemyModel.EnemyHealthController.CurrentLife > enemyModel.Stats.MaxHealth * 0.33)
        {
            if (!_rouletteNodes.ContainsKey(charge))
            {
                if (Vector3.Distance(transform.position, bossEnemyAIHandler.BossLineOfSight.Target.position) > enemyModel.Stats.AttackRange)
                {
                    _rouletteNodes.Add(sGround, 27);
                    _rouletteNodes.Add(clap, 33);
                    _rouletteNodes.Add(charge, 40);
                }
                else
                {
                    _rouletteNodes.Add(clap, 27);
                    _rouletteNodes.Add(charge, 33);
                    _rouletteNodes.Add(sGround, 40);
                }
            }
        }
        else
        {
            if (!_rouletteNodes.ContainsKey(tBall))
            {
                if (Vector3.Distance(transform.position, bossEnemyAIHandler.BossLineOfSight.Target.position) > enemyModel.Stats.AttackRange)
                {
                    _rouletteNodes.Add(sGround, 10);
                    _rouletteNodes.Add(clap, 30);
                    _rouletteNodes.Add(charge, 15);
                    _rouletteNodes.Add(tBall, 45);
                }
                else
                {
                    _rouletteNodes.Add(clap, 10);
                    _rouletteNodes.Add(charge, 30);
                    _rouletteNodes.Add(sGround, 45);
                    _rouletteNodes.Add(tBall, 15);
                }
            }
        }
    }

    public void RouletteAction()
    {
        Node nodeRoulette = _roulette.Run(_rouletteNodes);

        nodeRoulette.Execute();
    }
}
