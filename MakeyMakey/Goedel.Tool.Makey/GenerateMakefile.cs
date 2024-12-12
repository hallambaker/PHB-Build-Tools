// Script Syntax Version:  1.0

//  © 2015-2021 by Threshold Secrets LLC.
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
using  Goedel.Utilities;
using System;
using System.IO;
using System.Collections.Generic;
using Goedel.Registry;
namespace Goedel.Tool.Makey;
public partial class Generate : global::Goedel.Registry.Script {

	  public Generate (TextWriter Output) : base (Output) {}
	 string Prefix = "! "; //"\t";
	
	
	/// <summary>	
	/// Preamble
	/// </summary>
	/// <param name="options"></param>
	public void Preamble (VSFile VSFile) {
		_Output.Write ("# This file is generated automatically from the Visual Studio Project\n{0}", _Indent);
		_Output.Write ("# File. If you make changes to this file and do not update the project\n{0}", _Indent);
		_Output.Write ("# file, changes will be lost when the file is regenerated.\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("# NB: This process will fail if any of the paths have spaces in them\n{0}", _Indent);
		_Output.Write ("# While it is possible to work around the lack of support for spaces in \n{0}", _Indent);
		_Output.Write ("# file paths in gmake, it is not possible to do this reliably in the tools\n{0}", _Indent);
		_Output.Write ("# that it invokes to build the system. Rather than do half a job, it seems\n{0}", _Indent);
		_Output.Write ("# safest to simply reject the corner case\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("# The following targets are defined (well planned)\n{0}", _Indent);
		_Output.Write ("#\n{0}", _Indent);
		_Output.Write ("# make 			Compile for the current platform\n{0}", _Indent);
		_Output.Write ("# make cross		Compile for all platforms\n{0}", _Indent);
		_Output.Write ("# make install		Compile and install\n{0}", _Indent);
		_Output.Write ("# make clean		Delete all target and intermediate files\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("# The following build flags are supported\n{0}", _Indent);
		_Output.Write ("#\n{0}", _Indent);
		_Output.Write ("# make mode= release | debug			Build release or debug version\n{0}", _Indent);
		_Output.Write ("# make arch= this | all | <x>			Bundle for the current platform, all platforms\n{0}", _Indent);
		_Output.Write ("#										or the specified platform\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("# Define the default target directories (referenced projects must all follow same scheme)\n{0}", _Indent);
		_Output.Write ("#\n{0}", _Indent);
		_Output.Write ("# By default, we arrange the mono targets as follows:\n{0}", _Indent);
		_Output.Write ("#\n{0}", _Indent);
		_Output.Write ("# <Source>					The source code directory\n{0}", _Indent);
		_Output.Write ("# <Source>/mono/			Equivalent to VS bin directory\n{0}", _Indent);
		_Output.Write ("# <Source>/mono/Debug		Equivalent to VS bin/Debug directory\n{0}", _Indent);
		_Output.Write ("# <Source>/mono/Release	Equivalent to VS bin/Debug directory\n{0}", _Indent);
		_Output.Write ("#\n{0}", _Indent);
		_Output.Write ("# If the target is an executable, the following directories are also created:\n{0}", _Indent);
		_Output.Write ("#\n{0}", _Indent);
		_Output.Write ("# <Source>/This			The bundled executable for the platform the code was compiled on\n{0}", _Indent);
		_Output.Write ("# <Source>/<Arch1>			The bundled executable for the platform <Arch1>	\n{0}", _Indent);
		_Output.Write ("#\n{0}", _Indent);
		_Output.Write ("# If the install target is selected, the bundles will be installed in\n{0}", _Indent);
		_Output.Write ("#\n{0}", _Indent);
		_Output.Write ("# ~/Tools/This				The bundled executable for platform the code is built on\n{0}", _Indent);
		_Output.Write ("# ~/Tools/<Arch1>			The bundled executable for this platform <Arch1>	\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("export TARGETROOT		?= mono\n{0}", _Indent);
		_Output.Write ("export MODE				?= Release\n{0}", _Indent);
		_Output.Write ("export ARCH				?= This\n{0}", _Indent);
		_Output.Write ("export Packages			?= $(HOME)/Packages\n{0}", _Indent);
		_Output.Write ("export PackagesPath		?= /lib/net40\n{0}", _Indent);
		_Output.Write ("export Libraries		?= $(HOME)/Libraries\n{0}", _Indent);
		_Output.Write ("export LibrariesPath	?= /Mono\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("export TARGETBIN	= $(TARGETROOT)/$(MODE)\n{0}", _Indent);
		_Output.Write ("export TARGETEXE	= $(TARGETROOT)/$(ARCH)\n{0}", _Indent);
		_Output.Write ("export LIBRARYBIN	= $(Libraries)$(LibrariesPath)\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("export DESTDIR		?= $(HOME)/.local\n{0}", _Indent);
		_Output.Write ("export bindir		?= /bin\n{0}", _Indent);
		_Output.Write ("export libdir		?= /lib\n{0}", _Indent);
		_Output.Write ("export INSTALL_PROGRAM	?= $(DESTDIR)$(bindir)\n{0}", _Indent);
		_Output.Write ("export INSTALL_DATA		?= $(DESTDIR)$(libdir)\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("# Define the default compilers, linkers, packagers, etc.\n{0}", _Indent);
		_Output.Write ("export CSHARPDLL	?=  mcs /target:library\n{0}", _Indent);
		_Output.Write ("export CSHARPEXE	?=  mcs /target:exe\n{0}", _Indent);
		_Output.Write ("export BUNDLE		?=  mkbundle -L /usr/lib/mono/4.7-api --deps --static -o \n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("# The following tools are used in the goedel build system itself:\n{0}", _Indent);
		 PHBTool ("RFC2TXT",		"rfctool /in", "/txt");
		 PHBTool ("RFC2XML",		"rfctool /in", "/xml");
		 PHBTool ("RFC2MD",		"rfctool /in", "/md");
		 PHBTool ("RFC2HTML",		"rfctool /in", "/html");
		 PHBTool ("CommandCS",	"commandparse /in", "/cs");
		 PHBTool ("FSRCS",		"fsrgen /in", "/cs");
		 PHBTool ("Exceptional",	"exceptional /in", "/cs");
		 PHBTool ("GScript",		"gscript /in", "/cs");
		 PHBTool ("Goedel3",		"goedel3 /in", "/cs");
		 PHBTool ("ASN2CS",		"asn2 /in", "/cs");
		 PHBTool ("DomainerCS",	"domainer /in", "/cs");
		 PHBTool ("RegistryCS",	"registryconfig /in", "/cs");
		 PHBTool ("VSIXBuild",	"vsixbuild /in", "/cs");
		 PHBTool ("ProtoGen",		"protogen /in", "/cs");
		 PHBTool ("TrojanGTK",	"trojan /gtk", "/cs");
		_Output.Write ("\n{0}", _Indent);
		if (  (Prefix != "\t") ) {
			_Output.Write ("# Use the specified character as the prefix character. Note this may not \n{0}", _Indent);
			_Output.Write ("# be supported on versions of make other than gmake.\n{0}", _Indent);
			_Output.Write (".RECIPEPREFIX = {1}\n{0}", _Indent, Prefix);
			}
		}
	
	/// <summary>	
	/// GenerateVSMakefile
	/// </summary>
	/// <param name="options"></param>
	public void GenerateVSMakefile (VSProject Project) {
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("# Supplemental Makefile for Visual Studios Projects\n{0}", _Indent);
		_Output.Write ("#\n{0}", _Indent);
		_Output.Write ("# Visual Studio runs tools for most projects but not for shared projects.\n{0}", _Indent);
		_Output.Write ("# \n{0}", _Indent);
		_Output.Write ("# Prebuild items supported:\n{0}", _Indent);
		_Output.Write ("#   [None currently]\n{0}", _Indent);
		_Output.Write ("# \n{0}", _Indent);
		_Output.Write ("# PostBuild items supported:\n{0}", _Indent);
		_Output.Write ("#   * Copy library to locations on disk\n{0}", _Indent);
		_Output.Write ("#   * Build ilMerge executables\n{0}", _Indent);
		_Output.Write ("#   * Postprocess VSIX projects\n{0}", _Indent);
		_Output.Write ("#   * Copy executables\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write (".PHONY : all always clean install publish prebuild prebuildRecurse postbuild postbuildRecurse\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("MSBuildThisFileDirectory = \n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("LinkFiles = ", _Indent);
		foreach  (var File in Project.FixedLinkDependency) {
			_Output.Write ("\\\n{0}", _Indent);
			_Output.Write ("    {1}", _Indent, File);
			}
		foreach  (var File in Project.LinkDependency) {
			_Output.Write ("\\\n{0}", _Indent);
			_Output.Write ("    {1}", _Indent, File);
			}
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("ToolTargets = ", _Indent);
		foreach  (var File in Project.None) {
			if (  (File.BuildTool) ) {
				_Output.Write ("\\\n{0}", _Indent);
				_Output.Write ("	{1}", _Indent, File.BuildTarget);
				}
			}
		_Output.Write ("\n{0}", _Indent);
		foreach  (var File in Project.None) {
			if (  (File.BuildTool) ) {
				_Output.Write ("{1} : {2} \n{0}", _Indent, File.BuildTarget, File.BuildSource);
				_Output.Write ("	{1} {2} {3} {4}\n{0}", _Indent, File.BuildCommand, File.BuildSource, File.BuildFlag, File.BuildTarget);
				_Output.Write ("\n{0}", _Indent);
				}
			}
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("prebuildRecurse : \n{0}", _Indent);
		if (  (Project.ProjectType != ProjectType.Shared) ) {
			foreach  (var SharedProject in Project.SharedProject) {
				_Output.Write ("	cd {1} && nmake /c /f VS.make prebuild \n{0}", _Indent, SharedProject.RelativeDirectory);
				}
			}
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("postbuildRecurse :\n{0}", _Indent);
		if (  (Project.ProjectType != ProjectType.Shared) ) {
			foreach  (var SharedProject in Project.SharedProject) {
				_Output.Write ("	cd {1} && nmake /c /f VS.make postbuild \n{0}", _Indent, SharedProject.RelativeDirectory);
				}
			}
		_Output.Write ("\n{0}", _Indent);
		if (  (Project.ProjectType == ProjectType.Shared) ) {
			_Output.Write ("# Shared project, create build rules for custom tools.\n{0}", _Indent);
			_Output.Write ("prebuild : prebuildRecurse $(ToolTargets)\n{0}", _Indent);
			} else {
			_Output.Write ("# Non shared project, nothing to do\n{0}", _Indent);
			_Output.Write ("prebuild : prebuildRecurse $(ToolTargets)\n{0}", _Indent);
			//	version version.version AssemblyVersion.cs
			}
		_Output.Write ("	\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("postbuild : postbuildRecurse\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("postbuildwindows : \n{0}", _Indent);
		if (  (Project.IsExe) ) {
			_Output.Write ("	powershell publishtarget {1} $(LinkFiles) \n{0}", _Indent, Project.Target);
			} else if (  (Project.IsLibrary)) {
			_Output.Write ("	powershell publishtarget {1}\n{0}", _Indent, Project.Target);
			}
		_Output.Write ("\n{0}", _Indent);
		}
	
	/// <summary>	
	/// GenerateMakefile
	/// </summary>
	/// <param name="options"></param>
	public void GenerateMakefile (VSSolution Solution) {
		_Output.Write ("#\n{0}", _Indent);
		_Output.Write ("# Makefile for Visual Studio Solution ..\n{0}", _Indent);
		_Output.Write ("#\n{0}", _Indent);
		Preamble (Solution);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("# The main target\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write (".PHONY : all always clean install publish cross\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("# Need to identify the target directory using UnixPath()\n{0}", _Indent);
		_Output.Write ("# This file in directory {1}\n{0}", _Indent, Solution.Directory);
		_Output.Write ("\n{0}", _Indent);
		foreach  (var Item in Solution.Projects) {
			if (  (Item.MakeUnix()) ) {
				 var  Project = Item.Project;
				 var Target = Project.Target.UnixFile();
				 var Directory = Item.Directory.UnixPath();
				_Output.Write ("# Project : {1}\n{0}", _Indent, Target);
				_Output.Write ("# Item :  {1}\n{0}", _Indent, Directory);
				if (  (Directory.IndexOf(' ') >= 0) ) {
					_Output.Write ("***** Spaces in file path not supported\n{0}", _Indent);
					} else if (  (Project.IsExe)) {
					_Output.Write ("# Output :     {1}/$(TARGETEXE)/{2}\n{0}", _Indent, Directory, Target);
					} else if (  (Project.IsLibrary)) {
					_Output.Write ("# Output :     {1}/$(TARGETBIN)/{2}\n{0}", _Indent, Directory, Target);
					} else {
					}
				_Output.Write ("\n{0}", _Indent);
				_Output.Write ("all : {1}/$(TARGETBIN)/{2}\n{0}", _Indent, Directory, Project.Target);
				_Output.Write ("\n{0}", _Indent);
				foreach  (var Dep in Project.ProjectReference) {
					 var SubProject = Dep.SubProject;
					 var GUID = SubProject.ProjectGuid;
					 var SubProjectDir = SubProject.Directory.UnixCanonicalPath();
					if (  (Project.IsExe) ) {
						_Output.Write ("{1}/$(TARGETBIN)/{2} : {3}$(TARGETBIN)/{4}\n{0}", _Indent, Directory, Project.Target, SubProjectDir, SubProject.Target);
						} else if (  (Project.IsLibrary)) {
						_Output.Write ("{1}/$(TARGETBIN)/{2} : {3}$(TARGETBIN)/{4}\n{0}", _Indent, Directory, Project.Target, SubProjectDir, SubProject.Target);
						} else {
						}
					_Output.Write ("\n{0}", _Indent);
					}
				_Output.Write ("\n{0}", _Indent);
				_Output.Write ("{1}/$(TARGETBIN)/{2} : always\n{0}", _Indent, Directory, Project.Target);
				_Output.Write ("{1}echo \"\" >&2\n{0}", _Indent, Prefix);
				_Output.Write ("{1}echo \"*** Directory {2}\" >&2\n{0}", _Indent, Prefix, Directory);
				_Output.Write ("{1}make NORECURSE=true -C {2}\n{0}", _Indent, Prefix, Directory);
				_Output.Write ("\n{0}", _Indent);
				}
			}
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("# clean all projects\n{0}", _Indent);
		_Output.Write ("clean :\n{0}", _Indent);
		foreach  (var Item in Solution.Projects) {
			if (  (Item.MakeUnix()) ) {
				 var Directory = Item.Directory.UnixPath();
				_Output.Write ("{1}make clean NORECURSE=true -C {2}\n{0}", _Indent, Prefix, Directory);
				}
			}
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("# publish all projects\n{0}", _Indent);
		_Output.Write ("publish : all\n{0}", _Indent);
		foreach  (var Item in Solution.Projects) {
			if (  (Item.MakeUnix()) ) {
				 var Directory = Item.Directory.UnixPath();
				_Output.Write ("{1}make publish NORECURSE=true -C {2}\n{0}", _Indent, Prefix, Directory);
				}
			}
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("# install all projects\n{0}", _Indent);
		_Output.Write ("install : all\n{0}", _Indent);
		foreach  (var Item in Solution.Projects) {
			if (  (Item.MakeUnix()) ) {
				 var Directory = Item.Directory.UnixPath();
				_Output.Write ("{1}make install NORECURSE=true -C {2}\n{0}", _Indent, Prefix, Directory);
				}
			}
		_Output.Write ("\n{0}", _Indent);
		}
	
	/// <summary>	
	/// GenerateMakefile
	/// </summary>
	/// <param name="options"></param>
	public void GenerateMakefile (VSProject Project) {
		_Output.Write ("#\n{0}", _Indent);
		_Output.Write ("# Makefile for Visual Studio Project {1}\n{0}", _Indent, Project.AssemblyName);
		_Output.Write ("#\n{0}", _Indent);
		Preamble (Project);
		 bool HaveLink = (Project.LinkDependency.Count > 0) |  (Project.FixedLinkDependency.Count > 0);
		 bool HavePackage = Project.PrivateReference.Count > 0;
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("# The main target \n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("SourceFiles = ", _Indent);
		foreach  (var File in Project.SourceDependency) {
			_Output.Write ("\\\n{0}", _Indent);
			_Output.Write ("     {1}", _Indent, File.UnixFile());
			}
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("LinkFiles = ", _Indent);
		foreach  (var File in Project.FixedLinkDependency) {
			_Output.Write ("\\\n{0}", _Indent);
			_Output.Write ("    $(LIBRARYBIN)/{1}", _Indent, File);
			}
		foreach  (var File in Project.LinkDependency) {
			_Output.Write ("\\\n{0}", _Indent);
			_Output.Write ("    $(TARGETBIN)/{1}", _Indent, File);
			}
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("LinkFilesComma = ", _Indent);
		 var LinkSep = new Separator ("", ",");
		foreach  (var File in Project.FixedLinkDependency) {
			_Output.Write ("{1}$(LIBRARYBIN)/{2}", _Indent, LinkSep, File);
			}
		foreach  (var File in Project.LinkDependency) {
			_Output.Write ("{1}$(TARGETBIN)/{2}", _Indent, LinkSep, File);
			}
		foreach  (var File in Project.AdditionalLinkDependency) {
			_Output.Write ("{1}{2}", _Indent, LinkSep, File);
			}
		_Output.Write ("\n{0}", _Indent);
		foreach  (var Item in Project.PrivateReference) {
			_Output.Write ("#  Nuget {1} :  {2}\n{0}", _Indent, Item.Include, Item.HintPath);
			}
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("NugetFiles = ", _Indent);
		foreach  (var Item in Project.PrivateReference) {
			_Output.Write ("\\\n{0}", _Indent);
			_Output.Write ("    {1}", _Indent, Item.HintPath.UnixFile());
			}
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("NugetFilesComa = ", _Indent);
		 var PkgSep = new Separator ("", ",");
		foreach  (var Item in Project.PrivateReference) {
			_Output.Write ("{1}\\\n{0}", _Indent, PkgSep);
			_Output.Write ("    {1}", _Indent, Item.HintPath.UnixFile());
			}
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("# A) Main target packaged\n{0}", _Indent);
		if (  (Project.IsExe) ) {
			_Output.Write ("$(TARGETEXE)/{1} :| $(TARGETEXE)\n{0}", _Indent, Project.AssemblyName);
			_Output.Write ("$(TARGETEXE)/{1} : $(TARGETBIN)/{2}.exe \n{0}", _Indent, Project.AssemblyName, Project.AssemblyName);
			_Output.Write ("{1}$(BUNDLE) $@ $^ $(LinkFiles)  $(NugetFiles)\n{0}", _Indent, Prefix);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("# B) Main target executable\n{0}", _Indent);
			_Output.Write ("$(TARGETBIN)/{1} :| $(TARGETBIN)\n{0}", _Indent, Project.Target);
			_Output.Write ("$(TARGETBIN)/{1} : $(SourceFiles) $(LinkFiles) $(NugetFiles)\n{0}", _Indent, Project.Target);
			_Output.Write ("{1}$(CSHARPEXE) /out:$@  $(SourceFiles) ", _Indent, Prefix);
			if (  HaveLink ) {
				_Output.Write ("-reference:$(LinkFilesComma) ", _Indent);
				}
			if (  HavePackage ) {
				_Output.Write ("-r:$(NugetFilesComa) ", _Indent);
				}
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			} else if (  (Project.IsLibrary)) {
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("$(TARGETBIN)/{1} :| $(LIBRARYBIN)\n{0}", _Indent, Project.Target);
			_Output.Write ("$(TARGETBIN)/{1} :| $(TARGETBIN)\n{0}", _Indent, Project.Target);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("$(TARGETBIN)/{1} :  $(SourceFiles) $(LinkFiles) $(NugetFiles)\n{0}", _Indent, Project.Target);
			_Output.Write ("{1}$(CSHARPDLL) /out:$@  $(SourceFiles) ", _Indent, Prefix);
			if (  HaveLink ) {
				_Output.Write ("-reference:$(LinkFilesComma) ", _Indent);
				}
			if (  HavePackage ) {
				_Output.Write ("-r:$(NugetFilesComa) ", _Indent);
				}
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("{1}cp $(TARGETBIN)/{2} $(LIBRARYBIN)/{3}\n{0}", _Indent, Prefix, Project.Target, Project.Target);
			_Output.Write ("\n{0}", _Indent);
			} else {
			}
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("# Directories etc.\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("$(INSTALL_PROGRAM) :\n{0}", _Indent);
		_Output.Write ("{1}mkdir -p $(INSTALL_PROGRAM) \n{0}", _Indent, Prefix);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("$(INSTALL_DATA) :\n{0}", _Indent);
		_Output.Write ("{1}mkdir -p $(INSTALL_DATA) \n{0}", _Indent, Prefix);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("$(TARGETBIN) :\n{0}", _Indent);
		_Output.Write ("{1}mkdir -p $(TARGETBIN) \n{0}", _Indent, Prefix);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("$(TARGETEXE) :\n{0}", _Indent);
		_Output.Write ("{1}mkdir -p $(TARGETEXE) \n{0}", _Indent, Prefix);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("$(LIBRARYBIN) :\n{0}", _Indent);
		_Output.Write ("{1}mkdir -p $(LIBRARYBIN)\n{0}", _Indent, Prefix);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("# Generated code\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("ifdef PHB_BUILD\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		foreach  (var Item in Project.None) {
			if (  (Item.Generator != null) ) {
				_Output.Write ("{1} : {2}\n{0}", _Indent, Item.LastGenOutput, Item.Include);
				_Output.Write ("{1} $(Custom_{2}) {3} $(Custom_{4}_FLAG) {5} \n{0}", _Indent, Prefix, Item.Generator, Item.Include, Item.Generator, Item.LastGenOutput);
				}
			}
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("endif\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write (".PHONY : clean install publish debian rpm\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("# Referenced projects\n{0}", _Indent);
		_Output.Write ("#\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("# Recursive make targets, do not execute if the variable NORECURSE is defined\n{0}", _Indent);
		_Output.Write ("ifndef NORECURSE\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		foreach  (var Item in Project.ProjectReference) {
			_Output.Write ("{1}/$(TARGETBIN)/{2}.dll : recursive\n{0}", _Indent, Item.Include.UnixPath(), Item.SubProject.AssemblyName);
			_Output.Write ("{1}$(MAKE) -C {2}\n{0}", _Indent, Prefix, Item.Include.UnixPath());
			_Output.Write ("\n{0}", _Indent);
			}
		_Output.Write ("endif\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		foreach  (var Item in Project.ProjectReference) {
			_Output.Write ("$(TARGETBIN)/{1}.dll :| $(TARGETBIN)\n{0}", _Indent, Item.SubProject.AssemblyName);
			_Output.Write ("$(TARGETBIN)/{1}.dll : {2}/$(TARGETBIN)/{3}.dll\n{0}", _Indent, Item.SubProject.AssemblyName, Item.Include.UnixPath(), Item.SubProject.AssemblyName);
			_Output.Write ("{1} cp {2}/$(TARGETBIN)/{3}.dll $(TARGETBIN)/{4}.dll\n{0}", _Indent, Prefix, Item.Include.UnixPath(), Item.SubProject.AssemblyName, Item.SubProject.AssemblyName);
			_Output.Write ("\n{0}", _Indent);
			}
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("# Referenced Libraries\n{0}", _Indent);
		_Output.Write ("#\n{0}", _Indent);
		foreach  (var Item in Project.PrivateReference) {
			_Output.Write ("$(TARGETBIN)/{1} :| $(TARGETBIN)\n{0}", _Indent, Item.Name);
			_Output.Write ("$(TARGETBIN)/{1} : {2}\n{0}", _Indent, Item.Name, Item.HintPath.UnixFile());
			_Output.Write ("{1} cp   {2} $(TARGETBIN)/{3}\n{0}", _Indent, Prefix, Item.HintPath.UnixFile(), Item.Name);
			}
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("# Clean up\n{0}", _Indent);
		_Output.Write ("#\n{0}", _Indent);
		_Output.Write ("# At the moment we only delete the currently indicated target. This allows a \n{0}", _Indent);
		_Output.Write ("# developer to do a make clean / make when a problem with a corrupted intermediate\n{0}", _Indent);
		_Output.Write ("# file is suspected.\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("clean : \n{0}", _Indent);
		_Output.Write ("{1}rm -f $(TARGETBIN)/*\n{0}", _Indent, Prefix);
		_Output.Write ("{1}rm -f $(TARGETEXE)/*\n{0}", _Indent, Prefix);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("# Install\n{0}", _Indent);
		_Output.Write ("#\n{0}", _Indent);
		_Output.Write ("# Install files to a tools directory. Default is ~/.local/bin\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		if (  (Project.IsExe) ) {
			_Output.Write ("install : $(INSTALL_PROGRAM)/{1} \n{0}", _Indent, Project.AssemblyName);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("$(INSTALL_PROGRAM)/{1}  :| $(INSTALL_PROGRAM)\n{0}", _Indent, Project.AssemblyName);
			_Output.Write ("$(INSTALL_PROGRAM)/{1}  : $(TARGETEXE)/{2} \n{0}", _Indent, Project.AssemblyName, Project.AssemblyName);
			_Output.Write ("{1}cp $^ $@ \n{0}", _Indent, Prefix);
			_Output.Write ("\n{0}", _Indent);
			} else if (  (Project.OutputType == "Library")) {
			_Output.Write ("install : $(INSTALL_DATA)/{1}.dll\n{0}", _Indent, Project.AssemblyName);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("$(INSTALL_DATA)/{1}.dll :| $(INSTALL_DATA)\n{0}", _Indent, Project.AssemblyName);
			_Output.Write ("$(INSTALL_DATA)/{1}.dll : $(TARGETBIN)/{2}.dll\n{0}", _Indent, Project.AssemblyName, Project.AssemblyName);
			_Output.Write ("{1}cp $^ $@ \n{0}", _Indent, Prefix);
			_Output.Write ("\n{0}", _Indent);
			}
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("# To Do List\n{0}", _Indent);
		_Output.Write ("#\n{0}", _Indent);
		_Output.Write ("# 1) Redo install target\n{0}", _Indent);
		_Output.Write ("#    Create a wrapper script and library \n{0}", _Indent);
		_Output.Write ("# 2) Create a Debian target\n{0}", _Indent);
		_Output.Write ("# 3) Create a RPM target\n{0}", _Indent);
		_Output.Write ("# 4) Create a nuget target\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		_Output.Write ("\n{0}", _Indent);
		}
	
	/// <summary>	
	/// PHBTool
	/// </summary>
	/// <param name="options"></param>
	/// <param name="options"></param>
	/// <param name="options"></param>
	public void PHBTool (string Tag, string Tool, string Flag) {
		_Output.Write ("export Custom_{1}		?= {2}\n{0}", _Indent, Tag, Tool);
		_Output.Write ("export Custom_{1}_FLAG	?= {2}\n{0}", _Indent, Tag, Flag);
		}
	}
