using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyControl : MonoBehaviour
{
    [Header("ステータス")]
    [SerializeField]
    private float maxHp;

    private float hp;

    [Header("移動用変数")]
    [SerializeField]
    private float interval;

    [SerializeField]
    private float maxSpeed;

    [SerializeField]
    private float smoothTime; //目的地までにかかる時間

    Vector3 velocity = Vector3.zero;

    private Vector3 randomPos;

    [SerializeField]
    private Transform rangeA, rangeB; //対角に設置

    private float moveTimer; //時間計測用

    [HideInInspector]
    private bool isMove = false;

    private Vector3 lastPosition;

    [SerializeField]
    private GameObject player;


    //ここから攻撃用変数
    [Header("攻撃用変数")]
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
        //プレイヤーの方を向く
       this.transform.LookAt(player.transform);

        //ランダムな位置に移動する
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


        //ここから攻撃
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
