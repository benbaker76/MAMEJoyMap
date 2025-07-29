using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Reflection;

namespace MAMEJoyMap
{
    public partial class frmMain : Form
    {
        private DirectInputManager _directInputManager = null;

        private MapNode[,] _mainMap = null;
        private MapNode[,] _keyMap = null;

        private Bitmap _mainBitmap = null;
        private Bitmap _gridBitmap = null;
        private Bitmap _mapBitmap = null;
        private Bitmap _keyBitmap = null;
        private Bitmap _marioBitmap = null;

        private Point _startPoint;
        private Point _endPoint;

        private MapNode[] _lastJoyValue = null;
        private MapValue _stickyValue = MapValue.Neutral;

        private bool _mouseDown = false;

        private MapNode _keyedMap = null;

        private Point _marioPoint;

        private string _fileName = null;
        private string _mapFolder = null;

        private string _map4WayDiagonal = "4444s8888..444458888.444555888.ss5.222555666.222256666.2222s6666.2222s6666";
        private string _map4WaySticky = "s8.4s8.44s8.4445";
        private string _map8Way = "7778...4445";

        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            var version = Assembly.GetExecutingAssembly().GetName().Version;

            this.Text = String.Format("{0} {1}", Application.ProductName, version.ToString(3));

            _directInputManager = new DirectInputManager(this);
            _directInputManager.OnJoyInput += new DirectInputManager.DIJoyButtonDownHandler(OnJoyInput);

            _mapFolder = Path.Combine(Application.StartupPath, "Maps");

            _mainMap = new MapNode[9, 9];
            _keyMap = new MapNode[3, 4];

            _mainBitmap = new Bitmap(217, 217);
            _gridBitmap = new Bitmap(217, 217);
            _mapBitmap = new Bitmap(217, 217);

            _keyBitmap = new Bitmap(73, 97);
            _marioBitmap = new Bitmap(73, 97);

            _marioPoint = new Point(25, 37);

            picMain.Image = _mainBitmap;
            picKey.Image = _keyBitmap;
            picMario.Image = _marioBitmap;

            _lastJoyValue = new MapNode[8];

            for (int i = 0; i < 8; i++)
                _lastJoyValue[i] = new MapNode(0, 0, ImageType.Neutral);

            for (int y = 0; y < 9; y++)
                for (int x = 0; x < 9; x++)
                    _mainMap[x, y] = new MapNode(x * 25, y * 25, ImageType.Neutral);

            int count = 0;

            for (int y = 0; y < 4; y++)
                for (int x = 0; x < 3; x++)
                    if (count < imageList1.Images.Count - 1)
                        _keyMap[x, y] = new MapNode(x * 25, y * 25, (ImageType)count++);

            using (Graphics g = Graphics.FromImage(_gridBitmap))
            {
                DrawGrayGrid(g);
                DrawBlackGrid(g);
            }

            DrawMap(true);
            DrawKey();
            DrawMario();
        }

        private void OnJoyInput(object sender, JoyEventArgs e)
        {
            Point p = new Point((int)(((double)(e.JoyState.X + 255) / 510.0) * 217.0) / 25, (int)(((double)(e.JoyState.Y + 255) / 510.0) * 217.0) / 25);

            if (_lastJoyValue != null)
                _lastJoyValue[e.JoyId].JoyPos = false;

            _mainMap[p.X, p.Y].JoyPos = true;
            bool sticky = (_mainMap[p.X, p.Y].MapValue == MapValue.Sticky);

            if (!sticky)
                _stickyValue = _mainMap[p.X, p.Y].MapValue;

            MapNode mapNode = (sticky ? _lastJoyValue[e.JoyId] : _mainMap[p.X, p.Y]);
            MapValue mapValue = (sticky ? _stickyValue : mapNode.MapValue);

            if (mapNode != _lastJoyValue[e.JoyId])
                toolStripStatusLabel1.Text = mapValue.ToString();

            _lastJoyValue[e.JoyId] = _mainMap[p.X, p.Y];

            if (TryMoveMario(mapValue))
                DrawMario();

            DrawMap(false);
        }

