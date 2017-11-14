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

# Project : Goedel.Cryptography.Framework.dll
# Item :  Libraries/Goedel.Cryptography.Framework
# Output :     Libraries/Goedel.Cryptography.Framework/$(TARGETBIN)/Goedel.Cryptography.Framework.dll

all : Libraries/Goedel.Cryptography.Framework/$(TARGETBIN)/Goedel.Cryptography.Framework.dll

Libraries/Goedel.Cryptography.Framework/$(TARGETBIN)/Goedel.Cryptography.Framework.dll : Libraries/Goedel.ASN/$(TARGETBIN)/Goedel.ASN.dll

Libraries/Goedel.Cryptography.Framework/$(TARGETBIN)/Goedel.Cryptography.Framework.dll : Libraries/Goedel.Cryptography/$(TARGETBIN)/Goedel.Cryptography.dll

Libraries/Goedel.Cryptography.Framework/$(TARGETBIN)/Goedel.Cryptography.Framework.dll : Libraries/Goedel.Utilities/$(TARGETBIN)/Goedel.Utilities.dll


Libraries/Goedel.Cryptography.Framework/$(TARGETBIN)/Goedel.Cryptography.Framework.dll : always
! echo "" >&2
! echo "*** Directory Libraries/Goedel.Cryptography.Framework" >&2
! make NORECURSE=true -C Libraries/Goedel.Cryptography.Framework

# Project : Goedel.Cryptography.KeyFile.dll
# Item :  Libraries/Goedel.Cryptography.KeyFile
# Output :     Libraries/Goedel.Cryptography.KeyFile/$(TARGETBIN)/Goedel.Cryptography.KeyFile.dll

all : Libraries/Goedel.Cryptography.KeyFile/$(TARGETBIN)/Goedel.Cryptography.KeyFile.dll

Libraries/Goedel.Cryptography.KeyFile/$(TARGETBIN)/Goedel.Cryptography.KeyFile.dll : Libraries/Goedel.ASN/$(TARGETBIN)/Goedel.ASN.dll

Libraries/Goedel.Cryptography.KeyFile/$(TARGETBIN)/Goedel.Cryptography.KeyFile.dll : Libraries/Goedel.Cryptography/$(TARGETBIN)/Goedel.Cryptography.dll

Libraries/Goedel.Cryptography.KeyFile/$(TARGETBIN)/Goedel.Cryptography.KeyFile.dll : Libraries/Goedel.FSR/$(TARGETBIN)/Goedel.FSR.dll

Libraries/Goedel.Cryptography.KeyFile/$(TARGETBIN)/Goedel.Cryptography.KeyFile.dll : Libraries/Goedel.Utilities/$(TARGETBIN)/Goedel.Utilities.dll

Libraries/Goedel.Cryptography.KeyFile/$(TARGETBIN)/Goedel.Cryptography.KeyFile.dll : Libraries/Goedel.Cryptography.Framework/$(TARGETBIN)/Goedel.Cryptography.Framework.dll

Libraries/Goedel.Cryptography.KeyFile/$(TARGETBIN)/Goedel.Cryptography.KeyFile.dll : Libraries/Goedel.IO/$(TARGETBIN)/Goedel.IO.dll


Libraries/Goedel.Cryptography.KeyFile/$(TARGETBIN)/Goedel.Cryptography.KeyFile.dll : always
! echo "" >&2
! echo "*** Directory Libraries/Goedel.Cryptography.KeyFile" >&2
! make NORECURSE=true -C Libraries/Goedel.Cryptography.KeyFile

# Project : Goedel.Cryptography.Windows.dll
# Item :  Libraries/Goedel.Cryptography.Windows
# Output :     Libraries/Goedel.Cryptography.Windows/$(TARGETBIN)/Goedel.Cryptography.Windows.dll

all : Libraries/Goedel.Cryptography.Windows/$(TARGETBIN)/Goedel.Cryptography.Windows.dll

Libraries/Goedel.Cryptography.Windows/$(TARGETBIN)/Goedel.Cryptography.Windows.dll : Libraries/Goedel.Cryptography.Jose/$(TARGETBIN)/Goedel.Cryptography.Jose.dll

Libraries/Goedel.Cryptography.Windows/$(TARGETBIN)/Goedel.Cryptography.Windows.dll : Libraries/Goedel.Cryptography/$(TARGETBIN)/Goedel.Cryptography.dll

Libraries/Goedel.Cryptography.Windows/$(TARGETBIN)/Goedel.Cryptography.Windows.dll : Libraries/Goedel.Protocol/$(TARGETBIN)/Goedel.Protocol.dll

Libraries/Goedel.Cryptography.Windows/$(TARGETBIN)/Goedel.Cryptography.Windows.dll : Libraries/Goedel.Utilities/$(TARGETBIN)/Goedel.Utilities.dll

Libraries/Goedel.Cryptography.Windows/$(TARGETBIN)/Goedel.Cryptography.Windows.dll : Libraries/Goedel.Cryptography.Framework/$(TARGETBIN)/Goedel.Cryptography.Framework.dll

Libraries/Goedel.Cryptography.Windows/$(TARGETBIN)/Goedel.Cryptography.Windows.dll : Libraries/Goedel.IO/$(TARGETBIN)/Goedel.IO.dll


Libraries/Goedel.Cryptography.Windows/$(TARGETBIN)/Goedel.Cryptography.Windows.dll : always
! echo "" >&2
! echo "*** Directory Libraries/Goedel.Cryptography.Windows" >&2
! make NORECURSE=true -C Libraries/Goedel.Cryptography.Windows

# Project : Goedel.Cryptography.Linux.dll
# Item :  Libraries/Goedel.Cryptography.Linux
# Output :     Libraries/Goedel.Cryptography.Linux/$(TARGETBIN)/Goedel.Cryptography.Linux.dll

all : Libraries/Goedel.Cryptography.Linux/$(TARGETBIN)/Goedel.Cryptography.Linux.dll

Libraries/Goedel.Cryptography.Linux/$(TARGETBIN)/Goedel.Cryptography.Linux.dll : Libraries/Goedel.Cryptography.Jose/$(TARGETBIN)/Goedel.Cryptography.Jose.dll

Libraries/Goedel.Cryptography.Linux/$(TARGETBIN)/Goedel.Cryptography.Linux.dll : Libraries/Goedel.Cryptography/$(TARGETBIN)/Goedel.Cryptography.dll

Libraries/Goedel.Cryptography.Linux/$(TARGETBIN)/Goedel.Cryptography.Linux.dll : Libraries/Goedel.Discovery/$(TARGETBIN)/Goedel.Discovery.dll

Libraries/Goedel.Cryptography.Linux/$(TARGETBIN)/Goedel.Cryptography.Linux.dll : Libraries/Goedel.Protocol/$(TARGETBIN)/Goedel.Protocol.dll

Libraries/Goedel.Cryptography.Linux/$(TARGETBIN)/Goedel.Cryptography.Linux.dll : Libraries/Goedel.Utilities/$(TARGETBIN)/Goedel.Utilities.dll

Libraries/Goedel.Cryptography.Linux/$(TARGETBIN)/Goedel.Cryptography.Linux.dll : Libraries/Goedel.Cryptography.Framework/$(TARGETBIN)/Goedel.Cryptography.Framework.dll

Libraries/Goedel.Cryptography.Linux/$(TARGETBIN)/Goedel.Cryptography.Linux.dll : Libraries/Goedel.Cryptography.KeyFile/$(TARGETBIN)/Goedel.Cryptography.KeyFile.dll

Libraries/Goedel.Cryptography.Linux/$(TARGETBIN)/Goedel.Cryptography.Linux.dll : Libraries/Goedel.IO/$(TARGETBIN)/Goedel.IO.dll


