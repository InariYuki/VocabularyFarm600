using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShotManager : MonoBehaviour
{

    private void ScreenShot()
    {
        string path=Application.streamingAssetsPath;
        ScreenCapture.CaptureScreenshot(path+$"/{Random.Range(1000,100000)}.png");
    }
}