        private void DrawKey()
        {
            using (Graphics g = Graphics.FromImage(_keyBitmap))
            {
                g.Clear(Color.FromArgb(207, 215, 196));

                int count = 0;

                using (SolidBrush sb = new SolidBrush(Color.Yellow))
                {
                    for (int y = 0; y < 4; y++)
                        for (int x = 0; x < 3; x++)
                            if (count++ < imageList1.Images.Count - 1)
                                if (_keyMap[x, y].Selected)
                                    g.FillRectangle(sb, x * 24, y * 24, 24, 24);
                }

                using (Pen p = new Pen(Color.Black))
                {
                    for (int y = 0; y < 10; y++)
                    {
                        for (int x = 0; x < 4; x++)
                        {
                            g.DrawLine(p, x * 24, 0, x * 24, 217);
                            g.DrawLine(p, 0, y * 24, 217, y * 24);
                        }
                    }
                }

                count = 0;

                for (int y = 0; y < 4; y++)
                    for (int x = 0; x < 3; x++)
                        if (count < imageList1.Images.Count - 1)
                            g.DrawImageUnscaled(imageList1.Images[count++], x * 24 + 1, y * 24 + 1);
            }

            picKey.Invalidate();
        }

        private void DrawBackground(Graphics g)
        {
            g.Clear(Color.FromArgb(207, 215, 196));
        }

        private void DrawSelection(Graphics g)
        {
            using (SolidBrush sb = new SolidBrush(Color.Yellow))
            {
                for (int y = 0; y < 9; y++)
                    for (int x = 0; x < 9; x++)
                        if (_mainMap[x, y].Selected)
                            g.FillRectangle(sb, x * 24, y * 24, 24, 24);
            }
        }

        private void DrawJoyPos(Graphics g)
        {
            using (SolidBrush sb = new SolidBrush(Color.Red))
            {
                for (int y = 0; y < 9; y++)
                    for (int x = 0; x < 9; x++)
                        if (_mainMap[x, y].JoyPos)
                            g.FillRectangle(sb, x * 24, y * 24, 24, 24);
            }
        }

        private void DrawGrayGrid(Graphics g)
        {
            using (Pen p = new Pen(Color.FromArgb(192, 192, 192)))
            {
                for (int y = 0; y < 10; y++)
                {
                    for (int x = 0; x < 10; x++)
                    {
                        g.DrawLine(p, x * 24, 0, x * 24, 217);
                        g.DrawLine(p, 0, y * 24, 217, y * 24);
                    }
                }
            }
        }

        private void DrawBlackGrid(Graphics g)
        {
            using (Pen p = new Pen(Color.Black))
            {
                for (int y = 0; y < 4; y++)
                {
                    for (int x = 0; x < 4; x++)
                    {
                        g.DrawLine(p, x * 72, 0, x * 72, 217);
                        g.DrawLine(p, 0, y * 72, 217, y * 72);
                    }
                }
            }
        }

        private void DrawGrid(Graphics g)
        {
            g.DrawImageUnscaled(_gridBitmap, 0, 0);
        }

        private void DrawMap(Graphics g)
        {
            g.DrawImageUnscaled(_mapBitmap, 0, 0);
        }

        private void UpdateMap()
        {
            using (Graphics g = Graphics.FromImage(_mapBitmap))
            {
                g.Clear(Color.FromArgb(0, 0, 0, 0));

                for (int y = 0; y < 9; y++)
                    for (int x = 0; x < 9; x++)
                        if ((int)_mainMap[x, y].ImageType < imageList1.Images.Count - 1)
                            g.DrawImageUnscaled(imageList1.Images[(int)_mainMap[x, y].ImageType], x * 24 + 1, y * 24 + 1);
            }
        }

        private void DrawMap(bool updateMap)
        {
            using (Graphics g = Graphics.FromImage(_mainBitmap))
            {
                DrawBackground(g);
                DrawSelection(g);
                DrawJoyPos(g);
                DrawGrid(g);

                if (updateMap)
                    UpdateMap();

                DrawMap(g);
            }

            picMain.Invalidate();
        }

        private void DrawMario()
        {
            using (Graphics g = Graphics.FromImage(_marioBitmap))
            {
                g.Clear(Color.FromArgb(207, 215, 196));

                using (SolidBrush sb = new SolidBrush(Color.Red))
                    if (10 < imageList1.Images.Count)
                        g.DrawImageUnscaled(imageList1.Images[10], _marioPoint.X, _marioPoint.Y);
            }

            picMario.Invalidate();
        }

