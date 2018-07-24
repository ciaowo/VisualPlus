﻿#region Namespace

using System;
using System.Drawing;
using System.Drawing.Drawing2D;

using VisualPlus.Enumerators;
using VisualPlus.Structure;

#endregion

namespace VisualPlus.Renders
{
    public sealed class VisualElementRenderer
    {
        #region Methods

        /// <summary>Draws the control background, with a BackColor and the specified BackgroundImage.</summary>
        /// <param name="graphics">The graphics to draw on.</param>
        /// <param name="image">The background image to use for the background.</param>
        /// <param name="border">The shape settings.</param>
        /// <param name="color">The color.</param>
        /// <param name="enabled">The enabled.</param>
        /// <param name="mouseState">The mouse state.</param>
        /// <param name="rectangle">The coordinates of the rectangle to draw.</param>
        public static void DrawElement(Graphics graphics, Image image, Border border, ColorState color, bool enabled, MouseStates mouseState, Rectangle rectangle)
        {
            GraphicsPath _elementGraphicsPath = VisualBorderRenderer.CreateBorderTypePath(rectangle, border);
            graphics.SetClip(_elementGraphicsPath);
            Color _colorState = ColorState.BackColorState(color, enabled, mouseState);
            graphics.FillRectangle(new SolidBrush(_colorState), rectangle);
            graphics.ResetClip();

            if (image != null)
            {
                graphics.SetClip(_elementGraphicsPath);
                graphics.DrawImage(image, rectangle);
                graphics.ResetClip();
            }

            VisualBorderRenderer.DrawBorderStyle(graphics, border, _elementGraphicsPath, mouseState);
        }

        /// <summary>Draws the control background, with a BackColor and the specified BackgroundImage.</summary>
        /// <param name="graphics">The graphics to draw on.</param>
        /// <param name="image">The background image to use for the background.</param>
        /// <param name="border">The shape settings.</param>
        /// <param name="color">The color.</param>
        /// <param name="enabled">The enabled.</param>
        /// <param name="mouseState">The mouse state.</param>
        /// <param name="rectangle">The coordinates of the rectangle to draw.</param>
        public static void DrawElement(Graphics graphics, Image image, Border border, ControlColorState color, bool enabled, MouseStates mouseState, Rectangle rectangle)
        {
            GraphicsPath _elementGraphicsPath = VisualBorderRenderer.CreateBorderTypePath(rectangle, border);
            graphics.SetClip(_elementGraphicsPath);
            Color _colorState = ControlColorState.BackColorState(color, enabled, mouseState);
            graphics.FillRectangle(new SolidBrush(_colorState), rectangle);
            graphics.ResetClip();

            if (image != null)
            {
                graphics.SetClip(_elementGraphicsPath);
                graphics.DrawImage(image, rectangle);
                graphics.ResetClip();
            }

            VisualBorderRenderer.DrawBorderStyle(graphics, border, _elementGraphicsPath, mouseState);
        }

        /// <summary>Draws an arrow.</summary>
        /// <param name="graphics">The specified graphics to draw on.</param>
        /// <param name="color">The color.</param>
        /// <param name="disabled">The disabled.</param>
        /// <param name="enabled">The enabled.</param>
        /// <param name="image">The image.</param>
        /// <param name="rectangle">The rectangle.</param>
        /// <param name="direction">The direction.</param>
        public static void RenderArrow(Graphics graphics, Color color, Color disabled, bool enabled, Image image, Rectangle rectangle, Alignment.Vertical direction)
        {
            if (image != null)
            {
                graphics.DrawImage(image, rectangle);
            }
            else
            {
                Color colorState = enabled ? color : disabled;
                RenderTriangle(graphics, rectangle, colorState, direction);
            }
        }

        /// <summary>Draws an arrow image.</summary>
        /// <param name="graphics">The specified graphics to draw on.</param>
        /// <param name="color">The color.</param>
        /// <param name="image">The image.</param>
        /// <param name="rectangle">The rectangle.</param>
        /// <param name="direction">The direction.</param>
        public static void RenderArrowImage(Graphics graphics, Color color, Image image, Rectangle rectangle, Alignment.Vertical direction)
        {
            if (image != null)
            {
                // TODO: Flip image based on direction.

                graphics.DrawImage(image, rectangle);
            }
            else
            {
                RenderTriangle(graphics, rectangle, color, direction);
            }
        }

        /// <summary>Render bars.</summary>
        /// <param name="graphics">The graphics.</param>
        /// <param name="point">The point.</param>
        /// <param name="size">The size.</param>
        /// <param name="color">The color.</param>
        /// <param name="bars">The bars.</param>
        /// <param name="spacing">The spacing.</param>
        public static void RenderBars(Graphics graphics, Point point, Size size, Color color, int bars, int spacing)
        {
            // TODO: Add orientation, auto align in middle (to avoid drawing from top down since size can change depending on # bars.)
            int bump = spacing;
            for (var i = 0; i < bars; i++)
            {
                // Construct bar
                Pen linePen = new Pen(color, 2);

                // X , Y
                Point pt1 = new Point(point.X, point.Y + bump);

                // X , Y
                Point pt2 = new Point(point.X + size.Width, point.Y + bump);

                // Draw line bar
                graphics.DrawLine(linePen, pt1, pt2);

                // Prepare for next bar drawing
                bump = bump + spacing;
            }
        }

        /// <summary>Draw a triangle.</summary>
        /// <param name="graphics">The graphics to draw on.</param>
        /// <param name="rectangle">The button rectangle.</param>
        /// <param name="color">The color.</param>
        /// <param name="direction">The direction.</param>
        public static void RenderTriangle(Graphics graphics, Rectangle rectangle, Color color, Alignment.Vertical direction)
        {
            var points = new Point[3];

            switch (direction)
            {
                case Alignment.Vertical.Up:
                    {
                        points[0].X = rectangle.X + (rectangle.Width / 2);
                        points[0].Y = rectangle.Y;

                        points[1].X = rectangle.X;
                        points[1].Y = rectangle.Y + rectangle.Height;

                        points[2].X = rectangle.X + rectangle.Width;
                        points[2].Y = rectangle.Y + rectangle.Height;
                        break;
                    }

                case Alignment.Vertical.Down:
                    {
                        points[0].X = rectangle.X;
                        points[0].Y = rectangle.Y;

                        points[1].X = rectangle.X + rectangle.Width;
                        points[1].Y = rectangle.Y;

                        points[2].X = rectangle.X + (rectangle.Width / 2);
                        points[2].Y = rectangle.Y + rectangle.Height;
                        break;
                    }

                default:
                    {
                        throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
                    }
            }

            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.FillPolygon(new SolidBrush(color), points);
        }

        #endregion
    }
}