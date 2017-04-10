#Bootmaker

Bootmaker is a tool for converting markdown text into HTML with 
'decorations' for rich presentation in a Web presentation layer.
While the system was built to enable presentation using Bootstrap,
the tools are highly configurable and could in theory support
any site.

##The Markdown Source

The Web site source is in Markdown format files laid out in a 
hierarchy that matches the navigation layout for the final Web
site.

The Markdown flavor is based on GitHub Markdown with additional
semantics being expressed in a simplified form of XML tagging:

:Close tags do not require labels

::The sequence &lt;/> closes the enclosing XML element.

:Default attributes

::Since every hypertext anchor needs a target, the syntax
&lt;a=".. is a lot easier to remember than &lt;a href=".. 


##The tag definition file

The main control file for Bootmaker is the .mdsd file which contains
definitions of the tags to be used inside the markup and the
XML bindings into which they are to be converted.

The tag definition file allows the following types of tag to 
be declared:

:Meta 

::These are tags that declare data that is to be applied to the
whole document. For example, declaring the title or the list
of authors.

:Layout

::Layout tags are used to group together blocks of text. They can
be defined to automatically wrap themselves around particular types
of block.

:Block
  
::Block tags define a block of text in a particular format such
as a heading or a paragraph

:Annotation

::Annotation tags specify markup that is applied within a text
block to specify changes to a different type of font, to declare
hypertext anchors or text with particular semantics.

