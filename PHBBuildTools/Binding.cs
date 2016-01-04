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

namespace Goedel.ASN.VSIX {


	// NB The name of the Guid field MUST match the VSIX package generator
	// naming convention. Otherwise it doesn't work.

	// Or maybe not, getting this thing to work is hit and miss. Sometime you just
	// have to restart the system and it all works. Problem seems to be that Visual Studio 
	// can only handle so many module loads and unloads without a reset.

    static partial class GuidList {
        public const string guidASN2CSGeneratorString = "21DBBD9C-ACCB-4E74-90C0-E5D06AA47792";
        public static readonly Guid guidASN2CSGenerator = new Guid(guidASN2CSGeneratorString);
        };

    [ComVisible(true)]
    [Guid(GuidList.guidASN2CSGeneratorString)]
    [ProvideObject(typeof(ASN2CS))]
    [CodeGeneratorRegistration(typeof(ASN2CS), "ASN2CS", 
					vsContextGuids.vsContextGuidVCSProject, GeneratesDesignTimeSource = true)]
    [CodeGeneratorRegistration(typeof(ASN2CS), "ASN2CS", 
					vsContextGuids.vsContextGuidVBProject, GeneratesDesignTimeSource = true)]
    public class ASN2CS : IVsSingleFileGenerator, IObjectWithSite, IDisposable {
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

        ~ASN2CS () {
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
            pbstrDefaultExtension = "." + CodeProvider.FileExtension;
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
            var Parse = new global::Goedel.ASN.ASN2();
            var Schema = new Lexer(wszInputFilePath);
            Schema.Process(Reader, Parse);

            var Script = new global::Goedel.ASN.Generate(Writer);
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
            pbstrDefaultExtension = "." + CodeProvider.FileExtension;
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
            var Parse = new global::FSRGen.FSRStruct();
            var Schema = new Lexer(wszInputFilePath);
            Schema.Process(Reader, Parse);

            var Script = new global::FSRGen.Generate(Writer);
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
        public const string guidRegistryCSGeneratorString = "D8BAE8E4-14C0-4D17-B8F2-5DC8C8670894";
        public static readonly Guid guidRegistryCSGenerator = new Guid(guidRegistryCSGeneratorString);
        };

    [ComVisible(true)]
    [Guid(GuidList.guidRegistryCSGeneratorString)]
    [ProvideObject(typeof(RegistryCS))]
    [CodeGeneratorRegistration(typeof(RegistryCS), "RegistryCS", 
					vsContextGuids.vsContextGuidVCSProject, GeneratesDesignTimeSource = true)]
    [CodeGeneratorRegistration(typeof(RegistryCS), "RegistryCS", 
					vsContextGuids.vsContextGuidVBProject, GeneratesDesignTimeSource = true)]
    public class RegistryCS : IVsSingleFileGenerator, IObjectWithSite, IDisposable {
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

        ~RegistryCS () {
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
            pbstrDefaultExtension = "." + CodeProvider.FileExtension;
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
            var Parse = new global::RegistryConfig.ConfigItems();
            var Schema = new Lexer(wszInputFilePath);
            Schema.Process(Reader, Parse);

            var Script = new global::RegistryConfig.GenerateCS(Writer);
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
        public const string guidVSIXBuildGeneratorString = "B955047C-52BB-4E7F-8F5D-74A9E010591C";
        public static readonly Guid guidVSIXBuildGenerator = new Guid(guidVSIXBuildGeneratorString);
        };

    [ComVisible(true)]
    [Guid(GuidList.guidVSIXBuildGeneratorString)]
    [ProvideObject(typeof(VSIXBuild))]
    [CodeGeneratorRegistration(typeof(VSIXBuild), "VSIXBuild", 
					vsContextGuids.vsContextGuidVCSProject, GeneratesDesignTimeSource = true)]
    [CodeGeneratorRegistration(typeof(VSIXBuild), "VSIXBuild", 
					vsContextGuids.vsContextGuidVBProject, GeneratesDesignTimeSource = true)]
    public class VSIXBuild : IVsSingleFileGenerator, IObjectWithSite, IDisposable {
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

        ~VSIXBuild () {
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
            pbstrDefaultExtension = "." + CodeProvider.FileExtension;
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
            var Parse = new global::Goedel.VSIXBuild.VSIXBuild();
            var Schema = new Lexer(wszInputFilePath);
            Schema.Process(Reader, Parse);

            var Script = new global::Goedel.VSIXBuild.Generate(Writer);
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
            pbstrDefaultExtension = "." + CodeProvider.FileExtension;
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
            var Parse = new global::CommandP.CommandParse();
            var Schema = new Lexer(wszInputFilePath);
            Schema.Process(Reader, Parse);

            var Script = new global::CommandP.GenerateCS(Writer);
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
            pbstrDefaultExtension = "." + CodeProvider.FileExtension;
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
            var Parse = new global::Exceptional.Exceptions();
            var Schema = new Lexer(wszInputFilePath);
            Schema.Process(Reader, Parse);

            var Script = new global::Exceptional.Generate(Writer);
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
            pbstrDefaultExtension = "." + CodeProvider.FileExtension;
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
            var Script = new global::Goedel.Script.Script();
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
        public const string guidProtoGenGeneratorString = "E5524E3B-6100-4D23-A047-81815BCFEE27";
        public static readonly Guid guidProtoGenGenerator = new Guid(guidProtoGenGeneratorString);
        };

