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

ASN/ShellASN/$(TARGETBIN)/asn2.exe : ASN/ShellASN/../../Libraries/$(TARGETBIN)/Goedel.ASN.dll

ASN/ShellASN/$(TARGETBIN)/asn2.exe : ASN/ShellASN/../../Libraries/$(TARGETBIN)/Goedel.Command.dll

ASN/ShellASN/$(TARGETBIN)/asn2.exe : ASN/ShellASN/../../Libraries/$(TARGETBIN)/Goedel.Registry.dll

ASN/ShellASN/$(TARGETBIN)/asn2.exe : ASN/ShellASN/../../Libraries/$(TARGETBIN)/Goedel.Utilities.dll

ASN/ShellASN/$(TARGETBIN)/asn2.exe : ASN/ShellASN/../$(TARGETBIN)/Goedel.Tool.ASN.dll


ASN/ShellASN/$(TARGETBIN)/asn2.exe : always
! echo "" >&2
! echo "*** Directory ASN/ShellASN" >&2
! make NORECURSE=true -C ASN/ShellASN

# Project : Goedel.Tool.ASN.dll
# Item :  ASN/Goedel.Tool.ASN
# Output :     ASN/Goedel.Tool.ASN/$(TARGETBIN)/Goedel.Tool.ASN.dll

all : ASN/Goedel.Tool.ASN/$(TARGETBIN)/Goedel.Tool.ASN.dll

ASN/Goedel.Tool.ASN/$(TARGETBIN)/Goedel.Tool.ASN.dll : ASN/Goedel.Tool.ASN/../../Libraries/$(TARGETBIN)/Goedel.ASN.dll

ASN/Goedel.Tool.ASN/$(TARGETBIN)/Goedel.Tool.ASN.dll : ASN/Goedel.Tool.ASN/../../Libraries/$(TARGETBIN)/Goedel.Registry.dll

ASN/Goedel.Tool.ASN/$(TARGETBIN)/Goedel.Tool.ASN.dll : ASN/Goedel.Tool.ASN/../../Libraries/$(TARGETBIN)/Goedel.Utilities.dll


ASN/Goedel.Tool.ASN/$(TARGETBIN)/Goedel.Tool.ASN.dll : always
! echo "" >&2
! echo "*** Directory ASN/Goedel.Tool.ASN" >&2
! make NORECURSE=true -C ASN/Goedel.Tool.ASN

# Project : Goedel.Tool.Command.dll
# Item :  Command/Goedel.Tool.Command
# Output :     Command/Goedel.Tool.Command/$(TARGETBIN)/Goedel.Tool.Command.dll

all : Command/Goedel.Tool.Command/$(TARGETBIN)/Goedel.Tool.Command.dll

Command/Goedel.Tool.Command/$(TARGETBIN)/Goedel.Tool.Command.dll : Command/Goedel.Tool.Command/../../Libraries/$(TARGETBIN)/Goedel.Registry.dll

Command/Goedel.Tool.Command/$(TARGETBIN)/Goedel.Tool.Command.dll : Command/Goedel.Tool.Command/../../Libraries/$(TARGETBIN)/Goedel.Utilities.dll


Command/Goedel.Tool.Command/$(TARGETBIN)/Goedel.Tool.Command.dll : always
! echo "" >&2
! echo "*** Directory Command/Goedel.Tool.Command" >&2
! make NORECURSE=true -C Command/Goedel.Tool.Command

# Project : domainer.exe
# Item :  Domainer/ShellDomainer
# Output :     Domainer/ShellDomainer/$(TARGETEXE)/domainer.exe

all : Domainer/ShellDomainer/$(TARGETBIN)/domainer.exe

Domainer/ShellDomainer/$(TARGETBIN)/domainer.exe : Domainer/ShellDomainer/../../Libraries/$(TARGETBIN)/Goedel.Command.dll

Domainer/ShellDomainer/$(TARGETBIN)/domainer.exe : Domainer/ShellDomainer/../../Libraries/$(TARGETBIN)/Goedel.Registry.dll

Domainer/ShellDomainer/$(TARGETBIN)/domainer.exe : Domainer/ShellDomainer/../../Libraries/$(TARGETBIN)/Goedel.Utilities.dll

Domainer/ShellDomainer/$(TARGETBIN)/domainer.exe : Domainer/ShellDomainer/../$(TARGETBIN)/Goedel.Tool.Domainer.dll


Domainer/ShellDomainer/$(TARGETBIN)/domainer.exe : always
! echo "" >&2
! echo "*** Directory Domainer/ShellDomainer" >&2
! make NORECURSE=true -C Domainer/ShellDomainer

# Project : Goedel.Tool.Domainer.dll
# Item :  Domainer/Goedel.Tool.Domainer
# Output :     Domainer/Goedel.Tool.Domainer/$(TARGETBIN)/Goedel.Tool.Domainer.dll

all : Domainer/Goedel.Tool.Domainer/$(TARGETBIN)/Goedel.Tool.Domainer.dll

Domainer/Goedel.Tool.Domainer/$(TARGETBIN)/Goedel.Tool.Domainer.dll : Domainer/Goedel.Tool.Domainer/../../Libraries/$(TARGETBIN)/Goedel.Registry.dll

Domainer/Goedel.Tool.Domainer/$(TARGETBIN)/Goedel.Tool.Domainer.dll : Domainer/Goedel.Tool.Domainer/../../Libraries/$(TARGETBIN)/Goedel.Utilities.dll


Domainer/Goedel.Tool.Domainer/$(TARGETBIN)/Goedel.Tool.Domainer.dll : always
! echo "" >&2
! echo "*** Directory Domainer/Goedel.Tool.Domainer" >&2
! make NORECURSE=true -C Domainer/Goedel.Tool.Domainer

# Project : exceptional.exe
# Item :  Exceptional/ShellExceptional
# Output :     Exceptional/ShellExceptional/$(TARGETEXE)/exceptional.exe

all : Exceptional/ShellExceptional/$(TARGETBIN)/exceptional.exe

Exceptional/ShellExceptional/$(TARGETBIN)/exceptional.exe : Exceptional/ShellExceptional/../../Libraries/$(TARGETBIN)/Goedel.Command.dll

Exceptional/ShellExceptional/$(TARGETBIN)/exceptional.exe : Exceptional/ShellExceptional/../../Libraries/$(TARGETBIN)/Goedel.Registry.dll

Exceptional/ShellExceptional/$(TARGETBIN)/exceptional.exe : Exceptional/ShellExceptional/../../Libraries/$(TARGETBIN)/Goedel.Utilities.dll

Exceptional/ShellExceptional/$(TARGETBIN)/exceptional.exe : Exceptional/ShellExceptional/../$(TARGETBIN)/Goedel.Tool.Exceptional.dll


Exceptional/ShellExceptional/$(TARGETBIN)/exceptional.exe : always
! echo "" >&2
! echo "*** Directory Exceptional/ShellExceptional" >&2
! make NORECURSE=true -C Exceptional/ShellExceptional

# Project : Goedel.Tool.Exceptional.dll
# Item :  Exceptional/Goedel.Tool.Exceptional
# Output :     Exceptional/Goedel.Tool.Exceptional/$(TARGETBIN)/Goedel.Tool.Exceptional.dll

all : Exceptional/Goedel.Tool.Exceptional/$(TARGETBIN)/Goedel.Tool.Exceptional.dll

Exceptional/Goedel.Tool.Exceptional/$(TARGETBIN)/Goedel.Tool.Exceptional.dll : Exceptional/Goedel.Tool.Exceptional/../../Libraries/$(TARGETBIN)/Goedel.Registry.dll

Exceptional/Goedel.Tool.Exceptional/$(TARGETBIN)/Goedel.Tool.Exceptional.dll : Exceptional/Goedel.Tool.Exceptional/../../Libraries/$(TARGETBIN)/Goedel.Utilities.dll


Exceptional/Goedel.Tool.Exceptional/$(TARGETBIN)/Goedel.Tool.Exceptional.dll : always
! echo "" >&2
! echo "*** Directory Exceptional/Goedel.Tool.Exceptional" >&2
! make NORECURSE=true -C Exceptional/Goedel.Tool.Exceptional

# Project : Goedel.Tool.FSRGen.dll
# Item :  FSRGen/Goedel.Tool.FSRGen
# Output :     FSRGen/Goedel.Tool.FSRGen/$(TARGETBIN)/Goedel.Tool.FSRGen.dll

