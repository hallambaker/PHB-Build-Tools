using System;
using Goedel.Utilities;



namespace Goedel.Tool.Command {


    /// <summary>
    /// The input could not be parsed
    /// </summary>
    public class ParserException : global::System.Exception {

		/// <summary>
        /// Construct instance for exception "The input could not be parsed"
        /// </summary>		
		public ParserException () : base ("The input could not be parsed") {
			}
        
		/// <summary>
        /// Construct instance for exception "The input could not be parsed"
        /// </summary>		
        /// <param name="Description">Description of the error</param>	
		public ParserException (string Description) : base (Description) {
			}

		/// <summary>
        /// Construct instance for exception 		/// containing an inner exception.
        /// </summary>		
        /// <param name="Description">Description of the error</param>	
		/// <param name="Inner">Inner Exception</param>	
		public ParserException (string Description, System.Exception Inner) : 
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
				return new ParserException(Reason as string);
				}
			else {
				return new ParserException();
				}
            }
        }


    /// <summary>
    /// The file could not be read.
    /// </summary>
    public class UnknownOptionSet : ParserException {

		/// <summary>
        /// Construct instance for exception "The OptionSet is not specified"
        /// </summary>		
		public UnknownOptionSet () : base ("The OptionSet is not specified") {
			}
        
		/// <summary>
        /// Construct instance for exception "The OptionSet is not specified"
        /// </summary>		
        /// <param name="Description">Description of the error</param>	
		public UnknownOptionSet (string Description) : base (Description) {
			}

		/// <summary>
        /// Construct instance for exception 		/// containing an inner exception.
        /// </summary>		
        /// <param name="Description">Description of the error</param>	
		/// <param name="Inner">Inner Exception</param>	
		public UnknownOptionSet (string Description, System.Exception Inner) : 
				base (Description, Inner) {
			}


		/// <summary>
        /// Construct instance for exception using a userdata parameter of
		/// type ExceptionData and the format string "The OptionSet {0} is not specified"
        /// </summary>		
        /// <param name="Object">User data</param>	
		public UnknownOptionSet (ExceptionData Object) : 
				base (global::System.String.Format ("The OptionSet {0} is not specified",
					Object.String					)) => UserData = Object;


		/// <summary>
        /// Construct instance for exception using a userdata parameter of
		/// type ExceptionData and the format string "The OptionSet {0} is not specified"
        /// </summary>		
        /// <param name="Object">User data</param>	
		/// <param name="Inner">Inner Exception</param>	
		public UnknownOptionSet (ExceptionData Object, System.Exception Inner) : 
				base (global::System.String.Format ("The OptionSet {0} is not specified",
					Object.String					), Inner) => UserData = Object;



		
		/// <summary>
        /// The public fatory delegate
        /// </summary>
        public static new global::Goedel.Utilities.ThrowDelegate Throw = _Throw;

        static System.Exception _Throw(object Reason) {
			if (Reason as string != null) {
				return new UnknownOptionSet(Reason as string);
				}
			else if (Reason as ExceptionData != null) {
				return new UnknownOptionSet(Reason as ExceptionData);
				}
			else {
				return new UnknownOptionSet();
				}
            }
        }


	}
