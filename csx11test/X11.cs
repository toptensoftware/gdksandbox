
using System;
using System.Runtime.InteropServices;

namespace GuiKit.Interop.X11
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct XDisplay
    {
        public IntPtr ext_data;	/* hook for extension to hang data */
        public IntPtr private1;
        public int fd;			/* Network socket. */
        public int private2;
        public int proto_major_version;/* major version of server's X protocol */
        public int proto_minor_version;/* minor version of servers X protocol */
        public IntPtr vendor;		/* vendor of the server hardware */
        public ulong private3;
        public ulong private4;
        public ulong private5;
        public int private6;
        public IntPtr resource_alloc;
        public int byte_order;		/* screen byte order, LSBFirst, MSBFirst */
        public int bitmap_unit;	/* padding and data requirements */
        public int bitmap_pad;		/* padding requirements on bitmaps */
        public int bitmap_bit_order;	/* LeastSignificant or MostSignificant */
        public int nformats;		/* number of pixmap formats in list */
        public IntPtr pixmap_format;	/* pixmap format list */
        public int private8;
        public int release;		/* release of the server */
        public IntPtr private9;
        public IntPtr private10;
        public int qlen;		/* Length of input event queue */
        public ulong last_request_read; /* seq number of last event read */
        public ulong request;	/* sequence number of last request. */
        public IntPtr private11;
        public IntPtr private12;
        public IntPtr private13;
        public IntPtr private14;
        public uint max_request_size; /* maximum number 32 bit words in request*/
        public IntPtr db;
        public IntPtr private15;
        public IntPtr display_name;	/* "host:display" string used on this connect*/
        public int default_screen;	/* default screen for operations */
        public int nscreens;		/* number of screens on this server*/
        public Screen* screens;	/* pointer to list of screens */
        public ulong motion_buffer;	/* size of motion buffer */
        public ulong private16;
        public int min_keycode;	/* minimum defined keycode */
        public int max_keycode;	/* maximum defined keycode */
        public IntPtr private17;
        public IntPtr private18;
        public int private19;
        public IntPtr xdefaults;	/* contents of defaults from server */
        /* there is more to this structure, but it is private to Xlib */
    };

    [StructLayout(LayoutKind.Sequential)]
    public struct Screen
    {
        public IntPtr ext_data;	/* hook for extension to hang data */
        public IntPtr display;/* back pointer to display structure */
        public ulong root;		/* Root window id. */
        public int width;
        public int height;	/* width and height of screen */
        public int mwidth;
        public int mheight;	/* width and height of  in millimeters */
        public int ndepths;		/* number of depths possible */
        public IntPtr depths;		/* list of allowable depths on the screen */
        public int root_depth;		/* bits per pixel */
        public IntPtr root_visual;	/* root visual */
        public IntPtr default_gc;		/* GC for the root root visual */
        public ulong cmap;		/* default color map */
        public ulong white_pixel;
        public ulong black_pixel;	/* White and Black pixel values */
        public int max_maps;
        public int min_maps;	/* max and min color maps */
        public int backing_store;	/* Never, WhenMapped, Always */
        public bool save_unders;
        public long root_input_mask;	/* initial root input mask */
    };

    enum InputEventMask : long
    {
        NoEventMask			         = 0L,
        KeyPressMask			     = 1<<0,
        KeyReleaseMask			     = 1<<1,
        ButtonPressMask			     = 1<<2,
        ButtonReleaseMask		     = 1<<3,
        EnterWindowMask			     = 1<<4,
        LeaveWindowMask			     = 1<<5,
        PointerMotionMask		     = 1<<6,
        PointerMotionHintMask	     = 1<<7,
        Button1MotionMask		     = 1<<8,
        Button2MotionMask		     = 1<<9,
        Button3MotionMask		     = 1<<10,
        Button4MotionMask		     = 1<<11,
        Button5MotionMask		     = 1<<12,
        ButtonMotionMask		     = 1<<13,
        KeymapStateMask			     = 1<<14,
        ExposureMask			     = 1<<15,
        VisibilityChangeMask	     = 1<<16,
        StructureNotifyMask		     = 1<<17,
        ResizeRedirectMask		     = 1<<18,
        SubstructureNotifyMask	     = 1<<19,
        SubstructureRedirectMask     = 1<<20,
        FocusChangeMask			     = 1<<21,
        PropertyChangeMask		     = 1<<22,
        ColormapChangeMask		     = 1<<23,
        OwnerGrabButtonMask		     = 1<<24,
    }

    public enum InputEventType : int
    {
        KeyPress		= 2,
        KeyRelease		= 3,
        ButtonPress		= 4,
        ButtonRelease		= 5,
        MotionNotify		= 6,
        EnterNotify		= 7,
        LeaveNotify		= 8,
        FocusIn			= 9,
        FocusOut		= 10,
        KeymapNotify		= 11,
        Expose			= 12,
        GraphicsExpose		= 13,
        NoExpose		= 14,
        VisibilityNotify	= 15,
        CreateNotify		= 16,
        DestroyNotify		= 17,
        UnmapNotify		= 18,
        MapNotify		= 19,
        MapRequest		= 20,
        ReparentNotify		= 21,
        ConfigureNotify		= 22,
        ConfigureRequest	= 23,
        GravityNotify		= 24,
        ResizeRequest		= 25,
        CirculateNotify		= 26,
        CirculateRequest	= 27,
        PropertyNotify		= 28,
        SelectionClear		= 29,
        SelectionRequest	= 30,
        SelectionNotify		= 31,
        ColormapNotify		= 32,
        ClientMessage		= 33,
        MappingNotify		= 34,
        GenericEvent		= 35,
        LASTEvent		= 36,
    }


    [StructLayout(LayoutKind.Sequential)]
    public struct XAnyEvent
    {
        public InputEventType type;
        public ulong serial;	/* # of last request processed by server */
        public bool send_event;	/* true if this came from a SendEvent request */
        public IntPtr display;/* Display the event was read from */
        public ulong window;	/* window on which event was requested in event mask */
    };


    [StructLayout(LayoutKind.Sequential)]
    public struct XKeyEvent 
    {
        public InputEventType type;		/* of event */
        public ulong serial;	/* # of last request processed by server */
        public bool send_event;	/* true if this came from a SendEvent request */
        public IntPtr display;	/* Display the event was read from */
        public ulong window;	        /* "event" window it is reported relative to */
        public ulong root;	        /* root window that the event occurred on */
        public ulong subwindow;	/* child window */
        public ulong time;		/* milliseconds */
        public int x;
        public int y;		/* pointer x, y coordinates in event window */
        public int x_root;
        public int y_root;	/* coordinates relative to root */
        public uint state;	/* key or button mask */
        public uint keycode;	/* detail */
        public bool same_screen;	/* same screen flag */
    };

    [StructLayout(LayoutKind.Sequential)]
    public struct XButtonEvent
    {
        public int type;		/* of event */
        public ulong serial;	/* # of last request processed by server */
        public bool send_event;	/* true if this came from a SendEvent request */
        public IntPtr display;	/* Display the event was read from */
        public ulong window;	        /* "event" window it is reported relative to */
        public ulong root;	        /* root window that the event occurred on */
        public ulong subwindow;	/* child window */
        public ulong time;		/* milliseconds */
        public int x;
        public int y;		/* pointer x, y coordinates in event window */
        public int x_root;
        public int y_root;	/* coordinates relative to root */
        public uint state;	/* key or button mask */
        public uint button;	/* detail */
        public bool same_screen;	/* same screen flag */
    };

    [StructLayout(LayoutKind.Sequential)]
    public struct XMotionEvent
    {
        public int type;		/* of event */
        public ulong serial;	/* # of last request processed by server */
        public bool send_event;	/* true if this came from a SendEvent request */
        public IntPtr display;	/* Display the event was read from */
        public ulong window;	        /* "event" window it is reported relative to */
        public ulong root;	        /* root window that the event occurred on */
        public ulong subwindow;	/* child window */
        public ulong time;		/* milliseconds */
        public int x;
        public int y;		/* pointer x, y coordinates in event window */
        public int x_root;
        public int y_root;	/* coordinates relative to root */
        public uint state;	/* key or button mask */
        public char is_hint;		/* detail */
        public bool same_screen;	/* same screen flag */
    };

    [StructLayout(LayoutKind.Sequential)]
    public struct XCrossingEvent
    {
        public int type;		/* of event */
        public ulong serial;	/* # of last request processed by server */
        public bool send_event;	/* true if this came from a SendEvent request */
        public IntPtr display;	/* Display the event was read from */
        public ulong window;	        /* "event" window it is reported relative to */
        public ulong root;	        /* root window that the event occurred on */
        public ulong subwindow;	/* child window */
        public ulong time;		/* milliseconds */
        public int x;
        public int y;		/* pointer x, y coordinates in event window */
        public int x_root;
        public int y_root;	/* coordinates relative to root */
        public int mode;		/* NotifyNormal, NotifyGrab, NotifyUngrab */
        public int detail;
        /*
        * NotifyAncestor, NotifyVirtual, NotifyInferior,
        * NotifyNonlinear,NotifyNonlinearVirtual
        */
        bool same_screen;	/* same screen flag */
        bool focus;		/* boolean focus */
        uint state;	/* key or button mask */
    };

    [StructLayout(LayoutKind.Sequential)]
    public struct XFocusChangeEvent
    {
        public int type;		/* of event */
        public ulong serial;	/* # of last request processed by server */
        public bool send_event;	/* true if this came from a SendEvent request */
        public IntPtr display;	/* Display the event was read from */
        public ulong window;	        /* "event" window it is reported relative to */
        public int mode;		/* NotifyNormal, NotifyWhileGrabbed,
                                    NotifyGrab, NotifyUngrab */
        public int detail;
        /*
        * NotifyAncestor, NotifyVirtual, NotifyInferior,
        * NotifyNonlinear,NotifyNonlinearVirtual, NotifyPointer,
        * NotifyPointerRoot, NotifyDetailNone
        */
    };

    [StructLayout(LayoutKind.Sequential)]
    public struct XKeymapEvent
    {
        public int type;		/* of event */
        public ulong serial;	/* # of last request processed by server */
        public bool send_event;	/* true if this came from a SendEvent request */
        public IntPtr display;	/* Display the event was read from */
        public ulong window;	        /* "event" window it is reported relative to */
        public ulong key_vector_0;      // WAS: char key_vector[32]
        public ulong key_vector_1;
        public ulong key_vector_2;
        public ulong key_vector_3;
    };

    [StructLayout(LayoutKind.Sequential)]
    public struct XExposeEvent
    {
        public int type;		/* of event */
        public ulong serial;	/* # of last request processed by server */
        public bool send_event;	/* true if this came from a SendEvent request */
        public IntPtr display;	/* Display the event was read from */
        public ulong window;	        /* "event" window it is reported relative to */
        public int x;
        public int y;
        public int width;
        public int height;
        public int count;		/* if non-zero, at least this many more */
    };

    [StructLayout(LayoutKind.Sequential)]
    public struct XGraphicsExposeEvent
    {
        public int type;		/* of event */
        public ulong serial;	/* # of last request processed by server */
        public bool send_event;	/* true if this came from a SendEvent request */
        public IntPtr display;	/* Display the event was read from */
        public ulong window;	        /* "event" window it is reported relative to */
        public int x;
        public int y;
        public int width;
        public int height;
        public int count;		/* if non-zero, at least this many more */
        public int major_code;		/* core is CopyArea or CopyPlane */
        public int minor_code;		/* not defined in the core */
    };

    [StructLayout(LayoutKind.Sequential)]
    public struct XConfigureEvent
    {
        public int type;		/* of event */
        public ulong serial;	/* # of last request processed by server */
        public bool send_event;	/* true if this came from a SendEvent request */
        public IntPtr display;	/* Display the event was read from */
        public ulong ev;
        public ulong window;
        public int x;
        public int y;
        public int width;
        public int height;
        public int border_width;
        public ulong above;
        public bool override_redirect;
    };

    [StructLayout(LayoutKind.Sequential)]
    public struct XPropertyEvent
    {
        public int type;		/* of event */
        public ulong serial;	/* # of last request processed by server */
        public bool send_event;	/* true if this came from a SendEvent request */
        public IntPtr display;	/* Display the event was read from */
        public ulong window;
        public ulong atom;
        public ulong time;
        public int state;		/* NewValue, Deleted */
    };

    [StructLayout(LayoutKind.Sequential)]
    public struct XClientMessageEvent
    {
        public InputEventType type;		/* of event */
        public ulong serial;	/* # of last request processed by server */
        public bool send_event;	/* true if this came from a SendEvent request */
        public IntPtr display;	/* Display the event was read from */
        public ulong window;
        public ulong message_type;
        public int format;
        public long data0;
        public long data1;
        public long data2;
        public long data3;
        public long data4;

        public byte[] GetByteData()
        {
            var d = new byte[20];
            unsafe
            {
                fixed (byte* pd = d)
                fixed (long* ps = &data0)
                {
                    Buffer.MemoryCopy(ps, pd, 20, 20);
                }
            }
            return d;
        }

        public short[] GetShortData()
        {
            var d = new short[10];
            unsafe
            {
                fixed (short* pd = d)
                fixed (long* ps = &data0)
                {
                    Buffer.MemoryCopy(ps, pd, 20, 20);
                }
            }
            return d;
        }

        public long[] GetLongData()
        {
            var d = new long[5];
            unsafe
            {
                fixed (long* pd = d)
                fixed (long* ps = &data0)
                {
                    Buffer.MemoryCopy(ps, pd, 20, 20);
                }
            }
            return d;
        }
    };


    // This mess is to pad out the XEvent union to exactly 192 bytes
    #pragma warning disable 169     // The field '_XEventPadding.padN' is never used
    struct _XEventPadding
    {
        ulong pad1, pad2, pad3, pad4, pad5, pad6, pad7, pad8;
        ulong pad9, pad10, pad11, pad12, pad13, pad14, pad15, pad16;
        ulong pad17, pad18, pad19, pad20, pad21, pad22, pad23, pad24;
    }
    #pragma warning restore 169

    [StructLayout(LayoutKind.Explicit)]
    public struct XEvent 
    {
        [FieldOffset(0)] public InputEventType type;		/* must not be changed; first element */
        [FieldOffset(0)] public XAnyEvent xany;
        [FieldOffset(0)] public XKeyEvent xkey;
        [FieldOffset(0)] public XButtonEvent xbutton;
        [FieldOffset(0)] public XMotionEvent xmotion;
        [FieldOffset(0)] public XCrossingEvent xcrossing;
        [FieldOffset(0)] public XFocusChangeEvent xfocus;
        [FieldOffset(0)] public XExposeEvent xexpose;
        [FieldOffset(0)] public XGraphicsExposeEvent xgraphicsexpose;
        //[FieldOffset(0)] public XNoExposeEvent xnoexpose;
        //[FieldOffset(0)] public XVisibilityEvent xvisibility;
        //[FieldOffset(0)] public XCreateWindowEvent xcreatewindow;
        //[FieldOffset(0)] public XDestroyWindowEvent xdestroywindow;
        //[FieldOffset(0)] public XUnmapEvent xunmap;
        //[FieldOffset(0)] public XMapEvent xmap;
        //[FieldOffset(0)] public XMapRequestEvent xmaprequest;
        //[FieldOffset(0)] public XReparentEvent xreparent;
        [FieldOffset(0)] public XConfigureEvent xconfigure;
        //[FieldOffset(0)] public XGravityEvent xgravity;
        //[FieldOffset(0)] public XResizeRequestEvent xresizerequest;
        //[FieldOffset(0)] public XConfigureRequestEvent xconfigurerequest;
        //[FieldOffset(0)] public XCirculateEvent xcirculate;
        //[FieldOffset(0)] public XCirculateRequestEvent xcirculaterequest;
        [FieldOffset(0)] public XPropertyEvent xproperty;
        //[FieldOffset(0)] public XSelectionClearEvent xselectionclear;
        //[FieldOffset(0)] public XSelectionRequestEvent xselectionrequest;
        //[FieldOffset(0)] public XSelectionEvent xselection;
        //[FieldOffset(0)] public XColormapEvent xcolormap;
        [FieldOffset(0)] public XClientMessageEvent xclient;
        //[FieldOffset(0)] public XMappingEvent xmapping;
        //[FieldOffset(0)] public XErrorEvent xerror;
        //[FieldOffset(0)] public XKeymapEvent xkeymap;
        //[FieldOffset(0)] public XGenericEvent xgeneric;
        //[FieldOffset(0)] public XGenericEventCookie xcookie;
        [FieldOffset(0)] _XEventPadding padding;
    };


    [StructLayout(LayoutKind.Sequential)]
    public struct XComposeStatus 
    {
        IntPtr compose_ptr;	    /* state table pointer */
        int chars_matched;		/* match state */
    };


    public enum KeySym : ulong
    {
        XK_BackSpace                     = 0xff08,  /* Back space, back char */
        XK_Tab                           = 0xff09,
        XK_Linefeed                      = 0xff0a,  /* Linefeed, LF */
        XK_Clear                         = 0xff0b,
        XK_Return                        = 0xff0d,  /* Return, enter */
        XK_Pause                         = 0xff13,  /* Pause, hold */
        XK_Scroll_Lock                   = 0xff14,
        XK_Sys_Req                       = 0xff15,
        XK_Escape                        = 0xff1b,
        XK_Delete                        = 0xffff,  /* Delete, rubout */
        XK_Home                          = 0xff50,
        XK_Left                          = 0xff51,  /* Move left, left arrow */
        XK_Up                            = 0xff52,  /* Move up, up arrow */
        XK_Right                         = 0xff53,  /* Move right, right arrow */
        XK_Down                          = 0xff54,  /* Move down, down arrow */
        XK_Prior                         = 0xff55,  /* Prior, previous */
        XK_Page_Up                       = 0xff55,
        XK_Next                          = 0xff56,  /* Next */
        XK_Page_Down                     = 0xff56,
        XK_End                           = 0xff57,  /* EOL */
        XK_Begin                         = 0xff58,  /* BOL */
        XK_Select                        = 0xff60,  /* Select, mark */
        XK_Print                         = 0xff61,
        XK_Execute                       = 0xff62,  /* Execute, run, do */
        XK_Insert                        = 0xff63,  /* Insert, insert here */
        XK_Undo                          = 0xff65,
        XK_Redo                          = 0xff66,  /* Redo, again */
        XK_Menu                          = 0xff67,
        XK_Find                          = 0xff68,  /* Find, search */
        XK_Cancel                        = 0xff69,  /* Cancel, stop, abort, exit */
        XK_Help                          = 0xff6a,  /* Help */
        XK_Break                         = 0xff6b,
        XK_Mode_switch                   = 0xff7e,  /* Character set switch */
        XK_script_switch                 = 0xff7e,  /* Alias for mode_switch */
        XK_Num_Lock                      = 0xff7f,
        XK_KP_Space                      = 0xff80,  /* Space */
        XK_KP_Tab                        = 0xff89,
        XK_KP_Enter                      = 0xff8d,  /* Enter */
        XK_KP_F1                         = 0xff91,  /* PF1, KP_A, ... */
        XK_KP_F2                         = 0xff92,
        XK_KP_F3                         = 0xff93,
        XK_KP_F4                         = 0xff94,
        XK_KP_Home                       = 0xff95,
        XK_KP_Left                       = 0xff96,
        XK_KP_Up                         = 0xff97,
        XK_KP_Right                      = 0xff98,
        XK_KP_Down                       = 0xff99,
        XK_KP_Prior                      = 0xff9a,
        XK_KP_Page_Up                    = 0xff9a,
        XK_KP_Next                       = 0xff9b,
        XK_KP_Page_Down                  = 0xff9b,
        XK_KP_End                        = 0xff9c,
        XK_KP_Begin                      = 0xff9d,
        XK_KP_Insert                     = 0xff9e,
        XK_KP_Delete                     = 0xff9f,
        XK_KP_Equal                      = 0xffbd,  /* Equals */
        XK_KP_Multiply                   = 0xffaa,
        XK_KP_Add                        = 0xffab,
        XK_KP_Separator                  = 0xffac,  /* Separator, often comma */
        XK_KP_Subtract                   = 0xffad,
        XK_KP_Decimal                    = 0xffae,
        XK_KP_Divide                     = 0xffaf,
        XK_KP_0                          = 0xffb0,
        XK_KP_1                          = 0xffb1,
        XK_KP_2                          = 0xffb2,
        XK_KP_3                          = 0xffb3,
        XK_KP_4                          = 0xffb4,
        XK_KP_5                          = 0xffb5,
        XK_KP_6                          = 0xffb6,
        XK_KP_7                          = 0xffb7,
        XK_KP_8                          = 0xffb8,
        XK_KP_9                          = 0xffb9,
        XK_F1                            = 0xffbe,
        XK_F2                            = 0xffbf,
        XK_F3                            = 0xffc0,
        XK_F4                            = 0xffc1,
        XK_F5                            = 0xffc2,
        XK_F6                            = 0xffc3,
        XK_F7                            = 0xffc4,
        XK_F8                            = 0xffc5,
        XK_F9                            = 0xffc6,
        XK_F10                           = 0xffc7,
        XK_F11                           = 0xffc8,
        XK_L1                            = 0xffc8,
        XK_F12                           = 0xffc9,
        XK_L2                            = 0xffc9,
        XK_F13                           = 0xffca,
        XK_L3                            = 0xffca,
        XK_F14                           = 0xffcb,
        XK_L4                            = 0xffcb,
        XK_F15                           = 0xffcc,
        XK_L5                            = 0xffcc,
        XK_F16                           = 0xffcd,
        XK_L6                            = 0xffcd,
        XK_F17                           = 0xffce,
        XK_L7                            = 0xffce,
        XK_F18                           = 0xffcf,
        XK_L8                            = 0xffcf,
        XK_F19                           = 0xffd0,
        XK_L9                            = 0xffd0,
        XK_F20                           = 0xffd1,
        XK_L10                           = 0xffd1,
        XK_F21                           = 0xffd2,
        XK_R1                            = 0xffd2,
        XK_F22                           = 0xffd3,
        XK_R2                            = 0xffd3,
        XK_F23                           = 0xffd4,
        XK_R3                            = 0xffd4,
        XK_F24                           = 0xffd5,
        XK_R4                            = 0xffd5,
        XK_F25                           = 0xffd6,
        XK_R5                            = 0xffd6,
        XK_F26                           = 0xffd7,
        XK_R6                            = 0xffd7,
        XK_F27                           = 0xffd8,
        XK_R7                            = 0xffd8,
        XK_F28                           = 0xffd9,
        XK_R8                            = 0xffd9,
        XK_F29                           = 0xffda,
        XK_R9                            = 0xffda,
        XK_F30                           = 0xffdb,
        XK_R10                           = 0xffdb,
        XK_F31                           = 0xffdc,
        XK_R11                           = 0xffdc,
        XK_F32                           = 0xffdd,
        XK_R12                           = 0xffdd,
        XK_F33                           = 0xffde,
        XK_R13                           = 0xffde,
        XK_F34                           = 0xffdf,
        XK_R14                           = 0xffdf,
        XK_F35                           = 0xffe0,
        XK_R15                           = 0xffe0,
        XK_Shift_L                       = 0xffe1,  /* Left shift */
        XK_Shift_R                       = 0xffe2,  /* Right shift */
        XK_Control_L                     = 0xffe3,  /* Left control */
        XK_Control_R                     = 0xffe4,  /* Right control */
        XK_Caps_Lock                     = 0xffe5,  /* Caps lock */
        XK_Shift_Lock                    = 0xffe6,  /* Shift lock */
        XK_Meta_L                        = 0xffe7,  /* Left meta */
        XK_Meta_R                        = 0xffe8,  /* Right meta */
        XK_Alt_L                         = 0xffe9,  /* Left alt */
        XK_Alt_R                         = 0xffea,  /* Right alt */
        XK_Super_L                       = 0xffeb,  /* Left super */
        XK_Super_R                       = 0xffec,  /* Right super */
        XK_Hyper_L                       = 0xffed,  /* Left hyper */
        XK_Hyper_R                       = 0xffee,  /* Right hyper */
        XK_space                         = 0x0020,  /* U+0020 SPACE */
        XK_exclam                        = 0x0021,  /* U+0021 EXCLAMATION MARK */
        XK_quotedbl                      = 0x0022,  /* U+0022 QUOTATION MARK */
        XK_numbersign                    = 0x0023,  /* U+0023 NUMBER SIGN */
        XK_dollar                        = 0x0024,  /* U+0024 DOLLAR SIGN */
        XK_percent                       = 0x0025,  /* U+0025 PERCENT SIGN */
        XK_ampersand                     = 0x0026,  /* U+0026 AMPERSAND */
        XK_apostrophe                    = 0x0027,  /* U+0027 APOSTROPHE */
        XK_quoteright                    = 0x0027,  /* deprecated */
        XK_parenleft                     = 0x0028,  /* U+0028 LEFT PARENTHESIS */
        XK_parenright                    = 0x0029,  /* U+0029 RIGHT PARENTHESIS */
        XK_asterisk                      = 0x002a,  /* U+002A ASTERISK */
        XK_plus                          = 0x002b,  /* U+002B PLUS SIGN */
        XK_comma                         = 0x002c,  /* U+002C COMMA */
        XK_minus                         = 0x002d,  /* U+002D HYPHEN-MINUS */
        XK_period                        = 0x002e,  /* U+002E FULL STOP */
        XK_slash                         = 0x002f,  /* U+002F SOLIDUS */
        XK_0                             = 0x0030,  /* U+0030 DIGIT ZERO */
        XK_1                             = 0x0031,  /* U+0031 DIGIT ONE */
        XK_2                             = 0x0032,  /* U+0032 DIGIT TWO */
        XK_3                             = 0x0033,  /* U+0033 DIGIT THREE */
        XK_4                             = 0x0034,  /* U+0034 DIGIT FOUR */
        XK_5                             = 0x0035,  /* U+0035 DIGIT FIVE */
        XK_6                             = 0x0036,  /* U+0036 DIGIT SIX */
        XK_7                             = 0x0037,  /* U+0037 DIGIT SEVEN */
        XK_8                             = 0x0038,  /* U+0038 DIGIT EIGHT */
        XK_9                             = 0x0039,  /* U+0039 DIGIT NINE */
        XK_colon                         = 0x003a,  /* U+003A COLON */
        XK_semicolon                     = 0x003b,  /* U+003B SEMICOLON */
        XK_less                          = 0x003c,  /* U+003C LESS-THAN SIGN */
        XK_equal                         = 0x003d,  /* U+003D EQUALS SIGN */
        XK_greater                       = 0x003e,  /* U+003E GREATER-THAN SIGN */
        XK_question                      = 0x003f,  /* U+003F QUESTION MARK */
        XK_at                            = 0x0040,  /* U+0040 COMMERCIAL AT */
        XK_A                             = 0x0041,  /* U+0041 LATIN CAPITAL LETTER A */
        XK_B                             = 0x0042,  /* U+0042 LATIN CAPITAL LETTER B */
        XK_C                             = 0x0043,  /* U+0043 LATIN CAPITAL LETTER C */
        XK_D                             = 0x0044,  /* U+0044 LATIN CAPITAL LETTER D */
        XK_E                             = 0x0045,  /* U+0045 LATIN CAPITAL LETTER E */
        XK_F                             = 0x0046,  /* U+0046 LATIN CAPITAL LETTER F */
        XK_G                             = 0x0047,  /* U+0047 LATIN CAPITAL LETTER G */
        XK_H                             = 0x0048,  /* U+0048 LATIN CAPITAL LETTER H */
        XK_I                             = 0x0049,  /* U+0049 LATIN CAPITAL LETTER I */
        XK_J                             = 0x004a,  /* U+004A LATIN CAPITAL LETTER J */
        XK_K                             = 0x004b,  /* U+004B LATIN CAPITAL LETTER K */
        XK_L                             = 0x004c,  /* U+004C LATIN CAPITAL LETTER L */
        XK_M                             = 0x004d,  /* U+004D LATIN CAPITAL LETTER M */
        XK_N                             = 0x004e,  /* U+004E LATIN CAPITAL LETTER N */
        XK_O                             = 0x004f,  /* U+004F LATIN CAPITAL LETTER O */
        XK_P                             = 0x0050,  /* U+0050 LATIN CAPITAL LETTER P */
        XK_Q                             = 0x0051,  /* U+0051 LATIN CAPITAL LETTER Q */
        XK_R                             = 0x0052,  /* U+0052 LATIN CAPITAL LETTER R */
        XK_S                             = 0x0053,  /* U+0053 LATIN CAPITAL LETTER S */
        XK_T                             = 0x0054,  /* U+0054 LATIN CAPITAL LETTER T */
        XK_U                             = 0x0055,  /* U+0055 LATIN CAPITAL LETTER U */
        XK_V                             = 0x0056,  /* U+0056 LATIN CAPITAL LETTER V */
        XK_W                             = 0x0057,  /* U+0057 LATIN CAPITAL LETTER W */
        XK_X                             = 0x0058,  /* U+0058 LATIN CAPITAL LETTER X */
        XK_Y                             = 0x0059,  /* U+0059 LATIN CAPITAL LETTER Y */
        XK_Z                             = 0x005a,  /* U+005A LATIN CAPITAL LETTER Z */
        XK_bracketleft                   = 0x005b,  /* U+005B LEFT SQUARE BRACKET */
        XK_backslash                     = 0x005c,  /* U+005C REVERSE SOLIDUS */
        XK_bracketright                  = 0x005d,  /* U+005D RIGHT SQUARE BRACKET */
        XK_asciicircum                   = 0x005e,  /* U+005E CIRCUMFLEX ACCENT */
        XK_underscore                    = 0x005f,  /* U+005F LOW LINE */
        XK_grave                         = 0x0060,  /* U+0060 GRAVE ACCENT */
        XK_quoteleft                     = 0x0060,  /* deprecated */
        XK_a                             = 0x0061,  /* U+0061 LATIN SMALL LETTER A */
        XK_b                             = 0x0062,  /* U+0062 LATIN SMALL LETTER B */
        XK_c                             = 0x0063,  /* U+0063 LATIN SMALL LETTER C */
        XK_d                             = 0x0064,  /* U+0064 LATIN SMALL LETTER D */
        XK_e                             = 0x0065,  /* U+0065 LATIN SMALL LETTER E */
        XK_f                             = 0x0066,  /* U+0066 LATIN SMALL LETTER F */
        XK_g                             = 0x0067,  /* U+0067 LATIN SMALL LETTER G */
        XK_h                             = 0x0068,  /* U+0068 LATIN SMALL LETTER H */
        XK_i                             = 0x0069,  /* U+0069 LATIN SMALL LETTER I */
        XK_j                             = 0x006a,  /* U+006A LATIN SMALL LETTER J */
        XK_k                             = 0x006b,  /* U+006B LATIN SMALL LETTER K */
        XK_l                             = 0x006c,  /* U+006C LATIN SMALL LETTER L */
        XK_m                             = 0x006d,  /* U+006D LATIN SMALL LETTER M */
        XK_n                             = 0x006e,  /* U+006E LATIN SMALL LETTER N */
        XK_o                             = 0x006f,  /* U+006F LATIN SMALL LETTER O */
        XK_p                             = 0x0070,  /* U+0070 LATIN SMALL LETTER P */
        XK_q                             = 0x0071,  /* U+0071 LATIN SMALL LETTER Q */
        XK_r                             = 0x0072,  /* U+0072 LATIN SMALL LETTER R */
        XK_s                             = 0x0073,  /* U+0073 LATIN SMALL LETTER S */
        XK_t                             = 0x0074,  /* U+0074 LATIN SMALL LETTER T */
        XK_u                             = 0x0075,  /* U+0075 LATIN SMALL LETTER U */
        XK_v                             = 0x0076,  /* U+0076 LATIN SMALL LETTER V */
        XK_w                             = 0x0077,  /* U+0077 LATIN SMALL LETTER W */
        XK_x                             = 0x0078,  /* U+0078 LATIN SMALL LETTER X */
        XK_y                             = 0x0079,  /* U+0079 LATIN SMALL LETTER Y */
        XK_z                             = 0x007a,  /* U+007A LATIN SMALL LETTER Z */
        XK_braceleft                     = 0x007b,  /* U+007B LEFT CURLY BRACKET */
        XK_bar                           = 0x007c,  /* U+007C VERTICAL LINE */
        XK_braceright                    = 0x007d,  /* U+007D RIGHT CURLY BRACKET */
        XK_asciitilde                    = 0x007e,  /* U+007E TILDE */
    }

    public enum ImageFormat : int
    {
        XYBitmap	= 0,	/* depth 1, XYFormat */
        XYPixmap	= 1,	/* depth == drawable depth */
        ZPixmap		= 2,	/* depth == drawable depth */
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct XImage 
    {
        public int width;
        public int height;		/* size of image */
        public int xoffset;		/* number of pixels offset in X direction */
        public int format;			/* XYBitmap, XYPixmap, ZPixmap */
        public IntPtr data;			/* pointer to image data */
        public int byte_order;		/* data byte order, LSBFirst, MSBFirst */
        public int bitmap_unit;		/* quant. of scanline 8, 16, 32 */
        public int bitmap_bit_order;	/* LSBFirst, MSBFirst */
        public int bitmap_pad;		/* 8, 16, 32 either XY or ZPixmap */
        public int depth;			/* depth of image */
        public int bytes_per_line;		/* accelarator to next line */
        public int bits_per_pixel;		/* bits per pixel (ZPixmap) */
        public ulong red_mask;	/* bits in z arrangment */
        public ulong green_mask;
        public ulong blue_mask;
        public IntPtr obdata;		/* hook for the object routines to hang on */
        public IntPtr create_image;
        public IntPtr destroy_image;
        public IntPtr get_pixel;
        public IntPtr put_pixel;
        public IntPtr sub_image;
        public IntPtr add_pixel;
    };

	delegate int destroy_image(IntPtr ximage);
	delegate ulong get_pixel(IntPtr ximage, int x, int y);
	delegate int put_pixel(IntPtr ximage, int x, int y, ulong pixel);
	delegate IntPtr sub_image(IntPtr ximage, int l, int y, int w, uint h);
	delegate int add_pixel(IntPtr ximage, long add_pixel_value);

    static class Functions
    {
        const string libX11 = "libX11.so.6";

        [DllImport(libX11)]
        public static extern IntPtr XOpenDisplay([MarshalAs(UnmanagedType.LPUTF8Str)] string display_name);

        [DllImport(libX11)]
        public static extern int XCloseDisplay(IntPtr display);

        [DllImport(libX11)]
        public static extern ulong XCreateSimpleWindow(
            IntPtr display,
            ulong parent,
            int x,
            int y,
            int width,
            int height,
            int border_width,
            ulong border,
            ulong background
        );

        [DllImport(libX11)]
        public static extern int XDestroyWindow(IntPtr display, ulong window);

        [DllImport(libX11)]
        public static extern int XSelectInput(IntPtr display, ulong window, InputEventMask event_mask);

        [DllImport(libX11)]
        public static extern int XMapWindow(IntPtr display, ulong window);

        [DllImport(libX11)]
        public static extern int XStoreName(IntPtr display, ulong window, [MarshalAs(UnmanagedType.LPUTF8Str)] string window_name);

        [DllImport(libX11)]
        public static extern ulong XInternAtom(IntPtr display, [MarshalAs(UnmanagedType.LPUTF8Str)] string window_name, bool only_if_exits);

        [DllImport(libX11)]
        public static extern int XNextEvent(IntPtr display, out XEvent event_return);

        [DllImport(libX11)]
        public static extern int XLookupString(ref XKeyEvent event_struct, IntPtr buf, int bytes_buffer, out KeySym keysym, ref XComposeStatus status);

        [DllImport(libX11)]
        public static extern int XLookupString(ref XKeyEvent event_struct, IntPtr buf, int bytes_buffer, out KeySym keysym, IntPtr status);

        public static int XLookupString(ref XKeyEvent event_struct, byte[] buf, out KeySym keysym, IntPtr status)
        {
            unsafe
            {
                fixed (byte* p = buf)
                {
                    return XLookupString(ref event_struct, (IntPtr)p, buf.Length, out keysym, status);
                }
            }
        }

        [DllImport(libX11)]
        public static extern int XSetWMProtocols(IntPtr display, ulong window, IntPtr atoms, int count);

        public static int XSetWMProtocols(IntPtr display, ulong window, params ulong[] atoms)
        {
            unsafe
            {
                fixed (ulong* p = atoms)
                {
                    return XSetWMProtocols(display, window, (IntPtr)p, atoms.Length);
                }
            }
        }

        [DllImport(libX11)]
        public static extern IntPtr XCreateImage(
            IntPtr display,
            IntPtr visual,
            int depth,
            ImageFormat format,
            int offset,
            IntPtr data,
            uint width,
            uint height,
            int bitmap_pad,
            int bytes_per_line
        );

        public static void XDestroyImage(IntPtr ximage)
        {
            unsafe
            {
                XImage* pImage = (XImage*)ximage;
                Marshal.GetDelegateForFunctionPointer<destroy_image>(pImage->destroy_image)(ximage);
            }
        }

        [DllImport(libX11)]
        public static extern int XPutImage(
            IntPtr display,
            ulong drawable,
            IntPtr gc,            
            IntPtr ximage,
            int src_x,
            int src_y,
            int dest_x,
            int dest_y,
            int width,
            int height
        );

        public static int DefaultScreen(IntPtr dpy)
        {
            unsafe
            {
                return ((XDisplay*)dpy)->default_screen;
            }
        }

        public static unsafe Screen* ScreenOfDisplay(IntPtr dpy, int screen)
        {
            return ((XDisplay*)dpy)->screens + screen;
        }

        public static ulong RootWindow(IntPtr dpy, int screen)
        {
            unsafe
            {
                return ScreenOfDisplay(dpy, screen)->root;
            }
        }

        public static ulong BlackPixel(IntPtr dpy, int screen)
        {
            unsafe
            {
                return ScreenOfDisplay(dpy, screen)->black_pixel;
            }
        }

        public static ulong WhitePixel(IntPtr dpy, int screen)
        {
            unsafe
            {
                return ScreenOfDisplay(dpy, screen)->white_pixel;
            }
        }

        public static IntPtr DefaultVisual(IntPtr dpy, int screen)
        {
            unsafe
            {
                return ScreenOfDisplay(dpy, screen)->root_visual;
            }
        }

        public static int DefaultDepth(IntPtr dpy, int screen)
        {
            unsafe
            {
                return ScreenOfDisplay(dpy, screen)->root_depth;
            }
        }

        public static IntPtr DefaultGC(IntPtr dpy, int screen)
        {
            unsafe
            {
                return ScreenOfDisplay(dpy, screen)->default_gc;
            }
        }


    }
}