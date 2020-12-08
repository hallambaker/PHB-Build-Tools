This is the parser for detecting markup inside paragraphs.
t is separate from the MarkDown FSR because we want to use
it to parse text from other source formats such as HTML and Word.

This is NOT an XML parser

Can use </> to close any element
 Can use < and & as regular characters unless confusion would arise
Supports &&, &<, &>, &:, &*, &#, &=, etc as escapes
// Don't need to specify tags for principal attributes <img=file.jpg>
// Close tags are inferred from context
// Don't need to quote attribute values unless they contain spaces etc.

// There are no illegal productions in this markup. If the parser 
// encounters something that starts to look like an XML tag but it
// doesn't complete, this is interpreted as regular text. So <fred <
// produces the sequence "<fred <"

// If the input format does not provide a convenient way to specify 
// text literals (e.g. code blocks) the <pre=terminator> tag is used to
// mark the start of the literal block and the sequence 'terminator' the
// end. If no terminator is specified, the sequence </pre> is used.