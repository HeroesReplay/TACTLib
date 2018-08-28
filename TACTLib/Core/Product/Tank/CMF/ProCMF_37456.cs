﻿using static TACTLib.Core.Product.Tank.CMFCryptHandler;
using static TACTLib.Core.Product.Tank.ContentManifestFile;

namespace TACTLib.Core.Product.Tank.CMF {
    [CMFMetadata(AutoDetectVersion = true, Product = TACTLib.Product.Overwatch)]
    public class ProCMF_37456 : ICMFEncryptionProc {
        public byte[] Key(CMFHeader header, int length) {
            byte[] buffer = new byte[length];

            uint kidx = Keytable[header.DataCount & 511];
            uint increment = kidx % 61;
            for (uint i = 0; i != length; ++i) {
                buffer[i] = Keytable[kidx % 512];
                kidx += increment;
            }

            return buffer;
        }

        public byte[] IV(CMFHeader header, byte[] digest, int length) {
            byte[] buffer = new byte[length];

            uint kidx = (uint) (digest[7] + (ushort) (header.DataCount & 511));
            uint increment = (uint)header.EntryCount + digest[header.EntryCount % SHA1_DIGESTSIZE];
            for (int i = 0; i != length; ++i) {
                kidx += increment;
                buffer[i] = digest[kidx % SHA1_DIGESTSIZE];
            }

            return buffer;
        }

