using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Goedel.Utilities;
using Goedel.FSR;

namespace Goedel.Command {
    public partial class CommandLex {

        /// <summary>
        /// Construct a parser to read from a string to be specified in GetToken (data)
        /// </summary>
        LexStringReader LexStringReader;
        public CommandLex () {
            LexStringReader = new LexStringReader(null);
            Reader = LexStringReader;
            }

        /// <summary>
        /// Parse the specified string. Note, this is only valid if no LexReader
        /// was specified in the constructor.
        /// </summary>
        /// <param name="Data"></param>
        /// <returns></returns>
        public Token GetToken (string Data) {
            LexStringReader.String = Data;
            Reset();
            return GetToken();
            }


        /// <summary>
        /// Return the resulting string value
        /// </summary>
        public string Value { get => BuildValue.ToString(); }

        /// <summary>
        /// Return the resulting string value
        /// </summary>
        public string Flag { get => BuildFlag.ToString(); }

        public bool Not { get; set; }

        StringBuilder BuildValue = new StringBuilder();
        StringBuilder BuildFlag = new StringBuilder();

        /// <summary>
        /// Reset the value buffers to start a new parse.
        /// </summary>
        public override void Reset () {
            BuildValue.Clear();
            BuildFlag.Clear();
            }

        /// <summary>
        /// Reset the value buffers to start a new parse.
        /// </summary>
        /// <param name="c">The character read</param>
        public virtual void Reset (int c) {
            Reset();
            }

        /// <summary>
        /// Do nothing
        /// </summary>
        /// <param name="c">The character read</param>
        public virtual void Ignore (int c) {
            }

        /// <summary>
        /// Add a character to the value buffer
        /// </summary>
        /// <param name="c">The character read</param>
        public virtual void AddValue (int c) {
            BuildValue.Append((char)c);
            }

        /// <summary>
        /// Add a character to the flag buffer
        /// </summary>
        /// <param name="c">The character read</param>
        public virtual void AddFlag (int c) {
            BuildFlag.Append((char)c);
            }

        public virtual void AddFlagN (int c) {
            BuildFlag.Append((char)c);
            }

        public virtual void AddFlagNo (int c) {
            BuildFlag.Clear(); // delete 'no' prefix
            Not = true;
            }

        public virtual void Abort (int c) {
            }



        }
    }
