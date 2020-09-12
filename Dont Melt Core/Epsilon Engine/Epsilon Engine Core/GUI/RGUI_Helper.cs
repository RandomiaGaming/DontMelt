using UnityEngine;

public static class UI_Helper
{
    public static bool Controller_Input = true;
    private static Vector3 Last_Mouse_Pos;

    void Update()
    {
        if (Input_Manager.Up_Held() || Input_Manager.Down_Held() || Input_Manager.Left_Held() || Input_Manager.Right_Held() || Input_Manager.Submit_Held())
        {
            Controller_Input = true;
        }

        if (Input.touchCount > 0 || Input.GetMouseButton(0) || Input.GetMouseButton(1) || Mouse_Moved())
        {
            Controller_Input = false;
        }

        if (Controller_Input)
        {
            Cursor.visible = false;
        }
        else
        {
            Cursor.visible = true;
            Last_Mouse_Pos = Input.mousePosition;
        }
    }

    private bool Mouse_Moved()
    {
        if (Mathf.Max(Mathf.Abs(Input.mousePosition.x - Last_Mouse_Pos.x), Mathf.Abs(Input.mousePosition.y - Last_Mouse_Pos.y)) > (Screen.width + Screen.height) / 32)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}