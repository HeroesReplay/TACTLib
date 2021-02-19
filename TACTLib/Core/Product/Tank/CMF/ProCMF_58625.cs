﻿using static TACTLib.Core.Product.Tank.ManifestCryptoHandler;
using static TACTLib.Core.Product.Tank.ContentManifestFile;

namespace TACTLib.Core.Product.Tank.CMF {
    [ManifestCrypto(AutoDetectVersion = true, Product = TACTProduct.Overwatch)]
    public class ProCMF_58625 : ICMFEncryptionProc
    {
        public byte[] Key(CMFHeader header, int length)
        {
            byte[] buffer = new byte[length];

            uint kidx = Keytable[header.m_buildVersion & 511];
            for (uint i = 0; i != length; ++i)
            {
                buffer[i] = Keytable[SignedMod(kidx, 512)];
                kidx += 3;
            }
            
            return buffer;
        }

        public byte[] IV(CMFHeader header, byte[] digest, int length)
        {
            byte[] buffer = new byte[length];

            uint kidx = (uint) length * header.m_buildVersion;
            uint okidx = kidx;
            for (int i = 0; i != length; ++i)
            {
                buffer[i] = Keytable[SignedMod(kidx, 512)];
                if ((digest[6] & 1) != 0)
                    kidx += 37;
                else
                    kidx += okidx & 61;

                buffer[i] ^= digest[SignedMod(kidx - i, SHA1_DIGESTSIZE)];
            }

            return buffer;
        }

