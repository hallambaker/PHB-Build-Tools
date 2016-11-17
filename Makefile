#
# Makefile for Visual Studio Solution ..
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

.PHONY : all always clean install publish cross

# Need to identify the target directory using UnixPath()
# This file in directory 

# Project : asn2.exe
# Item :  ASN/ShellASN
# Output :     ASN/ShellASN/$(TARGETEXE)/asn2.exe

all : ASN/ShellASN/$(TARGETBIN)/asn2.exe

ASN/ShellASN/$(TARGETBIN)/asn2.exe : Libraries/Goedel.ASN/$(TARGETBIN)/Goedel.ASN.dll

ASN/ShellASN/$(TARGETBIN)/asn2.exe : Libraries/Goedel.Registry/$(TARGETBIN)/Goedel.Registry.dll

ASN/ShellASN/$(TARGETBIN)/asn2.exe : Libraries/Goedel.Utilities/$(TARGETBIN)/Goedel.Utilities.dll

ASN/ShellASN/$(TARGETBIN)/asn2.exe : ASN/Goedel.Tool.ASN/$(TARGETBIN)/Goedel.Tool.ASN.dll


ASN/ShellASN/$(TARGETBIN)/asn2.exe : always
! echo "" >&2
! echo "*** Directory ASN/ShellASN" >&2
! make NORECURSE=true -C ASN/ShellASN

# Project : Goedel.Tool.ASN.dll
# Item :  ASN/Goedel.Tool.ASN
# Output :     ASN/Goedel.Tool.ASN/$(TARGETBIN)/Goedel.Tool.ASN.dll

all : ASN/Goedel.Tool.ASN/$(TARGETBIN)/Goedel.Tool.ASN.dll

ASN/Goedel.Tool.ASN/$(TARGETBIN)/Goedel.Tool.ASN.dll : Libraries/Goedel.ASN/$(TARGETBIN)/Goedel.ASN.dll

ASN/Goedel.Tool.ASN/$(TARGETBIN)/Goedel.Tool.ASN.dll : Libraries/Goedel.Registry/$(TARGETBIN)/Goedel.Registry.dll

ASN/Goedel.Tool.ASN/$(TARGETBIN)/Goedel.Tool.ASN.dll : Libraries/Goedel.Utilities/$(TARGETBIN)/Goedel.Utilities.dll


ASN/Goedel.Tool.ASN/$(TARGETBIN)/Goedel.Tool.ASN.dll : always
! echo "" >&2
! echo "*** Directory ASN/Goedel.Tool.ASN" >&2
! make NORECURSE=true -C ASN/Goedel.Tool.ASN

# Project : Goedel.Tool.Command.dll
# Item :  Command/Goedel.Tool.Command
# Output :     Command/Goedel.Tool.Command/$(TARGETBIN)/Goedel.Tool.Command.dll

all : Command/Goedel.Tool.Command/$(TARGETBIN)/Goedel.Tool.Command.dll

Command/Goedel.Tool.Command/$(TARGETBIN)/Goedel.Tool.Command.dll : Libraries/Goedel.Registry/$(TARGETBIN)/Goedel.Registry.dll

Command/Goedel.Tool.Command/$(TARGETBIN)/Goedel.Tool.Command.dll : Libraries/Goedel.Utilities/$(TARGETBIN)/Goedel.Utilities.dll


Command/Goedel.Tool.Command/$(TARGETBIN)/Goedel.Tool.Command.dll : always
! echo "" >&2
! echo "*** Directory Command/Goedel.Tool.Command" >&2
! make NORECURSE=true -C Command/Goedel.Tool.Command

# Project : domainer.exe
# Item :  Domainer/ShellDomainer
# Output :     Domainer/ShellDomainer/$(TARGETEXE)/domainer.exe

all : Domainer/ShellDomainer/$(TARGETBIN)/domainer.exe

Domainer/ShellDomainer/$(TARGETBIN)/domainer.exe : Libraries/Goedel.Registry/$(TARGETBIN)/Goedel.Registry.dll

Domainer/ShellDomainer/$(TARGETBIN)/domainer.exe : Libraries/Goedel.Utilities/$(TARGETBIN)/Goedel.Utilities.dll

Domainer/ShellDomainer/$(TARGETBIN)/domainer.exe : Domainer/Goedel.Tool.Domainer/$(TARGETBIN)/Goedel.Tool.Domainer.dll


Domainer/ShellDomainer/$(TARGETBIN)/domainer.exe : always
! echo "" >&2
! echo "*** Directory Domainer/ShellDomainer" >&2
! make NORECURSE=true -C Domainer/ShellDomainer

# Project : Goedel.Tool.Domainer.dll
# Item :  Domainer/Goedel.Tool.Domainer
# Output :     Domainer/Goedel.Tool.Domainer/$(TARGETBIN)/Goedel.Tool.Domainer.dll

all : Domainer/Goedel.Tool.Domainer/$(TARGETBIN)/Goedel.Tool.Domainer.dll

Domainer/Goedel.Tool.Domainer/$(TARGETBIN)/Goedel.Tool.Domainer.dll : Libraries/Goedel.Registry/$(TARGETBIN)/Goedel.Registry.dll

Domainer/Goedel.Tool.Domainer/$(TARGETBIN)/Goedel.Tool.Domainer.dll : Libraries/Goedel.Utilities/$(TARGETBIN)/Goedel.Utilities.dll


Domainer/Goedel.Tool.Domainer/$(TARGETBIN)/Goedel.Tool.Domainer.dll : always
! echo "" >&2
! echo "*** Directory Domainer/Goedel.Tool.Domainer" >&2
! make NORECURSE=true -C Domainer/Goedel.Tool.Domainer

# Project : exceptional.exe
# Item :  Exceptional/ShellExceptional
# Output :     Exceptional/ShellExceptional/$(TARGETEXE)/exceptional.exe

all : Exceptional/ShellExceptional/$(TARGETBIN)/exceptional.exe

