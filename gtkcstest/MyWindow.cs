using System;
using System.Runtime.InteropServices;
using SkiaSharp;
using GuiKit.Interop.Gtk;
using static GuiKit.Interop.Gtk.Functions;
using System.Collections.Generic;

namespace gtkcstest
{
    static class MyWindow
    {
        static MyWindow()
        {
            // ComRegisterFunctionAttribute class
            typeId = g_type_register_static_simple(
                gtk_window_get_type(), "mywindow", 
                Marshal.SizeOf<MyWindowClass>(), InitClass,
                Marshal.SizeOf<MyWindowInstance>(), InitInstance,
                0);
        }

        public static readonly IntPtr typeId;

        [StructLayout(LayoutKind.Sequential)]
        struct MyWindowClass
        {
            public GtkWindowClass parent_class;
        }

        [StructLayout(LayoutKind.Sequential)]
        struct MyWindowInstance
        {
            public GtkWindow b;
        }

        public static IntPtr New(IntPtr app)
        {
            /*
            return g_object_new_with_properties(typeId, new Dictionary<string, object>() {
                { "application", app }
            });
            */

            return g_object_new_with_properties(typeId, 0, IntPtr.Zero, IntPtr.Zero);
        }

        static void InitClass(IntPtr classPtr, IntPtr classData)
        {
            unsafe
            {
                Override(ref ((GtkWidgetClass*)classPtr)->size_allocate, size_allocate, out base_size_allocate);
                Override(ref ((GtkWidgetClass*)classPtr)->configure_event, configure_event, out base_configure_event);
                Override(ref ((GtkWidgetClass*)classPtr)->window_state_event, window_state_event, out base_window_state_event);
                Override(ref ((GtkWidgetClass*)classPtr)->realize, realize, out base_realize);
                Override(ref ((GtkWidgetClass*)classPtr)->map, map, out base_map);
            }
        }


        static void InitInstance(IntPtr instancePtr, IntPtr classPtr)
        {
        }

        static bool initial_configure_pending = true;
        static int xTarget = 2469;
        static int yTarget = 76;
        static int xFrameDelta = 0;
        static int yFrameDelta = 35;

        static Action_IntPtr base_realize;
        static unsafe void realize(IntPtr self)
        {


            //gtk_window_fullscreen(self);
            Console.WriteLine(" -- WillRealize --");
            base_realize?.Invoke(self);
            Console.WriteLine(" -- DidRealize --");


            Console.WriteLine(" -- WillMove --");
            gtk_window_move(self, xTarget - xFrameDelta, yTarget - yFrameDelta);
            Console.WriteLine(" -- DidMove --");

            Console.WriteLine(" -- WillSize --");
            gtk_window_resize(self, 400, 300);
            Console.WriteLine(" -- DidSize --");

            gdk_window_get_origin(gtk_widget_get_window(self), out var x, out var y);
            gdk_window_get_frame_extents(gtk_widget_get_window(self), out var rect);
        }

        static Action_IntPtr base_map;
        static unsafe void map(IntPtr self)
        {
            Console.WriteLine(" -- WillMap --");
            base_map?.Invoke(self);
            Console.WriteLine(" -- DidMap --");
        }


        static Action_IntPtr_IntPtr base_size_allocate;
        static void size_allocate(IntPtr self, IntPtr allocation)
        {
            base_size_allocate?.Invoke(self, allocation);

            unsafe
            {
                GdkRectangle* pRect = (GdkRectangle*)allocation;
                //Console.WriteLine($"Allocate: {pRect->x},{pRect->y} x {pRect->width},{pRect->height}");
            }

            /*
             GTK_WIDGET_CLASS (my_application_window_parent_class)->size_allocate (widget,
                                                                        allocation);
            */
        }

        static Action_IntPtr_IntPtr base_window_state_event;
        static void window_state_event(IntPtr self, IntPtr ev)
        {
            base_window_state_event?.Invoke(self, ev);
            unsafe
            {
                GdkEventWindowState* pev = (GdkEventWindowState*)ev;
                //Console.WriteLine($"Window State: Maximized: {pev->new_window_state.HasFlag(GdkWindowState.GDK_WINDOW_STATE_MAXIMIZED)}");
            }
        }

        static Action_IntPtr_IntPtr_IntPtr base_configure_event;
        static void configure_event(IntPtr self, IntPtr ev, IntPtr data)
        {
            base_configure_event?.Invoke(self, ev, data);

            unsafe
            {
                var pev = (GdkEventConfigure*)ev;



                Console.WriteLine($"---");
                Console.WriteLine($"Configure: {pev->x},{pev->y} x {pev->width},{pev->height}");

                gdk_window_get_root_origin(gtk_widget_get_window(self), out var xF, out var yF);
                gdk_window_get_origin(gtk_widget_get_window(self), out var xC, out var yC);
                gdk_window_get_frame_extents(gtk_widget_get_window(self), out var rect);
                Console.WriteLine($"Extents      : {rect.x},{rect.y} x {rect.width},{rect.height}");
                Console.WriteLine($"Origin       : {xC},{yC}");
                Console.WriteLine($"Root Origin  : {xC},{yC}");

                Console.WriteLine($"---");

                if (initial_configure_pending && (pev->x != rect.x || pev->y != rect.y))
                {
                    int new_yFrameDelta = pev->y - rect.y;
                    int new_xFrameDelta = pev->x - rect.x;

                    if (new_yFrameDelta != yFrameDelta || new_xFrameDelta != xFrameDelta)
                    {
                        Console.WriteLine("Window needs moving...");
                        gtk_window_move(self, rect.x - (new_xFrameDelta - xFrameDelta), rect.y - (new_yFrameDelta - yFrameDelta));
                    }

                    xFrameDelta = new_xFrameDelta;
                    yFrameDelta = new_yFrameDelta;

                    Console.WriteLine($"Frame adjustment updated to {xFrameDelta},{yFrameDelta}");
                    initial_configure_pending = false;
                }
            }
        }
    }
}