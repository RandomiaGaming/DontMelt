using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public sealed class RG_Button : RG_Element
{
    public UnityEvent On_Click;
    private Image TG;
    private RectTransform RT;

    private new void Start()
    {
        base.Start();
        RT = GetComponent<RectTransform>();
        TG = GetComponent<Image>();
    }

    private void Update()
    {
        if (UI_Manager.Instance.Controller_Input)
        {
            if (Parent_Panel.Controller_Selected(this))
            {
                if (Input_Manager.Submit_Up())
                {
                    On_Click.Invoke();
                }

                if (Input_Manager.Submit_Held())
                {
                    TG.color = new Color(1, 0, 0, 1);
                }
                else
                {
                    TG.color = new Color(1, 0.333f, 0, 1);
                }
            }
            else
            {
                TG.color = new Color(1, 0.666f, 0, 1);
            }
        }
        else
        {
            Vector2 Mouse_Pos = Input.mousePosition;
            Vector2 min = Camera.main.WorldToScreenPoint(RT.TransformPoint(RT.rect.min));
            Vector2 max = Camera.main.WorldToScreenPoint(RT.TransformPoint(RT.rect.max));
            if (Mouse_Pos.x >= min.x && Mouse_Pos.x <= max.x && Mouse_Pos.y > min.y && Mouse_Pos.y < max.y)
            {
                if (Input.GetMouseButtonUp(0))
                {
                    On_Click.Invoke();
                }

                if (Input.GetMouseButton(0))
                {
                    TG.color = new Color(1, 0, 0, 1);
                }
                else
                {
                    TG.color = new Color(1, 0.333f, 0, 1);
                }
            }
            else
            {
                TG.color = new Color(1, 0.666f, 0, 1);
            }
        }
    }
}