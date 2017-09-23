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

namespace Goedel.VSIX.Binding.RFC {


	// NB The name of the Guid field MUST match the VSIX package generator
	// naming convention. Otherwise it doesn't work.

	// Or maybe not, getting this thing to work is hit and miss. Sometime you just
	// have to restart the system and it all works. Problem seems to be that Visual Studio 
	// can only handle so many module loads and unloads without a reset.

    static partial class GuidList {
        public const string GuidRFC2TXTGeneratorString = "548687B2-6C67-4F1B-86AA-343F7FD0F429";
        public static readonly Guid GuidRFC2TXTGenerator = new Guid(GuidRFC2TXTGeneratorString);
        };

    [ComVisible(true)]
    [Guid(GuidList.GuidRFC2TXTGeneratorString)]
    [ProvideObject(typeof(RFC2TXT))]
    [CodeGeneratorRegistration(typeof(RFC2TXT), "RFC2TXT", 
					vsContextGuids.vsContextGuidVCSProject, GeneratesDesignTimeSource = true)]
    [CodeGeneratorRegistration(typeof(RFC2TXT), "RFC2TXT", 
					"9A19103F-16F7-4668-BE54-9A1E7A4F7556", GeneratesDesignTimeSource = true)]
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
                    if (provider != null) {
                        codeDomProvider = (CodeDomProvider)provider.CodeDomProvider;
						}
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
            if (_disposed) {
                return;
				}
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
            pbstrDefaultExtension = ".txt";
            return VSConstants.S_OK;
            }

        public int Generate(string wszInputFilePath, 
				string bstrInputFileContents, 
				string wszDefaultNamespace, 
				IntPtr[] rgbOutputFileContents, 
				out uint pcbOutput, 
				IVsGeneratorProgress pGenerateProgress) {
            if (bstrInputFileContents == null) {
                throw new ArgumentException(bstrInputFileContents);
				}

            var Reader = new StringReader(bstrInputFileContents);
            var Writer = new StringWriter();

            // Process the data
            var Script = new global::Goedel.Document.RFCToolBinding.BindingRFC();
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
            if (site == null) {
                Marshal.ThrowExceptionForHR(VSConstants.E_NOINTERFACE);
				}

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
        public const string GuidRFC2XMLGeneratorString = "8DACB384-6ABA-40E9-83FB-3DD57A4BA597";
        public static readonly Guid GuidRFC2XMLGenerator = new Guid(GuidRFC2XMLGeneratorString);
        };

    [ComVisible(true)]
    [Guid(GuidList.GuidRFC2XMLGeneratorString)]
    [ProvideObject(typeof(RFC2XML))]
    [CodeGeneratorRegistration(typeof(RFC2XML), "RFC2XML", 
					vsContextGuids.vsContextGuidVCSProject, GeneratesDesignTimeSource = true)]
    [CodeGeneratorRegistration(typeof(RFC2XML), "RFC2XML", 
					"9A19103F-16F7-4668-BE54-9A1E7A4F7556", GeneratesDesignTimeSource = true)]
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
                    if (provider != null) {
                        codeDomProvider = (CodeDomProvider)provider.CodeDomProvider;
						}
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
            if (_disposed) {
                return;
				}
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
            pbstrDefaultExtension = ".xml";
            return VSConstants.S_OK;
            }

        public int Generate(string wszInputFilePath, 
				string bstrInputFileContents, 
				string wszDefaultNamespace, 
				IntPtr[] rgbOutputFileContents, 
				out uint pcbOutput, 
				IVsGeneratorProgress pGenerateProgress) {
            if (bstrInputFileContents == null) {
                throw new ArgumentException(bstrInputFileContents);
				}

            var Reader = new StringReader(bstrInputFileContents);
            var Writer = new StringWriter();

            // Process the data
            var Script = new global::Goedel.Document.RFCToolBinding.BindingRFC();
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
            if (site == null) {
                Marshal.ThrowExceptionForHR(VSConstants.E_NOINTERFACE);
				}

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
        public const string GuidRFC2MDGeneratorString = "F2CBE1D3-642A-4348-9CE4-844F11FCDA28";
        public static readonly Guid GuidRFC2MDGenerator = new Guid(GuidRFC2MDGeneratorString);
        };

    [ComVisible(true)]
    [Guid(GuidList.GuidRFC2MDGeneratorString)]
    [ProvideObject(typeof(RFC2MD))]
    [CodeGeneratorRegistration(typeof(RFC2MD), "RFC2MD", 
					vsContextGuids.vsContextGuidVCSProject, GeneratesDesignTimeSource = true)]
    [CodeGeneratorRegistration(typeof(RFC2MD), "RFC2MD", 
					"9A19103F-16F7-4668-BE54-9A1E7A4F7556", GeneratesDesignTimeSource = true)]
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
                    if (provider != null) {
                        codeDomProvider = (CodeDomProvider)provider.CodeDomProvider;
						}
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
            if (_disposed) {
                return;
				}
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
            pbstrDefaultExtension = ".md";
            return VSConstants.S_OK;
            }

        public int Generate(string wszInputFilePath, 
				string bstrInputFileContents, 
				string wszDefaultNamespace, 
				IntPtr[] rgbOutputFileContents, 
				out uint pcbOutput, 
				IVsGeneratorProgress pGenerateProgress) {
            if (bstrInputFileContents == null) {
                throw new ArgumentException(bstrInputFileContents);
				}

            var Reader = new StringReader(bstrInputFileContents);
            var Writer = new StringWriter();

            // Process the data
            var Script = new global::Goedel.Document.RFCToolBinding.BindingRFC();
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
            if (site == null) {
                Marshal.ThrowExceptionForHR(VSConstants.E_NOINTERFACE);
				}

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
        public const string GuidRFC2HTMLGeneratorString = "EF6FDFE2-6947-4A14-AD41-77C062FC0E67";
        public static readonly Guid GuidRFC2HTMLGenerator = new Guid(GuidRFC2HTMLGeneratorString);
        };

    [ComVisible(true)]
    [Guid(GuidList.GuidRFC2HTMLGeneratorString)]
    [ProvideObject(typeof(RFC2HTML))]
    [CodeGeneratorRegistration(typeof(RFC2HTML), "RFC2HTML", 
					vsContextGuids.vsContextGuidVCSProject, GeneratesDesignTimeSource = true)]
    [CodeGeneratorRegistration(typeof(RFC2HTML), "RFC2HTML", 
					"9A19103F-16F7-4668-BE54-9A1E7A4F7556", GeneratesDesignTimeSource = true)]
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
                    if (provider != null) {
                        codeDomProvider = (CodeDomProvider)provider.CodeDomProvider;
						}
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
            if (_disposed) {
                return;
				}
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
            pbstrDefaultExtension = ".html";
            return VSConstants.S_OK;
            }

        public int Generate(string wszInputFilePath, 
				string bstrInputFileContents, 
				string wszDefaultNamespace, 
				IntPtr[] rgbOutputFileContents, 
				out uint pcbOutput, 
				IVsGeneratorProgress pGenerateProgress) {
            if (bstrInputFileContents == null) {
                throw new ArgumentException(bstrInputFileContents);
				}

            var Reader = new StringReader(bstrInputFileContents);
            var Writer = new StringWriter();

            // Process the data
            var Script = new global::Goedel.Document.RFCToolBinding.BindingRFC();
            Script.Process2AML (wszInputFilePath, Reader, Writer);

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
            if (site == null) {
                Marshal.ThrowExceptionForHR(VSConstants.E_NOINTERFACE);
				}

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
        public const string GuidMD2AMLGeneratorString = "F2E41CD6-A03C-4C86-85E5-06C18F2E9595";
        public static readonly Guid GuidMD2AMLGenerator = new Guid(GuidMD2AMLGeneratorString);
        };

    [ComVisible(true)]
    [Guid(GuidList.GuidMD2AMLGeneratorString)]
    [ProvideObject(typeof(MD2AML))]
    [CodeGeneratorRegistration(typeof(MD2AML), "MD2AML", 
					vsContextGuids.vsContextGuidVCSProject, GeneratesDesignTimeSource = true)]
    [CodeGeneratorRegistration(typeof(MD2AML), "MD2AML", 
					"9A19103F-16F7-4668-BE54-9A1E7A4F7556", GeneratesDesignTimeSource = true)]
    [CodeGeneratorRegistration(typeof(MD2AML), "MD2AML", 
					vsContextGuids.vsContextGuidVBProject, GeneratesDesignTimeSource = true)]
    [CodeGeneratorRegistration(typeof(MD2AML), "MD2AML", 
				    "7CF6DF6D-3B04-46F8-A40B-537D21BCA0B4", GeneratesDesignTimeSource = true)]
    public class MD2AML : IVsSingleFileGenerator, IObjectWithSite, IDisposable {
        private object site = null;
        private CodeDomProvider codeDomProvider = null;
        private ServiceProvider serviceProvider = null;

        private CodeDomProvider CodeProvider {
            get {
                if (codeDomProvider == null) {
                    IVSMDCodeDomProvider provider = (IVSMDCodeDomProvider)SiteServiceProvider.GetService(typeof(IVSMDCodeDomProvider).GUID);
                    if (provider != null) {
                        codeDomProvider = (CodeDomProvider)provider.CodeDomProvider;
						}
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

        ~MD2AML () {
            Dispose(false);
            }

        protected virtual void Dispose(bool disposing)  {
            if (_disposed) {
                return;
				}
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
            pbstrDefaultExtension = ".aml";
            return VSConstants.S_OK;
            }

        public int Generate(string wszInputFilePath, 
				string bstrInputFileContents, 
				string wszDefaultNamespace, 
				IntPtr[] rgbOutputFileContents, 
				out uint pcbOutput, 
				IVsGeneratorProgress pGenerateProgress) {
            if (bstrInputFileContents == null) {
                throw new ArgumentException(bstrInputFileContents);
				}

            var Reader = new StringReader(bstrInputFileContents);
            var Writer = new StringWriter();

            // Process the data
            var Script = new global::Goedel.Document.RFCToolBinding.BindingRFC();
            Script.Process2AML (wszInputFilePath, Reader, Writer);

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
            if (site == null) {
                Marshal.ThrowExceptionForHR(VSConstants.E_NOINTERFACE);
				}

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