Exceptional/ShellExceptional/$(TARGETBIN)/exceptional.exe : Libraries/Goedel.Registry/$(TARGETBIN)/Goedel.Registry.dll

Exceptional/ShellExceptional/$(TARGETBIN)/exceptional.exe : Libraries/Goedel.Utilities/$(TARGETBIN)/Goedel.Utilities.dll

Exceptional/ShellExceptional/$(TARGETBIN)/exceptional.exe : Exceptional/Goedel.Tool.Exceptional/$(TARGETBIN)/Goedel.Tool.Exceptional.dll


Exceptional/ShellExceptional/$(TARGETBIN)/exceptional.exe : always
! echo "" >&2
! echo "*** Directory Exceptional/ShellExceptional" >&2
! make NORECURSE=true -C Exceptional/ShellExceptional

# Project : Goedel.Tool.Exceptional.dll
# Item :  Exceptional/Goedel.Tool.Exceptional
# Output :     Exceptional/Goedel.Tool.Exceptional/$(TARGETBIN)/Goedel.Tool.Exceptional.dll

all : Exceptional/Goedel.Tool.Exceptional/$(TARGETBIN)/Goedel.Tool.Exceptional.dll

Exceptional/Goedel.Tool.Exceptional/$(TARGETBIN)/Goedel.Tool.Exceptional.dll : Libraries/Goedel.Registry/$(TARGETBIN)/Goedel.Registry.dll

Exceptional/Goedel.Tool.Exceptional/$(TARGETBIN)/Goedel.Tool.Exceptional.dll : Libraries/Goedel.Utilities/$(TARGETBIN)/Goedel.Utilities.dll


Exceptional/Goedel.Tool.Exceptional/$(TARGETBIN)/Goedel.Tool.Exceptional.dll : always
! echo "" >&2
! echo "*** Directory Exceptional/Goedel.Tool.Exceptional" >&2
! make NORECURSE=true -C Exceptional/Goedel.Tool.Exceptional

# Project : Goedel.Tool.FSRGen.dll
# Item :  FSRGen/Goedel.Tool.FSRGen
# Output :     FSRGen/Goedel.Tool.FSRGen/$(TARGETBIN)/Goedel.Tool.FSRGen.dll

all : FSRGen/Goedel.Tool.FSRGen/$(TARGETBIN)/Goedel.Tool.FSRGen.dll

FSRGen/Goedel.Tool.FSRGen/$(TARGETBIN)/Goedel.Tool.FSRGen.dll : Libraries/Goedel.Registry/$(TARGETBIN)/Goedel.Registry.dll

FSRGen/Goedel.Tool.FSRGen/$(TARGETBIN)/Goedel.Tool.FSRGen.dll : Libraries/Goedel.Utilities/$(TARGETBIN)/Goedel.Utilities.dll


FSRGen/Goedel.Tool.FSRGen/$(TARGETBIN)/Goedel.Tool.FSRGen.dll : always
! echo "" >&2
! echo "*** Directory FSRGen/Goedel.Tool.FSRGen" >&2
! make NORECURSE=true -C FSRGen/Goedel.Tool.FSRGen

# Project : fsrgen.exe
# Item :  FSRGen/ShellFSRGen
# Output :     FSRGen/ShellFSRGen/$(TARGETEXE)/fsrgen.exe

all : FSRGen/ShellFSRGen/$(TARGETBIN)/fsrgen.exe

FSRGen/ShellFSRGen/$(TARGETBIN)/fsrgen.exe : Libraries/Goedel.Registry/$(TARGETBIN)/Goedel.Registry.dll

FSRGen/ShellFSRGen/$(TARGETBIN)/fsrgen.exe : Libraries/Goedel.Utilities/$(TARGETBIN)/Goedel.Utilities.dll

FSRGen/ShellFSRGen/$(TARGETBIN)/fsrgen.exe : FSRGen/Goedel.Tool.FSRGen/$(TARGETBIN)/Goedel.Tool.FSRGen.dll


FSRGen/ShellFSRGen/$(TARGETBIN)/fsrgen.exe : always
! echo "" >&2
! echo "*** Directory FSRGen/ShellFSRGen" >&2
! make NORECURSE=true -C FSRGen/ShellFSRGen

# Project : Goedel.Tool.Script.dll
# Item :  GScript/Goedel.Tool.Script
# Output :     GScript/Goedel.Tool.Script/$(TARGETBIN)/Goedel.Tool.Script.dll

all : GScript/Goedel.Tool.Script/$(TARGETBIN)/Goedel.Tool.Script.dll

GScript/Goedel.Tool.Script/$(TARGETBIN)/Goedel.Tool.Script.dll : Libraries/Goedel.Registry/$(TARGETBIN)/Goedel.Registry.dll

GScript/Goedel.Tool.Script/$(TARGETBIN)/Goedel.Tool.Script.dll : Libraries/Goedel.Utilities/$(TARGETBIN)/Goedel.Utilities.dll


GScript/Goedel.Tool.Script/$(TARGETBIN)/Goedel.Tool.Script.dll : always
! echo "" >&2
! echo "*** Directory GScript/Goedel.Tool.Script" >&2
! make NORECURSE=true -C GScript/Goedel.Tool.Script

# Project : gscript.exe
# Item :  GScript/ShellGScript
# Output :     GScript/ShellGScript/$(TARGETEXE)/gscript.exe

all : GScript/ShellGScript/$(TARGETBIN)/gscript.exe

GScript/ShellGScript/$(TARGETBIN)/gscript.exe : Libraries/Goedel.Registry/$(TARGETBIN)/Goedel.Registry.dll

GScript/ShellGScript/$(TARGETBIN)/gscript.exe : Libraries/Goedel.Utilities/$(TARGETBIN)/Goedel.Utilities.dll

GScript/ShellGScript/$(TARGETBIN)/gscript.exe : GScript/Goedel.Tool.Script/$(TARGETBIN)/Goedel.Tool.Script.dll


GScript/ShellGScript/$(TARGETBIN)/gscript.exe : always
! echo "" >&2
! echo "*** Directory GScript/ShellGScript" >&2
! make NORECURSE=true -C GScript/ShellGScript