Libraries/Goedel.Cryptography.Linux/$(TARGETBIN)/Goedel.Cryptography.Linux.dll : always
! echo "" >&2
! echo "*** Directory Libraries/Goedel.Cryptography.Linux" >&2
! make NORECURSE=true -C Libraries/Goedel.Cryptography.Linux

# Project : Goedel.IO.dll
# Item :  Libraries/Goedel.IO
# Output :     Libraries/Goedel.IO/$(TARGETBIN)/Goedel.IO.dll

all : Libraries/Goedel.IO/$(TARGETBIN)/Goedel.IO.dll


Libraries/Goedel.IO/$(TARGETBIN)/Goedel.IO.dll : always
! echo "" >&2
! echo "*** Directory Libraries/Goedel.IO" >&2
! make NORECURSE=true -C Libraries/Goedel.IO

# Project : Goedel.Protocol.Debug.dll
# Item :  Libraries/Goedel.Protocol.Debug
# Output :     Libraries/Goedel.Protocol.Debug/$(TARGETBIN)/Goedel.Protocol.Debug.dll

all : Libraries/Goedel.Protocol.Debug/$(TARGETBIN)/Goedel.Protocol.Debug.dll

Libraries/Goedel.Protocol.Debug/$(TARGETBIN)/Goedel.Protocol.Debug.dll : Libraries/Goedel.Protocol/$(TARGETBIN)/Goedel.Protocol.dll

Libraries/Goedel.Protocol.Debug/$(TARGETBIN)/Goedel.Protocol.Debug.dll : Libraries/Goedel.Utilities/$(TARGETBIN)/Goedel.Utilities.dll


Libraries/Goedel.Protocol.Debug/$(TARGETBIN)/Goedel.Protocol.Debug.dll : always
! echo "" >&2
! echo "*** Directory Libraries/Goedel.Protocol.Debug" >&2
! make NORECURSE=true -C Libraries/Goedel.Protocol.Debug

# Project : Goedel.Registry.dll
# Item :  Libraries/Goedel.Registry
# Output :     Libraries/Goedel.Registry/$(TARGETBIN)/Goedel.Registry.dll

all : Libraries/Goedel.Registry/$(TARGETBIN)/Goedel.Registry.dll

Libraries/Goedel.Registry/$(TARGETBIN)/Goedel.Registry.dll : Libraries/Goedel.FSR/$(TARGETBIN)/Goedel.FSR.dll

Libraries/Goedel.Registry/$(TARGETBIN)/Goedel.Registry.dll : Libraries/Goedel.Utilities/$(TARGETBIN)/Goedel.Utilities.dll


Libraries/Goedel.Registry/$(TARGETBIN)/Goedel.Registry.dll : always
! echo "" >&2
! echo "*** Directory Libraries/Goedel.Registry" >&2
! make NORECURSE=true -C Libraries/Goedel.Registry

# Project : Goedel.Test.dll
# Item :  Libraries/Goedel.Test
# Output :     Libraries/Goedel.Test/$(TARGETBIN)/Goedel.Test.dll

all : Libraries/Goedel.Test/$(TARGETBIN)/Goedel.Test.dll

Libraries/Goedel.Test/$(TARGETBIN)/Goedel.Test.dll : Libraries/Goedel.Cryptography.Windows/$(TARGETBIN)/Goedel.Cryptography.Windows.dll

Libraries/Goedel.Test/$(TARGETBIN)/Goedel.Test.dll : Libraries/Goedel.Cryptography/$(TARGETBIN)/Goedel.Cryptography.dll

Libraries/Goedel.Test/$(TARGETBIN)/Goedel.Test.dll : Libraries/Goedel.Discovery/$(TARGETBIN)/Goedel.Discovery.dll

Libraries/Goedel.Test/$(TARGETBIN)/Goedel.Test.dll : Libraries/Goedel.Utilities/$(TARGETBIN)/Goedel.Utilities.dll

Libraries/Goedel.Test/$(TARGETBIN)/Goedel.Test.dll : Libraries/Goedel.Cryptography.Framework/$(TARGETBIN)/Goedel.Cryptography.Framework.dll

Libraries/Goedel.Test/$(TARGETBIN)/Goedel.Test.dll : Libraries/Goedel.IO/$(TARGETBIN)/Goedel.IO.dll


Libraries/Goedel.Test/$(TARGETBIN)/Goedel.Test.dll : always
! echo "" >&2
! echo "*** Directory Libraries/Goedel.Test" >&2
! make NORECURSE=true -C Libraries/Goedel.Test

# Project : AAAConsole.exe
# Item :  Testing/AAAConsole
# Output :     Testing/AAAConsole/$(TARGETEXE)/AAAConsole.exe

all : Testing/AAAConsole/$(TARGETBIN)/AAAConsole.exe

Testing/AAAConsole/$(TARGETBIN)/AAAConsole.exe : Libraries/Goedel.ASN/$(TARGETBIN)/Goedel.ASN.dll

Testing/AAAConsole/$(TARGETBIN)/AAAConsole.exe : Libraries/Goedel.Command/$(TARGETBIN)/Goedel.Command.dll

Testing/AAAConsole/$(TARGETBIN)/AAAConsole.exe : Libraries/Goedel.Cryptography.Container/$(TARGETBIN)/Goedel.Cryptography.Container.dll

Testing/AAAConsole/$(TARGETBIN)/AAAConsole.exe : Libraries/Goedel.Cryptography.Jose/$(TARGETBIN)/Goedel.Cryptography.Jose.dll

Testing/AAAConsole/$(TARGETBIN)/AAAConsole.exe : Libraries/Goedel.Cryptography.Windows/$(TARGETBIN)/Goedel.Cryptography.Windows.dll

Testing/AAAConsole/$(TARGETBIN)/AAAConsole.exe : Libraries/Goedel.Cryptography/$(TARGETBIN)/Goedel.Cryptography.dll

Testing/AAAConsole/$(TARGETBIN)/AAAConsole.exe : Libraries/Goedel.Discovery/$(TARGETBIN)/Goedel.Discovery.dll

Testing/AAAConsole/$(TARGETBIN)/AAAConsole.exe : Libraries/Goedel.FSR/$(TARGETBIN)/Goedel.FSR.dll

Testing/AAAConsole/$(TARGETBIN)/AAAConsole.exe : Libraries/Goedel.Protocol/$(TARGETBIN)/Goedel.Protocol.dll

Testing/AAAConsole/$(TARGETBIN)/AAAConsole.exe : Libraries/Goedel.Utilities/$(TARGETBIN)/Goedel.Utilities.dll

Testing/AAAConsole/$(TARGETBIN)/AAAConsole.exe : Libraries/Goedel.Registry/$(TARGETBIN)/Goedel.Registry.dll

Testing/AAAConsole/$(TARGETBIN)/AAAConsole.exe : Libraries/Goedel.Cryptography.Framework/$(TARGETBIN)/Goedel.Cryptography.Framework.dll

Testing/AAAConsole/$(TARGETBIN)/AAAConsole.exe : Libraries/Goedel.Cryptography.KeyFile/$(TARGETBIN)/Goedel.Cryptography.KeyFile.dll

Testing/AAAConsole/$(TARGETBIN)/AAAConsole.exe : Libraries/Goedel.IO/$(TARGETBIN)/Goedel.IO.dll

Testing/AAAConsole/$(TARGETBIN)/AAAConsole.exe : Libraries/Goedel.Test/$(TARGETBIN)/Goedel.Test.dll

