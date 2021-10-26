using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyControl2 : MonoBehaviour
{
    [Header("�X�e�[�^�X")]
    [SerializeField]
    private float maxHp;

    private float hp;

    [Header("�ړ��p�ϐ�")]

    private Vector3 randomPos;

    [SerializeField]
    private Transform rangeA, rangeB; //�Ίp�ɐݒu

    [SerializeField]
    private GameObject player;

    private bool isCaution = false;

    [SerializeField]
    private float waitTime;

    private float time;

    //��������U���p�ϐ�
    [Header("�U���p�ϐ�")]

    private NavMeshAgent agent;

    private Animator anim;

    [SerializeField]
    private GameObject fire;

    void Start()
    {
        this.hp = this.maxHp;
        randomPos = RandomPosition.RandomPos(rangeA, rangeB);
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        GotoNextPoint();
    }


    void Update()
    {
        if (isCaution)
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
        //�v���C���[�̕�������
        this.transform.LookAt(player.transform);
        agent.destination = player.transform.position;
        anim.SetBool("isForward", true);

        //��������U��
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
            randomPos = RandomPosition.RandomPos(rangeA, rangeB);
            agent.destination = randomPos;
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
}

