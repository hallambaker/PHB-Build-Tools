using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Goedel.ASN {
    public class Constants {
        public const byte Boolean           =  1;
        public const byte Integer           =  2;
        public const byte BitString         =  3;
        public const byte OctetString       =  4;
        public const byte Null              =  5;
        public const byte ObjectIdentifier  =  6;
        public const byte ObjectDescriptor  =  7;
        public const byte External          =  8;
        public const byte Real              =  9;
        public const byte Numerated         = 10;
        public const byte Embedded          = 11;
        public const byte UTF8String        = 12;
        public const byte RelativeOid       = 13;
        public const byte Sequence          = 16;
        public const byte Set               = 17;
        public const byte NumericString     = 18;
        public const byte PrintableString   = 19;
        public const byte TeletexString     = 20;
        public const byte VideotexString    = 21;
        public const byte IA5String         = 22;
        public const byte UTCTime           = 23;
        public const byte GeneralizedTime   = 24;
        public const byte GraphicString     = 25;
        public const byte VisibleString     = 26;
        public const byte GeneralString     = 27;
        public const byte UniversalString   = 28;
        public const byte CharacterString   = 29;
        public const byte BMPString         = 30;


        }




    public abstract class Root {
        public virtual int [] OID { get { return null; } }  

        public virtual byte [] DER () {
            Goedel.ASN.Buffer Buffer = new Buffer ();
            this.Encode (Buffer);

            return Buffer.Data;
            }

        public abstract void Encode (Goedel.ASN.Buffer Buffer) ;

        }

    /// <summary>
    /// Utility class containing static methods.
    /// </summary>
    public class ASN {
        static char[] Dot = new char[] { '.' };

        public static int[] OIDToArray(string OID) {
            var Split = OID.Split(Dot);
            var Result = new int [Split.Length];
            for (var i = 0; i < Split.Length; i++) {
                Result [i] = Convert.ToInt32 (Split[i]);
                }
            return Result;
            }
        }

    }
