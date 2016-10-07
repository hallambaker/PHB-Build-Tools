// This file was automatically generated.
// To make this compile, I had to go to the nuget package manager and run"
// Install-Package Microsoft.VisualStudio.Shell.14.0


// Here is an article on building templates might be useful
// http://blogs.msdn.com/b/vsx/archive/2014/06/10/creating-a-vsix-deployable-project-or-item-template-with-custom-wizard-support.aspx

using System;
using System.CodeDom.Compiler;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Designer.Interfaces;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.OLE.Interop;
using VSLangProj80;
using Goedel.Registry;

using IOleServiceProvider = Microsoft.VisualStudio.OLE.Interop.IServiceProvider;

namespace Goedel.VSIX.Binding {


	// NB The name of the Guid field MUST match the VSIX package generator
	// naming convention. Otherwise it doesn't work.

	// Or maybe not, getting this thing to work is hit and miss. Sometime you just
	// have to restart the system and it all works. Problem seems to be that Visual Studio 
	// can only handle so many module loads and unloads without a reset.

    static partial class GuidList {
        public const string guidCommandCSGeneratorString = "8D382331-6F20-4C3C-A83B-7EEA39FA03AB";
        public static readonly Guid guidCommandCSGenerator = new Guid(guidCommandCSGeneratorString);
        };

    [ComVisible(true)]
    [Guid(GuidList.guidCommandCSGeneratorString)]
    [ProvideObject(typeof(CommandCS))]
    [CodeGeneratorRegistration(typeof(CommandCS), "CommandCS", 
					vsContextGuids.vsContextGuidVCSProject, GeneratesDesignTimeSource = true)]
    [CodeGeneratorRegistration(typeof(CommandCS), "CommandCS", 
					vsContextGuids.vsContextGuidVBProject, GeneratesDesignTimeSource = true)]
    public class CommandCS : IVsSingleFileGenerator, IObjectWithSite, IDisposable {
        private object site = null;
        private CodeDomProvider codeDomProvider = null;
        private ServiceProvider serviceProvider = null;

        private CodeDomProvider CodeProvider {
            get {
                if (codeDomProvider == null) {
                    IVSMDCodeDomProvider provider = (IVSMDCodeDomProvider)SiteServiceProvider.GetService(typeof(IVSMDCodeDomProvider).GUID);
                    if (provider != null)
                        codeDomProvider = (CodeDomProvider)provider.CodeDomProvider;
                    }
                return codeDomProvider;
                }
            }

        private ServiceProvider SiteServiceProvider {
            get {
                if (serviceProvider == null) {
                    IOleServiceProvider oleServiceProvider = site as IOleServiceProvider;
                    serviceProvider = new ServiceProvider(oleServiceProvider);
                    }
                return serviceProvider;
                }
            }

        #region IDisposable

        bool _disposed;

        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
            }

        ~CommandCS () {
            Dispose(false);
            }

        protected virtual void Dispose(bool disposing)  {
            if (_disposed)
                return;

            if (disposing) {
                if (serviceProvider != null) {
					serviceProvider.Dispose();
					}
                }

            // release any unmanaged objects
            // set the object references to null

            _disposed = true;
            }

        #endregion IDisposable

        #region IVsSingleFileGenerator

        public int DefaultExtension(out string pbstrDefaultExtension) {
            pbstrDefaultExtension = ".cs";
            return VSConstants.S_OK;
            }

        public int Generate(string wszInputFilePath, 
				string bstrInputFileContents, 
				string wszDefaultNamespace, 
				IntPtr[] rgbOutputFileContents, 
				out uint pcbOutput, 
				IVsGeneratorProgress pGenerateProgress) {
            if (bstrInputFileContents == null)
                throw new ArgumentException(bstrInputFileContents);

            var Reader = new StringReader(bstrInputFileContents);
            var Writer = new StringWriter();

            // Process the data
            var Parse = new global::Goedel.Tool.Command.CommandParse();
            var Schema = new Lexer(wszInputFilePath);
            Schema.Process(Reader, Parse);

            var Script = new global::Goedel.Tool.Command.GenerateCS(Writer);
            Script.Generate(Parse);

            // Convert writer data to a string and then a byte array
            var Text = Writer.ToString();
            var Data = Encoding.UTF8.GetBytes(Text);

			// Fill in the Visual Studio return buffer (this memory will be freed by VS)
            if (Data == null) {
                rgbOutputFileContents[0] = IntPtr.Zero;
                pcbOutput = 0;
                }
            else {
				var Length = Data.Length;
                rgbOutputFileContents[0] = Marshal.AllocCoTaskMem(Length);
                Marshal.Copy(Data, 0, rgbOutputFileContents[0], Length);
                pcbOutput = (uint)Length;
                }

            return VSConstants.S_OK;
            }

