using System;
using System.Runtime.InteropServices;
using GuiKit.Interop.Gtk;
using static GuiKit.Interop.Gtk.Functions;

namespace gtkcstest
{
    class Program
    {
        static IntPtr g_app;

        static void quitApp(IntPtr sender)
        {
            g_application_release(g_app);
            g_application_quit(g_app);
        }

        static void activate(IntPtr app, IntPtr user_data)
        {
//            var window = gtk_application_window_new(app);
            var window = MyWindow.New(app);
            gtk_window_set_title(window, "My C# GTK Window");
            gtk_window_set_default_size(window, 200, 200);


            var vbox = gtk_box_new(GtkOrientation.GTK_ORIENTATION_VERTICAL, 0);
            gtk_container_add(window, vbox);

            var menubar = gtk_menu_bar_new();
            var fileMenu = gtk_menu_new();

            var fileMi = gtk_menu_item_new();
            gtk_menu_item_set_label(fileMi, "File");

            var quitMi =gtk_menu_item_new();
            gtk_menu_item_set_label(quitMi, "Quit");

            g_signal_connect(quitMi, "activate", Immortal<Action_IntPtr>(quitApp), IntPtr.Zero);

            gtk_menu_item_set_submenu(fileMi, fileMenu);
            gtk_menu_shell_append(fileMenu, quitMi);
            gtk_menu_shell_append(menubar, fileMi);
            gtk_box_pack_start(vbox, menubar, false, false, 0);

            var widget = MyWidget.New();
            gtk_box_pack_start(vbox, widget, true, true, 0);


            gtk_widget_show_all(window);
        }

        static int Main(string[] args)
        {
            gtk_init(ref args);

            var display = gdk_display_get_default();
            var monitors = gdk_display_get_n_monitors(display);
            var primary_monitor = gdk_display_get_primary_monitor(display);
            for (int i=0; i<monitors; i++)
            {
                var monitor = gdk_display_get_monitor(display, i);
                GdkRectangle r;
                gdk_monitor_get_geometry(monitor, out r);
                bool primary = monitor == primary_monitor;
                Console.WriteLine($"Monitor {i}: {r.x},{r.y} x {r.width},{r.height} @ {gdk_monitor_get_scale_factor(monitor)} [{primary}]");
            }

            Console.WriteLine($"SizeOf<GtkApplicationWindow>: {Marshal.SizeOf<GtkApplicationWindow>()}");
            Console.WriteLine($"SizeOf<GtkApplicationWindowClass>: {Marshal.SizeOf<GtkApplicationWindowClass>()}");
            Console.WriteLine($"SizeOf<GdkEventConfigure>: {Marshal.SizeOf<GdkEventConfigure>()}");


            g_app = gtk_application_new("com.toptensoftware.example", GApplicationFlags.G_APPLICATION_FLAGS_NONE);

            Action_IntPtr_IntPtr del = activate;
            g_signal_connect(g_app, "activate", del, IntPtr.Zero);

            g_application_hold(g_app);
            int status = g_application_run(g_app, args);
            g_object_unref(g_app);
            return status;
        }
    }
}
