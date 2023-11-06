﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBossEnemyCombat : EnemyCombat
{
    [SerializeField] BossAI bossEnemyAIHandler;
    [SerializeField] float regularAttackThreshold, summonAttackThreshold, regularAttackTimer, enhancedAttackTimer, blockAttacksTimer;
    [SerializeField] float regularAttackCooldown, desperateAttackCooldown, blockAttacksCooldown;
    [SerializeField] float regenerationAmount;
    [SerializeField] Transform[] boidsSpawnPositions;
    [SerializeField] GameObject[] boidsPrefabs;
    int boidsCount;
    bool canSummon;

    float currentBlockTime = 0;
    FinalBossEnemyAnimations finalBossEnemyAnim;

    //Roulette Wheel variables
    Roulette _regularAttacksRoulette, _desperateAttacksRoulette;
    Dictionary<Node, int> _regularAttacksRouletteNodes = new Dictionary<Node, int>();
    Dictionary<Node, int> _desperateAttacksRouletteNodes = new Dictionary<Node, int>();
    //

    public float RegularAttackThreshold { get => regularAttackThreshold; set => regularAttackThreshold = value; }
    public float SummonAttackThreshold { get => summonAttackThreshold; set => summonAttackThreshold = value; }
    public float RegularAttackTimer { get => regularAttackTimer; set => regularAttackTimer = value; }
    public float EnhancedAttackTimer { get => enhancedAttackTimer; set => enhancedAttackTimer = value; }
    public float BlockAttacksTimer { get => blockAttacksTimer; set => blockAttacksTimer = value; }

    private void Start()
    {
        finalBossEnemyAnim = enemyAnim as FinalBossEnemyAnimations;
        RegularAttacksRouletteWheelSetUp();
        DesperateAttacksRouletteWheelSetUp();
    }

    public override void Attack()
    {
        bossEnemyAIHandler.BossObstaclAavoidanceSB.move = false;
        bossEnemyAIHandler.CurrentAttackTime += 1 * Time.deltaTime;
        if(bossEnemyAIHandler.CurrentAttackTime > regularAttackCooldown)
        {
            RegularRouletteAction();
            bossEnemyAIHandler.CurrentAttackTime = 0;
        }

    }
    public void SummonAttack()
    {
        foreach (var boid in boidsSpawnPositions)
        {
            if (boidsCount >= boidsSpawnPositions.Length)
            {
                Debug.Log("Enough boids spawned");
                canSummon = false;
                return;
            }
            Debug.Log("Boid instanced");
            Instantiate(boidsPrefabs[Random.Range(0, boidsPrefabs.Length)], boid.position, Quaternion.identity);
            boidsCount++;
        }
    }
    public void DesperateAttack()
    {
        bossEnemyAIHandler.BossObstaclAavoidanceSB.move = false;
        bossEnemyAIHandler.CurrentAttackTime += 1 * Time.deltaTime;

        if (bossEnemyAIHandler.CurrentAttackTime > desperateAttackCooldown)
        {
            DesperateRouletteAction();
            bossEnemyAIHandler.CurrentAttackTime = 0;
        }
    }

    public void BlockAttacks()
    {
        currentBlockTime += Time.deltaTime;
        finalBossEnemyAnim.BlockAttacksAnimation(true);
        enemyModel.EnemyHealthController.GetHeal(regenerationAmount);
        if(currentBlockTime > blockAttacksCooldown)
        {
            finalBossEnemyAnim.BlockAttacksAnimation(false);
            currentBlockTime = 0;
        }
    }

    #region Regular Attack Roulette Wheel
    public void RegularAttacksRouletteWheelSetUp()
    {
        _regularAttacksRoulette = new Roulette();

        ActionNode attackAnim1 = new ActionNode(AttackAnimation1);
        ActionNode attackAnim2 = new ActionNode(AttackAnimation2);
        ActionNode attackAnim3 = new ActionNode(AttackAnimation3);

        _regularAttacksRouletteNodes.Add(attackAnim1, 35);
        _regularAttacksRouletteNodes.Add(attackAnim2, 20);
        _regularAttacksRouletteNodes.Add(attackAnim3, 10);

        ActionNode rouletteAction = new ActionNode(RegularRouletteAction);
    }

    public void RegularRouletteAction()
    {
        Node nodeRoulette = _regularAttacksRoulette.Run(_regularAttacksRouletteNodes);

        nodeRoulette.Execute();
    }

    void AttackAnimation1()
    {
        enemyAnim.AttackAnimation();
    }
    void AttackAnimation2()
    {
        enemyAnim.Attack2Animation();
    }
    void AttackAnimation3()
    {
        enemyAnim.Attack3Animation();
    }
    #endregion

    #region Desperate Attack Roulette Wheel
    public void DesperateAttacksRouletteWheelSetUp()
    {
        _desperateAttacksRoulette = new Roulette();

        ActionNode attackAnim1 = new ActionNode(DesperateAttackAnimation1);
        ActionNode attackAnim2 = new ActionNode(DesperateAttackAnimation2);
        ActionNode attackAnim3 = new ActionNode(DesperateAttackAnimation3);

        _desperateAttacksRouletteNodes.Add(attackAnim1, 20);
        _desperateAttacksRouletteNodes.Add(attackAnim2, 30);
        _desperateAttacksRouletteNodes.Add(attackAnim3, 18);

        ActionNode rouletteAction = new ActionNode(DesperateRouletteAction);
    }

    public void DesperateRouletteAction()
    {
        Node nodeRoulette = _desperateAttacksRoulette.Run(_desperateAttacksRouletteNodes);

        nodeRoulette.Execute();
    }

    void DesperateAttackAnimation1()
    {
        finalBossEnemyAnim.DesperateAttackAnimation1();
    }
    void DesperateAttackAnimation2()
    {
        finalBossEnemyAnim.DesperateAttackAnimation2();
    }
    void DesperateAttackAnimation3()
    {
        finalBossEnemyAnim.DesperateAttackAnimation3();
    }
    #endregion

}