# Project : goedel3.exe
# Item :  Goedel3/ShellGoedel3
# Output :     Goedel3/ShellGoedel3/$(TARGETEXE)/goedel3.exe

all : Goedel3/ShellGoedel3/$(TARGETBIN)/goedel3.exe

Goedel3/ShellGoedel3/$(TARGETBIN)/goedel3.exe : Libraries/Goedel.Registry/$(TARGETBIN)/Goedel.Registry.dll

Goedel3/ShellGoedel3/$(TARGETBIN)/goedel3.exe : Libraries/Goedel.Utilities/$(TARGETBIN)/Goedel.Utilities.dll

Goedel3/ShellGoedel3/$(TARGETBIN)/goedel3.exe : Goedel3/Goedel.Tool.Schema/$(TARGETBIN)/Goedel.Tool.Schema.dll


Goedel3/ShellGoedel3/$(TARGETBIN)/goedel3.exe : always
! echo "" >&2
! echo "*** Directory Goedel3/ShellGoedel3" >&2
! make NORECURSE=true -C Goedel3/ShellGoedel3

# Project : Goedel.Tool.ProtoGen.dll
# Item :  Protogen/Goedel.Tool.ProtoGen
# Output :     Protogen/Goedel.Tool.ProtoGen/$(TARGETBIN)/Goedel.Tool.ProtoGen.dll

all : Protogen/Goedel.Tool.ProtoGen/$(TARGETBIN)/Goedel.Tool.ProtoGen.dll

Protogen/Goedel.Tool.ProtoGen/$(TARGETBIN)/Goedel.Tool.ProtoGen.dll : Libraries/Goedel.Registry/$(TARGETBIN)/Goedel.Registry.dll

Protogen/Goedel.Tool.ProtoGen/$(TARGETBIN)/Goedel.Tool.ProtoGen.dll : Libraries/Goedel.Utilities/$(TARGETBIN)/Goedel.Utilities.dll


Protogen/Goedel.Tool.ProtoGen/$(TARGETBIN)/Goedel.Tool.ProtoGen.dll : always
! echo "" >&2
! echo "*** Directory Protogen/Goedel.Tool.ProtoGen" >&2
! make NORECURSE=true -C Protogen/Goedel.Tool.ProtoGen

# Project : protogen.exe
# Item :  protogen/ShellProtoGen
# Output :     protogen/ShellProtoGen/$(TARGETEXE)/protogen.exe

all : protogen/ShellProtoGen/$(TARGETBIN)/protogen.exe

protogen/ShellProtoGen/$(TARGETBIN)/protogen.exe : Libraries/Goedel.Registry/$(TARGETBIN)/Goedel.Registry.dll

protogen/ShellProtoGen/$(TARGETBIN)/protogen.exe : Libraries/Goedel.Utilities/$(TARGETBIN)/Goedel.Utilities.dll

protogen/ShellProtoGen/$(TARGETBIN)/protogen.exe : Protogen/Goedel.Tool.ProtoGen/$(TARGETBIN)/Goedel.Tool.ProtoGen.dll


protogen/ShellProtoGen/$(TARGETBIN)/protogen.exe : always
! echo "" >&2
! echo "*** Directory protogen/ShellProtoGen" >&2
! make NORECURSE=true -C protogen/ShellProtoGen

# Project : Goedel.Tool.Schema.dll
# Item :  Goedel3/Goedel.Tool.Schema
# Output :     Goedel3/Goedel.Tool.Schema/$(TARGETBIN)/Goedel.Tool.Schema.dll

all : Goedel3/Goedel.Tool.Schema/$(TARGETBIN)/Goedel.Tool.Schema.dll

Goedel3/Goedel.Tool.Schema/$(TARGETBIN)/Goedel.Tool.Schema.dll : Libraries/Goedel.Registry/$(TARGETBIN)/Goedel.Registry.dll

Goedel3/Goedel.Tool.Schema/$(TARGETBIN)/Goedel.Tool.Schema.dll : Libraries/Goedel.Utilities/$(TARGETBIN)/Goedel.Utilities.dll


Goedel3/Goedel.Tool.Schema/$(TARGETBIN)/Goedel.Tool.Schema.dll : always
! echo "" >&2
! echo "*** Directory Goedel3/Goedel.Tool.Schema" >&2
! make NORECURSE=true -C Goedel3/Goedel.Tool.Schema

# Project : Goedel.Document.Markdown.dll
# Item :  RFCTool/Goedel.Document.Markdown
# Output :     RFCTool/Goedel.Document.Markdown/$(TARGETBIN)/Goedel.Document.Markdown.dll

all : RFCTool/Goedel.Document.Markdown/$(TARGETBIN)/Goedel.Document.Markdown.dll

RFCTool/Goedel.Document.Markdown/$(TARGETBIN)/Goedel.Document.Markdown.dll : Libraries/Goedel.FSR/$(TARGETBIN)/Goedel.FSR.dll

RFCTool/Goedel.Document.Markdown/$(TARGETBIN)/Goedel.Document.Markdown.dll : Libraries/Goedel.IO/$(TARGETBIN)/Goedel.IO.dll

RFCTool/Goedel.Document.Markdown/$(TARGETBIN)/Goedel.Document.Markdown.dll : Libraries/Goedel.Registry/$(TARGETBIN)/Goedel.Registry.dll

RFCTool/Goedel.Document.Markdown/$(TARGETBIN)/Goedel.Document.Markdown.dll : Libraries/Goedel.Utilities/$(TARGETBIN)/Goedel.Utilities.dll


RFCTool/Goedel.Document.Markdown/$(TARGETBIN)/Goedel.Document.Markdown.dll : always
! echo "" >&2
! echo "*** Directory RFCTool/Goedel.Document.Markdown" >&2
! make NORECURSE=true -C RFCTool/Goedel.Document.Markdown