all : FSRGen/Goedel.Tool.FSRGen/$(TARGETBIN)/Goedel.Tool.FSRGen.dll

FSRGen/Goedel.Tool.FSRGen/$(TARGETBIN)/Goedel.Tool.FSRGen.dll : FSRGen/Goedel.Tool.FSRGen/../../Libraries/$(TARGETBIN)/Goedel.Registry.dll

FSRGen/Goedel.Tool.FSRGen/$(TARGETBIN)/Goedel.Tool.FSRGen.dll : FSRGen/Goedel.Tool.FSRGen/../../Libraries/$(TARGETBIN)/Goedel.Utilities.dll


FSRGen/Goedel.Tool.FSRGen/$(TARGETBIN)/Goedel.Tool.FSRGen.dll : always
! echo "" >&2
! echo "*** Directory FSRGen/Goedel.Tool.FSRGen" >&2
! make NORECURSE=true -C FSRGen/Goedel.Tool.FSRGen

# Project : fsrgen.exe
# Item :  FSRGen/ShellFSRGen
# Output :     FSRGen/ShellFSRGen/$(TARGETEXE)/fsrgen.exe

all : FSRGen/ShellFSRGen/$(TARGETBIN)/fsrgen.exe

FSRGen/ShellFSRGen/$(TARGETBIN)/fsrgen.exe : FSRGen/ShellFSRGen/../../Libraries/$(TARGETBIN)/Goedel.Command.dll

FSRGen/ShellFSRGen/$(TARGETBIN)/fsrgen.exe : FSRGen/ShellFSRGen/../../Libraries/$(TARGETBIN)/Goedel.Registry.dll

FSRGen/ShellFSRGen/$(TARGETBIN)/fsrgen.exe : FSRGen/ShellFSRGen/../../Libraries/$(TARGETBIN)/Goedel.Utilities.dll

FSRGen/ShellFSRGen/$(TARGETBIN)/fsrgen.exe : FSRGen/ShellFSRGen/../$(TARGETBIN)/Goedel.Tool.FSRGen.dll


FSRGen/ShellFSRGen/$(TARGETBIN)/fsrgen.exe : always
! echo "" >&2
! echo "*** Directory FSRGen/ShellFSRGen" >&2
! make NORECURSE=true -C FSRGen/ShellFSRGen

# Project : Goedel.Tool.Script.dll
# Item :  GScript/Goedel.Tool.Script
# Output :     GScript/Goedel.Tool.Script/$(TARGETBIN)/Goedel.Tool.Script.dll

all : GScript/Goedel.Tool.Script/$(TARGETBIN)/Goedel.Tool.Script.dll

GScript/Goedel.Tool.Script/$(TARGETBIN)/Goedel.Tool.Script.dll : GScript/Goedel.Tool.Script/../../Libraries/$(TARGETBIN)/Goedel.Registry.dll

GScript/Goedel.Tool.Script/$(TARGETBIN)/Goedel.Tool.Script.dll : GScript/Goedel.Tool.Script/../../Libraries/$(TARGETBIN)/Goedel.Utilities.dll


GScript/Goedel.Tool.Script/$(TARGETBIN)/Goedel.Tool.Script.dll : always
! echo "" >&2
! echo "*** Directory GScript/Goedel.Tool.Script" >&2
! make NORECURSE=true -C GScript/Goedel.Tool.Script

# Project : gscript.exe
# Item :  GScript/ShellGScript
# Output :     GScript/ShellGScript/$(TARGETEXE)/gscript.exe

all : GScript/ShellGScript/$(TARGETBIN)/gscript.exe

GScript/ShellGScript/$(TARGETBIN)/gscript.exe : GScript/ShellGScript/../../Libraries/$(TARGETBIN)/Goedel.Command.dll

GScript/ShellGScript/$(TARGETBIN)/gscript.exe : GScript/ShellGScript/../../Libraries/$(TARGETBIN)/Goedel.Registry.dll

GScript/ShellGScript/$(TARGETBIN)/gscript.exe : GScript/ShellGScript/../../Libraries/$(TARGETBIN)/Goedel.Utilities.dll

GScript/ShellGScript/$(TARGETBIN)/gscript.exe : GScript/ShellGScript/../$(TARGETBIN)/Goedel.Tool.Script.dll


GScript/ShellGScript/$(TARGETBIN)/gscript.exe : always
! echo "" >&2
! echo "*** Directory GScript/ShellGScript" >&2
! make NORECURSE=true -C GScript/ShellGScript

# Project : goedel3.exe
# Item :  Goedel3/ShellGoedel3
# Output :     Goedel3/ShellGoedel3/$(TARGETEXE)/goedel3.exe

all : Goedel3/ShellGoedel3/$(TARGETBIN)/goedel3.exe

Goedel3/ShellGoedel3/$(TARGETBIN)/goedel3.exe : Goedel3/ShellGoedel3/../../Libraries/$(TARGETBIN)/Goedel.Command.dll

Goedel3/ShellGoedel3/$(TARGETBIN)/goedel3.exe : Goedel3/ShellGoedel3/../../Libraries/$(TARGETBIN)/Goedel.Registry.dll

Goedel3/ShellGoedel3/$(TARGETBIN)/goedel3.exe : Goedel3/ShellGoedel3/../../Libraries/$(TARGETBIN)/Goedel.Utilities.dll

Goedel3/ShellGoedel3/$(TARGETBIN)/goedel3.exe : Goedel3/ShellGoedel3/../$(TARGETBIN)/Goedel.Tool.Schema.dll


Goedel3/ShellGoedel3/$(TARGETBIN)/goedel3.exe : always
! echo "" >&2
! echo "*** Directory Goedel3/ShellGoedel3" >&2
! make NORECURSE=true -C Goedel3/ShellGoedel3

# Project : Goedel.Tool.ProtoGen.dll
# Item :  Protogen/Goedel.Tool.ProtoGen
# Output :     Protogen/Goedel.Tool.ProtoGen/$(TARGETBIN)/Goedel.Tool.ProtoGen.dll

all : Protogen/Goedel.Tool.ProtoGen/$(TARGETBIN)/Goedel.Tool.ProtoGen.dll

Protogen/Goedel.Tool.ProtoGen/$(TARGETBIN)/Goedel.Tool.ProtoGen.dll : Protogen/Goedel.Tool.ProtoGen/../../Libraries/$(TARGETBIN)/Goedel.Registry.dll

Protogen/Goedel.Tool.ProtoGen/$(TARGETBIN)/Goedel.Tool.ProtoGen.dll : Protogen/Goedel.Tool.ProtoGen/../../Libraries/$(TARGETBIN)/Goedel.Utilities.dll


Protogen/Goedel.Tool.ProtoGen/$(TARGETBIN)/Goedel.Tool.ProtoGen.dll : always
! echo "" >&2
! echo "*** Directory Protogen/Goedel.Tool.ProtoGen" >&2
! make NORECURSE=true -C Protogen/Goedel.Tool.ProtoGen

# Project : protogen.exe
# Item :  protogen/ShellProtoGen
# Output :     protogen/ShellProtoGen/$(TARGETEXE)/protogen.exe

all : protogen/ShellProtoGen/$(TARGETBIN)/protogen.exe

protogen/ShellProtoGen/$(TARGETBIN)/protogen.exe : protogen/ShellProtoGen/../../Libraries/$(TARGETBIN)/Goedel.Command.dll

protogen/ShellProtoGen/$(TARGETBIN)/protogen.exe : protogen/ShellProtoGen/../../Libraries/$(TARGETBIN)/Goedel.Registry.dll

protogen/ShellProtoGen/$(TARGETBIN)/protogen.exe : protogen/ShellProtoGen/../../Libraries/$(TARGETBIN)/Goedel.Utilities.dll

protogen/ShellProtoGen/$(TARGETBIN)/protogen.exe : protogen/ShellProtoGen/../$(TARGETBIN)/Goedel.Tool.ProtoGen.dll


protogen/ShellProtoGen/$(TARGETBIN)/protogen.exe : always
! echo "" >&2
! echo "*** Directory protogen/ShellProtoGen" >&2
! make NORECURSE=true -C protogen/ShellProtoGen

# Project : Goedel.Tool.Schema.dll
# Item :  Goedel3/Goedel.Tool.Schema
# Output :     Goedel3/Goedel.Tool.Schema/$(TARGETBIN)/Goedel.Tool.Schema.dll

all : Goedel3/Goedel.Tool.Schema/$(TARGETBIN)/Goedel.Tool.Schema.dll

