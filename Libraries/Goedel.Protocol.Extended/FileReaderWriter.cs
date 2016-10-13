using System;
using System.IO;

namespace Goedel.Protocol.Extended {
    // This implmentation accesses the file directly.


    public class FileCharacterTextStream : CharacterTextStream {
        FileStream Source;

        public override void Mark() {
            MarkPosition = Source.Position;
            MarkBuffer = Buffer;
            }

        public override void Restore() {
            if (MarkPosition >= 0) {
                Source.Seek(MarkPosition, SeekOrigin.Begin);
                Buffer = MarkBuffer;
                }
            }

        public FileCharacterTextStream(FileStream FileStream) {
            this.Source = FileStream;
            }

        protected override int ReadChar() {
            var C1 = Source.ReadByte();
            if (C1 < 0) {
                _EOF = true;
                }
            return C1;
            }

        }

   }
