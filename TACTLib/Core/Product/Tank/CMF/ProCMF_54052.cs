﻿using static TACTLib.Core.Product.Tank.CMFCryptHandler;
using static TACTLib.Core.Product.Tank.ContentManifestFile;

namespace TACTLib.Core.Product.Tank.CMF {
    [CMFMetadataAttribute(AutoDetectVersion = true, Product = TACTProduct.Overwatch)]
    public class ProCMF_54052 : ICMFEncryptionProc
    {
        public byte[] Key(CMFHeader header, int length)
        {
            byte[] buffer = new byte[length];

            uint kidx = Keytable[SignedMod(length * Keytable[0], 512)];
            for (int i = 0; i != length; ++i)
            {
                buffer[i] = Keytable[SignedMod(kidx, 512)];
                switch (SignedMod(kidx, 3))
                {
                    case 0:
                        kidx += 103;
                        break;
                    case 1:
                        kidx = (uint)SignedMod(4 * kidx, header.BuildVersion);
                        break;
                    case 2:
                        --kidx;
                        break;
                }
            }

            return buffer;
        }

        public byte[] IV(CMFHeader header, byte[] digest, int length)
        {
            byte[] buffer = new byte[length];

            uint kidx = (uint) (2 * digest[5]);
            for (int i = 0; i != length; ++i) {
                buffer[i] = Keytable[SignedMod(kidx, 512)];
                kidx += (uint) header.EntryCount + digest[header.EntryCount % SHA1_DIGESTSIZE];
                buffer[i] ^= digest[SignedMod(i + header.BuildVersion, SHA1_DIGESTSIZE)];
            }

            return buffer;
        }