Goedel3/Goedel.Tool.Schema/$(TARGETBIN)/Goedel.Tool.Schema.dll : Goedel3/Goedel.Tool.Schema/../../Libraries/$(TARGETBIN)/Goedel.Registry.dll

Goedel3/Goedel.Tool.Schema/$(TARGETBIN)/Goedel.Tool.Schema.dll : Goedel3/Goedel.Tool.Schema/../../Libraries/$(TARGETBIN)/Goedel.Utilities.dll


Goedel3/Goedel.Tool.Schema/$(TARGETBIN)/Goedel.Tool.Schema.dll : always
! echo "" >&2
! echo "*** Directory Goedel3/Goedel.Tool.Schema" >&2
! make NORECURSE=true -C Goedel3/Goedel.Tool.Schema

# Project : Goedel.Document.Markdown.dll
# Item :  RFCTool/Goedel.Document.Markdown
# Output :     RFCTool/Goedel.Document.Markdown/$(TARGETBIN)/Goedel.Document.Markdown.dll

all : RFCTool/Goedel.Document.Markdown/$(TARGETBIN)/Goedel.Document.Markdown.dll

RFCTool/Goedel.Document.Markdown/$(TARGETBIN)/Goedel.Document.Markdown.dll : RFCTool/Goedel.Document.Markdown/../../Libraries/$(TARGETBIN)/Goedel.FSR.dll

RFCTool/Goedel.Document.Markdown/$(TARGETBIN)/Goedel.Document.Markdown.dll : RFCTool/Goedel.Document.Markdown/../../Libraries/$(TARGETBIN)/Goedel.IO.dll

RFCTool/Goedel.Document.Markdown/$(TARGETBIN)/Goedel.Document.Markdown.dll : RFCTool/Goedel.Document.Markdown/../../Libraries/$(TARGETBIN)/Goedel.Registry.dll

RFCTool/Goedel.Document.Markdown/$(TARGETBIN)/Goedel.Document.Markdown.dll : RFCTool/Goedel.Document.Markdown/../../Libraries/$(TARGETBIN)/Goedel.Utilities.dll


RFCTool/Goedel.Document.Markdown/$(TARGETBIN)/Goedel.Document.Markdown.dll : always
! echo "" >&2
! echo "*** Directory RFCTool/Goedel.Document.Markdown" >&2
! make NORECURSE=true -C RFCTool/Goedel.Document.Markdown

# Project : Goedel.Document.RFC.Convert.dll
# Item :  RFCTool/Goedel.Document.RFC.Convert
# Output :     RFCTool/Goedel.Document.RFC.Convert/$(TARGETBIN)/Goedel.Document.RFC.Convert.dll

all : RFCTool/Goedel.Document.RFC.Convert/$(TARGETBIN)/Goedel.Document.RFC.Convert.dll

RFCTool/Goedel.Document.RFC.Convert/$(TARGETBIN)/Goedel.Document.RFC.Convert.dll : RFCTool/Goedel.Document.RFC.Convert/../../Libraries/$(TARGETBIN)/Goedel.Registry.dll

RFCTool/Goedel.Document.RFC.Convert/$(TARGETBIN)/Goedel.Document.RFC.Convert.dll : RFCTool/Goedel.Document.RFC.Convert/../../Libraries/$(TARGETBIN)/Goedel.Utilities.dll

RFCTool/Goedel.Document.RFC.Convert/$(TARGETBIN)/Goedel.Document.RFC.Convert.dll : RFCTool/Goedel.Document.RFC.Convert/../$(TARGETBIN)/Goedel.Document.Markdown.dll

RFCTool/Goedel.Document.RFC.Convert/$(TARGETBIN)/Goedel.Document.RFC.Convert.dll : RFCTool/Goedel.Document.RFC.Convert/../$(TARGETBIN)/Goedel.Document.RFC.dll


RFCTool/Goedel.Document.RFC.Convert/$(TARGETBIN)/Goedel.Document.RFC.Convert.dll : always
! echo "" >&2
! echo "*** Directory RFCTool/Goedel.Document.RFC.Convert" >&2
! make NORECURSE=true -C RFCTool/Goedel.Document.RFC.Convert

# Project : registryconfig.exe
# Item :  RegistryConfig/ShellRegistryConfig
# Output :     RegistryConfig/ShellRegistryConfig/$(TARGETEXE)/registryconfig.exe

all : RegistryConfig/ShellRegistryConfig/$(TARGETBIN)/registryconfig.exe

RegistryConfig/ShellRegistryConfig/$(TARGETBIN)/registryconfig.exe : RegistryConfig/ShellRegistryConfig/../../Libraries/$(TARGETBIN)/Goedel.Command.dll

RegistryConfig/ShellRegistryConfig/$(TARGETBIN)/registryconfig.exe : RegistryConfig/ShellRegistryConfig/../../Libraries/$(TARGETBIN)/Goedel.Registry.dll

RegistryConfig/ShellRegistryConfig/$(TARGETBIN)/registryconfig.exe : RegistryConfig/ShellRegistryConfig/../../Libraries/$(TARGETBIN)/Goedel.Utilities.dll

RegistryConfig/ShellRegistryConfig/$(TARGETBIN)/registryconfig.exe : RegistryConfig/ShellRegistryConfig/../$(TARGETBIN)/Goedel.Tool.RegistryConfig.dll


RegistryConfig/ShellRegistryConfig/$(TARGETBIN)/registryconfig.exe : always
! echo "" >&2
! echo "*** Directory RegistryConfig/ShellRegistryConfig" >&2
! make NORECURSE=true -C RegistryConfig/ShellRegistryConfig

# Project : Goedel.Tool.RegistryConfig.dll
# Item :  RegistryConfig/Goedel.Tool.RegistryConfig
# Output :     RegistryConfig/Goedel.Tool.RegistryConfig/$(TARGETBIN)/Goedel.Tool.RegistryConfig.dll

all : RegistryConfig/Goedel.Tool.RegistryConfig/$(TARGETBIN)/Goedel.Tool.RegistryConfig.dll

RegistryConfig/Goedel.Tool.RegistryConfig/$(TARGETBIN)/Goedel.Tool.RegistryConfig.dll : RegistryConfig/Goedel.Tool.RegistryConfig/../../Libraries/$(TARGETBIN)/Goedel.Registry.dll

RegistryConfig/Goedel.Tool.RegistryConfig/$(TARGETBIN)/Goedel.Tool.RegistryConfig.dll : RegistryConfig/Goedel.Tool.RegistryConfig/../../Libraries/$(TARGETBIN)/Goedel.Utilities.dll


RegistryConfig/Goedel.Tool.RegistryConfig/$(TARGETBIN)/Goedel.Tool.RegistryConfig.dll : always
! echo "" >&2
! echo "*** Directory RegistryConfig/Goedel.Tool.RegistryConfig" >&2
! make NORECURSE=true -C RegistryConfig/Goedel.Tool.RegistryConfig

# Project : Goedel.Document.RFC.dll
# Item :  RFCTool/Goedel.Tool.RFCTool
# Output :     RFCTool/Goedel.Tool.RFCTool/$(TARGETBIN)/Goedel.Document.RFC.dll

all : RFCTool/Goedel.Tool.RFCTool/$(TARGETBIN)/Goedel.Document.RFC.dll

RFCTool/Goedel.Tool.RFCTool/$(TARGETBIN)/Goedel.Document.RFC.dll : RFCTool/Goedel.Tool.RFCTool/../../Libraries/$(TARGETBIN)/Goedel.FSR.dll

RFCTool/Goedel.Tool.RFCTool/$(TARGETBIN)/Goedel.Document.RFC.dll : RFCTool/Goedel.Tool.RFCTool/../../Libraries/$(TARGETBIN)/Goedel.IO.dll

RFCTool/Goedel.Tool.RFCTool/$(TARGETBIN)/Goedel.Document.RFC.dll : RFCTool/Goedel.Tool.RFCTool/../../Libraries/$(TARGETBIN)/Goedel.Registry.dll

RFCTool/Goedel.Tool.RFCTool/$(TARGETBIN)/Goedel.Document.RFC.dll : RFCTool/Goedel.Tool.RFCTool/../../Libraries/$(TARGETBIN)/Goedel.Utilities.dll

RFCTool/Goedel.Tool.RFCTool/$(TARGETBIN)/Goedel.Document.RFC.dll : RFCTool/Goedel.Tool.RFCTool/../$(TARGETBIN)/Goedel.Document.Markdown.dll


