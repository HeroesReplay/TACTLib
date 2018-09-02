﻿using static TACTLib.Core.Product.Tank.CMFCryptHandler;
using static TACTLib.Core.Product.Tank.ContentManifestFile;

namespace TACTLib.Core.Product.Tank.CMF {
    [CMFMetadata(AutoDetectVersion = true, Product = TACTProduct.Overwatch)]
    public class ProCMF_44916 : ICMFEncryptionProc {
        public byte[] Key(CMFHeader header, int length) {
            byte[] buffer = new byte[length];

            uint kidx = Keytable[header.DataCount & 511];
            uint increment = header.BuildVersion * (uint)header.DataCount % 7;
            for (int i = 0; i != length; ++i) {
                buffer[i] = Keytable[SignedMod(kidx, 512)];
                kidx += increment;
            }

            return buffer;
        }

        public byte[] IV(CMFHeader header, byte[] digest, int length) {
            byte[] buffer = new byte[length];

            uint kidx = Keytable[digest[7] * Keytable[0] & 511];
            for (int i = 0; i != length; ++i) {
                buffer[i] = Keytable[SignedMod(kidx, 512)];
                kidx = header.BuildVersion - kidx;
                buffer[i] ^= digest[SignedMod(kidx + i, SHA1_DIGESTSIZE)];
            }

            return buffer;
        }