        private static readonly byte[] Keytable = {
            0xB9, 0xF4, 0x31, 0xD5, 0xFC, 0xDF, 0xA6, 0x15, 0xCF, 0x19, 0xE8, 0x8C, 0x5A, 0xCA, 0x08, 0x0B,
            0xA2, 0x2C, 0x79, 0x19, 0x17, 0xD4, 0x80, 0xEA, 0x8D, 0x1C, 0x5A, 0x60, 0x22, 0x04, 0x6A, 0xFD,
            0x70, 0xAC, 0x56, 0x7F, 0xA7, 0x7F, 0x83, 0x23, 0xDF, 0xCF, 0x12, 0xA3, 0x3D, 0x5A, 0x18, 0xB1,
            0x2D, 0xDA, 0x84, 0xD2, 0x95, 0xA1, 0xCA, 0xED, 0x17, 0x4F, 0x12, 0x08, 0x77, 0xB8, 0x4C, 0x71,
            0xA3, 0x5A, 0x74, 0x84, 0x2A, 0x57, 0xC7, 0x82, 0x89, 0x95, 0x16, 0x45, 0x7D, 0x14, 0x52, 0x32,
            0x5F, 0xF3, 0x4C, 0xB2, 0x95, 0x8B, 0xCB, 0x60, 0x13, 0xE5, 0x02, 0xF4, 0x03, 0xB5, 0xC9, 0x72,
            0xF9, 0xF1, 0xC1, 0x6B, 0xD0, 0x10, 0xE6, 0x81, 0x4E, 0xAC, 0x8F, 0x88, 0x55, 0x46, 0xCF, 0xDC,
            0x1D, 0x04, 0x2E, 0x81, 0x3C, 0x07, 0x9A, 0x1A, 0x79, 0xEF, 0x46, 0x4C, 0x73, 0x76, 0x5F, 0x0B,
            0xB2, 0xA5, 0xC0, 0x3D, 0xBD, 0x1F, 0x41, 0xE2, 0xFA, 0x97, 0x53, 0x21, 0x63, 0x95, 0x74, 0xF4,
            0xDC, 0x2F, 0x7F, 0x59, 0x51, 0x0F, 0x4A, 0x94, 0x61, 0xD5, 0x7E, 0x0E, 0x47, 0x7D, 0x33, 0x23,
            0xB2, 0x12, 0x6E, 0x1F, 0x07, 0x08, 0x2C, 0x52, 0x77, 0x41, 0x6F, 0xB5, 0x79, 0xC6, 0xAF, 0x7B,
            0x81, 0x9F, 0xFF, 0x6B, 0x8A, 0x0B, 0xF8, 0x2B, 0x48, 0x67, 0xB6, 0xF3, 0x16, 0x7F, 0x1B, 0x7C,
            0x76, 0x63, 0x3F, 0x1D, 0x4E, 0x3C, 0x69, 0x81, 0x26, 0xBA, 0xC7, 0x46, 0x7A, 0xFC, 0x53, 0x4B,
            0x4F, 0x0C, 0xD8, 0xDD, 0xCC, 0x0F, 0x93, 0x0B, 0x6D, 0xD7, 0x6C, 0x69, 0xDB, 0x61, 0x5C, 0xB0,
            0x78, 0x47, 0x47, 0x38, 0xDC, 0x1F, 0xE4, 0x3F, 0x41, 0x77, 0x1D, 0x91, 0xEA, 0x85, 0x9E, 0x8A,
            0x0A, 0x86, 0x7C, 0xB8, 0x7B, 0xA1, 0xC2, 0xD8, 0x3F, 0x39, 0x58, 0xEE, 0x88, 0xF7, 0x48, 0x68,
            0xF0, 0xB9, 0xB6, 0xEF, 0x39, 0xEE, 0xDB, 0xF2, 0x78, 0x2D, 0x4C, 0xEA, 0xAD, 0xC3, 0xCC, 0x8D,
            0xA0, 0xD3, 0x44, 0xD5, 0xC4, 0xA7, 0x6F, 0xA1, 0x89, 0xD9, 0xDC, 0xFC, 0xB4, 0xD5, 0xB0, 0xDB,
            0x4C, 0xFD, 0x98, 0xF9, 0x1F, 0xC8, 0xA0, 0xC1, 0x16, 0xA7, 0x1F, 0xFB, 0x07, 0xC4, 0xDF, 0x00,
            0x1D, 0x95, 0xAC, 0xE2, 0x25, 0x83, 0xAE, 0x8F, 0x4E, 0xAF, 0xC8, 0x1C, 0xDB, 0x97, 0x32, 0xF9,
            0x25, 0xE0, 0x42, 0x4C, 0xD1, 0x69, 0x57, 0x6E, 0x67, 0xE2, 0x33, 0x57, 0xFF, 0xB2, 0x8C, 0x35,
            0x97, 0x46, 0x0A, 0xED, 0x94, 0x8F, 0x4E, 0xE0, 0x39, 0x22, 0x83, 0xDD, 0x80, 0x49, 0xAD, 0x1F,
            0xE4, 0xC1, 0x37, 0x6C, 0x78, 0x6A, 0x8F, 0x6B, 0x28, 0x7A, 0x9A, 0x9A, 0x6F, 0x56, 0xB6, 0x77,
            0x5F, 0xC6, 0x5D, 0x7B, 0x3D, 0xFF, 0xDC, 0x7C, 0xE2, 0x80, 0xF1, 0x8B, 0x9D, 0x02, 0xF3, 0x29,
            0x54, 0x53, 0x1C, 0x19, 0x19, 0xB1, 0x34, 0xE3, 0x05, 0xF2, 0xB3, 0xE4, 0x68, 0x22, 0x79, 0xF8,
            0xE9, 0x06, 0xC1, 0xC5, 0x14, 0x57, 0xB2, 0xF9, 0xA2, 0xF4, 0x65, 0xAC, 0xEA, 0xE4, 0x13, 0xF4,
            0x51, 0x50, 0x39, 0xFB, 0x9F, 0xCE, 0xD8, 0xBE, 0x59, 0x6F, 0xB6, 0x95, 0xB0, 0x9E, 0x79, 0xCE,
            0x79, 0xAE, 0x48, 0x86, 0x77, 0x25, 0x4C, 0x83, 0x6B, 0xE2, 0xF0, 0xB3, 0x02, 0x32, 0x3A, 0x7F,
            0x4B, 0x6E, 0x81, 0xDE, 0x9C, 0xA6, 0xBA, 0x85, 0xF6, 0xD9, 0x43, 0xDA, 0x20, 0x34, 0x78, 0xAD,
            0xD1, 0xDB, 0xBD, 0x6F, 0xFD, 0x91, 0xA1, 0x8A, 0x32, 0x5A, 0xBE, 0x57, 0x4D, 0x92, 0xE3, 0xFC,
            0x3D, 0x2D, 0x69, 0x62, 0xAA, 0x86, 0xD6, 0xA0, 0xBF, 0x37, 0xD9, 0x6A, 0x4A, 0x44, 0xD8, 0xA2,
            0x8F, 0xA7, 0xC9, 0x7E, 0x6D, 0x78, 0x92, 0x78, 0x0A, 0x4F, 0xBD, 0x5E, 0xC3, 0x52, 0x5A, 0x2A
        };
    }
}