        private bool TryMoveMario(MapValue MapValue)
        {
            int mapValue = (int)MapValue;
            bool moveMario = false;

            if ((mapValue & (int)MapType.Up) > 0)
            {
                _marioPoint.Y -= 2;

                if (_marioPoint.Y >= 0)
                    moveMario = true;
                else
                    _marioPoint.Y = 74;
            }
            if ((mapValue & (int)MapType.Left) > 0)
            {
                _marioPoint.X -= 2;

                if (_marioPoint.X >= 0)
                    moveMario = true;
                else
                    _marioPoint.X = 50;
            }
            if ((mapValue & (int)MapType.Right) > 0)
            {
                _marioPoint.X += 2;

                if (_marioPoint.X <= 50)
                    moveMario = true;
                else
                    _marioPoint.X = 0;
            }
            if ((mapValue & (int)MapType.Down) > 0)
            {
                _marioPoint.Y += 2;

                if (_marioPoint.Y <= 74)
                    moveMario = true;
                else
                    _marioPoint.Y = 0;
            }

            return moveMario;
        }

        private void UpdateMainSelection()
        {
            Point startPoint = new Point(_startPoint.X < _endPoint.X ? _startPoint.X : _endPoint.X, _startPoint.Y < _endPoint.Y ? _startPoint.Y : _endPoint.Y);
            Point endPoint = new Point(_endPoint.X > _startPoint.X ? _endPoint.X : _startPoint.X, _endPoint.Y > _startPoint.Y ? _endPoint.Y : _startPoint.Y);

            Rectangle selectRect = new Rectangle(startPoint.X, startPoint.Y, endPoint.X - startPoint.X, endPoint.Y - startPoint.Y);

            bool drawMap = false;

            for (int y = 0; y < 9; y++)
            {
                for (int x = 0; x < 9; x++)
                {
                    if (selectRect.IntersectsWith(_mainMap[x, y].Rectangle))
                    {
                        if (!_mainMap[x, y].Selected)
                        {
                            _mainMap[x, y].Selected = true;
                            drawMap = true;
                        }
                    }
                    else
                    {
                        if (_mainMap[x, y].Selected)
                        {
                            _mainMap[x, y].Selected = false;
                            drawMap = true;
                        }
                    }
                }
            }

            if (drawMap)
                DrawMap(false);
        }

        private void UpdateMainSelectionName()
        {
            for (int y = 0; y < 9; y++)
            {
                for (int x = 0; x < 9; x++)
                {
                    if (_mainMap[x, y].Rectangle.Contains(_endPoint))
                        toolStripStatusLabel1.Text = _mainMap[x, y].ImageType.ToString();
                }
            }
        }

        private void UpdateKey(Point p)
        {
            int count = 0;
            _keyedMap = null;

            for (int y = 0; y < 4; y++)
            {
                for (int x = 0; x < 3; x++)
                {
                    if (count++ < imageList1.Images.Count - 1)
                    {
                        if (_keyMap[x, y].Rectangle.Contains(p))
                        {
                            _keyMap[x, y].Selected = true;
                            _keyedMap = _keyMap[x, y];

                            toolStripStatusLabel1.Text = _keyedMap.ImageType.ToString();
                        }
                        else
                            _keyMap[x, y].Selected = false;
                    }
                }
            }
        }

        private void ClearMap()
        {
            for (int y = 0; y < 9; y++)
                for (int x = 0; x < 9; x++)
                    _mainMap[x, y].ImageType = ImageType.Neutral;
        }

        private void OpenMapFile()
        {
            ClearMap();

            string[] iniFile = File.ReadAllLines(_fileName);

            for (int i = 0; i < iniFile.Length; i++)
            {
                if (iniFile[i].StartsWith("joystick_map"))
                {
                    MapStrToMap(iniFile[i].Replace("joystick_map", "").Trim());
                    break;
                }
            }
        }

        private ImageType MapValueToImageType(MapValue mapValue)
        {
            switch (mapValue)
            {
                case MapValue.UpLeft:
                    return ImageType.UpLeft;
                case MapValue.Up:
                    return ImageType.Up;
                case MapValue.UpRight:
                    return ImageType.UpRight;
                case MapValue.Left:
                    return ImageType.Left;
                case MapValue.Neutral:
                    return ImageType.Neutral;
                case MapValue.Right:
                    return ImageType.Right;
                case MapValue.DownLeft:
                    return ImageType.DownLeft;
                case MapValue.Down:
                    return ImageType.Down;
                case MapValue.DownRight:
                    return ImageType.DownRight;
                case MapValue.Sticky:
                    return ImageType.Sticky;
                default:
                    return ImageType.Neutral;
            }
        }

