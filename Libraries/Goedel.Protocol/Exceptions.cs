using System;



namespace Goedel.Protocol {


    /// <summary>
    /// Receive chunked data from HTTP source.
    /// </summary>
    public class Dechunking : global::System.Exception {

		/// <summary>
        /// Create an instance of the exception.
        /// </summary>		
		public Dechunking () : base () {
			}
        
		/// <summary>
        /// Create an instance of the exception.
        /// </summary>		
        /// <param name="Description">Description of the error</param>	
		public Dechunking (string Description) : base (Description) {
			}

		/// <summary>
        /// Create an instance of the exception.
        /// </summary>		
        /// <param name="Description">Description of the error</param>	
		/// <param name="Inner">Inner Exception</param>	
		public Dechunking (string Description, System.Exception Inner) : 
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
				return new Dechunking(Reason as string);
				}


			else {
				return new Dechunking("Key could not be read");
				}
            }
        }


    /// <summary>
    /// The requested operation is not known to this server.
    /// </summary>
    public class UnknownOperation : Dechunking {

		/// <summary>
        /// Create an instance of the exception.
        /// </summary>		
		public UnknownOperation () : base () {
			}
        
		/// <summary>
        /// Create an instance of the exception.
        /// </summary>		
        /// <param name="Description">Description of the error</param>	
		public UnknownOperation (string Description) : base (Description) {
			}

		/// <summary>
        /// Create an instance of the exception.
        /// </summary>		
        /// <param name="Description">Description of the error</param>	
		/// <param name="Inner">Inner Exception</param>	
		public UnknownOperation (string Description, System.Exception Inner) : 
				base (Description, Inner) {
			}




		
		/// <summary>
        /// The public fatory delegate
        /// </summary>
        public static new global::Goedel.Utilities.ThrowDelegate Throw;

        static System.Exception _Throw(object Reason) {
			if (Reason as string != null) {
				return new UnknownOperation(Reason as string);
				}


			else {
				return new UnknownOperation("The requested operation is not known to this server.");
				}
            }
        }


    /// <summary>
    /// Message exceeds permitted size limit
    /// </summary>
    public class MessageTooBig : Dechunking {

		/// <summary>
        /// Create an instance of the exception.
        /// </summary>		
		public MessageTooBig () : base () {
			}
        
		/// <summary>
        /// Create an instance of the exception.
        /// </summary>		
        /// <param name="Description">Description of the error</param>	
		public MessageTooBig (string Description) : base (Description) {
			}

		/// <summary>
        /// Create an instance of the exception.
        /// </summary>		
        /// <param name="Description">Description of the error</param>	
		/// <param name="Inner">Inner Exception</param>	
		public MessageTooBig (string Description, System.Exception Inner) : 
				base (Description, Inner) {
			}




		
		/// <summary>
        /// The public fatory delegate
        /// </summary>
        public static new global::Goedel.Utilities.ThrowDelegate Throw;

        static System.Exception _Throw(object Reason) {
			if (Reason as string != null) {
				return new MessageTooBig(Reason as string);
				}


			else {
				return new MessageTooBig("Message is too big");
				}
            }
        }


	}
