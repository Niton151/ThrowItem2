using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyControl2 : MonoBehaviour
{
    [Header("ステータス")]
    [SerializeField]
    private float maxHp;

    private float hp;

    [Header("移動用変数")]

    private Vector3 randomPos;

    private GameObject player;

    private bool isCaution = false;

    [SerializeField]
    private float waitTime;

    private float time;

    [SerializeField]
    private float moveRange = 5f;

    //ここから攻撃用変数
    [Header("攻撃用変数")]

    private NavMeshAgent agent;

    private Animator anim;

    [SerializeField]
    private GameObject fire;

    void Start()
    {
        this.hp = this.maxHp;
        randomPos = RandomPosition.RandomPos(moveRange);
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        GotoNextPoint();
        player = GameObject.Find("Player");
    }


    void Update()
    {
        if (isCaution && EnemyControl.isTimeStop == false)
        {
            AttackMode();
        }
        else GotoNextPoint();

        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            StopHere();
        }
    }

    public void EnemyAttacked(float damage)
    {
        this.hp -= damage;
        if (this.hp <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void Attack()
    {
        anim.SetBool("isAttack", true);
        fire.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isCaution = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isCaution = false;
        }
    }

    private void AttackMode()
    {
        //プレイヤーの方を向く
        this.transform.LookAt(player.transform);
        agent.destination = player.transform.position;
        anim.SetBool("isForward", true);

        //ここから攻撃
        if (Vector3.Distance(this.transform.position, player.transform.position) < 3f)
        {
            anim.SetBool("isForward", false);
            agent.isStopped = true;
            Attack();
        }
        else 
        {
            anim.SetBool("isAttack", false);
            fire.SetActive(false);
        }       
    }

    private void GotoNextPoint()
    {
        anim.SetBool("isForward", true);
        if(agent.remainingDistance < 3f)
        {
            anim.SetBool("isForward", false);
            agent.isStopped = false;
            randomPos = RandomPosition.RandomPos(moveRange);
            agent.destination = this.transform.position + randomPos;
        }       
    }

    private void StopHere()
    {
        //NavMeshAgentを止める
        agent.isStopped = true;
        //待ち時間を数える
        time += Time.deltaTime;

        //待ち時間が設定された数値を超えると発動
        if (time > waitTime)
        {
            //目標地点を設定し直す
            GotoNextPoint();
            time = 0;
        }
    }
}

