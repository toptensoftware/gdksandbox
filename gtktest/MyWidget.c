#include <gtk/gtk.h>
#include "MyWidget.h"

G_DEFINE_TYPE(MyWidget, mywidget, GTK_TYPE_WIDGET)


static void     mywidget_realize       (GtkWidget        *widget);
static void     mywidget_unrealize     (GtkWidget        *widget);
static void     mywidget_map           (GtkWidget        *widget);
static void     mywidget_unmap         (GtkWidget        *widget);
static gboolean mywidget_draw          (GtkWidget        *widget, cairo_t          *cr);
static void     mywidget_size_allocate (GtkWidget        *widget, GtkAllocation    *allocation);

GdkPixbuf* pixbuf;


static void mywidget_class_init(MyWidgetClass* klass )
{
  GtkWidgetClass *widget_class = GTK_WIDGET_CLASS(klass);
  widget_class->realize = mywidget_realize;
  widget_class->unrealize = mywidget_unrealize;
  widget_class->map = mywidget_map;
  widget_class->unmap = mywidget_unmap;
  widget_class->draw = mywidget_draw;
  widget_class->size_allocate = mywidget_size_allocate;

  GError* pErr = NULL;
  pixbuf = gdk_pixbuf_new_from_file("/home/brad/Projects/gtktest/tts.png", &pErr);
  if (!pixbuf)
  {
    printf("Failed to load image %s", pErr->message);
  }
}

static void mywidget_init(MyWidget* self )
{
	gtk_widget_set_has_window(GTK_WIDGET(self), TRUE);
//  gtk_widget_set_halign(GTK_WIDGET(self), GTK_ALIGN_FILL);
  //gtk_widget_set_valign(GTK_WIDGET(self), GTK_ALIGN_FILL);
}

GtkWidget* mywidget_new(void)
{
  return g_object_new(GTK_TYPE_MYWIDGET, NULL);
}


static void
mywidget_realize (GtkWidget *widget)
{
  GtkAllocation allocation;
  gtk_widget_get_allocation (widget, &allocation);

  gtk_widget_set_realized (widget, TRUE);

  GdkWindowAttr attributes;
  attributes.x = allocation.x;
  attributes.y = allocation.y;
  attributes.width = allocation.width;
  attributes.height = allocation.height;
  attributes.window_type = GDK_WINDOW_CHILD;
  attributes.event_mask = gtk_widget_get_events (widget)
                        | GDK_BUTTON_MOTION_MASK
                        | GDK_BUTTON_PRESS_MASK
                        | GDK_BUTTON_RELEASE_MASK
                        | GDK_EXPOSURE_MASK
                        | GDK_ENTER_NOTIFY_MASK
                        | GDK_LEAVE_NOTIFY_MASK;

  printf("realize: %i %i\n", attributes.width, attributes.height);

  attributes.visual = gtk_widget_get_visual (widget);
  attributes.wclass = GDK_INPUT_OUTPUT;
  gint attributes_mask = GDK_WA_X | GDK_WA_Y | GDK_WA_VISUAL;
  GdkWindow *window = gdk_window_new (gtk_widget_get_parent_window (widget),
                          &attributes, attributes_mask);
  gtk_widget_set_window (widget, window);
  gtk_widget_register_window (widget, window);
}

static void
mywidget_unrealize (GtkWidget *widget)
{
  GTK_WIDGET_CLASS (mywidget_parent_class)->unrealize (widget);
}
static void
mywidget_map (GtkWidget *widget)
{
  GTK_WIDGET_CLASS (mywidget_parent_class)->map (widget);
}

static void
mywidget_unmap (GtkWidget *widget)
{
  GTK_WIDGET_CLASS(mywidget_parent_class)->unmap (widget);
}

static void
mywidget_size_allocate (GtkWidget     *widget,
                             GtkAllocation *allocation)
{
  GtkAllocation child_allocation;
  gtk_widget_set_allocation (widget, allocation);
  child_allocation.x = 0;
  child_allocation.y = 0;
  child_allocation.width = allocation->width;
  child_allocation.height = allocation->height;

  printf("size alloc: %i %i\n", allocation->width, allocation->height);

  if (gtk_widget_get_realized (widget))
  {
     if (gtk_widget_get_has_window (widget))
        gdk_window_move_resize (gtk_widget_get_window (widget),
                                allocation->x,
                                allocation->y,
                                child_allocation.width,
                                child_allocation.height);
  }
}


static gboolean mywidget_draw(GtkWidget* widget, cairo_t* cr)
{
  GtkAllocation alloc;
  gtk_widget_get_allocation(widget, &alloc);
  cairo_set_source_rgb (cr, 1,0,0);
  cairo_rectangle (cr, 10, 10, alloc.width - 20, alloc.height - 20);
  cairo_fill (cr);

  if (pixbuf)
  {
    cairo_format_t format = gdk_pixbuf_get_has_alpha(pixbuf) ? CAIRO_FORMAT_ARGB32 : CAIRO_FORMAT_RGB24;
    GdkColorspace cs = gdk_pixbuf_get_colorspace(pixbuf);

    cairo_surface_t* surface = cairo_image_surface_create_for_data(
      gdk_pixbuf_get_pixels(pixbuf), 
      gdk_pixbuf_get_has_alpha(pixbuf) ? CAIRO_FORMAT_ARGB32 : CAIRO_FORMAT_RGB24,
      gdk_pixbuf_get_width(pixbuf),
      gdk_pixbuf_get_height(pixbuf),
      gdk_pixbuf_get_rowstride(pixbuf)
    );


    cairo_set_source_surface(cr, surface, 10, 10);
    cairo_rectangle (cr, 10, 10, alloc.width - 20, alloc.height - 20);
    cairo_fill (cr);

    cairo_surface_destroy(surface);
  }
}