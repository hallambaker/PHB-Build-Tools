using System;



namespace Goedel.ASN {


    /// <summary>
    /// An error occurred in the decoding of presumed ASN.1 binary data.
    /// </summary>
    public class ASNDecodingException : global::System.Exception {

		/// <summary>
        /// Create an instance of the exception.
        /// </summary>		
		public ASNDecodingException () : base () {
			}
        
		/// <summary>
        /// Create an instance of the exception.
        /// </summary>		
        /// <param name="Description">Description of the error</param>	
		public ASNDecodingException (string Description) : base (Description) {
			}

		/// <summary>
        /// Create an instance of the exception.
        /// </summary>		
        /// <param name="Description">Description of the error</param>	
		/// <param name="Inner">Inner Exception</param>	
		public ASNDecodingException (string Description, System.Exception Inner) : 
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
				return new ASNDecodingException(Reason as string);
				}


			else {
				return new ASNDecodingException("An ASN.1 Decoding exception occurred");
				}
            }
        }


    /// <summary>
    /// The data could not be decoded due to an implementation restriction
    /// in the decoder. This should not happen when attempting to decode
    /// legitimate inputs for the intended field of use.
    /// </summary>
    public class Implementation : ASNDecodingException {

		/// <summary>
        /// Create an instance of the exception.
        /// </summary>		
		public Implementation () : base () {
			}
        
		/// <summary>
        /// Create an instance of the exception.
        /// </summary>		
        /// <param name="Description">Description of the error</param>	
		public Implementation (string Description) : base (Description) {
			}

		/// <summary>
        /// Create an instance of the exception.
        /// </summary>		
        /// <param name="Description">Description of the error</param>	
		/// <param name="Inner">Inner Exception</param>	
		public Implementation (string Description, System.Exception Inner) : 
				base (Description, Inner) {
			}




		
		/// <summary>
        /// The public fatory delegate
        /// </summary>
        public static new global::Goedel.Utilities.ThrowDelegate Throw;

        static System.Exception _Throw(object Reason) {
			if (Reason as string != null) {
				return new Implementation(Reason as string);
				}


			else {
				return new Implementation("Implementation restriction");
				}
            }
        }


    /// <summary>
    /// A length specification was invalid.
    /// </summary>
    public class InvalidLength : ASNDecodingException {

		/// <summary>
        /// Create an instance of the exception.
        /// </summary>		
		public InvalidLength () : base () {
			}
        
		/// <summary>
        /// Create an instance of the exception.
        /// </summary>		
        /// <param name="Description">Description of the error</param>	
		public InvalidLength (string Description) : base (Description) {
			}

		/// <summary>
        /// Create an instance of the exception.
        /// </summary>		
        /// <param name="Description">Description of the error</param>	
		/// <param name="Inner">Inner Exception</param>	
		public InvalidLength (string Description, System.Exception Inner) : 
				base (Description, Inner) {
			}




		
		/// <summary>
        /// The public fatory delegate
        /// </summary>
        public static new global::Goedel.Utilities.ThrowDelegate Throw;

        static System.Exception _Throw(object Reason) {
			if (Reason as string != null) {
				return new InvalidLength(Reason as string);
				}


			else {
				return new InvalidLength("Length invalid");
				}
            }
        }


    /// <summary>
    /// An indefinite length was specified in a context where it is not
    /// permitted. For example, a DER encoded item.
    /// </summary>
    public class IndefiniteLengthInvalid : ASNDecodingException {

		/// <summary>
        /// Create an instance of the exception.
        /// </summary>		
		public IndefiniteLengthInvalid () : base () {
			}
        
		/// <summary>
        /// Create an instance of the exception.
        /// </summary>		
        /// <param name="Description">Description of the error</param>	
		public IndefiniteLengthInvalid (string Description) : base (Description) {
			}

		/// <summary>
        /// Create an instance of the exception.
        /// </summary>		
        /// <param name="Description">Description of the error</param>	
		/// <param name="Inner">Inner Exception</param>	
		public IndefiniteLengthInvalid (string Description, System.Exception Inner) : 
				base (Description, Inner) {
			}




		
		/// <summary>
        /// The public fatory delegate
        /// </summary>
        public static new global::Goedel.Utilities.ThrowDelegate Throw;

        static System.Exception _Throw(object Reason) {
			if (Reason as string != null) {
				return new IndefiniteLengthInvalid(Reason as string);
				}


			else {
				return new IndefiniteLengthInvalid("Indefinite length not valid");
				}
            }
        }


    /// <summary>
    /// The declared length of an item exceeds the available data.
    /// This typically happens if the data item has been truncated
    /// or a malicious payload is attempting a buffer overflow attack.
    /// </summary>
    public class LengthExceedsInput : ASNDecodingException {

		/// <summary>
        /// Create an instance of the exception.
        /// </summary>		
		public LengthExceedsInput () : base () {
			}
        
		/// <summary>
        /// Create an instance of the exception.
        /// </summary>		
        /// <param name="Description">Description of the error</param>	
		public LengthExceedsInput (string Description) : base (Description) {
			}

		/// <summary>
        /// Create an instance of the exception.
        /// </summary>		
        /// <param name="Description">Description of the error</param>	
		/// <param name="Inner">Inner Exception</param>	
		public LengthExceedsInput (string Description, System.Exception Inner) : 
				base (Description, Inner) {
			}




		
		/// <summary>
        /// The public fatory delegate
        /// </summary>
        public static new global::Goedel.Utilities.ThrowDelegate Throw;

        static System.Exception _Throw(object Reason) {
			if (Reason as string != null) {
				return new LengthExceedsInput(Reason as string);
				}


			else {
				return new LengthExceedsInput("Length exceeds data input");
				}
            }
        }


    /// <summary>
    /// The length of an inner length encoded item exceeds that of the
    /// enclosing item. This means that either the data is not ASN.1,
    /// the data has been corrupted or is a malicious payload intended to
    /// perform a buffer overflow attack.
    /// </summary>
    public class LengthExceedsStructure : ASNDecodingException {

		/// <summary>
        /// Create an instance of the exception.
        /// </summary>		
		public LengthExceedsStructure () : base () {
			}
        
		/// <summary>
        /// Create an instance of the exception.
        /// </summary>		
        /// <param name="Description">Description of the error</param>	
		public LengthExceedsStructure (string Description) : base (Description) {
			}

		/// <summary>
        /// Create an instance of the exception.
        /// </summary>		
        /// <param name="Description">Description of the error</param>	
		/// <param name="Inner">Inner Exception</param>	
		public LengthExceedsStructure (string Description, System.Exception Inner) : 
				base (Description, Inner) {
			}




		
		/// <summary>
        /// The public fatory delegate
        /// </summary>
        public static new global::Goedel.Utilities.ThrowDelegate Throw;

        static System.Exception _Throw(object Reason) {
			if (Reason as string != null) {
				return new LengthExceedsStructure(Reason as string);
				}


			else {
				return new LengthExceedsStructure("Length exceeds current structure");
				}
            }
        }


    /// <summary>
    /// A sequence of items was expected.
    /// </summary>
    public class ExpectedSequence : ASNDecodingException {

		/// <summary>
        /// Create an instance of the exception.
        /// </summary>		
		public ExpectedSequence () : base () {
			}
        
		/// <summary>
        /// Create an instance of the exception.
        /// </summary>		
        /// <param name="Description">Description of the error</param>	
		public ExpectedSequence (string Description) : base (Description) {
			}

		/// <summary>
        /// Create an instance of the exception.
        /// </summary>		
        /// <param name="Description">Description of the error</param>	
		/// <param name="Inner">Inner Exception</param>	
		public ExpectedSequence (string Description, System.Exception Inner) : 
				base (Description, Inner) {
			}




		
		/// <summary>
        /// The public fatory delegate
        /// </summary>
        public static new global::Goedel.Utilities.ThrowDelegate Throw;

        static System.Exception _Throw(object Reason) {
			if (Reason as string != null) {
				return new ExpectedSequence(Reason as string);
				}


			else {
				return new ExpectedSequence("Expected Sequence");
				}
            }
        }


    /// <summary>
    /// Data was encountered in an unexpected location.
    /// </summary>
    public class UnExpectedData : ASNDecodingException {

		/// <summary>
        /// Create an instance of the exception.
        /// </summary>		
		public UnExpectedData () : base () {
			}
        
		/// <summary>
        /// Create an instance of the exception.
        /// </summary>		
        /// <param name="Description">Description of the error</param>	
		public UnExpectedData (string Description) : base (Description) {
			}

		/// <summary>
        /// Create an instance of the exception.
        /// </summary>		
        /// <param name="Description">Description of the error</param>	
		/// <param name="Inner">Inner Exception</param>	
		public UnExpectedData (string Description, System.Exception Inner) : 
				base (Description, Inner) {
			}




		
		/// <summary>
        /// The public fatory delegate
        /// </summary>
        public static new global::Goedel.Utilities.ThrowDelegate Throw;

        static System.Exception _Throw(object Reason) {
			if (Reason as string != null) {
				return new UnExpectedData(Reason as string);
				}


			else {
				return new UnExpectedData("Unexpected Data");
				}
            }
        }


    /// <summary>
    /// An integer was expected.
    /// </summary>
    public class ExpectedInteger : ASNDecodingException {

		/// <summary>
        /// Create an instance of the exception.
        /// </summary>		
		public ExpectedInteger () : base () {
			}
        
		/// <summary>
        /// Create an instance of the exception.
        /// </summary>		
        /// <param name="Description">Description of the error</param>	
		public ExpectedInteger (string Description) : base (Description) {
			}

		/// <summary>
        /// Create an instance of the exception.
        /// </summary>		
        /// <param name="Description">Description of the error</param>	
		/// <param name="Inner">Inner Exception</param>	
		public ExpectedInteger (string Description, System.Exception Inner) : 
				base (Description, Inner) {
			}




		
		/// <summary>
        /// The public fatory delegate
        /// </summary>
        public static new global::Goedel.Utilities.ThrowDelegate Throw;

        static System.Exception _Throw(object Reason) {
			if (Reason as string != null) {
				return new ExpectedInteger(Reason as string);
				}


			else {
				return new ExpectedInteger("Expected Integer");
				}
            }
        }


    /// <summary>
    /// The size of an integer exceeds the implementation limit. This
    /// is unlikely to occur in normal use.
    /// </summary>
    public class IntegerOverflow : ASNDecodingException {

		/// <summary>
        /// Create an instance of the exception.
        /// </summary>		
		public IntegerOverflow () : base () {
			}
        
		/// <summary>
        /// Create an instance of the exception.
        /// </summary>		
        /// <param name="Description">Description of the error</param>	
		public IntegerOverflow (string Description) : base (Description) {
			}

		/// <summary>
        /// Create an instance of the exception.
        /// </summary>		
        /// <param name="Description">Description of the error</param>	
		/// <param name="Inner">Inner Exception</param>	
		public IntegerOverflow (string Description, System.Exception Inner) : 
				base (Description, Inner) {
			}




		
		/// <summary>
        /// The public fatory delegate
        /// </summary>
        public static new global::Goedel.Utilities.ThrowDelegate Throw;

        static System.Exception _Throw(object Reason) {
			if (Reason as string != null) {
				return new IntegerOverflow(Reason as string);
				}


			else {
				return new IntegerOverflow("Integer too large");
				}
            }
        }


	}
