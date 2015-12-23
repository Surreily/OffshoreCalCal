using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace OffshoreCalCal.Common.Controls
{
    /// <summary>
    /// A custom Grid that displays borders.
    /// </summary>
    public class BorderGrid : Grid
    {

        #region Constructor
        
        /// <summary>
        /// Static constructor to register properties.
        /// </summary>
        static BorderGrid()
        {
            BorderBrushProperty = DependencyProperty.Register("BorderBrush", typeof(Color), typeof(BorderGrid));
        }

        #endregion // Constructor

        #region Properties

        #region BorderBrush

        static DependencyProperty BorderBrushProperty;
        public Color BorderBrush
        {
            get
            {
                return (Color)base.GetValue(BorderBrushProperty);
            }
            set
            {
                base.SetValue(BorderBrushProperty, value);
            }
        }

        #endregion // BorderBrush

        #endregion // Properties

        #region Methods

        #region Overriden

        protected override void OnRender(DrawingContext dc)
        {
            // Prepare the Pen object
            Pen pen = new Pen(Brushes.Black, 1);
            pen.Freeze();
            double halfPenWidth = pen.Thickness / 2;

            // Prepare a GuidelineSet for pixel snapping
            GuidelineSet guidelines = new GuidelineSet();

            // Add each column
            double leftOffset = 0;
            foreach (ColumnDefinition column in ColumnDefinitions)
            {
                guidelines.GuidelinesX.Add(leftOffset + halfPenWidth);
                leftOffset += column.ActualWidth;
            }
            // Add the rightmost line
            guidelines.GuidelinesX.Add(leftOffset + halfPenWidth);

            // Add the bottom
            guidelines.GuidelinesY.Add(this.ActualHeight + halfPenWidth);

            // Draw the lines
            dc.PushGuidelineSet(guidelines);
            leftOffset = 0;
            foreach (ColumnDefinition column in ColumnDefinitions)
            {
                dc.DrawLine(pen, new Point(leftOffset, 0), new Point(leftOffset, this.ActualHeight));
                leftOffset += column.ActualWidth;
            }
            dc.DrawLine(pen, new Point(leftOffset, 0), new Point(leftOffset, this.ActualHeight));
            dc.DrawLine(pen, new Point(0, this.ActualHeight + 2), new Point(this.ActualWidth, this.ActualHeight + 2));
            dc.Pop();

            base.OnRender(dc);
        }

        #endregion // Overriden

        #endregion // Methods
    }
}
