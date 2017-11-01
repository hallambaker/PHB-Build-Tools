
/*
using System;
using System.Collections.Generic;
using Goedel.Utilities;


namespace $projectname$ {



        /// <summary>
        /// Delagate for binding a container object to an existing file.
        /// </summary>
        /// <param name="FileName">The file to bind</param>
        /// <param name="DataEncoding">The data encoding to use if a new file is created 
        /// (defaults to JSON currently but will soon default to JSON-B).</param>
        /// <param name="UseTree">If true and a new container is being written, create
        /// a tree based index.</param>
        /// <param name="DigestAlgorithm">If not set to NULL, use a cryptographic
        /// container that authenticates the data using a chain (if UseTree is false) or
        /// a Merkle tree (otherwise).</param>
        /// <returns>The created container.</returns>
        public static IContainer NewContainerFactory (
                    string FileName,
                    FileStatus FileStatus = FileStatus.Append,
                    DataEncoding DataEncoding = DataEncoding.JSON,
                    bool UseTree = false,
                    CryptoAlgorithmID DigestAlgorithm = CryptoAlgorithmID.NULL) {

    var FileStream = FileName.FileStream(FileStatus);
    return NewContainerFactory(FileStream, DataEncoding, UseTree, DigestAlgorithm);

    }



/// <summary>
/// Delagate for binding a container object to an existing file.
/// </summary>
/// <param name="FileName">The file to bind</param>
/// <param name="DataEncoding">The data encoding to use if a new file is created 
/// (defaults to JSON currently but will soon default to JSON-B).</param>
/// <param name="UseTree">If true and a new container is being written, create
/// a tree based index.</param>
/// <param name="DigestAlgorithm">If not set to NULL, use a cryptographic
/// container that authenticates the data using a chain (if UseTree is false) or
/// a Merkle tree (otherwise).</param>
/// <returns>The created container.</returns>
public static IContainer BindContainerFactory (
            string FileName,
            FileStatus FileStatus = FileStatus.Append,
            DataEncoding DataEncoding = DataEncoding.JSON,
            bool UseTree = false,
            CryptoAlgorithmID DigestAlgorithm = CryptoAlgorithmID.NULL) {

    var FileStream = FileName.FileStream(FileStatus);
    return BindContainerFactory(FileStream, DataEncoding, UseTree, DigestAlgorithm);

    }
}




/// <summary>
/// Delagate for binding a container object to an existing file.
/// </summary>
/// <param name="FileName">The file to bind</param>
/// <param name="DataEncoding">The data encoding to use if a new file is created 
/// (defaults to JSON currently but will soon default to JSON-B).</param>
/// <param name="UseTree">If true and a new container is being written, create
/// a tree based index.</param>
/// <param name="DigestAlgorithm">If not set to NULL, use a cryptographic
/// container that authenticates the data using a chain (if UseTree is false) or
/// a Merkle tree (otherwise).</param>
/// <returns>The created container.</returns>
public static IContainer BindContainerFactory (
            FileStream FileStream,
            DataEncoding DataEncoding = DataEncoding.JSON,
            bool UseTree = false,
            CryptoAlgorithmID DigestAlgorithm = CryptoAlgorithmID.NULL) {

    FileStream.Seek(0, SeekOrigin.End);
    if (FileStream.Position > 0) {
        throw new NYI();
        // we have a file, read the first record
        }
    else {

        if (UseTree) {
            if (DigestAlgorithm == CryptoAlgorithmID.NULL) {
                return ContainerTree.BindContainer(FileStream, DataEncoding);
                }

            else {
                return ContainerMerkleTree.BindContainer(FileStream, DataEncoding);
                }
            }
        else {
            if (DigestAlgorithm == CryptoAlgorithmID.NULL) {
                return ContainerSimple.BindContainer(FileStream, DataEncoding);
                }

            else {
                return ContainerChain.BindContainer(FileStream, DataEncoding);
                }
            }
        }
    }




/// <summary>
/// Delagate for binding a container object to an existing file.
/// </summary>
/// <param name="FileName">The file to bind</param>
/// <param name="DataEncoding">The data encoding to use if a new file is created 
/// (defaults to JSON currently but will soon default to JSON-B).</param>
/// <param name="UseTree">If true and a new container is being written, create
/// a tree based index.</param>
/// <param name="DigestAlgorithm">If not set to NULL, use a cryptographic
/// container that authenticates the data using a chain (if UseTree is false) or
/// a Merkle tree (otherwise).</param>
/// <returns>The created container.</returns>
public static IContainer NewContainerFactory (
            FileStream FileStream,
            DataEncoding DataEncoding = DataEncoding.JSON,
            bool UseTree = false,
            CryptoAlgorithmID DigestAlgorithm = CryptoAlgorithmID.NULL) {


    if (UseTree) {
        if (DigestAlgorithm == CryptoAlgorithmID.NULL) {
            return ContainerTree.BindContainer(FileStream, DataEncoding);
            }

        else {
            return ContainerMerkleTree.BindContainer(FileStream, DataEncoding);
            }
        }
    else {
        if (DigestAlgorithm == CryptoAlgorithmID.NULL) {
            return new ContainerSimple(FileStream, DataEncoding);
            }

        else {
            return ContainerChain.BindContainer(FileStream, DataEncoding);
            }
        }
    }




/// <summary>
/// Delagate for binding a container object to an existing file.
/// </summary>
/// <param name="FileName">The file to bind</param>
/// <param name="DataEncoding">The data encoding to use if a new file is created 
/// (defaults to JSON currently but will soon default to JSON-B).</param>
/// <param name="UseTree">If true and a new container is being written, create
/// a tree based index.</param>
/// <param name="DigestAlgorithm">If not set to NULL, use a cryptographic
/// container that authenticates the data using a chain (if UseTree is false) or
/// a Merkle tree (otherwise).</param>
/// <returns>The created container.</returns>
public delegate IContainer BindContainerFactoryDelegate (
            string FileName,
            FileStatus FileStatus = FileStatus.Append,
            DataEncoding DataEncoding = DataEncoding.JSON,
            bool UseTree = false,
            CryptoAlgorithmID DigestAlgorithm = CryptoAlgorithmID.NULL);


            */