﻿using static TACTLib.Core.Product.Tank.CMFCryptHandler;
using static TACTLib.Core.Product.Tank.ContentManifestFile;

namespace TACTLib.Core.Product.Tank.CMF {
    [CMFMetadata(AutoDetectVersion = true, Product = TACTLib.Product.Overwatch)]
    public class ProCMF_39362 : ICMFEncryptionProc {
        public byte[] Key(CMFHeader header, int length) {
            byte[] buffer = new byte[length];

            uint kidx = Keytable[length * Keytable[0] % 512];
            for (int i = 0; i != length; ++i) {
                buffer[i] = Keytable[kidx % 512];
                kidx += (uint)header.EntryCount;
            }

            return buffer;
        }

        public byte[] IV(CMFHeader header, byte[] digest, int length) {
            byte[] buffer = new byte[length];

            uint kidx = Keytable[(2 * digest[13] - length) % 512];
            uint increment = header.BuildVersion * (uint)header.DataCount % 7;
            for (int i = 0; i != length; ++i) {
                buffer[i] = Keytable[kidx % 512];
                kidx += increment;
                buffer[i] ^= digest[(kidx - 73) % SHA1_DIGESTSIZE];
            }

            return buffer;
        }

        private static readonly byte[] Keytable = {
            0x4D, 0xB4, 0xC1, 0x28, 0xD3, 0x24, 0x90, 0x79, 0xB9, 0x20, 0x9D, 0x57, 0x47, 0x71, 0xC8, 0xDD,
            0x2D, 0x0A, 0x6B, 0x44, 0xE5, 0x55, 0x67, 0x47, 0xAF, 0xA7, 0x4E, 0xE5, 0x43, 0xD4, 0x8E, 0x40,
            0x1B, 0x78, 0xB1, 0x53, 0x93, 0x36, 0x23, 0xC6, 0xCF, 0x51, 0xA3, 0xD5, 0x2E, 0x3D, 0x94, 0x4B,
            0x0E, 0xC5, 0x39, 0xEC, 0xC7, 0x6E, 0x0B, 0x30, 0x34, 0x3C, 0x43, 0xB1, 0xDD, 0x7B, 0xFB, 0xDE,
            0xDE, 0x94, 0x52, 0x6B, 0x7D, 0x5A, 0xEF, 0x59, 0xE9, 0x90, 0x5E, 0xF6, 0xAF, 0x70, 0xEC, 0x0D,
            0x96, 0x45, 0x42, 0xFD, 0x21, 0x43, 0xB0, 0x7F, 0x6D, 0xF3, 0x90, 0x1E, 0x46, 0x49, 0x27, 0xA8,
            0x08, 0x5E, 0xEB, 0x9F, 0x6A, 0xBA, 0x71, 0x29, 0x61, 0x9D, 0x92, 0xC4, 0x38, 0xC1, 0x6B, 0xF0,
            0x0C, 0xD2, 0xC6, 0x12, 0x12, 0x69, 0x77, 0xDB, 0x67, 0x5B, 0x92, 0x7A, 0x05, 0xFE, 0xDA, 0xFC,
            0x06, 0xE7, 0x54, 0xC9, 0x26, 0x04, 0x27, 0xE0, 0x78, 0xEE, 0xCD, 0xE7, 0xBE, 0xE4, 0xF8, 0x79,
            0x86, 0xD6, 0x21, 0x48, 0x1B, 0x11, 0x9F, 0x79, 0xFD, 0xD4, 0x74, 0x84, 0xCC, 0x09, 0x4D, 0xEF,
            0x37, 0xC3, 0x9C, 0xE3, 0x54, 0xE3, 0xD9, 0xD8, 0xA9, 0x1E, 0x83, 0xDF, 0xD1, 0x36, 0x74, 0xB5,
            0x86, 0xCF, 0x7D, 0x1A, 0xC9, 0x6F, 0x99, 0xD0, 0x55, 0xC0, 0x37, 0x17, 0x90, 0xF3, 0x6B, 0x43,
            0xA7, 0x41, 0x10, 0x77, 0x46, 0x8F, 0xFF, 0x53, 0xE1, 0x81, 0x41, 0xB2, 0xEB, 0x8C, 0x41, 0x7E,
            0xCC, 0x53, 0x3A, 0xD7, 0xA6, 0xB1, 0xA3, 0x4C, 0x72, 0x79, 0x94, 0x35, 0xBD, 0x03, 0x0E, 0x56,
            0x38, 0xCD, 0x67, 0x18, 0xE7, 0x2F, 0xDD, 0x27, 0xBD, 0xF3, 0x43, 0xEE, 0xEF, 0xCB, 0xD3, 0x59,
            0x3F, 0x44, 0x12, 0x11, 0x76, 0xFD, 0xD5, 0x2B, 0xA5, 0xD3, 0x9B, 0x4C, 0x9F, 0xD8, 0x13, 0xED,
            0x8E, 0xD1, 0xBB, 0xE1, 0xD6, 0xDF, 0x05, 0x81, 0x1A, 0x60, 0x33, 0x32, 0x0E, 0xB7, 0xF8, 0xD6,
            0x0F, 0x2E, 0x88, 0x4E, 0x25, 0xAF, 0xDB, 0xB8, 0xA8, 0xDD, 0x37, 0x52, 0xAB, 0x3F, 0xB0, 0xB2,
            0x48, 0x1B, 0x0A, 0xCD, 0xC9, 0xC2, 0xDE, 0x65, 0xCA, 0x5A, 0xFB, 0x36, 0x80, 0xCA, 0x68, 0xBE,
            0x52, 0x3B, 0xBF, 0x2D, 0xC1, 0x26, 0x47, 0xEB, 0xFC, 0x17, 0xA2, 0xD3, 0x90, 0x6C, 0xDB, 0x5F,
            0xCF, 0x6D, 0x1C, 0x86, 0x78, 0x5F, 0x40, 0x3B, 0xFE, 0x9C, 0xE0, 0x85, 0x0B, 0x65, 0x7D, 0x36,
            0x01, 0x6B, 0x5A, 0x9D, 0x7B, 0x3D, 0x3F, 0xE2, 0x63, 0x6A, 0xEA, 0x73, 0xD1, 0x57, 0xA7, 0x07,
            0x4B, 0x18, 0x14, 0x37, 0xB1, 0x57, 0xDE, 0x5B, 0xF7, 0x5B, 0xB7, 0xF1, 0x6D, 0xCC, 0x2F, 0xFA,
            0xF3, 0x36, 0x06, 0xC1, 0x6E, 0x7A, 0x74, 0x7C, 0x02, 0xE6, 0xA0, 0x41, 0x37, 0x8B, 0xCF, 0xE5,
            0x16, 0x34, 0xBB, 0xC5, 0xAA, 0x40, 0x0B, 0xA2, 0x2C, 0x36, 0xB2, 0xB7, 0x77, 0x26, 0x94, 0x09,
            0x5C, 0xA2, 0x74, 0x39, 0x74, 0x08, 0x27, 0xE7, 0xF7, 0x1D, 0xFE, 0x5B, 0x1B, 0xC8, 0xD5, 0xDC,
            0x3B, 0xBE, 0x72, 0xDE, 0x6A, 0xBD, 0x0C, 0x2B, 0xAD, 0x86, 0x6A, 0x1C, 0xDA, 0xB2, 0xE4, 0x9F,
            0x9A, 0xF9, 0xE1, 0x01, 0xAC, 0x30, 0x9A, 0x18, 0x53, 0x5E, 0xB7, 0xBE, 0x75, 0x88, 0xF5, 0x64,
            0x38, 0xC2, 0x90, 0x77, 0x85, 0xCB, 0x70, 0x89, 0x15, 0x77, 0x8C, 0xE9, 0xFB, 0xA0, 0x04, 0x98,
            0xEE, 0x56, 0x29, 0xE8, 0xD2, 0xFF, 0x85, 0xCA, 0xDB, 0x7C, 0xA0, 0xE1, 0x2F, 0xF4, 0x54, 0xD2,
            0xDE, 0x71, 0xDA, 0xF3, 0x4B, 0x5A, 0xD9, 0xE8, 0x40, 0x2F, 0x1F, 0xA1, 0x64, 0x04, 0x1C, 0xA2,
            0x75, 0x5A, 0x2A, 0x6A, 0x5A, 0x34, 0x88, 0x87, 0x12, 0xBB, 0xF0, 0x7F, 0x71, 0xB8, 0x9E, 0xD3
        };
    }
}