# Project : Goedel.Document.RFC.Convert.dll
# Item :  RFCTool/Goedel.Document.RFC.Convert
# Output :     RFCTool/Goedel.Document.RFC.Convert/$(TARGETBIN)/Goedel.Document.RFC.Convert.dll

all : RFCTool/Goedel.Document.RFC.Convert/$(TARGETBIN)/Goedel.Document.RFC.Convert.dll

RFCTool/Goedel.Document.RFC.Convert/$(TARGETBIN)/Goedel.Document.RFC.Convert.dll : Libraries/Goedel.Registry/$(TARGETBIN)/Goedel.Registry.dll

RFCTool/Goedel.Document.RFC.Convert/$(TARGETBIN)/Goedel.Document.RFC.Convert.dll : Libraries/Goedel.Utilities/$(TARGETBIN)/Goedel.Utilities.dll

RFCTool/Goedel.Document.RFC.Convert/$(TARGETBIN)/Goedel.Document.RFC.Convert.dll : RFCTool/Goedel.Document.Markdown/$(TARGETBIN)/Goedel.Document.Markdown.dll

RFCTool/Goedel.Document.RFC.Convert/$(TARGETBIN)/Goedel.Document.RFC.Convert.dll : RFCTool/Goedel.Tool.RFCTool/$(TARGETBIN)/Goedel.Document.RFC.dll


RFCTool/Goedel.Document.RFC.Convert/$(TARGETBIN)/Goedel.Document.RFC.Convert.dll : always
! echo "" >&2
! echo "*** Directory RFCTool/Goedel.Document.RFC.Convert" >&2
! make NORECURSE=true -C RFCTool/Goedel.Document.RFC.Convert

# Project : registryconfig.exe
# Item :  RegistryConfig/ShellRegistryConfig
# Output :     RegistryConfig/ShellRegistryConfig/$(TARGETEXE)/registryconfig.exe

all : RegistryConfig/ShellRegistryConfig/$(TARGETBIN)/registryconfig.exe

RegistryConfig/ShellRegistryConfig/$(TARGETBIN)/registryconfig.exe : Libraries/Goedel.Registry/$(TARGETBIN)/Goedel.Registry.dll

RegistryConfig/ShellRegistryConfig/$(TARGETBIN)/registryconfig.exe : Libraries/Goedel.Utilities/$(TARGETBIN)/Goedel.Utilities.dll

RegistryConfig/ShellRegistryConfig/$(TARGETBIN)/registryconfig.exe : RegistryConfig/Goedel.Tool.RegistryConfig/$(TARGETBIN)/Goedel.Tool.RegistryConfig.dll


RegistryConfig/ShellRegistryConfig/$(TARGETBIN)/registryconfig.exe : always
! echo "" >&2
! echo "*** Directory RegistryConfig/ShellRegistryConfig" >&2
! make NORECURSE=true -C RegistryConfig/ShellRegistryConfig

# Project : Goedel.Tool.RegistryConfig.dll
# Item :  RegistryConfig/Goedel.Tool.RegistryConfig
# Output :     RegistryConfig/Goedel.Tool.RegistryConfig/$(TARGETBIN)/Goedel.Tool.RegistryConfig.dll

all : RegistryConfig/Goedel.Tool.RegistryConfig/$(TARGETBIN)/Goedel.Tool.RegistryConfig.dll

RegistryConfig/Goedel.Tool.RegistryConfig/$(TARGETBIN)/Goedel.Tool.RegistryConfig.dll : Libraries/Goedel.Registry/$(TARGETBIN)/Goedel.Registry.dll

RegistryConfig/Goedel.Tool.RegistryConfig/$(TARGETBIN)/Goedel.Tool.RegistryConfig.dll : Libraries/Goedel.Utilities/$(TARGETBIN)/Goedel.Utilities.dll


RegistryConfig/Goedel.Tool.RegistryConfig/$(TARGETBIN)/Goedel.Tool.RegistryConfig.dll : always
! echo "" >&2
! echo "*** Directory RegistryConfig/Goedel.Tool.RegistryConfig" >&2
! make NORECURSE=true -C RegistryConfig/Goedel.Tool.RegistryConfig

# Project : Goedel.Document.RFC.dll
# Item :  RFCTool/Goedel.Tool.RFCTool
# Output :     RFCTool/Goedel.Tool.RFCTool/$(TARGETBIN)/Goedel.Document.RFC.dll

all : RFCTool/Goedel.Tool.RFCTool/$(TARGETBIN)/Goedel.Document.RFC.dll

RFCTool/Goedel.Tool.RFCTool/$(TARGETBIN)/Goedel.Document.RFC.dll : Libraries/Goedel.Registry/$(TARGETBIN)/Goedel.Registry.dll

RFCTool/Goedel.Tool.RFCTool/$(TARGETBIN)/Goedel.Document.RFC.dll : Libraries/Goedel.Utilities/$(TARGETBIN)/Goedel.Utilities.dll

RFCTool/Goedel.Tool.RFCTool/$(TARGETBIN)/Goedel.Document.RFC.dll : RFCTool/Goedel.Document.Markdown/$(TARGETBIN)/Goedel.Document.Markdown.dll


RFCTool/Goedel.Tool.RFCTool/$(TARGETBIN)/Goedel.Document.RFC.dll : always
! echo "" >&2
! echo "*** Directory RFCTool/Goedel.Tool.RFCTool" >&2
! make NORECURSE=true -C RFCTool/Goedel.Tool.RFCTool

# Project : Goedel.Tool.VSIXBuild.dll
# Item :  VSIXBuild/Goedel.Tool.VSIXBuild
# Output :     VSIXBuild/Goedel.Tool.VSIXBuild/$(TARGETBIN)/Goedel.Tool.VSIXBuild.dll

all : VSIXBuild/Goedel.Tool.VSIXBuild/$(TARGETBIN)/Goedel.Tool.VSIXBuild.dll

VSIXBuild/Goedel.Tool.VSIXBuild/$(TARGETBIN)/Goedel.Tool.VSIXBuild.dll : Libraries/Goedel.Registry/$(TARGETBIN)/Goedel.Registry.dll

