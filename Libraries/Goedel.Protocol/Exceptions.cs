using System;
using Goedel.Utilities;



namespace Goedel.Protocol {


    /// <summary>
    /// </summary>
    public class Dechunking : global::System.Exception {

		/// <summary>
        /// Construct instance for exception "Key could not be read"
        /// </summary>		
		public Dechunking () : base ("Key could not be read") {
			}
        
		/// <summary>
        /// Construct instance for exception "Key could not be read"
        /// </summary>		
        /// <param name="Description">Description of the error</param>	
		public Dechunking (string Description) : base (Description) {
			}

		/// <summary>
        /// Construct instance for exception 		/// containing an inner exception.
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
        public static global::Goedel.Utilities.ThrowDelegate Throw = _Throw;

        static System.Exception _Throw(object Reason) {
			if (Reason as string != null) {
				return new Dechunking(Reason as string);
				}
			else {
				return new Dechunking();
				}
            }
        }


    /// <summary>
    /// The requested operation is not known to this server.
    /// </summary>
    public class UnknownOperation : Dechunking {

		/// <summary>
        /// Construct instance for exception "The requested operation is not known to this server."
        /// </summary>		
		public UnknownOperation () : base ("The requested operation is not known to this server.") {
			}
        
		/// <summary>
        /// Construct instance for exception "The requested operation is not known to this server."
        /// </summary>		
        /// <param name="Description">Description of the error</param>	
		public UnknownOperation (string Description) : base (Description) {
			}

		/// <summary>
        /// Construct instance for exception 		/// containing an inner exception.
        /// </summary>		
        /// <param name="Description">Description of the error</param>	
		/// <param name="Inner">Inner Exception</param>	
		public UnknownOperation (string Description, System.Exception Inner) : 
				base (Description, Inner) {
			}




		
		/// <summary>
        /// The public fatory delegate
        /// </summary>
        public static new global::Goedel.Utilities.ThrowDelegate Throw = _Throw;

        static System.Exception _Throw(object Reason) {
			if (Reason as string != null) {
				return new UnknownOperation(Reason as string);
				}
			else {
				return new UnknownOperation();
				}
            }
        }


    /// <summary>
    /// Message exceeds permitted size limit
    /// </summary>
    public class MessageTooBig : Dechunking {

		/// <summary>
        /// Construct instance for exception "Message is too big"
        /// </summary>		
		public MessageTooBig () : base ("Message is too big") {
			}
        
		/// <summary>
        /// Construct instance for exception "Message is too big"
        /// </summary>		
        /// <param name="Description">Description of the error</param>	
		public MessageTooBig (string Description) : base (Description) {
			}

		/// <summary>
        /// Construct instance for exception 		/// containing an inner exception.
        /// </summary>		
        /// <param name="Description">Description of the error</param>	
		/// <param name="Inner">Inner Exception</param>	
		public MessageTooBig (string Description, System.Exception Inner) : 
				base (Description, Inner) {
			}




		
		/// <summary>
        /// The public fatory delegate
        /// </summary>
        public static new global::Goedel.Utilities.ThrowDelegate Throw = _Throw;

        static System.Exception _Throw(object Reason) {
			if (Reason as string != null) {
				return new MessageTooBig(Reason as string);
				}
			else {
				return new MessageTooBig();
				}
            }
        }


	}
