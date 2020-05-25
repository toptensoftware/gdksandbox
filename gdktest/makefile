gtktest: main.c
	gcc `pkg-config --cflags gtk+-3.0` -ggdb -o gdktest main.c `pkg-config --libs gtk+-3.0`

install:
	cp -r ./share/ /usr/
	update-icon-caches /usr/share/icons/*
	
uninstall:
	sudo find /usr/share/ -type f -name 'gtktest.*' -delete
	update-icon-caches /usr/share/icons/*
