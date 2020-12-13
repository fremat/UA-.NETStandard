/* ========================================================================
 * Copyright (c) 2005-2020 The OPC Foundation, Inc. All rights reserved.
 *
 * OPC Foundation MIT License 1.00
 * 
 * Permission is hereby granted, free of charge, to any person
 * obtaining a copy of this software and associated documentation
 * files (the "Software"), to deal in the Software without
 * restriction, including without limitation the rights to use,
 * copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the
 * Software is furnished to do so, subject to the following
 * conditions:
 * 
 * The above copyright notice and this permission notice shall be
 * included in all copies or substantial portions of the Software.
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
 * EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
 * OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
 * NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
 * HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
 * WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
 * FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
 * OTHER DEALINGS IN THE SOFTWARE.
 *
 * The complete license agreement can be found here:
 * http://opcfoundation.org/License/MIT/1.00/
 * ======================================================================*/

using System;
using System.Security.Cryptography;

namespace Opc.Ua.Security.Certificates
{
    /// <summary>
    /// Oid constants defined for ASN encoding/decoding.
    /// </summary>
    public static class Oids
    {
        // Asymmetric encryption algorithms, padding
        public const string Dsa = "1.2.840.10040.4.1";
        public const string Rsa = "1.2.840.113549.1.1.1";
        public const string RsaOaep = "1.2.840.113549.1.1.7";
        public const string RsaPss = "1.2.840.113549.1.1.10";

        // RSA PKCS1 signatures
        public const string RsaPkcs1Sha1 = "1.2.840.113549.1.1.5";
        public const string RsaPkcs1Sha256 = "1.2.840.113549.1.1.11";
        public const string RsaPkcs1Sha384 = "1.2.840.113549.1.1.12";
        public const string RsaPkcs1Sha512 = "1.2.840.113549.1.1.13";

        // ECDsa signatures
        public const string ECDsaWithSha1 = "1.2.840.10045.4.1";
        public const string ECDsaWithSha256 = "1.2.840.10045.4.3.2";
        public const string ECDsaWithSha384 = "1.2.840.10045.4.3.3";
        public const string ECDsaWithSha512 = "1.2.840.10045.4.3.4";

        // CRL extensions
        public const string CrlNumber = "2.5.29.20";
        public const string CrlReasonCode = "2.5.29.21";

        // Transport Layer Security(TLS) World Wide Web(WWW) server authentication
        public const string ServerAuthentication = "1.3.6.1.5.5.7.3.1";
        public const string ClientAuthentication = "1.3.6.1.5.5.7.3.2";

        // Authority Inforamtion access
        public const string AuthorityInfoAccess = "1.3.6.1.5.5.7.1.1";
        public const string OnlineCertificateStatusProtocol = "1.3.6.1.5.5.7.48.1";
        public const string CertificateAuthorityIssuers = "1.3.6.1.5.5.7.48.2";

        public const string CRLDistributionPoint = "2.5.29.31";

        /// <summary>
        /// Get the RSA oid for a hash algorithm signature.
        /// </summary>
        /// <param name="hashAlgorithm">The hash algorithm name.</param>
        public static string GetRSAOid(HashAlgorithmName hashAlgorithm)
        {
            if (hashAlgorithm == HashAlgorithmName.SHA1)
            {
                return Oids.RsaPkcs1Sha1;
            }
            else if (hashAlgorithm == HashAlgorithmName.SHA256)
            {
                return Oids.RsaPkcs1Sha256;
            }
            else if (hashAlgorithm == HashAlgorithmName.SHA384)
            {
                return Oids.RsaPkcs1Sha384;
            }
            else if (hashAlgorithm == HashAlgorithmName.SHA512)
            {
                return Oids.RsaPkcs1Sha512;
            }
            else
            {
                throw new NotSupportedException($"Signing RSA with hash {hashAlgorithm.Name} is not supported. ");
            }
        }

        /// <summary>
        /// Get the ECDsa oid for a hash algorithm signature.
        /// </summary>
        /// <param name="hashAlgorithm">The hash algorithm name.</param>
        public static string GetECDsaOid(HashAlgorithmName hashAlgorithm)
        {
            if (hashAlgorithm == HashAlgorithmName.SHA1)
            {
                return Oids.ECDsaWithSha1;
            }
            else if (hashAlgorithm == HashAlgorithmName.SHA256)
            {
                return Oids.ECDsaWithSha256;
            }
            else if (hashAlgorithm == HashAlgorithmName.SHA384)
            {
                return Oids.ECDsaWithSha384;
            }
            else if (hashAlgorithm == HashAlgorithmName.SHA512)
            {
                return Oids.ECDsaWithSha512;
            }
            else
            {
                throw new NotSupportedException($"Signing ECDsa with hash {hashAlgorithm.Name} is not supported. ");
            }
        }

        /// <summary>
        /// Get the hash algorithm used to sign a certificate.
        /// </summary>
        /// <param name="oid">The signature algorithm oid.</param>
        public static HashAlgorithmName GetHashAlgorithmName(string oid)
        {
            switch (oid)
            {
                case Oids.ECDsaWithSha1:
                case Oids.RsaPkcs1Sha1:
                    return HashAlgorithmName.SHA1;
                case Oids.ECDsaWithSha256:
                case Oids.RsaPkcs1Sha256:
                    return HashAlgorithmName.SHA256;
                case Oids.ECDsaWithSha384:
                case Oids.RsaPkcs1Sha384:
                    return HashAlgorithmName.SHA384;
                case Oids.ECDsaWithSha512:
                case Oids.RsaPkcs1Sha512:
                    return HashAlgorithmName.SHA512;
            }
            throw new NotSupportedException($"Hash algorithm {oid} is not supported. ");
        }

    }
}
