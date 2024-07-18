using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    [SerializeField]
    private List<string> messsage = new List<string>
    {   "?????",
        "??????????????",
        "?????R?[???h?X???[?v????A?L????Q????????????????????????",
        "??????E??????y??????????",

        //?????
        "??X?A?l?X????a????????????",
        "??????F??????n???O??????????????????",
        "?????????u?????????????????v??????A?l???U????J?n???????",
        "?l????????|?I??U?????R???????99.9%????????????",
        "???????????i?K??R?[???h?X???[?v???u????????????т????",

        "????n????N???????????????@?B?u?f?R???v????????????????????i?????????",
        "?????????????f?R?????????????A?u?R???T?[?o?[?v?????l????????????????????????",
        "?f?[?^????????????A????n???c???????l?H???A??????u???v????????s???K?v????????",
        "??????????????????????u???b?N?z?[????????????z?????A?f?[?^???????????",
        "???????u???b?N?z?[??????????????????????",
        "????????????A?u???b?N?z?[????f?R????????????????????",

        "????????????????????A?????????????????????????",
        //????????

        "??????????e?n?_??e???|?[?g????????????A??????s?????????????????e???|?[?g?????????",
        //?e???|?[?g?{?^?????

        "?e???|?[?g???????",
        "?u???b?N?z?[??????????^?????????????????",
        "?????f?R???????????????????",
        "???????????U??????????",
        //HP???
        //??

        "???????????????????",
        "???????U????????f?R?????o?????????A?}?????????????_??e???|?[?g???????????",
        //???_?A????

        "???????????",
        "????????????m?F??s???????",
        "????????????G???B?{?^?????????f?[?^?G???A??e???|?[?g?????",
        //?f?[?^?G???A??e???|?[?g

        "??????????????????]???????????",
        "??????????????f?[?^???????????????A???????????L?????p???????",
        "???????????A?C?e????N???t?g???邱?????????",
        "??????????????????????E?????????",
        "????????????????????????f????",
        "??????????F??G???A?????????????????????f???????????",
        "????^?u???b?g?[????N???t?g????????K?v?f??????????????",
        "???????????????????HP?????????A???????????????",
        //???????
        //?N???t?g???

        "????????????",
        //????

        "???????????f?R?????R?????????e??}?K?W????????????",
        //?N???t?g

        "?f?[?^?G???A???e???????????????????邱?????????",
        "?????_????????",
        //???_??e???|?[?g

        "???????????????????????????",
        "?????x??????????????????",
        //?t?B?[???h??e???|?[?g

        "?f?R?????????????W??????????",
        "????????A???????????????????",
        "?????^??",
        //??
        //?A??

        "?????????",
        "???????????l?O??R???T?[?o?[???",
        "???????x??????",
        "???????????"
    };

    [SerializeField]
    private List<Sprite> sprites;

    private float timer = 0;

    private float interval = 0;

    private int latestIndex = 0;

    public static int returnBaseCount = 0;

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

    public bool isTutorial;

    private bool isStart;

    [SerializeField]
    private Toggle toggle;

    private GameObject uiHelper;
    
    [SerializeField] private GameObject portal;

    void Start()
    {
        uiHelper = GameObject.Find("UIHelpers");
    }

    void Update()
    {
        if (OVRInput.GetDown(OVRInput.RawButton.RIndexTrigger))
        {
            timer = 100;
        }
        
        if (isStart == false)
        {
            canvas.SetActive(false);
            isTutorial = toggle.isOn;
        }

        if (isTutorial && isStart == true)
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
            canvas.SetActive(false);
            tellGre.SetActive(true);
        }
    }

    private void FirstTutorial()
    {
        timer += Time.deltaTime;
        interval = messsage[latestIndex].Length / 4;
        if (timer >= interval)
        {
            if (latestIndex == 0)
            {
                canvas.SetActive(true);
                GoMessage();
            }
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
                if (player.transform.position.y < 20)
                {
                    GoMessage();
                    EnemyControl.isTimeStop = true;
                }
            }
            else if (latestIndex == 20)
            {
                //ActiveImage(7);
                if (isRunning == true)
                {
                    GoMessage();
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
                if (!isAddEnemy)
                {
                    enemySpawn.GetComponent<ItemSpawn>().AddEnemy(); 
                    enemySpawn.GetComponent<ItemSpawn>().RemoveEnemy(0);
                    isAddEnemy = true;
                }
            }
            else if(latestIndex < 26)
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
                portal.SetActive(true);
                if(isCraftTell == true)
                {
                    GoMessage();
                }
            }
            else if(latestIndex == 35)
            {
                ActiveImage(8);
                GoMessage();
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
                GoMessage();
            }
            else if(latestIndex == 39)
            {
                image.enabled = false;
                GoMessage();
            }
            else if(latestIndex == 43)
            {
                canvas.SetActive(false);
                if (isReturnBase)
                {
                    canvas.SetActive(true);
                    GoMessage();
                }
            }
            else if(latestIndex == 46)
            {
                canvas.SetActive(false);
                GoMessage();
            }

            else if(latestIndex < 46)
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
            else if(latestIndex < 51)
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
        
        Debug.Log(latestIndex);
    }

    public void GameStart()
    {
        isStart = true;
        toggle.gameObject.transform.root.gameObject.SetActive(false);
        uiHelper.SetActive(false);
    }
}
