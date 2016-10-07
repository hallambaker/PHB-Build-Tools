#
# Makefile for Visual Studio Project PHBRFC
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
     Binding1.cs\
     Properties/AssemblyInfo.cs

LinkFiles = \
    $(TARGETBIN)/Goedel.ASN.dll\
    $(TARGETBIN)/Goedel.Registry.dll\
    $(TARGETBIN)/Goedel.Tool.RFCToolBinding.dll\
    $(TARGETBIN)/LibBootMaker.dll\
    $(TARGETBIN)/LibBridge.dll\
    $(TARGETBIN)/Goedel.Tool.RFCTool.dll

LinkFilesComma = $(TARGETBIN)/Goedel.ASN.dll,$(TARGETBIN)/Goedel.Registry.dll,$(TARGETBIN)/Goedel.Tool.RFCToolBinding.dll,$(TARGETBIN)/LibBootMaker.dll,$(TARGETBIN)/LibBridge.dll,$(TARGETBIN)/Goedel.Tool.RFCTool.dll
#  Nuget Microsoft.VisualStudio.Imaging, Version=14.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a, processorArchitecture=MSIL :  ..\packages\Microsoft.VisualStudio.Imaging.14.1.24720\lib\net45\Microsoft.VisualStudio.Imaging.dll
#  Nuget Microsoft.VisualStudio.OLE.Interop, Version=7.1.40304.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a :  ..\packages\Microsoft.VisualStudio.OLE.Interop.7.10.6070\lib\Microsoft.VisualStudio.OLE.Interop.dll
#  Nuget Microsoft.VisualStudio.Shell.14.0, Version=14.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a, processorArchitecture=MSIL :  ..\packages\Microsoft.VisualStudio.Shell.14.0.14.1.24720\lib\Microsoft.VisualStudio.Shell.14.0.dll
#  Nuget Microsoft.VisualStudio.Shell.Immutable.11.0, Version=11.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a, processorArchitecture=MSIL :  ..\packages\Microsoft.VisualStudio.Shell.Immutable.11.0.11.0.50727\lib\net45\Microsoft.VisualStudio.Shell.Immutable.11.0.dll
#  Nuget Microsoft.VisualStudio.Shell.Immutable.12.0, Version=12.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a, processorArchitecture=MSIL :  ..\packages\Microsoft.VisualStudio.Shell.Immutable.12.0.12.0.21003\lib\net45\Microsoft.VisualStudio.Shell.Immutable.12.0.dll
#  Nuget Microsoft.VisualStudio.Shell.Immutable.14.0, Version=14.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a, processorArchitecture=MSIL :  ..\packages\Microsoft.VisualStudio.Shell.Immutable.14.0.14.1.24720\lib\net45\Microsoft.VisualStudio.Shell.Immutable.14.0.dll
#  Nuget Microsoft.VisualStudio.Shell.Interop, Version=7.1.40304.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a :  ..\packages\Microsoft.VisualStudio.Shell.Interop.7.10.6071\lib\Microsoft.VisualStudio.Shell.Interop.dll
#  Nuget Microsoft.VisualStudio.Shell.Interop.8.0, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a :  ..\packages\Microsoft.VisualStudio.Shell.Interop.8.0.8.0.50727\lib\Microsoft.VisualStudio.Shell.Interop.8.0.dll
#  Nuget Microsoft.VisualStudio.Shell.Interop.9.0, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a :  ..\packages\Microsoft.VisualStudio.Shell.Interop.9.0.9.0.30729\lib\Microsoft.VisualStudio.Shell.Interop.9.0.dll
#  Nuget Microsoft.VisualStudio.TextManager.Interop, Version=7.1.40304.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a :  ..\packages\Microsoft.VisualStudio.TextManager.Interop.7.10.6070\lib\Microsoft.VisualStudio.TextManager.Interop.dll
#  Nuget Microsoft.VisualStudio.TextManager.Interop.8.0, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a :  ..\packages\Microsoft.VisualStudio.TextManager.Interop.8.0.8.0.50727\lib\Microsoft.VisualStudio.TextManager.Interop.8.0.dll
#  Nuget Microsoft.VisualStudio.Threading, Version=14.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a, processorArchitecture=MSIL :  ..\packages\Microsoft.VisualStudio.Threading.14.1.111\lib\net45\Microsoft.VisualStudio.Threading.dll
#  Nuget Microsoft.VisualStudio.Utilities, Version=14.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a, processorArchitecture=MSIL :  ..\packages\Microsoft.VisualStudio.Utilities.14.1.24720\lib\net45\Microsoft.VisualStudio.Utilities.dll
#  Nuget Microsoft.VisualStudio.Validation, Version=14.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a, processorArchitecture=MSIL :  ..\packages\Microsoft.VisualStudio.Validation.14.1.111\lib\net45\Microsoft.VisualStudio.Validation.dll


