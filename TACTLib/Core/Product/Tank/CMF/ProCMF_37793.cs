﻿using static TACTLib.Core.Product.Tank.CMFCryptHandler;
using static TACTLib.Core.Product.Tank.ContentManifestFile;

namespace TACTLib.Core.Product.Tank.CMF {
    [CMFMetadata(AutoDetectVersion = true, Product = TACTProduct.Overwatch)]
    public class ProCMF_37793 : ICMFEncryptionProc {
        public byte[] Key(CMFHeader header, int length) {
            byte[] buffer = new byte[length];

            uint kidx = Constrain(length * header.BuildVersion);
            for (int i = 0; i != length; ++i) {
                buffer[i] = Keytable[kidx % 512];
                kidx += 3;
            }

            return buffer;
        }

        public byte[] IV(CMFHeader header, byte[] digest, int length) {
            byte[] buffer = new byte[length];

            uint kidx = Constrain(length * header.BuildVersion);
            uint increment = kidx % 29;
            for (int i = 0; i != length; ++i) {
                buffer[i] = Keytable[kidx % 512];
                kidx += increment;
                buffer[i] ^= (byte) ((digest[(kidx + header.EntryCount) % SHA1_DIGESTSIZE] + 1) % 0xFF);
            }

            return buffer;
        }