        private ImageType GetRowMirrorImage(MapValue mapValue)
        {
            int mapVal = (int)mapValue;
            int mapLeft = (int)MapType.Left;
            int mapRight = (int)MapType.Right;
            int mapUp = (int)MapType.Up;
            int mapDown = (int)MapType.Down;

            return MapValueToImageType((MapValue)((mapVal & (mapLeft | mapRight)) | ((mapVal & mapUp) << 1) | ((mapVal & mapDown) >> 1)));
        }

        private ImageType GetColumnMirrorImage(MapValue mapValue)
        {
            int mapVal = (int)mapValue;
            int mapUp = (int)MapType.Up;
            int mapDown = (int)MapType.Down;
            int mapLeft = (int)MapType.Left;
            int mapRight = (int)MapType.Right;

            return MapValueToImageType((MapValue)((mapVal & (mapUp | mapDown)) | ((mapVal & mapLeft) << 1) | ((mapVal & mapRight) >> 1)));
        }

        private void MapStrToMap(string strMap)
        {
            string[] strMapArray = strMap.Split(new char[] { '.' }, StringSplitOptions.None);

            MapFileToMap(strMapArray);

            DrawMap(true);
        }

        private void MapFileToMap(string[] strMap)
        {
            for (int y = 0; y < 9; y++)
            {
                if (y < strMap.Length)
                {
                    for (int x = 0; x < 9; x++)
                    {
                        if (strMap[y].Length == 0 && y > 0)
                        {
                            _mainMap[x, y].ImageType = _mainMap[x, y - 1].ImageType;

                            continue;
                        }

                        if (x < strMap[y].Length)
                        {
                            switch (strMap[y][x])
                            {
                                case (char)MapChar.UpLeft:
                                    _mainMap[x, y].ImageType = ImageType.UpLeft;
                                    break;
                                case (char)MapChar.Up:
                                    _mainMap[x, y].ImageType = ImageType.Up;
                                    break;
                                case (char)MapChar.UpRight:
                                    _mainMap[x, y].ImageType = ImageType.UpRight;
                                    break;
                                case (char)MapChar.Left:
                                    _mainMap[x, y].ImageType = ImageType.Left;
                                    break;
                                case (char)MapChar.Neutral:
                                    _mainMap[x, y].ImageType = ImageType.Neutral;
                                    break;
                                case (char)MapChar.Right:
                                    _mainMap[x, y].ImageType = ImageType.Right;
                                    break;
                                case (char)MapChar.DownLeft:
                                    _mainMap[x, y].ImageType = ImageType.DownLeft;
                                    break;
                                case (char)MapChar.Down:
                                    _mainMap[x, y].ImageType = ImageType.Down;
                                    break;
                                case (char)MapChar.DownRight:
                                    _mainMap[x, y].ImageType = ImageType.DownRight;
                                    break;
                                case (char)MapChar.Sticky:
                                    _mainMap[x, y].ImageType = ImageType.Sticky;
                                    break;
                            }
                        }
                        else
                        {
                            if (x < 5 && x > 0)
                                _mainMap[x, y].ImageType = _mainMap[x - 1, y].ImageType;
                            else
                                _mainMap[x, y].ImageType = GetColumnMirrorImage(_mainMap[8 - x, y].MapValue);
                        }
                    }
                }
                else
                {
                    for (int x = 0; x < 9; x++)
                    {
                        if (y < 5 && y > 0)
                            _mainMap[x, y].ImageType = _mainMap[x, y - 1].ImageType;
                        else
                            _mainMap[x, y].ImageType = GetRowMirrorImage(_mainMap[x, 8 - y].MapValue);
                    }
                }
            }
        }

