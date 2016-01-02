// #script 1.0 
// Script Syntax Version:  1.0
// #license MITLicense 

//  Copyright ©  2011 by Default Deny Security Inc.
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
// #pclass Goedel.VSIXBuild Generate 
using System;
using System.IO;
using System.Collections.Generic;
using Goedel.Registry;
namespace Goedel.VSIXBuild {
	public partial class Generate : global::Goedel.Registry.Script {
		public Generate () : base () {
			}
		public Generate (TextWriter Output) : base (Output) {
			}

		// #% DateTime GenerateTime = DateTime.UtcNow; 
		 DateTime GenerateTime = DateTime.UtcNow;
		//  
		// #method GenerateCS VSIXBuild VSIXBuild 
		

		//
		// GenerateCS
		//
		public void GenerateCS (VSIXBuild VSIXBuild) {
			// #foreach (var Toplevel in VSIXBuild.Top) 
			foreach  (var Toplevel in VSIXBuild.Top) {
				// #switchcast VSIXBuildType Toplevel 
				switch (Toplevel._Tag ()) {
					// #casecast Namespace Cast 
					case VSIXBuildType.Namespace: {
					  Namespace Cast = (Namespace) Toplevel; 
					// // This file was automatically generated. 
					_Output.Write ("// This file was automatically generated.\n{0}", _Indent);
					// // To make this compile, I had to go to the nuget package manager and run" 
					_Output.Write ("// To make this compile, I had to go to the nuget package manager and run\"\n{0}", _Indent);
					// // Install-Package Microsoft.VisualStudio.Shell.14.0 
					_Output.Write ("// Install-Package Microsoft.VisualStudio.Shell.14.0\n{0}", _Indent);
					//  
					_Output.Write ("\n{0}", _Indent);
					// using System; 
					_Output.Write ("using System;\n{0}", _Indent);
					// using System.CodeDom.Compiler; 
					_Output.Write ("using System.CodeDom.Compiler;\n{0}", _Indent);
					// using System.IO; 
					_Output.Write ("using System.IO;\n{0}", _Indent);
					// using System.Runtime.InteropServices; 
					_Output.Write ("using System.Runtime.InteropServices;\n{0}", _Indent);
					// using System.Text; 
					_Output.Write ("using System.Text;\n{0}", _Indent);
					// using Microsoft.VisualStudio; 
					_Output.Write ("using Microsoft.VisualStudio;\n{0}", _Indent);
					// using Microsoft.VisualStudio.Designer.Interfaces; 
					_Output.Write ("using Microsoft.VisualStudio.Designer.Interfaces;\n{0}", _Indent);
					// using Microsoft.VisualStudio.Shell; 
					_Output.Write ("using Microsoft.VisualStudio.Shell;\n{0}", _Indent);
					// using Microsoft.VisualStudio.Shell.Interop; 
					_Output.Write ("using Microsoft.VisualStudio.Shell.Interop;\n{0}", _Indent);
					// using Microsoft.VisualStudio.OLE.Interop; 
					_Output.Write ("using Microsoft.VisualStudio.OLE.Interop;\n{0}", _Indent);
					// using VSLangProj80; 
					_Output.Write ("using VSLangProj80;\n{0}", _Indent);
					// using Goedel.Registry; 
					_Output.Write ("using Goedel.Registry;\n{0}", _Indent);
					//  
					_Output.Write ("\n{0}", _Indent);
					// using IOleServiceProvider = Microsoft.VisualStudio.OLE.Interop.IServiceProvider; 
					_Output.Write ("using IOleServiceProvider = Microsoft.VisualStudio.OLE.Interop.IServiceProvider;\n{0}", _Indent);
					//  
					_Output.Write ("\n{0}", _Indent);
					// namespace #{Cast.Name} { 
					_Output.Write ("namespace {1} {{\n{0}", _Indent, Cast.Name);
					//  
					_Output.Write ("\n{0}", _Indent);
					// #foreach (var Extension in Cast.Entries) 
					foreach  (var Extension in Cast.Entries) {
						// #switchcast VSIXBuildType Extension.Is 
						switch (Extension.Is._Tag ()) {
							// #casecast Generator xCast 
							case VSIXBuildType.Generator: {
							  Generator xCast = (Generator) Extension.Is; 
							// #% MakeGenerator (Extension); 
							
							 MakeGenerator (Extension);
							// #end switchcast 
						break; }
							}
						// #end foreach 
						}
					//  
					_Output.Write ("\n{0}", _Indent);
					//     } 
					_Output.Write ("    }}\n{0}", _Indent);
					//  
					_Output.Write ("\n{0}", _Indent);
					// #end switchcast 
				break; }
					}
				// #end foreach 
				}
			// #end method 
			}
		//  
		//  
		// #method MakeGenerator Member Extension 
		