Testing/AAAConsole/$(TARGETBIN)/AAAConsole.exe : Testing/Test.Goedel.Cryptography.Container/$(TARGETBIN)/Test.Goedel.Cryptography.Container.dll


Testing/AAAConsole/$(TARGETBIN)/AAAConsole.exe : always
! echo "" >&2
! echo "*** Directory Testing/AAAConsole" >&2
! make NORECURSE=true -C Testing/AAAConsole

# Project : Test.Goedel.ASN.dll
# Item :  Testing/Test.Goedel.ASN
# Output :     Testing/Test.Goedel.ASN/$(TARGETBIN)/Test.Goedel.ASN.dll

all : Testing/Test.Goedel.ASN/$(TARGETBIN)/Test.Goedel.ASN.dll

Testing/Test.Goedel.ASN/$(TARGETBIN)/Test.Goedel.ASN.dll : Libraries/Goedel.ASN/$(TARGETBIN)/Goedel.ASN.dll

Testing/Test.Goedel.ASN/$(TARGETBIN)/Test.Goedel.ASN.dll : Libraries/Goedel.Cryptography/$(TARGETBIN)/Goedel.Cryptography.dll

Testing/Test.Goedel.ASN/$(TARGETBIN)/Test.Goedel.ASN.dll : Libraries/Goedel.Discovery/$(TARGETBIN)/Goedel.Discovery.dll

Testing/Test.Goedel.ASN/$(TARGETBIN)/Test.Goedel.ASN.dll : Libraries/Goedel.Utilities/$(TARGETBIN)/Goedel.Utilities.dll

Testing/Test.Goedel.ASN/$(TARGETBIN)/Test.Goedel.ASN.dll : Libraries/Goedel.Cryptography.Framework/$(TARGETBIN)/Goedel.Cryptography.Framework.dll

Testing/Test.Goedel.ASN/$(TARGETBIN)/Test.Goedel.ASN.dll : Libraries/Goedel.Test/$(TARGETBIN)/Goedel.Test.dll


Testing/Test.Goedel.ASN/$(TARGETBIN)/Test.Goedel.ASN.dll : always
! echo "" >&2
! echo "*** Directory Testing/Test.Goedel.ASN" >&2
! make NORECURSE=true -C Testing/Test.Goedel.ASN

# Project : Test.Goedel.Cryptography.dll
# Item :  Testing/Test.Goedel.Cryptography
# Output :     Testing/Test.Goedel.Cryptography/$(TARGETBIN)/Test.Goedel.Cryptography.dll

all : Testing/Test.Goedel.Cryptography/$(TARGETBIN)/Test.Goedel.Cryptography.dll

Testing/Test.Goedel.Cryptography/$(TARGETBIN)/Test.Goedel.Cryptography.dll : Libraries/Goedel.Cryptography.Jose/$(TARGETBIN)/Goedel.Cryptography.Jose.dll

Testing/Test.Goedel.Cryptography/$(TARGETBIN)/Test.Goedel.Cryptography.dll : Libraries/Goedel.Cryptography/$(TARGETBIN)/Goedel.Cryptography.dll

Testing/Test.Goedel.Cryptography/$(TARGETBIN)/Test.Goedel.Cryptography.dll : Libraries/Goedel.FSR/$(TARGETBIN)/Goedel.FSR.dll

Testing/Test.Goedel.Cryptography/$(TARGETBIN)/Test.Goedel.Cryptography.dll : Libraries/Goedel.Utilities/$(TARGETBIN)/Goedel.Utilities.dll

Testing/Test.Goedel.Cryptography/$(TARGETBIN)/Test.Goedel.Cryptography.dll : Libraries/Goedel.Test/$(TARGETBIN)/Goedel.Test.dll

Testing/Test.Goedel.Cryptography/$(TARGETBIN)/Test.Goedel.Cryptography.dll : Libraries/Goedel.Cryptography.Framework/$(TARGETBIN)/Goedel.Cryptography.Framework.dll


Testing/Test.Goedel.Cryptography/$(TARGETBIN)/Test.Goedel.Cryptography.dll : always
! echo "" >&2
! echo "*** Directory Testing/Test.Goedel.Cryptography" >&2
! make NORECURSE=true -C Testing/Test.Goedel.Cryptography

# Project : Test.Goedel.Cryptography.Jose.dll
# Item :  Testing/Test.Goedel.Cryptography.Jose
# Output :     Testing/Test.Goedel.Cryptography.Jose/$(TARGETBIN)/Test.Goedel.Cryptography.Jose.dll

all : Testing/Test.Goedel.Cryptography.Jose/$(TARGETBIN)/Test.Goedel.Cryptography.Jose.dll

Testing/Test.Goedel.Cryptography.Jose/$(TARGETBIN)/Test.Goedel.Cryptography.Jose.dll : Libraries/Goedel.ASN/$(TARGETBIN)/Goedel.ASN.dll

Testing/Test.Goedel.Cryptography.Jose/$(TARGETBIN)/Test.Goedel.Cryptography.Jose.dll : Libraries/Goedel.Cryptography.Jose/$(TARGETBIN)/Goedel.Cryptography.Jose.dll

Testing/Test.Goedel.Cryptography.Jose/$(TARGETBIN)/Test.Goedel.Cryptography.Jose.dll : Libraries/Goedel.Cryptography.KeyFile/$(TARGETBIN)/Goedel.Cryptography.KeyFile.dll

Testing/Test.Goedel.Cryptography.Jose/$(TARGETBIN)/Test.Goedel.Cryptography.Jose.dll : Libraries/Goedel.Cryptography/$(TARGETBIN)/Goedel.Cryptography.dll

Testing/Test.Goedel.Cryptography.Jose/$(TARGETBIN)/Test.Goedel.Cryptography.Jose.dll : Libraries/Goedel.Discovery/$(TARGETBIN)/Goedel.Discovery.dll

Testing/Test.Goedel.Cryptography.Jose/$(TARGETBIN)/Test.Goedel.Cryptography.Jose.dll : Libraries/Goedel.FSR/$(TARGETBIN)/Goedel.FSR.dll

Testing/Test.Goedel.Cryptography.Jose/$(TARGETBIN)/Test.Goedel.Cryptography.Jose.dll : Libraries/Goedel.Utilities/$(TARGETBIN)/Goedel.Utilities.dll

Testing/Test.Goedel.Cryptography.Jose/$(TARGETBIN)/Test.Goedel.Cryptography.Jose.dll : Libraries/Goedel.Cryptography.Windows/$(TARGETBIN)/Goedel.Cryptography.Windows.dll

Testing/Test.Goedel.Cryptography.Jose/$(TARGETBIN)/Test.Goedel.Cryptography.Jose.dll : Libraries/Goedel.Cryptography.Framework/$(TARGETBIN)/Goedel.Cryptography.Framework.dll

Testing/Test.Goedel.Cryptography.Jose/$(TARGETBIN)/Test.Goedel.Cryptography.Jose.dll : Libraries/Goedel.IO/$(TARGETBIN)/Goedel.IO.dll

Testing/Test.Goedel.Cryptography.Jose/$(TARGETBIN)/Test.Goedel.Cryptography.Jose.dll : Libraries/Goedel.Protocol/$(TARGETBIN)/Goedel.Protocol.dll

Testing/Test.Goedel.Cryptography.Jose/$(TARGETBIN)/Test.Goedel.Cryptography.Jose.dll : Libraries/Goedel.Test/$(TARGETBIN)/Goedel.Test.dll


Testing/Test.Goedel.Cryptography.Jose/$(TARGETBIN)/Test.Goedel.Cryptography.Jose.dll : always
! echo "" >&2
! echo "*** Directory Testing/Test.Goedel.Cryptography.Jose" >&2
! make NORECURSE=true -C Testing/Test.Goedel.Cryptography.Jose

