#ifndef _MYWIDGET_H
#define _MYWIDGET_H

#include <gtk/gtk.h>

#define GTK_TYPE_MYWIDGET                 (mywidget_get_type ())
#define GTK_MYWIDGET(obj)                 (G_TYPE_CHECK_INSTANCE_CAST ((obj), GTK_TYPE_MYWIDGET, MyWidget))
#define GTK_MYWIDGET_CLASS(klass)         (G_TYPE_CHECK_CLASS_CAST ((klass), GTK_TYPE_MYWIDGET, MyWidgetClass))
#define GTK_IS_MYWIDGET(obj)              (G_TYPE_CHECK_INSTANCE_TYPE ((obj), GTK_TYPE_MYWIDGET))
#define GTK_IS_MYWIDGET_CLASS(klass)      (G_TYPE_CHECK_CLASS_TYPE ((klass), GTK_TYPE_MYWIDGET))
#define GTK_MYWIDGET_GET_CLASS(obj)       (G_TYPE_INSTANCE_GET_CLASS ((obj), GTK_TYPE_MYWIDGET, MyWidgetClass))

typedef struct _MyWidget             MyWidget;
typedef struct _MyWidgetClass        MyWidgetClass;

struct _MyWidget
{
  GtkWidget b;
};

struct _MyWidgetClass
{
  GtkWidgetClass        parent_class;
};

GType mywidget_get_type() G_GNUC_CONST;
GtkWidget* mywidget_new();

#endif   // _MYWIDGET_H