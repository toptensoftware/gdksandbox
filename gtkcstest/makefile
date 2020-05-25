PROJNAME=gtkcstest
VERSION=1.0-1
DEBDIR=./bin/deb/$(PROJNAME)_$(VERSION)

install:
	cp -r setup/debroot/ /
	update-icon-caches /usr/share/icons/*
	
uninstall:
	sudo find /usr/share/ -type f -name '$(PROJNAME).*' -delete
	update-icon-caches /usr/share/icons/*

deb:
	dotnet publish --configuration Release
	rm -rf ./bin/deb
	mkdir ./bin/deb
	cp -r ./setup/debroot $(DEBDIR)
	cp -r ./bin/Release/netcoreapp3.1/publish/* $(DEBDIR)/usr/share/$(PROJNAME)
	for f in $(DEBDIR)/DEBIAN/* ; do \
		sed -i 's/BUILD_VERSION/$(VERSION)/g' $$f ; \
		sed -i 's/BUILD_PROJNAME/$(PROJNAME)/g' $$f ; \
	done
	dpkg-deb --build $(DEBDIR)