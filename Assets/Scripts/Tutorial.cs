using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    [SerializeField]
    private List<string> messsage = new List<string>
    {   "�𓀊���",
        "���͂悤�������܂�",
        "���Ȃ̃R�[���h�X���[�v�ɂ��A�L����Q���������Ă���\��������̂�",
        "���̐��E�ɂ��Čy���������܂�",

        //���ŋ�
        "�́X�A�l�X�͕��a�ɕ�炵�Ă��܂���",
        "������F������n���O�����̂�����Ă��܂���",
        "���̐����́u���̐��̕����𖕏�����v�Ƃ����A�l�ނɍU�����J�n���܂���",
        "�l�ނ͂��̈��|�I�ȍU���ɑ΍R�ł�����99.9%�����ł��܂���",
        "���Ȃ��͎����i�K�̃R�[���h�X���[�v���u�ɂ���Đ������т܂���",

        "���ݒn��ł͐N���҂������������@�B�u�f�R���v�ɂ���ĕ����̕����������i�߂��Ă��܂�",
        "�����ł��Ȃ��ɂ̓f�R���̓��������˂āA�u�R���T�[�o�[�v�Ƃ��Đl�ނ̕�����ۑ����Ă��������܂�",
        "�f�[�^��ۑ����邽�߂ɂ́A�܂��n��Ɏc���Ă���l�H���A���Ȃ킿�u�╨�v��������ɍs���K�v������܂�",
        "���̂�����ɂ��鋅�̂������ȃu���b�N�z�[���𐶐����Ĉ╨���z�����A�f�[�^�����Ă���܂�",
        "�������u���b�N�z�[���̐����ɂ͎��Ԃ�������܂�",
        "���Ȃ��ɂ͂��̊ԁA�u���b�N�z�[�����f�R���������Ă��������܂�",

        "�ł͂܂������ɂ��鋅�̂�͂�ŁA�╨�����肻���ȏꏊ�ɓ����Ă݂܂��傤",
        //�͂ޑ������

        "���̋��̂̒��e�n�_�Ƀe���|�[�g�ł���悤�ɂȂ�̂ŁA�����čs�����������݂͂Ȃ���e���|�[�g���Ă݂܂��傤",
        //�e���|�[�g�{�^������

        "�e���|�[�g�����ł�",
        "�u���b�N�z�[���͂��Ȃ��̐^��ɐ�������Ă����܂�",
        "�����f�R���̔������߂Â��Ă��܂�",
        "�����Ă��錕��U���Đ킢�܂��傤",
        "",
        //HP����
        //�퓬

        "�╨�̉�����������܂���",
        "�������U�����\�ȃf�R�����o�������̂ŁA�}���ŋ��̂�͂�ŋ��_�Ƀe���|�[�g���Ă�������",
        //���_�A�Ґ���

        "����ꂳ�܂ł���",
        "�ł͓��肵���╨���m�F�ɍs���܂��傤",
        "�����ɂ�����̂ɐG���B�{�^���������ƃf�[�^�G���A�Ƀe���|�[�g���܂�",
        //�f�[�^�G���A�Ƀe���|�[�g

        "�����ɂ͉�������╨���]������Ă��܂�",
        "�������╨�͂��ׂăf�[�^�����ĕ����\�Ȃ̂ŁA����������̂͗L�����p���܂��傤",
        "�����ł͕����A�C�e�����N���t�g���邱�Ƃ��ł��܂�",
        "�܂��͂�����ɗ����Ă���╨���E���Ă݂܂��傤",
        "�╨�̏㕔�ɕ\������Ă���͓̂���\�f�ނł�",
        "���ٕ̈��𓯂��F�̃G���A�ɓ��������ƕ\������Ă���f�ނ����ł��܂�",
        "���̃^�u���b�g�[���ɃN���t�g�ł�����̂ƕK�v�f�ނ������Ă���܂�",
        "�܂��͐���̐퓬�Ō�������HP���񕜂��邽�߂ɁA���ˊ������Ă݂܂��傤",
        //���ˊ�̍쐬
        //�N���t�g����

        "�ł͉񕜂��Ă݂܂��傤",
        //�񕜐���

        "���ɉ������̃f�R���ɑ΍R���邽�߂Ɍ��e�ƃ}�K�W�����쐬���܂��傤",
        //�N���t�g

        "�f�[�^�G���A�ł͒e��������Ɏ����ł������邱�Ƃ��ł��܂�",
        "�ł͋��_�ɖ߂�܂��傤",
        //���_�Ƀe���|�[�g

        "���̂�����̈╨�͎��̉���ōŌ�̂悤�ł�",
        "������x�╨�̉���Ɍ������܂��傤",
        //�t�B�[���h�Ƀe���|�[�g

        "�f�R���̔������ُ�ɏW�����Ă��܂�",
        "�댯�ł����A���Ȃ��Ȃ�ς�����͂��ł�",
        "�����^��",
        //�퓬
        //�A��

        "�������ł�",
        "���Ȃ��͂�����l�O�̃R���T�[�o�[�ł�",
        "���͂����x�݂܂��傤",
        "����ꂳ�܂ł���"
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
    private GameObject Rhand;

    [SerializeField]
    private GameObject Lhand;

    private static bool isGround;

    private static bool isRunning;

    void Start()
    {
        text.text = messsage[0];
    }

    void Update()
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
                GunCraft();
                break;
            default:
                break;
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
                if(isRunning == false)
                {
                    canvas.SetActive(true);
                    GoMessage();
                }
            }
            else if(latestIndex >= 22)
            {
                returnBaseCount = 1;
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

    }

    private void GunCraft()
    {

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
        Debug.Log(ir);
        isRunning = ir;
    }

    private void ActiveImage(int count)
    {
        image.enabled = true;
        image.sprite = sprites[count];
    }

    private void GoMessage()
    {
        timer = 0;
        interval = messsage[latestIndex].Length / 20;
        latestIndex++;
    }
}