        private string MapToString()
        {
            string strMap = null;

            for (int y = 0; y < 9; y++)
            {
                for (int x = 0; x < 9; x++)
                {
                    switch (_mainMap[x, y].ImageType)
                    {
                        case ImageType.UpLeft:
                            strMap += (char)MapChar.UpLeft;
                            break;
                        case ImageType.Up:
                            strMap += (char)MapChar.Up;
                            break;
                        case ImageType.UpRight:
                            strMap += (char)MapChar.UpRight;
                            break;
                        case ImageType.Left:
                            strMap += (char)MapChar.Left;
                            break;
                        case ImageType.Neutral:
                            strMap += (char)MapChar.Neutral;
                            break;
                        case ImageType.Right:
                            strMap += (char)MapChar.Right;
                            break;
                        case ImageType.DownLeft:
                            strMap += (char)MapChar.DownLeft;
                            break;
                        case ImageType.Down:
                            strMap += (char)MapChar.Down;
                            break;
                        case ImageType.DownRight:
                            strMap += (char)MapChar.DownRight;
                            break;
                        case ImageType.Sticky:
                            strMap += (char)MapChar.Sticky;
                            break;
                    }
                }

                strMap += '.';
            }

            return strMap;
        }

        private void SaveMapFile()
        {
            List<string> iniFile = new List<string>();

            if (File.Exists(_fileName))
                iniFile.AddRange(File.ReadAllLines(_fileName));

            string strMap = String.Format("joystick_map {0}", MapToString());
            bool found = false;

            for (int i = 0; i < iniFile.Count; i++)
            {
                if (iniFile[i].StartsWith("joystick_map"))
                {
                    iniFile[i] = strMap;
                    found = true;
                }
            }

            if (!found)
                iniFile.Add(strMap);

            File.WriteAllLines(_fileName, iniFile.ToArray());
        }

        private void picMain_MouseDown(object sender, MouseEventArgs e)
        {
            _startPoint = new Point(e.X, e.Y);
            _endPoint = new Point(e.X, e.Y);
            _mouseDown = true;

            UpdateMainSelection();
        }

        private void picMain_MouseMove(object sender, MouseEventArgs e)
        {
            _endPoint = new Point(e.X, e.Y);

            if (_mouseDown)
                UpdateMainSelection();
            else
                UpdateMainSelectionName();
        }

        private void picMain_MouseUp(object sender, MouseEventArgs e)
        {
            _endPoint = new Point(e.X, e.Y);
            _mouseDown = false;

            UpdateMainSelection();
        }

        private void picKey_Click(object sender, EventArgs e)
        {
            if (_keyedMap == null)
                return;

            for (int y = 0; y < 9; y++)
            {
                for (int x = 0; x < 9; x++)
                {
                    if (_mainMap[x, y].Selected)
                        _mainMap[x, y].ImageType = _keyedMap.ImageType;
                }
            }

            DrawMap(true);
        }

        private void picKey_MouseMove(object sender, MouseEventArgs e)
        {
            UpdateKey(new Point(e.X, e.Y));

            DrawKey();
        }

        private void mnuOpen_Click(object sender, EventArgs e)
        {
            if (FileIO.TryOpenFile(this, _mapFolder, null, ".ini", out _fileName))
            {
                toolStripStatusLabel2.Text = Path.GetFileNameWithoutExtension(_fileName);
                OpenMapFile();
            }

        }

        private void mnuSaveAs_Click(object sender, EventArgs e)
        {
            if (FileIO.TrySaveFile(this, _mapFolder, null, ".ini", out _fileName))
            {
                toolStripStatusLabel2.Text = Path.GetFileNameWithoutExtension(_fileName);
                SaveMapFile();
            }
        }

        private void mnuSave_Click(object sender, EventArgs e)
        {
            if (!File.Exists(_fileName))
            {
                toolStripStatusLabel2.Text = Path.GetFileNameWithoutExtension(_fileName);
                FileIO.TrySaveFile(this, _mapFolder, null, ".ini", out _fileName);
            }

            SaveMapFile();
        }

        private void mnu4WayDiagonal_Click(object sender, EventArgs e)
        {
            MapStrToMap(_map4WayDiagonal);
            toolStripStatusLabel2.Text = "4 Way Diagonal";
        }

        private void mnu4WaySticky_Click(object sender, EventArgs e)
        {
            MapStrToMap(_map4WaySticky);
            toolStripStatusLabel2.Text = "4 Way Sticky";
        }

        private void mnu8Way_Click(object sender, EventArgs e)
        {
            MapStrToMap(_map8Way);
            toolStripStatusLabel2.Text = "8 Way";
        }

        private void mnuAbout_Click(object sender, EventArgs e)
        {
            using (frmAbout aboutForm = new frmAbout())
                aboutForm.ShowDialog(this);
        }

        private void mnuClear_Click(object sender, EventArgs e)
        {
            ClearMap();
            DrawMap(true);
        }
    }
}