NugetFiles = \
    ../packages/Microsoft.VisualStudio.Imaging.14.1.24720/lib/net45/Microsoft.VisualStudio.Imaging.dll\
    ../packages/Microsoft.VisualStudio.OLE.Interop.7.10.6070/lib/Microsoft.VisualStudio.OLE.Interop.dll\
    ../packages/Microsoft.VisualStudio.Shell.14.0.14.1.24720/lib/Microsoft.VisualStudio.Shell.14.0.dll\
    ../packages/Microsoft.VisualStudio.Shell.Immutable.11.0.11.0.50727/lib/net45/Microsoft.VisualStudio.Shell.Immutable.11.0.dll\
    ../packages/Microsoft.VisualStudio.Shell.Immutable.12.0.12.0.21003/lib/net45/Microsoft.VisualStudio.Shell.Immutable.12.0.dll\
    ../packages/Microsoft.VisualStudio.Shell.Immutable.14.0.14.1.24720/lib/net45/Microsoft.VisualStudio.Shell.Immutable.14.0.dll\
    ../packages/Microsoft.VisualStudio.Shell.Interop.7.10.6071/lib/Microsoft.VisualStudio.Shell.Interop.dll\
    ../packages/Microsoft.VisualStudio.Shell.Interop.8.0.8.0.50727/lib/Microsoft.VisualStudio.Shell.Interop.8.0.dll\
    ../packages/Microsoft.VisualStudio.Shell.Interop.9.0.9.0.30729/lib/Microsoft.VisualStudio.Shell.Interop.9.0.dll\
    ../packages/Microsoft.VisualStudio.TextManager.Interop.7.10.6070/lib/Microsoft.VisualStudio.TextManager.Interop.dll\
    ../packages/Microsoft.VisualStudio.TextManager.Interop.8.0.8.0.50727/lib/Microsoft.VisualStudio.TextManager.Interop.8.0.dll\
    ../packages/Microsoft.VisualStudio.Threading.14.1.111/lib/net45/Microsoft.VisualStudio.Threading.dll\
    ../packages/Microsoft.VisualStudio.Utilities.14.1.24720/lib/net45/Microsoft.VisualStudio.Utilities.dll\
    ../packages/Microsoft.VisualStudio.Validation.14.1.111/lib/net45/Microsoft.VisualStudio.Validation.dll
