using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using UnityEngine;

public static class Save_Data_Manager
{
    public static Settings_Data Current_Settings = new Settings_Data();

    public static string Get_Json(object Original)
    {
        MemoryStream memoryStream = new MemoryStream();
        DataContractJsonSerializer JsonSerializer = new DataContractJsonSerializer(Original.GetType());
        JsonSerializer.WriteObject(memoryStream, Original);
        return Encoding.ASCII.GetString(memoryStream.ToArray());
    }

    public static object Get_Object(string Json, Type TypeOfOriginal)
    {
        try
        {
            byte[] JsonToBytes = Encoding.ASCII.GetBytes(Json);
            MemoryStream memoryStream = new MemoryStream(JsonToBytes);
            DataContractJsonSerializer ser1 = new DataContractJsonSerializer(TypeOfOriginal);
            object Output = ser1.ReadObject(memoryStream);
            return Output;
        }
        catch
        {
            return null;
        }
    }

    public static void Write_To_Disk(object Original, string SubPath)
    {
        if (SubPath != "")
        {
            FileStream stream = new FileStream(Application.persistentDataPath + SubPath, FileMode.Create);
            StreamWriter streamWriter = new StreamWriter(stream);
            streamWriter.Write(Get_Json(Original));
            streamWriter.Dispose();
            stream.Close();
        }
    }

    public static object Read_From_Disk(string SubPath, Type TypeOfOriginal)
    {
        if (File.Exists(Application.persistentDataPath + SubPath))
        {
            FileStream stream = new FileStream(Application.persistentDataPath + SubPath, FileMode.Open);
            StreamReader streamReader = new StreamReader(stream);
            string fileJson = streamReader.ReadToEnd();
            streamReader.Dispose();
            stream.Close();
            return Get_Object(fileJson, TypeOfOriginal);
        }
        else
        {
            return null;
        }
    }

    public static void Save_Settings()
    {
        Current_Settings.Clean();
        Write_To_Disk(Current_Settings, "/Settings.DMSettings");
    }

    public static void Load_Settings()
    {
        object LoadedSettings = Read_From_Disk("/Settings.DMSettings", typeof(Settings_Data));
        if (LoadedSettings != null && LoadedSettings.GetType() == typeof(Settings_Data))
        {
            Current_Settings = (Settings_Data) LoadedSettings;
        }
        else
        {
            Current_Settings = new Settings_Data();
        }

        Current_Settings.Clean();
    }

    public static Stage_Data Load_Stage(string File_Name)
    {
        Directory.CreateDirectory(Application.persistentDataPath + "/EditorStages");
        object LoadedStage = Read_From_Disk($"/EditorStages/{File_Name}.DMStage", typeof(Stage_Data));
        if (LoadedStage != null && LoadedStage.GetType() == typeof(Stage_Data))
        {
            Stage_Data Output = (Stage_Data) LoadedStage;
            Output.Clean();
            return Output;
        }
        else
        {
            return null;
        }
    }

    public static void Save_Stage(Stage_Data Stage)
    {
        Directory.CreateDirectory(Application.persistentDataPath + "/Editor Stages");
        Stage.Clean();
        Write_To_Disk(Stage, $"/Editor Stages/{Stage.Display_Name}.DMStage");
    }

    public static int Editor_Stage_Count()
    {
        Directory.CreateDirectory(Application.persistentDataPath + "/Editor Stages");
        return Directory.GetFiles(Application.persistentDataPath + "/Editor Stages").Length;
    }

    public static void Delete_From_Disk(string SubPath)
    {
        if (File.Exists($"{Application.persistentDataPath}/{SubPath}"))
        {
            File.Delete($"{Application.persistentDataPath}/{SubPath}");
        }
    }
}

public class Settings_Data
{
    public string Hashed_Password = "";
    public string Display_Name = "";
    public string Email = "";

