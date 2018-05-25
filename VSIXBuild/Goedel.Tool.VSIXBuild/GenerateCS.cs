// Script Syntax Version:  1.0

//  Copyright ©  2017 by 
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
using System;
using System.IO;
using System.Collections.Generic;
using Goedel.Registry;
namespace Goedel.Tool.VSIXBuild {
	/// <summary>A Goedel script.</summary>
	public partial class Generate : global::Goedel.Registry.Script {
		/// <summary>Default constructor.</summary>
		public Generate () : base () {
			}
		/// <summary>Constructor with output stream.</summary>
		/// <param name="Output">The output stream</param>
		public Generate (TextWriter Output) : base (Output) {
			}

		

		//
		// GenerateCS
		//
		public void GenerateCS (VSIXBuild VSIXBuild) {
			foreach  (var Toplevel in VSIXBuild.Top) {
				switch (Toplevel._Tag ()) {
					case VSIXBuildType.Namespace: {
					  Namespace Cast = (Namespace) Toplevel; 
					_Output.Write ("// This file was automatically generated.\n{0}", _Indent);
					_Output.Write ("// To make this compile, I had to go to the nuget package manager and run\"\n{0}", _Indent);
					_Output.Write ("// Install-Package Microsoft.VisualStudio.Shell.14.0\n{0}", _Indent);
					_Output.Write ("\n{0}", _Indent);
					_Output.Write ("\n{0}", _Indent);
					_Output.Write ("// Here is an article on building templates might be useful\n{0}", _Indent);
					_Output.Write ("// http://blogs.msdn.com/b/vsx/archive/2014/06/10/creating-a-vsix-deployable-project-or-item-template-with-custom-wizard-support.aspx\n{0}", _Indent);
					_Output.Write ("\n{0}", _Indent);
					_Output.Write ("using System;\n{0}", _Indent);
					_Output.Write ("using System.CodeDom.Compiler;\n{0}", _Indent);
					_Output.Write ("using System.IO;\n{0}", _Indent);
					_Output.Write ("using System.Runtime.InteropServices;\n{0}", _Indent);
					_Output.Write ("using System.Text;\n{0}", _Indent);
					_Output.Write ("using Microsoft.VisualStudio;\n{0}", _Indent);
					_Output.Write ("using Microsoft.VisualStudio.Designer.Interfaces;\n{0}", _Indent);
					_Output.Write ("using Microsoft.VisualStudio.Shell;\n{0}", _Indent);
					_Output.Write ("using Microsoft.VisualStudio.Shell.Interop;\n{0}", _Indent);
					_Output.Write ("using Microsoft.VisualStudio.OLE.Interop;\n{0}", _Indent);
					_Output.Write ("using VSLangProj80;\n{0}", _Indent);
					_Output.Write ("using Goedel.Registry;\n{0}", _Indent);
					_Output.Write ("\n{0}", _Indent);
					_Output.Write ("using IOleServiceProvider = Microsoft.VisualStudio.OLE.Interop.IServiceProvider;\n{0}", _Indent);
					_Output.Write ("\n{0}", _Indent);
					_Output.Write ("namespace {1} {{\n{0}", _Indent, Cast.Name);
					_Output.Write ("\n{0}", _Indent);
					foreach  (var Extension in Cast.Entries) {
						switch (Extension.Is._Tag ()) {
							case VSIXBuildType.Generator: { 
							
							 MakeGenerator (Extension);
						break; }
							}
						}
					_Output.Write ("\n{0}", _Indent);
					_Output.Write ("    }}\n{0}", _Indent);
					_Output.Write ("\n{0}", _Indent);
				break; }
					}
				}
			}
		

