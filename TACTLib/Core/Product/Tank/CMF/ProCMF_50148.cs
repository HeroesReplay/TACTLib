﻿using static TACTLib.Core.Product.Tank.CMFCryptHandler;
using static TACTLib.Core.Product.Tank.ContentManifestFile;

namespace TACTLib.Core.Product.Tank.CMF {
    [CMFMetadataAttribute(AutoDetectVersion = true, Product = TACTProduct.Overwatch)]
    public class ProCMF_50148 : ICMFEncryptionProc {
        public byte[] Key(CMFHeader header, int length) {
            byte[] buffer = new byte[length];

            uint kidx = (uint)length * header.BuildVersion;
            for (int i = 0; i != length; ++i)
            {
                buffer[i] = Keytable[SignedMod(kidx, 512)];
                kidx += 3;
            }

            return buffer;
        }

        public byte[] IV(CMFHeader header, byte[] digest, int length) {
            byte[] buffer = new byte[length];
            
            int kidx = length * (int)header.BuildVersion;
            for (int i = 0; i != length; ++i)
            {
                buffer[i] = Keytable[SignedMod(kidx, 512)];
                kidx -= 0x2B; // m_keytable[ ??? ->  0x13A ]
                buffer[i] ^= digest[SignedMod(kidx + header.DataCount, SHA1_DIGESTSIZE)];
            }

            return buffer;
        }

