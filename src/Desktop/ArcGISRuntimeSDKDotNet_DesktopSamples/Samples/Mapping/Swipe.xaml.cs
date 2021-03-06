﻿using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace ArcGISRuntimeSDKDotNet_DesktopSamples.Samples
{
    /// <summary>
    /// Shows how to swipe one map over another.
    /// </summary>
    /// <title>Swipe</title>
    /// <category>Mapping</category>
    public sealed partial class SwipeMap : UserControl
    {
        public SwipeMap()
        {
            this.InitializeComponent();

            thumb.RenderTransform = new TranslateTransform() { X = 0, Y = 0 };
            mapImagery.Clip = new RectangleGeometry() { Rect = new Rect(0, 0, 0, 0) };

            mapStreets.ExtentChanged += mapStreets_ExtentChanged;
        }

        private void Thumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            try
            {
                var transform = (TranslateTransform)thumb.RenderTransform;
                transform.X = Math.Max(0, Math.Min(transform.X + e.HorizontalChange, this.ActualWidth - thumb.ActualWidth));
                mapImagery.Clip = new RectangleGeometry() { Rect = new Rect(0, 0, transform.X, this.ActualHeight) };
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private void mapStreets_ExtentChanged(object sender, EventArgs e)
        {
            try
            {
                if (mapImagery.Extent != null)
                    mapImagery.SetView(mapStreets.Extent);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}
