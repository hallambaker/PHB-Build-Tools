// To make this work, I had to go to the nuget package manager and run"
// Install-Package Microsoft.VisualStudio.Shell.14.0

using System;
using System.CodeDom.Compiler;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Designer.Interfaces;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.OLE.Interop;
using VSLangProj80;

using IOleServiceProvider = Microsoft.VisualStudio.OLE.Interop.IServiceProvider;

namespace Microsoft.Demo.SimpleFileGenerator {
    [ComVisible(true)]
    [Guid(GuidList.guidSimpleFileGeneratorString)]
    [ProvideObject(typeof(SimpleGenerator))]
    [CodeGeneratorRegistration(typeof(SimpleGenerator), "SimpleGenerator", vsContextGuids.vsContextGuidVCSProject, GeneratesDesignTimeSource = true)]
    [CodeGeneratorRegistration(typeof(SimpleGenerator), "SimpleGenerator", vsContextGuids.vsContextGuidVBProject, GeneratesDesignTimeSource = true)]
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

        #region IVsSingleFileGenerator

        public int DefaultExtension(out string pbstrDefaultExtension)
            {
            pbstrDefaultExtension = "." + CodeProvider.FileExtension;
            return VSConstants.S_OK;
            }

        public int Generate(string wszInputFilePath, string bstrInputFileContents, string wszDefaultNamespace, IntPtr[] rgbOutputFileContents, out uint pcbOutput, IVsGeneratorProgress pGenerateProgress)
            {
            if (bstrInputFileContents == null)
                throw new ArgumentException(bstrInputFileContents);

            // generate our comment string based on the programming language used
            string comment = string.Empty;
            if (CodeProvider.FileExtension == "cs")
                comment = "// " + "SimpleGenerator invoked on : " + DateTime.Now.ToString();
            if (CodeProvider.FileExtension == "vb")
                comment = "' " + "SimpleGenerator invoked on: " + DateTime.Now.ToString();
            byte[] bytes = Encoding.UTF8.GetBytes(comment);

            if (bytes == null)
                {
                rgbOutputFileContents[0] = IntPtr.Zero;
                pcbOutput = 0;
                }
            else
                {
                rgbOutputFileContents[0] = Marshal.AllocCoTaskMem(bytes.Length);
                Marshal.Copy(bytes, 0, rgbOutputFileContents[0], bytes.Length);
                pcbOutput = (uint)bytes.Length;
                }

            return VSConstants.S_OK;
            }

        #endregion IVsSingleFileGenerator

        #region IObjectWithSite

        public void GetSite(ref Guid riid, out IntPtr ppvSite)
            {
            if (site == null)
                Marshal.ThrowExceptionForHR(VSConstants.E_NOINTERFACE);

            // Query for the interface using the site object initially passed to the generator
            IntPtr punk = Marshal.GetIUnknownForObject(site);
            int hr = Marshal.QueryInterface(punk, ref riid, out ppvSite);
            Marshal.Release(punk);
            Microsoft.VisualStudio.ErrorHandler.ThrowOnFailure(hr);
            }

        public void SetSite(object pUnkSite)
            {
            // Save away the site object for later use
            site = pUnkSite;

            // These are initialized on demand via our private CodeProvider and SiteServiceProvider properties
            codeDomProvider = null;
            serviceProvider = null;
            }

        #endregion IObjectWithSite
        }
    }
