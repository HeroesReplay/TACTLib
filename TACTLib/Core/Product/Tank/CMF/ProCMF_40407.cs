﻿using static TACTLib.Core.Product.Tank.CMFCryptHandler;
using static TACTLib.Core.Product.Tank.ContentManifestFile;

namespace TACTLib.Core.Product.Tank.CMF {
    [CMFMetadata(AutoDetectVersion = true, Product = TACTProduct.Overwatch)]
    public class ProCMF_40407 : ICMFEncryptionProc {
        public byte[] Key(CMFHeader header, int length) {
            byte[] buffer = new byte[length];

            uint kidx = Keytable[Keytable[0] * length % 512];
            for (int i = 0; i != length; ++i) {
                buffer[i] = Keytable[kidx % 512];
                kidx = header.BuildVersion - kidx;
            }

            return buffer;
        }

        public byte[] IV(CMFHeader header, byte[] digest, int length) {
            byte[] buffer = new byte[length];

            uint kidx = Keytable[header.BuildVersion & 511];
            uint increment = header.BuildVersion * (uint)header.DataCount % 7;
            for (int i = 0; i != length; ++i) {
                buffer[i] = Keytable[kidx % 512];
                kidx += increment;
                buffer[i] ^= digest[(kidx - 73) % SHA1_DIGESTSIZE];
            }

            return buffer;
        }

