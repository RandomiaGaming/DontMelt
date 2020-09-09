using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class Error_Manager : MonoBehaviour
{
    private static string Error_Message = "";
    public static bool Showing_Fatal = false;
    public static bool Showing_Error = false;

    public GameObject Error_Panel = null;
    public Text Error_Text = null;

    private void Update()
    {
        if (Error_Text != null)
        {
            Error_Text.text = Error_Message;
        }

        if (Showing_Error || Showing_Fatal)
        {
            Error_Panel.SetActive(true);
        }

        if (Input.anyKeyDown)
        {
            Showing_Error = false;
            if (Showing_Fatal)
            {
                Showing_Fatal = false;
                Process.GetCurrentProcess().Kill();
            }
        }
    }

    public static void ShowError(string Message, bool Fatal)
    {
        Error_Message = Message;
        Showing_Fatal = Fatal;
        Showing_Error = true;
    }
}