# Project : Test.Goedel.Cryptography.KeyFile.dll
# Item :  Testing/Test.Goedel.Cryptography.KeyFile
# Output :     Testing/Test.Goedel.Cryptography.KeyFile/$(TARGETBIN)/Test.Goedel.Cryptography.KeyFile.dll

all : Testing/Test.Goedel.Cryptography.KeyFile/$(TARGETBIN)/Test.Goedel.Cryptography.KeyFile.dll

Testing/Test.Goedel.Cryptography.KeyFile/$(TARGETBIN)/Test.Goedel.Cryptography.KeyFile.dll : Libraries/Goedel.Cryptography/$(TARGETBIN)/Goedel.Cryptography.dll

Testing/Test.Goedel.Cryptography.KeyFile/$(TARGETBIN)/Test.Goedel.Cryptography.KeyFile.dll : Libraries/Goedel.FSR/$(TARGETBIN)/Goedel.FSR.dll

Testing/Test.Goedel.Cryptography.KeyFile/$(TARGETBIN)/Test.Goedel.Cryptography.KeyFile.dll : Libraries/Goedel.Utilities/$(TARGETBIN)/Goedel.Utilities.dll

Testing/Test.Goedel.Cryptography.KeyFile/$(TARGETBIN)/Test.Goedel.Cryptography.KeyFile.dll : Libraries/Goedel.Cryptography.Framework/$(TARGETBIN)/Goedel.Cryptography.Framework.dll

Testing/Test.Goedel.Cryptography.KeyFile/$(TARGETBIN)/Test.Goedel.Cryptography.KeyFile.dll : Libraries/Goedel.Cryptography.KeyFile/$(TARGETBIN)/Goedel.Cryptography.KeyFile.dll

Testing/Test.Goedel.Cryptography.KeyFile/$(TARGETBIN)/Test.Goedel.Cryptography.KeyFile.dll : Libraries/Goedel.IO/$(TARGETBIN)/Goedel.IO.dll

Testing/Test.Goedel.Cryptography.KeyFile/$(TARGETBIN)/Test.Goedel.Cryptography.KeyFile.dll : Libraries/Goedel.Test/$(TARGETBIN)/Goedel.Test.dll


Testing/Test.Goedel.Cryptography.KeyFile/$(TARGETBIN)/Test.Goedel.Cryptography.KeyFile.dll : always
! echo "" >&2
! echo "*** Directory Testing/Test.Goedel.Cryptography.KeyFile" >&2
! make NORECURSE=true -C Testing/Test.Goedel.Cryptography.KeyFile

# Project : Test.Goedel.Cryptography.Linux.dll
# Item :  Testing/Test.Goedel.Cryptography.Linux
# Output :     Testing/Test.Goedel.Cryptography.Linux/$(TARGETBIN)/Test.Goedel.Cryptography.Linux.dll

all : Testing/Test.Goedel.Cryptography.Linux/$(TARGETBIN)/Test.Goedel.Cryptography.Linux.dll

Testing/Test.Goedel.Cryptography.Linux/$(TARGETBIN)/Test.Goedel.Cryptography.Linux.dll : Libraries/Goedel.Cryptography.Jose/$(TARGETBIN)/Goedel.Cryptography.Jose.dll

Testing/Test.Goedel.Cryptography.Linux/$(TARGETBIN)/Test.Goedel.Cryptography.Linux.dll : Libraries/Goedel.Cryptography.Linux/$(TARGETBIN)/Goedel.Cryptography.Linux.dll

Testing/Test.Goedel.Cryptography.Linux/$(TARGETBIN)/Test.Goedel.Cryptography.Linux.dll : Libraries/Goedel.Cryptography/$(TARGETBIN)/Goedel.Cryptography.dll

Testing/Test.Goedel.Cryptography.Linux/$(TARGETBIN)/Test.Goedel.Cryptography.Linux.dll : Libraries/Goedel.Protocol/$(TARGETBIN)/Goedel.Protocol.dll

Testing/Test.Goedel.Cryptography.Linux/$(TARGETBIN)/Test.Goedel.Cryptography.Linux.dll : Libraries/Goedel.Utilities/$(TARGETBIN)/Goedel.Utilities.dll

Testing/Test.Goedel.Cryptography.Linux/$(TARGETBIN)/Test.Goedel.Cryptography.Linux.dll : Libraries/Goedel.Cryptography.Framework/$(TARGETBIN)/Goedel.Cryptography.Framework.dll

Testing/Test.Goedel.Cryptography.Linux/$(TARGETBIN)/Test.Goedel.Cryptography.Linux.dll : Libraries/Goedel.Cryptography.KeyFile/$(TARGETBIN)/Goedel.Cryptography.KeyFile.dll

Testing/Test.Goedel.Cryptography.Linux/$(TARGETBIN)/Test.Goedel.Cryptography.Linux.dll : Libraries/Goedel.IO/$(TARGETBIN)/Goedel.IO.dll

Testing/Test.Goedel.Cryptography.Linux/$(TARGETBIN)/Test.Goedel.Cryptography.Linux.dll : Libraries/Goedel.Test/$(TARGETBIN)/Goedel.Test.dll


Testing/Test.Goedel.Cryptography.Linux/$(TARGETBIN)/Test.Goedel.Cryptography.Linux.dll : always
! echo "" >&2
! echo "*** Directory Testing/Test.Goedel.Cryptography.Linux" >&2
! make NORECURSE=true -C Testing/Test.Goedel.Cryptography.Linux

# Project : Test.Goedel.Cryptography.dll
# Item :  Testing/Test.Goedel.Cryptography.RSA
# Output :     Testing/Test.Goedel.Cryptography.RSA/$(TARGETBIN)/Test.Goedel.Cryptography.dll

all : Testing/Test.Goedel.Cryptography.RSA/$(TARGETBIN)/Test.Goedel.Cryptography.dll

Testing/Test.Goedel.Cryptography.RSA/$(TARGETBIN)/Test.Goedel.Cryptography.dll : Libraries/Goedel.ASN/$(TARGETBIN)/Goedel.ASN.dll

Testing/Test.Goedel.Cryptography.RSA/$(TARGETBIN)/Test.Goedel.Cryptography.dll : Libraries/Goedel.Cryptography.Jose/$(TARGETBIN)/Goedel.Cryptography.Jose.dll

Testing/Test.Goedel.Cryptography.RSA/$(TARGETBIN)/Test.Goedel.Cryptography.dll : Libraries/Goedel.Cryptography/$(TARGETBIN)/Goedel.Cryptography.dll

Testing/Test.Goedel.Cryptography.RSA/$(TARGETBIN)/Test.Goedel.Cryptography.dll : Libraries/Goedel.FSR/$(TARGETBIN)/Goedel.FSR.dll

Testing/Test.Goedel.Cryptography.RSA/$(TARGETBIN)/Test.Goedel.Cryptography.dll : Libraries/Goedel.Utilities/$(TARGETBIN)/Goedel.Utilities.dll

Testing/Test.Goedel.Cryptography.RSA/$(TARGETBIN)/Test.Goedel.Cryptography.dll : Libraries/Goedel.Cryptography.KeyFile/$(TARGETBIN)/Goedel.Cryptography.KeyFile.dll

Testing/Test.Goedel.Cryptography.RSA/$(TARGETBIN)/Test.Goedel.Cryptography.dll : Libraries/Goedel.IO/$(TARGETBIN)/Goedel.IO.dll

Testing/Test.Goedel.Cryptography.RSA/$(TARGETBIN)/Test.Goedel.Cryptography.dll : Libraries/Goedel.Test/$(TARGETBIN)/Goedel.Test.dll