RFCTool/Goedel.Tool.RFCTool/$(TARGETBIN)/Goedel.Document.RFC.dll : always
! echo "" >&2
! echo "*** Directory RFCTool/Goedel.Tool.RFCTool" >&2
! make NORECURSE=true -C RFCTool/Goedel.Tool.RFCTool

# Project : Goedel.Tool.VSIXBuild.dll
# Item :  VSIXBuild/Goedel.Tool.VSIXBuild
# Output :     VSIXBuild/Goedel.Tool.VSIXBuild/$(TARGETBIN)/Goedel.Tool.VSIXBuild.dll

all : VSIXBuild/Goedel.Tool.VSIXBuild/$(TARGETBIN)/Goedel.Tool.VSIXBuild.dll

VSIXBuild/Goedel.Tool.VSIXBuild/$(TARGETBIN)/Goedel.Tool.VSIXBuild.dll : VSIXBuild/Goedel.Tool.VSIXBuild/../../Libraries/$(TARGETBIN)/Goedel.Command.dll

VSIXBuild/Goedel.Tool.VSIXBuild/$(TARGETBIN)/Goedel.Tool.VSIXBuild.dll : VSIXBuild/Goedel.Tool.VSIXBuild/../../Libraries/$(TARGETBIN)/Goedel.Registry.dll

VSIXBuild/Goedel.Tool.VSIXBuild/$(TARGETBIN)/Goedel.Tool.VSIXBuild.dll : VSIXBuild/Goedel.Tool.VSIXBuild/../../Libraries/$(TARGETBIN)/Goedel.Utilities.dll


VSIXBuild/Goedel.Tool.VSIXBuild/$(TARGETBIN)/Goedel.Tool.VSIXBuild.dll : always
! echo "" >&2
! echo "*** Directory VSIXBuild/Goedel.Tool.VSIXBuild" >&2
! make NORECURSE=true -C VSIXBuild/Goedel.Tool.VSIXBuild

# Project : rfctool.exe
# Item :  RFCTool/ShellRFCTool
# Output :     RFCTool/ShellRFCTool/$(TARGETEXE)/rfctool.exe

all : RFCTool/ShellRFCTool/$(TARGETBIN)/rfctool.exe

RFCTool/ShellRFCTool/$(TARGETBIN)/rfctool.exe : RFCTool/ShellRFCTool/../../Libraries/$(TARGETBIN)/Goedel.Command.dll

RFCTool/ShellRFCTool/$(TARGETBIN)/rfctool.exe : RFCTool/ShellRFCTool/../../Libraries/$(TARGETBIN)/Goedel.FSR.dll

RFCTool/ShellRFCTool/$(TARGETBIN)/rfctool.exe : RFCTool/ShellRFCTool/../../Libraries/$(TARGETBIN)/Goedel.IO.dll

RFCTool/ShellRFCTool/$(TARGETBIN)/rfctool.exe : RFCTool/ShellRFCTool/../../Libraries/$(TARGETBIN)/Goedel.Registry.dll

RFCTool/ShellRFCTool/$(TARGETBIN)/rfctool.exe : RFCTool/ShellRFCTool/../../Libraries/$(TARGETBIN)/Goedel.Utilities.dll

RFCTool/ShellRFCTool/$(TARGETBIN)/rfctool.exe : RFCTool/ShellRFCTool/../$(TARGETBIN)/Goedel.Document.Markdown.dll

RFCTool/ShellRFCTool/$(TARGETBIN)/rfctool.exe : RFCTool/ShellRFCTool/../$(TARGETBIN)/Goedel.Document.RFC.Convert.dll

RFCTool/ShellRFCTool/$(TARGETBIN)/rfctool.exe : RFCTool/ShellRFCTool/../$(TARGETBIN)/Goedel.Document.OpenXML.dll

RFCTool/ShellRFCTool/$(TARGETBIN)/rfctool.exe : RFCTool/ShellRFCTool/../$(TARGETBIN)/Goedel.Document.RFC.dll


RFCTool/ShellRFCTool/$(TARGETBIN)/rfctool.exe : always
! echo "" >&2
! echo "*** Directory RFCTool/ShellRFCTool" >&2
! make NORECURSE=true -C RFCTool/ShellRFCTool

# Project : Goedel.Document.OpenXML.dll
# Item :  RFCTool/Goedel.Document.OpenXML
# Output :     RFCTool/Goedel.Document.OpenXML/$(TARGETBIN)/Goedel.Document.OpenXML.dll

all : RFCTool/Goedel.Document.OpenXML/$(TARGETBIN)/Goedel.Document.OpenXML.dll

RFCTool/Goedel.Document.OpenXML/$(TARGETBIN)/Goedel.Document.OpenXML.dll : RFCTool/Goedel.Document.OpenXML/../../Libraries/$(TARGETBIN)/Goedel.FSR.dll

RFCTool/Goedel.Document.OpenXML/$(TARGETBIN)/Goedel.Document.OpenXML.dll : RFCTool/Goedel.Document.OpenXML/../../Libraries/$(TARGETBIN)/Goedel.Registry.dll

RFCTool/Goedel.Document.OpenXML/$(TARGETBIN)/Goedel.Document.OpenXML.dll : RFCTool/Goedel.Document.OpenXML/../../Libraries/$(TARGETBIN)/Goedel.Utilities.dll

RFCTool/Goedel.Document.OpenXML/$(TARGETBIN)/Goedel.Document.OpenXML.dll : RFCTool/Goedel.Document.OpenXML/../$(TARGETBIN)/Goedel.Document.Markdown.dll

RFCTool/Goedel.Document.OpenXML/$(TARGETBIN)/Goedel.Document.OpenXML.dll : RFCTool/Goedel.Document.OpenXML/../$(TARGETBIN)/Goedel.Document.RFC.dll


RFCTool/Goedel.Document.OpenXML/$(TARGETBIN)/Goedel.Document.OpenXML.dll : always
! echo "" >&2
! echo "*** Directory RFCTool/Goedel.Document.OpenXML" >&2
! make NORECURSE=true -C RFCTool/Goedel.Document.OpenXML

# Project : libTrojanScript.dll
# Item :  Trojan3/Goedel.Trojan.Script
# Output :     Trojan3/Goedel.Trojan.Script/$(TARGETBIN)/libTrojanScript.dll

all : Trojan3/Goedel.Trojan.Script/$(TARGETBIN)/libTrojanScript.dll

Trojan3/Goedel.Trojan.Script/$(TARGETBIN)/libTrojanScript.dll : Trojan3/Goedel.Trojan.Script/../../Libraries/$(TARGETBIN)/Goedel.Registry.dll

Trojan3/Goedel.Trojan.Script/$(TARGETBIN)/libTrojanScript.dll : Trojan3/Goedel.Trojan.Script/../../Libraries/$(TARGETBIN)/Goedel.Utilities.dll


Trojan3/Goedel.Trojan.Script/$(TARGETBIN)/libTrojanScript.dll : always
! echo "" >&2
! echo "*** Directory Trojan3/Goedel.Trojan.Script" >&2
! make NORECURSE=true -C Trojan3/Goedel.Trojan.Script

# Project : trojan.exe
# Item :  Trojan3/Trojan3
# Output :     Trojan3/Trojan3/$(TARGETEXE)/trojan.exe

all : Trojan3/Trojan3/$(TARGETBIN)/trojan.exe

Trojan3/Trojan3/$(TARGETBIN)/trojan.exe : Trojan3/Trojan3/../../Libraries/$(TARGETBIN)/Goedel.Command.dll

Trojan3/Trojan3/$(TARGETBIN)/trojan.exe : Trojan3/Trojan3/../../Libraries/$(TARGETBIN)/Goedel.Registry.dll

Trojan3/Trojan3/$(TARGETBIN)/trojan.exe : Trojan3/Trojan3/../../Libraries/$(TARGETBIN)/Goedel.Utilities.dll

Trojan3/Trojan3/$(TARGETBIN)/trojan.exe : Trojan3/Trojan3/../../RegistryConfig/$(TARGETBIN)/Goedel.Tool.RegistryConfig.dll

Trojan3/Trojan3/$(TARGETBIN)/trojan.exe : Trojan3/Trojan3/../$(TARGETBIN)/libTrojanScript.dll