VSIXBuild/Goedel.Tool.VSIXBuild/$(TARGETBIN)/Goedel.Tool.VSIXBuild.dll : Libraries/Goedel.Utilities/$(TARGETBIN)/Goedel.Utilities.dll


VSIXBuild/Goedel.Tool.VSIXBuild/$(TARGETBIN)/Goedel.Tool.VSIXBuild.dll : always
! echo "" >&2
! echo "*** Directory VSIXBuild/Goedel.Tool.VSIXBuild" >&2
! make NORECURSE=true -C VSIXBuild/Goedel.Tool.VSIXBuild

# Project : rfctool.exe
# Item :  RFCTool/ShellRFCTool
# Output :     RFCTool/ShellRFCTool/$(TARGETEXE)/rfctool.exe

all : RFCTool/ShellRFCTool/$(TARGETBIN)/rfctool.exe

RFCTool/ShellRFCTool/$(TARGETBIN)/rfctool.exe : Libraries/Goedel.FSR/$(TARGETBIN)/Goedel.FSR.dll

RFCTool/ShellRFCTool/$(TARGETBIN)/rfctool.exe : Libraries/Goedel.IO/$(TARGETBIN)/Goedel.IO.dll

RFCTool/ShellRFCTool/$(TARGETBIN)/rfctool.exe : Libraries/Goedel.Registry/$(TARGETBIN)/Goedel.Registry.dll

RFCTool/ShellRFCTool/$(TARGETBIN)/rfctool.exe : Libraries/Goedel.Utilities/$(TARGETBIN)/Goedel.Utilities.dll

RFCTool/ShellRFCTool/$(TARGETBIN)/rfctool.exe : RFCTool/Goedel.Document.Markdown/$(TARGETBIN)/Goedel.Document.Markdown.dll

RFCTool/ShellRFCTool/$(TARGETBIN)/rfctool.exe : RFCTool/Goedel.Document.RFC.Convert/$(TARGETBIN)/Goedel.Document.RFC.Convert.dll

RFCTool/ShellRFCTool/$(TARGETBIN)/rfctool.exe : RFCTool/Goedel.Document.OpenXML/$(TARGETBIN)/Goedel.Document.OpenXML.dll

RFCTool/ShellRFCTool/$(TARGETBIN)/rfctool.exe : RFCTool/Goedel.Tool.RFCTool/$(TARGETBIN)/Goedel.Document.RFC.dll


RFCTool/ShellRFCTool/$(TARGETBIN)/rfctool.exe : always
! echo "" >&2
! echo "*** Directory RFCTool/ShellRFCTool" >&2
! make NORECURSE=true -C RFCTool/ShellRFCTool

# Project : Goedel.Document.OpenXML.dll
# Item :  RFCTool/Goedel.Document.OpenXML
# Output :     RFCTool/Goedel.Document.OpenXML/$(TARGETBIN)/Goedel.Document.OpenXML.dll

all : RFCTool/Goedel.Document.OpenXML/$(TARGETBIN)/Goedel.Document.OpenXML.dll

RFCTool/Goedel.Document.OpenXML/$(TARGETBIN)/Goedel.Document.OpenXML.dll : Libraries/Goedel.Registry/$(TARGETBIN)/Goedel.Registry.dll

RFCTool/Goedel.Document.OpenXML/$(TARGETBIN)/Goedel.Document.OpenXML.dll : Libraries/Goedel.Utilities/$(TARGETBIN)/Goedel.Utilities.dll

RFCTool/Goedel.Document.OpenXML/$(TARGETBIN)/Goedel.Document.OpenXML.dll : RFCTool/Goedel.Document.Markdown/$(TARGETBIN)/Goedel.Document.Markdown.dll

RFCTool/Goedel.Document.OpenXML/$(TARGETBIN)/Goedel.Document.OpenXML.dll : RFCTool/Goedel.Tool.RFCTool/$(TARGETBIN)/Goedel.Document.RFC.dll


RFCTool/Goedel.Document.OpenXML/$(TARGETBIN)/Goedel.Document.OpenXML.dll : always
! echo "" >&2
! echo "*** Directory RFCTool/Goedel.Document.OpenXML" >&2
! make NORECURSE=true -C RFCTool/Goedel.Document.OpenXML

# Project : libTrojanScript.dll
# Item :  Trojan3/Goedel.Trojan.Script
# Output :     Trojan3/Goedel.Trojan.Script/$(TARGETBIN)/libTrojanScript.dll

all : Trojan3/Goedel.Trojan.Script/$(TARGETBIN)/libTrojanScript.dll

Trojan3/Goedel.Trojan.Script/$(TARGETBIN)/libTrojanScript.dll : Libraries/Goedel.Registry/$(TARGETBIN)/Goedel.Registry.dll

Trojan3/Goedel.Trojan.Script/$(TARGETBIN)/libTrojanScript.dll : Libraries/Goedel.Utilities/$(TARGETBIN)/Goedel.Utilities.dll


Trojan3/Goedel.Trojan.Script/$(TARGETBIN)/libTrojanScript.dll : always
! echo "" >&2
! echo "*** Directory Trojan3/Goedel.Trojan.Script" >&2
! make NORECURSE=true -C Trojan3/Goedel.Trojan.Script

# Project : trojan.exe
# Item :  Trojan3/Trojan3
# Output :     Trojan3/Trojan3/$(TARGETEXE)/trojan.exe

all : Trojan3/Trojan3/$(TARGETBIN)/trojan.exe

Trojan3/Trojan3/$(TARGETBIN)/trojan.exe : Libraries/Goedel.Registry/$(TARGETBIN)/Goedel.Registry.dll

Trojan3/Trojan3/$(TARGETBIN)/trojan.exe : Libraries/Goedel.Utilities/$(TARGETBIN)/Goedel.Utilities.dll

Trojan3/Trojan3/$(TARGETBIN)/trojan.exe : RegistryConfig/Goedel.Tool.RegistryConfig/$(TARGETBIN)/Goedel.Tool.RegistryConfig.dll

