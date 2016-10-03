#
# Makefile for Visual Studio Project ...
#
# This file is generated automatically from the Visual Studio Project
# File. If you make changes to this file and do not update the project
# file, changes will be lost when the file is regenerated.

# The following targets are defined (well planned)
#
# make 			Compile for the current platform
# make cross		Compile for all platforms
# make install		Compile and install
# make clean		Delete all target and intermediate files

# The following build flags are supported
#
# make mode= release | debug			Build release or debug version
# make arch= this | all | <x>			Bundle for the current platform, all platforms
#										or the specified platform

# Define the default target directories (referenced projects must all follow same scheme)
#
# By default, we arrange the mono targets as follows:
#
# <Source>					The source code directory
# <Source>/mono/			Equivalent to VS bin directory
# <Source>/mono/Debug		Equivalent to VS bin/Debug directory
# <Source>/mono/Release	Equivalent to VS bin/Debug directory
#
# If the target is an executable, the following directories are also created:
#
# <Source>/This			The bundled executable for the platform the code was compiled on
# <Source>/<Arch1>			The bundled executable for the platform <Arch1>	
#
# If the install target is selected, the bundles will be installed in
#
# ~/Tools/This				The bundled executable for platform the code is built on
# ~/Tools/<Arch1>			The bundled executable for this platform <Arch1>	

export TARGETROOT	?= mono
export MODE			?= Release
export ARCH			?= This
export Packages		?= ~/Packages
export PackagesPath ?= /lib/net40

export TARGETBIN	= $(TARGETROOT)/$(MODE)
export TARGETEXE	= $(TARGETROOT)/$(ARCH)

export DESTDIR		?= ~/.local
export bindir		?= /bin
export libdir		?= /lib
export INSTALL_PROGRAM	?= $(DESTDIR)$(bindir)
export INSTALL_DATA		?= $(DESTDIR)$(bindir)

# Define the default compilers, linkers, packagers, etc.
export CSHARPDLL	?=  mcs /target:library
export CSHARPEXE	?=  mcs /target:exe
export BUNDLE		?=  mkbundle --deps --static -o 



# The following tools are used in the goedel build system itself:
export Custom_RFC2TXT		?= rfctool /in
export Custom_RFC2TXT_FLAG	?= /txt
export Custom_RFC2XML		?= rfctool /in
export Custom_RFC2XML_FLAG	?= /xml
export Custom_RFC2MD		?= rfctool /in
export Custom_RFC2MD_FLAG	?= /md
export Custom_RFC2HTML		?= rfctool /in
export Custom_RFC2HTML_FLAG	?= /html
export Custom_CommandCS		?= commandparse /in
export Custom_CommandCS_FLAG	?= /cs
export Custom_FSRCS		?= fsrgen /in
export Custom_FSRCS_FLAG	?= /cs
export Custom_Exceptional		?= exceptional /in
export Custom_Exceptional_FLAG	?= /cs
export Custom_GScript		?= gscript /in
export Custom_GScript_FLAG	?= /cs
export Custom_Goedel3		?= goedel3 /in
export Custom_Goedel3_FLAG	?= /cs
export Custom_ASN2CS		?= asn2 /in
export Custom_ASN2CS_FLAG	?= /cs
export Custom_DomainerCS		?= domainer /in
export Custom_DomainerCS_FLAG	?= /cs
export Custom_RegistryCS		?= registryconfig /in
export Custom_RegistryCS_FLAG	?= /cs
export Custom_VSIXBuild		?= vsixbuild /in
export Custom_VSIXBuild_FLAG	?= /cs
export Custom_ProtoGen		?= protogen /in
export Custom_ProtoGen_FLAG	?= /cs
export Custom_TrojanGTK		?= trojan /gtk
export Custom_TrojanGTK_FLAG	?= /cs

# Use the specified character as the prefix character. Note this may not 
# be supported on versions of make other than gmake.
.RECIPEPREFIX = ! 


# The main target

