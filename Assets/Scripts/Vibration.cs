using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Vibration
{
    /// <summary>
    /// Oculus Quest(やQuest2)のコントローラーを振動させる
    /// </summary>
    public static IEnumerator Vibrate(float duration = 0.1f, float frequency = 0.1f, float amplitude = 0.1f, OVRInput.Controller controller = OVRInput.Controller.Active) {
        //コントローラーを振動させる
        OVRInput.SetControllerVibration(frequency, amplitude, controller);

        //指定された時間待つ
        yield return new WaitForSeconds(duration);

        //コントローラーの振動を止める
        OVRInput.SetControllerVibration(0, 0, controller);
    }
}