        private static readonly byte[] Keytable = {
            0x91, 0x42, 0xF4, 0x03, 0xD9, 0xD7, 0x95, 0x94, 0x80, 0x5C, 0xB7, 0xB4, 0xD2, 0xC5, 0x42, 0x97, 
            0x98, 0xE7, 0x26, 0x8A, 0x64, 0xE6, 0xD2, 0xD5, 0x4F, 0x0C, 0xB5, 0xC6, 0x49, 0x0E, 0x00, 0xFC, 
            0x8A, 0x3B, 0xB3, 0x55, 0x7A, 0x01, 0xB7, 0xCF, 0xE5, 0x40, 0xE8, 0xB5, 0xD7, 0x79, 0x95, 0x28, 
            0xBD, 0x86, 0xEF, 0x5C, 0x0C, 0x43, 0xCC, 0xD3, 0xCE, 0xA4, 0x8B, 0x42, 0x3D, 0x63, 0xEE, 0x4B, 
            0xDC, 0xF0, 0xFE, 0x61, 0xED, 0x00, 0x38, 0x78, 0x26, 0x7B, 0x8B, 0x97, 0x58, 0x7D, 0xB3, 0x8D, 
            0xFA, 0xDE, 0x0A, 0xA6, 0x1F, 0xC9, 0xC4, 0xBB, 0xCA, 0x8F, 0x32, 0x62, 0x3E, 0x35, 0xEF, 0x25, 
            0xCA, 0x52, 0x1C, 0x85, 0x12, 0x36, 0x90, 0x10, 0x04, 0x7E, 0x0F, 0xB1, 0xE4, 0x22, 0x96, 0x3C, 
            0x3B, 0xE0, 0xC8, 0xD9, 0xDA, 0x31, 0xF0, 0xD1, 0x4E, 0x5A, 0xAB, 0x3B, 0x62, 0xDE, 0x95, 0xA8, 
            0x41, 0xB6, 0xA9, 0xE4, 0x79, 0x61, 0x87, 0x1C, 0x74, 0xBF, 0x90, 0xC8, 0x19, 0x06, 0x9B, 0xE7, 
            0x33, 0x5D, 0x1A, 0x0F, 0xBE, 0x5E, 0x38, 0xBE, 0xD6, 0xE7, 0x2A, 0xBB, 0x20, 0x68, 0x56, 0xB5, 
            0xA7, 0x1E, 0x73, 0x8E, 0xEA, 0x92, 0x6E, 0xEA, 0xFD, 0xA1, 0x50, 0xD5, 0xA3, 0xE9, 0x53, 0x54, 
            0xC7, 0x01, 0x8D, 0x60, 0xCD, 0xE6, 0x09, 0xDB, 0xF4, 0x10, 0x43, 0x6E, 0xD4, 0xC9, 0xBF, 0xF7, 
            0xAC, 0x7B, 0x85, 0x18, 0xFE, 0xFC, 0x1F, 0xB7, 0x7A, 0x36, 0xD6, 0x0F, 0x27, 0xB6, 0x1D, 0x7B, 
            0x7C, 0x37, 0xB9, 0xD4, 0xC7, 0x91, 0xAC, 0x1B, 0x45, 0xE6, 0xD4, 0x6F, 0xF9, 0xF1, 0xD9, 0xC3, 
            0xB9, 0xA8, 0x03, 0x88, 0x88, 0x60, 0xFB, 0xEE, 0x4F, 0x96, 0x44, 0xC0, 0x41, 0xFC, 0x6E, 0xA2, 
            0x6D, 0x87, 0x8E, 0x89, 0x01, 0xF1, 0x1B, 0x3F, 0xD0, 0x9B, 0x0F, 0x1D, 0xA6, 0x28, 0x1C, 0xBC, 
            0x37, 0x4B, 0xF1, 0x44, 0x96, 0x2B, 0xF7, 0x2E, 0xCE, 0x55, 0xF0, 0x22, 0x97, 0x9A, 0x9E, 0x79, 
            0x4F, 0x35, 0x3D, 0xE2, 0xD9, 0x6B, 0x78, 0x92, 0xE8, 0x1A, 0x08, 0x65, 0xFA, 0x9D, 0x16, 0x4E, 
            0x47, 0xAE, 0x93, 0xEE, 0x9E, 0x84, 0x1C, 0x86, 0x22, 0x6F, 0x03, 0x6A, 0x72, 0xDC, 0x4A, 0x00, 
            0x1D, 0x7C, 0xDB, 0x31, 0x34, 0xFB, 0x68, 0xE3, 0x5C, 0x7C, 0xAF, 0xB6, 0x6D, 0x54, 0x32, 0xCC, 
            0xF3, 0xFB, 0xC0, 0x95, 0x22, 0xDE, 0xE3, 0xC0, 0x7D, 0xAB, 0x86, 0x24, 0x86, 0x3C, 0xF0, 0x4B, 
            0x16, 0x78, 0x89, 0x2D, 0x7A, 0x6B, 0x8E, 0x8A, 0x24, 0x17, 0x12, 0x80, 0xA6, 0x7E, 0x5E, 0x55, 
            0xED, 0xE3, 0xD2, 0x5E, 0x66, 0xA9, 0xD6, 0x2E, 0xFF, 0xBA, 0x4B, 0xA5, 0x4E, 0xFD, 0x70, 0x4F, 
            0x5E, 0x64, 0xA6, 0x99, 0xF6, 0x64, 0x62, 0xB5, 0xB8, 0xE0, 0xB9, 0x0B, 0xC6, 0x2E, 0xA1, 0x13, 
            0xA2, 0xAD, 0x8A, 0xAF, 0xC8, 0xCB, 0x47, 0x18, 0xC0, 0x89, 0x4F, 0xE1, 0x62, 0xE9, 0x28, 0x33, 
            0xDF, 0x12, 0xAF, 0x3F, 0xAD, 0x6D, 0x19, 0x11, 0x53, 0x07, 0x38, 0x70, 0x37, 0x8B, 0x2E, 0x50, 
            0xF3, 0xE3, 0x81, 0x34, 0xA5, 0x4F, 0x6A, 0x4C, 0xF7, 0x17, 0x98, 0x81, 0xD8, 0xDA, 0x9E, 0x8F, 
            0x94, 0x94, 0x54, 0xE5, 0xB2, 0x92, 0x58, 0x6B, 0x4B, 0x76, 0xEA, 0x0D, 0xD0, 0x8A, 0x70, 0x25, 
            0x74, 0x49, 0x54, 0xBA, 0x9E, 0x15, 0x15, 0x06, 0xF2, 0x4B, 0x96, 0x7A, 0x07, 0x91, 0xF9, 0xDE, 
            0xC2, 0x47, 0x0D, 0x9E, 0x58, 0x34, 0xF2, 0xDD, 0xCF, 0x35, 0x7F, 0xA0, 0xAE, 0x15, 0x08, 0x60, 
            0x1E, 0x18, 0xB4, 0x76, 0x39, 0xA3, 0x26, 0xF3, 0x8B, 0x29, 0x54, 0x06, 0xBB, 0xF8, 0x17, 0x7C, 
            0xC8, 0xF5, 0x85, 0x95, 0xBC, 0x1E, 0x58, 0x73, 0x5A, 0x47, 0xF9, 0x51, 0x19, 0xF3, 0xE4, 0xA8
        };
    }
}