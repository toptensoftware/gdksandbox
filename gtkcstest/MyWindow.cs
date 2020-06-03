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

        static Action_IntPtr base_realize;
        static unsafe void realize(IntPtr self)
        {
            gtk_window_move(self, 2000, 100);
            gtk_window_resize(self, 300, 300);
            gtk_window_fullscreen(self);
            base_realize?.Invoke(self);
        }

        static Action_IntPtr base_map;
        static unsafe void map(IntPtr self)
        {
            base_map?.Invoke(self);
            //gdk_window_move(gtk_widget_get_window(self), 500, 50);
            //gdk_window_move_resize(gtk_widget_get_window(self), 1970, 320, 300, 300);
        }


        static Action_IntPtr_IntPtr base_size_allocate;
        static void size_allocate(IntPtr self, IntPtr allocation)
        {
            base_size_allocate?.Invoke(self, allocation);

            unsafe
            {
                GdkRectangle* pRect = (GdkRectangle*)allocation;
                Console.WriteLine($"Allocate: {pRect->x},{pRect->y} x {pRect->width},{pRect->height}");
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
                Console.WriteLine($"Window State: Maximized: {pev->new_window_state.HasFlag(GdkWindowState.GDK_WINDOW_STATE_MAXIMIZED)}");
            }
        }

        static Action_IntPtr_IntPtr_IntPtr base_configure_event;
        static void configure_event(IntPtr self, IntPtr ev, IntPtr data)
        {
            unsafe
            {
                var pev = (GdkEventConfigure*)ev;
                Console.WriteLine($"Configure: {pev->x},{pev->y} x {pev->width},{pev->height}");
            }

            base_configure_event?.Invoke(self, ev, data);
        }
}
}