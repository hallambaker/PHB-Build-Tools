// This file was automatically generated.
// To make this compile, I had to go to the nuget package manager and run"
// Install-Package Microsoft.VisualStudio.Shell.14.0

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

namespace Microsoft.Demo.SimpleFileGenerator {


	// NB The name of the Guid field MUST match the VSIX package generator
	// naming convention. Otherwise it doesn't work.

    static partial class GuidList {
        public const string guidSimpleGeneratorGeneratorString = "1263af09-d434-4a54-8c86-4d4000c394ab";
        public static readonly Guid guidSimpleGeneratorGenerator = new Guid(guidSimpleGeneratorGeneratorString);
        };

    [ComVisible(true)]
    [Guid(GuidList.guidSimpleGeneratorGeneratorString)]
    [ProvideObject(typeof(SimpleGenerator))]
    [CodeGeneratorRegistration(typeof(SimpleGenerator), "SimpleGenerator", 
					vsContextGuids.vsContextGuidVCSProject, GeneratesDesignTimeSource = true)]
    [CodeGeneratorRegistration(typeof(SimpleGenerator), "SimpleGenerator", 
					vsContextGuids.vsContextGuidVBProject, GeneratesDesignTimeSource = true)]
    public class SimpleGenerator : IVsSingleFileGenerator, IObjectWithSite {
        private object site = null;
        private CodeDomProvider codeDomProvider = null;
        private ServiceProvider serviceProvider = null;

        private CodeDomProvider CodeProvider
            {
            get
                {
                if (codeDomProvider == null)
                    {
                    IVSMDCodeDomProvider provider = (IVSMDCodeDomProvider)SiteServiceProvider.GetService(typeof(IVSMDCodeDomProvider).GUID);
                    if (provider != null)
                        codeDomProvider = (CodeDomProvider)provider.CodeDomProvider;
                    }
                return codeDomProvider;
                }
            }

        private ServiceProvider SiteServiceProvider
            {
            get
                {
                if (serviceProvider == null)
                    {
                    IOleServiceProvider oleServiceProvider = site as IOleServiceProvider;
                    serviceProvider = new ServiceProvider(oleServiceProvider);
                    }
                return serviceProvider;
                }
            }

        bool _disposed;

        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
            }

        ~SimpleGenerator() {
            Dispose(false);
            }

        protected virtual void Dispose(bool disposing)  {
            if (_disposed)
                return;

            if (disposing)
                {
                SiteServiceProvider.Dispose();
                }

            // release any unmanaged objects
            // set the object references to null

            _disposed = true;
            }


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
            var Parse = new Goedel.ASN.ASN2();
            var Schema = new Lexer(wszInputFilePath);
            Schema.Process(Reader, Parse);
            var Script = new Goedel.ASN.Generate(Writer);
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

