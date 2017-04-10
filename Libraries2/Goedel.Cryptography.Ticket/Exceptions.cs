using System;



namespace Goedel.Cryptography.Ticket {


    /// <summary>
    /// </summary>
    public class TicketException : global::System.Exception {

		/// <summary>
        /// Create an instance of the exception.
        /// </summary>		
		public TicketException () : base () {
			}
        
		/// <summary>
        /// Create an instance of the exception.
        /// </summary>		
        /// <param name="Description">Description of the error</param>	
		public TicketException (string Description) : base (Description) {
			}

		/// <summary>
        /// Create an instance of the exception.
        /// </summary>		
        /// <param name="Description">Description of the error</param>	
		/// <param name="Inner">Inner Exception</param>	
		public TicketException (string Description, System.Exception Inner) : 
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
				return new TicketException(Reason as string);
				}


			else {
				return new TicketException("A cryptographic ticket exception occurred");
				}
            }
        }


    /// <summary>
    /// 
    /// </summary>
    public class BadTicket : TicketException {

		/// <summary>
        /// Create an instance of the exception.
        /// </summary>		
		public BadTicket () : base () {
			}
        
		/// <summary>
        /// Create an instance of the exception.
        /// </summary>		
        /// <param name="Description">Description of the error</param>	
		public BadTicket (string Description) : base (Description) {
			}

		/// <summary>
        /// Create an instance of the exception.
        /// </summary>		
        /// <param name="Description">Description of the error</param>	
		/// <param name="Inner">Inner Exception</param>	
		public BadTicket (string Description, System.Exception Inner) : 
				base (Description, Inner) {
			}




		
		/// <summary>
        /// The public fatory delegate
        /// </summary>
        public static new global::Goedel.Utilities.ThrowDelegate Throw;

        static System.Exception _Throw(object Reason) {
			if (Reason as string != null) {
				return new BadTicket(Reason as string);
				}


			else {
				return new BadTicket("The ticket could not be read");
				}
            }
        }


	}
