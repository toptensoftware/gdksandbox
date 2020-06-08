#include <gtk/gtk.h>
#include "MyWidget.h"


//////////////////////////////////////////////////////////////////////////////////////////////////////////
// Main

GtkApplication *app;

static void
activate (GtkApplication* app,
          gpointer        user_data)
{
//  GtkWidget* window = gtk_application_window_new (app);
  GtkWidget* window = gtk_window_new(GTK_WINDOW_TOPLEVEL);

  gtk_window_set_title(GTK_WINDOW(window), "Window");
  gtk_window_set_default_size(GTK_WINDOW(window), 200, 200);

  GtkWidget* vbox = gtk_box_new(GTK_ORIENTATION_VERTICAL, 0);
  gtk_container_add(GTK_CONTAINER(window), vbox);

  GtkWidget* menubar = gtk_menu_bar_new();
  GtkWidget* fileMenu = gtk_menu_new();

  GtkWidget* fileMi = gtk_menu_item_new_with_label("File");
  GtkWidget* quitMi = gtk_menu_item_new_with_label("Quit");
  g_signal_connect_swapped(G_OBJECT(quitMi), "activate", G_CALLBACK (g_application_quit), app);

  gtk_menu_item_set_submenu(GTK_MENU_ITEM(fileMi), fileMenu);
  gtk_menu_shell_append(GTK_MENU_SHELL(fileMenu), quitMi);
  gtk_menu_shell_append(GTK_MENU_SHELL(menubar), fileMi);
  gtk_box_pack_start(GTK_BOX(vbox), menubar, FALSE, FALSE, 0);


  GtkWidget* widget = mywidget_new();
  gtk_box_pack_start(GTK_BOX(vbox), widget, TRUE, TRUE, 0);

  gtk_widget_show_all(window);
//  gdk_window_move_resize(gtk_widget_get_window(window),200, 200, 600, 400);

}

int
main (int argc, char **argv)
{
  int status;

  gtk_init(&argc, &argv);


  GdkDisplay* pDisplay = gdk_display_get_default();

  int monitors = gdk_display_get_n_monitors(pDisplay);
  printf("Monitors: %i\n", monitors);
  for (int i=0; i<monitors; i++)
  {
      GdkMonitor* pMonitor = gdk_display_get_monitor(pDisplay, i);
      GdkRectangle r;
      gdk_monitor_get_geometry(pMonitor, &r);
      printf("Monitor %i: %i,%i x %i,%i @ %i\n", i, r.x, r.y, r.width, r.height, gdk_monitor_get_scale_factor(pMonitor));
  }

    printf("GDK_SCALE: %s", g_getenv ("GDK_SCALE"));


  app = gtk_application_new ("org.gtk.example", G_APPLICATION_FLAGS_NONE);
  g_signal_connect (app, "activate", G_CALLBACK (activate), NULL);

  //g_application_hold(app);
  g_application_hold(G_APPLICATION(app));
  status = g_application_run (G_APPLICATION (app), argc, argv);
  g_object_unref (app);

  return status;
}

