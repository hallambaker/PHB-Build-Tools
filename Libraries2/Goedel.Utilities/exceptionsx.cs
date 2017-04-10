using System;



namespace Goedel.Utilities {


    /// <summary>
    /// This feature has not been implemented
    /// </summary>
    public class NYI : global::System.Exception {

		/// <summary>
        /// Create an instance of the exception.
        /// </summary>		
		public NYI () : base () {
			}
        
		/// <summary>
        /// Create an instance of the exception.
        /// </summary>		
        /// <param name="Description">Description of the error</param>	
		public NYI (string Description) : base (Description) {
			}

		/// <summary>
        /// Create an instance of the exception.
        /// </summary>		
        /// <param name="Description">Description of the error</param>	
		/// <param name="Inner">Inner Exception</param>	
		public NYI (string Description, System.Exception Inner) : 
				base (Description, Inner) {
			}

		/// <summary>
        /// User data associated with the exception.
        /// </summary>	
		public object UserData;



		
		/// <summary>
        /// The public fatory delegate
        /// </summary>
        public static global::Goedel.Utilities.ThrowDelegate Throw;

        static System.Exception _Throw(object Reason) {
			if (Reason as string != null) {
				return new NYI(Reason as string);
				}


			else {
				return new NYI("The feature has not been implemented");
				}
            }
        }


    /// <summary>
    /// The file could not be read.
    /// </summary>
    public class FileReadError : global::System.Exception {

		/// <summary>
        /// Create an instance of the exception.
        /// </summary>		
		public FileReadError () : base () {
			}
        
		/// <summary>
        /// Create an instance of the exception.
        /// </summary>		
        /// <param name="Description">Description of the error</param>	
		public FileReadError (string Description) : base (Description) {
			}

		/// <summary>
        /// Create an instance of the exception.
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
        /// Create an instance of the exception.
        /// </summary>		
        /// <param name="Object">User data</param>	
		/// <param name="Inner">Inner Exception</param>	
		public FileReadError (ExceptionData Object) : 
				base (String.Format ("The file 0 could not be read",
					Object.String					)) {
			UserData = Object;
			}

		/// <summary>
        /// Create an instance of the exception.
        /// </summary>		
        /// <param name="Object">User data</param>	
		/// <param name="Inner">Inner Exception</param>	
		public FileReadError (ExceptionData Object, System.Exception Inner) : 
				base (String.Format ("The file 0 could not be read",
					Object.String					), Inner) {
			UserData = Object;
			}



		
		/// <summary>
        /// The public fatory delegate
        /// </summary>
        public static global::Goedel.Utilities.ThrowDelegate Throw;

        static System.Exception _Throw(object Reason) {
			if (Reason as string != null) {
				return new FileReadError(Reason as string);
				}


			else {
				return new FileReadError("The file could not be read");
				}
            }
        }


	}
