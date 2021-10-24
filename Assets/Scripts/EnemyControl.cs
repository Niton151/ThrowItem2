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
    [SerializeField]
    private float interval;

    [SerializeField]
    private float maxSpeed;

    [SerializeField]
    private float smoothTime; //�ړI�n�܂łɂ����鎞��

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
    }

    
    void Update()
    {
        //�v���C���[�̕�������
       this.transform.LookAt(player.transform);

        //�����_���Ȉʒu�Ɉړ�����
        moveTimer += Time.deltaTime;

        if (lastPosition == transform.position && isMove == true) 
        {
            isMove = false;
        }

        if(moveTimer >= interval)
        {
            randomPos = RandomPosition.RandomPos(rangeA, rangeB);
            isMove = true;
            moveTimer = 0;
        }
        lastPosition = transform.position;
        transform.position = Vector3.SmoothDamp(transform.position, randomPos, ref velocity, smoothTime, maxSpeed);


        //��������U��
        attackTimer += Time.deltaTime;

        if (!isMove && attackTimer >= normalInterval)
        {
            Attack();
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
        nomalMuzzle.GetComponent<LongRangeWeapon>().CreatBullet();
        attackTimer = 0;
    }
}
