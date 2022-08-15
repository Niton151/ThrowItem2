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
    private float interval;

    [SerializeField]
    private float stableInterval;

    [SerializeField]
    private float cautionInterval;

    [SerializeField]
    private float maxSpeed;

    private float smoothTime;

    [SerializeField]
    private float stableSmoothTime; //目的地までにかかる時間

    [SerializeField]
    private float cautionSmoothTime;

    Vector3 velocity = Vector3.zero;

    private Vector3 randomPos;

    [SerializeField]
    private Transform rangeA, rangeB; //対角に設置

    private float moveTimer; //時間計測用

    [HideInInspector]
    private bool isMove = false;

    private Vector3 lastPosition;

    private GameObject player;

    private bool isCaution = false;

    private Vector3 latestPos;

    //ここから攻撃用変数
    [Header("攻撃用変数")]

    [SerializeField]
    private Transform nomalMuzzle;

    [SerializeField]
    private float normalInterval;

    private float attackTimer;

    public static bool isTimeStop;

    [SerializeField]
    private GameObject explosion;


    void Start()
    {
        this.hp = this.maxHp;
        moveTimer = interval - 1;
        randomPos = RandomPosition.RandomPos(rangeA, rangeB);
        player = GameObject.Find("PlayerCollider");
    }


    void Update()
    {
        if (isCaution && isTimeStop == false)
        {
            AttackMode();
        }
        else
        {
            smoothTime = stableSmoothTime;
            interval = stableInterval;
            Vector3 diff = transform.position - latestPos;   //前回からどこに進んだかをベクトルで取得
            latestPos = transform.position;  //前回のPositionの更新

            //ベクトルの大きさが0.01以上の時に向きを変える処理をする
            if (diff.magnitude > 0.01f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(diff); //向きを変更する
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5);
            }
        }


        //ランダムな位置に移動する
        moveTimer += Time.deltaTime;

        if (lastPosition == transform.position && isMove == true)
        {
            isMove = false;
        }

        if (moveTimer >= interval)
        {
            while (true)
            {
                randomPos = RandomPosition.RandomPos(rangeA, rangeB);
                if (Vector3.Distance(randomPos, player.transform.position) > 5f && randomPos.y > 3f)
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
            explosion.SetActive(true);
            Destroy(this.gameObject, 0.5f);
        }
    }

    private void Attack()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            if (hit.collider.gameObject.layer == player.layer)
            {
                nomalMuzzle.GetComponent<EnemyLRW>().CreatBullet();
                attackTimer = 0;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isCaution = true;
        }
        if (other.gameObject.CompareTag("PlayerBullet"))
        {

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
        smoothTime = cautionSmoothTime;
        interval = cautionInterval;
        //プレイヤーの方を向く
        this.transform.LookAt(player.transform);

        //ここから攻撃
        attackTimer += Time.deltaTime;

        if (!isMove && attackTimer >= normalInterval)
        {
            Attack();
        }
    }
}