        private static readonly byte[] Keytable = {
            0x09, 0x51, 0x37, 0x0C, 0xA1, 0xCB, 0x53, 0x3C, 0x24, 0xA6, 0x2B, 0x8C, 0xD6, 0x24, 0xAC, 0x2C,
            0x44, 0xAD, 0xD3, 0x08, 0xC7, 0x02, 0x79, 0x90, 0x9C, 0xAF, 0x66, 0xC9, 0xFE, 0xD2, 0xDF, 0xFB,
            0x40, 0x45, 0x4B, 0x6A, 0x18, 0xDF, 0x82, 0xC2, 0x20, 0x9A, 0x77, 0x2B, 0x48, 0x94, 0xAB, 0xC8,
            0x83, 0x94, 0xA1, 0xC4, 0x26, 0x9E, 0xBF, 0x2F, 0x36, 0x2E, 0xDD, 0x37, 0xEB, 0x87, 0xF0, 0x84,
            0xFB, 0xE8, 0xD5, 0x48, 0xC1, 0xE9, 0x2F, 0x83, 0x01, 0x36, 0x86, 0x33, 0xB9, 0x3A, 0xDB, 0x34,
            0x91, 0xA0, 0xCA, 0x64, 0xA4, 0x69, 0x6C, 0xC7, 0xD7, 0x3C, 0x6F, 0xA7, 0x52, 0xB9, 0x0D, 0xDC,
            0xC3, 0x24, 0x65, 0xF9, 0x57, 0x3B, 0x68, 0xE2, 0x27, 0x3C, 0x43, 0x0E, 0xFC, 0x33, 0x18, 0xCB,
            0x32, 0x60, 0xE7, 0xCA, 0xD8, 0x27, 0x12, 0xD0, 0x97, 0xF0, 0x7C, 0x23, 0x92, 0x4D, 0x52, 0x89,
            0x26, 0x68, 0x7D, 0x65, 0x75, 0x35, 0x3B, 0x68, 0x15, 0x96, 0x5C, 0xA8, 0xA5, 0x8E, 0x26, 0x10,
            0x77, 0x9B, 0x3E, 0xC7, 0xE7, 0x1C, 0x46, 0xEF, 0x44, 0xDE, 0x65, 0x17, 0x29, 0x39, 0x0B, 0x1D,
            0x0C, 0xE7, 0x09, 0x89, 0x4D, 0x0E, 0x65, 0x78, 0x15, 0xAE, 0xAB, 0x8C, 0x5E, 0xA4, 0xF8, 0xDE,
            0x36, 0xE9, 0x2A, 0x32, 0x91, 0xED, 0xBD, 0x15, 0x17, 0xD7, 0x3D, 0x65, 0x3E, 0xB3, 0x32, 0xCF,
            0xED, 0xAD, 0xE5, 0x92, 0xE7, 0xE4, 0x85, 0xA5, 0xE6, 0x0C, 0x78, 0x51, 0xED, 0x47, 0xED, 0xA2,
            0x81, 0x83, 0xB0, 0x5E, 0x0B, 0x71, 0x9E, 0xF9, 0x65, 0x5F, 0x0B, 0xA2, 0x42, 0xCF, 0x8E, 0x17,
            0x5D, 0xA1, 0x8F, 0x54, 0xCA, 0x3D, 0xB6, 0x36, 0x3E, 0x59, 0x4F, 0x80, 0x41, 0xCD, 0x67, 0x6E,
            0xFC, 0xB1, 0x2E, 0xC5, 0x22, 0x82, 0x20, 0xA9, 0x2F, 0x0A, 0x4D, 0xD5, 0x31, 0x39, 0x28, 0xEF,
            0x65, 0xF7, 0xC3, 0x50, 0x1D, 0x4E, 0xAF, 0xED, 0xCB, 0x78, 0x9C, 0xDF, 0x45, 0xE5, 0x26, 0xE9,
            0x4C, 0x80, 0x6E, 0x26, 0x84, 0x8E, 0xE9, 0x85, 0x35, 0x6E, 0x78, 0x96, 0x9A, 0x6B, 0x35, 0xEB,
            0x84, 0x5D, 0x1B, 0xCF, 0x68, 0x7F, 0xEC, 0x35, 0x61, 0xFA, 0xE2, 0x92, 0x8A, 0x59, 0xA9, 0x6D,
            0xEA, 0x42, 0xD0, 0xA3, 0x7B, 0x60, 0x70, 0x19, 0x71, 0x59, 0xDD, 0x95, 0x63, 0xDB, 0x8F, 0xB8,
            0x0C, 0x7C, 0x4C, 0x31, 0xC3, 0x46, 0xD0, 0xA3, 0x66, 0x28, 0xD1, 0xAB, 0xD9, 0x87, 0xE4, 0x34,
            0x8D, 0xDF, 0x8C, 0x9F, 0x29, 0x20, 0x5A, 0x36, 0x5B, 0x47, 0x43, 0xBD, 0x37, 0x15, 0xC6, 0x77,
            0xFB, 0xCA, 0x7A, 0xF8, 0x3F, 0xE9, 0x8D, 0x0E, 0xD3, 0x76, 0xAA, 0x1F, 0xCB, 0x41, 0xC9, 0x41,
            0xC6, 0x9E, 0x63, 0x58, 0xE7, 0x08, 0x84, 0x5A, 0x38, 0xCA, 0xAE, 0xE4, 0x0F, 0xDD, 0x63, 0x21,
            0xFD, 0x14, 0xBE, 0x4F, 0x4B, 0x27, 0x54, 0x0B, 0x36, 0x4D, 0xA5, 0xA4, 0x69, 0xFA, 0x10, 0xC4,
            0xFD, 0x88, 0xBC, 0x65, 0xD6, 0x23, 0xDE, 0x81, 0xC5, 0x29, 0xE3, 0x2A, 0x4D, 0xBC, 0x5B, 0x9C,
            0x59, 0x7D, 0xE6, 0x77, 0x5E, 0x14, 0x05, 0x87, 0x71, 0x1F, 0xBD, 0xA3, 0x48, 0xBC, 0x6D, 0x97,
            0xB2, 0x1E, 0xF5, 0xE6, 0x3B, 0xDF, 0x60, 0xA1, 0xAA, 0x8C, 0xF2, 0x7B, 0x40, 0xBF, 0x4B, 0x31,
            0xC1, 0xCF, 0x75, 0x1F, 0xC9, 0x4A, 0x29, 0x2A, 0x95, 0x4C, 0xE6, 0x1C, 0x51, 0x45, 0xF2, 0xCB,
            0x09, 0x70, 0xED, 0xF0, 0xC1, 0xBD, 0x87, 0x17, 0x7D, 0xB4, 0x41, 0x79, 0xAD, 0x0D, 0x55, 0xDF,
            0xCD, 0x5B, 0xE5, 0x52, 0x8D, 0xFE, 0xD2, 0x50, 0x17, 0x63, 0xA8, 0xD2, 0x6C, 0xE8, 0xC2, 0xF8,
            0x36, 0x43, 0x43, 0xE1, 0x05, 0x6B, 0xEA, 0x36, 0x33, 0xCA, 0xC7, 0x6C, 0x02, 0x54, 0x08, 0x33
        };
    }
}