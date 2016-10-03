using System;
using System.IO;
using System.Collections.Generic;
//using Goedel.Registry;
//using Goedel.Utilities;

namespace Goedel.Tool.Makey {

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.microsoft.com/developer/msbuild/2003")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://schemas.microsoft.com/developer/msbuild/2003", IsNullable = false)]



    public partial class Project {

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("PropertyGroup")]
        public PropertyGroupType[] PropertyGroup { get; set; }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ItemGroup")]
        public ItemGroupType[] ItemGroup { get; set; }
        }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://schemas.microsoft.com/developer/msbuild/2003")]
    public partial class PropertyGroupType {

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("OutputType")]
        public string OutputType { get; set; }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("AssemblyName")]
        public string AssemblyName { get; set; }

        }


    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://schemas.microsoft.com/developer/msbuild/2003")]
    public partial class ItemGroupType {

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Reference")]
        public ReferenceType[] Reference { get; set; }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Compile")]
        public CompileType[] Compile { get; set; }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("None")]
        public NoneType[] None { get; set; }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ProjectReference")]
        public ProjectReferenceType[] ProjectReference { get; set; }

        }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://schemas.microsoft.com/developer/msbuild/2003")]
    public partial class ReferenceType {

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute("Include")]
        public string Include { get; set; }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("HintPath")]
        public string HintPath { get; set; }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Private")]
        public string Private { get; set; }

        public string Name {
            get { return HintPath != null ? Path.GetFileName(HintPath) : null; }
            }

        }


    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://schemas.microsoft.com/developer/msbuild/2003")]
    public partial class CompileType {

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute("Include")]
        public string Include { get; set; }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("DependentUpon")]
        public string DependentUpon { get; set; }


        }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://schemas.microsoft.com/developer/msbuild/2003")]
    public partial class NoneType {

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute("Include")]
        public string Include { get; set; }
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Generator")]
        public string Generator { get; set; }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("LastGenOutput")]
        public string LastGenOutput { get; set; }
        }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://schemas.microsoft.com/developer/msbuild/2003")]
    public partial class ProjectReferenceType {

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute("Include")]
        public string Include { get; set; }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Project")]
        public string Project { get; set; }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Name")]
        public string Name { get; set; }

        }

    }
