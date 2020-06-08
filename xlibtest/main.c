#define XLIB_ILLEGAL_ACCESS
#include <X11/Xlib.h>
#include <X11/Xutil.h>
#include <X11/Xos.h>
 
#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <stdbool.h>
 
#include <sys/utsname.h>
 

bool getWindowParent(Display* display, Window winId, Window* pParent) {
    Window root, parent, *children = NULL;
    unsigned int num_children;

    if(!XQueryTree(display, winId, &root, &parent, &children, &num_children))
        return false;

    if (children)
        XFree((char *)children);

    winId = parent;
    *pParent = root;
    return true;
}

int main(int argc, char** argv)
{
      printf("%i\n", (int)sizeof(XImage));


  Display* dpy = XOpenDisplay(NULL);
  if (dpy == NULL) 
  {
    fprintf(stderr, "Cannot open display\n");
    exit(1);
  }

  int s = DefaultScreen(dpy);


  Atom t;
  int f;
  unsigned long n, b;
  unsigned char *data = 0;
  Atom a = XInternAtom(dpy, "_NET_FRAME_EXTENTS", True); /* Property to check */

  Window win = XCreateSimpleWindow(dpy, RootWindow(dpy, s), 500, 100, 660, 200, 1,
                                   BlackPixel(dpy, s), WhitePixel(dpy, s));
  XSelectInput(dpy, win, ExposureMask | KeyPressMask);
  XMapWindow(dpy, win);

  ZPixmap

  // Move it
  XWindowChanges ch;
  ch.x = 0;
  ch.y = 50;
  XConfigureWindow(dpy, win, CWX | CWY, &ch);

 
#if defined(__APPLE_CC__)  
  XStoreName(dpy, win, "Geeks3D.com - X11 window under Mac OS X (Lion)");
#else
  XStoreName(dpy, win, "Geeks3D.com - X11 window under Linux (Mint 10)");
#endif  
 
  Atom WM_DELETE_WINDOW = XInternAtom(dpy, "WM_DELETE_WINDOW", False); 
  XSetWMProtocols(dpy, win, &WM_DELETE_WINDOW, 1);  

  bool uname_ok = false;
  struct utsname sname;  
  int ret = uname(&sname);
  if (ret != -1)
  {
    uname_ok = true;
  }
 
  XEvent e;
  while (1) 
  {
    XNextEvent(dpy, &e);


  DefaultGC
    /* Window manager doesn't set up the extents immediately */
    /* Wait until they are set up and there are 4 of them */
/*
    if (XGetWindowProperty(dpy, win, a,
                   0, 4, False, AnyPropertyType,
                   &t, &f,
                   &n, &b, &data) != Success || n != 4 || b != 0) 
    {
        printf ("Waiting for extents\n");
    }
    else
    {
        int* extents = (int*)data;
        printf ("Got frame extents: left %i right %i top %i bottom %i\n",
            extents[0], extents[1], extents[2], extents[3]);
    }
*/    

/*
    if (e.type == Expose) 
    {
      int y_offset = 20;
 
#if defined(__APPLE_CC__)  
      const char* s1 = "X11 test app under Mac OS X Lion";
#else      
      const char* s1 = "X11 test app under Linux";
#endif      
 
      const char* s2 = "(C)2012 Geeks3D.com"; 
      XDrawString(dpy, win, DefaultGC(dpy, s), 10, y_offset, s1, strlen(s1));
      y_offset += 20;
      XDrawString(dpy, win, DefaultGC(dpy, s), 10, y_offset, s2, strlen(s2));
      y_offset += 20;
 
      if (uname_ok)
      {
        char buf[256] = {0};
 
        sprintf(buf, "System information:");
        XDrawString(dpy, win, DefaultGC(dpy, s), 10, y_offset, buf, strlen(buf));
        y_offset += 15;
 
        sprintf(buf, "- System: %s", sname.sysname);
        XDrawString(dpy, win, DefaultGC(dpy, s), 10, y_offset, buf, strlen(buf));
        y_offset += 15;
 
        sprintf(buf, "- Release: %s", sname.release);
        XDrawString(dpy, win, DefaultGC(dpy, s), 10, y_offset, buf, strlen(buf));
        y_offset += 15;
 
        sprintf(buf, "- Version: %s", sname.version);
        XDrawString(dpy, win, DefaultGC(dpy, s), 10, y_offset, buf, strlen(buf));
        y_offset += 15;
 
        sprintf(buf, "- Machine: %s", sname.machine);
        XDrawString(dpy, win, DefaultGC(dpy, s), 10, y_offset, buf, strlen(buf));
        y_offset += 20;
      }
 
 
      XWindowAttributes  wa;
      XGetWindowAttributes(dpy, win, &wa);
      int width = wa.width;
      int height = wa.height;


      int x, y;
      Window child;
      XTranslateCoordinates(dpy, win, RootWindow(dpy, s), 0, 0, &x, &y, &child);

      Window parent;
      getWindowParent(dpy, win, &parent);
      XWindowAttributes  waParent;
      XGetWindowAttributes(dpy, parent, &waParent);



      char buf[128]={0};
      sprintf(buf, "Current window size: %d,%d %dx%d (%d,%d)", x, y, width, height, waParent.x, waParent.y);
      XDrawString(dpy, win, DefaultGC(dpy, s), 10, y_offset, buf, strlen(buf));
      y_offset += 20;
    }
*/
  XPutImage()
 
    if (e.type == KeyPress)
    {
      char buf[128] = {0};
      KeySym keysym;
      int len = XLookupString(&e.xkey, buf, sizeof buf, &keysym, NULL);
      if (keysym == XK_Escape)
        break;
    }
 
    if ((e.type == ClientMessage) && 
        (e.xclient.data.l[0] == WM_DELETE_WINDOW))
    {
      break;
    }
  }
 
  XDestroyWindow(dpy, win);
  XCloseDisplay(dpy);
  return 0;
}