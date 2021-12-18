using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Goedel.Utilities;

namespace Goedel.Document.RFC {


    public class ListLevel {
        List<BlockType> ListItems = new();
        int ListPointer = -1;

        public delegate void OpenListDelegate (BlockType ListItem);
        public OpenListDelegate OpenListItem;

        public delegate void CloseListDelegate (BlockType ListItem);
        public CloseListDelegate CloseListItem;

        public string ID= "TBS";



        void OpenList (BlockType ListItem) {
            //TextWriter.Write(Start);
            ListPointer++;

            if (ListItems.Count < (ListPointer + 1)) {
                ListItems.Add(ListItem);
                }
            else {
                ListItems[ListPointer] = ListItem;
                }
            OpenListItem(ListItems[ListPointer]);
            }

        void CloseList () {
            CloseListItem(ListItems[ListPointer]);
            ListPointer--;
            }



        public void SetListLevel (int Level, BlockType ListItem, string IDRoot="") {
            ID = IDRoot;

            if (Level < ListPointer) {
                while (Level < ListPointer) {
                    CloseList();
                    }
                }
            if (Level < 0) {
                return;
                }

            if (Level > ListPointer) {
                while (Level > ListPointer) {
                    OpenList(ListItem);
                    }
                return;
                }


            // Level == ListPointer 
            if ((ListItems[ListPointer] == ListItem) |
                (ListItems[ListPointer] == BlockType.Term & ListItem == BlockType.Data)) {
                return;
                }
            CloseList();
            OpenList(ListItem);
            }



        public void ListLast() => SetListLevel(-1, BlockType.Data);




        }
    }