        private static readonly byte[] Keytable = {
            0x71, 0xF8, 0x8E, 0xCB, 0x4F, 0x13, 0xAC, 0x85, 0x24, 0xA5, 0x5C, 0x24, 0xCA, 0x32, 0x1C, 0xC9,
            0x6F, 0x28, 0x47, 0x3B, 0x31, 0xA3, 0x95, 0xC8, 0x91, 0x05, 0x22, 0x8C, 0xBB, 0xF5, 0x08, 0xBD,
            0xDE, 0x1D, 0xA6, 0xF4, 0x56, 0x0E, 0xBC, 0x47, 0x81, 0x0D, 0xD8, 0xAB, 0xB9, 0x15, 0xF3, 0x40,
            0x86, 0xED, 0x11, 0xC7, 0x2D, 0xA1, 0xF4, 0x2F, 0x4C, 0x5A, 0x7A, 0xED, 0xC4, 0xEC, 0x5F, 0x76,
            0x3F, 0xCA, 0xB9, 0x1E, 0xFA, 0x04, 0x95, 0x00, 0xBE, 0x64, 0x46, 0x89, 0xAB, 0xBD, 0x0F, 0xC3,
            0xCC, 0x88, 0x78, 0x80, 0x2A, 0x2E, 0x1A, 0x8F, 0x2F, 0xEB, 0xE0, 0x42, 0x3E, 0x04, 0x24, 0x69,
            0xC4, 0x1C, 0x59, 0x94, 0xD0, 0x53, 0x25, 0x0F, 0xDB, 0x3B, 0xEF, 0x9D, 0x2D, 0xB0, 0xD2, 0xDC,
            0xB7, 0x86, 0x72, 0x1F, 0x22, 0x97, 0xF1, 0x26, 0x0C, 0xE3, 0x4B, 0xE0, 0x02, 0x86, 0xCE, 0x9E,
            0xF8, 0x7F, 0xDA, 0x47, 0xE7, 0x89, 0x45, 0x08, 0x6F, 0xA3, 0xB4, 0x0A, 0xA3, 0x0A, 0x89, 0xB6,
            0x9A, 0x1C, 0xDE, 0x17, 0xA6, 0xC3, 0xE4, 0x81, 0x7D, 0x4B, 0x65, 0xB3, 0x51, 0xEA, 0x9D, 0xC9,
            0x92, 0x24, 0xEF, 0x2D, 0x80, 0xEA, 0x83, 0xBB, 0x41, 0x41, 0x52, 0x02, 0xF7, 0x0B, 0x87, 0x52,
            0x80, 0x53, 0x6F, 0xB4, 0x35, 0xEE, 0xF8, 0xAF, 0xE2, 0xEA, 0xF6, 0x3A, 0x40, 0xB6, 0x8E, 0xDC,
            0xDA, 0x99, 0x22, 0x9F, 0x40, 0x9F, 0x17, 0x85, 0xD6, 0xB1, 0x32, 0xC2, 0xB0, 0x7A, 0x5D, 0xEC,
            0xB3, 0x05, 0xCE, 0x57, 0x96, 0x5E, 0x6A, 0x58, 0x32, 0x62, 0xC3, 0x12, 0x71, 0xFB, 0xD1, 0x5E,
            0xEF, 0x65, 0x19, 0x3F, 0x3F, 0x5C, 0xE8, 0x93, 0xAC, 0x08, 0xA1, 0xA2, 0x8E, 0x42, 0x06, 0x9F,
            0x46, 0x22, 0xBD, 0x5D, 0xC4, 0x0F, 0x28, 0x49, 0x33, 0x00, 0xBB, 0x8D, 0x20, 0x64, 0x0E, 0xD4,
            0x52, 0xBA, 0x63, 0xE5, 0x31, 0xBF, 0x71, 0x4B, 0x72, 0xEC, 0x63, 0x5E, 0xD1, 0x4F, 0x14, 0x2D,
            0x53, 0xC0, 0x77, 0x4B, 0x73, 0x9E, 0xB6, 0x13, 0x19, 0x97, 0xFD, 0x28, 0xF9, 0xFD, 0x3C, 0xCD,
            0xC1, 0x8A, 0x91, 0x87, 0x14, 0xAA, 0x26, 0x2D, 0x82, 0x6A, 0xB4, 0x0C, 0x6F, 0x40, 0x01, 0x54,
            0xC5, 0x2D, 0x15, 0xBA, 0xFC, 0x13, 0x60, 0x25, 0x31, 0x5B, 0x92, 0x17, 0xD7, 0xDD, 0x44, 0x4F,
            0xF4, 0xF8, 0xE4, 0x62, 0xA2, 0xFF, 0x41, 0x07, 0x7A, 0xE6, 0xB3, 0xC7, 0xB4, 0x8C, 0xF4, 0xB1,
            0xE8, 0xA5, 0x8B, 0x51, 0x98, 0xE7, 0x8D, 0x7F, 0x99, 0x83, 0x87, 0xA0, 0x71, 0x5E, 0xB5, 0xDD,
            0x43, 0x8B, 0xAD, 0x4A, 0xBC, 0x90, 0x9A, 0x1A, 0xA0, 0x30, 0x02, 0x0E, 0x1C, 0x00, 0x11, 0x10,
            0x3A, 0x79, 0x77, 0x8B, 0x4B, 0x88, 0x5F, 0xEC, 0xB4, 0x66, 0x34, 0x63, 0xA6, 0x67, 0xD9, 0x8F,
            0x85, 0x7F, 0xF3, 0x5B, 0x35, 0xED, 0x8C, 0x86, 0x30, 0x63, 0xB1, 0x18, 0xF9, 0xBD, 0x11, 0x69,
            0xB9, 0x05, 0x44, 0xC4, 0xDC, 0x00, 0xD4, 0x28, 0xDF, 0xD0, 0x5A, 0x35, 0x1C, 0xC5, 0x9D, 0x4C,
            0xCA, 0x98, 0x18, 0xAA, 0x8B, 0x3D, 0x2B, 0xE4, 0xB3, 0xBE, 0x2C, 0xFE, 0xF6, 0xBF, 0x8C, 0x18,
            0x58, 0xF6, 0xA8, 0x9A, 0x2E, 0xDD, 0xE5, 0xC2, 0x29, 0xFE, 0x91, 0x54, 0xB3, 0xD8, 0x2E, 0xEE,
            0x08, 0xA6, 0x5E, 0xA9, 0x27, 0xF5, 0xCA, 0x00, 0x65, 0x2C, 0x01, 0xB6, 0x4E, 0x37, 0x6B, 0x88,
            0xB0, 0x6A, 0x55, 0x6A, 0x93, 0xB9, 0x26, 0x7D, 0x9C, 0xB2, 0x06, 0x82, 0xD7, 0x71, 0x30, 0x8D,
            0x78, 0x7D, 0x6D, 0xF8, 0xF0, 0xE5, 0x63, 0x57, 0x2C, 0xB6, 0x84, 0xD4, 0xC3, 0xBC, 0xFA, 0xBA,
            0xB2, 0x00, 0x1E, 0x91, 0x84, 0xCF, 0xC6, 0x6E, 0x9D, 0x2C, 0x95, 0x51, 0x78, 0x80, 0xE4, 0x54
        };
    }
}