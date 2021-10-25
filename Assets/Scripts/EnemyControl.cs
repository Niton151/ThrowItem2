using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyControl : MonoBehaviour
{
    [Header("�X�e�[�^�X")]
    [SerializeField]
    private float maxHp;

    private float hp;

    [Header("�ړ��p�ϐ�")]
    private float interval;

    [SerializeField]
    private float stableInterval;

    [SerializeField]
    private float cautionInterval;

    [SerializeField]
    private float maxSpeed;

    private float smoothTime;

    [SerializeField]
    private float stableSmoothTime; //�ړI�n�܂łɂ����鎞��

    [SerializeField]
    private float cautionSmoothTime;

    Vector3 velocity = Vector3.zero;

    private Vector3 randomPos;

    [SerializeField]
    private Transform rangeA, rangeB; //�Ίp�ɐݒu

    private float moveTimer; //���Ԍv���p

    [HideInInspector]
    private bool isMove = false;

    private Vector3 lastPosition;

    [SerializeField]
    private GameObject player;

    private bool isCaution = false;

    //��������U���p�ϐ�
    [Header("�U���p�ϐ�")]
    [SerializeField]
    private float power;

    [SerializeField]
    private Transform nomalMuzzle;

    [SerializeField]
    private float normalInterval;

    private float attackTimer;

    void Start()
    {
        this.hp = this.maxHp;
        moveTimer = interval - 1;
        randomPos = RandomPosition.RandomPos(rangeA, rangeB);
    }

    
    void Update()
    {
        Debug.Log(Vector3.Distance(randomPos, player.transform.position));
        if (isCaution)
        {
            smoothTime = cautionSmoothTime;
            interval = cautionInterval;
            //�v���C���[�̕�������
            this.transform.LookAt(player.transform);

            //��������U��
            attackTimer += Time.deltaTime;

            if (!isMove && attackTimer >= normalInterval)
            {
                Attack();
            }
        }
        else 
        {
            smoothTime = stableSmoothTime;
            interval = stableInterval;
        }

        //�����_���Ȉʒu�Ɉړ�����
        moveTimer += Time.deltaTime;

        if (lastPosition == transform.position && isMove == true) 
        {
            isMove = false;
        }

        if(moveTimer >= interval)
        {
            while (true)
            {
                randomPos = RandomPosition.RandomPos(rangeA, rangeB);
                if(Vector3.Distance(randomPos, player.transform.position) > 5f)
                {
                    break;
                }
            }
            isMove = true;
            moveTimer = 0;
        }
        lastPosition = transform.position;
        transform.position = Vector3.SmoothDamp(transform.position, randomPos, ref velocity, smoothTime, maxSpeed);  
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
        nomalMuzzle.GetComponent<EnemyLRW>().CreatBullet();
        attackTimer = 0;
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
}
