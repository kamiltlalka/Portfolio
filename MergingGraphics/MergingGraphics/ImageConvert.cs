using System.Drawing;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;
using System.Diagnostics;
using System.Threading;

namespace MergingGraphics
{
    internal class ImageConvert
    {
        static int endedThreads = 0;
        static bool started = true;


        [DllImport("C:\\Users\\tlaka\\source\\repos\\MergingGraphics\\x64\\Debug\\DLLcpp.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MyFunctionFromDll(int[] FirstPic, int[] SecondPic, int size, float procentage);

        [DllImport("C:\\Users\\tlaka\\source\\repos\\MergingGraphics\\x64\\Debug\\JAAasm.dll")]
        static extern IntPtr MyFunctionFromASM(int[] FirstPic, int[] SecondPic, int size, float procentage);

        //size = x*y*3


        public byte[] imageToByteArray(System.Drawing.Image imageIn, int sizex, int sizey)
        {

            byte[] outBit = new byte[sizex * sizey * 4];

            using (Bitmap bmp = new Bitmap(imageIn))
            {
                int counter = 0;


                for (int i = 0;i<sizex;i++)
                {
                    for (int j = 0; j < sizey; j++)
                    {

                        outBit[ counter] = bmp.GetPixel(i,j).R;
                        outBit[(counter+1)] = bmp.GetPixel(i,j).G;
                        outBit[(counter + 2)] = bmp.GetPixel(i,j).B;
                        counter += 3;

                    }

                }
            
            }

            return outBit;
        }

        public Bitmap byteArrayToImage(byte[] byteArrayIn, int sizex, int sizey)
        {

            

            using (Bitmap redBmp = new Bitmap(sizex, sizey, System.Drawing.Imaging.PixelFormat.Format24bppRgb))
            {
                int counter = 0;

                for (int i = 0; i < sizex; i++)
                {
                    for (int j = 0; j < sizey; j++)
                    {

                        redBmp.SetPixel(i, j, Color.FromArgb(byteArrayIn[+counter+2], byteArrayIn[counter], byteArrayIn[counter+1]));
                        counter += 3;
                    }

                }
                redBmp.Save(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\obrazki\\wyjscie.png", ImageFormat.Png);
                return redBmp;
            }




        }
        public byte[] cppmodify(byte[] FirstPic, byte[] SecondPic, int sizex, int sizey, float procentage, int threads)
        {
            Stopwatch sw = new Stopwatch();

            int[] firstpic = byteToInt(FirstPic, sizex, sizey);
            int[] secondpic = byteToInt(SecondPic, sizex, sizey);
            int[] result = new int[(sizex * sizey * 3)];
            float realratio = procentage * (float)0.01;
            int ratio = (sizex * sizey * 3) / threads;

            int factor = (sizex * sizey * 3) % threads;

            int[][] holder1 = new int[threads][];
            int[][] holder2 = new int[threads][];
            for (int k = 0; k < threads; k++) holder1[k] = new int[ratio + threads];
            for (int k = 0; k < threads; k++) holder2[k] = new int[ratio + threads];

            sw.Start();
            for (int i = 0; i < threads; i++)
            {
                
                if (i < threads - 1)
                {

                    
                    Array.Copy(FirstPic, (i * ratio), holder1[i], 0, ratio + threads);
                    Array.Copy(SecondPic, (i * ratio), holder2[i], 0, ratio + threads);
                   // cppfunc(holder1[i], holder2[i], (ratio + 0), realratio, result , i,ratio,threads);
                   
                    Thread newThread = new Thread(unused => cppfunc(holder1[i], holder2[i], (ratio + factor), realratio, result, i, ratio, factor));
                    newThread.Name = i+"";
                    newThread.Start();

                    while (started)
                    { }

                    started = true;


                }
                else {


                    int[] holder11 = new int[ratio + factor];
                    int[] holder22 = new int[ratio + factor];
                    Array.Copy(firstpic, (i * ratio), holder11, 0, ratio + factor);
                    Array.Copy(secondpic, (i * ratio), holder22, 0, ratio + factor);

                    IntPtr ptrtoint = MyFunctionFromDll(holder11, holder22, (ratio + factor), realratio); // extern function

                    IntPtr start = IntPtr.Add(ptrtoint, 4);
                    Marshal.Copy(start, result, i * ratio, (ratio + factor));
                    if(threads<2) Thread.Sleep(1);

                }
                
            }
            while (endedThreads < threads - 1)
            {
                
            }



            sw.Stop();


            byte[] outarray = intToByte(result, sizex, sizey);
            
            Console.WriteLine("Cpp threads = " + threads + " time = " + sw.Elapsed);

            return outarray;

        }

        public static void cppfunc(int[] FirstPic, int[] SecondPic, int size, float procentage, int[] output, int i, int ratio, int factor)
        {
            started = false;
            int number = Int32.Parse(Thread.CurrentThread.Name) ;

            IntPtr ptrtoint = MyFunctionFromDll(FirstPic, SecondPic, (ratio + factor), procentage); // extern function
            IntPtr start = IntPtr.Add(ptrtoint, 4);
            Marshal.Copy(start, output, number * ratio, ratio);

            Interlocked.Increment(ref endedThreads);
        }

        

        public byte[] asmmodify(byte[] FirstPic, byte[] SecondPic, int sizex, int sizey, float procentage, int threads)
        {
            Stopwatch sw = new Stopwatch();

            int[] firstpic = byteToInt(FirstPic, sizex, sizey);
            int[] secondpic = byteToInt(SecondPic, sizex, sizey);
            int[] result = new int[(sizex * sizey * 3)];
            float realratio = procentage * (float)0.01;
            int ratio = (sizex * sizey * 3) / threads;

            int factor = (sizex * sizey * 3) % threads;

            int[][] holder1 = new int[threads][];
            int[][] holder2 = new int[threads][];
            for (int k = 0; k < threads; k++) holder1[k] = new int[ratio + threads];
            for (int k = 0; k < threads; k++) holder2[k] = new int[ratio + threads];

            sw.Start();
            for (int i = 0; i < threads; i++)
            {

                if (i < threads - 1)
                {


                    Array.Copy(FirstPic, (i * ratio), holder1[i], 0, ratio + threads);
                    Array.Copy(SecondPic, (i * ratio), holder2[i], 0, ratio + threads);

                    // cppfunc(holder1[i], holder2[i], (ratio + 0), realratio, result , i,ratio,threads);

                    Thread newThread = new Thread(unused => asmfunc(holder1[i], holder2[i], (ratio + factor), realratio, result, i, ratio, factor));
                    newThread.Name = i + "";
                    newThread.Start();

                    while (started)
                    { }

                    started = true;


                }
                else
                {


                    int[] holder11 = new int[ratio + factor];
                    int[] holder22 = new int[ratio + factor];
                    Array.Copy(firstpic, (i * ratio), holder11, 0, ratio + factor);
                    Array.Copy(secondpic, (i * ratio), holder22, 0, ratio + factor);

                    IntPtr ptrtoint = MyFunctionFromASM(holder11, holder22, (ratio + factor), realratio); // extern function

                    IntPtr start = IntPtr.Add(ptrtoint, 4);
                    Marshal.Copy(start, result, i * ratio, (ratio + factor));


                }

            }
            while (endedThreads < threads - 1)
            {
                Thread.Sleep(1);
            }



            sw.Stop();


            byte[] outarray = intToByte(result, sizex, sizey);

            Console.WriteLine("Asm threads = " + threads + " time = " + sw.Elapsed);

            return outarray;

        }

        public static void asmfunc(int[] FirstPic, int[] SecondPic, int size, float procentage, int[] output, int i, int ratio, int factor)
        {
            started = false;
            int number = Int32.Parse(Thread.CurrentThread.Name);

            IntPtr ptrtoint = MyFunctionFromASM(FirstPic, SecondPic, (ratio + factor), procentage); // extern function
            IntPtr start = IntPtr.Add(ptrtoint, 4);
            Marshal.Copy(start, output, number * ratio, ratio);

            Interlocked.Increment(ref endedThreads);
        }
        int[] byteToInt(byte[] Pic, int sizex, int sizey)
        {
            int[] outint = new int[sizex * sizey * 3];
            for (int i = 0; i < (sizex * sizey * 3); i++)
            {
                outint[i] = Pic[i];
            
            }
            return outint;
        }

        byte[] intToByte(int[] Pic, int sizex, int sizey)
        {
            byte[] outint = new byte[sizex * sizey * 3];
            for (int i = 0; i < (sizex * sizey * 3); i++)
            {
                outint[i] = (byte)Pic[i];

            }
            return outint;
        }


    }
}