Trojan3/Trojan3/$(TARGETBIN)/trojan.exe : Trojan3/Goedel.Trojan.Script/$(TARGETBIN)/libTrojanScript.dll


Trojan3/Trojan3/$(TARGETBIN)/trojan.exe : always
! echo "" >&2
! echo "*** Directory Trojan3/Trojan3" >&2
! make NORECURSE=true -C Trojan3/Trojan3

# Project : vsixbuild.exe
# Item :  VSIXBuild/ShellVSIXBuild
# Output :     VSIXBuild/ShellVSIXBuild/$(TARGETEXE)/vsixbuild.exe

all : VSIXBuild/ShellVSIXBuild/$(TARGETBIN)/vsixbuild.exe

VSIXBuild/ShellVSIXBuild/$(TARGETBIN)/vsixbuild.exe : Libraries/Goedel.Registry/$(TARGETBIN)/Goedel.Registry.dll

VSIXBuild/ShellVSIXBuild/$(TARGETBIN)/vsixbuild.exe : Libraries/Goedel.Utilities/$(TARGETBIN)/Goedel.Utilities.dll

VSIXBuild/ShellVSIXBuild/$(TARGETBIN)/vsixbuild.exe : VSIXBuild/Goedel.Tool.VSIXBuild/$(TARGETBIN)/Goedel.Tool.VSIXBuild.dll


VSIXBuild/ShellVSIXBuild/$(TARGETBIN)/vsixbuild.exe : always
! echo "" >&2
! echo "*** Directory VSIXBuild/ShellVSIXBuild" >&2
! make NORECURSE=true -C VSIXBuild/ShellVSIXBuild

# Project : Goedel.Tool.Makey.dll
# Item :  MakeyMakey/Goedel.Tools.Makey
# Output :     MakeyMakey/Goedel.Tools.Makey/$(TARGETBIN)/Goedel.Tool.Makey.dll

all : MakeyMakey/Goedel.Tools.Makey/$(TARGETBIN)/Goedel.Tool.Makey.dll

MakeyMakey/Goedel.Tools.Makey/$(TARGETBIN)/Goedel.Tool.Makey.dll : Libraries/Goedel.Registry/$(TARGETBIN)/Goedel.Registry.dll

MakeyMakey/Goedel.Tools.Makey/$(TARGETBIN)/Goedel.Tool.Makey.dll : Libraries/Goedel.Utilities/$(TARGETBIN)/Goedel.Utilities.dll


MakeyMakey/Goedel.Tools.Makey/$(TARGETBIN)/Goedel.Tool.Makey.dll : always
! echo "" >&2
! echo "*** Directory MakeyMakey/Goedel.Tools.Makey" >&2
! make NORECURSE=true -C MakeyMakey/Goedel.Tools.Makey

# Project : Makey.exe
# Item :  MakeyMakey/ShellMakey
# Output :     MakeyMakey/ShellMakey/$(TARGETEXE)/Makey.exe

all : MakeyMakey/ShellMakey/$(TARGETBIN)/Makey.exe

MakeyMakey/ShellMakey/$(TARGETBIN)/Makey.exe : Libraries/Goedel.Registry/$(TARGETBIN)/Goedel.Registry.dll

MakeyMakey/ShellMakey/$(TARGETBIN)/Makey.exe : Libraries/Goedel.Utilities/$(TARGETBIN)/Goedel.Utilities.dll

MakeyMakey/ShellMakey/$(TARGETBIN)/Makey.exe : MakeyMakey/Goedel.Tools.Makey/$(TARGETBIN)/Goedel.Tool.Makey.dll


MakeyMakey/ShellMakey/$(TARGETBIN)/Makey.exe : always
! echo "" >&2
! echo "*** Directory MakeyMakey/ShellMakey" >&2
! make NORECURSE=true -C MakeyMakey/ShellMakey

# Project : commandparse.exe
# Item :  Command/ShellCommand
# Output :     Command/ShellCommand/$(TARGETEXE)/commandparse.exe

all : Command/ShellCommand/$(TARGETBIN)/commandparse.exe

Command/ShellCommand/$(TARGETBIN)/commandparse.exe : Libraries/Goedel.Registry/$(TARGETBIN)/Goedel.Registry.dll

Command/ShellCommand/$(TARGETBIN)/commandparse.exe : Libraries/Goedel.Utilities/$(TARGETBIN)/Goedel.Utilities.dll

Command/ShellCommand/$(TARGETBIN)/commandparse.exe : Command/Goedel.Tool.Command/$(TARGETBIN)/Goedel.Tool.Command.dll


Command/ShellCommand/$(TARGETBIN)/commandparse.exe : always
! echo "" >&2
! echo "*** Directory Command/ShellCommand" >&2
! make NORECURSE=true -C Command/ShellCommand

# Project : Packager.exe
# Item :  Packager
# Output :     Packager/$(TARGETEXE)/Packager.exe

all : Packager/$(TARGETBIN)/Packager.exe


Packager/$(TARGETBIN)/Packager.exe : always
! echo "" >&2
! echo "*** Directory Packager" >&2
! make NORECURSE=true -C Packager

# Project : Goedel.Utilities.dll
# Item :  Libraries/Goedel.Utilities
# Output :     Libraries/Goedel.Utilities/$(TARGETBIN)/Goedel.Utilities.dll

all : Libraries/Goedel.Utilities/$(TARGETBIN)/Goedel.Utilities.dll


Libraries/Goedel.Utilities/$(TARGETBIN)/Goedel.Utilities.dll : always
! echo "" >&2
! echo "*** Directory Libraries/Goedel.Utilities" >&2
! make NORECURSE=true -C Libraries/Goedel.Utilities

# Project : Goedel.Registry.dll
# Item :  Libraries/Goedel.Registry
# Output :     Libraries/Goedel.Registry/$(TARGETBIN)/Goedel.Registry.dll

all : Libraries/Goedel.Registry/$(TARGETBIN)/Goedel.Registry.dll

