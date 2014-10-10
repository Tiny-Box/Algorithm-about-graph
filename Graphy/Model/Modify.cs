using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GalaSoft.MvvmLight.Messaging;

namespace Graphy.Model
{
    class Modify
    {
        public static double[] GraySum = new double[256];
        public static double[] RedSum = new double[256];
        public static double[] GreenSum = new double[256];
        public static double[] BlueSum = new double[256];


        public void Sum(string filename)
        {

            BitmapImage bitmapSource = new BitmapImage(new Uri(filename));
            // 图像的宽度和高度
            int width = bitmapSource.PixelWidth;
            int height = bitmapSource.PixelHeight;
            // 图像的格式
            PixelFormat pixelFormat = bitmapSource.Format;
            // 每行的字节
            int stride = (width * pixelFormat.BitsPerPixel + 7) / 8;
            // 像素数组
            byte[] pixels = new byte[stride * height];

            // 复制像素到像素数组
            bitmapSource.CopyPixels(Int32Rect.Empty, pixels, stride, 0);

            // 初始化
            for (int i = 0; i < 256; i++)
            {
                GraySum[i] = 0;
                RedSum[i] = 0;
                GreenSum[i] = 0;
                BlueSum[i] = 0;
            }

            // 统计
            for (int i = 0; i < pixels.Length; i += 4)
            {
                RedSum[pixels[i]] += 1;
                GreenSum[pixels[i + 1]] += 1;
                BlueSum[pixels[i + 2]] += 1;
            }
            for (int i = 0; i < pixels.Length; i += 4)
            {
                byte gray = (byte)(0.299 * pixels[i] + 0.587 * pixels[i + 1] + 0.114 * pixels[i + 2]);
                pixels[i] = gray;
                GraySum[pixels[i]] += 3;
            }
        }

        // 灰度图生成
        public WriteableBitmap GrayFilter(BitmapImage bitmapSource)
        {
            // 图像的宽度和高度
            int width = bitmapSource.PixelWidth;
            int height = bitmapSource.PixelHeight;
            // 图像的格式
            PixelFormat pixelFormat = bitmapSource.Format;
            // 每行的字节
            int stride = (width * pixelFormat.BitsPerPixel + 7) / 8;
            // 调色板
            BitmapPalette Palette = new BitmapPalette(bitmapSource, 256);
            // 像素数组
            byte[] pixels = new byte[stride * height];

            // 复制像素到像素数组
            bitmapSource.CopyPixels(Int32Rect.Empty, pixels, stride, 0);

            //MessageBox.Show("Width: " + bitmapSource.Width.ToString() + "PixelWidth: " + bitmapSource.PixelWidth.ToString() +　"other: " + bitmapSource.DecodePixelWidth.ToString() + "Bit: " + pixelFormat.BitsPerPixel.ToString());

            //gray=BYTE(0.299*red)+BYTE(0.587*green)+BYTE(0.114*blue); 

            for (int i = 0; i < pixels.LongLength; i += 4)
            {
                byte gray = (byte)(0.299 * pixels[i] + 0.587 * pixels[i + 1] + 0.114 * pixels[i + 2]);
                pixels[i] = gray;
                pixels[i + 1] = gray;
                pixels[i + 2] = gray;
            }

            WriteableBitmap bitmap = new WriteableBitmap(BitmapSource.Create(width, height, bitmapSource.DpiX, bitmapSource.DpiY, pixelFormat, null,
                                             pixels, stride));
            return bitmap;
        }

        // Bayer抖动显示
        public WriteableBitmap LimbPatternBayer(BitmapImage bitmapSource)
        {
            int width = bitmapSource.PixelWidth;
            int height = bitmapSource.PixelHeight;
            PixelFormat pixelFormat = bitmapSource.Format;
            int stride = (width * pixelFormat.BitsPerPixel + 7) / 8;
            byte pixel;
            byte[] pixels = new byte[stride * height];

            // 复制像素到像素数组
            bitmapSource.CopyPixels(Int32Rect.Empty, pixels, stride, 0);

            byte[, ] BayerPattern = new byte[8, 8]{{ 0, 32,  8, 40,  2, 34, 10, 42},
                                                   {48, 16, 56, 24, 50, 18, 58, 26},
                                                   {12, 44,  4, 36, 14, 46,  6, 38},
                                                   {60, 28, 52, 20, 62, 30, 54, 22},
                                                   { 3, 35, 11, 43,  1, 33,  9, 41},
                                                   {51, 19, 59, 27, 49, 17, 57, 25},
                                                   {15, 47,  7, 39, 13, 45,  5, 37},
                                                   {63, 31, 55, 23, 61, 29, 53, 21}};
            for (int j = 0; j < height; j++)
            {
                for (int i = 0; i < stride; i++)
                {
                    pixel = pixels[stride * j + i];

                    if ((pixel >> 2) > BayerPattern[j & 7, i & 7])
                        pixels[stride * j + i] = 255;
                    else
                        pixels[stride * j + i] = 0;
                }
            }



            WriteableBitmap bitmap = new WriteableBitmap(BitmapSource.Create(width, height, bitmapSource.DpiX, bitmapSource.DpiY, pixelFormat, null,
                                             pixels, stride));
            return bitmap;
        }