        private static readonly byte[] Keytable = {
            0x60, 0x03, 0x09, 0x34, 0xBC, 0x31, 0x93, 0x6B, 0x72, 0x44, 0x43, 0xD1, 0xC7, 0x57, 0xA7, 0x0D, 
            0x26, 0x55, 0xB0, 0xF8, 0x15, 0x41, 0x4E, 0xBB, 0xED, 0xAB, 0x49, 0x4C, 0x58, 0x18, 0x95, 0x23, 
            0x85, 0xFA, 0x92, 0xEB, 0x56, 0x6B, 0x61, 0xC4, 0x60, 0x67, 0xDB, 0x4E, 0x6C, 0x3B, 0x94, 0x43, 
            0xFF, 0x7D, 0xF1, 0xA6, 0x28, 0x09, 0x12, 0x52, 0x73, 0xC6, 0xF3, 0x85, 0x49, 0x33, 0xEC, 0x5D, 
            0x31, 0xFF, 0x59, 0x52, 0x2E, 0xCE, 0x95, 0xEC, 0xCC, 0x52, 0x3D, 0xBC, 0xCD, 0x5F, 0xC9, 0x9B, 
            0x05, 0xC8, 0xD2, 0xA3, 0x61, 0x09, 0x9B, 0xDF, 0x0A, 0xE5, 0x0B, 0x27, 0xF9, 0x0E, 0x2A, 0xEA, 
            0xC2, 0x1F, 0x43, 0x0E, 0xF2, 0xF9, 0xC2, 0x7A, 0x30, 0x52, 0x22, 0x1E, 0xB0, 0x25, 0x97, 0x1B, 
            0x86, 0xA1, 0x26, 0x1F, 0x8A, 0x49, 0x1D, 0x47, 0xC9, 0xA7, 0x46, 0xCA, 0xF3, 0x07, 0xCB, 0xD1, 
            0x54, 0x6B, 0xE6, 0x00, 0x3E, 0xEE, 0x46, 0x82, 0xE6, 0xF1, 0x99, 0x64, 0x92, 0x42, 0xCB, 0x19, 
            0x85, 0x3B, 0xC8, 0x8D, 0xC2, 0xDB, 0x04, 0xC4, 0x10, 0x4B, 0x9B, 0xB0, 0xF2, 0x36, 0x4E, 0x6F, 
            0x65, 0x7B, 0x1C, 0xA1, 0x62, 0x3D, 0x7C, 0x62, 0x55, 0x0E, 0xC8, 0x7B, 0xD6, 0xBA, 0xB2, 0xFB, 
            0x07, 0xD9, 0x45, 0x04, 0xF0, 0x47, 0xE7, 0xC3, 0xF7, 0x02, 0x52, 0x35, 0x0D, 0x97, 0xC3, 0x19, 
            0xC0, 0x8F, 0x92, 0x10, 0xB1, 0x6B, 0xB8, 0x8D, 0xD0, 0x78, 0x46, 0x79, 0x6D, 0xA6, 0x61, 0x39, 
            0x1D, 0xF2, 0x35, 0x15, 0x3D, 0x80, 0x0E, 0x5D, 0x7F, 0x88, 0x83, 0xC3, 0xBD, 0x7F, 0x32, 0x35, 
            0x94, 0x73, 0x19, 0xDE, 0x74, 0x9C, 0x12, 0xDA, 0x75, 0x10, 0x73, 0xB2, 0x91, 0x31, 0x17, 0x63, 
            0xAD, 0xA7, 0x66, 0x74, 0x21, 0x06, 0x26, 0x90, 0x23, 0x0E, 0xE7, 0xDB, 0xDC, 0xA4, 0xF5, 0x3F, 
            0x0F, 0x43, 0x84, 0x13, 0x77, 0x5C, 0x23, 0xED, 0x8F, 0x0E, 0x4B, 0x99, 0xB4, 0x47, 0xB9, 0x17, 
            0xC7, 0x42, 0xEC, 0xCF, 0x66, 0xDD, 0x07, 0xBC, 0xD0, 0x7F, 0xEE, 0x10, 0xF6, 0x81, 0x43, 0xC5, 
            0x2A, 0x50, 0xF2, 0xF6, 0x0A, 0xD1, 0x1D, 0x44, 0x70, 0xD5, 0x63, 0x19, 0x8A, 0xB7, 0xB9, 0x90, 
            0x0C, 0x04, 0xE5, 0xBB, 0xA8, 0xC0, 0x3A, 0x3B, 0x03, 0x41, 0x0B, 0xCF, 0x80, 0xB4, 0x0A, 0xDC, 
            0x40, 0x70, 0xF0, 0x60, 0xB3, 0x59, 0x37, 0x45, 0x9D, 0x61, 0xE7, 0x95, 0x3C, 0x42, 0x0F, 0x40, 
            0x67, 0x52, 0x87, 0x01, 0xCA, 0xA5, 0x21, 0x64, 0x6F, 0xEF, 0x9C, 0xDA, 0x2E, 0x3D, 0x8D, 0xE7, 
            0x18, 0x84, 0xC3, 0x73, 0x48, 0xBA, 0x93, 0xE4, 0xBF, 0x9F, 0xF9, 0x0B, 0x97, 0x6B, 0x6B, 0xEF, 
            0xA0, 0xDD, 0x5B, 0x35, 0x09, 0x9A, 0xBF, 0x0F, 0xEE, 0x2F, 0xF8, 0x78, 0x32, 0xD0, 0x70, 0xA0, 
            0x2A, 0x2E, 0xD8, 0x9C, 0xF3, 0xD7, 0xC0, 0xB6, 0x25, 0x22, 0x2D, 0x03, 0x0A, 0xF5, 0xC1, 0x1E, 
            0xA1, 0xDE, 0x3F, 0x95, 0x80, 0xF6, 0xC8, 0x74, 0xFA, 0x56, 0xDC, 0x09, 0xD7, 0x0C, 0x56, 0x4A, 
            0x53, 0x00, 0x4E, 0x49, 0x7C, 0x5C, 0x6F, 0xAF, 0x5F, 0xD0, 0xE5, 0xCF, 0x99, 0x81, 0xD8, 0xAE, 
            0x21, 0x1F, 0xC5, 0x88, 0x01, 0xF6, 0x22, 0x0B, 0xD6, 0xAB, 0x0A, 0xBD, 0xDC, 0xBC, 0x73, 0x9D, 
            0xDA, 0xA4, 0x0F, 0xB6, 0x64, 0x06, 0x43, 0xF2, 0x8A, 0xD5, 0xD2, 0x97, 0xDE, 0x26, 0xE3, 0xC2, 
            0x4F, 0x83, 0x7B, 0x2B, 0xED, 0x49, 0xFF, 0x33, 0xE7, 0x1F, 0x96, 0xE1, 0xB1, 0x45, 0xE4, 0x50, 
            0xF1, 0xC3, 0x3C, 0x7B, 0xE9, 0x78, 0x3D, 0x2F, 0x82, 0xD1, 0x71, 0xCF, 0xBC, 0x22, 0x51, 0x4B, 
            0x6A, 0xA2, 0xFF, 0x28, 0x04, 0xF5, 0x9D, 0x48, 0xF8, 0x26, 0x81, 0x01, 0x55, 0x27, 0xF5, 0x29
        };
    }
}