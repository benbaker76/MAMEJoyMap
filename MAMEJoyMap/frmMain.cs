using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace MAMEJoyMap
{
    public partial class frmMain : Form
    {
        private DirectInput m_directInput = null;

        private MapNode[,] m_mainMap = null;
        private MapNode[,] m_keyMap = null;

        private Bitmap m_mainBitmap = null;
        private Bitmap m_gridBitmap = null;
        private Bitmap m_mapBitmap = null;
        private Bitmap m_keyBitmap = null;
        private Bitmap m_marioBitmap = null;

        private Point m_startPoint;
        private Point m_endPoint;

        private MapNode[] m_lastJoyValue = null;
        private MapValue m_stickyValue = MapValue.Neutral;

        private bool m_mouseDown = false;

        private MapNode m_keyedMap = null;

        private Point m_marioPoint;

        private string m_fileName = null;
        private string m_mapFolder = null;

        private string m_map4WayDiagonal = "4444s8888..444458888.444555888.ss5.222555666.222256666.2222s6666.2222s6666";
        private string m_map4WaySticky = "s8.4s8.44s8.4445";
        private string m_map8Way = "7778...4445";

        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            m_directInput = new DirectInput(this);
            m_directInput.OnJoyInput += new DirectInput.DIJoyButtonDownHandler(OnJoyInput);

            m_mapFolder = Path.Combine(Application.StartupPath, "Maps");

            m_mainMap = new MapNode[9, 9];
            m_keyMap = new MapNode[3, 4];

            m_mainBitmap = new Bitmap(217, 217);
            m_gridBitmap = new Bitmap(217, 217);
            m_mapBitmap = new Bitmap(217, 217);

            m_keyBitmap = new Bitmap(73, 97);
            m_marioBitmap = new Bitmap(73, 97);

            m_marioPoint = new Point(25, 37);

            picMain.Image = m_mainBitmap;
            picKey.Image = m_keyBitmap;
            picMario.Image = m_marioBitmap;

            m_lastJoyValue = new MapNode[8];

            for (int i = 0; i < 8; i++)
                m_lastJoyValue[i] = new MapNode(0, 0, ImageType.Neutral);

            for (int y = 0; y < 9; y++)
                for (int x = 0; x < 9; x++)
                    m_mainMap[x, y] = new MapNode(x * 25, y * 25, ImageType.Neutral);

            int count = 0;

            for (int y = 0; y < 4; y++)
                for (int x = 0; x < 3; x++)
                    if (count < imageList1.Images.Count - 1)
                        m_keyMap[x, y] = new MapNode(x * 25, y * 25, (ImageType)count++);

            using (Graphics g = Graphics.FromImage(m_gridBitmap))
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

            if (m_lastJoyValue != null)
                m_lastJoyValue[e.JoyId].JoyPos = false;

            m_mainMap[p.X, p.Y].JoyPos = true;
            bool sticky = (m_mainMap[p.X, p.Y].MapValue == MapValue.Sticky);

            if (!sticky)
                m_stickyValue = m_mainMap[p.X, p.Y].MapValue;

            MapNode mapNode = (sticky ? m_lastJoyValue[e.JoyId] : m_mainMap[p.X, p.Y]);
            MapValue mapValue = (sticky ? m_stickyValue : mapNode.MapValue);

            if (mapNode != m_lastJoyValue[e.JoyId])
                toolStripStatusLabel1.Text = mapValue.ToString();

            m_lastJoyValue[e.JoyId] = m_mainMap[p.X, p.Y];

            if (TryMoveMario(mapValue))
                DrawMario();

            DrawMap(false);
        }

        private void DrawKey()
        {
            using (Graphics g = Graphics.FromImage(m_keyBitmap))
            {
                g.Clear(Color.FromArgb(207, 215, 196));

                int count = 0;

                using (SolidBrush sb = new SolidBrush(Color.Yellow))
                {
                    for (int y = 0; y < 4; y++)
                        for (int x = 0; x < 3; x++)
                            if (count++ < imageList1.Images.Count - 1)
                                if (m_keyMap[x, y].Selected)
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
                        if (m_mainMap[x, y].Selected)
                            g.FillRectangle(sb, x * 24, y * 24, 24, 24);
            }
        }

        private void DrawJoyPos(Graphics g)
        {
            using (SolidBrush sb = new SolidBrush(Color.Red))
            {
                for (int y = 0; y < 9; y++)
                    for (int x = 0; x < 9; x++)
                        if (m_mainMap[x, y].JoyPos)
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
            g.DrawImageUnscaled(m_gridBitmap, 0, 0);
        }

        private void DrawMap(Graphics g)
        {
            g.DrawImageUnscaled(m_mapBitmap, 0, 0);
        }

        private void UpdateMap()
        {
            using (Graphics g = Graphics.FromImage(m_mapBitmap))
            {
                g.Clear(Color.FromArgb(0, 0, 0, 0));

                for (int y = 0; y < 9; y++)
                    for (int x = 0; x < 9; x++)
                        if ((int)m_mainMap[x, y].ImageType < imageList1.Images.Count - 1)
                            g.DrawImageUnscaled(imageList1.Images[(int)m_mainMap[x, y].ImageType], x * 24 + 1, y * 24 + 1);
            }
        }

        private void DrawMap(bool updateMap)
        {
            using (Graphics g = Graphics.FromImage(m_mainBitmap))
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
            using (Graphics g = Graphics.FromImage(m_marioBitmap))
            {
                g.Clear(Color.FromArgb(207, 215, 196));

                using (SolidBrush sb = new SolidBrush(Color.Red))
                    if(10 < imageList1.Images.Count)
                        g.DrawImageUnscaled(imageList1.Images[10], m_marioPoint.X, m_marioPoint.Y);
            }

            picMario.Invalidate();
        }

        private bool TryMoveMario(MapValue MapValue)
        {
            int mapValue = (int)MapValue;
            bool moveMario = false;

            if ((mapValue & (int)MapType.Up) > 0)
            {
                m_marioPoint.Y -= 2;

                if (m_marioPoint.Y >= 0)
                    moveMario = true;
                else
                    m_marioPoint.Y = 74;
            }
            if ((mapValue & (int)MapType.Left) > 0)
            {
                m_marioPoint.X -= 2;

                if (m_marioPoint.X >= 0)
                    moveMario = true;
                else
                    m_marioPoint.X = 50;
            }
            if ((mapValue & (int)MapType.Right) > 0)
            {
                m_marioPoint.X += 2;

                if (m_marioPoint.X <= 50)
                    moveMario = true;
                else
                    m_marioPoint.X = 0;
            }
            if ((mapValue & (int)MapType.Down) > 0)
            {
                m_marioPoint.Y += 2;

                if (m_marioPoint.Y <= 74)
                    moveMario = true;
                else
                    m_marioPoint.Y = 0;
            }

            return moveMario;
        }

        private void UpdateMainSelection()
        {
            Point startPoint = new Point(m_startPoint.X < m_endPoint.X ? m_startPoint.X : m_endPoint.X, m_startPoint.Y < m_endPoint.Y ? m_startPoint.Y : m_endPoint.Y);
            Point endPoint = new Point(m_endPoint.X > m_startPoint.X ? m_endPoint.X : m_startPoint.X, m_endPoint.Y > m_startPoint.Y ? m_endPoint.Y : m_startPoint.Y);

            Rectangle selectRect = new Rectangle(startPoint.X, startPoint.Y, endPoint.X - startPoint.X, endPoint.Y - startPoint.Y);

            bool drawMap = false;

            for (int y = 0; y < 9; y++)
            {
                for (int x = 0; x < 9; x++)
                {
                    if (selectRect.IntersectsWith(m_mainMap[x, y].Rectangle))
                    {
                        if (!m_mainMap[x, y].Selected)
                        {
                            m_mainMap[x, y].Selected = true;
                            drawMap = true;
                        }
                    }
                    else
                    {
                        if (m_mainMap[x, y].Selected)
                        {
                            m_mainMap[x, y].Selected = false;
                            drawMap = true;
                        }
                    }
                }
            }

            if(drawMap)
                DrawMap(false);
        }

        private void UpdateMainSelectionName()
        {
            for (int y = 0; y < 9; y++)
            {
                for (int x = 0; x < 9; x++)
                {
                    if (m_mainMap[x, y].Rectangle.Contains(m_endPoint))
                        toolStripStatusLabel1.Text = m_mainMap[x, y].ImageType.ToString();
                }
            }
        }

        private void UpdateKey(Point p)
        {
            int count = 0;
            m_keyedMap = null;

            for (int y = 0; y < 4; y++)
            {
                for (int x = 0; x < 3; x++)
                {
                    if (count++ < imageList1.Images.Count - 1)
                    {
                        if (m_keyMap[x, y].Rectangle.Contains(p))
                        {
                            m_keyMap[x, y].Selected = true;
                            m_keyedMap = m_keyMap[x, y];

                            toolStripStatusLabel1.Text = m_keyedMap.ImageType.ToString();
                        }
                        else
                            m_keyMap[x, y].Selected = false;
                    }
                }
            }
        }

        private void ClearMap()
        {
            for (int y = 0; y < 9; y++)
                for (int x = 0; x < 9; x++)
                    m_mainMap[x, y].ImageType = ImageType.Neutral;
        }

        private void OpenMapFile()
        {
            ClearMap();

            string[] iniFile = File.ReadAllLines(m_fileName);

            for(int i=0; i<iniFile.Length; i++)
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
                            m_mainMap[x, y].ImageType = m_mainMap[x, y - 1].ImageType;

                            continue;
                        }

                        if (x < strMap[y].Length)
                        {
                            switch (strMap[y][x])
                            {
                                case (char)MapChar.UpLeft:
                                    m_mainMap[x, y].ImageType = ImageType.UpLeft;
                                    break;
                                case (char)MapChar.Up:
                                    m_mainMap[x, y].ImageType = ImageType.Up;
                                    break;
                                case (char)MapChar.UpRight:
                                    m_mainMap[x, y].ImageType = ImageType.UpRight;
                                    break;
                                case (char)MapChar.Left:
                                    m_mainMap[x, y].ImageType = ImageType.Left;
                                    break;
                                case (char)MapChar.Neutral:
                                    m_mainMap[x, y].ImageType = ImageType.Neutral;
                                    break;
                                case (char)MapChar.Right:
                                    m_mainMap[x, y].ImageType = ImageType.Right;
                                    break;
                                case (char)MapChar.DownLeft:
                                    m_mainMap[x, y].ImageType = ImageType.DownLeft;
                                    break;
                                case (char)MapChar.Down:
                                    m_mainMap[x, y].ImageType = ImageType.Down;
                                    break;
                                case (char)MapChar.DownRight:
                                    m_mainMap[x, y].ImageType = ImageType.DownRight;
                                    break;
                                case (char)MapChar.Sticky:
                                    m_mainMap[x, y].ImageType = ImageType.Sticky;
                                    break;
                            }
                        }
                        else
                        {
                            if (x < 5 && x > 0)
                                m_mainMap[x, y].ImageType = m_mainMap[x - 1, y].ImageType;
                            else
                                m_mainMap[x, y].ImageType = GetColumnMirrorImage(m_mainMap[8 - x, y].MapValue);
                        }
                    }
                }
                else
                {
                    for (int x = 0; x < 9; x++)
                    {
                        if (y < 5 && y > 0)
                            m_mainMap[x, y].ImageType = m_mainMap[x, y - 1].ImageType;
                        else
                            m_mainMap[x, y].ImageType = GetRowMirrorImage(m_mainMap[x, 8 - y].MapValue);
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
                    switch (m_mainMap[x, y].ImageType)
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

            if (File.Exists(m_fileName))
                iniFile.AddRange(File.ReadAllLines(m_fileName));

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

            File.WriteAllLines(m_fileName, iniFile.ToArray());
        }

        private void picMain_MouseDown(object sender, MouseEventArgs e)
        {
            m_startPoint = new Point(e.X, e.Y);
            m_endPoint = new Point(e.X, e.Y);
            m_mouseDown = true;

            UpdateMainSelection();
        }

        private void picMain_MouseMove(object sender, MouseEventArgs e)
        {
            m_endPoint = new Point(e.X, e.Y);

            if (m_mouseDown)
                UpdateMainSelection();
            else
                UpdateMainSelectionName();
        }

        private void picMain_MouseUp(object sender, MouseEventArgs e)
        {
            m_endPoint = new Point(e.X, e.Y);
            m_mouseDown = false;

            UpdateMainSelection();
        }

        private void picKey_Click(object sender, EventArgs e)
        {
            if (m_keyedMap == null)
                return;

            for (int y = 0; y < 9; y++)
            {
                for (int x = 0; x < 9; x++)
                {
                    if (m_mainMap[x, y].Selected)
                        m_mainMap[x, y].ImageType = m_keyedMap.ImageType;
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
            if (FileIO.TryOpenFile(this, m_mapFolder, null, ".ini", out m_fileName))
            {
                toolStripStatusLabel2.Text = Path.GetFileNameWithoutExtension(m_fileName);
                OpenMapFile();
            }

        }

        private void mnuSaveAs_Click(object sender, EventArgs e)
        {
            if (FileIO.TrySaveFile(this, m_mapFolder, null, ".ini", out m_fileName))
            {
                toolStripStatusLabel2.Text = Path.GetFileNameWithoutExtension(m_fileName);
                SaveMapFile();
            }
        }

        private void mnuSave_Click(object sender, EventArgs e)
        {
            if (!File.Exists(m_fileName))
            {
                toolStripStatusLabel2.Text = Path.GetFileNameWithoutExtension(m_fileName);
                FileIO.TrySaveFile(this, m_mapFolder, null, ".ini", out m_fileName);
            }

            SaveMapFile();
        }

        private void mnu4WayDiagonal_Click(object sender, EventArgs e)
        {
            MapStrToMap(m_map4WayDiagonal);
            toolStripStatusLabel2.Text = "4 Way Diagonal";
        }

        private void mnu4WaySticky_Click(object sender, EventArgs e)
        {
            MapStrToMap(m_map4WaySticky);
            toolStripStatusLabel2.Text = "4 Way Sticky";
        }

        private void mnu8Way_Click(object sender, EventArgs e)
        {
            MapStrToMap(m_map8Way);
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