using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace Goedel.ToolScript.Classifier {


    [Export(typeof(IClassifierProvider))]
    [ContentType("script")]
    internal class DiffClassifierProvider : IClassifierProvider {
        [Import]
        internal IClassificationTypeRegistryService ClassificationRegistry = null;

        static ScriptClassifier ScriptClassifier;

        public IClassifier GetClassifier(ITextBuffer buffer) {
            if (ScriptClassifier == null)
                ScriptClassifier = new ScriptClassifier(ClassificationRegistry);

            return ScriptClassifier;
            }
        }
    }
