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
        public const string GuidCommandCSGeneratorString = "8D382331-6F20-4C3C-A83B-7EEA39FA03AB";
        public static readonly Guid GuidCommandCSGenerator = new Guid(GuidCommandCSGeneratorString);
        };

    [ComVisible(true)]
    [Guid(GuidList.GuidCommandCSGeneratorString)]
    [ProvideObject(typeof(CommandCS))]
    [CodeGeneratorRegistration(typeof(CommandCS), "CommandCS", 
					 "{FAE04EC1-301F-11D3-BF4B-00C04F79EFBC}", GeneratesDesignTimeSource = true)]
    [CodeGeneratorRegistration(typeof(CommandCS), "CommandCS", 
					"{9A19103F-16F7-4668-BE54-9A1E7A4F7556}", GeneratesDesignTimeSource = true)]
    [CodeGeneratorRegistration(typeof(CommandCS), "CommandCS", 
					"{164B10B9-B200-11D0-8C61-00A0C91E29D5}", GeneratesDesignTimeSource = true)]
    public class CommandCS : IVsSingleFileGenerator, IObjectWithSite, IDisposable {
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

        ~CommandCS () {
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
            pbstrDefaultExtension = ".cs";
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
        public const string GuidFSRCSGeneratorString = "83A2D491-F2C5-407F-B1A6-1D4FC9B4C053";
        public static readonly Guid GuidFSRCSGenerator = new Guid(GuidFSRCSGeneratorString);
        };

    [ComVisible(true)]
    [Guid(GuidList.GuidFSRCSGeneratorString)]
    [ProvideObject(typeof(FSRCS))]
    [CodeGeneratorRegistration(typeof(FSRCS), "FSRCS", 
					 "{FAE04EC1-301F-11D3-BF4B-00C04F79EFBC}", GeneratesDesignTimeSource = true)]
    [CodeGeneratorRegistration(typeof(FSRCS), "FSRCS", 
					"{9A19103F-16F7-4668-BE54-9A1E7A4F7556}", GeneratesDesignTimeSource = true)]
    [CodeGeneratorRegistration(typeof(FSRCS), "FSRCS", 
					"{164B10B9-B200-11D0-8C61-00A0C91E29D5}", GeneratesDesignTimeSource = true)]
    public class FSRCS : IVsSingleFileGenerator, IObjectWithSite, IDisposable {
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

        ~FSRCS () {
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
            pbstrDefaultExtension = ".cs";
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
        public const string GuidExceptionalGeneratorString = "7F47F99D-A484-4628-8AE4-0D866F7F7C43";
        public static readonly Guid GuidExceptionalGenerator = new Guid(GuidExceptionalGeneratorString);
        };

    [ComVisible(true)]
    [Guid(GuidList.GuidExceptionalGeneratorString)]
    [ProvideObject(typeof(Exceptional))]
    [CodeGeneratorRegistration(typeof(Exceptional), "Exceptional", 
					 "{FAE04EC1-301F-11D3-BF4B-00C04F79EFBC}", GeneratesDesignTimeSource = true)]
    [CodeGeneratorRegistration(typeof(Exceptional), "Exceptional", 
					"{9A19103F-16F7-4668-BE54-9A1E7A4F7556}", GeneratesDesignTimeSource = true)]
    [CodeGeneratorRegistration(typeof(Exceptional), "Exceptional", 
					"{164B10B9-B200-11D0-8C61-00A0C91E29D5}", GeneratesDesignTimeSource = true)]
    public class Exceptional : IVsSingleFileGenerator, IObjectWithSite, IDisposable {
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

        ~Exceptional () {
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
            pbstrDefaultExtension = ".cs";
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
        public const string GuidGScriptGeneratorString = "038C7FC8-029C-4511-8AEB-B7FC0741C463";
        public static readonly Guid GuidGScriptGenerator = new Guid(GuidGScriptGeneratorString);
        };

    [ComVisible(true)]
    [Guid(GuidList.GuidGScriptGeneratorString)]
    [ProvideObject(typeof(GScript))]
    [CodeGeneratorRegistration(typeof(GScript), "GScript", 
					 "{FAE04EC1-301F-11D3-BF4B-00C04F79EFBC}", GeneratesDesignTimeSource = true)]
    [CodeGeneratorRegistration(typeof(GScript), "GScript", 
					"{9A19103F-16F7-4668-BE54-9A1E7A4F7556}", GeneratesDesignTimeSource = true)]
    [CodeGeneratorRegistration(typeof(GScript), "GScript", 
					"{164B10B9-B200-11D0-8C61-00A0C91E29D5}", GeneratesDesignTimeSource = true)]
    public class GScript : IVsSingleFileGenerator, IObjectWithSite, IDisposable {
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

        ~GScript () {
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
            pbstrDefaultExtension = ".cs";
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
        public const string GuidGoedel3GeneratorString = "446C3603-F595-41BF-8BF3-0FA7C6895F33";
        public static readonly Guid GuidGoedel3Generator = new Guid(GuidGoedel3GeneratorString);
        };

    [ComVisible(true)]
    [Guid(GuidList.GuidGoedel3GeneratorString)]
    [ProvideObject(typeof(Goedel3))]
    [CodeGeneratorRegistration(typeof(Goedel3), "Goedel3", 
					 "{FAE04EC1-301F-11D3-BF4B-00C04F79EFBC}", GeneratesDesignTimeSource = true)]
    [CodeGeneratorRegistration(typeof(Goedel3), "Goedel3", 
					"{9A19103F-16F7-4668-BE54-9A1E7A4F7556}", GeneratesDesignTimeSource = true)]
    [CodeGeneratorRegistration(typeof(Goedel3), "Goedel3", 
					"{164B10B9-B200-11D0-8C61-00A0C91E29D5}", GeneratesDesignTimeSource = true)]
    public class Goedel3 : IVsSingleFileGenerator, IObjectWithSite, IDisposable {
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

        ~Goedel3 () {
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
            pbstrDefaultExtension = ".cs";
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
        public const string GuidASN2CSGeneratorString = "21DBBD9C-ACCB-4E74-90C0-E5D06AA47792";
        public static readonly Guid GuidASN2CSGenerator = new Guid(GuidASN2CSGeneratorString);
        };

    [ComVisible(true)]
    [Guid(GuidList.GuidASN2CSGeneratorString)]
    [ProvideObject(typeof(ASN2CS))]
    [CodeGeneratorRegistration(typeof(ASN2CS), "ASN2CS", 
					 "{FAE04EC1-301F-11D3-BF4B-00C04F79EFBC}", GeneratesDesignTimeSource = true)]
    [CodeGeneratorRegistration(typeof(ASN2CS), "ASN2CS", 
					"{9A19103F-16F7-4668-BE54-9A1E7A4F7556}", GeneratesDesignTimeSource = true)]
    [CodeGeneratorRegistration(typeof(ASN2CS), "ASN2CS", 
					"{164B10B9-B200-11D0-8C61-00A0C91E29D5}", GeneratesDesignTimeSource = true)]
    public class ASN2CS : IVsSingleFileGenerator, IObjectWithSite, IDisposable {
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

        ~ASN2CS () {
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
            pbstrDefaultExtension = ".cs";
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
            var Parse = new global::Goedel.Tool.ASN.ASN2();
            var Schema = new Lexer(wszInputFilePath);
            Schema.Process(Reader, Parse);

            var Script = new global::Goedel.Tool.ASN.Generate(Writer);
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
        public const string GuidDomainerCSGeneratorString = "EE77623F-6882-4ECB-ABC3-70C4A740BD59";
        public static readonly Guid GuidDomainerCSGenerator = new Guid(GuidDomainerCSGeneratorString);
        };

    [ComVisible(true)]
    [Guid(GuidList.GuidDomainerCSGeneratorString)]
    [ProvideObject(typeof(DomainerCS))]
    [CodeGeneratorRegistration(typeof(DomainerCS), "DomainerCS", 
					 "{FAE04EC1-301F-11D3-BF4B-00C04F79EFBC}", GeneratesDesignTimeSource = true)]
    [CodeGeneratorRegistration(typeof(DomainerCS), "DomainerCS", 
					"{9A19103F-16F7-4668-BE54-9A1E7A4F7556}", GeneratesDesignTimeSource = true)]
    [CodeGeneratorRegistration(typeof(DomainerCS), "DomainerCS", 
					"{164B10B9-B200-11D0-8C61-00A0C91E29D5}", GeneratesDesignTimeSource = true)]
    public class DomainerCS : IVsSingleFileGenerator, IObjectWithSite, IDisposable {
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

        ~DomainerCS () {
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
            pbstrDefaultExtension = ".cs";
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
            var Parse = new global::Goedel.Tool.Domainer.Domainer();
            var Schema = new Lexer(wszInputFilePath);
            Schema.Process(Reader, Parse);

            var Script = new global::Goedel.Tool.Domainer.Generate(Writer);
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
        public const string GuidRegistryCSGeneratorString = "D8BAE8E4-14C0-4D17-B8F2-5DC8C8670894";
        public static readonly Guid GuidRegistryCSGenerator = new Guid(GuidRegistryCSGeneratorString);
        };

    [ComVisible(true)]
    [Guid(GuidList.GuidRegistryCSGeneratorString)]
    [ProvideObject(typeof(RegistryCS))]
    [CodeGeneratorRegistration(typeof(RegistryCS), "RegistryCS", 
					 "{FAE04EC1-301F-11D3-BF4B-00C04F79EFBC}", GeneratesDesignTimeSource = true)]
    [CodeGeneratorRegistration(typeof(RegistryCS), "RegistryCS", 
					"{9A19103F-16F7-4668-BE54-9A1E7A4F7556}", GeneratesDesignTimeSource = true)]
    [CodeGeneratorRegistration(typeof(RegistryCS), "RegistryCS", 
					"{164B10B9-B200-11D0-8C61-00A0C91E29D5}", GeneratesDesignTimeSource = true)]
    public class RegistryCS : IVsSingleFileGenerator, IObjectWithSite, IDisposable {
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

        ~RegistryCS () {
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
            pbstrDefaultExtension = ".cs";
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
            var Parse = new global::Goedel.Tool.RegistryConfig.ConfigItems();
            var Schema = new Lexer(wszInputFilePath);
            Schema.Process(Reader, Parse);

            var Script = new global::Goedel.Tool.RegistryConfig.GenerateCS(Writer);
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
        public const string GuidVSIXBuildGeneratorString = "B955047C-52BB-4E7F-8F5D-74A9E010591C";
        public static readonly Guid GuidVSIXBuildGenerator = new Guid(GuidVSIXBuildGeneratorString);
        };

    [ComVisible(true)]
    [Guid(GuidList.GuidVSIXBuildGeneratorString)]
    [ProvideObject(typeof(VSIXBuild))]
    [CodeGeneratorRegistration(typeof(VSIXBuild), "VSIXBuild", 
					 "{FAE04EC1-301F-11D3-BF4B-00C04F79EFBC}", GeneratesDesignTimeSource = true)]
    [CodeGeneratorRegistration(typeof(VSIXBuild), "VSIXBuild", 
					"{9A19103F-16F7-4668-BE54-9A1E7A4F7556}", GeneratesDesignTimeSource = true)]
    [CodeGeneratorRegistration(typeof(VSIXBuild), "VSIXBuild", 
					"{164B10B9-B200-11D0-8C61-00A0C91E29D5}", GeneratesDesignTimeSource = true)]
    public class VSIXBuild : IVsSingleFileGenerator, IObjectWithSite, IDisposable {
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

        ~VSIXBuild () {
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
            pbstrDefaultExtension = ".cs";
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
            var Parse = new global::Goedel.Tool.VSIXBuild.VSIXBuild();
            var Schema = new Lexer(wszInputFilePath);
            Schema.Process(Reader, Parse);

            var Script = new global::Goedel.Tool.VSIXBuild.Generate(Writer);
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
        public const string GuidProtoGenGeneratorString = "E5524E3B-6100-4D23-A047-81815BCFEE27";
        public static readonly Guid GuidProtoGenGenerator = new Guid(GuidProtoGenGeneratorString);
        };

    [ComVisible(true)]
    [Guid(GuidList.GuidProtoGenGeneratorString)]
    [ProvideObject(typeof(ProtoGen))]
    [CodeGeneratorRegistration(typeof(ProtoGen), "ProtoGen", 
					 "{FAE04EC1-301F-11D3-BF4B-00C04F79EFBC}", GeneratesDesignTimeSource = true)]
    [CodeGeneratorRegistration(typeof(ProtoGen), "ProtoGen", 
					"{9A19103F-16F7-4668-BE54-9A1E7A4F7556}", GeneratesDesignTimeSource = true)]
    [CodeGeneratorRegistration(typeof(ProtoGen), "ProtoGen", 
					"{164B10B9-B200-11D0-8C61-00A0C91E29D5}", GeneratesDesignTimeSource = true)]
    public class ProtoGen : IVsSingleFileGenerator, IObjectWithSite, IDisposable {
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

        ~ProtoGen () {
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
            pbstrDefaultExtension = ".cs";
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
            var Parse = new global::Goedel.Tool.ProtoGen.ProtoStruct();
            var Schema = new Lexer(wszInputFilePath);
            Schema.Process(Reader, Parse);

            var Script = new global::Goedel.Tool.ProtoGen.Generate(Writer);
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
        public const string GuidRFC2TXTGeneratorString = "548687B2-6C67-4F1B-86AA-343F7FD0F429";
        public static readonly Guid GuidRFC2TXTGenerator = new Guid(GuidRFC2TXTGeneratorString);
        };

    [ComVisible(true)]
    [Guid(GuidList.GuidRFC2TXTGeneratorString)]
    [ProvideObject(typeof(RFC2TXT))]
    [CodeGeneratorRegistration(typeof(RFC2TXT), "RFC2TXT", 
					 "{FAE04EC1-301F-11D3-BF4B-00C04F79EFBC}", GeneratesDesignTimeSource = true)]
    [CodeGeneratorRegistration(typeof(RFC2TXT), "RFC2TXT", 
					"{9A19103F-16F7-4668-BE54-9A1E7A4F7556}", GeneratesDesignTimeSource = true)]
    [CodeGeneratorRegistration(typeof(RFC2TXT), "RFC2TXT", 
					"{164B10B9-B200-11D0-8C61-00A0C91E29D5}", GeneratesDesignTimeSource = true)]
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
					 "{FAE04EC1-301F-11D3-BF4B-00C04F79EFBC}", GeneratesDesignTimeSource = true)]
    [CodeGeneratorRegistration(typeof(RFC2XML), "RFC2XML", 
					"{9A19103F-16F7-4668-BE54-9A1E7A4F7556}", GeneratesDesignTimeSource = true)]
    [CodeGeneratorRegistration(typeof(RFC2XML), "RFC2XML", 
					"{164B10B9-B200-11D0-8C61-00A0C91E29D5}", GeneratesDesignTimeSource = true)]
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
					 "{FAE04EC1-301F-11D3-BF4B-00C04F79EFBC}", GeneratesDesignTimeSource = true)]
    [CodeGeneratorRegistration(typeof(RFC2MD), "RFC2MD", 
					"{9A19103F-16F7-4668-BE54-9A1E7A4F7556}", GeneratesDesignTimeSource = true)]
    [CodeGeneratorRegistration(typeof(RFC2MD), "RFC2MD", 
					"{164B10B9-B200-11D0-8C61-00A0C91E29D5}", GeneratesDesignTimeSource = true)]
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
					 "{FAE04EC1-301F-11D3-BF4B-00C04F79EFBC}", GeneratesDesignTimeSource = true)]
    [CodeGeneratorRegistration(typeof(RFC2HTML), "RFC2HTML", 
					"{9A19103F-16F7-4668-BE54-9A1E7A4F7556}", GeneratesDesignTimeSource = true)]
    [CodeGeneratorRegistration(typeof(RFC2HTML), "RFC2HTML", 
					"{164B10B9-B200-11D0-8C61-00A0C91E29D5}", GeneratesDesignTimeSource = true)]
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
					 "{FAE04EC1-301F-11D3-BF4B-00C04F79EFBC}", GeneratesDesignTimeSource = true)]
    [CodeGeneratorRegistration(typeof(MD2AML), "MD2AML", 
					"{9A19103F-16F7-4668-BE54-9A1E7A4F7556}", GeneratesDesignTimeSource = true)]
    [CodeGeneratorRegistration(typeof(MD2AML), "MD2AML", 
					"{164B10B9-B200-11D0-8C61-00A0C91E29D5}", GeneratesDesignTimeSource = true)]
    [CodeGeneratorRegistration(typeof(MD2AML), "MD2AML", 
				    "{7CF6DF6D-3B04-46F8-A40B-537D21BCA0B4}", GeneratesDesignTimeSource = true)]
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