Trojan3/Trojan3/$(TARGETBIN)/trojan.exe : always
! echo "" >&2
! echo "*** Directory Trojan3/Trojan3" >&2
! make NORECURSE=true -C Trojan3/Trojan3

# Project : vsixbuild.exe
# Item :  VSIXBuild/ShellVSIXBuild
# Output :     VSIXBuild/ShellVSIXBuild/$(TARGETEXE)/vsixbuild.exe

all : VSIXBuild/ShellVSIXBuild/$(TARGETBIN)/vsixbuild.exe

VSIXBuild/ShellVSIXBuild/$(TARGETBIN)/vsixbuild.exe : VSIXBuild/ShellVSIXBuild/../../Libraries/$(TARGETBIN)/Goedel.Command.dll

VSIXBuild/ShellVSIXBuild/$(TARGETBIN)/vsixbuild.exe : VSIXBuild/ShellVSIXBuild/../../Libraries/$(TARGETBIN)/Goedel.Registry.dll

VSIXBuild/ShellVSIXBuild/$(TARGETBIN)/vsixbuild.exe : VSIXBuild/ShellVSIXBuild/../../Libraries/$(TARGETBIN)/Goedel.Utilities.dll

VSIXBuild/ShellVSIXBuild/$(TARGETBIN)/vsixbuild.exe : VSIXBuild/ShellVSIXBuild/../$(TARGETBIN)/Goedel.Tool.VSIXBuild.dll


VSIXBuild/ShellVSIXBuild/$(TARGETBIN)/vsixbuild.exe : always
! echo "" >&2
! echo "*** Directory VSIXBuild/ShellVSIXBuild" >&2
! make NORECURSE=true -C VSIXBuild/ShellVSIXBuild

# Project : Goedel.Tool.Makey.dll
# Item :  MakeyMakey/Goedel.Tools.Makey
# Output :     MakeyMakey/Goedel.Tools.Makey/$(TARGETBIN)/Goedel.Tool.Makey.dll

all : MakeyMakey/Goedel.Tools.Makey/$(TARGETBIN)/Goedel.Tool.Makey.dll

MakeyMakey/Goedel.Tools.Makey/$(TARGETBIN)/Goedel.Tool.Makey.dll : MakeyMakey/Goedel.Tools.Makey/../../Libraries/$(TARGETBIN)/Goedel.FSR.dll

MakeyMakey/Goedel.Tools.Makey/$(TARGETBIN)/Goedel.Tool.Makey.dll : MakeyMakey/Goedel.Tools.Makey/../../Libraries/$(TARGETBIN)/Goedel.Registry.dll

MakeyMakey/Goedel.Tools.Makey/$(TARGETBIN)/Goedel.Tool.Makey.dll : MakeyMakey/Goedel.Tools.Makey/../../Libraries/$(TARGETBIN)/Goedel.Utilities.dll


MakeyMakey/Goedel.Tools.Makey/$(TARGETBIN)/Goedel.Tool.Makey.dll : always
! echo "" >&2
! echo "*** Directory MakeyMakey/Goedel.Tools.Makey" >&2
! make NORECURSE=true -C MakeyMakey/Goedel.Tools.Makey

# Project : Makey.exe
# Item :  MakeyMakey/ShellMakey
# Output :     MakeyMakey/ShellMakey/$(TARGETEXE)/Makey.exe

all : MakeyMakey/ShellMakey/$(TARGETBIN)/Makey.exe

MakeyMakey/ShellMakey/$(TARGETBIN)/Makey.exe : MakeyMakey/ShellMakey/../../Libraries/$(TARGETBIN)/Goedel.Command.dll

MakeyMakey/ShellMakey/$(TARGETBIN)/Makey.exe : MakeyMakey/ShellMakey/../../Libraries/$(TARGETBIN)/Goedel.Registry.dll

MakeyMakey/ShellMakey/$(TARGETBIN)/Makey.exe : MakeyMakey/ShellMakey/../../Libraries/$(TARGETBIN)/Goedel.Utilities.dll

MakeyMakey/ShellMakey/$(TARGETBIN)/Makey.exe : MakeyMakey/ShellMakey/../$(TARGETBIN)/Goedel.Tool.Makey.dll


MakeyMakey/ShellMakey/$(TARGETBIN)/Makey.exe : always
! echo "" >&2
! echo "*** Directory MakeyMakey/ShellMakey" >&2
! make NORECURSE=true -C MakeyMakey/ShellMakey

# Project : commandparse.exe
# Item :  Command/ShellCommand
# Output :     Command/ShellCommand/$(TARGETEXE)/commandparse.exe

all : Command/ShellCommand/$(TARGETBIN)/commandparse.exe

Command/ShellCommand/$(TARGETBIN)/commandparse.exe : Command/ShellCommand/../../Libraries/$(TARGETBIN)/Goedel.Command.dll

Command/ShellCommand/$(TARGETBIN)/commandparse.exe : Command/ShellCommand/../../Libraries/$(TARGETBIN)/Goedel.Registry.dll

Command/ShellCommand/$(TARGETBIN)/commandparse.exe : Command/ShellCommand/../../Libraries/$(TARGETBIN)/Goedel.Utilities.dll

Command/ShellCommand/$(TARGETBIN)/commandparse.exe : Command/ShellCommand/../$(TARGETBIN)/Goedel.Tool.Command.dll


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

# Project : Goedel.Registry.dll
# Item :  Libraries/Goedel.Registry
# Output :     Libraries/Goedel.Registry/$(TARGETBIN)/Goedel.Registry.dll

all : Libraries/Goedel.Registry/$(TARGETBIN)/Goedel.Registry.dll

Libraries/Goedel.Registry/$(TARGETBIN)/Goedel.Registry.dll : Libraries/Goedel.Registry/../$(TARGETBIN)/Goedel.FSR.dll

Libraries/Goedel.Registry/$(TARGETBIN)/Goedel.Registry.dll : Libraries/Goedel.Registry/../$(TARGETBIN)/Goedel.Utilities.dll


Libraries/Goedel.Registry/$(TARGETBIN)/Goedel.Registry.dll : always
! echo "" >&2
! echo "*** Directory Libraries/Goedel.Registry" >&2
! make NORECURSE=true -C Libraries/Goedel.Registry

# Project : Goedel.IO.dll
# Item :  Libraries/Goedel.IO
# Output :     Libraries/Goedel.IO/$(TARGETBIN)/Goedel.IO.dll

all : Libraries/Goedel.IO/$(TARGETBIN)/Goedel.IO.dll


Libraries/Goedel.IO/$(TARGETBIN)/Goedel.IO.dll : always
! echo "" >&2
! echo "*** Directory Libraries/Goedel.IO" >&2
! make NORECURSE=true -C Libraries/Goedel.IO

# Project : bootmakermini.exe
# Item :  RFCTool/ShellBootMaker
# Output :     RFCTool/ShellBootMaker/$(TARGETEXE)/bootmakermini.exe

all : RFCTool/ShellBootMaker/$(TARGETBIN)/bootmakermini.exe

RFCTool/ShellBootMaker/$(TARGETBIN)/bootmakermini.exe : RFCTool/ShellBootMaker/../../Libraries/$(TARGETBIN)/Goedel.Command.dll

RFCTool/ShellBootMaker/$(TARGETBIN)/bootmakermini.exe : RFCTool/ShellBootMaker/../../Libraries/$(TARGETBIN)/Goedel.FSR.dll

RFCTool/ShellBootMaker/$(TARGETBIN)/bootmakermini.exe : RFCTool/ShellBootMaker/../../Libraries/$(TARGETBIN)/Goedel.IO.dll

RFCTool/ShellBootMaker/$(TARGETBIN)/bootmakermini.exe : RFCTool/ShellBootMaker/../../Libraries/$(TARGETBIN)/Goedel.Registry.dll

RFCTool/ShellBootMaker/$(TARGETBIN)/bootmakermini.exe : RFCTool/ShellBootMaker/../../Libraries/$(TARGETBIN)/Goedel.Utilities.dll

RFCTool/ShellBootMaker/$(TARGETBIN)/bootmakermini.exe : RFCTool/ShellBootMaker/../$(TARGETBIN)/Goedel.Document.Markdown.dll

RFCTool/ShellBootMaker/$(TARGETBIN)/bootmakermini.exe : RFCTool/ShellBootMaker/../$(TARGETBIN)/Goedel.Document.OpenXML.dll


RFCTool/ShellBootMaker/$(TARGETBIN)/bootmakermini.exe : always
! echo "" >&2
! echo "*** Directory RFCTool/ShellBootMaker" >&2
! make NORECURSE=true -C RFCTool/ShellBootMaker