Testing/Test.Goedel.Cryptography.RSA/$(TARGETBIN)/Test.Goedel.Cryptography.dll : Libraries/Goedel.Cryptography.Framework/$(TARGETBIN)/Goedel.Cryptography.Framework.dll


Testing/Test.Goedel.Cryptography.RSA/$(TARGETBIN)/Test.Goedel.Cryptography.dll : always
! echo "" >&2
! echo "*** Directory Testing/Test.Goedel.Cryptography.RSA" >&2
! make NORECURSE=true -C Testing/Test.Goedel.Cryptography.RSA

# Project : Test.Goedel.Cryptography.Windows.dll
# Item :  Testing/Test.Goedel.Cryptography.Windows
# Output :     Testing/Test.Goedel.Cryptography.Windows/$(TARGETBIN)/Test.Goedel.Cryptography.Windows.dll

all : Testing/Test.Goedel.Cryptography.Windows/$(TARGETBIN)/Test.Goedel.Cryptography.Windows.dll

Testing/Test.Goedel.Cryptography.Windows/$(TARGETBIN)/Test.Goedel.Cryptography.Windows.dll : Libraries/Goedel.Cryptography.Jose/$(TARGETBIN)/Goedel.Cryptography.Jose.dll

Testing/Test.Goedel.Cryptography.Windows/$(TARGETBIN)/Test.Goedel.Cryptography.Windows.dll : Libraries/Goedel.Cryptography.Windows/$(TARGETBIN)/Goedel.Cryptography.Windows.dll

Testing/Test.Goedel.Cryptography.Windows/$(TARGETBIN)/Test.Goedel.Cryptography.Windows.dll : Libraries/Goedel.Cryptography/$(TARGETBIN)/Goedel.Cryptography.dll

Testing/Test.Goedel.Cryptography.Windows/$(TARGETBIN)/Test.Goedel.Cryptography.Windows.dll : Libraries/Goedel.Utilities/$(TARGETBIN)/Goedel.Utilities.dll

Testing/Test.Goedel.Cryptography.Windows/$(TARGETBIN)/Test.Goedel.Cryptography.Windows.dll : Libraries/Goedel.Cryptography.Framework/$(TARGETBIN)/Goedel.Cryptography.Framework.dll

Testing/Test.Goedel.Cryptography.Windows/$(TARGETBIN)/Test.Goedel.Cryptography.Windows.dll : Libraries/Goedel.Cryptography.KeyFile/$(TARGETBIN)/Goedel.Cryptography.KeyFile.dll

Testing/Test.Goedel.Cryptography.Windows/$(TARGETBIN)/Test.Goedel.Cryptography.Windows.dll : Libraries/Goedel.IO/$(TARGETBIN)/Goedel.IO.dll

Testing/Test.Goedel.Cryptography.Windows/$(TARGETBIN)/Test.Goedel.Cryptography.Windows.dll : Libraries/Goedel.Test/$(TARGETBIN)/Goedel.Test.dll


Testing/Test.Goedel.Cryptography.Windows/$(TARGETBIN)/Test.Goedel.Cryptography.Windows.dll : always
! echo "" >&2
! echo "*** Directory Testing/Test.Goedel.Cryptography.Windows" >&2
! make NORECURSE=true -C Testing/Test.Goedel.Cryptography.Windows

# Project : Test.Goedel.DNS.dll
# Item :  Testing/Test.Goedel.DNS
# Output :     Testing/Test.Goedel.DNS/$(TARGETBIN)/Test.Goedel.DNS.dll

all : Testing/Test.Goedel.DNS/$(TARGETBIN)/Test.Goedel.DNS.dll

Testing/Test.Goedel.DNS/$(TARGETBIN)/Test.Goedel.DNS.dll : Libraries/Goedel.Discovery/$(TARGETBIN)/Goedel.Discovery.dll

Testing/Test.Goedel.DNS/$(TARGETBIN)/Test.Goedel.DNS.dll : Libraries/Goedel.Utilities/$(TARGETBIN)/Goedel.Utilities.dll

Testing/Test.Goedel.DNS/$(TARGETBIN)/Test.Goedel.DNS.dll : Libraries/Goedel.Test/$(TARGETBIN)/Goedel.Test.dll


Testing/Test.Goedel.DNS/$(TARGETBIN)/Test.Goedel.DNS.dll : always
! echo "" >&2
! echo "*** Directory Testing/Test.Goedel.DNS" >&2
! make NORECURSE=true -C Testing/Test.Goedel.DNS

# Project : Goedel.Command.dll
# Item :  Libraries/Goedel.Command
# Output :     Libraries/Goedel.Command/$(TARGETBIN)/Goedel.Command.dll

all : Libraries/Goedel.Command/$(TARGETBIN)/Goedel.Command.dll

Libraries/Goedel.Command/$(TARGETBIN)/Goedel.Command.dll : Libraries/Goedel.FSR/$(TARGETBIN)/Goedel.FSR.dll

Libraries/Goedel.Command/$(TARGETBIN)/Goedel.Command.dll : Libraries/Goedel.Registry/$(TARGETBIN)/Goedel.Registry.dll

Libraries/Goedel.Command/$(TARGETBIN)/Goedel.Command.dll : Libraries/Goedel.Utilities/$(TARGETBIN)/Goedel.Utilities.dll


Libraries/Goedel.Command/$(TARGETBIN)/Goedel.Command.dll : always
! echo "" >&2
! echo "*** Directory Libraries/Goedel.Command" >&2
! make NORECURSE=true -C Libraries/Goedel.Command

# Project : Test.Goedel.Command.dll
# Item :  Testing/Test.Goedel.Command
# Output :     Testing/Test.Goedel.Command/$(TARGETBIN)/Test.Goedel.Command.dll

all : Testing/Test.Goedel.Command/$(TARGETBIN)/Test.Goedel.Command.dll

Testing/Test.Goedel.Command/$(TARGETBIN)/Test.Goedel.Command.dll : Libraries/Goedel.Command/$(TARGETBIN)/Goedel.Command.dll

Testing/Test.Goedel.Command/$(TARGETBIN)/Test.Goedel.Command.dll : Libraries/Goedel.FSR/$(TARGETBIN)/Goedel.FSR.dll

Testing/Test.Goedel.Command/$(TARGETBIN)/Test.Goedel.Command.dll : Libraries/Goedel.Utilities/$(TARGETBIN)/Goedel.Utilities.dll

Testing/Test.Goedel.Command/$(TARGETBIN)/Test.Goedel.Command.dll : Libraries/Goedel.Registry/$(TARGETBIN)/Goedel.Registry.dll

Testing/Test.Goedel.Command/$(TARGETBIN)/Test.Goedel.Command.dll : Libraries/Goedel.Test/$(TARGETBIN)/Goedel.Test.dll


Testing/Test.Goedel.Command/$(TARGETBIN)/Test.Goedel.Command.dll : always
! echo "" >&2
! echo "*** Directory Testing/Test.Goedel.Command" >&2
! make NORECURSE=true -C Testing/Test.Goedel.Command

# Project : ContentProject.dll
# Item :  Documentation/StandardLibraries/ContentProject
# Output :     Documentation/StandardLibraries/ContentProject/$(TARGETBIN)/ContentProject.dll

all : Documentation/StandardLibraries/ContentProject/$(TARGETBIN)/ContentProject.dll


Documentation/StandardLibraries/ContentProject/$(TARGETBIN)/ContentProject.dll : always
! echo "" >&2
! echo "*** Directory Documentation/StandardLibraries/ContentProject" >&2
! make NORECURSE=true -C Documentation/StandardLibraries/ContentProject

