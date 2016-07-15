Goedel
======

The Goedel meta-synthesizer, a tool for building software tools [MIT License]

* How many times have you thought, 'This code could be written by a machine'?

* How many times have you looked at a program where large parts consist of the same basic template filled out in very
    slightly different ways?

* How many times have you wanted to take a pile of legacy code written in the template style and wanted to turn
    it into something more maintainable without having to start from scratch?

If the answer to any of the questions above is 'a lot' then Goedel may be a tool that can save you a lot of coding
time and even more importantly frustration. Goedel is a tool for building code synthesis tools (and yes, it is written
in itself of course). 

A more likely reason for being interested in using Goedel is to use the software tools that have already been written
using it. For example:

   * Command Line inerface parser
   * Web Service compiler (Currently generates JSON/REST but could be retargetted for XML/SOAP)
   * DNS Resource Record parser (comming)
   * MIME Header Parser (For SMTP/HTTP/etc type protocols)

In the past I wrote a synthesizer that would generate user interface code for X11 and other windows systems. That would
probably need to be rewritten if it was to be relevant again. But I did find it quicker to write a tool to implement 
just one X11 program than to code from scratch.

The code itself is written in C# but should compile on unix pretty easily using Mono. The code produced by the tools 
can be in any language you like. I have used Goedel to generate code in C, C#, Java, Fortran, Occam, SQL and many
others. You can also generate documentation, I have generated HTML, XML, LaTeX.


History
=======

I wrote the first version of Goedel in 1988 but then I got distracted by a little networking project I was also working
on at CERN. This version of Goedel is Goedel 2.0 which is a complete rewrite of the original to make use of the 
features in object oriented programming.

After I started working on Goedel, another group of folk came along and told me that they had already 'claimed'
the name. I didn't see that this was legitimate since my paper was published first and the programming language
they had built had nothing particularly 'Goedelian' about it. So I didn't see any reason to change then and I
still don't today when their project is dead.