# Project : OfficeLib.dll
# Item :  RFCTool/Goedel.Document.Office
# Output :     RFCTool/Goedel.Document.Office/$(TARGETBIN)/OfficeLib.dll

all : RFCTool/Goedel.Document.Office/$(TARGETBIN)/OfficeLib.dll


RFCTool/Goedel.Document.Office/$(TARGETBIN)/OfficeLib.dll : always
! echo "" >&2
! echo "*** Directory RFCTool/Goedel.Document.Office" >&2
! make NORECURSE=true -C RFCTool/Goedel.Document.Office

# Project : bootmaker.exe
# Item :  RFCTool/ShellBootMakerOffice
# Output :     RFCTool/ShellBootMakerOffice/$(TARGETEXE)/bootmaker.exe

all : RFCTool/ShellBootMakerOffice/$(TARGETBIN)/bootmaker.exe

RFCTool/ShellBootMakerOffice/$(TARGETBIN)/bootmaker.exe : RFCTool/ShellBootMakerOffice/../../Libraries/$(TARGETBIN)/Goedel.Command.dll

RFCTool/ShellBootMakerOffice/$(TARGETBIN)/bootmaker.exe : RFCTool/ShellBootMakerOffice/../../Libraries/$(TARGETBIN)/Goedel.FSR.dll

RFCTool/ShellBootMakerOffice/$(TARGETBIN)/bootmaker.exe : RFCTool/ShellBootMakerOffice/../../Libraries/$(TARGETBIN)/Goedel.IO.dll

RFCTool/ShellBootMakerOffice/$(TARGETBIN)/bootmaker.exe : RFCTool/ShellBootMakerOffice/../../Libraries/$(TARGETBIN)/Goedel.Registry.dll

RFCTool/ShellBootMakerOffice/$(TARGETBIN)/bootmaker.exe : RFCTool/ShellBootMakerOffice/../../Libraries/$(TARGETBIN)/Goedel.Utilities.dll

RFCTool/ShellBootMakerOffice/$(TARGETBIN)/bootmaker.exe : RFCTool/ShellBootMakerOffice/../$(TARGETBIN)/Goedel.Document.Markdown.dll

RFCTool/ShellBootMakerOffice/$(TARGETBIN)/bootmaker.exe : RFCTool/ShellBootMakerOffice/../$(TARGETBIN)/OfficeLib.dll

RFCTool/ShellBootMakerOffice/$(TARGETBIN)/bootmaker.exe : RFCTool/ShellBootMakerOffice/../$(TARGETBIN)/Goedel.Document.OpenXML.dll


RFCTool/ShellBootMakerOffice/$(TARGETBIN)/bootmaker.exe : always
! echo "" >&2
! echo "*** Directory RFCTool/ShellBootMakerOffice" >&2
! make NORECURSE=true -C RFCTool/ShellBootMakerOffice

# Project : TestCommandParse.exe
# Item :  Command/TestCommandParse
# Output :     Command/TestCommandParse/$(TARGETEXE)/TestCommandParse.exe

all : Command/TestCommandParse/$(TARGETBIN)/TestCommandParse.exe

Command/TestCommandParse/$(TARGETBIN)/TestCommandParse.exe : Command/TestCommandParse/../../Libraries/$(TARGETBIN)/Goedel.Command.dll

Command/TestCommandParse/$(TARGETBIN)/TestCommandParse.exe : Command/TestCommandParse/../../Libraries/$(TARGETBIN)/Goedel.Registry.dll

Command/TestCommandParse/$(TARGETBIN)/TestCommandParse.exe : Command/TestCommandParse/../../Libraries/$(TARGETBIN)/Goedel.Utilities.dll

Command/TestCommandParse/$(TARGETBIN)/TestCommandParse.exe : Command/TestCommandParse/../$(TARGETBIN)/Goedel.Tool.Command.dll


Command/TestCommandParse/$(TARGETBIN)/TestCommandParse.exe : always
! echo "" >&2
! echo "*** Directory Command/TestCommandParse" >&2
! make NORECURSE=true -C Command/TestCommandParse

# Project : Goedel.Command.dll
# Item :  Libraries/Goedel.Command
# Output :     Libraries/Goedel.Command/$(TARGETBIN)/Goedel.Command.dll

all : Libraries/Goedel.Command/$(TARGETBIN)/Goedel.Command.dll

Libraries/Goedel.Command/$(TARGETBIN)/Goedel.Command.dll : Libraries/Goedel.Command/../$(TARGETBIN)/Goedel.FSR.dll

Libraries/Goedel.Command/$(TARGETBIN)/Goedel.Command.dll : Libraries/Goedel.Command/../$(TARGETBIN)/Goedel.Registry.dll

Libraries/Goedel.Command/$(TARGETBIN)/Goedel.Command.dll : Libraries/Goedel.Command/../$(TARGETBIN)/Goedel.Utilities.dll


Libraries/Goedel.Command/$(TARGETBIN)/Goedel.Command.dll : always
! echo "" >&2
! echo "*** Directory Libraries/Goedel.Command" >&2
! make NORECURSE=true -C Libraries/Goedel.Command

# Project : Goedel.Document.Test.dll
# Item :  RFCTool/Goedel.Document.Test
# Output :     RFCTool/Goedel.Document.Test/$(TARGETBIN)/Goedel.Document.Test.dll

all : RFCTool/Goedel.Document.Test/$(TARGETBIN)/Goedel.Document.Test.dll

RFCTool/Goedel.Document.Test/$(TARGETBIN)/Goedel.Document.Test.dll : RFCTool/Goedel.Document.Test/../../Libraries/$(TARGETBIN)/Goedel.FSR.dll

RFCTool/Goedel.Document.Test/$(TARGETBIN)/Goedel.Document.Test.dll : RFCTool/Goedel.Document.Test/../../Libraries/$(TARGETBIN)/Goedel.IO.dll

RFCTool/Goedel.Document.Test/$(TARGETBIN)/Goedel.Document.Test.dll : RFCTool/Goedel.Document.Test/../../Libraries/$(TARGETBIN)/Goedel.Registry.dll

RFCTool/Goedel.Document.Test/$(TARGETBIN)/Goedel.Document.Test.dll : RFCTool/Goedel.Document.Test/../../Libraries/$(TARGETBIN)/Goedel.Utilities.dll

RFCTool/Goedel.Document.Test/$(TARGETBIN)/Goedel.Document.Test.dll : RFCTool/Goedel.Document.Test/../$(TARGETBIN)/Goedel.Document.Markdown.dll

RFCTool/Goedel.Document.Test/$(TARGETBIN)/Goedel.Document.Test.dll : RFCTool/Goedel.Document.Test/../$(TARGETBIN)/Goedel.Document.OpenXML.dll

RFCTool/Goedel.Document.Test/$(TARGETBIN)/Goedel.Document.Test.dll : RFCTool/Goedel.Document.Test/../$(TARGETBIN)/Goedel.Document.RFC.dll


RFCTool/Goedel.Document.Test/$(TARGETBIN)/Goedel.Document.Test.dll : always
! echo "" >&2
! echo "*** Directory RFCTool/Goedel.Document.Test" >&2
! make NORECURSE=true -C RFCTool/Goedel.Document.Test

# Project : RunDocs.exe
# Item :  RunDocs
# Output :     RunDocs/$(TARGETEXE)/RunDocs.exe

all : RunDocs/$(TARGETBIN)/RunDocs.exe

RunDocs/$(TARGETBIN)/RunDocs.exe : RunDocs/../Libraries/$(TARGETBIN)/Goedel.FSR.dll

RunDocs/$(TARGETBIN)/RunDocs.exe : RunDocs/../Libraries/$(TARGETBIN)/Goedel.IO.dll

RunDocs/$(TARGETBIN)/RunDocs.exe : RunDocs/../Libraries/$(TARGETBIN)/Goedel.Utilities.dll

RunDocs/$(TARGETBIN)/RunDocs.exe : RunDocs/../RFCTool/$(TARGETBIN)/Goedel.Document.Markdown.dll

RunDocs/$(TARGETBIN)/RunDocs.exe : RunDocs/../RFCTool/$(TARGETBIN)/OfficeLib.dll

RunDocs/$(TARGETBIN)/RunDocs.exe : RunDocs/../RFCTool/$(TARGETBIN)/Goedel.Document.OpenXML.dll

