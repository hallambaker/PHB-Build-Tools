using System;
using Goedel.Utilities;



namespace Goedel.Document.RFC {


    /// <summary>
    /// </summary>
    public class HTMLParse : global::System.Exception {

		/// <summary>
        /// Construct instance for exception "HTML Parser error"
        /// </summary>		
		public HTMLParse () : base ("HTML Parser error") {
			}
        
		/// <summary>
        /// Construct instance for exception "HTML Parser error"
        /// </summary>		
        /// <param name="Description">Description of the error</param>	
		public HTMLParse (string Description) : base (Description) {
			}

		/// <summary>
        /// Construct instance for exception 		/// containing an inner exception.
        /// </summary>		
        /// <param name="Description">Description of the error</param>	
		/// <param name="Inner">Inner Exception</param>	
		public HTMLParse (string Description, System.Exception Inner) : 
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
				return new HTMLParse(Reason as string);
				}
			else {
				return new HTMLParse();
				}
            }
        }


    /// <summary>
    /// </summary>
    public class HTMLParseNoBody : HTMLParse {

		/// <summary>
        /// Construct instance for exception "HTML Parser error no HTML body element"
        /// </summary>		
		public HTMLParseNoBody () : base ("HTML Parser error no HTML body element") {
			}
        
		/// <summary>
        /// Construct instance for exception "HTML Parser error no HTML body element"
        /// </summary>		
        /// <param name="Description">Description of the error</param>	
		public HTMLParseNoBody (string Description) : base (Description) {
			}

		/// <summary>
        /// Construct instance for exception 		/// containing an inner exception.
        /// </summary>		
        /// <param name="Description">Description of the error</param>	
		/// <param name="Inner">Inner Exception</param>	
		public HTMLParseNoBody (string Description, System.Exception Inner) : 
				base (Description, Inner) {
			}




		
		/// <summary>
        /// The public fatory delegate
        /// </summary>
        public static new global::Goedel.Utilities.ThrowDelegate Throw = _Throw;

        static System.Exception _Throw(object Reason) {
			if (Reason as string != null) {
				return new HTMLParseNoBody(Reason as string);
				}
			else {
				return new HTMLParseNoBody();
				}
            }
        }


    /// <summary>
    /// </summary>
    public class HTMLParseNoH1 : HTMLParse {

		/// <summary>
        /// Construct instance for exception "HTML Parser error no HTML H1 element for title"
        /// </summary>		
		public HTMLParseNoH1 () : base ("HTML Parser error no HTML H1 element for title") {
			}
        
		/// <summary>
        /// Construct instance for exception "HTML Parser error no HTML H1 element for title"
        /// </summary>		
        /// <param name="Description">Description of the error</param>	
		public HTMLParseNoH1 (string Description) : base (Description) {
			}

		/// <summary>
        /// Construct instance for exception 		/// containing an inner exception.
        /// </summary>		
        /// <param name="Description">Description of the error</param>	
		/// <param name="Inner">Inner Exception</param>	
		public HTMLParseNoH1 (string Description, System.Exception Inner) : 
				base (Description, Inner) {
			}




		
		/// <summary>
        /// The public fatory delegate
        /// </summary>
        public static new global::Goedel.Utilities.ThrowDelegate Throw = _Throw;

        static System.Exception _Throw(object Reason) {
			if (Reason as string != null) {
				return new HTMLParseNoH1(Reason as string);
				}
			else {
				return new HTMLParseNoH1();
				}
            }
        }


    /// <summary>
    /// </summary>
    public class HTMLParseNoH2 : HTMLParse {

		/// <summary>
        /// Construct instance for exception "HTML Parser error no HTML H2 element for start of document"
        /// </summary>		
		public HTMLParseNoH2 () : base ("HTML Parser error no HTML H2 element for start of document") {
			}
        
		/// <summary>
        /// Construct instance for exception "HTML Parser error no HTML H2 element for start of document"
        /// </summary>		
        /// <param name="Description">Description of the error</param>	
		public HTMLParseNoH2 (string Description) : base (Description) {
			}

		/// <summary>
        /// Construct instance for exception 		/// containing an inner exception.
        /// </summary>		
        /// <param name="Description">Description of the error</param>	
		/// <param name="Inner">Inner Exception</param>	
		public HTMLParseNoH2 (string Description, System.Exception Inner) : 
				base (Description, Inner) {
			}




		
		/// <summary>
        /// The public fatory delegate
        /// </summary>
        public static new global::Goedel.Utilities.ThrowDelegate Throw = _Throw;

        static System.Exception _Throw(object Reason) {
			if (Reason as string != null) {
				return new HTMLParseNoH2(Reason as string);
				}
			else {
				return new HTMLParseNoH2();
				}
            }
        }


    /// <summary>
    /// </summary>
    public class NotFoundReserved : HTMLParse {

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
					Object.String					)) {
			UserData = Object;
			}

		/// <summary>
        /// Construct instance for exception using a userdata parameter of
		/// type ExceptionData and the format string "Expected reserved word, token {0} was not found"
        /// </summary>		
        /// <param name="Object">User data</param>	
		/// <param name="Inner">Inner Exception</param>	
		public NotFoundReserved (ExceptionData Object, System.Exception Inner) : 
				base (global::System.String.Format ("Expected reserved word, token {0} was not found",
					Object.String					), Inner) {
			UserData = Object;
			}



		
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


    /// <summary>
    /// </summary>
    public class IPRInvalid : global::System.Exception {

		/// <summary>
        /// Construct instance for exception "Invalid IPR term specified"
        /// </summary>		
		public IPRInvalid () : base ("Invalid IPR term specified") {
			}
        
		/// <summary>
        /// Construct instance for exception "Invalid IPR term specified"
        /// </summary>		
        /// <param name="Description">Description of the error</param>	
		public IPRInvalid (string Description) : base (Description) {
			}

		/// <summary>
        /// Construct instance for exception 		/// containing an inner exception.
        /// </summary>		
        /// <param name="Description">Description of the error</param>	
		/// <param name="Inner">Inner Exception</param>	
		public IPRInvalid (string Description, System.Exception Inner) : 
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
				return new IPRInvalid(Reason as string);
				}
			else {
				return new IPRInvalid();
				}
            }
        }


	}
