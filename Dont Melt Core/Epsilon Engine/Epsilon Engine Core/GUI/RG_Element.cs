using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
[RequireComponent(typeof(Image))]
public class RG_Element : MonoBehaviour
{
    private RG_Element Up;
    private RG_Element Down;
    [HideInInspector] public RG_Panel Parent_Panel;

    public RG_Element Move_Up()
    {
        return Up;
    }

    public RG_Element Move_Down()
    {
        return Down;
    }

    protected void Start()
    {
        if (transform.parent != null)
        {
            int Index = -1;
            for (int i = 0; i < transform.parent.childCount; i++)
            {
                if (transform.parent.GetChild(i).gameObject == gameObject)
                {
                    Index = i;
                }
            }

            if (Index == 0)
            {
                Up = transform.parent.GetChild(transform.parent.childCount - 1).GetComponent<RG_Element>();
            }
            else
            {
                Up = transform.parent.GetChild(Index - 1).GetComponent<RG_Element>();
            }

            if (Index == transform.parent.childCount - 1)
            {
                Down = transform.parent.GetChild(0).GetComponent<RG_Element>();
            }
            else
            {
                Down = transform.parent.GetChild(Index + 1).GetComponent<RG_Element>();
            }
        }
    }
}