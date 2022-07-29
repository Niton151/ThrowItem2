using UnityEngine;
using UnityEngine.UI;
using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Debug.Log()��UI.Text�ɕ\��
/// </summary>
public class CatchLog : MonoBehaviour
{
    private Text text;
    private StringBuilder builder = new StringBuilder();
    private bool autoScroll = true;

    [SerializeField, Tooltip("�e�L�X�g�̐擪�Ɏ�����������")]
    private bool useTimeStamp = true;

    [SerializeField, Tooltip("���O�̎�ʂɉ����ĐF��t����")]
    private bool coloredByLogType = true;

    [SerializeField, Tooltip("����̕������܂ރ��O�͕�����Ȃ�")]
    private string[] ignore = new string[] { "[OVR" };

    private void Awake()
    {
        text = this.GetComponent<Text>();
        if (text == null)
        {
            this.enabled = false;
            throw new NullReferenceException("No text component found.");
        }

        if (autoScroll)
            text.verticalOverflow = VerticalWrapMode.Truncate;

        if (coloredByLogType)
            text.supportRichText = true;

        text.text = string.Empty;
    }

    private void OnEnable()
    {
        Application.logMessageReceived += HandleLog;
        builder = new StringBuilder();
    }

    private void OnDisable()
    {
        Application.logMessageReceived -= HandleLog;
        builder = null;
    }

    private void HandleLog(string logText, string stackTrace, LogType logType)
    {
        builder.Clear();

        if (0 < ignore.Length)
        {
            for (int i = 0; i < ignore.Length; i++)
            {
                if (ignore[i] != string.Empty && logText.Contains(ignore[i]))
                    return;
            }
        }

        if (useTimeStamp)
            builder.Append(string.Format("[{0}:{1:D3}] ", DateTime.Now.ToLongTimeString(), DateTime.Now.Millisecond));

        if (coloredByLogType)
        {
            switch (logType)
            {
                case LogType.Assert:
                case LogType.Warning:
                    logText = GetColoredString(logText, "yellow");
                    break;
                case LogType.Error:
                case LogType.Exception:
                    logText = GetColoredString(logText, "red");
                    break;
                default:
                    break;
            }
        }

        builder.Append(logText);
        builder.Append(Environment.NewLine);

        text.text += builder.ToString();

        if (autoScroll && text.verticalOverflow == VerticalWrapMode.Truncate)
            AdjustText(text);
    }

    /// <summary>
    /// ������ɐF�t��
    /// </summary>
    /// <param name="src"></param>
    /// <param name="color"></param>
    /// <returns></returns>
    private string GetColoredString(string src, string color)
    {
        return string.Format("<color={0}>{1}</color>", color, src);
    }

    /// <summary>
    /// Text�͈͓̔�ɕ��������߂�
    /// </summary>
    /// <param name="t"></param>
    private void AdjustText(Text t)
    {
        TextGenerator generator = t.cachedTextGenerator;
        var settings = t.GetGenerationSettings(t.rectTransform.rect.size);
        generator.Populate(t.text, settings);

        int countVisible = generator.characterCountVisible;
        if (countVisible == 0 || t.text.Length <= countVisible)
            return;

        int truncatedCount = t.text.Length - countVisible;
        var lines = t.text.Split('\n');
        foreach (string line in lines)
        {
            // ���؂�Ă��镶������0�ɂȂ�܂ŁA�e�L�X�g�̐擪�s��������Ă䂭
            t.text = t.text.Remove(0, line.Length + 1);
            truncatedCount -= (line.Length + 1);
            if (truncatedCount <= 0)
                break;
        }
    }
}