# Project : Goedel.Cryptography.Container.dll
# Item :  Libraries/Goedel.Cryptography.Container
# Output :     Libraries/Goedel.Cryptography.Container/$(TARGETBIN)/Goedel.Cryptography.Container.dll

all : Libraries/Goedel.Cryptography.Container/$(TARGETBIN)/Goedel.Cryptography.Container.dll

Libraries/Goedel.Cryptography.Container/$(TARGETBIN)/Goedel.Cryptography.Container.dll : Libraries/Goedel.Cryptography.Jose/$(TARGETBIN)/Goedel.Cryptography.Jose.dll

Libraries/Goedel.Cryptography.Container/$(TARGETBIN)/Goedel.Cryptography.Container.dll : Libraries/Goedel.Cryptography/$(TARGETBIN)/Goedel.Cryptography.dll

Libraries/Goedel.Cryptography.Container/$(TARGETBIN)/Goedel.Cryptography.Container.dll : Libraries/Goedel.Protocol/$(TARGETBIN)/Goedel.Protocol.dll

Libraries/Goedel.Cryptography.Container/$(TARGETBIN)/Goedel.Cryptography.Container.dll : Libraries/Goedel.Utilities/$(TARGETBIN)/Goedel.Utilities.dll

Libraries/Goedel.Cryptography.Container/$(TARGETBIN)/Goedel.Cryptography.Container.dll : Libraries/Goedel.IO/$(TARGETBIN)/Goedel.IO.dll


Libraries/Goedel.Cryptography.Container/$(TARGETBIN)/Goedel.Cryptography.Container.dll : always
! echo "" >&2
! echo "*** Directory Libraries/Goedel.Cryptography.Container" >&2
! make NORECURSE=true -C Libraries/Goedel.Cryptography.Container

# Project : Test.Goedel.Cryptography.Container.dll
# Item :  Testing/Test.Goedel.Cryptography.Container
# Output :     Testing/Test.Goedel.Cryptography.Container/$(TARGETBIN)/Test.Goedel.Cryptography.Container.dll

all : Testing/Test.Goedel.Cryptography.Container/$(TARGETBIN)/Test.Goedel.Cryptography.Container.dll

Testing/Test.Goedel.Cryptography.Container/$(TARGETBIN)/Test.Goedel.Cryptography.Container.dll : Libraries/Goedel.Cryptography.Container/$(TARGETBIN)/Goedel.Cryptography.Container.dll

Testing/Test.Goedel.Cryptography.Container/$(TARGETBIN)/Test.Goedel.Cryptography.Container.dll : Libraries/Goedel.Cryptography.Jose/$(TARGETBIN)/Goedel.Cryptography.Jose.dll

Testing/Test.Goedel.Cryptography.Container/$(TARGETBIN)/Test.Goedel.Cryptography.Container.dll : Libraries/Goedel.Cryptography/$(TARGETBIN)/Goedel.Cryptography.dll

Testing/Test.Goedel.Cryptography.Container/$(TARGETBIN)/Test.Goedel.Cryptography.Container.dll : Libraries/Goedel.Protocol/$(TARGETBIN)/Goedel.Protocol.dll

Testing/Test.Goedel.Cryptography.Container/$(TARGETBIN)/Test.Goedel.Cryptography.Container.dll : Libraries/Goedel.Utilities/$(TARGETBIN)/Goedel.Utilities.dll

Testing/Test.Goedel.Cryptography.Container/$(TARGETBIN)/Test.Goedel.Cryptography.Container.dll : Libraries/Goedel.IO/$(TARGETBIN)/Goedel.IO.dll

Testing/Test.Goedel.Cryptography.Container/$(TARGETBIN)/Test.Goedel.Cryptography.Container.dll : Libraries/Goedel.Test/$(TARGETBIN)/Goedel.Test.dll


Testing/Test.Goedel.Cryptography.Container/$(TARGETBIN)/Test.Goedel.Cryptography.Container.dll : always
! echo "" >&2
! echo "*** Directory Testing/Test.Goedel.Cryptography.Container" >&2
! make NORECURSE=true -C Testing/Test.Goedel.Cryptography.Container

# Project : Goedel.Protocol.Exchange.dll
# Item :  Libraries/Goedel.Protocol.Exchange
# Output :     Libraries/Goedel.Protocol.Exchange/$(TARGETBIN)/Goedel.Protocol.Exchange.dll

all : Libraries/Goedel.Protocol.Exchange/$(TARGETBIN)/Goedel.Protocol.Exchange.dll

Libraries/Goedel.Protocol.Exchange/$(TARGETBIN)/Goedel.Protocol.Exchange.dll : Libraries/Goedel.Cryptography.Jose/$(TARGETBIN)/Goedel.Cryptography.Jose.dll

Libraries/Goedel.Protocol.Exchange/$(TARGETBIN)/Goedel.Protocol.Exchange.dll : Libraries/Goedel.Cryptography/$(TARGETBIN)/Goedel.Cryptography.dll

Libraries/Goedel.Protocol.Exchange/$(TARGETBIN)/Goedel.Protocol.Exchange.dll : Libraries/Goedel.Protocol/$(TARGETBIN)/Goedel.Protocol.dll

Libraries/Goedel.Protocol.Exchange/$(TARGETBIN)/Goedel.Protocol.Exchange.dll : Libraries/Goedel.Utilities/$(TARGETBIN)/Goedel.Utilities.dll


Libraries/Goedel.Protocol.Exchange/$(TARGETBIN)/Goedel.Protocol.Exchange.dll : always
! echo "" >&2
! echo "*** Directory Libraries/Goedel.Protocol.Exchange" >&2
! make NORECURSE=true -C Libraries/Goedel.Protocol.Exchange

# Project : Goedel.Protocol.Exchange.Server.dll
# Item :  Libraries/Goedel.Protocol.Exchange.Server
# Output :     Libraries/Goedel.Protocol.Exchange.Server/$(TARGETBIN)/Goedel.Protocol.Exchange.Server.dll

all : Libraries/Goedel.Protocol.Exchange.Server/$(TARGETBIN)/Goedel.Protocol.Exchange.Server.dll

Libraries/Goedel.Protocol.Exchange.Server/$(TARGETBIN)/Goedel.Protocol.Exchange.Server.dll : Libraries/Goedel.Cryptography.Jose/$(TARGETBIN)/Goedel.Cryptography.Jose.dll

Libraries/Goedel.Protocol.Exchange.Server/$(TARGETBIN)/Goedel.Protocol.Exchange.Server.dll : Libraries/Goedel.Cryptography/$(TARGETBIN)/Goedel.Cryptography.dll

Libraries/Goedel.Protocol.Exchange.Server/$(TARGETBIN)/Goedel.Protocol.Exchange.Server.dll : Libraries/Goedel.Protocol/$(TARGETBIN)/Goedel.Protocol.dll

Libraries/Goedel.Protocol.Exchange.Server/$(TARGETBIN)/Goedel.Protocol.Exchange.Server.dll : Libraries/Goedel.Utilities/$(TARGETBIN)/Goedel.Utilities.dll

Libraries/Goedel.Protocol.Exchange.Server/$(TARGETBIN)/Goedel.Protocol.Exchange.Server.dll : Libraries/Goedel.Protocol.Exchange/$(TARGETBIN)/Goedel.Protocol.Exchange.dll


Libraries/Goedel.Protocol.Exchange.Server/$(TARGETBIN)/Goedel.Protocol.Exchange.Server.dll : always
! echo "" >&2
! echo "*** Directory Libraries/Goedel.Protocol.Exchange.Server" >&2
! make NORECURSE=true -C Libraries/Goedel.Protocol.Exchange.Server

