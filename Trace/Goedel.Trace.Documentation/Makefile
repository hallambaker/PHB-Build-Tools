#
# Makefile for Visual Studio Project Goedel.Trace.Documentation
#
# This file is generated automatically from the Visual Studio Project
# File. If you make changes to this file and do not update the project
# file, changes will be lost when the file is regenerated.

# NB: This process will fail if any of the paths have spaces in them
# While it is possible to work around the lack of support for spaces in 
# file paths in gmake, it is not possible to do this reliably in the tools
# that it invokes to build the system. Rather than do half a job, it seems
# safest to simply reject the corner case


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

export TARGETROOT		?= mono
export MODE				?= Release
export ARCH				?= This
export Packages			?= $(HOME)/Packages
export PackagesPath		?= /lib/net40
export Libraries		?= $(HOME)/Libraries
export LibrariesPath	?= /Mono


export TARGETBIN	= $(TARGETROOT)/$(MODE)
export TARGETEXE	= $(TARGETROOT)/$(ARCH)
export LIBRARYBIN	= $(Libraries)$(LibrariesPath)

export DESTDIR		?= $(HOME)/.local
export bindir		?= /bin
export libdir		?= /lib
export INSTALL_PROGRAM	?= $(DESTDIR)$(bindir)
export INSTALL_DATA		?= $(DESTDIR)$(libdir)

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

SourceFiles = \
     Dispatch.cs\
     Documentation.cs\
     ExampleGenerator.cs\
     Properties/AssemblyInfo.cs\
     TraceServerPortalTraced.cs

LinkFiles = \
    $(TARGETBIN)/Goedel.Command.dll\
    $(TARGETBIN)/Goedel.IO.dll\
    $(TARGETBIN)/Goedel.Protocol.Debug.dll\
    $(TARGETBIN)/Goedel.Protocol.dll\
    $(TARGETBIN)/Goedel.Registry.dll\
    $(TARGETBIN)/Goedel.Utilities.dll\
    $(TARGETBIN)/Goedel.Trace.Client.dll\
    $(TARGETBIN)/Goedel.Trace.Server.dll\
    $(TARGETBIN)/Goedel.Trace.dll

LinkFilesComma = $(TARGETBIN)/Goedel.Command.dll,$(TARGETBIN)/Goedel.IO.dll,$(TARGETBIN)/Goedel.Protocol.Debug.dll,$(TARGETBIN)/Goedel.Protocol.dll,$(TARGETBIN)/Goedel.Registry.dll,$(TARGETBIN)/Goedel.Utilities.dll,$(TARGETBIN)/Goedel.Trace.Client.dll,$(TARGETBIN)/Goedel.Trace.Server.dll,$(TARGETBIN)/Goedel.Trace.dll


NugetFiles = 
NugetFilesComa = 

# A) Main target packaged
$(TARGETEXE)/Goedel.Trace.Documentation :| $(TARGETEXE)
$(TARGETEXE)/Goedel.Trace.Documentation : $(TARGETBIN)/Goedel.Trace.Documentation.exe 
! $(BUNDLE) $@ $^ $(LinkFiles)  $(NugetFiles)

# B) Main target executable
$(TARGETBIN)/Goedel.Trace.Documentation.exe :| $(TARGETBIN)
$(TARGETBIN)/Goedel.Trace.Documentation.exe : $(SourceFiles) $(LinkFiles) $(NugetFiles)
! $(CSHARPEXE) /out:$@  $(SourceFiles) -reference:$(LinkFilesComma) 


# Directories etc.

$(INSTALL_PROGRAM) :
! mkdir -p $(INSTALL_PROGRAM) 

$(INSTALL_DATA) :
! mkdir -p $(INSTALL_DATA) 

$(TARGETBIN) :
! mkdir -p $(TARGETBIN) 

$(TARGETEXE) :
! mkdir -p $(TARGETEXE) 

$(LIBRARYBIN) :
! mkdir -p $(LIBRARYBIN)

# Generated code

ifdef PHB_BUILD

Documentation.cs : Documentation.command
!  $(Custom_CommandCS) Documentation.command $(Custom_CommandCS_FLAG) Documentation.cs 
ExampleGenerator.cs : ExampleGenerator.script
!  $(Custom_GScript) ExampleGenerator.script $(Custom_GScript_FLAG) ExampleGenerator.cs 

endif

.PHONY : clean install publish debian rpm


# Referenced projects
#

# Recursive make targets, do not execute if the variable NORECURSE is defined
ifndef NORECURSE

../../Libraries/Goedel.Command/$(TARGETBIN)/Goedel.Command.dll : recursive
! $(MAKE) -C ../../Libraries/Goedel.Command

../../Libraries/Goedel.IO/$(TARGETBIN)/Goedel.IO.dll : recursive
! $(MAKE) -C ../../Libraries/Goedel.IO

../../Libraries/Goedel.Protocol.Debug/$(TARGETBIN)/Goedel.Protocol.Debug.dll : recursive
! $(MAKE) -C ../../Libraries/Goedel.Protocol.Debug

../../Libraries/Goedel.Protocol/$(TARGETBIN)/Goedel.Protocol.dll : recursive
! $(MAKE) -C ../../Libraries/Goedel.Protocol