RunDocs/$(TARGETBIN)/RunDocs.exe : RunDocs/../RFCTool/$(TARGETBIN)/Goedel.Document.RFC.Convert.dll

RunDocs/$(TARGETBIN)/RunDocs.exe : RunDocs/../RFCTool/$(TARGETBIN)/Goedel.Document.Test.dll

RunDocs/$(TARGETBIN)/RunDocs.exe : RunDocs/../RFCTool/$(TARGETBIN)/Goedel.Document.RFC.dll


RunDocs/$(TARGETBIN)/RunDocs.exe : always
! echo "" >&2
! echo "*** Directory RunDocs" >&2
! make NORECURSE=true -C RunDocs

# Project : Goedel.Trace.dll
# Item :  Trace/Goedel.Trace
# Output :     Trace/Goedel.Trace/$(TARGETBIN)/Goedel.Trace.dll

all : Trace/Goedel.Trace/$(TARGETBIN)/Goedel.Trace.dll

Trace/Goedel.Trace/$(TARGETBIN)/Goedel.Trace.dll : Trace/Goedel.Trace/../../../buildtools/Libraries/$(TARGETBIN)/Goedel.Protocol.dll

Trace/Goedel.Trace/$(TARGETBIN)/Goedel.Trace.dll : Trace/Goedel.Trace/../../../buildtools/Libraries/$(TARGETBIN)/Goedel.Utilities.dll

Trace/Goedel.Trace/$(TARGETBIN)/Goedel.Trace.dll : Trace/Goedel.Trace/../../Libraries/$(TARGETBIN)/Goedel.Protocol.Framework.dll


Trace/Goedel.Trace/$(TARGETBIN)/Goedel.Trace.dll : always
! echo "" >&2
! echo "*** Directory Trace/Goedel.Trace" >&2
! make NORECURSE=true -C Trace/Goedel.Trace

# Project : Goedel.Trace.Client.dll
# Item :  Trace/Goedel.Trace.Client
# Output :     Trace/Goedel.Trace.Client/$(TARGETBIN)/Goedel.Trace.Client.dll

all : Trace/Goedel.Trace.Client/$(TARGETBIN)/Goedel.Trace.Client.dll

Trace/Goedel.Trace.Client/$(TARGETBIN)/Goedel.Trace.Client.dll : Trace/Goedel.Trace.Client/../../../buildtools/Libraries/$(TARGETBIN)/Goedel.Protocol.dll

Trace/Goedel.Trace.Client/$(TARGETBIN)/Goedel.Trace.Client.dll : Trace/Goedel.Trace.Client/../../../buildtools/Libraries/$(TARGETBIN)/Goedel.Utilities.dll

Trace/Goedel.Trace.Client/$(TARGETBIN)/Goedel.Trace.Client.dll : Trace/Goedel.Trace.Client/../../Libraries/$(TARGETBIN)/.dll

Trace/Goedel.Trace.Client/$(TARGETBIN)/Goedel.Trace.Client.dll : Trace/Goedel.Trace.Client/../$(TARGETBIN)/Goedel.Trace.dll


Trace/Goedel.Trace.Client/$(TARGETBIN)/Goedel.Trace.Client.dll : always
! echo "" >&2
! echo "*** Directory Trace/Goedel.Trace.Client" >&2
! make NORECURSE=true -C Trace/Goedel.Trace.Client

# Project : Goedel.Trace.Documentation.exe
# Item :  Trace/Goedel.Trace.Documentation
# Output :     Trace/Goedel.Trace.Documentation/$(TARGETEXE)/Goedel.Trace.Documentation.exe

all : Trace/Goedel.Trace.Documentation/$(TARGETBIN)/Goedel.Trace.Documentation.exe

Trace/Goedel.Trace.Documentation/$(TARGETBIN)/Goedel.Trace.Documentation.exe : Trace/Goedel.Trace.Documentation/../../../buildtools/Libraries/$(TARGETBIN)/Goedel.Command.dll

Trace/Goedel.Trace.Documentation/$(TARGETBIN)/Goedel.Trace.Documentation.exe : Trace/Goedel.Trace.Documentation/../../../buildtools/Libraries/$(TARGETBIN)/Goedel.IO.dll

Trace/Goedel.Trace.Documentation/$(TARGETBIN)/Goedel.Trace.Documentation.exe : Trace/Goedel.Trace.Documentation/../../../buildtools/Libraries/$(TARGETBIN)/Goedel.Protocol.Debug.dll

Trace/Goedel.Trace.Documentation/$(TARGETBIN)/Goedel.Trace.Documentation.exe : Trace/Goedel.Trace.Documentation/../../../buildtools/Libraries/$(TARGETBIN)/Goedel.Protocol.dll

Trace/Goedel.Trace.Documentation/$(TARGETBIN)/Goedel.Trace.Documentation.exe : Trace/Goedel.Trace.Documentation/../../../buildtools/Libraries/$(TARGETBIN)/Goedel.Registry.dll

Trace/Goedel.Trace.Documentation/$(TARGETBIN)/Goedel.Trace.Documentation.exe : Trace/Goedel.Trace.Documentation/../../../buildtools/Libraries/$(TARGETBIN)/Goedel.Utilities.dll

Trace/Goedel.Trace.Documentation/$(TARGETBIN)/Goedel.Trace.Documentation.exe : Trace/Goedel.Trace.Documentation/../$(TARGETBIN)/Goedel.Trace.Client.dll

Trace/Goedel.Trace.Documentation/$(TARGETBIN)/Goedel.Trace.Documentation.exe : Trace/Goedel.Trace.Documentation/../$(TARGETBIN)/Goedel.Trace.Server.dll

Trace/Goedel.Trace.Documentation/$(TARGETBIN)/Goedel.Trace.Documentation.exe : Trace/Goedel.Trace.Documentation/../$(TARGETBIN)/Goedel.Trace.dll


Trace/Goedel.Trace.Documentation/$(TARGETBIN)/Goedel.Trace.Documentation.exe : always
! echo "" >&2
! echo "*** Directory Trace/Goedel.Trace.Documentation" >&2
! make NORECURSE=true -C Trace/Goedel.Trace.Documentation

# Project : Goedel.Trace.Server.dll
# Item :  Trace/Goedel.Trace.Server
# Output :     Trace/Goedel.Trace.Server/$(TARGETBIN)/Goedel.Trace.Server.dll

all : Trace/Goedel.Trace.Server/$(TARGETBIN)/Goedel.Trace.Server.dll

Trace/Goedel.Trace.Server/$(TARGETBIN)/Goedel.Trace.Server.dll : Trace/Goedel.Trace.Server/../../../buildtools/Libraries/$(TARGETBIN)/Goedel.IO.dll

Trace/Goedel.Trace.Server/$(TARGETBIN)/Goedel.Trace.Server.dll : Trace/Goedel.Trace.Server/../../../buildtools/Libraries/$(TARGETBIN)/Goedel.Protocol.Framework.dll

Trace/Goedel.Trace.Server/$(TARGETBIN)/Goedel.Trace.Server.dll : Trace/Goedel.Trace.Server/../../../buildtools/Libraries/$(TARGETBIN)/Goedel.Protocol.dll

Trace/Goedel.Trace.Server/$(TARGETBIN)/Goedel.Trace.Server.dll : Trace/Goedel.Trace.Server/../../../buildtools/Libraries/$(TARGETBIN)/Goedel.Utilities.dll

Trace/Goedel.Trace.Server/$(TARGETBIN)/Goedel.Trace.Server.dll : Trace/Goedel.Trace.Server/../$(TARGETBIN)/Goedel.Trace.dll


Trace/Goedel.Trace.Server/$(TARGETBIN)/Goedel.Trace.Server.dll : always
! echo "" >&2
! echo "*** Directory Trace/Goedel.Trace.Server" >&2
! make NORECURSE=true -C Trace/Goedel.Trace.Server

# Project : TraceServer.exe
# Item :  Trace/Goedel.Trace.Shell.Server
# Output :     Trace/Goedel.Trace.Shell.Server/$(TARGETEXE)/TraceServer.exe

all : Trace/Goedel.Trace.Shell.Server/$(TARGETBIN)/TraceServer.exe

Trace/Goedel.Trace.Shell.Server/$(TARGETBIN)/TraceServer.exe : Trace/Goedel.Trace.Shell.Server/../../../buildtools/Libraries/$(TARGETBIN)/Goedel.Command.dll

