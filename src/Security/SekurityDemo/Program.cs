using System;
using System.Security;
using System.Security.Permissions;
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;
using System.Xml;
using System.Text;
using System.IO;

using Security.Cryptography;

namespace Security
{
    class Program
    {
        public const string xmlFileName = @"D:\temp\data3.txt";
        public const int RsaN = 0;
        public const string privateKeyFileName = @"D:\Daten\Develop\DOT.NET\Operationen\Dokumentation\Lizenzen\Demo512PrivateKey.txt";
        public const string publicKeyFileName = @"D:\Daten\Develop\DOT.NET\Operationen\Dokumentation\Lizenzen\Demo512PublicKey.txt";
        public const string outFile = @"d:\temp\signedxml.xml";
        public const string outFileFullTrust = @"d:\temp\signedxml-FullTrust.xml";

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                Program p = new Program();

                p.Test2(RsaN, Program.publicKeyFileName, Program.privateKeyFileName, false);
                p.TestXmlSignature(RsaN, Program.publicKeyFileName, Program.privateKeyFileName);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "\n" + ex.StackTrace);
            }
        }

        void CheckSignature(string xmlFileName, string publicKeyFileName)
        {
            if (XmlCryptography.CheckSignature(xmlFileName, publicKeyFileName))
            {
                Console.WriteLine("OK");
            }
            else
            {
                Console.WriteLine("Error");
            }
        }

        void TestXmlSignature(int rsaManagedN, string publicKeyFileName, string privateKeyFileName)
        {
            Console.WriteLine("XML digital signature");
            Console.WriteLine("---------------------");
            Console.WriteLine();

            XmlTextWriter writer = new XmlTextWriter(outFileFullTrust, System.Text.Encoding.UTF8);
            writer.Formatting = Formatting.Indented;
            Console.Write("MSRSA     : ");
            XmlCryptography.Sign(xmlFileName, privateKeyFileName, writer);
            writer.Flush();
            writer.Close();
            CheckSignature(outFileFullTrust, publicKeyFileName);

            writer = new XmlTextWriter(outFile, System.Text.Encoding.UTF8);
            Console.Write("RSAManaged: ");
            string privateKey = new StreamReader(privateKeyFileName).ReadToEnd();
            StreamReader reader = new StreamReader(xmlFileName);
            string xml = reader.ReadToEnd();
            XmlCryptography.SignNoFullTrust(xml, privateKey, writer);
            writer.Flush();
            writer.Close();
            CheckSignature(outFile, publicKeyFileName);


        }

        void Test2(int rsaManagedN, string publicKeyFileName, string privateKeyFileName, bool generateKey)
        {
            // Dieses klappt bis 4096

            bool ok;

            Console.WriteLine();
            Console.WriteLine("Public Key Cryptography for Shared Webhosting Environments");
            Console.WriteLine("----------------------------------------------------------");
            Console.WriteLine("{0}\npublic key ={1}\nprivate key={2}", rsaManagedN, publicKeyFileName, privateKeyFileName);
            Console.WriteLine();

            // We will need one of these...
            RSAManaged rsaManaged = new RSAManaged();

            // And we will compare our results with one of these:
            //RSACryptoServiceProvider msrsa = new RSACryptoServiceProvider(rsaManagedN);
            RSACryptoServiceProvider msrsa = new RSACryptoServiceProvider();

            if (generateKey)
            {
                // Generate a new public-private key pair and export it as an XML string
                // The resulting XML string can be used in scripts to load up the same key later
                // by passing it to RSAManaged.FromXmlString ();
                rsaManaged.GenerateKeyPair();
                ok = rsaManaged.CheckPrivateKey();
                if (!ok)
                {
                    Console.WriteLine("GenerateKeyPair generated an invalid key; " +
                        "complain to your software vendor :)");
                    return;
                }
                string xml = rsaManaged.ToXmlString(true);
                Console.WriteLine("Generated the following RSA key:\n");
                Console.WriteLine(xml);
            }


            // And just to show you how it's done, here's a key we prepared earlier:
            // Note that this key should be kept secret!  Sad to say, it needs to 
            // appear in your scripts so you might like to lock it away in a DLL
            // (shared webhosting environments are not very secure)
            string previously_generated_private_key =
                "<RSAKeyValue>" +
                "    <Modulus>6eChle22XFSVa5LZcZrdOcGq4EKSO1mRwqccPWbtgEm2CFf8oXdkFkVO+dDryMZyYB+xACFbq0/ZD2uByLQAKw==</Modulus>" +
            "    <Exponent>AQAB</Exponent>" +
            "    <P>/l4Qiqve4fWyyWPpCn1BPlMkE3Qy02ieVuCrznQmfZM=</P>" +
            "    <Q>62DmmOSvPe9PsmEweo1R8IdB8c8d0B58f5boICyt0gk=</Q>" +
            "    <DP>1fiYn53uUlOVPrWtviYZMO1NRpQTgSTbNSevPm8URcM=</DP>" +
            "    <DQ>Ene54AkhTsS2BhLmENeBtFOIcwaDGk8qCYC3mb6nrLE=</DQ>" +
            "    <InverseQ>ZKMeZYfDw2pVD3bKKf3GMtPMMJyBm4i7pBNwg9wU2HY=</InverseQ>" +
            "    <D>jdTsKUA/lz60XshvlbWU87G/LsEwbU2kV6eAOLxyy5i/CsDw4pCUCku8SfvvEumDyVUQETGCenKrX+ocE9JUAQ==</D>" +
            "</RSAKeyValue>";

            if (!string.IsNullOrEmpty(privateKeyFileName))
            {
                previously_generated_private_key = new StreamReader(privateKeyFileName).ReadToEnd();
            }

            rsaManaged.FromXmlString(previously_generated_private_key);
            ok = rsaManaged.CheckPrivateKey();
            if (!ok)
            {
                Console.WriteLine("previously_generated_private_key is invalid");
                return;
            }

            // Will need this later...
            msrsa.FromXmlString(previously_generated_private_key);

            // And finally the corresponding public key, which consists of just
            // the modulus and exponent parts and is known to all and sundry
            string public_key =
                "<RSAKeyValue>" +
                "    <Modulus>6eChle22XFSVa5LZcZrdOcGq4EKSO1mRwqccPWbtgEm2CFf8oXdkFkVO+dDryMZyYB+xACFbq0/ZD2uByLQAKw==</Modulus>" +
            "    <Exponent>AQAB</Exponent>" +
            "</RSAKeyValue>";
            if (!string.IsNullOrEmpty(publicKeyFileName))
            {
                public_key = new StreamReader(publicKeyFileName).ReadToEnd();
            }

            RSAManaged rsaManaged_public = new RSAManaged();
            rsaManaged_public.FromXmlString(public_key);

            //RSACryptoServiceProvider msrsa_public = new RSACryptoServiceProvider(rsaManagedN);
            RSACryptoServiceProvider msrsa_public = new RSACryptoServiceProvider();
            msrsa_public.FromXmlString(public_key);

            // OK, here we go...

            Console.WriteLine("Digital signatures");
            Console.WriteLine("------------------");
            Console.WriteLine();

            // Now let's sign a string; note that what we actually sign is an array of bytes
            SHA1CryptoServiceProvider hasher = new SHA1CryptoServiceProvider();
            string sign_me = "Sign this string";
            ASCIIEncoding ByteConverter = new ASCIIEncoding();
            byte[] b = ByteConverter.GetBytes(sign_me);
            byte[] signature = rsaManaged.SignData(b, hasher);
            Console.WriteLine("Digital signature generated by RSAManaged is:\n" +
                BitConverter.ToString(signature));
            Console.WriteLine();

            // Demonstrate that we generate the same signature as RSACryptoServiceProvider does
            signature = msrsa.SignData(b, hasher);
            Console.WriteLine("Digital signature generated by RSACryptoServiceProvider is:\n" +
                BitConverter.ToString(signature));
            Console.WriteLine();

            // Now verify that our signed data is indeed correctly signed
            ok = rsaManaged_public.VerifyData(b, hasher, signature);
            if (ok)
                Console.WriteLine("Signature verified by RSAManaged OK");
            else
                Console.WriteLine("Signature verification by RSAManaged failed");

            ok = msrsa.VerifyData(b, hasher, signature);
            if (ok)
                Console.WriteLine("Signature verified by RSACryptoServiceProvider OK");
            else
                Console.WriteLine("Signature verification by RSACryptoServiceProvider failed");

            Console.WriteLine();

            //
            // Now let's try encrypting some data
            // Note that data can only be encrypted in small chunks; typically, a DES (symmetric)
            // key is encrypted this way and sent to the other party in encrypted form.  The
            // communicating parties can then use symmetric key encryption for the body of the
            // message, which is much faster.
            //
            // Note that we cannot directly compare the encrypted string with that produced by 
            // RSACryptoServiceProvider as it includes random 'padding' bytes
            //

            Console.WriteLine("Encryption");
            Console.WriteLine("----------");
            Console.WriteLine();

            string encrypt_me = "Encrypt this string";
            b = ByteConverter.GetBytes(encrypt_me);
            byte[] encrypted_bytes = rsaManaged_public.Encrypt(b, false);
            Console.WriteLine("Encrypted data generated by RSAManaged is:\n" +
                BitConverter.ToString(encrypted_bytes));
            Console.WriteLine();

            byte[] decrypted_bytes = rsaManaged.Decrypt(encrypted_bytes, false);
            string decrypted_string = ByteConverter.GetString(decrypted_bytes);
            Console.WriteLine("Encrypted by RSAManaged, decrypted by RSAManaged, result = " +
                decrypted_string);

            decrypted_bytes = msrsa.Decrypt(encrypted_bytes, false);
            decrypted_string = ByteConverter.GetString(decrypted_bytes);
            Console.WriteLine("Encrypted by RSAManaged, decrypted by RSACSP,     result = " +
                decrypted_string);
            Console.WriteLine();

            encrypted_bytes = msrsa_public.Encrypt(b, false);
            Console.WriteLine("Encrypted data generated by RSACSP is:\n" +
                BitConverter.ToString(encrypted_bytes));
            Console.WriteLine();

            decrypted_bytes = msrsa.Decrypt(encrypted_bytes, false);
            decrypted_string = ByteConverter.GetString(decrypted_bytes);
            Console.WriteLine("Encrypted by RSACSP, decrypted by RSACSP,     result = " + decrypted_string);

            decrypted_bytes = rsaManaged.Decrypt(encrypted_bytes, false);
            decrypted_string = ByteConverter.GetString(decrypted_bytes);
            Console.WriteLine("Encrypted by RSACSP, decrypted by RSAManaged, result = " + decrypted_string);
        }
    }
}

