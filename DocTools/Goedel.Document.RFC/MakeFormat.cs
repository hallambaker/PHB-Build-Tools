using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goedel.Document.RFC {
    public abstract class MakeFormat {

        
        public abstract void MakeMeta (string Tag, string Text, int Indent=0);

        public virtual void MakeMetaParagraph(string Tag, string Text) => MakeMeta(Tag, Text);

        public virtual void MakeMeta (string Tag, List<string> Texts) {
            if (Texts == null) {
                return;
                }
            foreach (var Text in Texts) {
                MakeMeta(Tag, Text, 0);
                }
            }


        public void WriteHeader (BlockDocument Document) {
            MakeMetaParagraph("title", Document.Title);
            MakeMetaParagraph("abbrev", Document.TitleAbrrev);
            foreach (var Series in Document.SeriesInfos) {
                MakeMeta("series", Series.Value);
                MakeMeta("status", Series.Status, 1);
                MakeMeta("stream", Series.Stream, 1);
                MakeMeta("version", Series.ExplicitVersion, 1);
                }

            MakeMeta("ipr", Document.Ipr);
            MakeMeta("area", Document.Area);
            MakeMeta("workgroup", Document.Workgroup);
            MakeMeta("number", Document.Number);
            MakeMeta("category", Document.Category);
            MakeMeta("updates", Document.Updates);
            MakeMeta("obsoletes", Document.Obsoletes);
            MakeMeta("seriesnumber", Document.SeriesNumber);


            WriteAuthors(Document.Authors);

            MakeMeta("also", Document.Also);
            foreach (var Keyword in Document.Keywords) {
                MakeMeta("keyword", Keyword);
                }
            }

        public void WriteAuthors (List<Author> Authors) {
            foreach (var Author in Authors) {
                MakeMeta("author", Author.Name);
                MakeMeta("initials", Author.Initials, 1);
                MakeMeta("organization", Author.Organization, 1);
                MakeMeta("surname", Author.Surname, 1);
                MakeMeta("phone", Author.Phone, 1);
                MakeMeta("email", Author.Email, 1);
                MakeMeta("uri", Author.URI, 1);
                MakeMeta("street", Author.Street, 1);
                MakeMeta("city", Author.City, 1);
                MakeMeta("code", Author.Code, 1);
                MakeMeta("country", Author.Country, 1);
                }
            }

        }



    }