Libraries/Goedel.Registry/$(TARGETBIN)/Goedel.Registry.dll : Libraries/Goedel.Utilities/$(TARGETBIN)/Goedel.Utilities.dll


Libraries/Goedel.Registry/$(TARGETBIN)/Goedel.Registry.dll : always
! echo "" >&2
! echo "*** Directory Libraries/Goedel.Registry" >&2
! make NORECURSE=true -C Libraries/Goedel.Registry

# Project : Goedel.FSR.dll
# Item :  Libraries/Goedel.FSR
# Output :     Libraries/Goedel.FSR/$(TARGETBIN)/Goedel.FSR.dll

all : Libraries/Goedel.FSR/$(TARGETBIN)/Goedel.FSR.dll

Libraries/Goedel.FSR/$(TARGETBIN)/Goedel.FSR.dll : Libraries/Goedel.Utilities/$(TARGETBIN)/Goedel.Utilities.dll


Libraries/Goedel.FSR/$(TARGETBIN)/Goedel.FSR.dll : always
! echo "" >&2
! echo "*** Directory Libraries/Goedel.FSR" >&2
! make NORECURSE=true -C Libraries/Goedel.FSR

# Project : Goedel.IO.dll
# Item :  Libraries/Goedel.IO
# Output :     Libraries/Goedel.IO/$(TARGETBIN)/Goedel.IO.dll

all : Libraries/Goedel.IO/$(TARGETBIN)/Goedel.IO.dll


Libraries/Goedel.IO/$(TARGETBIN)/Goedel.IO.dll : always
! echo "" >&2
! echo "*** Directory Libraries/Goedel.IO" >&2
! make NORECURSE=true -C Libraries/Goedel.IO

# Project : Goedel.ASN.dll
# Item :  Libraries/Goedel.ASN
# Output :     Libraries/Goedel.ASN/$(TARGETBIN)/Goedel.ASN.dll

all : Libraries/Goedel.ASN/$(TARGETBIN)/Goedel.ASN.dll

Libraries/Goedel.ASN/$(TARGETBIN)/Goedel.ASN.dll : Libraries/Goedel.Utilities/$(TARGETBIN)/Goedel.Utilities.dll


Libraries/Goedel.ASN/$(TARGETBIN)/Goedel.ASN.dll : always
! echo "" >&2
! echo "*** Directory Libraries/Goedel.ASN" >&2
! make NORECURSE=true -C Libraries/Goedel.ASN

# Project : bootmaker.exe
# Item :  RFCTool/ShellBootMaker
# Output :     RFCTool/ShellBootMaker/$(TARGETEXE)/bootmaker.exe

all : RFCTool/ShellBootMaker/$(TARGETBIN)/bootmaker.exe

RFCTool/ShellBootMaker/$(TARGETBIN)/bootmaker.exe : Libraries/Goedel.FSR/$(TARGETBIN)/Goedel.FSR.dll

RFCTool/ShellBootMaker/$(TARGETBIN)/bootmaker.exe : Libraries/Goedel.Registry/$(TARGETBIN)/Goedel.Registry.dll

RFCTool/ShellBootMaker/$(TARGETBIN)/bootmaker.exe : Libraries/Goedel.Utilities/$(TARGETBIN)/Goedel.Utilities.dll

RFCTool/ShellBootMaker/$(TARGETBIN)/bootmaker.exe : RFCTool/Goedel.Document.Markdown/$(TARGETBIN)/Goedel.Document.Markdown.dll

RFCTool/ShellBootMaker/$(TARGETBIN)/bootmaker.exe : RFCTool/Goedel.Document.OpenXML/$(TARGETBIN)/Goedel.Document.OpenXML.dll


RFCTool/ShellBootMaker/$(TARGETBIN)/bootmaker.exe : always
! echo "" >&2
! echo "*** Directory RFCTool/ShellBootMaker" >&2
! make NORECURSE=true -C RFCTool/ShellBootMaker



# clean all projects
clean :
! make clean NORECURSE=true -C ASN/ShellASN
! make clean NORECURSE=true -C ASN/Goedel.Tool.ASN
! make clean NORECURSE=true -C Command/Goedel.Tool.Command
! make clean NORECURSE=true -C Domainer/ShellDomainer
! make clean NORECURSE=true -C Domainer/Goedel.Tool.Domainer
! make clean NORECURSE=true -C Exceptional/ShellExceptional
! make clean NORECURSE=true -C Exceptional/Goedel.Tool.Exceptional
! make clean NORECURSE=true -C FSRGen/Goedel.Tool.FSRGen
! make clean NORECURSE=true -C FSRGen/ShellFSRGen
! make clean NORECURSE=true -C GScript/Goedel.Tool.Script
! make clean NORECURSE=true -C GScript/ShellGScript
! make clean NORECURSE=true -C Goedel3/ShellGoedel3
! make clean NORECURSE=true -C Protogen/Goedel.Tool.ProtoGen
! make clean NORECURSE=true -C protogen/ShellProtoGen
! make clean NORECURSE=true -C Goedel3/Goedel.Tool.Schema
! make clean NORECURSE=true -C RFCTool/Goedel.Document.Markdown
! make clean NORECURSE=true -C RFCTool/Goedel.Document.RFC.Convert
! make clean NORECURSE=true -C RegistryConfig/ShellRegistryConfig
! make clean NORECURSE=true -C RegistryConfig/Goedel.Tool.RegistryConfig
! make clean NORECURSE=true -C RFCTool/Goedel.Tool.RFCTool
! make clean NORECURSE=true -C VSIXBuild/Goedel.Tool.VSIXBuild
! make clean NORECURSE=true -C RFCTool/ShellRFCTool
! make clean NORECURSE=true -C RFCTool/Goedel.Document.OpenXML
! make clean NORECURSE=true -C Trojan3/Goedel.Trojan.Script
! make clean NORECURSE=true -C Trojan3/Trojan3
! make clean NORECURSE=true -C VSIXBuild/ShellVSIXBuild
! make clean NORECURSE=true -C MakeyMakey/Goedel.Tools.Makey
! make clean NORECURSE=true -C MakeyMakey/ShellMakey
! make clean NORECURSE=true -C Command/ShellCommand
! make clean NORECURSE=true -C Packager
! make clean NORECURSE=true -C Libraries/Goedel.Utilities
! make clean NORECURSE=true -C Libraries/Goedel.Registry
! make clean NORECURSE=true -C Libraries/Goedel.FSR
! make clean NORECURSE=true -C Libraries/Goedel.IO
! make clean NORECURSE=true -C Libraries/Goedel.ASN
! make clean NORECURSE=true -C RFCTool/ShellBootMaker