NugetFilesComa = \
    ../packages/Microsoft.VisualStudio.Imaging.14.1.24720/lib/net45/Microsoft.VisualStudio.Imaging.dll,\
    ../packages/Microsoft.VisualStudio.OLE.Interop.7.10.6070/lib/Microsoft.VisualStudio.OLE.Interop.dll,\
    ../packages/Microsoft.VisualStudio.Shell.14.0.14.1.24720/lib/Microsoft.VisualStudio.Shell.14.0.dll,\
    ../packages/Microsoft.VisualStudio.Shell.Immutable.11.0.11.0.50727/lib/net45/Microsoft.VisualStudio.Shell.Immutable.11.0.dll,\
    ../packages/Microsoft.VisualStudio.Shell.Immutable.12.0.12.0.21003/lib/net45/Microsoft.VisualStudio.Shell.Immutable.12.0.dll,\
    ../packages/Microsoft.VisualStudio.Shell.Immutable.14.0.14.1.24720/lib/net45/Microsoft.VisualStudio.Shell.Immutable.14.0.dll,\
    ../packages/Microsoft.VisualStudio.Shell.Interop.7.10.6071/lib/Microsoft.VisualStudio.Shell.Interop.dll,\
    ../packages/Microsoft.VisualStudio.Shell.Interop.8.0.8.0.50727/lib/Microsoft.VisualStudio.Shell.Interop.8.0.dll,\
    ../packages/Microsoft.VisualStudio.Shell.Interop.9.0.9.0.30729/lib/Microsoft.VisualStudio.Shell.Interop.9.0.dll,\
    ../packages/Microsoft.VisualStudio.TextManager.Interop.7.10.6070/lib/Microsoft.VisualStudio.TextManager.Interop.dll,\
    ../packages/Microsoft.VisualStudio.TextManager.Interop.8.0.8.0.50727/lib/Microsoft.VisualStudio.TextManager.Interop.8.0.dll,\
    ../packages/Microsoft.VisualStudio.Threading.14.1.111/lib/net45/Microsoft.VisualStudio.Threading.dll,\
    ../packages/Microsoft.VisualStudio.Utilities.14.1.24720/lib/net45/Microsoft.VisualStudio.Utilities.dll,\
    ../packages/Microsoft.VisualStudio.Validation.14.1.111/lib/net45/Microsoft.VisualStudio.Validation.dll

# A) Main target packaged

$(TARGETBIN)/PHBRFC.dll :| $(TARGETBIN)

$(TARGETBIN)/PHBRFC.dll :  $(SourceFiles) $(LinkFiles) $(NugetFiles)
! $(CSHARPDLL) /out:$@  $(SourceFiles) -reference:$(LinkFilesComma) -r:$(NugetFilesComa) 


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

ifdef PHB_BUILD

Binding1.cs : Binding.vsixbuild
!  $(Custom_VSIXBuild) Binding.vsixbuild $(Custom_VSIXBuild_FLAG) Binding1.cs 

endif

.PHONY : clean install publish debian rpm


# Referenced projects
#

# Recursive make targets, do not execute if the variable NORECURSE is defined
ifndef NORECURSE

../../ASN/Goedel.ASN/$(TARGETBIN)/Goedel.ASN.dll : recursive
! $(MAKE) -C ../../ASN/Goedel.ASN

../../Goedel.Registry/$(TARGETBIN)/Goedel.Registry.dll : recursive
! $(MAKE) -C ../../Goedel.Registry

../../RFCTool/Goedel.Tool.RFCTool.Binding/$(TARGETBIN)/Goedel.Tool.RFCToolBinding.dll : recursive
! $(MAKE) -C ../../RFCTool/Goedel.Tool.RFCTool.Binding

../../RFCTool/LibBootMaker/$(TARGETBIN)/LibBootMaker.dll : recursive
! $(MAKE) -C ../../RFCTool/LibBootMaker

../../RFCTool/LibBridge/$(TARGETBIN)/LibBridge.dll : recursive
! $(MAKE) -C ../../RFCTool/LibBridge

../../RFCTool/Goedel.Tool.RFCTool/$(TARGETBIN)/Goedel.Tool.RFCTool.dll : recursive
! $(MAKE) -C ../../RFCTool/Goedel.Tool.RFCTool

endif

$(TARGETBIN)/Goedel.ASN.dll :| $(TARGETBIN)
$(TARGETBIN)/Goedel.ASN.dll : ../../ASN/Goedel.ASN/$(TARGETBIN)/Goedel.ASN.dll
!  cp ../../ASN/Goedel.ASN/$(TARGETBIN)/Goedel.ASN.dll $(TARGETBIN)/Goedel.ASN.dll