        #endregion IVsSingleFileGenerator

		// The IObjectWithSite interface is not currently required but might be
		// in the future if we ever get to the point where multiple file generation
		// is supported.

        #region IObjectWithSite

        public void GetSite(ref Guid riid, out IntPtr ppvSite) {
            if (site == null)
                Marshal.ThrowExceptionForHR(VSConstants.E_NOINTERFACE);

            // Query for the interface using the site object initially passed to the generator
            IntPtr punk = Marshal.GetIUnknownForObject(site);
            int hr = Marshal.QueryInterface(punk, ref riid, out ppvSite);
            Marshal.Release(punk);
            Microsoft.VisualStudio.ErrorHandler.ThrowOnFailure(hr);
            }

        public void SetSite(object pUnkSite) {
            // Save away the site object for later use
            site = pUnkSite;

            // These are initialized on demand via our private CodeProvider and SiteServiceProvider properties
            codeDomProvider = null;
            serviceProvider = null;
            }

        #endregion IObjectWithSite
        }

	// NB The name of the Guid field MUST match the VSIX package generator
	// naming convention. Otherwise it doesn't work.

	// Or maybe not, getting this thing to work is hit and miss. Sometime you just
	// have to restart the system and it all works. Problem seems to be that Visual Studio 
	// can only handle so many module loads and unloads without a reset.

    static partial class GuidList {
        public const string guidFSRCSGeneratorString = "83A2D491-F2C5-407F-B1A6-1D4FC9B4C053";
        public static readonly Guid guidFSRCSGenerator = new Guid(guidFSRCSGeneratorString);
        };

    [ComVisible(true)]
    [Guid(GuidList.guidFSRCSGeneratorString)]
    [ProvideObject(typeof(FSRCS))]
    [CodeGeneratorRegistration(typeof(FSRCS), "FSRCS", 
					vsContextGuids.vsContextGuidVCSProject, GeneratesDesignTimeSource = true)]
    [CodeGeneratorRegistration(typeof(FSRCS), "FSRCS", 
					vsContextGuids.vsContextGuidVBProject, GeneratesDesignTimeSource = true)]
    public class FSRCS : IVsSingleFileGenerator, IObjectWithSite, IDisposable {
        private object site = null;
        private CodeDomProvider codeDomProvider = null;
        private ServiceProvider serviceProvider = null;

        private CodeDomProvider CodeProvider {
            get {
                if (codeDomProvider == null) {
                    IVSMDCodeDomProvider provider = (IVSMDCodeDomProvider)SiteServiceProvider.GetService(typeof(IVSMDCodeDomProvider).GUID);
                    if (provider != null)
                        codeDomProvider = (CodeDomProvider)provider.CodeDomProvider;
                    }
                return codeDomProvider;
                }
            }

        private ServiceProvider SiteServiceProvider {
            get {
                if (serviceProvider == null) {
                    IOleServiceProvider oleServiceProvider = site as IOleServiceProvider;
                    serviceProvider = new ServiceProvider(oleServiceProvider);
                    }
                return serviceProvider;
                }
            }

        #region IDisposable

        bool _disposed;

        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
            }

        ~FSRCS () {
            Dispose(false);
            }

        protected virtual void Dispose(bool disposing)  {
            if (_disposed)
                return;

            if (disposing) {
                if (serviceProvider != null) {
					serviceProvider.Dispose();
					}
                }

            // release any unmanaged objects
            // set the object references to null

            _disposed = true;
            }

        #endregion IDisposable

        #region IVsSingleFileGenerator

        public int DefaultExtension(out string pbstrDefaultExtension) {
            pbstrDefaultExtension = ".cs";
            return VSConstants.S_OK;
            }

        public int Generate(string wszInputFilePath, 
				string bstrInputFileContents, 
				string wszDefaultNamespace, 
				IntPtr[] rgbOutputFileContents, 
				out uint pcbOutput, 
				IVsGeneratorProgress pGenerateProgress) {
            if (bstrInputFileContents == null)
                throw new ArgumentException(bstrInputFileContents);

            var Reader = new StringReader(bstrInputFileContents);
            var Writer = new StringWriter();

            // Process the data
            var Parse = new global::Goedel.Tool.FSRGen.FSRSchema();
            var Schema = new Lexer(wszInputFilePath);
            Schema.Process(Reader, Parse);

            var Script = new global::Goedel.Tool.FSRGen.Generate(Writer);
            Script.GenerateCS(Parse);

            // Convert writer data to a string and then a byte array
            var Text = Writer.ToString();
            var Data = Encoding.UTF8.GetBytes(Text);

			// Fill in the Visual Studio return buffer (this memory will be freed by VS)
            if (Data == null) {
                rgbOutputFileContents[0] = IntPtr.Zero;
                pcbOutput = 0;
                }
            else {
				var Length = Data.Length;
                rgbOutputFileContents[0] = Marshal.AllocCoTaskMem(Length);
                Marshal.Copy(Data, 0, rgbOutputFileContents[0], Length);
                pcbOutput = (uint)Length;
                }

            return VSConstants.S_OK;
            }

        #endregion IVsSingleFileGenerator

		// The IObjectWithSite interface is not currently required but might be
		// in the future if we ever get to the point where multiple file generation
		// is supported.

        #region IObjectWithSite

        public void GetSite(ref Guid riid, out IntPtr ppvSite) {
            if (site == null)
                Marshal.ThrowExceptionForHR(VSConstants.E_NOINTERFACE);

            // Query for the interface using the site object initially passed to the generator
            IntPtr punk = Marshal.GetIUnknownForObject(site);
            int hr = Marshal.QueryInterface(punk, ref riid, out ppvSite);
            Marshal.Release(punk);
            Microsoft.VisualStudio.ErrorHandler.ThrowOnFailure(hr);
            }

        public void SetSite(object pUnkSite) {
            // Save away the site object for later use
            site = pUnkSite;

            // These are initialized on demand via our private CodeProvider and SiteServiceProvider properties
            codeDomProvider = null;
            serviceProvider = null;
            }

        #endregion IObjectWithSite
        }

	// NB The name of the Guid field MUST match the VSIX package generator
	// naming convention. Otherwise it doesn't work.

	// Or maybe not, getting this thing to work is hit and miss. Sometime you just
	// have to restart the system and it all works. Problem seems to be that Visual Studio 
	// can only handle so many module loads and unloads without a reset.

    static partial class GuidList {
        public const string guidExceptionalGeneratorString = "7F47F99D-A484-4628-8AE4-0D866F7F7C43";
        public static readonly Guid guidExceptionalGenerator = new Guid(guidExceptionalGeneratorString);
        };

    [ComVisible(true)]
    [Guid(GuidList.guidExceptionalGeneratorString)]
    [ProvideObject(typeof(Exceptional))]
    [CodeGeneratorRegistration(typeof(Exceptional), "Exceptional", 
					vsContextGuids.vsContextGuidVCSProject, GeneratesDesignTimeSource = true)]
    [CodeGeneratorRegistration(typeof(Exceptional), "Exceptional", 
					vsContextGuids.vsContextGuidVBProject, GeneratesDesignTimeSource = true)]
    public class Exceptional : IVsSingleFileGenerator, IObjectWithSite, IDisposable {
        private object site = null;
        private CodeDomProvider codeDomProvider = null;
        private ServiceProvider serviceProvider = null;

        private CodeDomProvider CodeProvider {
            get {
                if (codeDomProvider == null) {
                    IVSMDCodeDomProvider provider = (IVSMDCodeDomProvider)SiteServiceProvider.GetService(typeof(IVSMDCodeDomProvider).GUID);
                    if (provider != null)
                        codeDomProvider = (CodeDomProvider)provider.CodeDomProvider;
                    }
                return codeDomProvider;
                }
            }

        private ServiceProvider SiteServiceProvider {
            get {
                if (serviceProvider == null) {
                    IOleServiceProvider oleServiceProvider = site as IOleServiceProvider;
                    serviceProvider = new ServiceProvider(oleServiceProvider);
                    }
                return serviceProvider;
                }
            }

        #region IDisposable

        bool _disposed;

        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
            }

        ~Exceptional () {
            Dispose(false);
            }

        protected virtual void Dispose(bool disposing)  {
            if (_disposed)
                return;

            if (disposing) {
                if (serviceProvider != null) {
					serviceProvider.Dispose();
					}
                }

            // release any unmanaged objects
            // set the object references to null

            _disposed = true;
            }

        #endregion IDisposable

        #region IVsSingleFileGenerator

        public int DefaultExtension(out string pbstrDefaultExtension) {
            pbstrDefaultExtension = ".cs";
            return VSConstants.S_OK;
            }

        public int Generate(string wszInputFilePath, 
				string bstrInputFileContents, 
				string wszDefaultNamespace, 
				IntPtr[] rgbOutputFileContents, 
				out uint pcbOutput, 
				IVsGeneratorProgress pGenerateProgress) {
            if (bstrInputFileContents == null)
                throw new ArgumentException(bstrInputFileContents);

            var Reader = new StringReader(bstrInputFileContents);
            var Writer = new StringWriter();

            // Process the data
            var Parse = new global::Goedel.Tool.Exceptional.Exceptions();
            var Schema = new Lexer(wszInputFilePath);
            Schema.Process(Reader, Parse);

            var Script = new global::Goedel.Tool.Exceptional.Generate(Writer);
            Script.GenerateCSX(Parse);

            // Convert writer data to a string and then a byte array
            var Text = Writer.ToString();
            var Data = Encoding.UTF8.GetBytes(Text);

			// Fill in the Visual Studio return buffer (this memory will be freed by VS)
            if (Data == null) {
                rgbOutputFileContents[0] = IntPtr.Zero;
                pcbOutput = 0;
                }
            else {
				var Length = Data.Length;
                rgbOutputFileContents[0] = Marshal.AllocCoTaskMem(Length);
                Marshal.Copy(Data, 0, rgbOutputFileContents[0], Length);
                pcbOutput = (uint)Length;
                }

            return VSConstants.S_OK;
            }

        #endregion IVsSingleFileGenerator

		// The IObjectWithSite interface is not currently required but might be
		// in the future if we ever get to the point where multiple file generation
		// is supported.

        #region IObjectWithSite

        public void GetSite(ref Guid riid, out IntPtr ppvSite) {
            if (site == null)
                Marshal.ThrowExceptionForHR(VSConstants.E_NOINTERFACE);

            // Query for the interface using the site object initially passed to the generator
            IntPtr punk = Marshal.GetIUnknownForObject(site);
            int hr = Marshal.QueryInterface(punk, ref riid, out ppvSite);
            Marshal.Release(punk);
            Microsoft.VisualStudio.ErrorHandler.ThrowOnFailure(hr);
            }

        public void SetSite(object pUnkSite) {
            // Save away the site object for later use
            site = pUnkSite;

            // These are initialized on demand via our private CodeProvider and SiteServiceProvider properties
            codeDomProvider = null;
            serviceProvider = null;
            }

        #endregion IObjectWithSite
        }

	// NB The name of the Guid field MUST match the VSIX package generator
	// naming convention. Otherwise it doesn't work.

	// Or maybe not, getting this thing to work is hit and miss. Sometime you just
	// have to restart the system and it all works. Problem seems to be that Visual Studio 
	// can only handle so many module loads and unloads without a reset.

    static partial class GuidList {
        public const string guidGScriptGeneratorString = "038C7FC8-029C-4511-8AEB-B7FC0741C463";
        public static readonly Guid guidGScriptGenerator = new Guid(guidGScriptGeneratorString);
        };

    [ComVisible(true)]
    [Guid(GuidList.guidGScriptGeneratorString)]
    [ProvideObject(typeof(GScript))]
    [CodeGeneratorRegistration(typeof(GScript), "GScript", 
					vsContextGuids.vsContextGuidVCSProject, GeneratesDesignTimeSource = true)]
    [CodeGeneratorRegistration(typeof(GScript), "GScript", 
					vsContextGuids.vsContextGuidVBProject, GeneratesDesignTimeSource = true)]
    public class GScript : IVsSingleFileGenerator, IObjectWithSite, IDisposable {
        private object site = null;
        private CodeDomProvider codeDomProvider = null;
        private ServiceProvider serviceProvider = null;

        private CodeDomProvider CodeProvider {
            get {
                if (codeDomProvider == null) {
                    IVSMDCodeDomProvider provider = (IVSMDCodeDomProvider)SiteServiceProvider.GetService(typeof(IVSMDCodeDomProvider).GUID);
                    if (provider != null)
                        codeDomProvider = (CodeDomProvider)provider.CodeDomProvider;
                    }
                return codeDomProvider;
                }
            }

        private ServiceProvider SiteServiceProvider {
            get {
                if (serviceProvider == null) {
                    IOleServiceProvider oleServiceProvider = site as IOleServiceProvider;
                    serviceProvider = new ServiceProvider(oleServiceProvider);
                    }
                return serviceProvider;
                }
            }

        #region IDisposable

        bool _disposed;

        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
            }

        ~GScript () {
            Dispose(false);
            }

        protected virtual void Dispose(bool disposing)  {
            if (_disposed)
                return;

            if (disposing) {
                if (serviceProvider != null) {
					serviceProvider.Dispose();
					}
                }

            // release any unmanaged objects
            // set the object references to null

            _disposed = true;
            }

        #endregion IDisposable

        #region IVsSingleFileGenerator

        public int DefaultExtension(out string pbstrDefaultExtension) {
            pbstrDefaultExtension = ".cs";
            return VSConstants.S_OK;
            }

        public int Generate(string wszInputFilePath, 
				string bstrInputFileContents, 
				string wszDefaultNamespace, 
				IntPtr[] rgbOutputFileContents, 
				out uint pcbOutput, 
				IVsGeneratorProgress pGenerateProgress) {
            if (bstrInputFileContents == null)
                throw new ArgumentException(bstrInputFileContents);

            var Reader = new StringReader(bstrInputFileContents);
            var Writer = new StringWriter();

            // Process the data
            var Script = new global::Goedel.Tool.Script.Script();
            Script.Process (wszInputFilePath, Reader, Writer);

            // Convert writer data to a string and then a byte array
            var Text = Writer.ToString();
            var Data = Encoding.UTF8.GetBytes(Text);

			// Fill in the Visual Studio return buffer (this memory will be freed by VS)
            if (Data == null) {
                rgbOutputFileContents[0] = IntPtr.Zero;
                pcbOutput = 0;
                }
            else {
				var Length = Data.Length;
                rgbOutputFileContents[0] = Marshal.AllocCoTaskMem(Length);
                Marshal.Copy(Data, 0, rgbOutputFileContents[0], Length);
                pcbOutput = (uint)Length;
                }

            return VSConstants.S_OK;
            }

        #endregion IVsSingleFileGenerator

		// The IObjectWithSite interface is not currently required but might be
		// in the future if we ever get to the point where multiple file generation
		// is supported.

        #region IObjectWithSite

        public void GetSite(ref Guid riid, out IntPtr ppvSite) {
            if (site == null)
                Marshal.ThrowExceptionForHR(VSConstants.E_NOINTERFACE);

            // Query for the interface using the site object initially passed to the generator
            IntPtr punk = Marshal.GetIUnknownForObject(site);
            int hr = Marshal.QueryInterface(punk, ref riid, out ppvSite);
            Marshal.Release(punk);
            Microsoft.VisualStudio.ErrorHandler.ThrowOnFailure(hr);
            }

        public void SetSite(object pUnkSite) {
            // Save away the site object for later use
            site = pUnkSite;

            // These are initialized on demand via our private CodeProvider and SiteServiceProvider properties
            codeDomProvider = null;
            serviceProvider = null;
            }

        #endregion IObjectWithSite
        }

	// NB The name of the Guid field MUST match the VSIX package generator
	// naming convention. Otherwise it doesn't work.

	// Or maybe not, getting this thing to work is hit and miss. Sometime you just
	// have to restart the system and it all works. Problem seems to be that Visual Studio 
	// can only handle so many module loads and unloads without a reset.

    static partial class GuidList {
        public const string guidGoedel3GeneratorString = "446C3603-F595-41BF-8BF3-0FA7C6895F33";
        public static readonly Guid guidGoedel3Generator = new Guid(guidGoedel3GeneratorString);
        };

    [ComVisible(true)]
    [Guid(GuidList.guidGoedel3GeneratorString)]
    [ProvideObject(typeof(Goedel3))]
    [CodeGeneratorRegistration(typeof(Goedel3), "Goedel3", 
					vsContextGuids.vsContextGuidVCSProject, GeneratesDesignTimeSource = true)]
    [CodeGeneratorRegistration(typeof(Goedel3), "Goedel3", 
					vsContextGuids.vsContextGuidVBProject, GeneratesDesignTimeSource = true)]
    public class Goedel3 : IVsSingleFileGenerator, IObjectWithSite, IDisposable {
        private object site = null;
        private CodeDomProvider codeDomProvider = null;
        private ServiceProvider serviceProvider = null;

        private CodeDomProvider CodeProvider {
            get {
                if (codeDomProvider == null) {
                    IVSMDCodeDomProvider provider = (IVSMDCodeDomProvider)SiteServiceProvider.GetService(typeof(IVSMDCodeDomProvider).GUID);
                    if (provider != null)
                        codeDomProvider = (CodeDomProvider)provider.CodeDomProvider;
                    }
                return codeDomProvider;
                }
            }

        private ServiceProvider SiteServiceProvider {
            get {
                if (serviceProvider == null) {
                    IOleServiceProvider oleServiceProvider = site as IOleServiceProvider;
                    serviceProvider = new ServiceProvider(oleServiceProvider);
                    }
                return serviceProvider;
                }
            }

        #region IDisposable

        bool _disposed;

        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
            }

        ~Goedel3 () {
            Dispose(false);
            }

        protected virtual void Dispose(bool disposing)  {
            if (_disposed)
                return;

            if (disposing) {
                if (serviceProvider != null) {
					serviceProvider.Dispose();
					}
                }

            // release any unmanaged objects
            // set the object references to null

            _disposed = true;
            }

        #endregion IDisposable

        #region IVsSingleFileGenerator

        public int DefaultExtension(out string pbstrDefaultExtension) {
            pbstrDefaultExtension = ".cs";
            return VSConstants.S_OK;
            }

        public int Generate(string wszInputFilePath, 
				string bstrInputFileContents, 
				string wszDefaultNamespace, 
				IntPtr[] rgbOutputFileContents, 
				out uint pcbOutput, 
				IVsGeneratorProgress pGenerateProgress) {
            if (bstrInputFileContents == null)
                throw new ArgumentException(bstrInputFileContents);

            var Reader = new StringReader(bstrInputFileContents);
            var Writer = new StringWriter();

            // Process the data
            var Parse = new global::GoedelSchema.Goedel();
            var Schema = new Lexer(wszInputFilePath);
            Schema.Process(Reader, Parse);

            var Script = new global::GoedelSchema.GenerateParser(Writer);
            Script.GenerateCS(Parse);

            // Convert writer data to a string and then a byte array
            var Text = Writer.ToString();
            var Data = Encoding.UTF8.GetBytes(Text);

			// Fill in the Visual Studio return buffer (this memory will be freed by VS)
            if (Data == null) {
                rgbOutputFileContents[0] = IntPtr.Zero;
                pcbOutput = 0;
                }
            else {
				var Length = Data.Length;
                rgbOutputFileContents[0] = Marshal.AllocCoTaskMem(Length);
                Marshal.Copy(Data, 0, rgbOutputFileContents[0], Length);
                pcbOutput = (uint)Length;
                }

            return VSConstants.S_OK;
            }

        #endregion IVsSingleFileGenerator

		// The IObjectWithSite interface is not currently required but might be
		// in the future if we ever get to the point where multiple file generation
		// is supported.

        #region IObjectWithSite

        public void GetSite(ref Guid riid, out IntPtr ppvSite) {
            if (site == null)
                Marshal.ThrowExceptionForHR(VSConstants.E_NOINTERFACE);

            // Query for the interface using the site object initially passed to the generator
            IntPtr punk = Marshal.GetIUnknownForObject(site);
            int hr = Marshal.QueryInterface(punk, ref riid, out ppvSite);
            Marshal.Release(punk);
            Microsoft.VisualStudio.ErrorHandler.ThrowOnFailure(hr);
            }

        public void SetSite(object pUnkSite) {
            // Save away the site object for later use
            site = pUnkSite;

            // These are initialized on demand via our private CodeProvider and SiteServiceProvider properties
            codeDomProvider = null;
            serviceProvider = null;
            }

        #endregion IObjectWithSite
        }

    }

