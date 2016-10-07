﻿//   Copyright © 2015 by Comodo Group Inc.
//  
//  Permission is hereby granted, free of charge, to any person obtaining a copy
//  of this software and associated documentation files (the "Software"), to deal
//  in the Software without restriction, including without limitation the rights
//  to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//  copies of the Software, and to permit persons to whom the Software is
//  furnished to do so, subject to the following conditions:
//  
//  The above copyright notice and this permission notice shall be included in
//  all copies or substantial portions of the Software.
//  
//  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//  IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//  AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//  LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//  OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
//  THE SOFTWARE.
//  
//  

using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Goedel.Protocol;

namespace Goedel.Protocol {

    // Transaction Classes
    /// <summary>
    /// The base class for transaction requests
    /// </summary>
    public abstract class Request : JSONObject {

        /// <summary>
        /// Tag identifying this class.
        /// </summary>
        /// <returns>The tag</returns>
		public override string Tag() {
            return "Request";
            }


        /// <summary>
        /// Serialize this object to the specified output stream.
        /// </summary>
        /// <param name="Writer">Output stream</param>
        /// <param name="wrap">If true, output is wrapped with object
        /// start and end sequences '{ ... }'.</param>
        /// <param name="first">If true, item is the first entry in a list.</param>
		public override void Serialize(Writer Writer, bool wrap, ref bool first) {
            SerializeX(Writer, wrap, ref first);
            }

        /// <summary>
        /// Serialize this object to the specified output stream.
        /// Unlike the Serlialize() method, this method is not inherited from the
        /// parent class allowing a specific version of the method to be called.
        /// </summary>
        /// <param name="_Writer">Output stream</param>
        /// <param name="_wrap">If true, output is wrapped with object
        /// start and end sequences '{ ... }'.</param>
        /// <param name="_first">If true, item is the first entry in a list.</param>
		public new void SerializeX(Writer _Writer, bool _wrap, ref bool _first) {
            if (_wrap) {
                _Writer.WriteObjectStart();
                }
            if (_wrap) {
                _Writer.WriteObjectEnd();
                }
            }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="JSONReader"></param>
        /// <param name="Tag"></param>
		public override void DeserializeToken(JSONReader JSONReader, string Tag) {
            // Don't have any default elements.
            return;
            }


        }

    /// <summary>
    /// </summary>
    public abstract class Response : JSONObject {
        private int _Status = 201;  // Default value for status is success

        /// <summary>
        /// Describes the status code (ignored by processors)
        /// </summary>
        private string _StatusDescription;


        /// <summary>
        /// Numeric status return code value
        /// </summary>
		public virtual int StatusCode {
            get { return _Status; }
            set { _Status = value; }
            }

        /// <summary>
        /// Description of the status code (for debugging).
        /// </summary>
        public virtual string StatusDescriptionCode {
            get {
                return _StatusDescription;
                }
            set {
                _StatusDescription = value;
                }
            }




        /// <summary>
        /// Tag identifying this class.
        /// </summary>
        /// <returns>The tag</returns>
		public override string Tag() {
            return "Response";
            }

 
        }
    }
