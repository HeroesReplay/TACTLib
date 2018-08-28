﻿using static TACTLib.Core.Product.Tank.CMFCryptHandler;
using static TACTLib.Core.Product.Tank.ContentManifestFile;

namespace TACTLib.Core.Product.Tank.CMF {
    [CMFMetadata(AutoDetectVersion = true, Product = TACTLib.Product.Overwatch)]
    public class ProCMF_37646 : ICMFEncryptionProc {
        public byte[] Key(CMFHeader header, int length) {
            byte[] buffer = new byte[length];

            uint kidx = Keytable[header.DataCount & 511];
            const uint increment = 3;
            for (int i = 0; i != length; ++i) {
                buffer[i] = Keytable[kidx % 512];
                kidx += increment;
            }

            return buffer;
        }

        public byte[] IV(CMFHeader header, byte[] digest, int length) {
            byte[] buffer = new byte[length];

            uint kidx = Constrain(header.BuildVersion * length);
            uint increment = header.BuildVersion * (uint)header.DataCount % 7;
            for (int i = 0; i != length; ++i) {
                buffer[i] = Keytable[kidx % 512];
                kidx += increment;
                buffer[i] ^= digest[(kidx - 73) % SHA1_DIGESTSIZE];
            }

            return buffer;
        }

        private static readonly byte[] Keytable = {
            0x4D, 0x3C, 0x16, 0x0E, 0x58, 0x69, 0xA0, 0xAF, 0xD4, 0x13, 0x38, 0x3D, 0xCC, 0xAA, 0xD1, 0x94,
            0x6D, 0x7D, 0xF9, 0x3D, 0x61, 0x13, 0xB7, 0x6A, 0x05, 0x05, 0xFC, 0xFD, 0xED, 0x9E, 0xE3, 0x2A,
            0xEC, 0x3E, 0x54, 0x73, 0x22, 0xA8, 0x7C, 0xAC, 0x8B, 0xAB, 0x2E, 0x28, 0xAC, 0x26, 0x64, 0x24,
            0x0B, 0x70, 0x40, 0xEE, 0xC5, 0x4A, 0x11, 0x52, 0xD8, 0x48, 0xB5, 0xBC, 0x99, 0x4B, 0x68, 0x18,
            0xA6, 0x7D, 0x4B, 0xF3, 0x73, 0x7C, 0x51, 0x67, 0x2F, 0xE7, 0x91, 0xFD, 0x55, 0x95, 0x3E, 0xEF,
            0x8E, 0x25, 0x94, 0x6C, 0x31, 0xD7, 0x92, 0x26, 0x7A, 0xE8, 0xF1, 0x0D, 0x45, 0xCE, 0x41, 0xEA,
            0x65, 0x85, 0xC3, 0x56, 0x1A, 0x57, 0xFC, 0x2F, 0x1B, 0xF2, 0xEA, 0x93, 0x91, 0xCB, 0x8A, 0xB3,
            0x7D, 0xF1, 0x1A, 0x97, 0x5F, 0xC1, 0x5C, 0xD6, 0x81, 0xED, 0x02, 0x3D, 0x31, 0xD2, 0x64, 0x8E,
            0x9F, 0xE3, 0x9F, 0x9A, 0x34, 0xC5, 0x88, 0xF9, 0xDE, 0x50, 0x15, 0xBF, 0xC6, 0x28, 0x14, 0xEA,
            0x9E, 0xB3, 0xD6, 0xC1, 0x56, 0x5B, 0xFC, 0x1A, 0x6A, 0x4E, 0xDA, 0x9B, 0x90, 0x13, 0xC7, 0xC8,
            0x89, 0x53, 0xBF, 0x91, 0x8D, 0x15, 0x36, 0xE3, 0xB2, 0x5B, 0xEF, 0x35, 0x59, 0x46, 0x9D, 0x66,
            0x41, 0x5D, 0x32, 0x9D, 0x35, 0x7D, 0xD0, 0x78, 0x4F, 0x7D, 0x49, 0xBF, 0x02, 0x2E, 0xE8, 0xDB,
            0x63, 0xF8, 0xA5, 0x85, 0xB0, 0x16, 0x56, 0x85, 0x37, 0xBA, 0x64, 0xE7, 0xEC, 0x92, 0x07, 0x83,
            0x34, 0x3B, 0x84, 0x04, 0x30, 0x8C, 0x8B, 0x75, 0x02, 0x0C, 0x02, 0x40, 0x8E, 0x3B, 0xDC, 0x34,
            0x0F, 0x7B, 0x04, 0x42, 0x93, 0x99, 0xD1, 0xD4, 0xC1, 0x1F, 0x2D, 0x49, 0x4D, 0x32, 0xBD, 0x14,
            0x49, 0xFA, 0xF1, 0xDE, 0xF9, 0xD3, 0xAD, 0xAF, 0x0F, 0x22, 0x20, 0xC1, 0x30, 0x9B, 0xAE, 0x97,
            0x43, 0x1A, 0x32, 0x6F, 0x45, 0xFD, 0xA7, 0x3E, 0x9B, 0xA2, 0x90, 0x96, 0x2E, 0xDE, 0x98, 0xA7,
            0xB7, 0x53, 0x32, 0x2A, 0x07, 0x5A, 0x0B, 0x9C, 0x91, 0x1E, 0x9A, 0xCB, 0xC8, 0xDE, 0x78, 0xC8,
            0xBD, 0x1C, 0xCA, 0xEC, 0x49, 0x74, 0x45, 0x94, 0xD8, 0x9E, 0x24, 0x9F, 0xC3, 0x5C, 0x84, 0xB1,
            0x0C, 0x12, 0xC3, 0x4E, 0xBD, 0x72, 0xB1, 0x1C, 0x81, 0x26, 0x16, 0x3C, 0x1A, 0xDF, 0x04, 0x87,
            0x49, 0x1B, 0x3B, 0x74, 0x66, 0x29, 0x3C, 0x51, 0xD6, 0xF4, 0x97, 0x2B, 0x9C, 0x4A, 0xDF, 0x9C,
            0xAF, 0x4E, 0x60, 0x1A, 0x49, 0x8F, 0x37, 0x4B, 0x38, 0x04, 0x1D, 0x00, 0x6E, 0x96, 0x7F, 0xC9,
            0x77, 0xA5, 0xF8, 0x89, 0x67, 0x79, 0x91, 0x17, 0xC0, 0xC7, 0xEA, 0x89, 0x8E, 0x6B, 0xAA, 0x0A,
            0x8B, 0xEF, 0x01, 0x54, 0xA9, 0x0F, 0xC8, 0x05, 0xC3, 0x76, 0xF2, 0x8B, 0x78, 0xA3, 0x7D, 0xA4,
            0xAB, 0x64, 0x4B, 0xD1, 0x2D, 0x06, 0xC8, 0x54, 0x0B, 0xA9, 0x36, 0x3C, 0x02, 0xF1, 0xA0, 0xF5,
            0xDF, 0x54, 0xA9, 0x7E, 0x1A, 0xE8, 0x84, 0x76, 0x44, 0x46, 0x80, 0xD9, 0xFF, 0xC2, 0xA4, 0x5B,
            0x03, 0xF4, 0x0B, 0x80, 0x59, 0x3F, 0xF7, 0xDF, 0xAF, 0xC8, 0xE6, 0x32, 0x81, 0x83, 0x8D, 0xB1,
            0xE8, 0xBE, 0xAD, 0x03, 0x2E, 0x23, 0x57, 0xD9, 0x90, 0x0B, 0xDB, 0xF6, 0xB8, 0xDE, 0x97, 0xA3,
            0xF0, 0xC0, 0x85, 0x31, 0xCE, 0x63, 0xAB, 0x2E, 0xFE, 0xB3, 0x23, 0x55, 0xDB, 0x71, 0x12, 0x6A,
            0xD5, 0xF1, 0x2D, 0x0B, 0x3C, 0x03, 0xD6, 0x1D, 0x68, 0x60, 0x03, 0x9F, 0x73, 0xA2, 0x9B, 0x35,
            0x00, 0xD8, 0x6D, 0x30, 0xD6, 0x77, 0x4C, 0x5F, 0xA5, 0x39, 0xB9, 0x06, 0x42, 0xA2, 0xA4, 0x24,
            0xD9, 0xE2, 0xE9, 0x6E, 0x16, 0x0B, 0xF5, 0x25, 0xCB, 0xE3, 0x83, 0x96, 0xA1, 0x6E, 0xAC, 0x4D
        };
    }
}