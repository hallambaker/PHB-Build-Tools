// This header file is generated automatically
// 
// Changes to this file will be overwritten

// Generated on: (TBS)
// Tool vertion: (TBS)
// Defines:      __HEADER_PROTOGENLIB_ALLOCATE_H

#ifndef __HEADER_PROTOGENLIB_ALLOCATE_H
#define __HEADER_PROTOGENLIB_ALLOCATE_H 1

#include <protogenlib\json_types.h>
STATUS Z_Init () ;
STATUS Z_Terminate () ;
STATUS Z_Allocate (void *Parent, IntT Size, void **ResultOut) ;
STATUS Z_Reallocate (void *Data, IntT Size, void **ResultOut) ;
STATUS Z_Free (void *Data) ;
STATUS Z_Free_Child (Z_Header *Header) ;
STATUS Z_Check (void *Data) ;
STATUS Z_Check_Header (Z_Header *Header) ;

#endif // __HEADER_PROTOGENLIB_ALLOCATE_H