../../Libraries/Goedel.Registry/$(TARGETBIN)/Goedel.Registry.dll : recursive
! $(MAKE) -C ../../Libraries/Goedel.Registry

../../Libraries/Goedel.Utilities/$(TARGETBIN)/Goedel.Utilities.dll : recursive
! $(MAKE) -C ../../Libraries/Goedel.Utilities

../Goedel.Trace.Client/$(TARGETBIN)/Goedel.Trace.Client.dll : recursive
! $(MAKE) -C ../Goedel.Trace.Client

../Goedel.Trace.Server/$(TARGETBIN)/Goedel.Trace.Server.dll : recursive
! $(MAKE) -C ../Goedel.Trace.Server

../Goedel.Trace/$(TARGETBIN)/Goedel.Trace.dll : recursive
! $(MAKE) -C ../Goedel.Trace

endif

$(TARGETBIN)/Goedel.Command.dll :| $(TARGETBIN)
$(TARGETBIN)/Goedel.Command.dll : ../../Libraries/Goedel.Command/$(TARGETBIN)/Goedel.Command.dll
!  cp ../../Libraries/Goedel.Command/$(TARGETBIN)/Goedel.Command.dll $(TARGETBIN)/Goedel.Command.dll

$(TARGETBIN)/Goedel.IO.dll :| $(TARGETBIN)
$(TARGETBIN)/Goedel.IO.dll : ../../Libraries/Goedel.IO/$(TARGETBIN)/Goedel.IO.dll
!  cp ../../Libraries/Goedel.IO/$(TARGETBIN)/Goedel.IO.dll $(TARGETBIN)/Goedel.IO.dll

$(TARGETBIN)/Goedel.Protocol.Debug.dll :| $(TARGETBIN)
$(TARGETBIN)/Goedel.Protocol.Debug.dll : ../../Libraries/Goedel.Protocol.Debug/$(TARGETBIN)/Goedel.Protocol.Debug.dll
!  cp ../../Libraries/Goedel.Protocol.Debug/$(TARGETBIN)/Goedel.Protocol.Debug.dll $(TARGETBIN)/Goedel.Protocol.Debug.dll

$(TARGETBIN)/Goedel.Protocol.dll :| $(TARGETBIN)
$(TARGETBIN)/Goedel.Protocol.dll : ../../Libraries/Goedel.Protocol/$(TARGETBIN)/Goedel.Protocol.dll
!  cp ../../Libraries/Goedel.Protocol/$(TARGETBIN)/Goedel.Protocol.dll $(TARGETBIN)/Goedel.Protocol.dll

$(TARGETBIN)/Goedel.Registry.dll :| $(TARGETBIN)
$(TARGETBIN)/Goedel.Registry.dll : ../../Libraries/Goedel.Registry/$(TARGETBIN)/Goedel.Registry.dll
!  cp ../../Libraries/Goedel.Registry/$(TARGETBIN)/Goedel.Registry.dll $(TARGETBIN)/Goedel.Registry.dll

$(TARGETBIN)/Goedel.Utilities.dll :| $(TARGETBIN)
$(TARGETBIN)/Goedel.Utilities.dll : ../../Libraries/Goedel.Utilities/$(TARGETBIN)/Goedel.Utilities.dll
!  cp ../../Libraries/Goedel.Utilities/$(TARGETBIN)/Goedel.Utilities.dll $(TARGETBIN)/Goedel.Utilities.dll

$(TARGETBIN)/Goedel.Trace.Client.dll :| $(TARGETBIN)
$(TARGETBIN)/Goedel.Trace.Client.dll : ../Goedel.Trace.Client/$(TARGETBIN)/Goedel.Trace.Client.dll
!  cp ../Goedel.Trace.Client/$(TARGETBIN)/Goedel.Trace.Client.dll $(TARGETBIN)/Goedel.Trace.Client.dll

$(TARGETBIN)/Goedel.Trace.Server.dll :| $(TARGETBIN)
$(TARGETBIN)/Goedel.Trace.Server.dll : ../Goedel.Trace.Server/$(TARGETBIN)/Goedel.Trace.Server.dll
!  cp ../Goedel.Trace.Server/$(TARGETBIN)/Goedel.Trace.Server.dll $(TARGETBIN)/Goedel.Trace.Server.dll

$(TARGETBIN)/Goedel.Trace.dll :| $(TARGETBIN)
$(TARGETBIN)/Goedel.Trace.dll : ../Goedel.Trace/$(TARGETBIN)/Goedel.Trace.dll
!  cp ../Goedel.Trace/$(TARGETBIN)/Goedel.Trace.dll $(TARGETBIN)/Goedel.Trace.dll


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


install : $(INSTALL_PROGRAM)/Goedel.Trace.Documentation 

$(INSTALL_PROGRAM)/Goedel.Trace.Documentation  :| $(INSTALL_PROGRAM)
$(INSTALL_PROGRAM)/Goedel.Trace.Documentation  : $(TARGETEXE)/Goedel.Trace.Documentation 
! cp $^ $@ 




# To Do List
#
# 1) Redo install target
#    Create a wrapper script and library 
# 2) Create a Debian target
# 3) Create a RPM target
# 4) Create a nuget target


