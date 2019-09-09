namespace EncryptDecryptFiles
{
    using Syngenta.SIP.Implementation.Service;
    using System;
    using System.IO;

    /// <summary>
    /// Defines the <see cref="Program" />
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// The Main
        /// </summary>
        /// <param name="args">The args<see cref="string[]"/></param>
        internal static void Main(string[] args)
        {

            string sourceDirectory = @"d:\temp\sipdocsfromdev";
            string targetNormalDirectory = @"d:\temp\sipdocsNormal";
            string targetDirectory = @"d:\temp\sipdocstoprod";
            string sourceKey = @"<RSAKeyValue>
<Modulus>0yWMMty8aj1rKxEjMyJb1+Zn+X5Q4yDLrqITWN2p9FtT628s9Eq3u5jEmunDV/PBrnZJDU1zU+YLCFhcAXNTK3k4dA8wjalXpqaRaPRoyAT/0wG5vk4zB11q+jZiP4AWN2kFpcKaoWT32p3RKmShCRxPfA25ITPkROcUJ2fNGgs=
</Modulus>
<Exponent>AQAB</Exponent>
<P>2HeRjG1m9fYKMjCMTNG51VBCj7IqM+MU4u63PMspxZhxoMjV1Z3PeE4MVUGT40GNRbr8MrkDi6MptpaH/n5ZGQ==</P>
<Q>+bU8bAZ78J8uqmoMdPSsOCPhy2L1O8bCuI3AQMUw3LV6vPh2ce7Fe9Qsss3n6h+Kc355b5mww80acBRrfeScww==</Q>
<DP>AgFBgNp2jKqlt4dJlWCP5xDiPPB6gZaF2AA8BD0heuoJi2Kw0FSw2j1m0ddUplMyGsY7lcCu7rkpJr7ILaw/6Q==</DP>
<DQ>40QXBP51yQ8nysQQKFYTdFj8BT9AkTIaQRwKUSjnisjFN74vFwzIwiXetLIAlC42tVzzqyXHIr3GhSGgn4j1Pw==</DQ>
<InverseQ>TrQrLQFeRe1GSNaLZkiEx/4nk6Ui9mWUKbyvxd4DeVbWkX7hTiThfqe41hoaeBQzuCYV+uUSIqwY7AFCXGY1Iw==</InverseQ>
<D>CyifVarLvFOdn8uKkC+/KdRP52QRE0fO69495oLkcfkHoNghyJFQT3Y94a/79mB3+mfe6XiP4T21lI3S4vK80OU2nOLzupSLYPD8/bjHUyUZeTPuXEUHfAIkQD1TPmSoKqsgRBhYQDkQWTdIYpk0tGWXquQvKA8UwFtq0+cbl2k=</D>
<AESKeyValue>
    <Key>0iHWs84ctAgNLceojK4gWA==</Key>
    <IV>P1KzXBbCYbrodrXiWzUxlA==</IV>
</AESKeyValue>
</RSAKeyValue>";
            string targetKey = @"<RSAKeyValue>    <Modulus>8S01KGiTbWmWB2w7CiFT8S2pUdZZqW4gl2c1tRNFCAG9dvQwzX/i71PNVLRyraWvzSU80MAtu3RUFtKgjmSdUHHh1BZpegaoVgocDfZHxr2IIYCz0+QQSp2zkoL+sAXfUikCNSM0sS4b/ylgi5bEKLcio9LG6G8N2tkGv7WK7Sk=</Modulus>    <Exponent>AQAB</Exponent>    <P>/1Y91lvmOKCOTYPlDoaXY085WU+9xc+2ieoW52HsvX/+/nG8VGd3U1+kx2pN7n7su/QnB8o3ni66aBMvO2svmQ==</P>    <Q>8c2NP8GgOACOO42cBlFvGtmvNBCaJveG2yXv4SiShJypjXMhe9F/kM8tQx1f7rprAHHwRiwrfDFP3qtA1J1kEQ==</Q>    <DP>/nLV6rDoCpRNWwfOEpyjbm67qrnhVqf4sxUZP/mA7xMHbE6xK7dz3RzZ23OfN8U6as8SuhuVM0hDsIU1s1PMOQ==</DP>    <DQ>R7P2kNNwGWtAwjQp8k2vrryikcPikm+QU+gaDCCl4iE6vjFH2pmzRFPLTj2ltvscw9MFtyld88QDtTn/TY0moQ==</DQ>    <InverseQ>XZEZODHvMtP3F25ydhbrtYoN3PS2WufgDWbDk4TDIsR+SJki6G+MAn/hII6NGGKr8pNoCJ0OM4jDmhH03pptLA==</InverseQ>    <D>GoM6P0Vzw4jibdF3kg8E39QBs5XpSGdrXMmDTXMfWRh74ApLHSQsadvgDCpEGxPGRjqMO3SM8nkAz3t7G/wykNE1Ut79+nqpM6icZM1ZkBxHVwbkzBFsre/mhrNKF5xFl4jGk3VzZ9C9qoKkG4fMm1mdBaOKGsYqz21tVpgewsE=</D>    <AESKeyValue>      <Key>7fm/rtT0lWbcrVENAcKSdA==</Key>      <IV>MsEZGhAxwPGTCUJ8RKQTIw==</IV>    </AESKeyValue>  </RSAKeyValue>";
            CryptoService cryptoService = new CryptoService();
            if (!Directory.Exists(sourceDirectory))
            {
                return;
            }

            if (!Directory.Exists(targetNormalDirectory))
            {
                Directory.CreateDirectory(targetNormalDirectory);
            }

            if (!Directory.Exists(targetDirectory))
            {
                Directory.CreateDirectory(targetDirectory);
            }
            try
            {
                var files = Directory.GetFiles(sourceDirectory, "*.pdf");
                foreach (var file in files)
                {
                    try
                    {
                        var normarlFileData = cryptoService.Decrypt(sourceKey, File.ReadAllBytes(file));
                        File.WriteAllBytes(Path.Combine(targetNormalDirectory, Path.GetFileName(file)), normarlFileData);
                        continue;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Failed for {0} - {1}", Path.GetFileName(file), ex.Message);
                    }

                    try
                    {
                        var normarlFileData = cryptoService.Decrypt(targetKey, File.ReadAllBytes(file));
                        File.WriteAllBytes(Path.Combine(targetNormalDirectory, Path.GetFileName(file)), normarlFileData);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Failed for {0} - {1}", Path.GetFileName(file), ex.Message);
                    }
                }

                files = Directory.GetFiles(targetNormalDirectory, "*.pdf");
                foreach (var file in files)
                {
                    try
                    {
                        var encryptedFileData = cryptoService.Encrypt(targetKey, File.ReadAllBytes(file));
                        File.WriteAllBytes(Path.Combine(targetDirectory, Path.GetFileName(file)), encryptedFileData);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Failed for {0} - {1}", Path.GetFileName(file), ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {

            }

            Console.WriteLine("done");
            Console.ReadLine();
        }
    }
}