    public List<string> Item_History = new List<string>() { };
    public List<Editor_Tool> Tool_History = new List<Editor_Tool>() { };

    public int GameVolume = 50;

    public void Clean()
    {
        GameVolume = Mathf.Clamp(GameVolume, 0, 100);
        Item_History = Item_History.Distinct().ToList();
        if (Item_History == null)
        {
            Item_History = new List<string>();
        }
    }
}

public struct Tile_Data
{
    public string Item_Name;
    public string SID;
    public Vector2Int Position;

    public Tile_Data(string Item_Name, Vector2Int Position, string SID)
    {
        this.Item_Name = Item_Name;
        this.Position = Position;
        this.SID = SID;
    }
}

public class Stage_Data
{
    public string USID = "";
    public string Display_Name = "";

    public Vector2Int Player_Pos = new Vector2Int(0, 0);
    public Vector2Int Goal_Pos = new Vector2Int(5, 0);
    public List<Tile_Data> Tile_Data = new List<Tile_Data>();

    public bool Equals(Stage_Data Other_Stage)
    {
        if (Other_Stage == null)
        {
            return false;
        }
        else
        {
            if (Player_Pos != Other_Stage.Player_Pos || Tile_Data.Count != Other_Stage.Tile_Data.Count ||
                Goal_Pos != Other_Stage.Goal_Pos)
            {
                return false;
            }

            for (int i = 0; i < Tile_Data.Count; i++)
            {
                if (Tile_Data[i].Item_Name != Other_Stage.Tile_Data[i].Item_Name ||
                    Tile_Data[i].Position != Other_Stage.Tile_Data[i].Position ||
                    Tile_Data[i].SID != Other_Stage.Tile_Data[i].SID)
                {
                    return false;
                }
            }

            return true;
        }
    }

    public Stage_Data Clone()
    {
        Stage_Data Clone = new Stage_Data();
        Clone.Display_Name = Display_Name;
        Clone.Player_Pos = Player_Pos;
        Clone.Goal_Pos = Goal_Pos;
        Clone.Tile_Data = new List<Tile_Data>(Tile_Data);
        return Clone;
    }

    public void Clean()
    {
        if (Player_Pos == Goal_Pos)
        {
            Player_Pos = new Vector2Int(0, 0);
            Goal_Pos = new Vector2Int(5, 1);
        }

        List<Vector2Int> Occupied_Positions = new List<Vector2Int>() {Player_Pos, Goal_Pos};
        List<Tile_Data> Cleaned_Tile_Data = new List<Tile_Data>();
        foreach (Tile_Data dat in Tile_Data)
        {
            bool Occupied = false;
            foreach (Vector2Int pos in Occupied_Positions)
            {
                if (pos == dat.Position)
                {
                    Occupied = true;
                }
            }

            if (!Occupied)
            {
                Occupied_Positions.Add(dat.Position);
                Cleaned_Tile_Data.Add(dat);
            }
        }

        Tile_Data = new List<Tile_Data>(Cleaned_Tile_Data);
    }

    public void Set_Tile(Vector2Int position, string NewItem)
    {
        if (Player_Pos != position && Goal_Pos != position)
        {
            Delete_Tile(position);
            Tile_Data.Add(new Tile_Data(NewItem, position, ""));
            Clean();
        }
    }

    public void Delete_Tile(Vector2Int position)
    {
        for (int i = 0; i < Tile_Data.Count; i++)
        {
            if (Tile_Data[i].Position == position)
            {
                Tile_Data.RemoveAt(i);
                i--;
            }
        }

        Clean();
    }

    public string Get_Tile(Vector2Int position)
    {
        Clean();
        if (Player_Pos == position)
        {
            return "Player";
        }
        else if (Goal_Pos == position)
        {
            return "GoalGate";
        }

        foreach (Tile_Data dat in Tile_Data)
        {
            if (dat.Position == position)
            {
                return dat.Item_Name;
            }
        }

        return "None";
    }
}