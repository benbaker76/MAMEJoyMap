using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace MAMEJoyMap
{
    public enum ImageType
    {
        UpLeft,
        Up,
        UpRight,
        Left,
        Neutral,
        Right,
        DownLeft,
        Down,
        DownRight,
        Sticky
    }

    public enum MapChar
    {
        UpLeft = '7',
        Up = '8',
        UpRight = '9',
        Left = '4',
        Neutral = '5',
        Right = '6',
        DownLeft = '1',
        Down = '2',
        DownRight = '3',
        Sticky = 's'
    }

    public enum MapValue
    {
        UpLeft = MapType.Up | MapType.Left,
        Up = MapType.Up,
        UpRight = MapType.Up | MapType.Right,
        Left = MapType.Left,
        Neutral = MapType.Neutral,
        Right = MapType.Right,
        DownLeft = MapType.Down | MapType.Left,
        Down = MapType.Down,
        DownRight = MapType.Down | MapType.Right,
        Sticky = MapType.Sticky
    }

    public enum MapType
    {
        Neutral = 0x00,
        Left = 0x01,
        Right = 0x02,
        Up = 0x04,
        Down = 0x08,
        Sticky = 0x0f
    }

    class MapNode
    {
        public ImageType ImageType = ImageType.Neutral;

        public bool Selected = false;
        public bool JoyPos = false;

        public Rectangle Rectangle;

        public MapNode(int x, int y, ImageType imageType)
        {
            Rectangle = new Rectangle(x, y, 25, 25);
            ImageType = imageType;
        }

        public MapValue MapValue
        {
            get
            {
                switch (ImageType)
                {
                case ImageType.UpLeft:
                    return MapValue.UpLeft;
                case ImageType.Up:
                    return MapValue.Up;
                case ImageType.UpRight:
                    return MapValue.UpRight;
                case ImageType.Left:
                    return MapValue.Left;
                case ImageType.Neutral:
                    return MapValue.Neutral;
                case ImageType.Right:
                    return MapValue.Right;
                case ImageType.DownLeft:
                    return MapValue.DownLeft;
                case ImageType.Down:
                    return MapValue.Down;
                case ImageType.DownRight:
                    return MapValue.DownRight;
                case ImageType.Sticky:
                    return MapValue.Sticky;
                default:
                    return MapValue.Neutral;
                }
            }
        }
    }
}
