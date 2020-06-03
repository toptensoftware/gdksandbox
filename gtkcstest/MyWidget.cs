using System;
using System.Runtime.InteropServices;
using SkiaSharp;
using GuiKit.Interop.Gtk;
using static GuiKit.Interop.Gtk.Functions;

namespace gtkcstest
{
    static class MyWidget
    {
        static MyWidget()
        {
            // ComRegisterFunctionAttribute class
            typeId = g_type_register_static_simple(
                gtk_widget_get_type(), "mywidget", 
                Marshal.SizeOf<MyWidgetClass>(), InitClass,
                Marshal.SizeOf<MyWidgetInstance>(), InitInstance,
                0);
        }

        public static readonly IntPtr typeId;

        [StructLayout(LayoutKind.Sequential)]
        struct MyWidgetClass
        {
            public GtkWidgetClass parent_class;
        }

        [StructLayout(LayoutKind.Sequential)]
        struct MyWidgetInstance
        {
            public GtkWidget b;
        }

        public static IntPtr New()
        {
            return g_object_new_with_properties(typeId, 0, IntPtr.Zero, IntPtr.Zero);
        }

        static void InitClass(IntPtr classPtr, IntPtr classData)
        {
            unsafe
            {
                MyWidgetClass* pClass = (MyWidgetClass*)classPtr;
                pClass->parent_class.realize = Immortal<Action_IntPtr>(realize);
                pClass->parent_class.size_allocate = Immortal<Action_IntPtr_IntPtr>(size_allocate);
                pClass->parent_class.draw = Immortal<Action_IntPtr_IntPtr>(draw);
            }
        }

        static void InitInstance(IntPtr instancePtr, IntPtr classPtr)
        {
            gtk_widget_set_has_window(instancePtr, true); 
        }

        static void realize(IntPtr self)
        {
            GdkRectangle allocation;
            gtk_widget_get_allocation(self, out allocation);

            gtk_widget_set_realized(self, true);

            GdkWindowAttr attributes = new GdkWindowAttr();
            attributes.x = allocation.x;
            attributes.y = allocation.y;
            attributes.width = allocation.width;
            attributes.height = allocation.height;
            attributes.window_type = GdkWindowType.GDK_WINDOW_CHILD;
            attributes.event_mask = gtk_widget_get_events(self)
                                    | GdkEventMask.GDK_BUTTON_MOTION_MASK
                                    | GdkEventMask.GDK_BUTTON_PRESS_MASK
                                    | GdkEventMask.GDK_BUTTON_RELEASE_MASK
                                    | GdkEventMask.GDK_EXPOSURE_MASK
                                    | GdkEventMask.GDK_ENTER_NOTIFY_MASK
                                    | GdkEventMask.GDK_LEAVE_NOTIFY_MASK;
            attributes.visual = gtk_widget_get_visual(self);
            attributes.wclass = GdkWindowWindowClass.GDK_INPUT_OUTPUT;
            GdkWindowAttributesType attributes_mask = GdkWindowAttributesType.GDK_WA_X | GdkWindowAttributesType.GDK_WA_Y | GdkWindowAttributesType.GDK_WA_VISUAL;
            IntPtr window = gdk_window_new(gtk_widget_get_parent_window(self), ref attributes, attributes_mask);
            gtk_widget_set_window(self, window);
            gtk_widget_register_window(self, window);
        }

        static unsafe void size_allocate(IntPtr self, IntPtr allocation)
        {
            GdkRectangle* pAllocation = (GdkRectangle*)allocation;
            gtk_widget_set_allocation(self, ref *pAllocation);

            GdkRectangle child_allocation;
            child_allocation.x = 0;
            child_allocation.y = 0;
            child_allocation.width = pAllocation->width;
            child_allocation.height = pAllocation->height;

            if (gtk_widget_get_realized(self) && gtk_widget_get_has_window(self))
            {
                gdk_window_move_resize(gtk_widget_get_window (self),
                                        pAllocation->x,
                                        pAllocation->y,
                                        child_allocation.width,
                                        child_allocation.height);
            }
        }

        static void draw(IntPtr self, IntPtr cr)
        {
            GdkRectangle alloc;
            gtk_widget_get_allocation(self, out alloc);

            /*
            cairo_set_source_rgb (cr, 1,0,0);
            cairo_rectangle (cr, 10, 10, alloc.width - 20, alloc.height - 20);
            cairo_fill (cr);
            */

            // Create bitmap
            using (var bitmap =  new SKBitmap(alloc.width, alloc.height, SKColorType.Bgra8888, SKAlphaType.Premul))
            {
                IntPtr length;
                IntPtr pixels = bitmap.GetPixels(out length);
                int stride = bitmap.BytesPerPixel * bitmap.Width;

                // Draw something
                using (var sksurface = SKSurface.Create(bitmap.Info, pixels, stride))
                {
//                    sksurface.Canvas.Clear();
                    sksurface.Canvas.DrawRect(0, 0, alloc.width, alloc.height, new SKPaint() { Color = new SKColor(255,0,0,255),  IsStroke = false });
                    sksurface.Canvas.DrawRoundRect(new SKRoundRect(new SKRect(20, 20, alloc.width - 20, alloc.height - 20), 20, 20), new SKPaint() { Color = new SKColor(0,0,255,255),  IsStroke = false, IsAntialias = true });
                    sksurface.Canvas.Flush();
                }

                // Blt the surface
                var surface = cairo_image_surface_create_for_data(pixels, cairo_format_t.CAIRO_FORMAT_ARGB32, alloc.width, alloc.height, stride);
                cairo_set_source_surface(cr, surface, 0, 0);
                cairo_set_operator(cr, cairo_operator_t.CAIRO_OPERATOR_SOURCE);
                cairo_rectangle(cr, 0, 0, alloc.width, alloc.height);
                cairo_fill(cr);
                cairo_surface_destroy(surface);
            }
        }
    }
}