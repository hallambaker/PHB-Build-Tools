SETLOCAL
@echo "Current Directory %cd%"
@echo "Batch file is in %~dp0"

cd %~dp0

batc stringcopy.c		protogenlib\stringcopy.h /lazy
batc allocate.c			protogenlib\allocate.h /lazy
batc buffer.c			protogenlib\buffer.h /lazy
batc debug.c			protogenlib\debug.h /lazy
batc base64.c			protogenlib\base64.h /lazy
batc bstring.c			protogenlib\bstring.h /lazy
batc buffer.c			protogenlib\buffer.h /lazy
batc serialize.c		protogenlib\serialize.h /lazy
batc deserialize.c		protogenlib\deserialize.h /lazy
batc context.c			protogenlib\context.h /lazy
batc stream.c			protogenlib\stream.h /lazy
batc stream_socket.c	protogenlib\stream_socket.h /lazy
batc stream_memory.c	protogenlib\stream_memory.h /lazy
batc stream_network.c	protogenlib\stream_network.h /lazy
batc time.c				protogenlib\time.h /lazy

delete ..\..\..\Libraries\protogenlib\*
copy *.h protogenlib\
copy ProtoGenlib\* %INCLUDE_FILES%\protogenlib


exit /b 0


