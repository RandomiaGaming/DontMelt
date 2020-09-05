using System.Collections.Generic;
using UnityEngine;

public static class Input_Manager
{
    public static bool Jump_Down()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space))
        {
            return true;
        }

        return false;
    }

    public static bool Submit_Held()
    {
        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Return))
        {
            return true;
        }

        return false;
    }

    public static bool Submit_Up()
    {
        if (Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.Return))
        {
            return true;
        }

        return false;
    }

    public static bool Right_Held()
    {
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            return true;
        }

        return false;
    }

    public static bool Left_Held()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            return true;
        }

        return false;
    }

    public static bool Up_Held()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            return true;
        }

        return false;
    }

    public static bool Down_Held()
    {
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            return true;
        }

        return false;
    }

    public static int Move_Axis()
    {
        if (Right_Held() && !Left_Held())
        {
            return 1;
        }
        else if (Left_Held() && !Right_Held())
        {
            return -1;
        }
        else
        {
            return 0;
        }
    }
}