using System;
using Goedel.Utilities;



namespace Goedel.Schema {


    /// <summary>
    /// </summary>
    public class SchemaParse : global::System.Exception {

		/// <summary>
        /// Construct instance for exception "The schema could not be parsed"
        /// </summary>		
		public SchemaParse () : base ("The schema could not be parsed") {
			}
        
		/// <summary>
        /// Construct instance for exception "The schema could not be parsed"
        /// </summary>		
        /// <param name="Description">Description of the error</param>	
		public SchemaParse (string Description) : base (Description) {
			}

		/// <summary>
        /// Construct instance for exception 		/// containing an inner exception.
        /// </summary>		
        /// <param name="Description">Description of the error</param>	
		/// <param name="Inner">Inner Exception</param>	
		public SchemaParse (string Description, System.Exception Inner) : 
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
				return new SchemaParse(Reason as string);
				}
			else {
				return new SchemaParse();
				}
            }
        }


    /// <summary>
    /// </summary>
    public class NotFoundReserved : SchemaParse {

		/// <summary>
        /// Construct instance for exception "An error occurred"
        /// </summary>		
		public NotFoundReserved () : base ("An error occurred") {
			}
        
		/// <summary>
        /// Construct instance for exception "An error occurred"
        /// </summary>		
        /// <param name="Description">Description of the error</param>	
		public NotFoundReserved (string Description) : base (Description) {
			}

		/// <summary>
        /// Construct instance for exception 		/// containing an inner exception.
        /// </summary>		
        /// <param name="Description">Description of the error</param>	
		/// <param name="Inner">Inner Exception</param>	
		public NotFoundReserved (string Description, System.Exception Inner) : 
				base (Description, Inner) {
			}


		/// <summary>
        /// Construct instance for exception using a userdata parameter of
		/// type ExceptionData and the format string "Expected reserved word, token {0} was not found"
        /// </summary>		
        /// <param name="Object">User data</param>	
		public NotFoundReserved (ExceptionData Object) : 
				base (global::System.String.Format ("Expected reserved word, token {0} was not found",
					Object.String					)) => UserData = Object;


		/// <summary>
        /// Construct instance for exception using a userdata parameter of
		/// type ExceptionData and the format string "Expected reserved word, token {0} was not found"
        /// </summary>		
        /// <param name="Object">User data</param>	
		/// <param name="Inner">Inner Exception</param>	
		public NotFoundReserved (ExceptionData Object, System.Exception Inner) : 
				base (global::System.String.Format ("Expected reserved word, token {0} was not found",
					Object.String					), Inner) => UserData = Object;



		
		/// <summary>
        /// The public fatory delegate
        /// </summary>
        public static new global::Goedel.Utilities.ThrowDelegate Throw = _Throw;

        static System.Exception _Throw(object Reason) {
			if (Reason as string != null) {
				return new NotFoundReserved(Reason as string);
				}
			else if (Reason as ExceptionData != null) {
				return new NotFoundReserved(Reason as ExceptionData);
				}
			else {
				return new NotFoundReserved();
				}
            }
        }


	}
