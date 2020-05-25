#include <gdk/gdk.h>
#include <glib.h>

//////////////////////////////////////////////////////////////////////////////////////////////////////////
// Main

GMainLoop* mainloop;

void event_func(GdkEvent *event, gpointer data)
{
    printf("Event %i!\n", event->any.type);
    if (event->any.type == GDK_DELETE)
    {
        g_main_loop_quit(mainloop);
        return;
    }

/*
    if (event->any.type == GDK_EXPOSE)
    {
        GdkWindow* window = event->any.window;

        // Get a context
        cairo_region_t* region = gdk_window_get_visible_region(window);
        GdkDrawingContext* ctx = gdk_window_begin_draw_frame(window, region);

        // Get a cairo context
        cairo_t* cr = gdk_drawing_context_get_cairo_context(ctx);

        // Draw something!
        cairo_set_source_rgb (cr, 1,0,0);
        cairo_rectangle (cr, 0, 0, 10000, 10000);
        cairo_fill (cr);

        // Clean up
        gdk_window_end_draw_frame(window, ctx);
        cairo_region_destroy(region);
    }
*/
}

int
main (int argc, char **argv)
{
    // Init GDK
    if (!gdk_init_check (&argc, &argv)) {
        return FALSE;
    }

    // Create the window
    GdkWindowAttr attributes;
    memset(&attributes, 0, sizeof(attributes));
    attributes.window_type = GDK_WINDOW_TOPLEVEL;
    attributes.width = 400;
    attributes.height = 400;
    attributes.wclass = GDK_INPUT_OUTPUT;
    GdkWindow* window = gdk_window_new(NULL, &attributes, 0);

    gdk_event_handler_set(event_func, NULL, NULL);

    // Create event loop
    mainloop = g_main_loop_new(NULL, TRUE);


    // Show it
    gdk_window_show(window);

    // Run it
    g_main_loop_run(mainloop);

    gdk_window_destroy(window);

    // Clean up
    g_main_loop_unref(mainloop);
}

