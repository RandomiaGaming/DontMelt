using System.Collections.Generic;

namespace DontMelt
{
    public enum Editor_Tool
    {
        Paint,
        Fill,
        Grid_Fill,
        Selection_Paint,
        Selection_Fill,
        Selection_Grid_Fill,
        Move,
        Pick,
        Pan,
    }
    public class StageEditor : Item
    {
        public static List<Stage_Data> History_Data;
        public static Stage_Data Current_Data;
        public static Stage_Data Preview_Data;

        private Editor_Tool Current_Tool = Editor_Tool.Paint;

        public StageGenerator SG;

        void Update()
        {
            NullCheck();

            RefreshCurrentAction();
            RefreshMouseDraw();
            RefreshMouseBoxFill();
            RefreshMousePan();
            RefreshMouseScroll();
            RefreshTouchDraw();
            RefreshTouchBoxFill();
            RefreshDoubleTouch();
            RefreshTouchMove();
            RefreshMouseMove();

            RefreshUndoCount();

            if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Z))
            {
                Undo();
            }

            if (CurrentAction == EditorAction.None || CurrentAction == EditorAction.DoubleTouch ||
                CurrentAction == EditorAction.MousePan || CurrentAction == EditorAction.MouseScroll)
            {
                SG.CurrentStageData = Current_Data.Clone();
            }
            else
            {
                SG.CurrentStageData = Preview_Data.Clone();
            }
        }

        private void RefreshUndoCount()
        {
            if (History_Data == null)
            {
                History_Data = new List<Stage_Data>();
            }

            while (History_Data.Count > 20)
            {
                History_Data.RemoveAt(History_Data.Count - 1);
            }
        }

        private Vector2Int MMStartPos;

        private void RefreshMouseMove()
        {
            if (!SaveDataHelper.Current_Settings.SelectionMode)
            {
                return true;
            }

            if (Data.Get_Tile(Position) == "None")
            {
                return true;
            }

            if (Data.Get_Tile(Position) == SaveDataHelper.Current_Settings.itemHistory[0])
            {
                return true;
            }

            return false;
        }
        private bool PartOfSelection(Vector2Int Position, Stage_Data Data)
        {
            if (CurrentAction == EditorAction.MouseMove)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    OnActionStart();
                    MMStartPos = SG.WorldToGrid(Camera.main.ScreenToWorldPoint(Input.mousePosition));
                }
                else if (Input.GetMouseButton(0))
                {
                    OnActionStart();
                }
                else if (Input.GetMouseButtonUp(0))
                {
                    OnActionEnded();
                }
            }
        }

        private Vector2Int TMStartPos;

        private void RefreshTouchMove()
        {
            if (CurrentAction == EditorAction.TouchMove)
            {
                if (Input.touchCount >= 1 && Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    OnActionStart();
                    TMStartPos = SG.WorldToGrid(Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position));
                }
                else if (Input.touchCount >= 1 && Input.GetTouch(0).phase != TouchPhase.Ended)
                {
                    OnActionStart();
                }
                else if (Input.touchCount >= 1 && Input.GetTouch(0).phase == TouchPhase.Ended)
                {
                    OnActionEnded();
                }
            }
        }

        private float DTStartDistance = 0;
        private float DTStartZoom = 5;
        Vector2 DTStartTouch = Vector2.zero;
        Vector2 DTStartCam = Vector2.zero;

        private void RefreshDoubleTouch()
        {
            if (CurrentAction == EditorAction.DoubleTouch)
            {
                if (Input.touchCount >= 2 && Input.GetTouch(1).phase == TouchPhase.Began)
                {
                    DTStartDistance = Vector3.Distance(Input.GetTouch(0).position, Input.GetTouch(1).position);
                    DTStartZoom = Camera.main.orthographicSize;
                    DTStartTouch = new Vector2((Input.GetTouch(0).position.x + Input.GetTouch(1).position.x) / 2,
                        (Input.GetTouch(0).position.y + Input.GetTouch(1).position.y) / 2);
                    DTStartCam = Camera.main.transform.position;
                }
                else if (Input.touchCount >= 2)
                {
                    Camera.main.orthographicSize = DTStartZoom -
                                                   ((DTStartDistance - Vector3.Distance(Input.GetTouch(0).position,
                                                       Input.GetTouch(1).position)) * -0.025f);
                    Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, 40, 400);
                    Vector2 Offset = DTStartTouch -
                                     new Vector2((Input.GetTouch(0).position.x + Input.GetTouch(1).position.x) / 2,
                                         (Input.GetTouch(0).position.y + Input.GetTouch(1).position.y) / 2);
                    Offset.x *= (Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).x -
                                 Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0)).x) /
                                (0 - Screen.width);
                    Offset.y *= (Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).y -
                                 Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0)).y) /
                                (0 - Screen.height);
                    Camera.main.transform.position = new Vector3((DTStartCam + Offset).x, (DTStartCam + Offset).y, -10);
                }
                else
                {
                    CurrentAction = EditorAction.None;
                }
            }
        }

        private Vector2Int TBFStartPos;

        private void RefreshTouchBoxFill()
        {
            if (CurrentAction == EditorAction.TouchBoxFill)
            {
                if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    OnActionStart();
                    TBFStartPos = SG.WorldToGrid(Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position));
                }
                else if (Input.touchCount > 0)
                {
                    OnActionStart();
                    Vector2Int TBFCurrentPos = SG.WorldToGrid(Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position));
                    Vector2Int min = new Vector2Int(Mathf.Min(TBFStartPos.x, TBFCurrentPos.x),
                        Mathf.Min(TBFStartPos.y, TBFCurrentPos.y));
                    Vector2Int max = new Vector2Int(Mathf.Max(TBFStartPos.x, TBFCurrentPos.x),
                        Mathf.Max(TBFStartPos.y, TBFCurrentPos.y));
                    for (int x = min.x; x <= max.x; x++)
                    {
                        for (int y = min.y; y <= max.y; y++)
                        {
                            if (SaveDataHelper.Current_Settings.EraseMode)
                            {
                                if (PartOfSelection(new Vector2Int(x, y), Preview_Data))
                                {
                                    Preview_Data.Delete_Tile(new Vector2Int(x, y));
                                }
                            }
                            else
                            {
                                if (PartOfSelection(new Vector2Int(x, y), Preview_Data))
                                {
                                    Preview_Data.Set_Tile(new Vector2Int(x, y),
                                        SaveDataHelper.Current_Settings.itemHistory[0]);
                                }
                            }
                        }
                    }
                }
                else
                {
                    OnActionEnded();
                }
            }
        }

        private void RefreshTouchDraw()
        {
            if (CurrentAction == EditorAction.TouchDraw)
            {
                if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    OnActionStart();
                }
                else if (Input.touchCount > 0)
                {
                    if (SaveDataHelper.Current_Settings.EraseMode)
                    {
                        if (PartOfSelection(SG.WorldToGrid(Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position)),
                            Preview_Data))
                        {
                            Preview_Data.Delete_Tile(
                                SG.WorldToGrid(Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position)));
                        }
                    }
                    else
                    {
                        if (PartOfSelection(SG.WorldToGrid(Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position)),
                            Preview_Data))
                        {
                            Preview_Data.Set_Tile(
                                SG.WorldToGrid(Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position)),
                                SaveDataHelper.Current_Settings.itemHistory[0]);
                        }
                    }
                }
                else
                {
                    OnActionEnded();
                }
            }
        }

        private void RefreshMouseScroll()
        {
            if (CurrentAction == EditorAction.MouseScroll)
            {
                if (Input.GetAxis("MouseScrollWheel") != 0)
                {
                    Camera.main.orthographicSize += Input.GetAxis("MouseScrollWheel") * -0.25f;
                    Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, 2.5f, 25);
                }
                else
                {
                    CurrentAction = EditorAction.None;
                }
            }
        }

        private Vector3 MSStartMouse = Vector3.zero;
        private Vector3 MSStartCam = Vector3.zero;

        private void RefreshMousePan()
        {
            if (CurrentAction == EditorAction.MousePan)
            {
                if (Input.GetMouseButtonDown(2))
                {
                    MSStartMouse = Input.mousePosition;
                    MSStartCam = Camera.main.transform.position;
                }
                else if (Input.GetMouseButton(2))
                {
                    Vector3 Offset = MSStartMouse - Input.mousePosition;
                    Offset.x *= (Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).x -
                                 Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0)).x) /
                                (0 - Screen.width);
                    Offset.y *= (Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).y -
                                 Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0)).y) /
                                (0 - Screen.height);
                    Camera.main.transform.position = MSStartCam + Offset;
                }
                else if (Input.GetMouseButtonUp(2))
                {
                    SwitchAction(EditorAction.None);
                    MSStartMouse = Vector3.zero;
                    MSStartCam = Vector3.zero;
                }
            }
            else
            {
                MSStartMouse = Vector3.zero;
                MSStartCam = Vector3.zero;
            }
        }

        private Vector2Int MBFStartPos;

        private void RefreshMouseBoxFill()
        {
            if (CurrentAction == EditorAction.MouseBoxFill)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    OnActionStart();
                    MBFStartPos = SG.WorldToGrid(Camera.main.ScreenToWorldPoint(Input.mousePosition));
                }
                else if (Input.GetMouseButton(0))
                {
                    OnActionStart();
                    Vector2Int MBFCurrentPos = SG.WorldToGrid(Camera.main.ScreenToWorldPoint(Input.mousePosition));
                    Vector2Int min = new Vector2Int(Mathf.Min(MBFStartPos.x, MBFCurrentPos.x),
                        Mathf.Min(MBFStartPos.y, MBFCurrentPos.y));
                    Vector2Int max = new Vector2Int(Mathf.Max(MBFStartPos.x, MBFCurrentPos.x),
                        Mathf.Max(MBFStartPos.y, MBFCurrentPos.y));
                    for (int x = min.x; x <= max.x; x++)
                    {
                        for (int y = min.y; y <= max.y; y++)
                        {
                            if (SaveDataHelper.Current_Settings.EraseMode)
                            {
                                if (PartOfSelection(new Vector2Int(x, y), Preview_Data))
                                {
                                    Preview_Data.Delete_Tile(new Vector2Int(x, y));
                                }
                            }
                            else
                            {
                                if (PartOfSelection(new Vector2Int(x, y), Preview_Data))
                                {
                                    Preview_Data.Set_Tile(new Vector2Int(x, y),
                                        SaveDataHelper.Current_Settings.itemHistory[0]);
                                }
                            }
                        }
                    }
                }
                else if (Input.GetMouseButtonUp(0))
                {
                    OnActionEnded();
                }
            }
        }

        private void RefreshMouseDraw()
        {
            if (CurrentAction == EditorAction.MouseDraw)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    OnActionStart();
                }
                else if (Input.GetMouseButton(0))
                {
                    if (SaveDataHelper.Current_Settings.EraseMode)
                    {
                        if (PartOfSelection(SG.WorldToGrid(Camera.main.ScreenToWorldPoint(Input.mousePosition)),
                            Preview_Data))
                        {
                            Preview_Data.Delete_Tile(SG.WorldToGrid(Camera.main.ScreenToWorldPoint(Input.mousePosition)));
                        }
                    }
                    else
                    {
                        if (PartOfSelection(SG.WorldToGrid(Camera.main.ScreenToWorldPoint(Input.mousePosition)),
                            Preview_Data))
                        {
                            Preview_Data.Set_Tile(SG.WorldToGrid(Camera.main.ScreenToWorldPoint(Input.mousePosition)),
                                SaveDataHelper.Current_Settings.itemHistory[0]);
                        }
                    }
                }
                else if (Input.GetMouseButtonUp(0))
                {
                    OnActionEnded();
                }
            }
        }

        private void RefreshCurrentAction()
        {
            if (!SaveDataHelper.Current_Settings.BoxFillMode && Input.GetMouseButtonDown(0) &&
                !OverUI(Input.mousePosition) &&
                Current_Data.Tile_Deletable(SG.WorldToGrid(Camera.main.ScreenToWorldPoint(Input.mousePosition))))
            {
                SwitchAction(EditorAction.MouseDraw);
            }
            else if (SaveDataHelper.Current_Settings.BoxFillMode && Input.GetMouseButtonDown(0) &&
                     !OverUI(Input.mousePosition) &&
                     Current_Data.Tile_Deletable(SG.WorldToGrid(Camera.main.ScreenToWorldPoint(Input.mousePosition))))
            {
                SwitchAction(EditorAction.MouseBoxFill);
            }
            else if (Input.GetMouseButtonDown(0) && !OverUI(Input.mousePosition) &&
                     !Current_Data.Tile_Deletable(SG.WorldToGrid(Camera.main.ScreenToWorldPoint(Input.mousePosition))))
            {
                SwitchAction(EditorAction.MouseMove);
            }
            else if (Input.GetMouseButtonDown(2) && !OverUI(Input.mousePosition))
            {
                SwitchAction(EditorAction.MousePan);
            }
            else if (Input.GetAxis("MouseScrollWheel") != 0 && !OverUI(Input.mousePosition))
            {
                SwitchAction(EditorAction.MouseScroll);
            }
            else if (!SaveDataHelper.Current_Settings.BoxFillMode && Input.touchCount == 1 &&
                     Input.GetTouch(0).phase == TouchPhase.Began && !OverUI(Input.GetTouch(0).position) &&
                     Current_Data.Tile_Deletable(SG.WorldToGrid(Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position)))
            )
            {
                SwitchAction(EditorAction.TouchDraw);
            }
            else if (SaveDataHelper.Current_Settings.BoxFillMode && Input.touchCount == 1 &&
                     Input.GetTouch(0).phase == TouchPhase.Began && !OverUI(Input.GetTouch(0).position) &&
                     Current_Data.Tile_Deletable(SG.WorldToGrid(Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position)))
            )
            {
                SwitchAction(EditorAction.TouchBoxFill);
            }
            else if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began &&
                     !OverUI(Input.GetTouch(0).position) &&
                     !Current_Data.Tile_Deletable(
                         SG.WorldToGrid(Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position)))
            )
            {
                SwitchAction(EditorAction.TouchMove);
            }
            else if (Input.touchCount >= 2 && Input.GetTouch(1).phase == TouchPhase.Began &&
                     !OverUI(Input.GetTouch(0).position))
            {
                SwitchAction(EditorAction.DoubleTouch);
            }
        }

        private void OnActionStart()
        {
            Preview_Data = Current_Data.Clone();
        }

        private void OnActionEnded()
        {
            CreateUndoPoint();
            Current_Data = Preview_Data.Clone();
            Preview_Data = new Stage_Data();
            SwitchAction(EditorAction.None);
        }

        public void SwitchAction(EditorAction NewAction)
        {
            if (CurrentAction != NewAction)
            {
                Preview_Data = new Stage_Data();
            }

            CurrentAction = NewAction;
        }

        public void SwitchSelectedItem(string NewItem)
        {
            SaveDataHelper.Current_Settings.itemHistory =
                SaveDataHelper.Current_Settings.itemHistory.Distinct().ToList();
            SaveDataHelper.Current_Settings.itemHistory.Remove(NewItem);
            SaveDataHelper.Current_Settings.itemHistory.Insert(0, NewItem);
        }

        public void Undo()
        {
            if (History_Data.Count > 0)
            {
                Current_Data = History_Data[0].Clone();
                History_Data.RemoveAt(0);
                SwitchAction(EditorAction.None);
            }
        }

        public void CreateUndoPoint()
        {
            History_Data.Insert(0, Current_Data.Clone());
        }
    }
}