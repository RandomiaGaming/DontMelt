using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public sealed class RG_Slider : RG_Element
{
    public int Value = 50;
    private float Controller_Offset = 0;
    public string Text = "Volume: <value>%";

    private Text TX;
    private Image Mask;
    private Image Mask_Graphic;
    private Image IM;
    private RectTransform RT;

    private new void Start()
    {
        base.Start();
        Mask_Graphic = transform.GetChild(0).GetChild(0).GetComponent<Image>();
        RT = GetComponent<RectTransform>();
        IM = GetComponent<Image>();
        TX = transform.GetChild(1).GetComponent<Text>();
        Mask = transform.GetChild(0).GetComponent<Image>();
    }
    private void Update()
    {
        if (UI_Manager.Instance.Controller_Input)
        {
            if (Parent_Panel.Controller_Selected(this))
            {
                IM.color = new Color(1, 0.333f, 0, 1);
                Mask_Graphic.color = new Color(1, 0, 0, 1);
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    Controller_Offset += Input_Manager.Move_Axis() * Time.deltaTime * 10;
                }
                else
                {
                    Controller_Offset += Input_Manager.Move_Axis() * Time.deltaTime * 50;
                }

                Value += (int)Controller_Offset;
                Controller_Offset -= (int)Controller_Offset;
            }
            else
            {
                IM.color = new Color(1, 0.666f, 0, 1);
                Mask_Graphic.color = new Color(1, 0.333f, 0, 1);
            }
        }
        else
        {
            Vector2 Mouse_Pos = Input.mousePosition;
            Vector2 min = Camera.main.WorldToScreenPoint(RT.TransformPoint(RT.rect.min));
            Vector2 max = Camera.main.WorldToScreenPoint(RT.TransformPoint(RT.rect.max));
            if (Mouse_Pos.x >= min.x && Mouse_Pos.x <= max.x && Mouse_Pos.y > min.y && Mouse_Pos.y < max.y)
            {
                IM.color = new Color(1, 0.333f, 0, 1);
                Mask_Graphic.color = new Color(1, 0, 0, 1);
            }
            else
            {
                IM.color = new Color(1, 0.666f, 0, 1);
                Mask_Graphic.color = new Color(1, 0.333f, 0, 1);
            }
        }

        Value = Mathf.Clamp(Value, 0, 100);
        Mask.fillAmount = Value / 100f;
        TX.text = Text.Replace("<value>", Value.ToString());
    }
}