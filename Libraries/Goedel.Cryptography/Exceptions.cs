using System;



namespace Goedel.Cryptography {


    /// <summary>
    /// Base class for cryptographic exceptions.
    /// </summary>
    public class CryptographicException : global::System.Exception {

		/// <summary>
        /// Create an instance of the exception.
        /// </summary>		
		public CryptographicException () : base () {
			}
        
		/// <summary>
        /// Create an instance of the exception.
        /// </summary>		
        /// <param name="Description">Description of the error</param>	
		public CryptographicException (string Description) : base (Description) {
			}

		/// <summary>
        /// Create an instance of the exception.
        /// </summary>		
        /// <param name="Description">Description of the error</param>	
		/// <param name="Inner">Inner Exception</param>	
		public CryptographicException (string Description, System.Exception Inner) : 
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
				return new CryptographicException(Reason as string);
				}
			else {
				return new CryptographicException("A cryptographic exception occurred");
				}
            }
        }


    /// <summary>
    /// Placeholder exception to be thrown as a placeholder to mark
    /// code needing improvement.
    /// </summary>
    public class ImplementationLimit : CryptographicException {

		/// <summary>
        /// Create an instance of the exception.
        /// </summary>		
		public ImplementationLimit () : base () {
			}
        
		/// <summary>
        /// Create an instance of the exception.
        /// </summary>		
        /// <param name="Description">Description of the error</param>	
		public ImplementationLimit (string Description) : base (Description) {
			}

		/// <summary>
        /// Create an instance of the exception.
        /// </summary>		
        /// <param name="Description">Description of the error</param>	
		/// <param name="Inner">Inner Exception</param>	
		public ImplementationLimit (string Description, System.Exception Inner) : 
				base (Description, Inner) {
			}




		
		/// <summary>
        /// The public fatory delegate
        /// </summary>
        public static new global::Goedel.Utilities.ThrowDelegate Throw;

        static System.Exception _Throw(object Reason) {
			if (Reason as string != null) {
				return new ImplementationLimit(Reason as string);
				}


			else {
				return new ImplementationLimit("Some implementation limit hit");
				}
            }
        }


    /// <summary>
    /// Some or all of the quorum parameters are incorrect.
    /// </summary>
    public class InvalidQuorum : CryptographicException {

		/// <summary>
        /// Create an instance of the exception.
        /// </summary>		
		public InvalidQuorum () : base () {
			}
        
		/// <summary>
        /// Create an instance of the exception.
        /// </summary>		
        /// <param name="Description">Description of the error</param>	
		public InvalidQuorum (string Description) : base (Description) {
			}

		/// <summary>
        /// Create an instance of the exception.
        /// </summary>		
        /// <param name="Description">Description of the error</param>	
		/// <param name="Inner">Inner Exception</param>	
		public InvalidQuorum (string Description, System.Exception Inner) : 
				base (Description, Inner) {
			}




		
		/// <summary>
        /// The public fatory delegate
        /// </summary>
        public static new global::Goedel.Utilities.ThrowDelegate Throw;

        static System.Exception _Throw(object Reason) {
			if (Reason as string != null) {
				return new InvalidQuorum(Reason as string);
				}


			else {
				return new InvalidQuorum("Quorum parameters invalid");
				}
            }
        }


    /// <summary>
    /// The recovery attempt failed because there weren't enough shares
    /// to recover the key.
    /// </summary>
    public class InsufficientShares : InvalidQuorum {

		/// <summary>
        /// Create an instance of the exception.
        /// </summary>		
		public InsufficientShares () : base () {
			}
        
		/// <summary>
        /// Create an instance of the exception.
        /// </summary>		
        /// <param name="Description">Description of the error</param>	
		public InsufficientShares (string Description) : base (Description) {
			}

		/// <summary>
        /// Create an instance of the exception.
        /// </summary>		
        /// <param name="Description">Description of the error</param>	
		/// <param name="Inner">Inner Exception</param>	
		public InsufficientShares (string Description, System.Exception Inner) : 
				base (Description, Inner) {
			}




		
		/// <summary>
        /// The public fatory delegate
        /// </summary>
        public static new global::Goedel.Utilities.ThrowDelegate Throw;

        static System.Exception _Throw(object Reason) {
			if (Reason as string != null) {
				return new InsufficientShares(Reason as string);
				}


			else {
				return new InsufficientShares("Not enough shares to recover key");
				}
            }
        }


    /// <summary>
    /// The number of shares required to create a quorum must be 
    /// equal to or smaller than the number of shares
    /// </summary>
    public class QuorumExceedsShares : InvalidQuorum {

		/// <summary>
        /// Create an instance of the exception.
        /// </summary>		
		public QuorumExceedsShares () : base () {
			}
        
		/// <summary>
        /// Create an instance of the exception.
        /// </summary>		
        /// <param name="Description">Description of the error</param>	
		public QuorumExceedsShares (string Description) : base (Description) {
			}

		/// <summary>
        /// Create an instance of the exception.
        /// </summary>		
        /// <param name="Description">Description of the error</param>	
		/// <param name="Inner">Inner Exception</param>	
		public QuorumExceedsShares (string Description, System.Exception Inner) : 
				base (Description, Inner) {
			}




		
		/// <summary>
        /// The public fatory delegate
        /// </summary>
        public static new global::Goedel.Utilities.ThrowDelegate Throw;

        static System.Exception _Throw(object Reason) {
			if (Reason as string != null) {
				return new QuorumExceedsShares(Reason as string);
				}


			else {
				return new QuorumExceedsShares("Quorum can\'t exceed shares");
				}
            }
        }


    /// <summary>
    /// The quorum must require at least 2 shares.
    /// </summary>
    public class QuorumInsufficient : InvalidQuorum {

		/// <summary>
        /// Create an instance of the exception.
        /// </summary>		
		public QuorumInsufficient () : base () {
			}
        
		/// <summary>
        /// Create an instance of the exception.
        /// </summary>		
        /// <param name="Description">Description of the error</param>	
		public QuorumInsufficient (string Description) : base (Description) {
			}

		/// <summary>
        /// Create an instance of the exception.
        /// </summary>		
        /// <param name="Description">Description of the error</param>	
		/// <param name="Inner">Inner Exception</param>	
		public QuorumInsufficient (string Description, System.Exception Inner) : 
				base (Description, Inner) {
			}




		
		/// <summary>
        /// The public fatory delegate
        /// </summary>
        public static new global::Goedel.Utilities.ThrowDelegate Throw = _Throw;

        static System.Exception _Throw(object Reason) {
			if (Reason as string != null) {
				return new QuorumInsufficient(Reason as string);
				}


			else {
				return new QuorumInsufficient("Quorum must be at least 2");
				}
            }
        }


    /// <summary>
    /// There must be at least two key shares.
    /// </summary>
    public class SharesInsufficient : InvalidQuorum {

		/// <summary>
        /// Create an instance of the exception.
        /// </summary>		
		public SharesInsufficient () : base () {
			}
        
		/// <summary>
        /// Create an instance of the exception.
        /// </summary>		
        /// <param name="Description">Description of the error</param>	
		public SharesInsufficient (string Description) : base (Description) {
			}

		/// <summary>
        /// Create an instance of the exception.
        /// </summary>		
        /// <param name="Description">Description of the error</param>	
		/// <param name="Inner">Inner Exception</param>	
		public SharesInsufficient (string Description, System.Exception Inner) : 
				base (Description, Inner) {
			}




		
		/// <summary>
        /// The public fatory delegate
        /// </summary>
        public static new global::Goedel.Utilities.ThrowDelegate Throw;

        static System.Exception _Throw(object Reason) {
			if (Reason as string != null) {
				return new SharesInsufficient(Reason as string);
				}


			else {
				return new SharesInsufficient("Shares must be at least 2");
				}
            }
        }


    /// <summary>
    /// This implementation does not support the number of shares
    /// that were requested.
    /// </summary>
    public class QuorumExceeded : InvalidQuorum {

		/// <summary>
        /// Create an instance of the exception.
        /// </summary>		
		public QuorumExceeded () : base () {
			}
        
		/// <summary>
        /// Create an instance of the exception.
        /// </summary>		
        /// <param name="Description">Description of the error</param>	
		public QuorumExceeded (string Description) : base (Description) {
			}

		/// <summary>
        /// Create an instance of the exception.
        /// </summary>		
        /// <param name="Description">Description of the error</param>	
		/// <param name="Inner">Inner Exception</param>	
		public QuorumExceeded (string Description, System.Exception Inner) : 
				base (Description, Inner) {
			}




		
		/// <summary>
        /// The public fatory delegate
        /// </summary>
        public static new global::Goedel.Utilities.ThrowDelegate Throw;

        static System.Exception _Throw(object Reason) {
			if (Reason as string != null) {
				return new QuorumExceeded(Reason as string);
				}


			else {
				return new QuorumExceeded("Too many shares specified");
				}
            }
        }


    /// <summary>
    /// The number of shares required to create a quorum exceeds the maximum
    /// permitted polynomial degree.
    /// </summary>
    public class QuorumDegreeExceeded : InvalidQuorum {

		/// <summary>
        /// Create an instance of the exception.
        /// </summary>		
		public QuorumDegreeExceeded () : base () {
			}
        
		/// <summary>
        /// Create an instance of the exception.
        /// </summary>		
        /// <param name="Description">Description of the error</param>	
		public QuorumDegreeExceeded (string Description) : base (Description) {
			}

		/// <summary>
        /// Create an instance of the exception.
        /// </summary>		
        /// <param name="Description">Description of the error</param>	
		/// <param name="Inner">Inner Exception</param>	
		public QuorumDegreeExceeded (string Description, System.Exception Inner) : 
				base (Description, Inner) {
			}




		
		/// <summary>
        /// The public fatory delegate
        /// </summary>
        public static new global::Goedel.Utilities.ThrowDelegate Throw;

        static System.Exception _Throw(object Reason) {
			if (Reason as string != null) {
				return new QuorumDegreeExceeded(Reason as string);
				}


			else {
				return new QuorumDegreeExceeded("Degree too high");
				}
            }
        }


    /// <summary>
    /// The shares presented are not from the same set of shares.
    /// </summary>
    public class MismatchedShares : InvalidQuorum {

		/// <summary>
        /// Create an instance of the exception.
        /// </summary>		
		public MismatchedShares () : base () {
			}
        
		/// <summary>
        /// Create an instance of the exception.
        /// </summary>		
        /// <param name="Description">Description of the error</param>	
		public MismatchedShares (string Description) : base (Description) {
			}

		/// <summary>
        /// Create an instance of the exception.
        /// </summary>		
        /// <param name="Description">Description of the error</param>	
		/// <param name="Inner">Inner Exception</param>	
		public MismatchedShares (string Description, System.Exception Inner) : 
				base (Description, Inner) {
			}




		
		/// <summary>
        /// The public fatory delegate
        /// </summary>
        public static new global::Goedel.Utilities.ThrowDelegate Throw;

        static System.Exception _Throw(object Reason) {
			if (Reason as string != null) {
				return new MismatchedShares(Reason as string);
				}


			else {
				return new MismatchedShares("Keys must have same threshold");
				}
            }
        }


    /// <summary>
    /// Thje data presented did not match the expected fingerprint
    /// value.			
    /// </summary>
    public class FingerprintMatchFailed : CryptographicException {

		/// <summary>
        /// Create an instance of the exception.
        /// </summary>		
		public FingerprintMatchFailed () : base () {
			}
        
		/// <summary>
        /// Create an instance of the exception.
        /// </summary>		
        /// <param name="Description">Description of the error</param>	
		public FingerprintMatchFailed (string Description) : base (Description) {
			}

		/// <summary>
        /// Create an instance of the exception.
        /// </summary>		
        /// <param name="Description">Description of the error</param>	
		/// <param name="Inner">Inner Exception</param>	
		public FingerprintMatchFailed (string Description, System.Exception Inner) : 
				base (Description, Inner) {
			}




		
		/// <summary>
        /// The public fatory delegate
        /// </summary>
        public static new global::Goedel.Utilities.ThrowDelegate Throw;

        static System.Exception _Throw(object Reason) {
			if (Reason as string != null) {
				return new FingerprintMatchFailed(Reason as string);
				}


			else {
				return new FingerprintMatchFailed("Data did not match expected fingerprint value");
				}
            }
        }


    /// <summary>
    /// The specified key did not have a valid cryptographic
    /// provider. This may be because the key algorithm is 
    /// not supported or the key parameters were found to be invalid.
    /// </summary>
    public class NoProviderSpecified : CryptographicException {

		/// <summary>
        /// Create an instance of the exception.
        /// </summary>		
		public NoProviderSpecified () : base () {
			}
        
		/// <summary>
        /// Create an instance of the exception.
        /// </summary>		
        /// <param name="Description">Description of the error</param>	
		public NoProviderSpecified (string Description) : base (Description) {
			}

		/// <summary>
        /// Create an instance of the exception.
        /// </summary>		
        /// <param name="Description">Description of the error</param>	
		/// <param name="Inner">Inner Exception</param>	
		public NoProviderSpecified (string Description, System.Exception Inner) : 
				base (Description, Inner) {
			}




		
		/// <summary>
        /// The public fatory delegate
        /// </summary>
        public static new global::Goedel.Utilities.ThrowDelegate Throw;

        static System.Exception _Throw(object Reason) {
			if (Reason as string != null) {
				return new NoProviderSpecified(Reason as string);
				}


			else {
				return new NoProviderSpecified("No provider specified");
				}
            }
        }


    /// <summary>
    /// The implementation does not support the requested key size
    /// </summary>
    public class KeySizeNotSupported : CryptographicException {

		/// <summary>
        /// Create an instance of the exception.
        /// </summary>		
		public KeySizeNotSupported () : base () {
			}
        
		/// <summary>
        /// Create an instance of the exception.
        /// </summary>		
        /// <param name="Description">Description of the error</param>	
		public KeySizeNotSupported (string Description) : base (Description) {
			}

		/// <summary>
        /// Create an instance of the exception.
        /// </summary>		
        /// <param name="Description">Description of the error</param>	
		/// <param name="Inner">Inner Exception</param>	
		public KeySizeNotSupported (string Description, System.Exception Inner) : 
				base (Description, Inner) {
			}




		
		/// <summary>
        /// The public fatory delegate
        /// </summary>
        public static new global::Goedel.Utilities.ThrowDelegate Throw;

        static System.Exception _Throw(object Reason) {
			if (Reason as string != null) {
				return new KeySizeNotSupported(Reason as string);
				}


			else {
				return new KeySizeNotSupported("The requested key size is not supported");
				}
            }
        }


    /// <summary>
    /// Initialization of the cryptographic support library failed.
    /// This is a fatal error that cannot be recovered from.
    /// </summary>
    public class InitializationFailed : CryptographicException {

		/// <summary>
        /// Create an instance of the exception.
        /// </summary>		
		public InitializationFailed () : base () {
			}
        
		/// <summary>
        /// Create an instance of the exception.
        /// </summary>		
        /// <param name="Description">Description of the error</param>	
		public InitializationFailed (string Description) : base (Description) {
			}

		/// <summary>
        /// Create an instance of the exception.
        /// </summary>		
        /// <param name="Description">Description of the error</param>	
		/// <param name="Inner">Inner Exception</param>	
		public InitializationFailed (string Description, System.Exception Inner) : 
				base (Description, Inner) {
			}




		
		/// <summary>
        /// The public fatory delegate
        /// </summary>
        public static new global::Goedel.Utilities.ThrowDelegate Throw;

        static System.Exception _Throw(object Reason) {
			if (Reason as string != null) {
				return new InitializationFailed(Reason as string);
				}


			else {
				return new InitializationFailed("Initialization of the cryptographic support library failed.");
				}
            }
        }


	}
