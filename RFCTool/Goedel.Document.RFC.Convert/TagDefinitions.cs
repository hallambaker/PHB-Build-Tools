namespace BridgeLib {
    public partial class Constants {
        public const string Value = @"
Class Goedel.MarkLib TagDefinitions
	
	Block figure
	Block include
	Block pre
		Flag plaintext

	Meta bibliography

	Meta author
		Meta firstname
		Meta surname
		Meta organization
		Meta organizationabbrev
		Meta street
		Meta city
		Meta code
		Meta country
		Meta phone
		Meta uir
		Meta initials
		Meta email


	Meta bibsource
	Meta style
	Meta priority


	Block p
		Level 0
		XML ""p"" ""id""

	Block h1
		Level 1
		XML ""h1"" ""id""
	Block h2
		Level 2
		XML ""h2"" ""id""
	Block h3
		Level 3
		XML ""h3"" ""id""
	Block h4
		Level 4
		XML ""h4"" ""id""
	Block h5
		Level 5
		XML ""h5"" ""id""
	Block h6
		Level 6
		XML ""h6"" ""id""

	Block appendix
		Level 1
		XML ""h1"" ""id""

	Layout table
	Layout tablerow
		Stack table
	Layout tablecell
		Stack table
		Stack tablerow

	Layout ul
		XML ""ul"" ""class""
	Layout ol
		XML ""ol"" ""class""
	Layout dl
		XML ""dl"" ""class""

	Block li
		Stack ul
		Stack row
		XML ""li"" ""class""

	Block nli
		Stack ol
		Stack row
		XML ""li"" ""class""

	Block dt
		Stack dl
		Stack row
		XML ""dt"" ""class""
	Block dd
		Stack dl
		Stack row
		XML ""dd"" ""class""


	Annotation a
		Remark 
			|Text anchor
		XML ""a"" ""href""

	Annotation b
		Remark 
			|Bold font
		XML ""b"" ""class""

	Annotation i
		Remark 
			|Italic font
		XML ""i"" ""class""

	Annotation u
		Remark 
			|Underline
		XML ""u"" ""class""
		 
	Annotation s
		Remark 
			|Strikethrough
		XML ""s"" ""class""

	Annotation x
		Remark
			|Code (monospaced) font
		XML ""code"" ""class""

	Annotation d
		Remark
			|Definition of a term
		XML ""d"" ""class""


	Item img
		Remark
			|Image
		XML ""img"" ""src""
			Default ""class"" ""img-responsive""

	Item cite
		Remark
			|Citation of an external resource
		XML ""b"" ""class""

	Item ref
		Remark
			|Reference to an external resource
		XML ""b"" ""class""


	Meta layout 
		Flag master 
		Flag navigator 
		Flag toc 
		Flag tof 
		Flag tor 
		Flag tod  


	Meta imgref

	Layout row
		Markup ""<div class=\""row\"">"" ""<div class=\""row\"">"" ""</div>""

	Layout col
		Stack row
		Integer count
		Markup ""<div class=\""span1\"">"" ""<div class=\""span{0}\"">"" ""</div>""

	Meta series
		Meta status
		Meta stream

	Meta title
	Meta abbrev
	Meta consensus

	Meta keyword
	Meta version
	Meta meta

	Meta ipr
	Meta workgroup

	Meta number
	Meta category
	Meta updates
	Meta obsoletes
	Meta seriesnumber

	Meta year
	Meta month
	Meta day

		// These are used for MAML parsing
	Meta id
	Meta contenttype";
        }
    }