        // Floyd-Steinberg抖动显示
        public WriteableBitmap DitherFloydSteinberg(BitmapImage bitmapSource)
        {
            // 图像的宽度和高度
            int width = bitmapSource.PixelWidth;
            int height = bitmapSource.PixelHeight;
            // 图像的格式
            PixelFormat pixelFormat = bitmapSource.Format;
            // 每行的字节
            int stride = (width * pixelFormat.BitsPerPixel + 7) / 8;
            // 调色板
            BitmapPalette Palette = new BitmapPalette(bitmapSource, 256);
            // 循环变量
            int i, j;
            // 误差传播系数
            double temp, error;
            // 临时像素变量
            byte pixel;
            // 像素数组
            byte[] pixels = new byte[stride * height];

            // 复制像素到像素数组
            bitmapSource.CopyPixels(Int32Rect.Empty, pixels, stride, 0);

            for (j = 0; j < height; j++)
            {
                for (i = 0; i < stride; i++)
                {
                    pixel = pixels[stride * j + i];

                    if (pixel > 128)
                    {
                        // 白色
                        pixels[stride * j + i] = 255;

                        // 计算误差
                        error = (double)(pixel - 255.0);
                    }
                    else
                    {
                        // 黑色
                        pixels[stride * j + i] = 0;

                        // 计算误差
                        error = (double)pixel;
                    }

                    // 如果不是边界
                    if (i < stride - 1)
                    {
                        // 向右
                        temp = (float)pixels[stride * j + i + 1];

                        temp = temp + error * (1.5 / 8.0);

                        if (temp > 255.0)
                            temp = 255.0;

                        pixels[stride * j + i + 1] = (byte)temp;
                    }

                    // 如果不是边界
                    if (j < height - 1)
                    {
                        // 向下
                        temp = (float)pixels[stride * (j + 1) + i];

                        temp = temp + error * (1.5 / 8.0);

                        if (i < stride -1)
                        {
                            // 向右下
                            temp = (float)pixels[stride * (j + 1) + i + 1];

                            temp = temp + error * (2.0 / 16.0);

                            pixels[stride * (j + 1) + i + 1] = (byte)temp;
                        }
                    }
                }
            }

            WriteableBitmap bitmap = new WriteableBitmap(BitmapSource.Create(width, height, bitmapSource.DpiX, bitmapSource.DpiY, pixelFormat, Palette, pixels, stride));
            return bitmap;
        }

        // 灰度统计
        public void GraySumMethod(BitmapImage bitmapSource)
        {
            //// 图像的宽度和高度
            //int width = bitmapSource.PixelWidth;
            //int height = bitmapSource.PixelHeight;
            //// 图像的格式
            //PixelFormat pixelFormat = bitmapSource.Format;
            //// 每行的字节
            //int stride = (width * pixelFormat.BitsPerPixel + 7) / 8;
            //// 调色板
            //BitmapPalette Palette = new BitmapPalette(bitmapSource, 256);
            //// 像素数组
            //byte[] pixels = new byte[stride * height];

            //// 复制像素到像素数组
            //bitmapSource.CopyPixels(Int32Rect.Empty, pixels, stride, 0);

            ////MessageBox.Show("Width: " + bitmapSource.Width.ToString() + "PixelWidth: " + bitmapSource.PixelWidth.ToString() + "other: " + bitmapSource.DecodePixelWidth.ToString() + "Bit: " + pixelFormat.BitsPerPixel.ToString());

            ////gray=BYTE(0.299*red)+BYTE(0.587*green)+BYTE(0.114*blue); 

            //for (int i = 0; i < pixels.LongLength; i += 4)
            //{
            //    byte gray = (byte)(0.299 * pixels[i] + 0.587 * pixels[i + 1] + 0.114 * pixels[i + 2]);
            //    pixels[i] = gray;
            //    pixels[i + 1] = gray;
            //    pixels[i + 2] = gray;
            //}

            

            

            //// 初始化
            //for (int i = 0; i < 256; i++ )
            //{
            //    GraySum[i] = 0;
            //}

            //// 统计
            //for (int i = 0; i < pixels.Length; i++)
            //{
            //    GraySum[pixels[i]] += 1;
            //}


            SumGra Gray = new SumGra();
            Gray.Show();
            

        }

        // 灰度阈值
        public WriteableBitmap ThresholdTrans(BitmapImage bitmapSource)
        {
            // 图像的宽度和高度
            int width = bitmapSource.PixelWidth;
            int height = bitmapSource.PixelHeight;
            // 图像的格式
            PixelFormat pixelFormat = bitmapSource.Format;
            // 每行的字节
            int stride = (width * pixelFormat.BitsPerPixel + 7) / 8;
            // 调色板
            BitmapPalette Palette = new BitmapPalette(bitmapSource, 256);
            // 像素数组
            byte[] pixels = new byte[stride * height];
            // 阈值
            byte Thre = 128;

            // 复制像素到像素数组
            bitmapSource.CopyPixels(Int32Rect.Empty, pixels, stride, 0);

            //MessageBox.Show("Width: " + bitmapSource.Width.ToString() + "PixelWidth: " + bitmapSource.PixelWidth.ToString() + "other: " + bitmapSource.DecodePixelWidth.ToString() + "Bit: " + pixelFormat.BitsPerPixel.ToString());

            //gray=BYTE(0.299*red)+BYTE(0.587*green)+BYTE(0.114*blue); 

            for (int i = 0; i < pixels.LongLength; i += 4)
            {
                byte gray = ( (byte)(0.299 * pixels[i] + 0.587 * pixels[i + 1] + 0.114 * pixels[i + 2]) > Thre )? (byte)255 : (byte)0;
                pixels[i] = gray;
                pixels[i + 1] = gray;
                pixels[i + 2] = gray;
            }

            WriteableBitmap bitmap = new WriteableBitmap(BitmapSource.Create(width, height, bitmapSource.DpiX, bitmapSource.DpiY, pixelFormat, null, pixels, stride));
            return bitmap;
        }
    }
}
