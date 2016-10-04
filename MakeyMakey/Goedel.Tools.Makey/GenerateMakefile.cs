// #script 1.0 
// Script Syntax Version:  1.0
// #license MITLicense 

//  Unknown by Unknown
//  
//  Permission is hereby granted, free of charge, to any person obtaining a copy
//  of this software and associated documentation files (the "Software"), to deal
//  in the Software without restriction, including without limitation the rights
//  to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//  copies of the Software, and to permit persons to whom the Software is
//  furnished to do so, subject to the following conditions:
//  
//  The above copyright notice and this permission notice shall be included in
//  all copies or substantial portions of the Software.
//  
//  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//  IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//  AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//  LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//  OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
//  THE SOFTWARE.
//  
//  
// #xclass Goedel.Tool.Makey Generate 
using System;
using System.IO;
using System.Collections.Generic;
using Goedel.Registry;
namespace Goedel.Tool.Makey {
	public partial class Generate : global::Goedel.Registry.Script {

		// #%  public Generate (TextWriter Output) : base (Output) {} 
		  public Generate (TextWriter Output) : base (Output) {}
		// #% string Prefix = "! "; //"\t"; 
		 string Prefix = "! "; //"\t";
		//  
		//  
		// #method Preamble VSFile VSFile 
		

		//
		// Preamble
		//
		public void Preamble (VSFile VSFile) {
			// ## This file is generated automatically from the Visual Studio Project 
			_Output.Write ("# This file is generated automatically from the Visual Studio Project\n{0}", _Indent);
			// ## File. If you make changes to this file and do not update the project 
			_Output.Write ("# File. If you make changes to this file and do not update the project\n{0}", _Indent);
			// ## file, changes will be lost when the file is regenerated. 
			_Output.Write ("# file, changes will be lost when the file is regenerated.\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			// ## NB: This process will fail if any of the paths have spaces in them 
			_Output.Write ("# NB: This process will fail if any of the paths have spaces in them\n{0}", _Indent);
			// ## While it is possible to work around the lack of support for spaces in  
			_Output.Write ("# While it is possible to work around the lack of support for spaces in \n{0}", _Indent);
			// ## file paths in gmake, it is not possible to do this reliably in the tools 
			_Output.Write ("# file paths in gmake, it is not possible to do this reliably in the tools\n{0}", _Indent);
			// ## that it invokes to build the system. Rather than do half a job, it seems 
			_Output.Write ("# that it invokes to build the system. Rather than do half a job, it seems\n{0}", _Indent);
			// ## safest to simply reject the corner case 
			_Output.Write ("# safest to simply reject the corner case\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			// ## The following targets are defined (well planned) 
			_Output.Write ("# The following targets are defined (well planned)\n{0}", _Indent);
			// ## 
			_Output.Write ("#\n{0}", _Indent);
			// ## make 			Compile for the current platform 
			_Output.Write ("# make 			Compile for the current platform\n{0}", _Indent);
			// ## make cross		Compile for all platforms 
			_Output.Write ("# make cross		Compile for all platforms\n{0}", _Indent);
			// ## make install		Compile and install 
			_Output.Write ("# make install		Compile and install\n{0}", _Indent);
			// ## make clean		Delete all target and intermediate files 
			_Output.Write ("# make clean		Delete all target and intermediate files\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			// ## The following build flags are supported 
			_Output.Write ("# The following build flags are supported\n{0}", _Indent);
			// ## 
			_Output.Write ("#\n{0}", _Indent);
			// ## make mode= release | debug			Build release or debug version 
			_Output.Write ("# make mode= release | debug			Build release or debug version\n{0}", _Indent);
			// ## make arch= this | all | <x>			Bundle for the current platform, all platforms 
			_Output.Write ("# make arch= this | all | <x>			Bundle for the current platform, all platforms\n{0}", _Indent);
			// ##										or the specified platform 
			_Output.Write ("#										or the specified platform\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			// ## Define the default target directories (referenced projects must all follow same scheme) 
			_Output.Write ("# Define the default target directories (referenced projects must all follow same scheme)\n{0}", _Indent);
			// ## 
			_Output.Write ("#\n{0}", _Indent);
			// ## By default, we arrange the mono targets as follows: 
			_Output.Write ("# By default, we arrange the mono targets as follows:\n{0}", _Indent);
			// ## 
			_Output.Write ("#\n{0}", _Indent);
			// ## <Source>					The source code directory 
			_Output.Write ("# <Source>					The source code directory\n{0}", _Indent);
			// ## <Source>/mono/			Equivalent to VS bin directory 
			_Output.Write ("# <Source>/mono/			Equivalent to VS bin directory\n{0}", _Indent);
			// ## <Source>/mono/Debug		Equivalent to VS bin/Debug directory 
			_Output.Write ("# <Source>/mono/Debug		Equivalent to VS bin/Debug directory\n{0}", _Indent);
			// ## <Source>/mono/Release	Equivalent to VS bin/Debug directory 
			_Output.Write ("# <Source>/mono/Release	Equivalent to VS bin/Debug directory\n{0}", _Indent);
			// ## 
			_Output.Write ("#\n{0}", _Indent);
			// ## If the target is an executable, the following directories are also created: 
			_Output.Write ("# If the target is an executable, the following directories are also created:\n{0}", _Indent);
			// ## 
			_Output.Write ("#\n{0}", _Indent);
			// ## <Source>/This			The bundled executable for the platform the code was compiled on 
			_Output.Write ("# <Source>/This			The bundled executable for the platform the code was compiled on\n{0}", _Indent);
			// ## <Source>/<Arch1>			The bundled executable for the platform <Arch1>	 
			_Output.Write ("# <Source>/<Arch1>			The bundled executable for the platform <Arch1>	\n{0}", _Indent);
			// ## 
			_Output.Write ("#\n{0}", _Indent);
			// ## If the install target is selected, the bundles will be installed in 
			_Output.Write ("# If the install target is selected, the bundles will be installed in\n{0}", _Indent);
			// ## 
			_Output.Write ("#\n{0}", _Indent);
			// ## ~/Tools/This				The bundled executable for platform the code is built on 
			_Output.Write ("# ~/Tools/This				The bundled executable for platform the code is built on\n{0}", _Indent);
			// ## ~/Tools/<Arch1>			The bundled executable for this platform <Arch1>	 
			_Output.Write ("# ~/Tools/<Arch1>			The bundled executable for this platform <Arch1>	\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			// export TARGETROOT	?= mono 
			_Output.Write ("export TARGETROOT	?= mono\n{0}", _Indent);
			// export MODE			?= Release 
			_Output.Write ("export MODE			?= Release\n{0}", _Indent);
			// export ARCH			?= This 
			_Output.Write ("export ARCH			?= This\n{0}", _Indent);
			// export Packages		?= ~/Packages 
			_Output.Write ("export Packages		?= ~/Packages\n{0}", _Indent);
			// export PackagesPath ?= /lib/net40 
			_Output.Write ("export PackagesPath ?= /lib/net40\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			// export TARGETBIN	= $(TARGETROOT)/$(MODE) 
			_Output.Write ("export TARGETBIN	= $(TARGETROOT)/$(MODE)\n{0}", _Indent);
			// export TARGETEXE	= $(TARGETROOT)/$(ARCH) 
			_Output.Write ("export TARGETEXE	= $(TARGETROOT)/$(ARCH)\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			// export DESTDIR		?= ~/.local 
			_Output.Write ("export DESTDIR		?= ~/.local\n{0}", _Indent);
			// export bindir		?= /bin 
			_Output.Write ("export bindir		?= /bin\n{0}", _Indent);
			// export libdir		?= /lib 
			_Output.Write ("export libdir		?= /lib\n{0}", _Indent);
			// export INSTALL_PROGRAM	?= $(DESTDIR)$(bindir) 
			_Output.Write ("export INSTALL_PROGRAM	?= $(DESTDIR)$(bindir)\n{0}", _Indent);
			// export INSTALL_DATA		?= $(DESTDIR)$(libdir) 
			_Output.Write ("export INSTALL_DATA		?= $(DESTDIR)$(libdir)\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			// ## Define the default compilers, linkers, packagers, etc. 
			_Output.Write ("# Define the default compilers, linkers, packagers, etc.\n{0}", _Indent);
			// export CSHARPDLL	?=  mcs /target:library 
			_Output.Write ("export CSHARPDLL	?=  mcs /target:library\n{0}", _Indent);
			// export CSHARPEXE	?=  mcs /target:exe 
			_Output.Write ("export CSHARPEXE	?=  mcs /target:exe\n{0}", _Indent);
			// export BUNDLE		?=  mkbundle --deps --static -o  
			_Output.Write ("export BUNDLE		?=  mkbundle --deps --static -o \n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			// ## The following tools are used in the goedel build system itself: 
			_Output.Write ("# The following tools are used in the goedel build system itself:\n{0}", _Indent);
			// #% PHBTool ("RFC2TXT",		"rfctool /in", "/txt"); 
			 PHBTool ("RFC2TXT",		"rfctool /in", "/txt");
			// #% PHBTool ("RFC2XML",		"rfctool /in", "/xml"); 
			 PHBTool ("RFC2XML",		"rfctool /in", "/xml");
			// #% PHBTool ("RFC2MD",		"rfctool /in", "/md"); 
			 PHBTool ("RFC2MD",		"rfctool /in", "/md");
			// #% PHBTool ("RFC2HTML",		"rfctool /in", "/html"); 
			 PHBTool ("RFC2HTML",		"rfctool /in", "/html");
			// #% PHBTool ("CommandCS",	"commandparse /in", "/cs"); 
			 PHBTool ("CommandCS",	"commandparse /in", "/cs");
			// #% PHBTool ("FSRCS",		"fsrgen /in", "/cs"); 
			 PHBTool ("FSRCS",		"fsrgen /in", "/cs");
			// #% PHBTool ("Exceptional",	"exceptional /in", "/cs"); 
			 PHBTool ("Exceptional",	"exceptional /in", "/cs");
			// #% PHBTool ("GScript",		"gscript /in", "/cs"); 
			 PHBTool ("GScript",		"gscript /in", "/cs");
			// #% PHBTool ("Goedel3",		"goedel3 /in", "/cs"); 
			 PHBTool ("Goedel3",		"goedel3 /in", "/cs");
			// #% PHBTool ("ASN2CS",		"asn2 /in", "/cs"); 
			 PHBTool ("ASN2CS",		"asn2 /in", "/cs");
			// #% PHBTool ("DomainerCS",	"domainer /in", "/cs"); 
			 PHBTool ("DomainerCS",	"domainer /in", "/cs");
			// #% PHBTool ("RegistryCS",	"registryconfig /in", "/cs"); 
			 PHBTool ("RegistryCS",	"registryconfig /in", "/cs");
			// #% PHBTool ("VSIXBuild",	"vsixbuild /in", "/cs"); 
			 PHBTool ("VSIXBuild",	"vsixbuild /in", "/cs");
			// #% PHBTool ("ProtoGen",		"protogen /in", "/cs"); 
			 PHBTool ("ProtoGen",		"protogen /in", "/cs");
			// #% PHBTool ("TrojanGTK",	"trojan /gtk", "/cs"); 
			 PHBTool ("TrojanGTK",	"trojan /gtk", "/cs");
			//  
			_Output.Write ("\n{0}", _Indent);
			// #if (Prefix != "\t") 
			if (  (Prefix != "\t") ) {
				// ## Use the specified character as the prefix character. Note this may not  
				_Output.Write ("# Use the specified character as the prefix character. Note this may not \n{0}", _Indent);
				// ## be supported on versions of make other than gmake. 
				_Output.Write ("# be supported on versions of make other than gmake.\n{0}", _Indent);
				// .RECIPEPREFIX = #{Prefix} 
				_Output.Write (".RECIPEPREFIX = {1}\n{0}", _Indent, Prefix);
				// #end if 
				}
			// #end method 
			}
		//  
		//  
		// #method GenerateMakefile VSSolution Solution 
		

		//
		// GenerateMakefile
		//
		public void GenerateMakefile (VSSolution Solution) {
			// ## 
			_Output.Write ("#\n{0}", _Indent);
			// ## Makefile for Visual Studio Solution .. 
			_Output.Write ("# Makefile for Visual Studio Solution ..\n{0}", _Indent);
			// ## 
			_Output.Write ("#\n{0}", _Indent);
			// #call Preamble Solution 
			Preamble (Solution);
			//  
			_Output.Write ("\n{0}", _Indent);
			// ## The main target 
			_Output.Write ("# The main target\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			// .PHONY : all always 
			_Output.Write (".PHONY : all always\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			// ## Need to identify the target directory using UnixPath() 
			_Output.Write ("# Need to identify the target directory using UnixPath()\n{0}", _Indent);
			// ## This file in directory #{Solution.Directory} 
			_Output.Write ("# This file in directory {1}\n{0}", _Indent, Solution.Directory);
			//  
			_Output.Write ("\n{0}", _Indent);
			// #foreach (var Item in Solution.Projects) 
			foreach  (var Item in Solution.Projects) {
				// #if (Item.MakeUnix()) 
				if (  (Item.MakeUnix()) ) {
					// #% var  Project = Item.Project; 
					 var  Project = Item.Project;
					// #% var Target = Project.Target.UnixFile(); 
					 var Target = Project.Target.UnixFile();
					// #% var Directory = Item.Directory.UnixPath(); 
					 var Directory = Item.Directory.UnixPath();
					// ## Project : #{Target} 
					_Output.Write ("# Project : {1}\n{0}", _Indent, Target);
					// ## Item :  #{Directory} 
					_Output.Write ("# Item :  {1}\n{0}", _Indent, Directory);
					// #if (Directory.IndexOf(' ') >= 0) 
					if (  (Directory.IndexOf(' ') >= 0) ) {
						// ***** Spaces in file path not supported 
						_Output.Write ("***** Spaces in file path not supported\n{0}", _Indent);
						// #elseif (Project.IsExe) 
						} else if (  (Project.IsExe)) {
						// ## Output :     #{Directory}/$(TARGETEXE)/#{Target} 
						_Output.Write ("# Output :     {1}/$(TARGETEXE)/{2}\n{0}", _Indent, Directory, Target);
						// #elseif (Project.IsLibrary) 
						} else if (  (Project.IsLibrary)) {
						// ## Output :     #{Directory}/$(TARGETBIN)/#{Target} 
						_Output.Write ("# Output :     {1}/$(TARGETBIN)/{2}\n{0}", _Indent, Directory, Target);
						// #else 
						} else {
						// #end if 
						}
					//  
					_Output.Write ("\n{0}", _Indent);
					// all : #{Directory}/$(TARGETBIN)/#{Project.Target} 
					_Output.Write ("all : {1}/$(TARGETBIN)/{2}\n{0}", _Indent, Directory, Project.Target);
					//  
					_Output.Write ("\n{0}", _Indent);
					// #foreach (var Dep in Project.ProjectReference) 
					foreach  (var Dep in Project.ProjectReference) {
						// #% var SubProject = Dep.SubProject; 
						 var SubProject = Dep.SubProject;
						// #% var GUID = SubProject.ProjectGuid; 
						 var GUID = SubProject.ProjectGuid;
						// #% var SubProjectRef = Solution.ByGuid (GUID); 
						 var SubProjectRef = Solution.ByGuid (GUID);
						// #% var SubProjectDir = Solution.ByGuid (GUID).Directory.UnixPath(); 
						 var SubProjectDir = Solution.ByGuid (GUID).Directory.UnixPath();
						// #! SubTarget #{SubProject.Target} 
						// SubTarget #{SubProject.Target}
						// #! SubProject #{SubProjectDir} 
						// SubProject #{SubProjectDir}
						// #! GUID #{GUID} 
						// GUID #{GUID}
						// #if (Project.IsExe) 
						if (  (Project.IsExe) ) {
							// #{Directory}/$(TARGETBIN)/#{Project.Target} : #{SubProjectDir}/$(TARGETBIN)/#{SubProject.Target} 
							_Output.Write ("{1}/$(TARGETBIN)/{2} : {3}/$(TARGETBIN)/{4}\n{0}", _Indent, Directory, Project.Target, SubProjectDir, SubProject.Target);
							// #elseif (Project.IsLibrary) 
							} else if (  (Project.IsLibrary)) {
							// #{Directory}/$(TARGETBIN)/#{Project.Target} : #{SubProjectDir}/$(TARGETBIN)/#{SubProject.Target} 
							_Output.Write ("{1}/$(TARGETBIN)/{2} : {3}/$(TARGETBIN)/{4}\n{0}", _Indent, Directory, Project.Target, SubProjectDir, SubProject.Target);
							// #else 
							} else {
							// #end if 
							}
						//  
						_Output.Write ("\n{0}", _Indent);
						// #end foreach 
						}
					//  
					_Output.Write ("\n{0}", _Indent);
					// #{Directory}/$(TARGETBIN)/#{Project.Target} : always 
					_Output.Write ("{1}/$(TARGETBIN)/{2} : always\n{0}", _Indent, Directory, Project.Target);
					// #{Prefix}make NORECURSE=true -C #{Directory} 
					_Output.Write ("{1}make NORECURSE=true -C {2}\n{0}", _Indent, Prefix, Directory);
					//  
					_Output.Write ("\n{0}", _Indent);
					// #end if 
					}
				// #end foreach 
				}
			//  
			_Output.Write ("\n{0}", _Indent);
			// #end method 
			}
		//  
		//  
		// #method GenerateMakefile VSProject Project 
		

		//
		// GenerateMakefile
		//
		public void GenerateMakefile (VSProject Project) {
			// ## 
			_Output.Write ("#\n{0}", _Indent);
			// ## Makefile for Visual Studio Project ... 
			_Output.Write ("# Makefile for Visual Studio Project ...\n{0}", _Indent);
			// ## 
			_Output.Write ("#\n{0}", _Indent);
			// #call Preamble Project 
			Preamble (Project);
			// #% bool HaveSoure = Project.SourceDependency.Count > 0; 
			 bool HaveSoure = Project.SourceDependency.Count > 0;
			// #% bool HaveLink = Project.LinkDependency.Count > 0; 
			 bool HaveLink = Project.LinkDependency.Count > 0;
			// #% bool HavePackage = Project.PrivateReference.Count > 0; 
			 bool HavePackage = Project.PrivateReference.Count > 0;
			//  
			_Output.Write ("\n{0}", _Indent);
			// ## The main target  
			_Output.Write ("# The main target \n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			// SourceFiles = #! 
			_Output.Write ("SourceFiles = ", _Indent);
			// #foreach (var File in Project.SourceDependency) 
			foreach  (var File in Project.SourceDependency) {
				// \ 
				_Output.Write ("\\\n{0}", _Indent);
				//      #{File.UnixFile()}#! 
				_Output.Write ("     {1}", _Indent, File.UnixFile());
				// #end foreach 
				}
			//  
			_Output.Write ("\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			// LinkFiles = #! 
			_Output.Write ("LinkFiles = ", _Indent);
			// #foreach (var File in Project.LinkDependency) 
			foreach  (var File in Project.LinkDependency) {
				// \ 
				_Output.Write ("\\\n{0}", _Indent);
				//     $(TARGETBIN)/#{File}#! 
				_Output.Write ("    $(TARGETBIN)/{1}", _Indent, File);
				// #end foreach 
				}
			//  
			_Output.Write ("\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			// LinkFilesComma = #! 
			_Output.Write ("LinkFilesComma = ", _Indent);
			// #% var LinkSep = new Separator ("", ","); 
			 var LinkSep = new Separator ("", ",");
			// #foreach (var File in Project.LinkDependency) 
			foreach  (var File in Project.LinkDependency) {
				// #{LinkSep}$(TARGETBIN)/#{File}#! 
				_Output.Write ("{1}$(TARGETBIN)/{2}", _Indent, LinkSep, File);
				// #end foreach 
				}
			//  
			_Output.Write ("\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			// NugetFiles = #! 
			_Output.Write ("NugetFiles = ", _Indent);
			// #foreach (var File in Project.PrivateReference) 
			foreach  (var File in Project.PrivateReference) {
				// \ 
				_Output.Write ("\\\n{0}", _Indent);
				//     $(TARGETBIN)/#{File}#! 
				_Output.Write ("    $(TARGETBIN)/{1}", _Indent, File);
				// #end foreach 
				}
			//  
			_Output.Write ("\n{0}", _Indent);
			// NugetFilesComa = #! 
			_Output.Write ("NugetFilesComa = ", _Indent);
			// #% var PkgSep = new Separator ("", ","); 
			 var PkgSep = new Separator ("", ",");
			// #foreach (var File in Project.PrivateReference) 
			foreach  (var File in Project.PrivateReference) {
				// #{PkgSep}\ 
				_Output.Write ("{1}\\\n{0}", _Indent, PkgSep);
				//     $(TARGETBIN)/#{File}#! 
				_Output.Write ("    $(TARGETBIN)/{1}", _Indent, File);
				// #end foreach 
				}
			//  
			_Output.Write ("\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			// ## A) Main target packaged 
			_Output.Write ("# A) Main target packaged\n{0}", _Indent);
			// #if (Project.IsExe) 
			if (  (Project.IsExe) ) {
				// $(TARGETEXE)/#{Project.AssemblyName} :| $(TARGETEXE) 
				_Output.Write ("$(TARGETEXE)/{1} :| $(TARGETEXE)\n{0}", _Indent, Project.AssemblyName);
				// $(TARGETEXE)/#{Project.AssemblyName} : $(TARGETBIN)/#{Project.AssemblyName}.exe  
				_Output.Write ("$(TARGETEXE)/{1} : $(TARGETBIN)/{2}.exe \n{0}", _Indent, Project.AssemblyName, Project.AssemblyName);
				// #{Prefix}$(BUNDLE) $@ $^ $(LinkFiles) 
				_Output.Write ("{1}$(BUNDLE) $@ $^ $(LinkFiles)\n{0}", _Indent, Prefix);
				//  
				_Output.Write ("\n{0}", _Indent);
				// ## B) Main target executable 
				_Output.Write ("# B) Main target executable\n{0}", _Indent);
				// $(TARGETBIN)/#{Project.Target} :| $(TARGETBIN) 
				_Output.Write ("$(TARGETBIN)/{1} :| $(TARGETBIN)\n{0}", _Indent, Project.Target);
				// $(TARGETBIN)/#{Project.Target} : $(SourceFiles) $(LinkFiles) $(NugetFiles) 
				_Output.Write ("$(TARGETBIN)/{1} : $(SourceFiles) $(LinkFiles) $(NugetFiles)\n{0}", _Indent, Project.Target);
				// #{Prefix}$(CSHARPEXE) /out:$@  $(SourceFiles) #! 
				_Output.Write ("{1}$(CSHARPEXE) /out:$@  $(SourceFiles) ", _Indent, Prefix);
				// #if HaveLink 
				if (  HaveLink ) {
					// -reference:$(LinkFilesComma) #! 
					_Output.Write ("-reference:$(LinkFilesComma) ", _Indent);
					// #end if 
					}
				// #if HavePackage 
				if (  HavePackage ) {
					// -pkg:$(NugetFilesComa) #! 
					_Output.Write ("-pkg:$(NugetFilesComa) ", _Indent);
					// #end if 
					}
				//  
				_Output.Write ("\n{0}", _Indent);
				//  
				_Output.Write ("\n{0}", _Indent);
				// #elseif (Project.IsLibrary) 
				} else if (  (Project.IsLibrary)) {
				//  
				_Output.Write ("\n{0}", _Indent);
				// $(TARGETBIN)/#{Project.Target} :| $(TARGETBIN) 
				_Output.Write ("$(TARGETBIN)/{1} :| $(TARGETBIN)\n{0}", _Indent, Project.Target);
				//  
				_Output.Write ("\n{0}", _Indent);
				// $(TARGETBIN)/#{Project.Target} :  $(SourceFiles) $(LinkFiles) $(NugetFiles) 
				_Output.Write ("$(TARGETBIN)/{1} :  $(SourceFiles) $(LinkFiles) $(NugetFiles)\n{0}", _Indent, Project.Target);
				// #{Prefix}$(CSHARPDLL) /out:$@  $(SourceFiles) #! 
				_Output.Write ("{1}$(CSHARPDLL) /out:$@  $(SourceFiles) ", _Indent, Prefix);
				// #if HaveLink 
				if (  HaveLink ) {
					// -reference:$(LinkFilesComma) #! 
					_Output.Write ("-reference:$(LinkFilesComma) ", _Indent);
					// #end if 
					}
				// #if HavePackage 
				if (  HavePackage ) {
					// -pkg:$(NugetFilesComa) #! 
					_Output.Write ("-pkg:$(NugetFilesComa) ", _Indent);
					// #end if 
					}
				//  
				_Output.Write ("\n{0}", _Indent);
				//  
				_Output.Write ("\n{0}", _Indent);
				// #else 
				} else {
				// #end if 
				}
			//  
			_Output.Write ("\n{0}", _Indent);
			// ## Directories etc. 
			_Output.Write ("# Directories etc.\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			// $(INSTALL_PROGRAM) : 
			_Output.Write ("$(INSTALL_PROGRAM) :\n{0}", _Indent);
			// #{Prefix}mkdir -p $(INSTALL_PROGRAM)  
			_Output.Write ("{1}mkdir -p $(INSTALL_PROGRAM) \n{0}", _Indent, Prefix);
			//  
			_Output.Write ("\n{0}", _Indent);
			// $(INSTALL_DATA) : 
			_Output.Write ("$(INSTALL_DATA) :\n{0}", _Indent);
			// #{Prefix}mkdir -p $(INSTALL_DATA)  
			_Output.Write ("{1}mkdir -p $(INSTALL_DATA) \n{0}", _Indent, Prefix);
			//  
			_Output.Write ("\n{0}", _Indent);
			// $(TARGETBIN) : 
			_Output.Write ("$(TARGETBIN) :\n{0}", _Indent);
			// #{Prefix}mkdir -p $(TARGETBIN)  
			_Output.Write ("{1}mkdir -p $(TARGETBIN) \n{0}", _Indent, Prefix);
			//  
			_Output.Write ("\n{0}", _Indent);
			// $(TARGETEXE) : 
			_Output.Write ("$(TARGETEXE) :\n{0}", _Indent);
			// #{Prefix}mkdir -p $(TARGETEXE)  
			_Output.Write ("{1}mkdir -p $(TARGETEXE) \n{0}", _Indent, Prefix);
			//  
			_Output.Write ("\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			// ## Generated code 
			_Output.Write ("# Generated code\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			// ifdef PHB_BUILD 
			_Output.Write ("ifdef PHB_BUILD\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			// #foreach (var Item in Project.None) 
			foreach  (var Item in Project.None) {
				// #if (Item.Generator != null) 
				if (  (Item.Generator != null) ) {
					// #{Item.LastGenOutput} : #{Item.Include} 
					_Output.Write ("{1} : {2}\n{0}", _Indent, Item.LastGenOutput, Item.Include);
					// #{Prefix} $(Custom_#{Item.Generator}) #{Item.Include} $(Custom_#{Item.Generator}_FLAG) #{Item.LastGenOutput}  
					_Output.Write ("{1} $(Custom_{2}) {3} $(Custom_{4}_FLAG) {5} \n{0}", _Indent, Prefix, Item.Generator, Item.Include, Item.Generator, Item.LastGenOutput);
					// #end if 
					}
				// #end foreach 
				}
			//  
			_Output.Write ("\n{0}", _Indent);
			// endif 
			_Output.Write ("endif\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			// .PHONY : clean install cross recursive 
			_Output.Write (".PHONY : clean install cross recursive\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			// ## Referenced projects 
			_Output.Write ("# Referenced projects\n{0}", _Indent);
			// ## 
			_Output.Write ("#\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			// ## Recursive make targets, do not execute if the variable NORECURSE is defined 
			_Output.Write ("# Recursive make targets, do not execute if the variable NORECURSE is defined\n{0}", _Indent);
			// ifndef NORECURSE 
			_Output.Write ("ifndef NORECURSE\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			// #foreach (var Item in Project.ProjectReference) 
			foreach  (var Item in Project.ProjectReference) {
				// #{Item.Include.UnixPath()}/$(TARGETBIN)/#{Item.SubProject.AssemblyName}.dll : recursive 
				_Output.Write ("{1}/$(TARGETBIN)/{2}.dll : recursive\n{0}", _Indent, Item.Include.UnixPath(), Item.SubProject.AssemblyName);
				// #{Prefix}$(MAKE) -C #{Item.Include.UnixPath()} 
				_Output.Write ("{1}$(MAKE) -C {2}\n{0}", _Indent, Prefix, Item.Include.UnixPath());
				//  
				_Output.Write ("\n{0}", _Indent);
				// #end foreach 
				}
			// endif 
			_Output.Write ("endif\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			// # copy libraries into the base library 
			// #foreach (var Item in Project.ProjectReference) 
			foreach  (var Item in Project.ProjectReference) {
				// $(TARGETBIN)/#{Item.SubProject.AssemblyName}.dll :| $(TARGETBIN) 
				_Output.Write ("$(TARGETBIN)/{1}.dll :| $(TARGETBIN)\n{0}", _Indent, Item.SubProject.AssemblyName);
				// $(TARGETBIN)/#{Item.SubProject.AssemblyName}.dll : #{Item.Include.UnixPath()}/$(TARGETBIN)/#{Item.SubProject.AssemblyName}.dll 
				_Output.Write ("$(TARGETBIN)/{1}.dll : {2}/$(TARGETBIN)/{3}.dll\n{0}", _Indent, Item.SubProject.AssemblyName, Item.Include.UnixPath(), Item.SubProject.AssemblyName);
				// #{Prefix} cp #{Item.Include.UnixPath()}/$(TARGETBIN)/#{Item.SubProject.AssemblyName}.dll $(TARGETBIN)/#{Item.SubProject.AssemblyName}.dll 
				_Output.Write ("{1} cp {2}/$(TARGETBIN)/{3}.dll $(TARGETBIN)/{4}.dll\n{0}", _Indent, Prefix, Item.Include.UnixPath(), Item.SubProject.AssemblyName, Item.SubProject.AssemblyName);
				//  
				_Output.Write ("\n{0}", _Indent);
				// #end foreach 
				}
			//  
			_Output.Write ("\n{0}", _Indent);
			// ## Referenced Libraries 
			_Output.Write ("# Referenced Libraries\n{0}", _Indent);
			// ## 
			_Output.Write ("#\n{0}", _Indent);
			// #foreach (var Item in Project.PrivateReference) 
			foreach  (var Item in Project.PrivateReference) {
				// $(TARGETBIN)/#{Item.Name} :| $(TARGETBIN) 
				_Output.Write ("$(TARGETBIN)/{1} :| $(TARGETBIN)\n{0}", _Indent, Item.Name);
				// $(TARGETBIN)/#{Item.Name} : #{Item.HintPath.UnixFile()} 
				_Output.Write ("$(TARGETBIN)/{1} : {2}\n{0}", _Indent, Item.Name, Item.HintPath.UnixFile());
				// #{Prefix} cp   #{Item.HintPath.UnixFile()} $(TARGETBIN)/#{Item.Name} 
				_Output.Write ("{1} cp   {2} $(TARGETBIN)/{3}\n{0}", _Indent, Prefix, Item.HintPath.UnixFile(), Item.Name);
				// #end foreach 
				}
			//  
			_Output.Write ("\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			// ## Clean up 
			_Output.Write ("# Clean up\n{0}", _Indent);
			// ## 
			_Output.Write ("#\n{0}", _Indent);
			// ## At the moment we only delete the currently indicated target. This allows a  
			_Output.Write ("# At the moment we only delete the currently indicated target. This allows a \n{0}", _Indent);
			// ## developer to do a make clean / make when a problem with a corrupted intermediate 
			_Output.Write ("# developer to do a make clean / make when a problem with a corrupted intermediate\n{0}", _Indent);
			// ## file is suspected. 
			_Output.Write ("# file is suspected.\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			// clean :  
			_Output.Write ("clean : \n{0}", _Indent);
			// #{Prefix}rm -f $(TARGETBIN)/* 
			_Output.Write ("{1}rm -f $(TARGETBIN)/*\n{0}", _Indent, Prefix);
			// #{Prefix}rm -f $(TARGETEXE)/* 
			_Output.Write ("{1}rm -f $(TARGETEXE)/*\n{0}", _Indent, Prefix);
			//  
			_Output.Write ("\n{0}", _Indent);
			// ## Install 
			_Output.Write ("# Install\n{0}", _Indent);
			// ## 
			_Output.Write ("#\n{0}", _Indent);
			// ## Install files to a tools directory. Default is ~/.local/bin 
			_Output.Write ("# Install files to a tools directory. Default is ~/.local/bin\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			// #if (Project.IsExe) 
			if (  (Project.IsExe) ) {
				// install : $(INSTALL_PROGRAM)/#{Project.AssemblyName}  
				_Output.Write ("install : $(INSTALL_PROGRAM)/{1} \n{0}", _Indent, Project.AssemblyName);
				//  
				_Output.Write ("\n{0}", _Indent);
				// $(INSTALL_PROGRAM)/#{Project.AssemblyName}  :| $(INSTALL_PROGRAM) 
				_Output.Write ("$(INSTALL_PROGRAM)/{1}  :| $(INSTALL_PROGRAM)\n{0}", _Indent, Project.AssemblyName);
				// $(INSTALL_PROGRAM)/#{Project.AssemblyName}  : $(TARGETEXE)/#{Project.AssemblyName}  
				_Output.Write ("$(INSTALL_PROGRAM)/{1}  : $(TARGETEXE)/{2} \n{0}", _Indent, Project.AssemblyName, Project.AssemblyName);
				// #{Prefix}cp $^ $@  
				_Output.Write ("{1}cp $^ $@ \n{0}", _Indent, Prefix);
				//  
				_Output.Write ("\n{0}", _Indent);
				// #elseif (Project.OutputType == "Library") 
				} else if (  (Project.OutputType == "Library")) {
				// install : $(INSTALL_DATA)/#{Project.AssemblyName}.dll 
				_Output.Write ("install : $(INSTALL_DATA)/{1}.dll\n{0}", _Indent, Project.AssemblyName);
				//  
				_Output.Write ("\n{0}", _Indent);
				// $(INSTALL_DATA)/#{Project.AssemblyName}.dll :| $(INSTALL_DATA) 
				_Output.Write ("$(INSTALL_DATA)/{1}.dll :| $(INSTALL_DATA)\n{0}", _Indent, Project.AssemblyName);
				// $(INSTALL_DATA)/#{Project.AssemblyName}.dll : $(TARGETBIN)/#{Project.AssemblyName}.dll 
				_Output.Write ("$(INSTALL_DATA)/{1}.dll : $(TARGETBIN)/{2}.dll\n{0}", _Indent, Project.AssemblyName, Project.AssemblyName);
				// #{Prefix}cp $^ $@  
				_Output.Write ("{1}cp $^ $@ \n{0}", _Indent, Prefix);
				//  
				_Output.Write ("\n{0}", _Indent);
				// #end if 
				}
			//  
			_Output.Write ("\n{0}", _Indent);
			// ## Cross 
			_Output.Write ("# Cross\n{0}", _Indent);
			// ## 
			_Output.Write ("#\n{0}", _Indent);
			// ## Cross compilation targets.  
			_Output.Write ("# Cross compilation targets. \n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			// #if (Project.IsExe) 
			if (  (Project.IsExe) ) {
				// #foreach (var Arch in Project.CrossTargets) 
				foreach  (var Arch in Project.CrossTargets) {
					// cross : $(TARGETROOT)/#{Arch}/#{Project.AssemblyName}  
					_Output.Write ("cross : $(TARGETROOT)/{1}/{2} \n{0}", _Indent, Arch, Project.AssemblyName);
					// $(TARGETROOT)/#{Arch}/#{Project.AssemblyName} : $(TARGETBIN)/#{Project.AssemblyName}.exe 
					_Output.Write ("$(TARGETROOT)/{1}/{2} : $(TARGETBIN)/{3}.exe\n{0}", _Indent, Arch, Project.AssemblyName, Project.AssemblyName);
					// #{Prefix}mkdir -p $(TARGETROOT)/#{Arch}/#{Project.AssemblyName}  
					_Output.Write ("{1}mkdir -p $(TARGETROOT)/{2}/{3} \n{0}", _Indent, Prefix, Arch, Project.AssemblyName);
					// #{Prefix}$(BUNDLE) $@ --cross ${Arch} $^ 
					_Output.Write ("{1}$(BUNDLE) $@ --cross ${{Arch}} $^\n{0}", _Indent, Prefix);
					//  
					_Output.Write ("\n{0}", _Indent);
					// #end foreach 
					}
				// #end if 
				}
			//  
			_Output.Write ("\n{0}", _Indent);
			// #end method 
			}
		//  
		// #method3 PHBTool string Tag string Tool string Flag 
		

		//
		// PHBTool
		//
		public void PHBTool (string Tag, string Tool, string Flag) {
			// export Custom_#{Tag}		?= #{Tool} 
			_Output.Write ("export Custom_{1}		?= {2}\n{0}", _Indent, Tag, Tool);
			// export Custom_#{Tag}_FLAG	?= #{Flag} 
			_Output.Write ("export Custom_{1}_FLAG	?= {2}\n{0}", _Indent, Tag, Flag);
			// #end method3 
			}
		//  
		//  
		// #end xclass 
		}
	}
