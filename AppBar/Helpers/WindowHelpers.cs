using System.Windows.Forms;
using System.Drawing;

namespace AppBar.Helpers
{
    static public class WindowHelpers
    {
        public static Screen CurrentScreen(Point p)
        {
            return Screen.FromPoint(p);
        }

        public static void MovePosition(double currentX,double currentY,ref double XPosition, ref double YPosition, WindowLocation location, double WindowWidth, double WindowHeight)
        {
            Screen s = CurrentScreen(new System.Drawing.Point((int)currentX, (int)currentY));
            switch (location)
            {
                case WindowLocation.TOP:
                    YPosition = s.Bounds.Y;
                    XPosition = s.Bounds.X + s.Bounds.Width / 2 - ((double)WindowWidth) / 2;
                    break;
                case WindowLocation.BOTTOM:
                    YPosition = s.Bounds.Y + s.Bounds.Height - WindowHeight;
                    XPosition = s.Bounds.X + s.Bounds.Width / 2 - ((double)WindowWidth) / 2;
                    break;
                case WindowLocation.LEFT:
                    YPosition = s.Bounds.Y + s.Bounds.Height / 2 - WindowHeight / 2;
                    XPosition = s.Bounds.X;
                    break;
                case WindowLocation.RIGHT:
                    YPosition = s.Bounds.Y + s.Bounds.Height / 2 - WindowHeight / 2;
                    XPosition = s.Bounds.X + s.Bounds.Width - WindowWidth;
                    break;
                default:
                    break;
            }
        }
    }

    public enum WindowLocation
    {
        NONE = -1,
        LEFT = 0,
        TOP = 1,
        RIGHT = 2,
        BOTTOM = 3
    }
}
