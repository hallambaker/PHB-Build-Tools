using System.ComponentModel.Composition;
using System.Windows.Media;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace Goedel.ToolScript.Classifier {
    internal static class ScriptClassificationDefinitions {
        #region Content Type and File Extension Definitions

        [Export]
        [Name("script")]
        [BaseDefinition("text")]
        internal static ContentTypeDefinition scriptContentTypeDefinition = null;

        [Export]
        [FileExtension(".script")]
        [ContentType("script")]
        internal static FileExtensionToContentTypeDefinition scriptFileExtensionDefinition = null;

        [Export]
        [FileExtension(".gscript")]
        [ContentType("script")]
        internal static FileExtensionToContentTypeDefinition gscriptFileExtensionDefinition = null;

        #endregion

        #region Classification Type Definitions

        [Export]
        [Name("script")]
        internal static ClassificationTypeDefinition scriptClassificationDefinition = null;

        [Export]
        [Name("script.markup")]
        [BaseDefinition("script")]
        internal static ClassificationTypeDefinition ScriptMarkupDefinition = null;

        #endregion

        #region Classification Format Productions

        [Export(typeof(EditorFormatDefinition))]
        [ClassificationType(ClassificationTypeNames = "script.markup")]
        [Name("script.markup")]
        internal sealed class ScriptMarkupFormat : ClassificationFormatDefinition {
            public ScriptMarkupFormat() {
                ForegroundColor = Colors.Blue;
                }
            }
        #endregion
        }
    }
