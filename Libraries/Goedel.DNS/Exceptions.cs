using System;
using Goedel.Utilities;



namespace Goedel.DNS {


    /// <summary>
    /// An internal assertion check failed.
    /// </summary>
    public class DNSError : global::System.Exception {

		/// <summary>
        /// Construct instance for exception "A DNS Error occurred"
        /// </summary>		
		public DNSError () : base ("A DNS Error occurred") {
			}
        
		/// <summary>
        /// Construct instance for exception "A DNS Error occurred"
        /// </summary>		
        /// <param name="Description">Description of the error</param>	
		public DNSError (string Description) : base (Description) {
			}

		/// <summary>
        /// Construct instance for exception 		/// containing an inner exception.
        /// </summary>		
        /// <param name="Description">Description of the error</param>	
		/// <param name="Inner">Inner Exception</param>	
		public DNSError (string Description, System.Exception Inner) : 
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
				return new DNSError(Reason as string);
				}
			else {
				return new DNSError();
				}
            }
        }


    /// <summary>
    /// This feature has not been implemented
    /// </summary>
    public class NYI : DNSError {

		/// <summary>
        /// Construct instance for exception "The feature has not been implemented"
        /// </summary>		
		public NYI () : base ("The feature has not been implemented") {
			}
        
		/// <summary>
        /// Construct instance for exception "The feature has not been implemented"
        /// </summary>		
        /// <param name="Description">Description of the error</param>	
		public NYI (string Description) : base (Description) {
			}

		/// <summary>
        /// Construct instance for exception 		/// containing an inner exception.
        /// </summary>		
        /// <param name="Description">Description of the error</param>	
		/// <param name="Inner">Inner Exception</param>	
		public NYI (string Description, System.Exception Inner) : 
				base (Description, Inner) {
			}




		
		/// <summary>
        /// The public fatory delegate
        /// </summary>
        public static new global::Goedel.Utilities.ThrowDelegate Throw = _Throw;

        static System.Exception _Throw(object Reason) {
			if (Reason as string != null) {
				return new NYI(Reason as string);
				}
			else {
				return new NYI();
				}
            }
        }


    /// <summary>
    /// An internal assertion check failed.
    /// </summary>
    public class Internal : DNSError {

		/// <summary>
        /// Construct instance for exception "An internal error occurred"
        /// </summary>		
		public Internal () : base ("An internal error occurred") {
			}
        
		/// <summary>
        /// Construct instance for exception "An internal error occurred"
        /// </summary>		
        /// <param name="Description">Description of the error</param>	
		public Internal (string Description) : base (Description) {
			}

		/// <summary>
        /// Construct instance for exception 		/// containing an inner exception.
        /// </summary>		
        /// <param name="Description">Description of the error</param>	
		/// <param name="Inner">Inner Exception</param>	
		public Internal (string Description, System.Exception Inner) : 
				base (Description, Inner) {
			}




		
		/// <summary>
        /// The public fatory delegate
        /// </summary>
        public static new global::Goedel.Utilities.ThrowDelegate Throw = _Throw;

        static System.Exception _Throw(object Reason) {
			if (Reason as string != null) {
				return new Internal(Reason as string);
				}
			else {
				return new Internal();
				}
            }
        }


    /// <summary>
    /// Buffer Overflow
    /// </summary>
    public class DNSBufferOverflow : DNSError {

		/// <summary>
        /// Construct instance for exception "Buffer Overflow"
        /// </summary>		
		public DNSBufferOverflow () : base ("Buffer Overflow") {
			}
        
		/// <summary>
        /// Construct instance for exception "Buffer Overflow"
        /// </summary>		
        /// <param name="Description">Description of the error</param>	
		public DNSBufferOverflow (string Description) : base (Description) {
			}

		/// <summary>
        /// Construct instance for exception 		/// containing an inner exception.
        /// </summary>		
        /// <param name="Description">Description of the error</param>	
		/// <param name="Inner">Inner Exception</param>	
		public DNSBufferOverflow (string Description, System.Exception Inner) : 
				base (Description, Inner) {
			}




		
		/// <summary>
        /// The public fatory delegate
        /// </summary>
        public static new global::Goedel.Utilities.ThrowDelegate Throw = _Throw;

        static System.Exception _Throw(object Reason) {
			if (Reason as string != null) {
				return new DNSBufferOverflow(Reason as string);
				}
			else {
				return new DNSBufferOverflow();
				}
            }
        }


    /// <summary>
    /// Read Truncated
    /// </summary>
    public class DNSReadTruncated : DNSError {

		/// <summary>
        /// Construct instance for exception "Read Truncated"
        /// </summary>		
		public DNSReadTruncated () : base ("Read Truncated") {
			}
        
		/// <summary>
        /// Construct instance for exception "Read Truncated"
        /// </summary>		
        /// <param name="Description">Description of the error</param>	
		public DNSReadTruncated (string Description) : base (Description) {
			}

		/// <summary>
        /// Construct instance for exception 		/// containing an inner exception.
        /// </summary>		
        /// <param name="Description">Description of the error</param>	
		/// <param name="Inner">Inner Exception</param>	
		public DNSReadTruncated (string Description, System.Exception Inner) : 
				base (Description, Inner) {
			}




		
		/// <summary>
        /// The public fatory delegate
        /// </summary>
        public static new global::Goedel.Utilities.ThrowDelegate Throw = _Throw;

        static System.Exception _Throw(object Reason) {
			if (Reason as string != null) {
				return new DNSReadTruncated(Reason as string);
				}
			else {
				return new DNSReadTruncated();
				}
            }
        }


    /// <summary>
    /// The DNS response contained a label that is too long
    /// </summary>
    public class DNSLabelTooLong : DNSError {

		/// <summary>
        /// Construct instance for exception "The DNS response contained a label that is too long"
        /// </summary>		
		public DNSLabelTooLong () : base ("The DNS response contained a label that is too long") {
			}
        
		/// <summary>
        /// Construct instance for exception "The DNS response contained a label that is too long"
        /// </summary>		
        /// <param name="Description">Description of the error</param>	
		public DNSLabelTooLong (string Description) : base (Description) {
			}

		/// <summary>
        /// Construct instance for exception 		/// containing an inner exception.
        /// </summary>		
        /// <param name="Description">Description of the error</param>	
		/// <param name="Inner">Inner Exception</param>	
		public DNSLabelTooLong (string Description, System.Exception Inner) : 
				base (Description, Inner) {
			}




		
		/// <summary>
        /// The public fatory delegate
        /// </summary>
        public static new global::Goedel.Utilities.ThrowDelegate Throw = _Throw;

        static System.Exception _Throw(object Reason) {
			if (Reason as string != null) {
				return new DNSLabelTooLong(Reason as string);
				}
			else {
				return new DNSLabelTooLong();
				}
            }
        }


    /// <summary>
    /// The DNS response contained an illegal character
    /// </summary>
    public class DNSIllegalCharacter : DNSError {

		/// <summary>
        /// Construct instance for exception "The DNS response contained an illegal character"
        /// </summary>		
		public DNSIllegalCharacter () : base ("The DNS response contained an illegal character") {
			}
        
		/// <summary>
        /// Construct instance for exception "The DNS response contained an illegal character"
        /// </summary>		
        /// <param name="Description">Description of the error</param>	
		public DNSIllegalCharacter (string Description) : base (Description) {
			}

		/// <summary>
        /// Construct instance for exception 		/// containing an inner exception.
        /// </summary>		
        /// <param name="Description">Description of the error</param>	
		/// <param name="Inner">Inner Exception</param>	
		public DNSIllegalCharacter (string Description, System.Exception Inner) : 
				base (Description, Inner) {
			}




		
		/// <summary>
        /// The public fatory delegate
        /// </summary>
        public static new global::Goedel.Utilities.ThrowDelegate Throw = _Throw;

        static System.Exception _Throw(object Reason) {
			if (Reason as string != null) {
				return new DNSIllegalCharacter(Reason as string);
				}
			else {
				return new DNSIllegalCharacter();
				}
            }
        }


    /// <summary>
    /// The data is not a valid IP address
    /// </summary>
    public class DNSInvalidAddress : DNSError {

		/// <summary>
        /// Construct instance for exception "The data is not a valid IP address"
        /// </summary>		
		public DNSInvalidAddress () : base ("The data is not a valid IP address") {
			}
        
		/// <summary>
        /// Construct instance for exception "The data is not a valid IP address"
        /// </summary>		
        /// <param name="Description">Description of the error</param>	
		public DNSInvalidAddress (string Description) : base (Description) {
			}

		/// <summary>
        /// Construct instance for exception 		/// containing an inner exception.
        /// </summary>		
        /// <param name="Description">Description of the error</param>	
		/// <param name="Inner">Inner Exception</param>	
		public DNSInvalidAddress (string Description, System.Exception Inner) : 
				base (Description, Inner) {
			}




		
		/// <summary>
        /// The public fatory delegate
        /// </summary>
        public static new global::Goedel.Utilities.ThrowDelegate Throw = _Throw;

        static System.Exception _Throw(object Reason) {
			if (Reason as string != null) {
				return new DNSInvalidAddress(Reason as string);
				}
			else {
				return new DNSInvalidAddress();
				}
            }
        }


    /// <summary>
    /// The DNS response contained a tag that is too long
    /// </summary>
    public class DNSTagTooLong : DNSError {

		/// <summary>
        /// Construct instance for exception "The DNS response contained a tag that is too long"
        /// </summary>		
		public DNSTagTooLong () : base ("The DNS response contained a tag that is too long") {
			}
        
		/// <summary>
        /// Construct instance for exception "The DNS response contained a tag that is too long"
        /// </summary>		
        /// <param name="Description">Description of the error</param>	
		public DNSTagTooLong (string Description) : base (Description) {
			}

		/// <summary>
        /// Construct instance for exception 		/// containing an inner exception.
        /// </summary>		
        /// <param name="Description">Description of the error</param>	
		/// <param name="Inner">Inner Exception</param>	
		public DNSTagTooLong (string Description, System.Exception Inner) : 
				base (Description, Inner) {
			}




		
		/// <summary>
        /// The public fatory delegate
        /// </summary>
        public static new global::Goedel.Utilities.ThrowDelegate Throw = _Throw;

        static System.Exception _Throw(object Reason) {
			if (Reason as string != null) {
				return new DNSTagTooLong(Reason as string);
				}
			else {
				return new DNSTagTooLong();
				}
            }
        }


	}
