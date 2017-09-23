<id>12cd4543-7aeb-41b3-81cf-4f828b2f76d7
<version>1
<contenttype>developerConceptualDocument

These are the support libraries that are used to build systems from
code generated with the PHB Build tools created using the Goedel
code metasynthesizer.

The core idea of Goedel is that the task of the programmer is to
bridge the mental gap between the problem domain and the implementation
domain. The greater that gap is, the more time consuming and error prone
the process.

The core idea of Goedel is that the task of the programmer is to
bridge the mental gap between the problem domain and the implementation
domain. The greater that gap is, the more time consuming and error prone
the process. Closing the gap with tools that move the implementation domain
closer to the problem domain provides better efficiency and more importantly 
better quality.


##Transition to .NET Standard Libraries

During the development of the code, Microsoft introduced and then deprecated the
use of 'Portable Libraries'. As a result, the code is divided into two
parts according to whether features supported in the portable libraries were 
present or not.

The introduction of .NET Standard should eliminiate the need for some but not all
of the prior partitioning of the code base. Unfortunately, while .NET Standard is
a much more satisfactory approach in theory, the implementation of the tool set is not yet
solid enough to make the transition. Consequently, only the libraries 
previously converted to .NET Portable have been converted to .NET Standard.



