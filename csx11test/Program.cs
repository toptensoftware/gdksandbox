using System;
using System.Runtime.InteropServices;
using GuiKit.Interop.X11;
using SkiaSharp;
using static GuiKit.Interop.X11.Functions;

// Transparent windows...
// https://stackoverflow.com/questions/39906128/how-to-create-semi-transparent-white-window-in-xlib
// https://magcius.github.io/xplain/article/composite.html

namespace csx11test
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"{Marshal.SizeOf<XImage>()}");

            var display = XOpenDisplay(null);

            var screen = DefaultScreen(display);

            int width = 660;
            int height = 200;

            var win = XCreateSimpleWindow(display, RootWindow(display, screen), 500, 100, width, height, 1,
                                   BlackPixel(display, screen), WhitePixel(display, screen));
            XSelectInput(display, win, InputEventMask.ExposureMask | InputEventMask.KeyPressMask | InputEventMask.PointerMotionMask);
            XMapWindow(display, win);
            XStoreName(display, win, "X11 C# Test");

            var WM_DELETE_WINDOW = XInternAtom(display, "WM_DELETE_WINDOW", false); 
            XSetWMProtocols(display, win, WM_DELETE_WINDOW);  

            // Create a bitmap
            var bitmap =  new SKBitmap(width, height, SKColorType.Bgra8888, SKAlphaType.Premul);
            IntPtr length;
            IntPtr pixels = bitmap.GetPixels(out length);
            int stride = bitmap.BytesPerPixel * bitmap.Width;

            // Draw something
            using (var sksurface = SKSurface.Create(bitmap.Info, pixels, stride))
            {
                sksurface.Canvas.Clear();
                sksurface.Canvas.DrawRect(0, 0, width, height, new SKPaint() { Color = new SKColor(255,0,0,255),  IsStroke = false });
                sksurface.Canvas.DrawRoundRect(new SKRoundRect(new SKRect(20, 20, width - 20, height - 20), 20, 20), new SKPaint() { Color = new SKColor(0,0,255,255),  IsStroke = false, IsAntialias = true });
                sksurface.Canvas.Flush();
            }

            // Create an image
            var visual = DefaultVisual(display, screen);
            var depth = DefaultDepth(display, screen);
            var img = XCreateImage(display, visual, depth, ImageFormat.ZPixmap, 0, pixels, (uint)width, (uint)height, 32, 0);


            while (true)
            {
                XNextEvent(display, out var e);

                switch (e.type)
                {
                    case InputEventType.KeyPress:
                        KeySym keySym;
                        var buf = new byte[128];
                        XLookupString(ref e.xkey, buf, out keySym, IntPtr.Zero);
                        if (keySym == KeySym.XK_Escape)
                            goto quit;
                        break;

                    case InputEventType.MotionNotify:
                        Console.WriteLine($"Motion: {e.xmotion.x_root},{e.xmotion.y_root}");
                        break;

                    case InputEventType.ClientMessage:
                        if (e.xclient.data0 == (long)WM_DELETE_WINDOW)
                            goto quit;
                        break;

                    case InputEventType.Expose:
                        XPutImage(display, win, DefaultGC(display, screen), img, 0, 0, 0, 0, width, height);
                        break;
                }

            }

quit:
            XDestroyWindow(display, win);
            XCloseDisplay(display);

        }
    }
}
