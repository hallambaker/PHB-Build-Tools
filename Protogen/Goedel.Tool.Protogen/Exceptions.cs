using System;
using Goedel.Utilities;



namespace Goedel.Tool.ProtoGen {


    /// <summary>
    /// The input could not be parsed
    /// </summary>
    public class UndefinedReference : global::System.Exception {

		/// <summary>
        /// Construct instance for exception "Reference to undefined object"
        /// </summary>		
		public UndefinedReference () : base ("Reference to undefined object") {
			}
        
		/// <summary>
        /// Construct instance for exception "Reference to undefined object"
        /// </summary>		
        /// <param name="Description">Description of the error</param>	
		public UndefinedReference (string Description) : base (Description) {
			}

		/// <summary>
        /// Construct instance for exception 		/// containing an inner exception.
        /// </summary>		
        /// <param name="Description">Description of the error</param>	
		/// <param name="Inner">Inner Exception</param>	
		public UndefinedReference (string Description, System.Exception Inner) : 
				base (Description, Inner) {
			}

		/// <summary>
        /// User data associated with the exception.
        /// </summary>	
		public object UserData;



		
		/// <summary>
        /// The public fatory delegate
        /// </summary>
        public static global::Goedel.Utilities.ThrowDelegate Throw = _Throw;

        static System.Exception _Throw(object Reason) {
			if (Reason as string != null) {
				return new UndefinedReference(Reason as string);
				}
			else {
				return new UndefinedReference();
				}
            }
        }


    /// <summary>
    /// The file could not be read.
    /// </summary>
    public class UndefinedParent : UndefinedReference {

		/// <summary>
        /// Construct instance for exception "The parent is not referenced."
        /// </summary>		
		public UndefinedParent () : base ("The parent is not referenced.") {
			}
        
		/// <summary>
        /// Construct instance for exception "The parent is not referenced."
        /// </summary>		
        /// <param name="Description">Description of the error</param>	
		public UndefinedParent (string Description) : base (Description) {
			}

		/// <summary>
        /// Construct instance for exception 		/// containing an inner exception.
        /// </summary>		
        /// <param name="Description">Description of the error</param>	
		/// <param name="Inner">Inner Exception</param>	
		public UndefinedParent (string Description, System.Exception Inner) : 
				base (Description, Inner) {
			}


		/// <summary>
        /// Construct instance for exception using a userdata parameter of
		/// type UndefinedParentError and the format string "Class {0} references unknown parent {1}"
        /// </summary>		
        /// <param name="Object">User data</param>	
		public UndefinedParent (UndefinedParentError Object) : 
				base (global::System.String.Format ("Class {0} references unknown parent {1}",
					Object.Class,
					Object.Inherits					)) => UserData = Object;


		/// <summary>
        /// Construct instance for exception using a userdata parameter of
		/// type UndefinedParentError and the format string "Class {0} references unknown parent {1}"
        /// </summary>		
        /// <param name="Object">User data</param>	
		/// <param name="Inner">Inner Exception</param>	
		public UndefinedParent (UndefinedParentError Object, System.Exception Inner) : 
				base (global::System.String.Format ("Class {0} references unknown parent {1}",
					Object.Class,
					Object.Inherits					), Inner) => UserData = Object;



		
		/// <summary>
        /// The public fatory delegate
        /// </summary>
        public static new global::Goedel.Utilities.ThrowDelegate Throw = _Throw;

        static System.Exception _Throw(object Reason) {
			if (Reason as string != null) {
				return new UndefinedParent(Reason as string);
				}
			else if (Reason as UndefinedParentError != null) {
				return new UndefinedParent(Reason as UndefinedParentError);
				}
			else {
				return new UndefinedParent();
				}
            }
        }


	}
