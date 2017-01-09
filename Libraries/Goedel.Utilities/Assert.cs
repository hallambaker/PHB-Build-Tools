using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace Goedel.Utilities {


    /// <summary>
    /// Delegate that will be thrown as an exception if a condition is met
    /// </summary>
    /// <param name="Reason"></param>
    /// <returns>The exception to throw</returns>
    public delegate System.Exception ThrowDelegate(string Reason);

    /// <summary>
    /// Convenience class for constructing an object on the fly to report exception
    /// parameters of type integer or string.
    /// </summary>
    public class ExceptionData {
        /// <summary>An integer value;</summary>
        public int Int { get; set; }

        /// <summary>A string value</summary>
        public string String { get; set; }

        /// <summary>
        /// Factory method to create and return object with specified integer
        /// and/or string values.
        /// </summary>
        /// <param name="Int">The integer value</param>
        /// <param name="String">The string value</param>
        /// <returns></returns>
        public ExceptionData Box (int Int=0, string String="") {
            return new ExceptionData() {
                Int = Int,
                String = String
                };
            }
        }



    /// <summary>
    /// Convenience routines to test various types of assertion and throw
    /// an exception using an exception factory method such as the ones
    /// created by Exceptional.
    /// </summary>
    public static class Assert {

        /// <summary>Throw an exception if the specified condition is true. 
        ///Assert.False (test, NYIException.Throw, "test was true")
        /// </summary>
        /// <param name="Condition">The condition</param>
        /// <param name="Throw">Delegate that creates the exception to be thrown if
        /// Condition is true</param>
        /// <param name="Reason">Reason, for debugging</param>
        public static void False(bool Condition, ThrowDelegate Throw, object Reason=null) {
            if (Condition) {
                if (Reason as string != null) {
                    throw Throw(Reason as string);
                    }
                else {
                    throw Throw("");
                    }
                }
            }

        /// <summary>Throw an exception if the specified condition is false. 
        ///Assert.True (test, NYIException.Throw, "test was false")
        /// </summary>
        /// <param name="Condition">The condition</param>
        /// <param name="Throw">Delegate that creates the exception to be thrown if
        /// Condition is true</param>
        /// <param name="Reason">Reason, for debugging</param>
        public static void True(bool Condition, ThrowDelegate Throw, string Reason=null) {
            False(!Condition, Throw, Reason);
            }

        /// <summary>Throw an exception if the specified object is not null. 
        /// </summary>
        /// <param name="Object">The condition</param>
        /// <param name="Throw">Delegate that creates the exception to be thrown if
        /// Condition is true</param>
        /// <param name="Reason">Reason, for debugging</param>
        public static void Null(object Object, ThrowDelegate Throw, string Reason=null) {
            True(Object == null, Throw, Reason);
            }

        /// <summary>Throw an exception if the specified object is not null. 
        /// </summary>
        /// <param name="Object">The condition</param>
        /// <param name="Throw">Delegate that creates the exception to be thrown if
        /// Condition is true</param>
        /// <param name="Reason">Reason, for debugging</param>
        public static void NotNull(object Object, ThrowDelegate Throw, string Reason=null) {
            True(Object != null, Throw, Reason);
            }



        }

    }

