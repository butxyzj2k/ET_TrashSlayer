using UnityEngine;

public class ConfigGameManager : MonoBehaviour
{
    void Start()
    {
       
        //Giới hạn FPS
        if(Screen.currentResolution.refreshRate >= 90) Application.targetFrameRate = 90;
        else if(Screen.currentResolution.refreshRate >= 60) Application.targetFrameRate = 60;
        else if(Screen.currentResolution.refreshRate >= 30) Application.targetFrameRate = 30;

        // Đặt độ phân giải tối đa là 1920x1080
        int maxWidth = 1920;
        int maxHeight = 1080;

        // Kiểm tra và giới hạn độ phân giải nếu màn hình lớn hơn 1920x1080
        if (Screen.width > maxWidth || Screen.height > maxHeight)
        {
            Screen.SetResolution(maxWidth, maxHeight, Screen.fullScreen);
        }
    }
}
