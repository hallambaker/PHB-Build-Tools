using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goedel.Registry {
    /// <summary>Base class for Command line parser types. This could do with
    /// some decrufting to remove implementation artifacts.</summary>
    public abstract class Type {

        /// <summary>The tag desription</summary>
        public string TagText;
        /// <summary>The command line flag.</summary>
        public string Text;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Type() {
            }

        /// <summary>
        /// Constructor with default value.
        /// </summary>
        /// <param name="Value">The value to set.</param>
        public Type(string Value) {
            Default(Value);
            }

        /// <summary>
        /// Convert value to string.
        /// </summary>
        /// <returns>The string value.</returns>
        public override string ToString() {
            return Text;
            }

        /// <summary>
        /// Register a new tag.
        /// </summary>
        /// <param name="Tag">The type tag</param>
        /// <param name="Registry">The registry to record tag</param>
        /// <param name="Index">The tag index.</param>
        public virtual void Register(string Tag, Registry Registry, int Index) {
            Registry.Register(Tag, Index);
            }

        /// <summary>
        /// Set the tag, this is used in cases where a flag has multiple aliases.
        /// </summary>
        /// <param name="TagIn">The tag text.</param>
        /// <returns>The value 1.</returns>
        public virtual int Tag(string TagIn) {
            TagText = TagIn;
            return 1;
            }

        /// <summary>
        /// Set parameter text.
        /// </summary>
        /// <param name="TextIn">Text to set.</param>
        public virtual void Parameter(string TextIn) {
            Text = TextIn;
            }

        /// <summary>
        /// Set the default value for the type.
        /// </summary>
        /// <param name="TextIn">The default value as it would be given on the command line.</param>
        public virtual void Default(string TextIn) {
            Parameter(TextIn);
            }

        /// <summary>
        /// Return usage report for error messages. Probably defunct.
        /// </summary>
        /// <param name="Tag">The type created</param>
        /// <param name="Value">The value</param>
        /// <param name="Usage">Platform dependent default prefix / for windows, - for Unix.</param>
        /// <returns>The string value</returns>
        public virtual string Usage(string Tag, string Value, char Usage) {
            if (Tag == null) {
                return Value;
                }
            return Usage + Tag + " " + Value;
            }
        }

    /// <summary>Type registry entry</summary>
    public class RegistryEntry {
        /// <summary>the tag</summary>
        public string Tag;

        /// <summary>Index of the tag</summary>
        public int Index;
        }

    /// <summary>Type registry</summary>
    public class Registry {

        /// <summary>The tagged registry entries</summary>
        List<RegistryEntry> Entries = new List<RegistryEntry>();

        /// <summary>
        /// Register an entry
        /// </summary>
        /// <param name="Tag">The tag value</param>
        /// <param name="Index">The position in the definition array</param>
        public void Register(string Tag, int Index) {
            RegistryEntry Entry = new RegistryEntry();
            Entry.Tag = Tag;
            Entry.Index = Index;
            Entries.Add(Entry);
            }

        /// <summary>
        /// Find the first matching entry
        /// </summary>
        /// <param name="Match">The tag to match</param>
        /// <returns>The match index.</returns>
		public int Find(string Match) {
            RegistryEntry Entry = Entries.Find(delegate (RegistryEntry TT) { return TT.Tag == Match; });

            if (Entry == null) {
                throw new Exception("Unknown option: " + Match);
                }
            return Entry.Index;
            }

        }
    }