$(TARGETBIN)/Goedel.Registry.dll :| $(TARGETBIN)
$(TARGETBIN)/Goedel.Registry.dll : ../../Goedel.Registry/$(TARGETBIN)/Goedel.Registry.dll
!  cp ../../Goedel.Registry/$(TARGETBIN)/Goedel.Registry.dll $(TARGETBIN)/Goedel.Registry.dll

$(TARGETBIN)/Goedel.Tool.RFCToolBinding.dll :| $(TARGETBIN)
$(TARGETBIN)/Goedel.Tool.RFCToolBinding.dll : ../../RFCTool/Goedel.Tool.RFCTool.Binding/$(TARGETBIN)/Goedel.Tool.RFCToolBinding.dll
!  cp ../../RFCTool/Goedel.Tool.RFCTool.Binding/$(TARGETBIN)/Goedel.Tool.RFCToolBinding.dll $(TARGETBIN)/Goedel.Tool.RFCToolBinding.dll

$(TARGETBIN)/LibBootMaker.dll :| $(TARGETBIN)
$(TARGETBIN)/LibBootMaker.dll : ../../RFCTool/LibBootMaker/$(TARGETBIN)/LibBootMaker.dll
!  cp ../../RFCTool/LibBootMaker/$(TARGETBIN)/LibBootMaker.dll $(TARGETBIN)/LibBootMaker.dll

$(TARGETBIN)/LibBridge.dll :| $(TARGETBIN)
$(TARGETBIN)/LibBridge.dll : ../../RFCTool/LibBridge/$(TARGETBIN)/LibBridge.dll
!  cp ../../RFCTool/LibBridge/$(TARGETBIN)/LibBridge.dll $(TARGETBIN)/LibBridge.dll

$(TARGETBIN)/Goedel.Tool.RFCTool.dll :| $(TARGETBIN)
$(TARGETBIN)/Goedel.Tool.RFCTool.dll : ../../RFCTool/Goedel.Tool.RFCTool/$(TARGETBIN)/Goedel.Tool.RFCTool.dll
!  cp ../../RFCTool/Goedel.Tool.RFCTool/$(TARGETBIN)/Goedel.Tool.RFCTool.dll $(TARGETBIN)/Goedel.Tool.RFCTool.dll


