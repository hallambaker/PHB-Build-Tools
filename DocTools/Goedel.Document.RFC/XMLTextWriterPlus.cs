using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Goedel.Registry;


namespace Goedel.Document.RFC {
    public abstract class XMLTextWriterPlus : XMLTextWriter {


        Stack<BlockType> ListItemStack = new();

        /// <summary>
        /// The types of text block
        /// </summary>
        protected enum Mode {
            /// <summary>Initial state</summary>
            None,
            /// <summary>List mode</summary>
            List,
            /// <summary>Paragraph data</summary>
            Paragraph,
            /// <summary>Definition List</summary>
            Definition
            }

        Mode CurrentMode = Mode.None;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="TextWriter">The output stream.</param>
        public XMLTextWriterPlus(TextWriter TextWriter) : base(TextWriter) {
            }


        /// <summary>
        /// Set the wrapping mode for the current text block
        /// </summary>
        /// <param name="TextBlock"></param>
        public void SetTextBlock(TextBlock TextBlock) {
            if (TextBlock as LI != null) {
                var LI = TextBlock as LI;

                if ((LI.Type == BlockType.Data) | (LI.Type == BlockType.Term)) {
                    SetModeDefinition();
                    }
                else {
                    SetModeList(LI.Type);
                    }
                }
            else {
                SetModeParagraph();
                }

            }

        /// <summary>
        /// Begin a block containing either Paragraph, Definition or List content.
        /// </summary>
        /// <param name="Mode">The type of content for this block</param>
        protected abstract void Open(Mode Mode);

        /// <summary>
        /// End a block containing either Paragraph, Definition or List content.
        /// </summary>
        /// <param name="Mode">The type of content for this block</param>
        protected abstract void Close(Mode Mode);

        /// <summary>
        /// Begin a list level
        /// </summary>
        /// <param name="ListItem">The type of list</param>
        protected abstract void OpenList(BlockType ListItem);

        /// <summary>
        /// End a list level
        /// </summary>
        /// <param name="ListItem">The type of list</param>
        protected abstract void CloseList(BlockType ListItem);






        void SetModeList(BlockType Type) {
            if (CurrentMode != Mode.List) {
                CloseBlock();
                CurrentMode = Mode.List;
                Open(CurrentMode);
                }
            }

        void SetModeParagraph() {
            if (CurrentMode != Mode.Paragraph) {
                CloseList();
                CloseBlock();
                CurrentMode = Mode.Paragraph;
                Open(CurrentMode);
                }
            }

        void SetModeDefinition() {
            if (CurrentMode != Mode.Definition) {
                CloseList();
                CloseBlock();
                CurrentMode = Mode.Definition;
                Open(CurrentMode);
                }
            }

        /// <summary>
        /// Open a block of textblock output
        /// </summary>
        public void OpenBlock() => CurrentMode = Mode.None;


        /// <summary>
        /// Close a block of TextBlock output
        /// </summary>
        public void CloseBlock () {
            if (CurrentMode == Mode.None) {
                return;
                }
            CloseList();
            Close(CurrentMode);
            }



        void CloseList() {
            while (ListItemStack.Count> 0) {
                var Item = ListItemStack.Pop();
                CloseList(Item);
                }
            }

        void SetListLevel(int Level, BlockType Type) {
            // Close all higher list levels
            while (Level < ListItemStack.Count) {
                var Item = ListItemStack.Pop();
                CloseList(Item);
                }

            // If we are at the same level and the type is different, close it
            if ((ListItemStack.Count == Level) & Level > 0) {
                if (ListItemStack.Peek() != Type) {
                    var Item = ListItemStack.Pop();
                    CloseList(Item);
                    }
                }

            // Open the necessary number of levels
            while (Level > ListItemStack.Count) {
                ListItemStack.Push(Type);
                OpenList(Type);
                }

            }


        }
    }
