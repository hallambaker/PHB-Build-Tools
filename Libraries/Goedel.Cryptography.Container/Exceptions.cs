using System;
using Goedel.Utilities;



namespace Goedel.Cryptography.Container {


    /// <summary>
    /// An attempt was made to access a container type that is not supported by the
    /// class.
    /// </summary>
    public class InvalidContainerTypeException : global::System.Exception {

		/// <summary>
        /// Construct instance for exception "This class does not support the specified container type."
        /// </summary>		
		public InvalidContainerTypeException () : base ("This class does not support the specified container type.") {
			}
        
		/// <summary>
        /// Construct instance for exception "This class does not support the specified container type."
        /// </summary>		
        /// <param name="Description">Description of the error</param>	
		public InvalidContainerTypeException (string Description) : base (Description) {
			}

		/// <summary>
        /// Construct instance for exception 		/// containing an inner exception.
        /// </summary>		
        /// <param name="Description">Description of the error</param>	
		/// <param name="Inner">Inner Exception</param>	
		public InvalidContainerTypeException (string Description, System.Exception Inner) : 
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
				return new InvalidContainerTypeException(Reason as string);
				}
			else {
				return new InvalidContainerTypeException();
				}
            }
        }


    /// <summary>
    /// An attempt was made to create a frame that is larger than the maximum frame size allowed by
    /// the application
    /// </summary>
    public class FrameTooLargeException : global::System.Exception {

		/// <summary>
        /// Construct instance for exception "The frame was too large for this implementation to handle"
        /// </summary>		
		public FrameTooLargeException () : base ("The frame was too large for this implementation to handle") {
			}
        
		/// <summary>
        /// Construct instance for exception "The frame was too large for this implementation to handle"
        /// </summary>		
        /// <param name="Description">Description of the error</param>	
		public FrameTooLargeException (string Description) : base (Description) {
			}

		/// <summary>
        /// Construct instance for exception 		/// containing an inner exception.
        /// </summary>		
        /// <param name="Description">Description of the error</param>	
		/// <param name="Inner">Inner Exception</param>	
		public FrameTooLargeException (string Description, System.Exception Inner) : 
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
				return new FrameTooLargeException(Reason as string);
				}
			else {
				return new FrameTooLargeException();
				}
            }
        }


    /// <summary>
    /// An attempt was made to create a new container file in a mode that did not
    /// allow creation of a new file.
    /// </summary>
    public class InvalidFileModeException : global::System.Exception {

		/// <summary>
        /// Construct instance for exception "The file mode is invalid for creating a new file"
        /// </summary>		
		public InvalidFileModeException () : base ("The file mode is invalid for creating a new file") {
			}
        
		/// <summary>
        /// Construct instance for exception "The file mode is invalid for creating a new file"
        /// </summary>		
        /// <param name="Description">Description of the error</param>	
		public InvalidFileModeException (string Description) : base (Description) {
			}

		/// <summary>
        /// Construct instance for exception 		/// containing an inner exception.
        /// </summary>		
        /// <param name="Description">Description of the error</param>	
		/// <param name="Inner">Inner Exception</param>	
		public InvalidFileModeException (string Description, System.Exception Inner) : 
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
				return new InvalidFileModeException(Reason as string);
				}
			else {
				return new InvalidFileModeException();
				}
            }
        }


    /// <summary>
    /// The file format was found to be invalid or otherwise corrupt.
    /// </summary>
    public class InvalidFileFormatException : global::System.Exception {

		/// <summary>
        /// Construct instance for exception "An error was encountered in reading the container file"
        /// </summary>		
		public InvalidFileFormatException () : base ("An error was encountered in reading the container file") {
			}
        
		/// <summary>
        /// Construct instance for exception "An error was encountered in reading the container file"
        /// </summary>		
        /// <param name="Description">Description of the error</param>	
		public InvalidFileFormatException (string Description) : base (Description) {
			}

		/// <summary>
        /// Construct instance for exception 		/// containing an inner exception.
        /// </summary>		
        /// <param name="Description">Description of the error</param>	
		/// <param name="Inner">Inner Exception</param>	
		public InvalidFileFormatException (string Description, System.Exception Inner) : 
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
				return new InvalidFileFormatException(Reason as string);
				}
			else {
				return new InvalidFileFormatException();
				}
            }
        }


	}
