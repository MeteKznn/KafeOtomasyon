using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace KahveOtomasyonProjesi
{
    public enum RoundedCorners
    {
        None = 0,
        All = 1,
        TopLeft = 2,
        TopRight = 4,
        BottomLeft = 8,
        BottomRight = 16,
        LeftSide = TopLeft | BottomLeft,
        RightSide = TopRight | BottomRight,
        TopSide = TopLeft | TopRight,
        BottomSide = BottomLeft | BottomRight
    }

    public static class ControlStyles
    {
        public static void ApplyBorderRadius(Control control, int borderRadius, RoundedCorners corners = RoundedCorners.All)
        {
            if (control == null) return;

            GraphicsPath path = new GraphicsPath();
            int radius = borderRadius;
            int width = control.Width;
            int height = control.Height;

            // Ensure dimensions are valid before creating the path
            if (width <= 0 || height <= 0 || radius <= 0)
            {
                control.Region = new Region(new Rectangle(0, 0, width, height)); // Reset to rectangular if invalid
                return;
            }

            // Adjust radius if it's too large for the control's dimensions
            if (radius * 2 > width) radius = width / 2;
            if (radius * 2 > height) radius = height / 2;

            // Top-left corner
            if (corners.HasFlag(RoundedCorners.TopLeft) || corners.HasFlag(RoundedCorners.All))
            {
                path.AddArc(0, 0, radius * 2, radius * 2, 180, 90);
            }
            else
            {
                path.AddLine(0, 0, 0, 0);
            }

            // Top-right corner
            if (corners.HasFlag(RoundedCorners.TopRight) || corners.HasFlag(RoundedCorners.All))
            {
                path.AddArc(width - radius * 2, 0, radius * 2, radius * 2, 270, 90);
            }
            else
            {
                path.AddLine(width, 0, width, 0);
            }

            // Bottom-right corner
            if (corners.HasFlag(RoundedCorners.BottomRight) || corners.HasFlag(RoundedCorners.All))
            {
                path.AddArc(width - radius * 2, height - radius * 2, radius * 2, radius * 2, 0, 90);
            }
            else
            {
                path.AddLine(width, height, width, height);
            }

            // Bottom-left corner
            if (corners.HasFlag(RoundedCorners.BottomLeft) || corners.HasFlag(RoundedCorners.All))
            {
                path.AddArc(0, height - radius * 2, radius * 2, radius * 2, 90, 90);
            }
            else
            {
                path.AddLine(0, height, 0, height);
            }

            path.CloseAllFigures();

            control.Region = new Region(path);

            // Optional: Reapply border radius on resize
            control.Resize -= (sender, e) => ApplyBorderRadius(control, borderRadius, corners);
            control.Resize += (sender, e) => ApplyBorderRadius(control, borderRadius, corners);
        }
    }
}