$(TARGETEXE)/Makey :| $(TARGETEXE)
$(TARGETEXE)/Makey : $(TARGETBIN)/Makey.exe 
! $(BUNDLE) $@ $^


$(TARGETBIN)/Makey.exe :| $(TARGETBIN)
$(TARGETBIN)/Makey.exe : Commands.cs 
$(TARGETBIN)/Makey.exe :| $(TARGETBIN)
$(TARGETBIN)/Makey.exe : Dispatch.cs 
$(TARGETBIN)/Makey.exe :| $(TARGETBIN)
$(TARGETBIN)/Makey.exe : Properties/AssemblyInfo.cs 
$(TARGETBIN)/Makey.exe : $(TARGETBIN)/Goedel.Registry.dll
$(TARGETBIN)/Makey.exe : $(TARGETBIN)/Goedel.Tools.Makey.dll

$(TARGETBIN/)Makey.exe :
! $(CSHARPEXE) /out:$@ $^




# Directories etc.

$(INSTALL_PROGRAM) :
! mkdir -p $(INSTALL_PROGRAM) 

$(INSTALL_DATA) :
! mkdir -p $(INSTALL_DATA) 

$(TARGETBIN) :
! mkdir -p $(TARGETBIN) 

$(TARGETEXE) :
! mkdir -p $(TARGETEXE) 


# Generated code

Commands.cs : Commands.command
!  $(Custom_CommandCS) Commands.command $(Custom_CommandCS_FLAG) Commands.cs 

.PHONY : clean install cross recursive


# Referenced projects
#

# Recursive make targets, do not execute if the variable NORECURSE is defined
ifndef NORECURSE

../../Goedel.Registry/$(TARGETBIN)/Goedel.Registry.dll : recursive
! $(MAKE) -C ../../Goedel.Registry

../Goedel.Tools.Makey/$(TARGETBIN)/Goedel.Tools.Makey.dll : recursive
! $(MAKE) -C ../Goedel.Tools.Makey

endif

$(TARGETBIN)/Goedel.Registry.dll : ../../Goedel.Registry/$(TARGETBIN)/Goedel.Registry.dll
!  cp ../../Goedel.Registry/$(TARGETBIN)/Goedel.Registry.dll $(TARGETBIN)/Goedel.Registry.dll

$(TARGETBIN)/Goedel.Tools.Makey.dll : ../Goedel.Tools.Makey/$(TARGETBIN)/Goedel.Tools.Makey.dll
!  cp ../Goedel.Tools.Makey/$(TARGETBIN)/Goedel.Tools.Makey.dll $(TARGETBIN)/Goedel.Tools.Makey.dll


# Referenced Libraries
#


# Clean up
#
# At the moment we only delete the currently indicated target. This allows a 
# developer to do a make clean / make when a problem with a corrupted intermediate
# file is suspected.

