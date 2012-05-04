﻿using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Xanotech.Tools {
    public static class DrawingTool {

        public static Image Read(string filename) {
            try {
                return Image.FromFile(filename);
            } catch (ArgumentException) {
                try {
                    using (var icon = new Icon(filename))
                        return icon.ToBitmap();
                } catch {
                    // Figure it out later
                } // end try-catch
            } // end try-catch
            return null;
        } // end method



        public static Image Read(string filename, int width, int height) {
            var img = Read(filename);
            if (img == null)
                return img;

            if (img.Width == width && img.Height == height)
                return img;

            using (img)
                return img.Resize(width, height);
        } // end method



        public static Image Resize(this Image img, int width, int height) {
            var newImage = new Bitmap(width, height);
            using (var g = Graphics.FromImage(newImage)) {
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.DrawImage(img, 0, 0, width, height);
            } // end using
            return newImage;
        } // end method



        public static Icon ToIcon(this Image img) {
            using (var bmp = new Bitmap(img)) {
                var ptr = bmp.GetHicon();
                return Icon.FromHandle(ptr);
            } // end using
        } // end method

    } // end class
} // end namespace