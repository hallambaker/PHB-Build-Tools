using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Windows.Media.Imaging;

namespace Goedel.Document.Office {
    public class ImageUtil {

        public static string TargetFile(string Source, string DestPath, string Var, string Ext) {
            return TargetFile(Source, DestPath, "_" + Var + Ext);
            }

        public static string TargetFile(string Source, string Dest, string Ext) {
            string Name = Path.GetFileNameWithoutExtension(Source);

            return Dest + "\\" + Name + Ext;
            }




        // Convert a source image file to a Jpeg  at specified resolutions
        public static bool ToJpg(string Source, string Dest, int[] Sizes) {
            var Image = BitmapRAW(Source);
            return ToJpg(Image, Source, Dest, Sizes);
            }

        public static bool ToJpg(Bitmap Image, string Source, string Dest, int[] Sizes) {
            bool result = false;
            

            foreach (int Size in Sizes) {
                result = result | ToJpg(Image, Source, Dest, Size);
                }

            return result;
            }

        // Convert a source image file to a JPeg at a specified resolution

        public static bool ToJpg(string Source, string DestPath, int Size) {
            var Image = BitmapRAW(Source);
            return ToJpg(Image, Source, DestPath, Size);
            }


        public static bool ToJpg(Bitmap Image, string OutFile, int Size) {

            var Bounded = ImageUtil.Bound(Image, Size);
            Bounded.Save(OutFile, ImageFormat.Jpeg);

            return true;
            }

        public static bool ToJpg(Bitmap Image, string Source, string DestPath, int Size) {
            //var OutFile = Dest + "_" + Size.ToString() + ".jpg";
            var OutFile = TargetFile(Source, DestPath, Size.ToString() , ".jpg");

            if (!RemakeTarget(Source, OutFile)) {
                return false;
                }


            var Bounded = ImageUtil.Bound(Image, Size);
            Bounded.Save(OutFile, ImageFormat.Jpeg);

            return true;
            }

        public static bool ToPng(string Source, string Dest, int[] Sizes) {
            var Image = BitmapRAW(Source);
            return ToPng(Image, Source, Dest, Sizes);
            }

        // Convert a source image file to a lossless PNG at specified resolutions
        public static bool ToPng(Bitmap Image, string Source, string DestPath, int[] Sizes) {
            bool result = false;
            foreach (int Size in Sizes) {
            result = result | ToPng(Image, Source, DestPath, Size);
                }

            return result;
            }

        public static bool ToPng(string Source, string DestPath, int Size) {
            var Image = BitmapRAW(Source);
            return ToPng(Image, Source, DestPath, Size);
            }

        // Convert a source image file to a lossless PNG at a specified resolution
        public static bool ToPng(Bitmap Image, string Source, string DestPath, int Size) {
            //var OutFile = Dest + "_" + Size.ToString() + ".png";
            var OutFile = TargetFile(Source, DestPath, Size.ToString(), ".png");
            if (!RemakeTarget(Source, OutFile)) {
                return false;
                }

            var Bounded = ImageUtil.Bound(Image, Size);
            Bounded.Save(OutFile, ImageFormat.Jpeg);

            return true;
            }


        public static bool RemakeTarget(string Source, string Dest) {
            if (!File.Exists(Source)) {
                return false;
                }
            if (!File.Exists(Dest)) {
                return true;
                }

            var SourceWrite = File.GetLastWriteTimeUtc(Source);
            var DestCreate = File.GetCreationTimeUtc(Dest);

            return SourceWrite >= DestCreate;
            }

        public static Bitmap BitmapRAW(string Source) {
            using (var FileStream = new FileStream(Source, FileMode.Open, FileAccess.Read, FileShare.Read)) {
                return BitmapRAW(FileStream);
                }
            }

        public static Bitmap BitmapRAW(Stream SourceStream) {

            var bmpDec = BitmapDecoder.Create(SourceStream,
                BitmapCreateOptions.DelayCreation, BitmapCacheOption.None);
            var bitmapSourcetest = bmpDec.Frames[0];

            var Bitmap = ImageUtil.Bitmap(bitmapSourcetest);

            return Bitmap;
            }


        public static Bitmap Bitmap(BitmapFrame bitmapsource) {
            System.Drawing.Bitmap bitmap;
            using (MemoryStream outStream = new MemoryStream()) {
                BitmapEncoder enc = new BmpBitmapEncoder();
                enc.Frames.Add(bitmapsource);
                enc.Save(outStream);
                bitmap = new System.Drawing.Bitmap(outStream);
                }
            return bitmap;
            }


        public static Bitmap Bound(Bitmap Image, int MaxDimension) {
        return Bound(Image, MaxDimension, MaxDimension);
            }

        // Confine an image to be no larger than a bounding box of MaxWidth x MaxHeight
        public static Bitmap Bound(Bitmap Image, int MaxWidth, int MaxHeight) {
            // Rely on the optimizer to only calculate these as necessary
            float HScale = ((float)MaxHeight) / Image.Size.Height;
            float VScale = ((float)MaxWidth) / Image.Size.Width;

            if ((Image.Size.Height > MaxHeight) &
                    (Image.Size.Width > MaxWidth)) {
                float Scale = HScale > VScale ? VScale : HScale;
                int Height = (int)(Image.Size.Height * Scale);
                int Width = (int)(Image.Size.Width * Scale);
                return Resize(Image, Width, Height);
                }
            else if (Image.Size.Width > MaxWidth) {
                int Height = (int)(Image.Size.Height * VScale);
                return Resize(Image, MaxWidth, Height);
                }
            else if (Image.Size.Height > MaxHeight) {
                int Width = (int)(Image.Size.Width * HScale);
                return Resize(Image, Width, MaxHeight);
                }

            else {
                return Image;
                }
            }


        /// Resize the image to the specified width and height.
        /// </summary>
        /// <param name="Image">The image to resize.</param>
        /// <param name="Width">The width to resize to.</param>
        /// <param name="Height">The height to resize to.</param>
        /// <returns>The resized image.</returns>
        public static Bitmap Resize(Bitmap Image, int Width, int Height) {
            var destRect = new Rectangle(0, 0, Width, Height);
            var destImage = new Bitmap(Width, Height);

            destImage.SetResolution(Image.HorizontalResolution, Image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage)) {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes()) {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(Image, destRect, 0, 0, Image.Width, Image.Height, GraphicsUnit.Pixel, wrapMode);
                    }
                }

            return destImage;
            }


        }
    }