clean : 
! rm -f $(TARGETBIN)/*
! rm -f $(TARGETEXE)/*

# Install
#
# Install files to a tools directory. Default is ~/.local/bin


install : $(INSTALL_PROGRAM)/Makey 

$(INSTALL_PROGRAM)/Makey  :| $(INSTALL_PROGRAM)
$(INSTALL_PROGRAM)/Makey  : $(TARGETEXE)/Makey 
! cp $^ $@ 


# Cross
#
# Cross compilation targets. 


cross : $(TARGETROOT)/4.4.0-linux-libc2.13-amd64/Makey 
$(TARGETROOT)/4.4.0-linux-libc2.13-amd64/Makey : $(TARGETBIN)/Makey.exe
! mkdir -p $(TARGETROOT)/4.4.0-linux-libc2.13-amd64/Makey 
! $(BUNDLE) $@ --cross ${Arch} $^

cross : $(TARGETROOT)/4.4.0-linux-libc2.13-armel/Makey 
$(TARGETROOT)/4.4.0-linux-libc2.13-armel/Makey : $(TARGETBIN)/Makey.exe
! mkdir -p $(TARGETROOT)/4.4.0-linux-libc2.13-armel/Makey 
! $(BUNDLE) $@ --cross ${Arch} $^

cross : $(TARGETROOT)/4.4.0-linux-libc2.13-armhf/Makey 
$(TARGETROOT)/4.4.0-linux-libc2.13-armhf/Makey : $(TARGETBIN)/Makey.exe
! mkdir -p $(TARGETROOT)/4.4.0-linux-libc2.13-armhf/Makey 
! $(BUNDLE) $@ --cross ${Arch} $^

cross : $(TARGETROOT)/4.4.0-linux-libc2.13-i386/Makey 
$(TARGETROOT)/4.4.0-linux-libc2.13-i386/Makey : $(TARGETBIN)/Makey.exe
! mkdir -p $(TARGETROOT)/4.4.0-linux-libc2.13-i386/Makey 
! $(BUNDLE) $@ --cross ${Arch} $^

cross : $(TARGETROOT)/4.4.0-macos-10.7-amd64/Makey 
$(TARGETROOT)/4.4.0-macos-10.7-amd64/Makey : $(TARGETBIN)/Makey.exe
! mkdir -p $(TARGETROOT)/4.4.0-macos-10.7-amd64/Makey 
! $(BUNDLE) $@ --cross ${Arch} $^

cross : $(TARGETROOT)/4.4.0-macos-10.7-i386/Makey 
$(TARGETROOT)/4.4.0-macos-10.7-i386/Makey : $(TARGETBIN)/Makey.exe
! mkdir -p $(TARGETROOT)/4.4.0-macos-10.7-i386/Makey 
! $(BUNDLE) $@ --cross ${Arch} $^

cross : $(TARGETROOT)/4.4.2-linux-libc2.13-amd64/Makey 
$(TARGETROOT)/4.4.2-linux-libc2.13-amd64/Makey : $(TARGETBIN)/Makey.exe
! mkdir -p $(TARGETROOT)/4.4.2-linux-libc2.13-amd64/Makey 
! $(BUNDLE) $@ --cross ${Arch} $^

cross : $(TARGETROOT)/4.4.2-linux-libc2.13-armel/Makey 
$(TARGETROOT)/4.4.2-linux-libc2.13-armel/Makey : $(TARGETBIN)/Makey.exe
! mkdir -p $(TARGETROOT)/4.4.2-linux-libc2.13-armel/Makey 
! $(BUNDLE) $@ --cross ${Arch} $^

cross : $(TARGETROOT)/4.4.2-linux-libc2.13-armhf/Makey 
$(TARGETROOT)/4.4.2-linux-libc2.13-armhf/Makey : $(TARGETBIN)/Makey.exe
! mkdir -p $(TARGETROOT)/4.4.2-linux-libc2.13-armhf/Makey 
! $(BUNDLE) $@ --cross ${Arch} $^

cross : $(TARGETROOT)/4.4.2-linux-libc2.13-i386/Makey 
$(TARGETROOT)/4.4.2-linux-libc2.13-i386/Makey : $(TARGETBIN)/Makey.exe
! mkdir -p $(TARGETROOT)/4.4.2-linux-libc2.13-i386/Makey 
! $(BUNDLE) $@ --cross ${Arch} $^

cross : $(TARGETROOT)/4.4.2-macos-10.7-amd64/Makey 
$(TARGETROOT)/4.4.2-macos-10.7-amd64/Makey : $(TARGETBIN)/Makey.exe
! mkdir -p $(TARGETROOT)/4.4.2-macos-10.7-amd64/Makey 
! $(BUNDLE) $@ --cross ${Arch} $^

cross : $(TARGETROOT)/4.4.2-macos-10.7-i386/Makey 
$(TARGETROOT)/4.4.2-macos-10.7-i386/Makey : $(TARGETBIN)/Makey.exe
! mkdir -p $(TARGETROOT)/4.4.2-macos-10.7-i386/Makey 
! $(BUNDLE) $@ --cross ${Arch} $^


