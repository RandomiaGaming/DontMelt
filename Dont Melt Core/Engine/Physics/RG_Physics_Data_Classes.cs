using DontMelt;
public sealed class RG_Collision
{
    public RG_Collider Other_Collider = null;
    public RG_Game_Object Other_Game_Object = null;
    public RG_Side_Info Side_Info = null;
    private RG_Collision() { }
    public static RG_Collision Create()
    {
        return new RG_Collision();
    }
    public RG_Collision Clone()
    {
        return Create(Other_Collider, Other_Game_Object, Side_Info.Clone());
    }
    public static RG_Collision Create(RG_Collider Other_Collider, RG_Game_Object Other_Game_Object, RG_Side_Info Side_Info)
    {
        RG_Collision Output = new RG_Collision();
        Output.Other_Collider = Other_Collider;
        Output.Other_Game_Object = Other_Game_Object;
        Output.Side_Info = Side_Info;
        return Output;
    }
}

public sealed class RG_Trigger_Overlap
{
    public RG_Collider Other_Collider = null;
    public RG_Game_Object Other_Game_Object = null;
    private RG_Trigger_Overlap() { }
    public static RG_Trigger_Overlap Create()
    {
        return new RG_Trigger_Overlap();
    }
    public RG_Trigger_Overlap Clone()
    {
        return Create(Other_Collider, Other_Game_Object);
    }
    public static RG_Trigger_Overlap Create(RG_Collider Other_Collider, RG_Game_Object Other_Game_Object)
    {
        RG_Trigger_Overlap Output = new RG_Trigger_Overlap();
        Output.Other_Collider = Other_Collider;
        Output.Other_Game_Object = Other_Game_Object;
        return Output;
    }
}

public sealed class RG_Side_Info
{
    public bool Top = false;
    public bool Bottom = false;
    public bool Left = false;
    public bool Right = false;

    private RG_Side_Info() { }
    public static RG_Side_Info Create()
    {
        return new RG_Side_Info();
    }
    public RG_Side_Info Clone()
    {
        return Create(Top, Bottom, Left, Right);
    }
    public static RG_Side_Info Create(bool Top, bool Bottom, bool Left, bool Right)
    {
        RG_Side_Info Output = new RG_Side_Info();
        Output.Top = Top;
        Output.Bottom = Bottom;
        Output.Left = Left;
        Output.Right = Right;
        return Output;
    }
}