# publish all projects
publish : all
! make publish NORECURSE=true -C ASN/ShellASN
! make publish NORECURSE=true -C ASN/Goedel.Tool.ASN
! make publish NORECURSE=true -C Command/Goedel.Tool.Command
! make publish NORECURSE=true -C Domainer/ShellDomainer
! make publish NORECURSE=true -C Domainer/Goedel.Tool.Domainer
! make publish NORECURSE=true -C Exceptional/ShellExceptional
! make publish NORECURSE=true -C Exceptional/Goedel.Tool.Exceptional
! make publish NORECURSE=true -C FSRGen/Goedel.Tool.FSRGen
! make publish NORECURSE=true -C FSRGen/ShellFSRGen
! make publish NORECURSE=true -C GScript/Goedel.Tool.Script
! make publish NORECURSE=true -C GScript/ShellGScript
! make publish NORECURSE=true -C Goedel3/ShellGoedel3
! make publish NORECURSE=true -C Protogen/Goedel.Tool.ProtoGen
! make publish NORECURSE=true -C protogen/ShellProtoGen
! make publish NORECURSE=true -C Goedel3/Goedel.Tool.Schema
! make publish NORECURSE=true -C RFCTool/Goedel.Document.Markdown
! make publish NORECURSE=true -C RFCTool/Goedel.Document.RFC.Convert
! make publish NORECURSE=true -C RegistryConfig/ShellRegistryConfig
! make publish NORECURSE=true -C RegistryConfig/Goedel.Tool.RegistryConfig
! make publish NORECURSE=true -C RFCTool/Goedel.Tool.RFCTool
! make publish NORECURSE=true -C VSIXBuild/Goedel.Tool.VSIXBuild
! make publish NORECURSE=true -C RFCTool/ShellRFCTool
! make publish NORECURSE=true -C RFCTool/Goedel.Document.OpenXML
! make publish NORECURSE=true -C Trojan3/Goedel.Trojan.Script
! make publish NORECURSE=true -C Trojan3/Trojan3
! make publish NORECURSE=true -C VSIXBuild/ShellVSIXBuild
! make publish NORECURSE=true -C MakeyMakey/Goedel.Tools.Makey
! make publish NORECURSE=true -C MakeyMakey/ShellMakey
! make publish NORECURSE=true -C Command/ShellCommand
! make publish NORECURSE=true -C Packager
! make publish NORECURSE=true -C Libraries/Goedel.Utilities
! make publish NORECURSE=true -C Libraries/Goedel.Registry
! make publish NORECURSE=true -C Libraries/Goedel.FSR
! make publish NORECURSE=true -C Libraries/Goedel.IO
! make publish NORECURSE=true -C Libraries/Goedel.ASN
! make publish NORECURSE=true -C RFCTool/ShellBootMaker

# install all projects
install : all
! make install NORECURSE=true -C ASN/ShellASN
! make install NORECURSE=true -C ASN/Goedel.Tool.ASN
! make install NORECURSE=true -C Command/Goedel.Tool.Command
! make install NORECURSE=true -C Domainer/ShellDomainer
! make install NORECURSE=true -C Domainer/Goedel.Tool.Domainer
! make install NORECURSE=true -C Exceptional/ShellExceptional
! make install NORECURSE=true -C Exceptional/Goedel.Tool.Exceptional
! make install NORECURSE=true -C FSRGen/Goedel.Tool.FSRGen
! make install NORECURSE=true -C FSRGen/ShellFSRGen
! make install NORECURSE=true -C GScript/Goedel.Tool.Script
! make install NORECURSE=true -C GScript/ShellGScript
! make install NORECURSE=true -C Goedel3/ShellGoedel3
! make install NORECURSE=true -C Protogen/Goedel.Tool.ProtoGen
! make install NORECURSE=true -C protogen/ShellProtoGen
! make install NORECURSE=true -C Goedel3/Goedel.Tool.Schema
! make install NORECURSE=true -C RFCTool/Goedel.Document.Markdown
! make install NORECURSE=true -C RFCTool/Goedel.Document.RFC.Convert
! make install NORECURSE=true -C RegistryConfig/ShellRegistryConfig
! make install NORECURSE=true -C RegistryConfig/Goedel.Tool.RegistryConfig
! make install NORECURSE=true -C RFCTool/Goedel.Tool.RFCTool
! make install NORECURSE=true -C VSIXBuild/Goedel.Tool.VSIXBuild
! make install NORECURSE=true -C RFCTool/ShellRFCTool
! make install NORECURSE=true -C RFCTool/Goedel.Document.OpenXML
! make install NORECURSE=true -C Trojan3/Goedel.Trojan.Script
! make install NORECURSE=true -C Trojan3/Trojan3
! make install NORECURSE=true -C VSIXBuild/ShellVSIXBuild
! make install NORECURSE=true -C MakeyMakey/Goedel.Tools.Makey
! make install NORECURSE=true -C MakeyMakey/ShellMakey
! make install NORECURSE=true -C Command/ShellCommand
! make install NORECURSE=true -C Packager
! make install NORECURSE=true -C Libraries/Goedel.Utilities
! make install NORECURSE=true -C Libraries/Goedel.Registry
! make install NORECURSE=true -C Libraries/Goedel.FSR
! make install NORECURSE=true -C Libraries/Goedel.IO
! make install NORECURSE=true -C Libraries/Goedel.ASN
! make install NORECURSE=true -C RFCTool/ShellBootMaker