Trace/Goedel.Trace.Shell.Server/$(TARGETBIN)/TraceServer.exe : Trace/Goedel.Trace.Shell.Server/../../../buildtools/Libraries/$(TARGETBIN)/Goedel.Protocol.Framework.dll

Trace/Goedel.Trace.Shell.Server/$(TARGETBIN)/TraceServer.exe : Trace/Goedel.Trace.Shell.Server/../../../buildtools/Libraries/$(TARGETBIN)/Goedel.Registry.dll

Trace/Goedel.Trace.Shell.Server/$(TARGETBIN)/TraceServer.exe : Trace/Goedel.Trace.Shell.Server/../../../buildtools/Libraries/$(TARGETBIN)/Goedel.Utilities.dll

Trace/Goedel.Trace.Shell.Server/$(TARGETBIN)/TraceServer.exe : Trace/Goedel.Trace.Shell.Server/../../Libraries/$(TARGETBIN)/Goedel.Protocol.dll

Trace/Goedel.Trace.Shell.Server/$(TARGETBIN)/TraceServer.exe : Trace/Goedel.Trace.Shell.Server/../$(TARGETBIN)/Goedel.Trace.Server.dll

Trace/Goedel.Trace.Shell.Server/$(TARGETBIN)/TraceServer.exe : Trace/Goedel.Trace.Shell.Server/../$(TARGETBIN)/Goedel.Trace.dll


Trace/Goedel.Trace.Shell.Server/$(TARGETBIN)/TraceServer.exe : always
! echo "" >&2
! echo "*** Directory Trace/Goedel.Trace.Shell.Server" >&2
! make NORECURSE=true -C Trace/Goedel.Trace.Shell.Server

# Project : Goedel.Protocol.Framework.dll
# Item :  Libraries/Goedel.Protocol.Framework
# Output :     Libraries/Goedel.Protocol.Framework/$(TARGETBIN)/Goedel.Protocol.Framework.dll

all : Libraries/Goedel.Protocol.Framework/$(TARGETBIN)/Goedel.Protocol.Framework.dll

Libraries/Goedel.Protocol.Framework/$(TARGETBIN)/Goedel.Protocol.Framework.dll : Libraries/Goedel.Protocol.Framework/../$(TARGETBIN)/Goedel.Platform.dll

Libraries/Goedel.Protocol.Framework/$(TARGETBIN)/Goedel.Protocol.Framework.dll : Libraries/Goedel.Protocol.Framework/../$(TARGETBIN)/Goedel.Utilities.dll

Libraries/Goedel.Protocol.Framework/$(TARGETBIN)/Goedel.Protocol.Framework.dll : Libraries/Goedel.Protocol.Framework/../$(TARGETBIN)/Goedel.Protocol.dll


Libraries/Goedel.Protocol.Framework/$(TARGETBIN)/Goedel.Protocol.Framework.dll : always
! echo "" >&2
! echo "*** Directory Libraries/Goedel.Protocol.Framework" >&2
! make NORECURSE=true -C Libraries/Goedel.Protocol.Framework

# Project : Goedel.Protocol.Debug.dll
# Item :  Libraries/Goedel.Protocol.Debug
# Output :     Libraries/Goedel.Protocol.Debug/$(TARGETBIN)/Goedel.Protocol.Debug.dll

all : Libraries/Goedel.Protocol.Debug/$(TARGETBIN)/Goedel.Protocol.Debug.dll

Libraries/Goedel.Protocol.Debug/$(TARGETBIN)/Goedel.Protocol.Debug.dll : Libraries/Goedel.Protocol.Debug/../$(TARGETBIN)/Goedel.Utilities.dll

Libraries/Goedel.Protocol.Debug/$(TARGETBIN)/Goedel.Protocol.Debug.dll : Libraries/Goedel.Protocol.Debug/../$(TARGETBIN)/Goedel.Protocol.dll

Libraries/Goedel.Protocol.Debug/$(TARGETBIN)/Goedel.Protocol.Debug.dll : Libraries/Goedel.Protocol.Debug/../$(TARGETBIN)/Goedel.Protocol.Framework.dll


Libraries/Goedel.Protocol.Debug/$(TARGETBIN)/Goedel.Protocol.Debug.dll : always
! echo "" >&2
! echo "*** Directory Libraries/Goedel.Protocol.Debug" >&2
! make NORECURSE=true -C Libraries/Goedel.Protocol.Debug

# Project : .dll
# Item :  Libraries/Goedel.Debug
# Output :     Libraries/Goedel.Debug/$(TARGETBIN)/.dll

all : Libraries/Goedel.Debug/$(TARGETBIN)/.dll


Libraries/Goedel.Debug/$(TARGETBIN)/.dll : always
! echo "" >&2
! echo "*** Directory Libraries/Goedel.Debug" >&2
! make NORECURSE=true -C Libraries/Goedel.Debug



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
! make clean NORECURSE=true -C Libraries/Goedel.Registry
! make clean NORECURSE=true -C Libraries/Goedel.IO
! make clean NORECURSE=true -C RFCTool/ShellBootMaker
! make clean NORECURSE=true -C RFCTool/Goedel.Document.Office
! make clean NORECURSE=true -C RFCTool/ShellBootMakerOffice
! make clean NORECURSE=true -C Command/TestCommandParse
! make clean NORECURSE=true -C Libraries/Goedel.Command
! make clean NORECURSE=true -C RFCTool/Goedel.Document.Test
! make clean NORECURSE=true -C RunDocs
! make clean NORECURSE=true -C Trace/Goedel.Trace
! make clean NORECURSE=true -C Trace/Goedel.Trace.Client
! make clean NORECURSE=true -C Trace/Goedel.Trace.Documentation
! make clean NORECURSE=true -C Trace/Goedel.Trace.Server
! make clean NORECURSE=true -C Trace/Goedel.Trace.Shell.Server
! make clean NORECURSE=true -C Libraries/Goedel.Protocol.Framework
! make clean NORECURSE=true -C Libraries/Goedel.Protocol.Debug
! make clean NORECURSE=true -C Libraries/Goedel.Debug

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
! make publish NORECURSE=true -C Libraries/Goedel.Registry
! make publish NORECURSE=true -C Libraries/Goedel.IO
! make publish NORECURSE=true -C RFCTool/ShellBootMaker
! make publish NORECURSE=true -C RFCTool/Goedel.Document.Office
! make publish NORECURSE=true -C RFCTool/ShellBootMakerOffice
! make publish NORECURSE=true -C Command/TestCommandParse
! make publish NORECURSE=true -C Libraries/Goedel.Command
! make publish NORECURSE=true -C RFCTool/Goedel.Document.Test
! make publish NORECURSE=true -C RunDocs
! make publish NORECURSE=true -C Trace/Goedel.Trace
! make publish NORECURSE=true -C Trace/Goedel.Trace.Client
! make publish NORECURSE=true -C Trace/Goedel.Trace.Documentation
! make publish NORECURSE=true -C Trace/Goedel.Trace.Server
! make publish NORECURSE=true -C Trace/Goedel.Trace.Shell.Server
! make publish NORECURSE=true -C Libraries/Goedel.Protocol.Framework
! make publish NORECURSE=true -C Libraries/Goedel.Protocol.Debug
! make publish NORECURSE=true -C Libraries/Goedel.Debug

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
! make install NORECURSE=true -C Libraries/Goedel.Registry
! make install NORECURSE=true -C Libraries/Goedel.IO
! make install NORECURSE=true -C RFCTool/ShellBootMaker
! make install NORECURSE=true -C RFCTool/Goedel.Document.Office
! make install NORECURSE=true -C RFCTool/ShellBootMakerOffice
! make install NORECURSE=true -C Command/TestCommandParse
! make install NORECURSE=true -C Libraries/Goedel.Command
! make install NORECURSE=true -C RFCTool/Goedel.Document.Test
! make install NORECURSE=true -C RunDocs
! make install NORECURSE=true -C Trace/Goedel.Trace
! make install NORECURSE=true -C Trace/Goedel.Trace.Client
! make install NORECURSE=true -C Trace/Goedel.Trace.Documentation
! make install NORECURSE=true -C Trace/Goedel.Trace.Server
! make install NORECURSE=true -C Trace/Goedel.Trace.Shell.Server
! make install NORECURSE=true -C Libraries/Goedel.Protocol.Framework
! make install NORECURSE=true -C Libraries/Goedel.Protocol.Debug
! make install NORECURSE=true -C Libraries/Goedel.Debug