		//
		// MakeGenerator
		//
		public void MakeGenerator (Member Extension) {
			 var Generator = Extension.Is as Generator;
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("	// NB The name of the Guid field MUST match the VSIX package generator\n{0}", _Indent);
			_Output.Write ("	// naming convention. Otherwise it doesn't work.\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("	// Or maybe not, getting this thing to work is hit and miss. Sometime you just\n{0}", _Indent);
			_Output.Write ("	// have to restart the system and it all works. Problem seems to be that Visual Studio \n{0}", _Indent);
			_Output.Write ("	// can only handle so many module loads and unloads without a reset.\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("    static partial class GuidList {{\n{0}", _Indent);
			_Output.Write ("        public const string Guid{1}GeneratorString = \"{2}\";\n{0}", _Indent, Extension.Name, Generator.GUID.Value);
			_Output.Write ("        public static readonly Guid Guid{1}Generator = new Guid(Guid{2}GeneratorString);\n{0}", _Indent, Extension.Name, Extension.Name);
			_Output.Write ("        }};\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("    [ComVisible(true)]\n{0}", _Indent);
			_Output.Write ("    [Guid(GuidList.Guid{1}GeneratorString)]\n{0}", _Indent, Extension.Name);
			_Output.Write ("    [ProvideObject(typeof({1}))]\n{0}", _Indent, Extension.Name);
			foreach  (var Project in Generator.Project) {
				if (  (Project.Value.ToString() =="CSharp")  ) {
					_Output.Write ("    [CodeGeneratorRegistration(typeof({1}), \"{2}\", \n{0}", _Indent, Extension.Name, Extension.Name);
					_Output.Write ("					 \"{{FAE04EC1-301F-11D3-BF4B-00C04F79EFBC}}\", GeneratesDesignTimeSource = true)]\n{0}", _Indent);
					_Output.Write ("    [CodeGeneratorRegistration(typeof({1}), \"{2}\", \n{0}", _Indent, Extension.Name, Extension.Name);
					_Output.Write ("					\"{{9A19103F-16F7-4668-BE54-9A1E7A4F7556}}\", GeneratesDesignTimeSource = true)]\n{0}", _Indent);
					} else if (  (Project.Value.ToString() =="VisualBasic") ) {
					_Output.Write ("    [CodeGeneratorRegistration(typeof({1}), \"{2}\", \n{0}", _Indent, Extension.Name, Extension.Name);
					_Output.Write ("					\"{{164B10B9-B200-11D0-8C61-00A0C91E29D5}}\", GeneratesDesignTimeSource = true)]\n{0}", _Indent);
					} else if (  (Project.Value.ToString() =="Wix") ) {
					_Output.Write ("    [CodeGeneratorRegistration(typeof({1}), \"{2}\", \n{0}", _Indent, Extension.Name, Extension.Name);
					_Output.Write ("				    \"{{E0EE8E7D-F498-459E-9E90-2B3D73124AD5}}\", GeneratesDesignTimeSource = true)]\n{0}", _Indent);
					} else if (  (Project.Value.ToString() =="SHFB") ) {
					_Output.Write ("    [CodeGeneratorRegistration(typeof({1}), \"{2}\", \n{0}", _Indent, Extension.Name, Extension.Name);
					_Output.Write ("				    \"{{7CF6DF6D-3B04-46F8-A40B-537D21BCA0B4}}\", GeneratesDesignTimeSource = true)]\n{0}", _Indent);
					}
				}
			_Output.Write ("    public class {1} : IVsSingleFileGenerator, IObjectWithSite, IDisposable {{\n{0}", _Indent, Extension.Name);
			_Output.Write ("        private object site = null;\n{0}", _Indent);
			_Output.Write ("        private CodeDomProvider codeDomProvider = null;\n{0}", _Indent);
			_Output.Write ("        private ServiceProvider serviceProvider = null;\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("        private CodeDomProvider CodeProvider {{\n{0}", _Indent);
			_Output.Write ("            get {{\n{0}", _Indent);
			_Output.Write ("                if (codeDomProvider == null) {{\n{0}", _Indent);
			_Output.Write ("                    IVSMDCodeDomProvider provider = (IVSMDCodeDomProvider)SiteServiceProvider.GetService(typeof(IVSMDCodeDomProvider).GUID);\n{0}", _Indent);
			_Output.Write ("                    if (provider != null) {{\n{0}", _Indent);
			_Output.Write ("                        codeDomProvider = (CodeDomProvider)provider.CodeDomProvider;\n{0}", _Indent);
			_Output.Write ("						}}\n{0}", _Indent);
			_Output.Write ("                    }}\n{0}", _Indent);
			_Output.Write ("                return codeDomProvider;\n{0}", _Indent);
			_Output.Write ("                }}\n{0}", _Indent);
			_Output.Write ("            }}\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("        private ServiceProvider SiteServiceProvider {{\n{0}", _Indent);
			_Output.Write ("            get {{\n{0}", _Indent);
			_Output.Write ("                if (serviceProvider == null) {{\n{0}", _Indent);
			_Output.Write ("                    IOleServiceProvider oleServiceProvider = site as IOleServiceProvider;\n{0}", _Indent);
			_Output.Write ("                    serviceProvider = new ServiceProvider(oleServiceProvider);\n{0}", _Indent);
			_Output.Write ("                    }}\n{0}", _Indent);
			_Output.Write ("                return serviceProvider;\n{0}", _Indent);
			_Output.Write ("                }}\n{0}", _Indent);
			_Output.Write ("            }}\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("        #region IDisposable\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("        bool _disposed;\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("        public void Dispose() {{\n{0}", _Indent);
			_Output.Write ("            Dispose(true);\n{0}", _Indent);
			_Output.Write ("            GC.SuppressFinalize(this);\n{0}", _Indent);
			_Output.Write ("            }}\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("        ~{1} () {{\n{0}", _Indent, Extension.Name);
			_Output.Write ("            Dispose(false);\n{0}", _Indent);
			_Output.Write ("            }}\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("        protected virtual void Dispose(bool disposing)  {{\n{0}", _Indent);
			_Output.Write ("            if (_disposed) {{\n{0}", _Indent);
			_Output.Write ("                return;\n{0}", _Indent);
			_Output.Write ("				}}\n{0}", _Indent);
			_Output.Write ("            if (disposing) {{\n{0}", _Indent);
			_Output.Write ("                if (serviceProvider != null) {{\n{0}", _Indent);
			_Output.Write ("					serviceProvider.Dispose();\n{0}", _Indent);
			_Output.Write ("					}}\n{0}", _Indent);
			_Output.Write ("                }}\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("            // release any unmanaged objects\n{0}", _Indent);
			_Output.Write ("            // set the object references to null\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("            _disposed = true;\n{0}", _Indent);
			_Output.Write ("            }}\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("        #endregion IDisposable\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("        #region IVsSingleFileGenerator\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("        public int DefaultExtension(out string pbstrDefaultExtension) {{\n{0}", _Indent);
			_Output.Write ("            pbstrDefaultExtension = \"{1}\";\n{0}", _Indent, Generator.Extension);
			_Output.Write ("            return VSConstants.S_OK;\n{0}", _Indent);
			_Output.Write ("            }}\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("        public int Generate(string wszInputFilePath, \n{0}", _Indent);
			_Output.Write ("				string bstrInputFileContents, \n{0}", _Indent);
			_Output.Write ("				string wszDefaultNamespace, \n{0}", _Indent);
			_Output.Write ("				IntPtr[] rgbOutputFileContents, \n{0}", _Indent);
			_Output.Write ("				out uint pcbOutput, \n{0}", _Indent);
			_Output.Write ("				IVsGeneratorProgress pGenerateProgress) {{\n{0}", _Indent);
			_Output.Write ("            if (bstrInputFileContents == null) {{\n{0}", _Indent);
			_Output.Write ("                throw new ArgumentException(bstrInputFileContents);\n{0}", _Indent);
			_Output.Write ("				}}\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("            var Reader = new StringReader(bstrInputFileContents);\n{0}", _Indent);
			_Output.Write ("            var Writer = new StringWriter();\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("            // Process the data\n{0}", _Indent);
			if (  (Generator.Process.Class != null) ) {
				_Output.Write ("            var Script = new global::{1}();\n{0}", _Indent, Generator.Process.Class);
				_Output.Write ("            Script.{1} (wszInputFilePath, Reader, Writer);\n{0}", _Indent, Generator.Process.Method);
				} else if (  (Generator.Parser != null)) {
				_Output.Write ("            var Parse = new global::{1}();\n{0}", _Indent, Generator.Parser.Name);
				_Output.Write ("            var Schema = new Lexer(wszInputFilePath);\n{0}", _Indent);
				_Output.Write ("            Schema.Process(Reader, Parse);\n{0}", _Indent);
				_Output.Write ("\n{0}", _Indent);
				_Output.Write ("            var Script = new global::{1}(Writer);\n{0}", _Indent, Generator.Script.Class);
				_Output.Write ("            Script.{1}(Parse);\n{0}", _Indent, Generator.Script.Method);
				}
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("            // Convert writer data to a string and then a byte array\n{0}", _Indent);
			_Output.Write ("            var Text = Writer.ToString();\n{0}", _Indent);
			_Output.Write ("            var Data = Encoding.UTF8.GetBytes(Text);\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("			// Fill in the Visual Studio return buffer (this memory will be freed by VS)\n{0}", _Indent);
			_Output.Write ("            if (Data == null) {{\n{0}", _Indent);
			_Output.Write ("                rgbOutputFileContents[0] = IntPtr.Zero;\n{0}", _Indent);
			_Output.Write ("                pcbOutput = 0;\n{0}", _Indent);
			_Output.Write ("                }}\n{0}", _Indent);
			_Output.Write ("            else {{\n{0}", _Indent);
			_Output.Write ("				var Length = Data.Length;\n{0}", _Indent);
			_Output.Write ("                rgbOutputFileContents[0] = Marshal.AllocCoTaskMem(Length);\n{0}", _Indent);
			_Output.Write ("                Marshal.Copy(Data, 0, rgbOutputFileContents[0], Length);\n{0}", _Indent);
			_Output.Write ("                pcbOutput = (uint)Length;\n{0}", _Indent);
			_Output.Write ("                }}\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("            return VSConstants.S_OK;\n{0}", _Indent);
			_Output.Write ("            }}\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("        #endregion IVsSingleFileGenerator\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("		// The IObjectWithSite interface is not currently required but might be\n{0}", _Indent);
			_Output.Write ("		// in the future if we ever get to the point where multiple file generation\n{0}", _Indent);
			_Output.Write ("		// is supported.\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("        #region IObjectWithSite\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("        public void GetSite(ref Guid riid, out IntPtr ppvSite) {{\n{0}", _Indent);
			_Output.Write ("            if (site == null) {{\n{0}", _Indent);
			_Output.Write ("                Marshal.ThrowExceptionForHR(VSConstants.E_NOINTERFACE);\n{0}", _Indent);
			_Output.Write ("				}}\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("            // Query for the interface using the site object initially passed to the generator\n{0}", _Indent);
			_Output.Write ("            IntPtr punk = Marshal.GetIUnknownForObject(site);\n{0}", _Indent);
			_Output.Write ("            int hr = Marshal.QueryInterface(punk, ref riid, out ppvSite);\n{0}", _Indent);
			_Output.Write ("            Marshal.Release(punk);\n{0}", _Indent);
			_Output.Write ("            Microsoft.VisualStudio.ErrorHandler.ThrowOnFailure(hr);\n{0}", _Indent);
			_Output.Write ("            }}\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("        public void SetSite(object pUnkSite) {{\n{0}", _Indent);
			_Output.Write ("            // Save away the site object for later use\n{0}", _Indent);
			_Output.Write ("            site = pUnkSite;\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("            // These are initialized on demand via our private CodeProvider and SiteServiceProvider properties\n{0}", _Indent);
			_Output.Write ("            codeDomProvider = null;\n{0}", _Indent);
			_Output.Write ("            serviceProvider = null;\n{0}", _Indent);
			_Output.Write ("            }}\n{0}", _Indent);
			_Output.Write ("\n{0}", _Indent);
			_Output.Write ("        #endregion IObjectWithSite\n{0}", _Indent);
			_Output.Write ("        }}\n{0}", _Indent);
			}
		}
	}