        private static readonly byte[] Keytable = {
            0xAC, 0x70, 0x7D, 0x5D, 0x48, 0x1D, 0x11, 0x1C, 0xE8, 0x0A, 0x13, 0xC7, 0x97, 0xD7, 0xC3, 0xDC, 
            0x6A, 0x3C, 0xC9, 0xE3, 0x8D, 0x99, 0x7D, 0x2F, 0x33, 0xAA, 0xAF, 0xA1, 0x58, 0xB7, 0xCA, 0xBD, 
            0x0E, 0x58, 0x79, 0xD7, 0xFD, 0x0A, 0x45, 0xE8, 0x04, 0x5C, 0x97, 0xF0, 0xC9, 0xA9, 0xE4, 0x21, 
            0xCC, 0xCA, 0x2D, 0xBD, 0x57, 0x3C, 0x4D, 0x4F, 0xF4, 0x6D, 0xB9, 0xA2, 0xE3, 0x80, 0x70, 0x31, 
            0x15, 0x8E, 0x0E, 0x8B, 0xC6, 0x66, 0x42, 0x14, 0x8C, 0xC7, 0xAD, 0x09, 0xB9, 0x49, 0x64, 0xEC, 
            0xD1, 0x8C, 0x3C, 0xF2, 0x49, 0xFB, 0xBC, 0xB2, 0xC9, 0xB4, 0x6A, 0xBA, 0x70, 0x93, 0x27, 0x66, 
            0x08, 0xE6, 0x3A, 0x4E, 0x43, 0x6C, 0x81, 0xDE, 0xBA, 0x8B, 0x1F, 0x9B, 0x5B, 0xD4, 0x54, 0xCF, 
            0x2A, 0xB7, 0xEE, 0x2F, 0xE4, 0xDE, 0x75, 0x5A, 0x59, 0xA4, 0x84, 0x48, 0xFD, 0x0D, 0x58, 0x83, 
            0x67, 0x76, 0x3C, 0x37, 0xF2, 0x77, 0xF8, 0x29, 0x33, 0xE4, 0x03, 0xA3, 0xDF, 0x39, 0xD7, 0x8B, 
            0x04, 0xF1, 0x80, 0xA1, 0x0C, 0xC7, 0xC4, 0xB8, 0x7A, 0x4E, 0x2D, 0x47, 0xE2, 0x75, 0x0B, 0xD5, 
            0x45, 0x7D, 0xB2, 0xB6, 0xD1, 0x67, 0xDE, 0x5C, 0x1E, 0xFF, 0xC7, 0xE3, 0x46, 0x3F, 0x8F, 0x42, 
            0x4E, 0x43, 0x61, 0x36, 0xF0, 0x80, 0x31, 0xFC, 0x55, 0xAB, 0xE7, 0x5A, 0x3B, 0xE5, 0x1C, 0x22, 
            0x1E, 0xBB, 0x86, 0x39, 0x6B, 0xE5, 0xAB, 0x32, 0xAA, 0x20, 0x5B, 0x22, 0x8D, 0x1A, 0x7E, 0x8C, 
            0x29, 0x7F, 0x6A, 0x44, 0x00, 0xAE, 0xF9, 0x5C, 0x27, 0xEB, 0x66, 0x70, 0xCA, 0x8B, 0x9F, 0x37, 
            0x89, 0x54, 0x12, 0xD8, 0xDA, 0xB5, 0xBD, 0xAB, 0xD3, 0x66, 0x1D, 0x41, 0x29, 0x48, 0x79, 0x11, 
            0x84, 0xE4, 0x1F, 0x9A, 0xCE, 0x2A, 0x35, 0xA4, 0x45, 0xC8, 0xC8, 0xD1, 0x70, 0xB0, 0xB7, 0x7A, 
            0xD9, 0x34, 0xD8, 0x7D, 0xC2, 0x7C, 0xFA, 0x5C, 0x6F, 0x7E, 0x4A, 0x96, 0x24, 0x91, 0xEF, 0xFD, 
            0xB8, 0xC8, 0xA3, 0xBF, 0x47, 0xD4, 0x00, 0x99, 0xB0, 0x60, 0x06, 0xB5, 0x2A, 0x4D, 0x8C, 0x4D, 
            0x14, 0x2E, 0x15, 0xAB, 0x2D, 0xD3, 0x78, 0x00, 0x55, 0x03, 0x4A, 0xDA, 0x4A, 0x64, 0x94, 0x38, 
            0x6E, 0x72, 0xE1, 0xE3, 0x21, 0xDC, 0x31, 0x69, 0xB1, 0xA2, 0x2B, 0x12, 0x61, 0xAD, 0xCE, 0xFC, 
            0xB0, 0x86, 0xAE, 0x02, 0x10, 0xA9, 0x65, 0x7E, 0x26, 0x23, 0xD6, 0xBB, 0xDC, 0x3F, 0x70, 0x1C, 
            0xDA, 0x8C, 0x47, 0x4F, 0xA3, 0x6F, 0xE0, 0x98, 0x04, 0x22, 0xF0, 0x49, 0x48, 0xC2, 0x6A, 0x9F, 
            0x53, 0xCD, 0x58, 0xFE, 0x07, 0x55, 0x53, 0x16, 0x2D, 0xA5, 0xDA, 0xBD, 0x3C, 0x94, 0x46, 0x1C, 
            0xCF, 0x07, 0x19, 0x4F, 0x14, 0xBC, 0xE2, 0x28, 0x9F, 0xCC, 0xC9, 0x18, 0xFB, 0x8C, 0x80, 0xBF, 
            0x46, 0x55, 0x5A, 0x19, 0x0D, 0x2C, 0x51, 0xFD, 0xD1, 0x21, 0x12, 0x02, 0x03, 0x9D, 0x96, 0x22, 
            0x99, 0xF5, 0x6D, 0xA7, 0x98, 0x51, 0x7A, 0xF3, 0x00, 0x23, 0xE7, 0x1C, 0xCD, 0x6B, 0x0D, 0xF9, 
            0x43, 0x55, 0x4F, 0x10, 0x93, 0xEB, 0x3A, 0x42, 0x8F, 0x66, 0x71, 0xD6, 0xC7, 0xFF, 0x36, 0x48, 
            0x4A, 0x3B, 0x87, 0xC9, 0x06, 0x8F, 0xE7, 0x12, 0x92, 0xDE, 0x5D, 0x95, 0x28, 0x54, 0x83, 0xD1, 
            0xE9, 0xAA, 0x32, 0xE0, 0xE7, 0x64, 0x93, 0x7B, 0x7E, 0x9F, 0x91, 0x4F, 0x8F, 0x4D, 0x30, 0x76, 
            0x6C, 0xFC, 0xF2, 0xD6, 0x28, 0x25, 0x9E, 0x24, 0x6E, 0xA2, 0x7D, 0x9A, 0x22, 0xCC, 0xFB, 0x9E, 
            0x5D, 0x11, 0x4D, 0x3D, 0x4C, 0xF1, 0x26, 0xD3, 0xBB, 0xAA, 0x71, 0x8D, 0x28, 0xA7, 0xB9, 0x3D, 
            0x13, 0xC1, 0x28, 0x86, 0x50, 0x6D, 0x8E, 0xC1, 0x71, 0x4F, 0x2C, 0x0A, 0x4F, 0xED, 0xFE, 0x13
        };
    }
}