# Project : Goedel.Utilities.dll
# Item :  Libraries/Goedel.Utilities
# Output :     Libraries/Goedel.Utilities/$(TARGETBIN)/Goedel.Utilities.dll

all : Libraries/Goedel.Utilities/$(TARGETBIN)/Goedel.Utilities.dll


Libraries/Goedel.Utilities/$(TARGETBIN)/Goedel.Utilities.dll : always
! echo "" >&2
! echo "*** Directory Libraries/Goedel.Utilities" >&2
! make NORECURSE=true -C Libraries/Goedel.Utilities

# Project : Goedel.ASN.dll
# Item :  Libraries/Goedel.ASN
# Output :     Libraries/Goedel.ASN/$(TARGETBIN)/Goedel.ASN.dll

all : Libraries/Goedel.ASN/$(TARGETBIN)/Goedel.ASN.dll

Libraries/Goedel.ASN/$(TARGETBIN)/Goedel.ASN.dll : Libraries/Goedel.Utilities/$(TARGETBIN)/Goedel.Utilities.dll


Libraries/Goedel.ASN/$(TARGETBIN)/Goedel.ASN.dll : always
! echo "" >&2
! echo "*** Directory Libraries/Goedel.ASN" >&2
! make NORECURSE=true -C Libraries/Goedel.ASN

# Project : Goedel.Cryptography.dll
# Item :  Libraries/Goedel.Cryptography
# Output :     Libraries/Goedel.Cryptography/$(TARGETBIN)/Goedel.Cryptography.dll

all : Libraries/Goedel.Cryptography/$(TARGETBIN)/Goedel.Cryptography.dll

Libraries/Goedel.Cryptography/$(TARGETBIN)/Goedel.Cryptography.dll : Libraries/Goedel.ASN/$(TARGETBIN)/Goedel.ASN.dll

Libraries/Goedel.Cryptography/$(TARGETBIN)/Goedel.Cryptography.dll : Libraries/Goedel.Utilities/$(TARGETBIN)/Goedel.Utilities.dll


Libraries/Goedel.Cryptography/$(TARGETBIN)/Goedel.Cryptography.dll : always
! echo "" >&2
! echo "*** Directory Libraries/Goedel.Cryptography" >&2
! make NORECURSE=true -C Libraries/Goedel.Cryptography

# Project : Goedel.Cryptography.Jose.dll
# Item :  Libraries/Goedel.Cryptography.Jose
# Output :     Libraries/Goedel.Cryptography.Jose/$(TARGETBIN)/Goedel.Cryptography.Jose.dll

all : Libraries/Goedel.Cryptography.Jose/$(TARGETBIN)/Goedel.Cryptography.Jose.dll

Libraries/Goedel.Cryptography.Jose/$(TARGETBIN)/Goedel.Cryptography.Jose.dll : Libraries/Goedel.ASN/$(TARGETBIN)/Goedel.ASN.dll

Libraries/Goedel.Cryptography.Jose/$(TARGETBIN)/Goedel.Cryptography.Jose.dll : Libraries/Goedel.Cryptography/$(TARGETBIN)/Goedel.Cryptography.dll

Libraries/Goedel.Cryptography.Jose/$(TARGETBIN)/Goedel.Cryptography.Jose.dll : Libraries/Goedel.Protocol/$(TARGETBIN)/Goedel.Protocol.dll

Libraries/Goedel.Cryptography.Jose/$(TARGETBIN)/Goedel.Cryptography.Jose.dll : Libraries/Goedel.Utilities/$(TARGETBIN)/Goedel.Utilities.dll


Libraries/Goedel.Cryptography.Jose/$(TARGETBIN)/Goedel.Cryptography.Jose.dll : always
! echo "" >&2
! echo "*** Directory Libraries/Goedel.Cryptography.Jose" >&2
! make NORECURSE=true -C Libraries/Goedel.Cryptography.Jose

# Project : Goedel.Cryptography.Ticket.dll
# Item :  Libraries/Goedel.Cryptography.Ticket
# Output :     Libraries/Goedel.Cryptography.Ticket/$(TARGETBIN)/Goedel.Cryptography.Ticket.dll

all : Libraries/Goedel.Cryptography.Ticket/$(TARGETBIN)/Goedel.Cryptography.Ticket.dll

Libraries/Goedel.Cryptography.Ticket/$(TARGETBIN)/Goedel.Cryptography.Ticket.dll : Libraries/Goedel.ASN/$(TARGETBIN)/Goedel.ASN.dll

Libraries/Goedel.Cryptography.Ticket/$(TARGETBIN)/Goedel.Cryptography.Ticket.dll : Libraries/Goedel.Cryptography/$(TARGETBIN)/Goedel.Cryptography.dll

Libraries/Goedel.Cryptography.Ticket/$(TARGETBIN)/Goedel.Cryptography.Ticket.dll : Libraries/Goedel.Utilities/$(TARGETBIN)/Goedel.Utilities.dll


Libraries/Goedel.Cryptography.Ticket/$(TARGETBIN)/Goedel.Cryptography.Ticket.dll : always
! echo "" >&2
! echo "*** Directory Libraries/Goedel.Cryptography.Ticket" >&2
! make NORECURSE=true -C Libraries/Goedel.Cryptography.Ticket

# Project : Goedel.FSR.dll
# Item :  Libraries/Goedel.FSR
# Output :     Libraries/Goedel.FSR/$(TARGETBIN)/Goedel.FSR.dll

all : Libraries/Goedel.FSR/$(TARGETBIN)/Goedel.FSR.dll

Libraries/Goedel.FSR/$(TARGETBIN)/Goedel.FSR.dll : Libraries/Goedel.Utilities/$(TARGETBIN)/Goedel.Utilities.dll


Libraries/Goedel.FSR/$(TARGETBIN)/Goedel.FSR.dll : always
! echo "" >&2
! echo "*** Directory Libraries/Goedel.FSR" >&2
! make NORECURSE=true -C Libraries/Goedel.FSR

# Project : Goedel.Discovery.dll
# Item :  Libraries/Goedel.Discovery
# Output :     Libraries/Goedel.Discovery/$(TARGETBIN)/Goedel.Discovery.dll

all : Libraries/Goedel.Discovery/$(TARGETBIN)/Goedel.Discovery.dll

Libraries/Goedel.Discovery/$(TARGETBIN)/Goedel.Discovery.dll : Libraries/Goedel.Utilities/$(TARGETBIN)/Goedel.Utilities.dll


Libraries/Goedel.Discovery/$(TARGETBIN)/Goedel.Discovery.dll : always
! echo "" >&2
! echo "*** Directory Libraries/Goedel.Discovery" >&2
! make NORECURSE=true -C Libraries/Goedel.Discovery

# Project : Goedel.Protocol.dll
# Item :  Libraries/Goedel.Protocol
# Output :     Libraries/Goedel.Protocol/$(TARGETBIN)/Goedel.Protocol.dll

all : Libraries/Goedel.Protocol/$(TARGETBIN)/Goedel.Protocol.dll

Libraries/Goedel.Protocol/$(TARGETBIN)/Goedel.Protocol.dll : Libraries/Goedel.Discovery/$(TARGETBIN)/Goedel.Discovery.dll

Libraries/Goedel.Protocol/$(TARGETBIN)/Goedel.Protocol.dll : Libraries/Goedel.Utilities/$(TARGETBIN)/Goedel.Utilities.dll


Libraries/Goedel.Protocol/$(TARGETBIN)/Goedel.Protocol.dll : always
! echo "" >&2
! echo "*** Directory Libraries/Goedel.Protocol" >&2
! make NORECURSE=true -C Libraries/Goedel.Protocol



