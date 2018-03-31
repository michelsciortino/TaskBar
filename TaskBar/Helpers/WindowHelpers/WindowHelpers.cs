using System.Windows.Forms;
using System.Drawing;

namespace TaskBar.Helpers
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
                case WindowLocation.Top:
                    YPosition = s.Bounds.Y;
                    XPosition = s.Bounds.X + s.Bounds.Width / 2 - ((double)WindowWidth) / 2;
                    break;
                case WindowLocation.Bottom:
                    YPosition = s.Bounds.Y + s.Bounds.Height - WindowHeight;
                    XPosition = s.Bounds.X + s.Bounds.Width / 2 - ((double)WindowWidth) / 2;
                    break;
                case WindowLocation.Left:
                    YPosition = s.Bounds.Y + s.Bounds.Height / 2 - WindowHeight / 2;
                    XPosition = s.Bounds.X;
                    break;
                case WindowLocation.Right:
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
        Float = -1,
        Left = 0,
        Top = 1,
        Right = 2,
        Bottom = 3
    }
}
