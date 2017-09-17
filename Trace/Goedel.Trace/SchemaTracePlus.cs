using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Goedel.Protocol;

namespace Goedel.Trace {


    /// <summary>
    ///
    /// Mesh profile for Recrypt applications.
    /// </summary>
    public abstract partial class TraceConfirm {

        public static void Initialize () {
            //???.Append(_TagDictionary);
            }
        }



    public partial class TraceProtocol {


        }


    public partial class TraceResponse {


        /// <summary>
        /// The tag dictionary for decoding
        /// </summary>
        public static new Dictionary<string, JSONFactoryDelegate> _TagDictionary =
            Merge(Message._TagDictionary, TraceProtocol._TagDictionary); // Hack: should fix the generator


        /// <summary>
        /// Numeric status return code value
        /// </summary>
        public override int StatusCode {
            get { return Status; }
            set { Status = value; }
            }

        /// <summary>
        /// Description of the status code (for debugging).
        /// </summary>
        public override string StatusDescriptionCode {
            get {
                return StatusDescription;
                }
            set {
                StatusDescription = value;
                }
            }

        /// <summary>
        /// Default constructor
        /// </summary>
        public TraceResponse () {
            StatusCode = 201;
            StatusDescriptionCode = "Operation completed successfully";
            }

        /// <summary>
        /// Performs a deep recursive copy of the structure.
        /// </summary>
        /// <returns>Deep copy of the object with all referenced objects
        /// copied.</returns>
        public override JSONObject DeepCopy () {
            // Convert this object to text:
            var Text = ToString();

            // Convert text back to an object:
            var Result = FromJSON(Text.JSONReader());

            return Result;
            }
        }

    public partial class TraceRequest {
        public static new Dictionary<string, JSONFactoryDelegate> _TagDictionary =
            Merge(Message._TagDictionary, TraceProtocol._TagDictionary);  // Hack: should fix the generator



        /// <summary>
        /// Performs a deep recursive copy of the structure.
        /// </summary>
        /// <returns>Deep copy of the object with all referenced objects
        /// copied.</returns>
        public override JSONObject DeepCopy () {
            // Convert this object to text:
            var Text = ToString();

            // Convert text back to an object:
            var Result = FromJSON(Text.JSONReader());

            return Result;
            }
        }
    }
