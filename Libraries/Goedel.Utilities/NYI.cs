using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goedel.Utilities {

    /// <summary>
    /// Exception for 'Not yet implemented' exception.
    /// </summary>
    public class NYI : System.Exception {
        /// <summary>
        /// The public fatory delegate
        /// </summary>
        public static ThrowDelegate Throw = _Throw;

        static System.Exception _Throw(string Reason) {
            return new NYI();
            }
        }
    }
