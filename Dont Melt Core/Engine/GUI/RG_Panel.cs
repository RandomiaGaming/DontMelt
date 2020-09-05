using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class RG_Panel : MonoBehaviour
{
    private RG_Element Controller_Target;
    public RG_Element Default_Element;
    private float Controller_Timer = 0;

    public bool Controller_Selected(RG_Element Target_Element)
    {
        if (UI_Manager.Instance.Controller_Input)
        {
            if (Target_Element == Controller_Target)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    void Start()
    {
        Input.simulateMouseWithTouches = false;
        Application.targetFrameRate = int.MaxValue;
        Controller_Target = Default_Element;
        foreach (RG_Element Child in GetComponentsInChildren<RG_Element>())
        {
            Child.Parent_Panel = this;
        }
    }

    private void Update()
    {
        if (Input_Manager.Down_Held())
        {
            Controller_Timer -= Time.deltaTime;
            if (Controller_Timer <= 0 && Controller_Target.Move_Down() != null)
            {
                Controller_Timer = 0.25f;
                Controller_Target = Controller_Target.Move_Down();
            }
        }
        else if (Input_Manager.Up_Held())
        {
            Controller_Timer -= Time.deltaTime;
            if (Controller_Timer <= 0 && Controller_Target.Move_Up() != null)
            {
                Controller_Timer = 0.25f;
                Controller_Target = Controller_Target.Move_Up();
            }
        }
        else
        {
            Controller_Timer = 0;
        }
    }
}