# Referenced Libraries
#
$(TARGETBIN)/Microsoft.VisualStudio.Imaging.dll :| $(TARGETBIN)
$(TARGETBIN)/Microsoft.VisualStudio.Imaging.dll : ../packages/Microsoft.VisualStudio.Imaging.14.1.24720/lib/net45/Microsoft.VisualStudio.Imaging.dll
!  cp   ../packages/Microsoft.VisualStudio.Imaging.14.1.24720/lib/net45/Microsoft.VisualStudio.Imaging.dll $(TARGETBIN)/Microsoft.VisualStudio.Imaging.dll
$(TARGETBIN)/Microsoft.VisualStudio.OLE.Interop.dll :| $(TARGETBIN)
$(TARGETBIN)/Microsoft.VisualStudio.OLE.Interop.dll : ../packages/Microsoft.VisualStudio.OLE.Interop.7.10.6070/lib/Microsoft.VisualStudio.OLE.Interop.dll
!  cp   ../packages/Microsoft.VisualStudio.OLE.Interop.7.10.6070/lib/Microsoft.VisualStudio.OLE.Interop.dll $(TARGETBIN)/Microsoft.VisualStudio.OLE.Interop.dll
$(TARGETBIN)/Microsoft.VisualStudio.Shell.14.0.dll :| $(TARGETBIN)
$(TARGETBIN)/Microsoft.VisualStudio.Shell.14.0.dll : ../packages/Microsoft.VisualStudio.Shell.14.0.14.1.24720/lib/Microsoft.VisualStudio.Shell.14.0.dll
!  cp   ../packages/Microsoft.VisualStudio.Shell.14.0.14.1.24720/lib/Microsoft.VisualStudio.Shell.14.0.dll $(TARGETBIN)/Microsoft.VisualStudio.Shell.14.0.dll
$(TARGETBIN)/Microsoft.VisualStudio.Shell.Immutable.11.0.dll :| $(TARGETBIN)
$(TARGETBIN)/Microsoft.VisualStudio.Shell.Immutable.11.0.dll : ../packages/Microsoft.VisualStudio.Shell.Immutable.11.0.11.0.50727/lib/net45/Microsoft.VisualStudio.Shell.Immutable.11.0.dll
!  cp   ../packages/Microsoft.VisualStudio.Shell.Immutable.11.0.11.0.50727/lib/net45/Microsoft.VisualStudio.Shell.Immutable.11.0.dll $(TARGETBIN)/Microsoft.VisualStudio.Shell.Immutable.11.0.dll
$(TARGETBIN)/Microsoft.VisualStudio.Shell.Immutable.12.0.dll :| $(TARGETBIN)
$(TARGETBIN)/Microsoft.VisualStudio.Shell.Immutable.12.0.dll : ../packages/Microsoft.VisualStudio.Shell.Immutable.12.0.12.0.21003/lib/net45/Microsoft.VisualStudio.Shell.Immutable.12.0.dll
!  cp   ../packages/Microsoft.VisualStudio.Shell.Immutable.12.0.12.0.21003/lib/net45/Microsoft.VisualStudio.Shell.Immutable.12.0.dll $(TARGETBIN)/Microsoft.VisualStudio.Shell.Immutable.12.0.dll
$(TARGETBIN)/Microsoft.VisualStudio.Shell.Immutable.14.0.dll :| $(TARGETBIN)
$(TARGETBIN)/Microsoft.VisualStudio.Shell.Immutable.14.0.dll : ../packages/Microsoft.VisualStudio.Shell.Immutable.14.0.14.1.24720/lib/net45/Microsoft.VisualStudio.Shell.Immutable.14.0.dll
!  cp   ../packages/Microsoft.VisualStudio.Shell.Immutable.14.0.14.1.24720/lib/net45/Microsoft.VisualStudio.Shell.Immutable.14.0.dll $(TARGETBIN)/Microsoft.VisualStudio.Shell.Immutable.14.0.dll
$(TARGETBIN)/Microsoft.VisualStudio.Shell.Interop.dll :| $(TARGETBIN)
$(TARGETBIN)/Microsoft.VisualStudio.Shell.Interop.dll : ../packages/Microsoft.VisualStudio.Shell.Interop.7.10.6071/lib/Microsoft.VisualStudio.Shell.Interop.dll
!  cp   ../packages/Microsoft.VisualStudio.Shell.Interop.7.10.6071/lib/Microsoft.VisualStudio.Shell.Interop.dll $(TARGETBIN)/Microsoft.VisualStudio.Shell.Interop.dll
$(TARGETBIN)/Microsoft.VisualStudio.Shell.Interop.8.0.dll :| $(TARGETBIN)
$(TARGETBIN)/Microsoft.VisualStudio.Shell.Interop.8.0.dll : ../packages/Microsoft.VisualStudio.Shell.Interop.8.0.8.0.50727/lib/Microsoft.VisualStudio.Shell.Interop.8.0.dll
!  cp   ../packages/Microsoft.VisualStudio.Shell.Interop.8.0.8.0.50727/lib/Microsoft.VisualStudio.Shell.Interop.8.0.dll $(TARGETBIN)/Microsoft.VisualStudio.Shell.Interop.8.0.dll
$(TARGETBIN)/Microsoft.VisualStudio.Shell.Interop.9.0.dll :| $(TARGETBIN)
$(TARGETBIN)/Microsoft.VisualStudio.Shell.Interop.9.0.dll : ../packages/Microsoft.VisualStudio.Shell.Interop.9.0.9.0.30729/lib/Microsoft.VisualStudio.Shell.Interop.9.0.dll
!  cp   ../packages/Microsoft.VisualStudio.Shell.Interop.9.0.9.0.30729/lib/Microsoft.VisualStudio.Shell.Interop.9.0.dll $(TARGETBIN)/Microsoft.VisualStudio.Shell.Interop.9.0.dll
$(TARGETBIN)/Microsoft.VisualStudio.TextManager.Interop.dll :| $(TARGETBIN)
$(TARGETBIN)/Microsoft.VisualStudio.TextManager.Interop.dll : ../packages/Microsoft.VisualStudio.TextManager.Interop.7.10.6070/lib/Microsoft.VisualStudio.TextManager.Interop.dll
!  cp   ../packages/Microsoft.VisualStudio.TextManager.Interop.7.10.6070/lib/Microsoft.VisualStudio.TextManager.Interop.dll $(TARGETBIN)/Microsoft.VisualStudio.TextManager.Interop.dll
$(TARGETBIN)/Microsoft.VisualStudio.TextManager.Interop.8.0.dll :| $(TARGETBIN)
$(TARGETBIN)/Microsoft.VisualStudio.TextManager.Interop.8.0.dll : ../packages/Microsoft.VisualStudio.TextManager.Interop.8.0.8.0.50727/lib/Microsoft.VisualStudio.TextManager.Interop.8.0.dll
!  cp   ../packages/Microsoft.VisualStudio.TextManager.Interop.8.0.8.0.50727/lib/Microsoft.VisualStudio.TextManager.Interop.8.0.dll $(TARGETBIN)/Microsoft.VisualStudio.TextManager.Interop.8.0.dll
$(TARGETBIN)/Microsoft.VisualStudio.Threading.dll :| $(TARGETBIN)
$(TARGETBIN)/Microsoft.VisualStudio.Threading.dll : ../packages/Microsoft.VisualStudio.Threading.14.1.111/lib/net45/Microsoft.VisualStudio.Threading.dll
!  cp   ../packages/Microsoft.VisualStudio.Threading.14.1.111/lib/net45/Microsoft.VisualStudio.Threading.dll $(TARGETBIN)/Microsoft.VisualStudio.Threading.dll
$(TARGETBIN)/Microsoft.VisualStudio.Utilities.dll :| $(TARGETBIN)
$(TARGETBIN)/Microsoft.VisualStudio.Utilities.dll : ../packages/Microsoft.VisualStudio.Utilities.14.1.24720/lib/net45/Microsoft.VisualStudio.Utilities.dll
!  cp   ../packages/Microsoft.VisualStudio.Utilities.14.1.24720/lib/net45/Microsoft.VisualStudio.Utilities.dll $(TARGETBIN)/Microsoft.VisualStudio.Utilities.dll
$(TARGETBIN)/Microsoft.VisualStudio.Validation.dll :| $(TARGETBIN)
$(TARGETBIN)/Microsoft.VisualStudio.Validation.dll : ../packages/Microsoft.VisualStudio.Validation.14.1.111/lib/net45/Microsoft.VisualStudio.Validation.dll
!  cp   ../packages/Microsoft.VisualStudio.Validation.14.1.111/lib/net45/Microsoft.VisualStudio.Validation.dll $(TARGETBIN)/Microsoft.VisualStudio.Validation.dll


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


install : $(INSTALL_DATA)/PHBRFC.dll

$(INSTALL_DATA)/PHBRFC.dll :| $(INSTALL_DATA)
$(INSTALL_DATA)/PHBRFC.dll : $(TARGETBIN)/PHBRFC.dll
! cp $^ $@ 




# To Do List
#
# 1) Redo install target
#    Create a wrapper script and library 
# 2) Create a Debian target
# 3) Create a RPM target
# 4) Create a nuget target