# clean all projects
clean :
! make clean NORECURSE=true -C Libraries/Goedel.Cryptography.Framework
! make clean NORECURSE=true -C Libraries/Goedel.Cryptography.KeyFile
! make clean NORECURSE=true -C Libraries/Goedel.Cryptography.Windows
! make clean NORECURSE=true -C Libraries/Goedel.Cryptography.Linux
! make clean NORECURSE=true -C Libraries/Goedel.IO
! make clean NORECURSE=true -C Libraries/Goedel.Protocol.Debug
! make clean NORECURSE=true -C Libraries/Goedel.Registry
! make clean NORECURSE=true -C Libraries/Goedel.Test
! make clean NORECURSE=true -C Testing/AAAConsole
! make clean NORECURSE=true -C Testing/Test.Goedel.ASN
! make clean NORECURSE=true -C Testing/Test.Goedel.Cryptography
! make clean NORECURSE=true -C Testing/Test.Goedel.Cryptography.Jose
! make clean NORECURSE=true -C Testing/Test.Goedel.Cryptography.KeyFile
! make clean NORECURSE=true -C Testing/Test.Goedel.Cryptography.Linux
! make clean NORECURSE=true -C Testing/Test.Goedel.Cryptography.RSA
! make clean NORECURSE=true -C Testing/Test.Goedel.Cryptography.Windows
! make clean NORECURSE=true -C Testing/Test.Goedel.DNS
! make clean NORECURSE=true -C Libraries/Goedel.Command
! make clean NORECURSE=true -C Testing/Test.Goedel.Command
! make clean NORECURSE=true -C Documentation/StandardLibraries/ContentProject
! make clean NORECURSE=true -C Libraries/Goedel.Cryptography.Container
! make clean NORECURSE=true -C Testing/Test.Goedel.Cryptography.Container
! make clean NORECURSE=true -C Libraries/Goedel.Protocol.Exchange
! make clean NORECURSE=true -C Libraries/Goedel.Protocol.Exchange.Server
! make clean NORECURSE=true -C Libraries/Goedel.Utilities
! make clean NORECURSE=true -C Libraries/Goedel.ASN
! make clean NORECURSE=true -C Libraries/Goedel.Cryptography
! make clean NORECURSE=true -C Libraries/Goedel.Cryptography.Jose
! make clean NORECURSE=true -C Libraries/Goedel.Cryptography.Ticket
! make clean NORECURSE=true -C Libraries/Goedel.FSR
! make clean NORECURSE=true -C Libraries/Goedel.Discovery
! make clean NORECURSE=true -C Libraries/Goedel.Protocol

# publish all projects
publish : all
! make publish NORECURSE=true -C Libraries/Goedel.Cryptography.Framework
! make publish NORECURSE=true -C Libraries/Goedel.Cryptography.KeyFile
! make publish NORECURSE=true -C Libraries/Goedel.Cryptography.Windows
! make publish NORECURSE=true -C Libraries/Goedel.Cryptography.Linux
! make publish NORECURSE=true -C Libraries/Goedel.IO
! make publish NORECURSE=true -C Libraries/Goedel.Protocol.Debug
! make publish NORECURSE=true -C Libraries/Goedel.Registry
! make publish NORECURSE=true -C Libraries/Goedel.Test
! make publish NORECURSE=true -C Testing/AAAConsole
! make publish NORECURSE=true -C Testing/Test.Goedel.ASN
! make publish NORECURSE=true -C Testing/Test.Goedel.Cryptography
! make publish NORECURSE=true -C Testing/Test.Goedel.Cryptography.Jose
! make publish NORECURSE=true -C Testing/Test.Goedel.Cryptography.KeyFile
! make publish NORECURSE=true -C Testing/Test.Goedel.Cryptography.Linux
! make publish NORECURSE=true -C Testing/Test.Goedel.Cryptography.RSA
! make publish NORECURSE=true -C Testing/Test.Goedel.Cryptography.Windows
! make publish NORECURSE=true -C Testing/Test.Goedel.DNS
! make publish NORECURSE=true -C Libraries/Goedel.Command
! make publish NORECURSE=true -C Testing/Test.Goedel.Command
! make publish NORECURSE=true -C Documentation/StandardLibraries/ContentProject
! make publish NORECURSE=true -C Libraries/Goedel.Cryptography.Container
! make publish NORECURSE=true -C Testing/Test.Goedel.Cryptography.Container
! make publish NORECURSE=true -C Libraries/Goedel.Protocol.Exchange
! make publish NORECURSE=true -C Libraries/Goedel.Protocol.Exchange.Server
! make publish NORECURSE=true -C Libraries/Goedel.Utilities
! make publish NORECURSE=true -C Libraries/Goedel.ASN
! make publish NORECURSE=true -C Libraries/Goedel.Cryptography
! make publish NORECURSE=true -C Libraries/Goedel.Cryptography.Jose
! make publish NORECURSE=true -C Libraries/Goedel.Cryptography.Ticket
! make publish NORECURSE=true -C Libraries/Goedel.FSR
! make publish NORECURSE=true -C Libraries/Goedel.Discovery
! make publish NORECURSE=true -C Libraries/Goedel.Protocol

# install all projects
install : all
! make install NORECURSE=true -C Libraries/Goedel.Cryptography.Framework
! make install NORECURSE=true -C Libraries/Goedel.Cryptography.KeyFile
! make install NORECURSE=true -C Libraries/Goedel.Cryptography.Windows
! make install NORECURSE=true -C Libraries/Goedel.Cryptography.Linux
! make install NORECURSE=true -C Libraries/Goedel.IO
! make install NORECURSE=true -C Libraries/Goedel.Protocol.Debug
! make install NORECURSE=true -C Libraries/Goedel.Registry
! make install NORECURSE=true -C Libraries/Goedel.Test
! make install NORECURSE=true -C Testing/AAAConsole
! make install NORECURSE=true -C Testing/Test.Goedel.ASN
! make install NORECURSE=true -C Testing/Test.Goedel.Cryptography
! make install NORECURSE=true -C Testing/Test.Goedel.Cryptography.Jose
! make install NORECURSE=true -C Testing/Test.Goedel.Cryptography.KeyFile
! make install NORECURSE=true -C Testing/Test.Goedel.Cryptography.Linux
! make install NORECURSE=true -C Testing/Test.Goedel.Cryptography.RSA
! make install NORECURSE=true -C Testing/Test.Goedel.Cryptography.Windows
! make install NORECURSE=true -C Testing/Test.Goedel.DNS
! make install NORECURSE=true -C Libraries/Goedel.Command
! make install NORECURSE=true -C Testing/Test.Goedel.Command
! make install NORECURSE=true -C Documentation/StandardLibraries/ContentProject
! make install NORECURSE=true -C Libraries/Goedel.Cryptography.Container
! make install NORECURSE=true -C Testing/Test.Goedel.Cryptography.Container
! make install NORECURSE=true -C Libraries/Goedel.Protocol.Exchange
! make install NORECURSE=true -C Libraries/Goedel.Protocol.Exchange.Server
! make install NORECURSE=true -C Libraries/Goedel.Utilities
! make install NORECURSE=true -C Libraries/Goedel.ASN
! make install NORECURSE=true -C Libraries/Goedel.Cryptography
! make install NORECURSE=true -C Libraries/Goedel.Cryptography.Jose
! make install NORECURSE=true -C Libraries/Goedel.Cryptography.Ticket
! make install NORECURSE=true -C Libraries/Goedel.FSR
! make install NORECURSE=true -C Libraries/Goedel.Discovery
! make install NORECURSE=true -C Libraries/Goedel.Protocol

