using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    [SerializeField]
    private List<string> messsage = new List<string>
    {   "解凍完了",
        "おはようございます",
        "即席のコールドスリープにより、記憶障害が発生している可能性があるので",
        "この世界について軽く説明します",

        //紙芝居
        "昔々、人々は平和に暮らしていました",
        "ある日宇宙から地球外生命体がやってきました",
        "その生物は「この星の文明を抹消する」といい、人類に攻撃を開始しました",
        "人類はその圧倒的な攻撃に対抗できず約99.9%が死滅しました",
        "あなたは実験段階のコールドスリープ装置によって生き延びました",

        "現在地上では侵略者が解き放った機械「デコン」によって文明の分解活動が進められています",
        "そこであなたにはデコンの討伐も兼ねて、「コンサーバー」として人類の文明を保存していただきます",
        "データを保存するためには、まだ地上に残っている人工物、すなわち「遺物」を回収しに行く必要があります",
        "そのあたりにある球体が小さなブラックホールを生成して遺物を吸収し、データ化してくれます",
        "しかしブラックホールの生成には時間がかかります",
        "あなたにはその間、ブラックホールをデコンから守っていただきます",

        "ではまずそこにある球体を掴んで、遺物がありそうな場所に投げてみましょう",
        //掴む操作説明

        "この球体の着弾地点にテレポートできるようになるので、持って行きたい武器を掴みながらテレポートしてみましょう",
        //テレポートボタン説明

        "テレポート成功です",
        "ブラックホールはあなたの真上に生成されていきます",
        "早速デコンの反応が近づいています",
        "持っている剣を振って戦いましょう",
        //HP説明
        //戦闘

        "遺物の回収が完了しました",
        "遠距離攻撃が可能なデコンが出現したので、急いで球体を掴んで拠点にテレポートしてください",
        //拠点帰還説明

        "お疲れさまでした",
        "では入手した遺物を確認に行きましょう",
        "左側にあるものに触れてBボタンを押すとデータエリアにテレポートします",
        //データエリアにテレポート

        "ここには回収した遺物が転送されてきます",
        "しかし遺物はすべてデータ化して復元可能なので、回収したものは有効活用しましょう",
        "ここでは武器やアイテムをクラフトすることができます",
        "まずはあたりに落ちている遺物を拾ってみましょう",
        "遺物の上部に表示されているのは入手可能素材です",
        "この異物を同じ色のエリアに投げ入れると表示されている素材を入手できます",
        "このタブレット端末にクラフトできるものと必要素材が書いてあります",
        "まずは先程の戦闘で減少したHPを回復するために、注射器を作ってみましょう",
        //注射器の作成
        //クラフト説明

        "では回復してみましょう",
        //回復説明

        "次に遠距離のデコンに対抗するために拳銃とマガジンを作成しましょう",
        //クラフト

        "データエリアでは弾を消費せずに試し打ちをすることができます",
        "では拠点に戻りましょう",
        //拠点にテレポート

        "このあたりの遺物は次の回収で最後のようです",
        "もう一度遺物の回収に向かいましょう",
        //フィールドにテレポート

        "デコンの反応が異常に集中しています",
        "危険ですが、あなたなら耐えられるはずです",
        "ご武運を",
        //戦闘
        //帰還

        "さすがです",
        "あなたはもう一人前のコンサーバーです",
        "今はもう休みましょう",
        "お疲れさまでした"
    };

    [SerializeField]
    private List<Sprite> sprites;

    private float timer = 0;

    private float interval = 0;

    private int latestIndex = 0;

    private static int returnBaseCount = 0;

    [SerializeField]
    private GameObject canvas;

    [SerializeField]
    private Text text;

    [SerializeField]
    private Image image;

    [SerializeField]
    private GameObject player;

    [SerializeField]
    private GameObject tellGre;

    [SerializeField]
    private GameObject enemySpawn;

    private static bool isGround;

    private static bool isRunning;

    private static bool isBase;

    private static bool isCraftTell;

    private static bool isCraftSyringe;

    private static bool isReturnBase;

    private bool isAddEnemy = false;

    [SerializeField]
    private bool isTutorial;

    void Start()
    {
        text.text = messsage[0];
    }

    void Update()
    {
        if (isTutorial)
        {
            switch (returnBaseCount)
            {
                case 0:
                    FirstTutorial();
                    break;
                case 1:
                    CraftTutorial();
                    break;
                case 2:
                    EndTutorial();
                    break;
                default:
                    break;
            }
        }
        else
        {
            text.enabled = false;
            tellGre.SetActive(true);
            //ActiveImage(11);
            if (OVRInput.GetDown(OVRInput.RawButton.A))
            {
                
            }
            if(isAddEnemy == false && returnBaseCount == 1)
            {
                enemySpawn.GetComponent<ItemSpawn>().AddEnemy();
                isAddEnemy = true;
            }
        }
    }

    private void FirstTutorial()
    {
        timer += Time.deltaTime;
        if (timer >= interval)
        {
            text.text = messsage[latestIndex];
            if (4 <= latestIndex && latestIndex <= 8)
            {
                ActiveImage(latestIndex - 4);
                GoMessage();
            }
            else if (latestIndex == 15)
            {
                tellGre.SetActive(true);
                ActiveImage(5);
                if (isGround == true)
                {
                    GoMessage();
                }
            }
            else if (latestIndex == 16)
            {
                ActiveImage(6);
                if (OVRInput.GetDown(OVRInput.RawButton.B) || Input.GetKeyDown(KeyCode.Space))
                {
                    GoMessage();
                    EnemyControl.isTimeStop = true;
                }
            }
            else if (latestIndex == 20)
            {
                ActiveImage(7);
                if (isRunning == true)
                {
                    interval = 10;
                    latestIndex = 21;
                }
            }
            else if (latestIndex == 21)
            {
                canvas.SetActive(false);
                EnemyControl.isTimeStop = false;
                if (isRunning == false)
                {
                    GoMessage();
                    canvas.SetActive(true);
                }
            }
            else if (latestIndex == 23)
            {
                image.enabled = false;
                enemySpawn.GetComponent<ItemSpawn>().AddEnemy();
            }
            else
            {
                image.enabled = false;
                GoMessage();
            }
        }
    }

    private void CraftTutorial()
    {
        timer += Time.deltaTime;
        if (timer >= interval)
        {
            text.text = messsage[latestIndex];
            if(latestIndex == 26)
            {
                if(isCraftTell == true)
                {
                    GoMessage();
                }
            }
            else if(latestIndex == 35)
            {
                ActiveImage(8);
                interval = 10;
                latestIndex = 36;
            }
            else if(latestIndex == 36)
            {
                canvas.SetActive(false);
                if (isCraftSyringe)
                {
                    GoMessage();
                    canvas.SetActive(true);
                }
            }
            else if(latestIndex == 37)
            {
                ActiveImage(9);
                var pc = player.GetComponent<PlayerControl>();
                if (pc.GetPlayerHP() == pc.GetPlayerMaxHP())
                {
                    GoMessage();
                }
            }
            else if(latestIndex == 38)
            {
                ActiveImage(10);
                interval = 10;
                latestIndex = 39;
            }
            else if(latestIndex == 39)
            {
                image.enabled = false;
                latestIndex = 40;
            }
            else if(latestIndex == 41)
            {
                if (isReturnBase)
                {
                    GoMessage();
                }
            }
            else if(latestIndex == 42)
            {
                if(isReturnBase == false)
                {
                    GoMessage();
                }
            }
            else if(latestIndex == 46)
            {
                canvas.SetActive(false);
                GoMessage();
            }

            else
            {
                image.enabled = false;
                GoMessage();
            }
        }
    }

    private void EndTutorial()
    {
        timer += Time.deltaTime;
        if (timer >= interval)
        {
            text.text = messsage[latestIndex];
            if(latestIndex == 47)
            {
                canvas.SetActive(true);
                GoMessage();
            }
            else if(latestIndex == 51)
            {
                canvas.SetActive(false);
            }
            else
            {
                image.enabled = false;
                GoMessage();
            }
        }
    }

    public static void IntoReturnCount(int i)
    {
        returnBaseCount = i;
    }

    public static void IntoIsGround(bool ig)
    {
        isGround = ig;
    }

    public static void IntoIsRunning(bool ir)
    {
        isRunning = ir;
    }

    public static void IntoIsBase(bool ib)
    {
        isBase = ib;
    }

    public static void IntoIsCraftTell(bool ict)
    {
        isCraftTell = ict;
    }

    public static void IntoIsCraftSyringe(bool ics)
    {
        isCraftSyringe = ics;
    }

    public static void IntoIsReturnBase(bool irb)
    {
        isReturnBase = irb;
    }

    private void ActiveImage(int count)
    {
        image.enabled = true;
        image.sprite = sprites[count];
    }

    private void GoMessage()
    {
        timer = 0;
        interval = messsage[latestIndex].Length / 4;
        latestIndex++;
    }
}
