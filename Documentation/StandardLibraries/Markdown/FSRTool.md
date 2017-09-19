<id>23330bd8-ccd3-4de5-b806-860b84e42380
<version>1
<contenttype>developerConceptualDocument

The VSIX Integration Tool is used to bind code generation tools into
a Visual Studio extension file. It does not require a support library
but the project it is used in must be created as a VSIX project on
a system with the Visual Studio Developer Extensions installed.

# VSIX Integration Tool

## Note Well

While the VSIX tools are used and supported by Microsoft, the number of 
active developers is in the thousands at most. Thus there is a very high
probability of hitting code paths that are untested and receiving build
errors that are misleading.

One problem that has occurred is that systems stop building mysteriously
and non deterministically. This appears to be due to a race condition 
to do with parallel builds and importing stale versions of libraries 
from cache. If this problem is encountered, the best way to clear it seems 
to be to go through all of the projects and delete the obj and bin 
directories and any other files that might have cached data. Then 
delete all the project references and rebuild. To reduce the cost of 
this process, the build libraries are now divided into three parts.



