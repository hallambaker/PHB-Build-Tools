﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Goedel.Document.Markdown {

    public delegate Document ParseDelegate(string FileName, TagCatalog TagCatalog);
    public delegate bool IncludeDelegate(string FileName, TagCatalog TagCatalog, Document Document);


    public class ParseRegistryEntry {
        public IncludeDelegate Include;
        public ParseDelegate Parse;
        }


    public class ParseRegistry {
        static Dictionary<string, ParseRegistryEntry> ByExtension = 
                        new Dictionary<string, ParseRegistryEntry> ();


        public static void Register (string Extension, ParseRegistryEntry Entry) {
            ByExtension.Add(Extension, Entry);
            }

        static ParseRegistryEntry GetEntry (string FileName) {
            var Extension = Path.GetExtension(FileName);

            ByExtension.TryGetValue(Extension, out var Entry);

            return Entry;
            }


        public static bool Include (string FileName, TagCatalog TagCatalog, Document Document) {
            var Entry = GetEntry(FileName);
            if (Entry == null) {
                return false;
                }

            Entry.Include(FileName, TagCatalog, Document);

            return true;
            }

        public static Document Parse(string FileName, TagCatalog TagCatalog) {
            var Entry = GetEntry(FileName);
            if (Entry == null) {
                return null;
                }

            var Document = Entry.Parse(FileName, TagCatalog);

            return Document;
            }

        }
    }
