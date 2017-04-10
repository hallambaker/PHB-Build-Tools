using System;

namespace Goedel.Platform {
    /// <summary>Placehiolder exception</summary>
    public class TBSException : System.Exception {
        /// <summary>Primitive exception</summary>
        public TBSException() {
            }
        /// <summary>Descriptive exception.</summary>
        /// <param name="message">Message to display.</param>
        public TBSException(string message)
            : base(message) {
            }
        }
    }
