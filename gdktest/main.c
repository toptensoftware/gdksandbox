#include <gdk/gdk.h>
#include <glib.h>

GMainLoop* g_mainloop;

void event_func(GdkEvent *event, gpointer data);

int main (int argc, char **argv)
{
    // Init GDK
    if (!gdk_init_check (&argc, &argv)) 
    {
        return 7;
    }

    // Setup event handler
    gdk_event_handler_set(event_func, NULL, NULL);

    // Create the window
    GdkWindowAttr attributes;
    memset(&attributes, 0, sizeof(attributes));
    attributes.event_mask = GDK_ALL_EVENTS_MASK;
    attributes.width = 640;
    attributes.height = 480;
    attributes.wclass = GDK_INPUT_OUTPUT;
    attributes.window_type = GDK_WINDOW_TOPLEVEL;
    attributes.override_redirect = FALSE;
    attributes.type_hint = GDK_WINDOW_TYPE_HINT_NORMAL;
    GdkWindow* window = gdk_window_new(NULL, &attributes, 0);

    gdk_window_set_title(window, "GDK Sandbox");

    // Create event loop
    g_mainloop = g_main_loop_new(NULL, TRUE);

    // Show it
    gdk_window_show(window);

    // Run it
    g_main_loop_run(g_mainloop);

    // Clean up
    gdk_window_destroy(window);
    g_main_loop_unref(g_mainloop);

    return 0;
}


void event_func(GdkEvent *event, gpointer data)
{
    printf("Event %i!\n", event->any.type);

    if (event->any.type == GDK_DELETE)
    {
        g_main_loop_quit(g_mainloop);
    }

    if (event->any.type == GDK_CONFIGURE)
    {
        printf("Size: %i %i\n", event->configure.width, event->configure.height);
        gdk_window_move_resize (event->configure.window, event->configure.x, event->configure.y, event->configure.width, event->configure.height);
    }

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
}