        private static readonly byte[] Keytable = {
            0xEE, 0xA0, 0xAF, 0x29, 0xC0, 0x55, 0x22, 0xF6, 0x9F, 0x07, 0x07, 0x17, 0xA5, 0x59, 0xF3, 0x87, 
            0x4B, 0x97, 0xAE, 0x8A, 0x22, 0x9A, 0x1F, 0xA7, 0x1E, 0x72, 0xF3, 0xC3, 0x75, 0xC3, 0x90, 0xA8, 
            0xE6, 0x10, 0xE5, 0xA5, 0x09, 0xEF, 0x36, 0x2A, 0xE8, 0x49, 0x85, 0x0B, 0xDD, 0x62, 0x31, 0x8A, 
            0xA7, 0xE8, 0x11, 0x89, 0xAA, 0xAE, 0x79, 0x91, 0xF7, 0x5D, 0x6F, 0x74, 0x94, 0x2E, 0x49, 0x5C, 
            0x4C, 0x76, 0xD2, 0x06, 0x17, 0xCC, 0x03, 0x37, 0x92, 0xF7, 0x29, 0x08, 0xCD, 0x77, 0x5B, 0xBD, 
            0x7F, 0x97, 0x65, 0xC2, 0x2D, 0x1E, 0xE7, 0x18, 0xEF, 0x90, 0x98, 0x12, 0x75, 0xB0, 0xC2, 0x64, 
            0x05, 0x47, 0x3B, 0x03, 0x8E, 0x78, 0xBB, 0xB5, 0xE2, 0xF2, 0x20, 0x1C, 0x7C, 0xA9, 0x78, 0xF7, 
            0x27, 0x89, 0xCD, 0xF0, 0x16, 0xA7, 0x48, 0x65, 0x88, 0x61, 0xEA, 0x5F, 0xC3, 0x2E, 0x69, 0xA3, 
            0x38, 0x59, 0x3B, 0x21, 0x85, 0x1B, 0x1E, 0x90, 0xA5, 0x93, 0xA2, 0xB3, 0x78, 0x0E, 0x19, 0x27, 
            0x20, 0x8B, 0x0F, 0xAB, 0x65, 0x3D, 0x17, 0x38, 0x80, 0x5E, 0x50, 0x77, 0x5B, 0x14, 0xCF, 0xDA, 
            0xC3, 0x6A, 0xCE, 0x18, 0xCD, 0x71, 0xAB, 0xE7, 0xDC, 0xDE, 0xA9, 0xB8, 0xB9, 0x3A, 0x68, 0x76, 
            0x0D, 0x70, 0xDA, 0xAD, 0x98, 0x3D, 0x18, 0x86, 0xFB, 0x3D, 0xF4, 0x63, 0x10, 0x70, 0x52, 0x3B, 
            0xB2, 0x14, 0xA2, 0xD8, 0xDE, 0x37, 0x83, 0x8D, 0xE6, 0x26, 0x20, 0x30, 0xB8, 0x6F, 0xF7, 0x7B, 
            0x9C, 0xEC, 0x1E, 0xFB, 0x50, 0x35, 0x8B, 0xF4, 0xC4, 0x2E, 0xCC, 0xD5, 0xFF, 0x3D, 0x25, 0x6E, 
            0x0A, 0x88, 0x5B, 0x79, 0x13, 0x59, 0xAD, 0x97, 0x42, 0xF5, 0x6B, 0x01, 0xAC, 0x47, 0xAD, 0xA5, 
            0xF5, 0xB1, 0x86, 0xDD, 0x0E, 0x29, 0x6A, 0xC5, 0xC4, 0x64, 0x65, 0x79, 0x59, 0x75, 0xF2, 0xE7, 
            0xD2, 0x71, 0xCB, 0x1D, 0x3D, 0x70, 0x85, 0x19, 0x8F, 0x7D, 0x73, 0x0C, 0xDA, 0xE4, 0xF9, 0x6D, 
            0x90, 0x60, 0x64, 0x27, 0x66, 0xE9, 0x70, 0x23, 0x5C, 0xA0, 0x6B, 0xD2, 0x18, 0x20, 0xA3, 0xAA, 
            0x87, 0x41, 0xBD, 0x2C, 0x1F, 0x9E, 0x89, 0x17, 0xE0, 0x2C, 0xDC, 0xE2, 0x28, 0x58, 0xAD, 0x3A, 
            0x96, 0xA2, 0xA2, 0xF4, 0x02, 0x4C, 0xFA, 0x61, 0x43, 0x9A, 0x1A, 0x2B, 0x87, 0xF2, 0xD2, 0xC9, 
            0xC3, 0xB9, 0xEB, 0xA2, 0x68, 0xF8, 0xC6, 0x79, 0x9F, 0xFD, 0xCB, 0x21, 0x6B, 0xF1, 0x14, 0x0F, 
            0xD4, 0x56, 0x6A, 0xDC, 0x6B, 0xF5, 0x4C, 0x81, 0x69, 0xC4, 0xAE, 0xF5, 0xF4, 0x2A, 0xC5, 0x52, 
            0xC5, 0xD9, 0x32, 0xFE, 0x1F, 0xE5, 0x2D, 0x72, 0xEB, 0x8F, 0xFD, 0xD8, 0x7C, 0x2B, 0x0C, 0xDC, 
            0xEF, 0xEE, 0xE0, 0xA2, 0x7E, 0x8C, 0xD4, 0x3E, 0xBF, 0xD8, 0xCF, 0xAA, 0x19, 0x64, 0x5D, 0x57, 
            0xD9, 0xA5, 0x4B, 0xC9, 0x83, 0x7C, 0x5C, 0x86, 0xBA, 0x95, 0x9E, 0x61, 0x93, 0xD1, 0x4B, 0x1A, 
            0xF5, 0x0A, 0xD0, 0xE8, 0xBA, 0xED, 0xD7, 0xB7, 0xAD, 0xF2, 0x3E, 0x2A, 0xD2, 0x52, 0x68, 0x58, 
            0xEC, 0x2F, 0xBD, 0xDE, 0x81, 0xA7, 0x5B, 0x74, 0x80, 0x83, 0x2F, 0xC1, 0x9B, 0x52, 0x36, 0x5F, 
            0x91, 0x8A, 0x6C, 0x6F, 0x9E, 0x1D, 0xD8, 0x52, 0x3C, 0xCC, 0x71, 0x5A, 0x9B, 0xF7, 0x8C, 0x76, 
            0x44, 0x3A, 0x30, 0x75, 0x1A, 0xD5, 0x29, 0x93, 0x2E, 0xD7, 0xA5, 0x5C, 0x0C, 0x4E, 0xA4, 0x4F, 
            0xA3, 0x43, 0x21, 0xC9, 0x17, 0x8D, 0xA0, 0xC1, 0x3B, 0x6A, 0xE8, 0x05, 0x9E, 0xA9, 0x07, 0x88, 
            0x8E, 0xB2, 0x82, 0x45, 0x33, 0x8C, 0x57, 0xDF, 0x46, 0x34, 0xA0, 0xDF, 0x4C, 0xD0, 0xD7, 0xE2, 
            0x1A, 0x84, 0x13, 0x67, 0x86, 0xDE, 0x94, 0xCD, 0x21, 0x5D, 0x9F, 0xA9, 0x4F, 0x17, 0x63, 0xA9
        };
    }
}