		//
		// MakeGenerator
		//
		public void MakeGenerator (Member Extension) {
			// #% var Generator = Extension.Is as Generator; 
			 var Generator = Extension.Is as Generator;
			//  
			_Output.Write ("\n{0}", _Indent);
			// 	// NB The name of the Guid field MUST match the VSIX package generator 
			_Output.Write ("	// NB The name of the Guid field MUST match the VSIX package generator\n{0}", _Indent);
			// 	// naming convention. Otherwise it doesn't work. 
			_Output.Write ("	// naming convention. Otherwise it doesn't work.\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			// 	// Or maybe not, getting this thing to work is hit and miss. Sometime you just 
			_Output.Write ("	// Or maybe not, getting this thing to work is hit and miss. Sometime you just\n{0}", _Indent);
			// 	// have to restart the system and it all works. Problem seems to be that Visual Studio  
			_Output.Write ("	// have to restart the system and it all works. Problem seems to be that Visual Studio \n{0}", _Indent);
			// 	// can only handle so many module loads and unloads without a reset. 
			_Output.Write ("	// can only handle so many module loads and unloads without a reset.\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			//     static partial class GuidList { 
			_Output.Write ("    static partial class GuidList {{\n{0}", _Indent);
			//         public const string guid#{Extension.Name}GeneratorString = "#{Generator.GUID.Value}"; 
			_Output.Write ("        public const string guid{1}GeneratorString = \"{2}\";\n{0}", _Indent, Extension.Name, Generator.GUID.Value);
			//         public static readonly Guid guid#{Extension.Name}Generator = new Guid(guid#{Extension.Name}GeneratorString); 
			_Output.Write ("        public static readonly Guid guid{1}Generator = new Guid(guid{2}GeneratorString);\n{0}", _Indent, Extension.Name, Extension.Name);
			//         }; 
			_Output.Write ("        }};\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			//     [ComVisible(true)] 
			_Output.Write ("    [ComVisible(true)]\n{0}", _Indent);
			//     [Guid(GuidList.guid#{Extension.Name}GeneratorString)] 
			_Output.Write ("    [Guid(GuidList.guid{1}GeneratorString)]\n{0}", _Indent, Extension.Name);
			//     [ProvideObject(typeof(#{Extension.Name}))] 
			_Output.Write ("    [ProvideObject(typeof({1}))]\n{0}", _Indent, Extension.Name);
			// #foreach (var Project in Generator.Project) 
			foreach  (var Project in Generator.Project) {
				// #if (Project.Value.ToString() =="CSharp")  
				if (  (Project.Value.ToString() =="CSharp")  ) {
					//     [CodeGeneratorRegistration(typeof(#{Extension.Name}), "#{Extension.Name}",  
					_Output.Write ("    [CodeGeneratorRegistration(typeof({1}), \"{2}\", \n{0}", _Indent, Extension.Name, Extension.Name);
					// 					vsContextGuids.vsContextGuidVCSProject, GeneratesDesignTimeSource = true)] 
					_Output.Write ("					vsContextGuids.vsContextGuidVCSProject, GeneratesDesignTimeSource = true)]\n{0}", _Indent);
					// #elseif (Project.Value.ToString() =="VisualBasic")  
					} else if (  (Project.Value.ToString() =="VisualBasic") ) {
					//     [CodeGeneratorRegistration(typeof(#{Extension.Name}), "#{Extension.Name}",  
					_Output.Write ("    [CodeGeneratorRegistration(typeof({1}), \"{2}\", \n{0}", _Indent, Extension.Name, Extension.Name);
					// 					vsContextGuids.vsContextGuidVBProject, GeneratesDesignTimeSource = true)] 
					_Output.Write ("					vsContextGuids.vsContextGuidVBProject, GeneratesDesignTimeSource = true)]\n{0}", _Indent);
					// #end if 
					}
				// #end foreach 
				}
			//     public class #{Extension.Name} : IVsSingleFileGenerator, IObjectWithSite, IDisposable { 
			_Output.Write ("    public class {1} : IVsSingleFileGenerator, IObjectWithSite, IDisposable {{\n{0}", _Indent, Extension.Name);
			//         private object site = null; 
			_Output.Write ("        private object site = null;\n{0}", _Indent);
			//         private CodeDomProvider codeDomProvider = null; 
			_Output.Write ("        private CodeDomProvider codeDomProvider = null;\n{0}", _Indent);
			//         private ServiceProvider serviceProvider = null; 
			_Output.Write ("        private ServiceProvider serviceProvider = null;\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			//         private CodeDomProvider CodeProvider { 
			_Output.Write ("        private CodeDomProvider CodeProvider {{\n{0}", _Indent);
			//             get { 
			_Output.Write ("            get {{\n{0}", _Indent);
			//                 if (codeDomProvider == null) { 
			_Output.Write ("                if (codeDomProvider == null) {{\n{0}", _Indent);
			//                     IVSMDCodeDomProvider provider = (IVSMDCodeDomProvider)SiteServiceProvider.GetService(typeof(IVSMDCodeDomProvider).GUID); 
			_Output.Write ("                    IVSMDCodeDomProvider provider = (IVSMDCodeDomProvider)SiteServiceProvider.GetService(typeof(IVSMDCodeDomProvider).GUID);\n{0}", _Indent);
			//                     if (provider != null) 
			_Output.Write ("                    if (provider != null)\n{0}", _Indent);
			//                         codeDomProvider = (CodeDomProvider)provider.CodeDomProvider; 
			_Output.Write ("                        codeDomProvider = (CodeDomProvider)provider.CodeDomProvider;\n{0}", _Indent);
			//                     } 
			_Output.Write ("                    }}\n{0}", _Indent);
			//                 return codeDomProvider; 
			_Output.Write ("                return codeDomProvider;\n{0}", _Indent);
			//                 } 
			_Output.Write ("                }}\n{0}", _Indent);
			//             } 
			_Output.Write ("            }}\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			//         private ServiceProvider SiteServiceProvider { 
			_Output.Write ("        private ServiceProvider SiteServiceProvider {{\n{0}", _Indent);
			//             get { 
			_Output.Write ("            get {{\n{0}", _Indent);
			//                 if (serviceProvider == null) { 
			_Output.Write ("                if (serviceProvider == null) {{\n{0}", _Indent);
			//                     IOleServiceProvider oleServiceProvider = site as IOleServiceProvider; 
			_Output.Write ("                    IOleServiceProvider oleServiceProvider = site as IOleServiceProvider;\n{0}", _Indent);
			//                     serviceProvider = new ServiceProvider(oleServiceProvider); 
			_Output.Write ("                    serviceProvider = new ServiceProvider(oleServiceProvider);\n{0}", _Indent);
			//                     } 
			_Output.Write ("                    }}\n{0}", _Indent);
			//                 return serviceProvider; 
			_Output.Write ("                return serviceProvider;\n{0}", _Indent);
			//                 } 
			_Output.Write ("                }}\n{0}", _Indent);
			//             } 
			_Output.Write ("            }}\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			//         ##region IDisposable 
			_Output.Write ("        #region IDisposable\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			//         bool _disposed; 
			_Output.Write ("        bool _disposed;\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			//         public void Dispose() { 
			_Output.Write ("        public void Dispose() {{\n{0}", _Indent);
			//             Dispose(true); 
			_Output.Write ("            Dispose(true);\n{0}", _Indent);
			//             GC.SuppressFinalize(this); 
			_Output.Write ("            GC.SuppressFinalize(this);\n{0}", _Indent);
			//             } 
			_Output.Write ("            }}\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			//         ~#{Extension.Name} () { 
			_Output.Write ("        ~{1} () {{\n{0}", _Indent, Extension.Name);
			//             Dispose(false); 
			_Output.Write ("            Dispose(false);\n{0}", _Indent);
			//             } 
			_Output.Write ("            }}\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			//         protected virtual void Dispose(bool disposing)  { 
			_Output.Write ("        protected virtual void Dispose(bool disposing)  {{\n{0}", _Indent);
			//             if (_disposed) 
			_Output.Write ("            if (_disposed)\n{0}", _Indent);
			//                 return; 
			_Output.Write ("                return;\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			//             if (disposing) { 
			_Output.Write ("            if (disposing) {{\n{0}", _Indent);
			//                 if (serviceProvider != null) { 
			_Output.Write ("                if (serviceProvider != null) {{\n{0}", _Indent);
			// 					serviceProvider.Dispose(); 
			_Output.Write ("					serviceProvider.Dispose();\n{0}", _Indent);
			// 					} 
			_Output.Write ("					}}\n{0}", _Indent);
			//                 } 
			_Output.Write ("                }}\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			//             // release any unmanaged objects 
			_Output.Write ("            // release any unmanaged objects\n{0}", _Indent);
			//             // set the object references to null 
			_Output.Write ("            // set the object references to null\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			//             _disposed = true; 
			_Output.Write ("            _disposed = true;\n{0}", _Indent);
			//             } 
			_Output.Write ("            }}\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			//         ##endregion IDisposable 
			_Output.Write ("        #endregion IDisposable\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			//         ##region IVsSingleFileGenerator 
			_Output.Write ("        #region IVsSingleFileGenerator\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			//         public int DefaultExtension(out string pbstrDefaultExtension) { 
			_Output.Write ("        public int DefaultExtension(out string pbstrDefaultExtension) {{\n{0}", _Indent);
			//             pbstrDefaultExtension = "." + CodeProvider.FileExtension; 
			_Output.Write ("            pbstrDefaultExtension = \".\" + CodeProvider.FileExtension;\n{0}", _Indent);
			//             return VSConstants.S_OK; 
			_Output.Write ("            return VSConstants.S_OK;\n{0}", _Indent);
			//             } 
			_Output.Write ("            }}\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			//         public int Generate(string wszInputFilePath,  
			_Output.Write ("        public int Generate(string wszInputFilePath, \n{0}", _Indent);
			// 				string bstrInputFileContents,  
			_Output.Write ("				string bstrInputFileContents, \n{0}", _Indent);
			// 				string wszDefaultNamespace,  
			_Output.Write ("				string wszDefaultNamespace, \n{0}", _Indent);
			// 				IntPtr[] rgbOutputFileContents,  
			_Output.Write ("				IntPtr[] rgbOutputFileContents, \n{0}", _Indent);
			// 				out uint pcbOutput,  
			_Output.Write ("				out uint pcbOutput, \n{0}", _Indent);
			// 				IVsGeneratorProgress pGenerateProgress) { 
			_Output.Write ("				IVsGeneratorProgress pGenerateProgress) {{\n{0}", _Indent);
			//             if (bstrInputFileContents == null) 
			_Output.Write ("            if (bstrInputFileContents == null)\n{0}", _Indent);
			//                 throw new ArgumentException(bstrInputFileContents); 
			_Output.Write ("                throw new ArgumentException(bstrInputFileContents);\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			//             var Reader = new StringReader(bstrInputFileContents); 
			_Output.Write ("            var Reader = new StringReader(bstrInputFileContents);\n{0}", _Indent);
			//             var Writer = new StringWriter(); 
			_Output.Write ("            var Writer = new StringWriter();\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			//             // Process the data 
			_Output.Write ("            // Process the data\n{0}", _Indent);
			// #if (Generator.Process.Class != null) 
			if (  (Generator.Process.Class != null) ) {
				//             var Script = new global::#{Generator.Process.Class}(); 
				_Output.Write ("            var Script = new global::{1}();\n{0}", _Indent, Generator.Process.Class);
				//             Script.#{Generator.Process.Method} (wszInputFilePath, Reader, Writer); 
				_Output.Write ("            Script.{1} (wszInputFilePath, Reader, Writer);\n{0}", _Indent, Generator.Process.Method);
				// #elseif (Generator.Parser != null) 
				} else if (  (Generator.Parser != null)) {
				//             var Parse = new global::#{Generator.Parser.Name}(); 
				_Output.Write ("            var Parse = new global::{1}();\n{0}", _Indent, Generator.Parser.Name);
				//             var Schema = new Lexer(wszInputFilePath); 
				_Output.Write ("            var Schema = new Lexer(wszInputFilePath);\n{0}", _Indent);
				//             Schema.Process(Reader, Parse); 
				_Output.Write ("            Schema.Process(Reader, Parse);\n{0}", _Indent);
				//  
				_Output.Write ("\n{0}", _Indent);
				//             var Script = new global::#{Generator.Script.Class}(Writer); 
				_Output.Write ("            var Script = new global::{1}(Writer);\n{0}", _Indent, Generator.Script.Class);
				//             Script.#{Generator.Script.Method}(Parse); 
				_Output.Write ("            Script.{1}(Parse);\n{0}", _Indent, Generator.Script.Method);
				// #end if 
				}
			//  
			_Output.Write ("\n{0}", _Indent);
			//             // Convert writer data to a string and then a byte array 
			_Output.Write ("            // Convert writer data to a string and then a byte array\n{0}", _Indent);
			//             var Text = Writer.ToString(); 
			_Output.Write ("            var Text = Writer.ToString();\n{0}", _Indent);
			//             var Data = Encoding.UTF8.GetBytes(Text); 
			_Output.Write ("            var Data = Encoding.UTF8.GetBytes(Text);\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			// 			// Fill in the Visual Studio return buffer (this memory will be freed by VS) 
			_Output.Write ("			// Fill in the Visual Studio return buffer (this memory will be freed by VS)\n{0}", _Indent);
			//             if (Data == null) { 
			_Output.Write ("            if (Data == null) {{\n{0}", _Indent);
			//                 rgbOutputFileContents[0] = IntPtr.Zero; 
			_Output.Write ("                rgbOutputFileContents[0] = IntPtr.Zero;\n{0}", _Indent);
			//                 pcbOutput = 0; 
			_Output.Write ("                pcbOutput = 0;\n{0}", _Indent);
			//                 } 
			_Output.Write ("                }}\n{0}", _Indent);
			//             else { 
			_Output.Write ("            else {{\n{0}", _Indent);
			// 				var Length = Data.Length; 
			_Output.Write ("				var Length = Data.Length;\n{0}", _Indent);
			//                 rgbOutputFileContents[0] = Marshal.AllocCoTaskMem(Length); 
			_Output.Write ("                rgbOutputFileContents[0] = Marshal.AllocCoTaskMem(Length);\n{0}", _Indent);
			//                 Marshal.Copy(Data, 0, rgbOutputFileContents[0], Length); 
			_Output.Write ("                Marshal.Copy(Data, 0, rgbOutputFileContents[0], Length);\n{0}", _Indent);
			//                 pcbOutput = (uint)Length; 
			_Output.Write ("                pcbOutput = (uint)Length;\n{0}", _Indent);
			//                 } 
			_Output.Write ("                }}\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			//             return VSConstants.S_OK; 
			_Output.Write ("            return VSConstants.S_OK;\n{0}", _Indent);
			//             } 
			_Output.Write ("            }}\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			//         ##endregion IVsSingleFileGenerator 
			_Output.Write ("        #endregion IVsSingleFileGenerator\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			// 		// The IObjectWithSite interface is not currently required but might be 
			_Output.Write ("		// The IObjectWithSite interface is not currently required but might be\n{0}", _Indent);
			// 		// in the future if we ever get to the point where multiple file generation 
			_Output.Write ("		// in the future if we ever get to the point where multiple file generation\n{0}", _Indent);
			// 		// is supported. 
			_Output.Write ("		// is supported.\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			//         ##region IObjectWithSite 
			_Output.Write ("        #region IObjectWithSite\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			//         public void GetSite(ref Guid riid, out IntPtr ppvSite) { 
			_Output.Write ("        public void GetSite(ref Guid riid, out IntPtr ppvSite) {{\n{0}", _Indent);
			//             if (site == null) 
			_Output.Write ("            if (site == null)\n{0}", _Indent);
			//                 Marshal.ThrowExceptionForHR(VSConstants.E_NOINTERFACE); 
			_Output.Write ("                Marshal.ThrowExceptionForHR(VSConstants.E_NOINTERFACE);\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			//             // Query for the interface using the site object initially passed to the generator 
			_Output.Write ("            // Query for the interface using the site object initially passed to the generator\n{0}", _Indent);
			//             IntPtr punk = Marshal.GetIUnknownForObject(site); 
			_Output.Write ("            IntPtr punk = Marshal.GetIUnknownForObject(site);\n{0}", _Indent);
			//             int hr = Marshal.QueryInterface(punk, ref riid, out ppvSite); 
			_Output.Write ("            int hr = Marshal.QueryInterface(punk, ref riid, out ppvSite);\n{0}", _Indent);
			//             Marshal.Release(punk); 
			_Output.Write ("            Marshal.Release(punk);\n{0}", _Indent);
			//             Microsoft.VisualStudio.ErrorHandler.ThrowOnFailure(hr); 
			_Output.Write ("            Microsoft.VisualStudio.ErrorHandler.ThrowOnFailure(hr);\n{0}", _Indent);
			//             } 
			_Output.Write ("            }}\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			//         public void SetSite(object pUnkSite) { 
			_Output.Write ("        public void SetSite(object pUnkSite) {{\n{0}", _Indent);
			//             // Save away the site object for later use 
			_Output.Write ("            // Save away the site object for later use\n{0}", _Indent);
			//             site = pUnkSite; 
			_Output.Write ("            site = pUnkSite;\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			//             // These are initialized on demand via our private CodeProvider and SiteServiceProvider properties 
			_Output.Write ("            // These are initialized on demand via our private CodeProvider and SiteServiceProvider properties\n{0}", _Indent);
			//             codeDomProvider = null; 
			_Output.Write ("            codeDomProvider = null;\n{0}", _Indent);
			//             serviceProvider = null; 
			_Output.Write ("            serviceProvider = null;\n{0}", _Indent);
			//             } 
			_Output.Write ("            }}\n{0}", _Indent);
			//  
			_Output.Write ("\n{0}", _Indent);
			//         ##endregion IObjectWithSite 
			_Output.Write ("        #endregion IObjectWithSite\n{0}", _Indent);
			//         } 
			_Output.Write ("        }}\n{0}", _Indent);
			// #end method 
			}
		//  
		// #end pclass 
		}
	}
