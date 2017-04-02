using System;
using Goedel.Utilities;

using Goedel.Utilities;


namespace Goedel.Registry {


    /// <summary>
    /// The file could not be read.
    /// </summary>
    public class FileReadError : global::System.Exception {

		/// <summary>
        /// Construct instance for exception "The file could not be read"
        /// </summary>		
		public FileReadError () : base ("The file could not be read") {
			}
        
		/// <summary>
        /// Construct instance for exception "The file could not be read"
        /// </summary>		
        /// <param name="Description">Description of the error</param>	
		public FileReadError (string Description) : base (Description) {
			}

		/// <summary>
        /// Construct instance for exception 		/// containing an inner exception.
        /// </summary>		
        /// <param name="Description">Description of the error</param>	
		/// <param name="Inner">Inner Exception</param>	
		public FileReadError (string Description, System.Exception Inner) : 
				base (Description, Inner) {
			}

		/// <summary>
        /// User data associated with the exception.
        /// </summary>	
		public object UserData;

		/// <summary>
        /// Construct instance for exception using a userdata parameter of
		/// type ExceptionData and the format string "The file {0} could not be read"
        /// </summary>		
        /// <param name="Object">User data</param>	
		public FileReadError (ExceptionData Object) : 
				base (String.Format ("The file {0} could not be read",
					Object.String					)) {
			UserData = Object;
			}

		/// <summary>
        /// Construct instance for exception using a userdata parameter of
		/// type ExceptionData and the format string "The file {0} could not be read"
        /// </summary>		
        /// <param name="Object">User data</param>	
		/// <param name="Inner">Inner Exception</param>	
		public FileReadError (ExceptionData Object, System.Exception Inner) : 
				base (String.Format ("The file {0} could not be read",
					Object.String					), Inner) {
			UserData = Object;
			}



		
		/// <summary>
        /// The public fatory delegate
        /// </summary>
        public static global::Goedel.Utilities.ThrowDelegate Throw = _Throw;

        static System.Exception _Throw(object Reason) {
			if (Reason as string != null) {
				return new FileReadError(Reason as string);
				}
			else if (Reason as ExceptionData != null) {
				return new FileReadError(Reason as ExceptionData);
				}
			else {
				return new FileReadError();
				}
            }
        }


	}
