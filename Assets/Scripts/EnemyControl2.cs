using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyControl2 : MonoBehaviour
{
    [Header("�X�e�[�^�X")]
    [SerializeField]
    private float maxHp;

    [SerializeField]
    private float hp;

    [Header("�ړ��p�ϐ�")]

    private Vector3 randomPos;

    private GameObject playerPos;

    private bool isCaution = true;

    [SerializeField]
    private float waitTime;

    private float time;

    [SerializeField]
    private float moveRange = 5f;

    [SerializeField]
    private GameObject exprode;

    //��������U���p�ϐ�
    [Header("�U���p�ϐ�")]

    private NavMeshAgent agent;

    private Animator anim;

    [SerializeField]
    private GameObject fire;

    void Start()
    {
        this.hp = this.maxHp;
        playerPos = GameObject.Find("PlayerPos");
        randomPos = RandomPosition.RandomPos(moveRange);
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        GotoNextPoint();
    }


    void Update()
    {
        if (isCaution && EnemyControl.isTimeStop == false)
        {
            AttackMode();
        }
        else
        {
            GotoNextPoint();
            if (!agent.pathPending && agent.remainingDistance < 2f)
            {
                StopHere();
            }
        }
    }

    public void EnemyAttacked(float damage)
    {
        this.hp -= damage;
        if (this.hp <= 0)
        {
            EnemyDead();
        }
    }

    private void Attack()
    {
        anim.SetBool("isAttack", true);
        fire.SetActive(true);
    }

    private void OnTriggerStay(Collider other)
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
        //�v���C���[�̕�������
        this.transform.LookAt(playerPos.transform);
        agent.destination = playerPos.transform.position;
        anim.SetBool("isForward", true);

        //��������U��
        if (Vector3.Distance(this.transform.position, playerPos.transform.position) < 3f)
        {
            anim.SetBool("isForward", false);
            agent.isStopped = true;
            Attack();
        }
        else 
        {
            anim.SetBool("isAttack", false);
            agent.isStopped = false;
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
        //NavMeshAgent���~�߂�
        agent.isStopped = true;
        //�҂����Ԃ𐔂���
        time += Time.deltaTime;

        //�҂����Ԃ��ݒ肳�ꂽ���l�𒴂���Ɣ���
        if (time > waitTime)
        {
            //�ڕW�n�_��ݒ肵����
            GotoNextPoint();
            time = 0;
        }
    }

    private void EnemyDead()
    {
        exprode.SetActive(true);
        Destroy(this.gameObject, 0.5f);
    }
}