    [ComVisible(true)]
    [Guid(GuidList.guidProtoGenGeneratorString)]
    [ProvideObject(typeof(ProtoGen))]
    [CodeGeneratorRegistration(typeof(ProtoGen), "ProtoGen", 
					vsContextGuids.vsContextGuidVCSProject, GeneratesDesignTimeSource = true)]
    [CodeGeneratorRegistration(typeof(ProtoGen), "ProtoGen", 
					vsContextGuids.vsContextGuidVBProject, GeneratesDesignTimeSource = true)]
    public class ProtoGen : IVsSingleFileGenerator, IObjectWithSite, IDisposable {
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

        ~ProtoGen () {
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
            pbstrDefaultExtension = "." + CodeProvider.FileExtension;
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
            var Parse = new global::ProtoGen.ProtoStruct();
            var Schema = new Lexer(wszInputFilePath);
            Schema.Process(Reader, Parse);

            var Script = new global::ProtoGen.Generate(Writer);
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
            pbstrDefaultExtension = "." + CodeProvider.FileExtension;
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

	// NB The name of the Guid field MUST match the VSIX package generator
	// naming convention. Otherwise it doesn't work.

	// Or maybe not, getting this thing to work is hit and miss. Sometime you just
	// have to restart the system and it all works. Problem seems to be that Visual Studio 
	// can only handle so many module loads and unloads without a reset.

    static partial class GuidList {
        public const string guidRFC2TXTGeneratorString = "548687B2-6C67-4F1B-86AA-343F7FD0F429";
        public static readonly Guid guidRFC2TXTGenerator = new Guid(guidRFC2TXTGeneratorString);
        };

    [ComVisible(true)]
    [Guid(GuidList.guidRFC2TXTGeneratorString)]
    [ProvideObject(typeof(RFC2TXT))]
    [CodeGeneratorRegistration(typeof(RFC2TXT), "RFC2TXT", 
					vsContextGuids.vsContextGuidVCSProject, GeneratesDesignTimeSource = true)]
    [CodeGeneratorRegistration(typeof(RFC2TXT), "RFC2TXT", 
					vsContextGuids.vsContextGuidVBProject, GeneratesDesignTimeSource = true)]
    public class RFC2TXT : IVsSingleFileGenerator, IObjectWithSite, IDisposable {
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

        ~RFC2TXT () {
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
            pbstrDefaultExtension = "." + CodeProvider.FileExtension;
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
            var Script = new global::RFCTool.BindingRFC();
            Script.Process2TXT (wszInputFilePath, Reader, Writer);

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
        public const string guidRFC2XMLGeneratorString = "8DACB384-6ABA-40E9-83FB-3DD57A4BA597";
        public static readonly Guid guidRFC2XMLGenerator = new Guid(guidRFC2XMLGeneratorString);
        };

    [ComVisible(true)]
    [Guid(GuidList.guidRFC2XMLGeneratorString)]
    [ProvideObject(typeof(RFC2XML))]
    [CodeGeneratorRegistration(typeof(RFC2XML), "RFC2XML", 
					vsContextGuids.vsContextGuidVCSProject, GeneratesDesignTimeSource = true)]
    [CodeGeneratorRegistration(typeof(RFC2XML), "RFC2XML", 
					vsContextGuids.vsContextGuidVBProject, GeneratesDesignTimeSource = true)]
    public class RFC2XML : IVsSingleFileGenerator, IObjectWithSite, IDisposable {
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

        ~RFC2XML () {
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
            pbstrDefaultExtension = "." + CodeProvider.FileExtension;
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
            var Script = new global::RFCTool.BindingRFC();
            Script.Process2XML (wszInputFilePath, Reader, Writer);

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
        public const string guidRFC2MDGeneratorString = "F2CBE1D3-642A-4348-9CE4-844F11FCDA28";
        public static readonly Guid guidRFC2MDGenerator = new Guid(guidRFC2MDGeneratorString);
        };

    [ComVisible(true)]
    [Guid(GuidList.guidRFC2MDGeneratorString)]
    [ProvideObject(typeof(RFC2MD))]
    [CodeGeneratorRegistration(typeof(RFC2MD), "RFC2MD", 
					vsContextGuids.vsContextGuidVCSProject, GeneratesDesignTimeSource = true)]
    [CodeGeneratorRegistration(typeof(RFC2MD), "RFC2MD", 
					vsContextGuids.vsContextGuidVBProject, GeneratesDesignTimeSource = true)]
    public class RFC2MD : IVsSingleFileGenerator, IObjectWithSite, IDisposable {
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

        ~RFC2MD () {
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
            pbstrDefaultExtension = "." + CodeProvider.FileExtension;
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
            var Script = new global::RFCTool.BindingRFC();
            Script.Process2MD (wszInputFilePath, Reader, Writer);

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
        public const string guidRFC2HTMLGeneratorString = "EF6FDFE2-6947-4A14-AD41-77C062FC0E67";
        public static readonly Guid guidRFC2HTMLGenerator = new Guid(guidRFC2HTMLGeneratorString);
        };

    [ComVisible(true)]
    [Guid(GuidList.guidRFC2HTMLGeneratorString)]
    [ProvideObject(typeof(RFC2HTML))]
    [CodeGeneratorRegistration(typeof(RFC2HTML), "RFC2HTML", 
					vsContextGuids.vsContextGuidVCSProject, GeneratesDesignTimeSource = true)]
    [CodeGeneratorRegistration(typeof(RFC2HTML), "RFC2HTML", 
					vsContextGuids.vsContextGuidVBProject, GeneratesDesignTimeSource = true)]
    public class RFC2HTML : IVsSingleFileGenerator, IObjectWithSite, IDisposable {
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

        ~RFC2HTML () {
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
            pbstrDefaultExtension = "." + CodeProvider.FileExtension;
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
            var Script = new global::RFCTool.BindingRFC();
            Script.Process2HTML (wszInputFilePath, Reader, Writer);

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

