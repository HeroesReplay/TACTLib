﻿using System;
using static TACTLib.Core.Product.Tank.CMFCryptHandler;
using static TACTLib.Core.Product.Tank.ContentManifestFile;

namespace TACTLib.Core.Product.Tank.CMF {
    [CMFMetadata(AutoDetectVersion = true, Product = TACTLib.Product.Overwatch)]
    public class ProCMF_41350 : ICMFEncryptionProc {
        public byte[] Key(CMFHeader header, int length) {
            byte[] buffer = new byte[length];

            uint kidx = Keytable[header.BuildVersion & 511];
            for (int i = 0; i != length; ++i) {
                buffer[i] = Keytable[kidx % 512];
                kidx += (uint)header.EntryCount;
            }

            return buffer;
        }

        public byte[] IV(CMFHeader header, byte[] digest, int length) {
            byte[] buffer = new byte[length];

            int keytableIndex = Math.Max((2 * digest[13] - length) % 512, 0);

            uint kidx = Keytable[keytableIndex];
            uint increment = (uint)header.EntryCount + digest[header.EntryCount % SHA1_DIGESTSIZE];
            for (int i = 0; i != length; ++i) {
                kidx += increment;
                buffer[i] = digest[kidx % SHA1_DIGESTSIZE];
            }

            return buffer;
        }

        private static readonly byte[] Keytable = {
            0x31, 0x3C, 0xFB, 0x62, 0xB2, 0x7A, 0xD7, 0xBE, 0x62, 0xAE, 0x39, 0x9D, 0x04, 0xCB, 0x82, 0x22,
            0xFF, 0x03, 0xA9, 0x5A, 0x07, 0xE3, 0x35, 0x56, 0x38, 0xD0, 0x65, 0xBE, 0xCF, 0x09, 0x20, 0x13,
            0x9A, 0x66, 0xC0, 0x3B, 0x19, 0x5F, 0x9F, 0x17, 0xE4, 0x18, 0xFF, 0x05, 0x37, 0x57, 0x41, 0x8A,
            0x3A, 0xCF, 0xDA, 0x43, 0x24, 0x84, 0x6C, 0xB1, 0x40, 0x09, 0x6F, 0xDD, 0x9E, 0x97, 0xAD, 0xD6,
            0xDF, 0xA5, 0x97, 0xA2, 0x8A, 0x86, 0x97, 0x1C, 0x31, 0xB6, 0xA9, 0xD3, 0x64, 0x55, 0x6B, 0x50,
            0xD5, 0xDC, 0xBC, 0x5F, 0x07, 0xAC, 0x04, 0xE9, 0x56, 0x8F, 0xDE, 0x13, 0x44, 0x29, 0xA2, 0xA3,
            0xC4, 0xFE, 0xA7, 0x3D, 0xEA, 0x56, 0x9A, 0xC8, 0x9C, 0xB2, 0x8C, 0x21, 0xCE, 0x87, 0x87, 0x5B,
            0xD9, 0x32, 0x0E, 0x21, 0x3B, 0x78, 0x41, 0xFC, 0xCE, 0xA8, 0xD4, 0xE1, 0x0D, 0x33, 0x2D, 0x9F,
            0xA0, 0xE7, 0x17, 0x90, 0x22, 0xF0, 0xE6, 0x3B, 0x0F, 0x76, 0x17, 0xA6, 0xFB, 0x9D, 0x12, 0x75,
            0xAB, 0xA5, 0x0A, 0x66, 0xA3, 0x08, 0xAB, 0x59, 0xEF, 0x79, 0xF9, 0x62, 0x18, 0x7E, 0x57, 0xC4,
            0xAF, 0x39, 0x69, 0x97, 0x19, 0x97, 0xAB, 0xD7, 0xFB, 0x90, 0xF1, 0xB6, 0xEA, 0x24, 0xDA, 0x7A,
            0xCB, 0x11, 0x1B, 0xAC, 0x60, 0xCD, 0x9F, 0x48, 0xC3, 0x82, 0x20, 0x43, 0x5F, 0x21, 0x25, 0x4C,
            0xD6, 0xB9, 0x86, 0x77, 0xF2, 0x70, 0x1F, 0xA7, 0xE1, 0xFD, 0xD9, 0x08, 0xDB, 0xC4, 0x28, 0x29,
            0xBE, 0x60, 0xC7, 0x8D, 0x6C, 0xB8, 0x8F, 0x70, 0x6A, 0xFD, 0x1D, 0xB0, 0xEC, 0x1D, 0x39, 0x29,
            0x6C, 0xDB, 0xCB, 0x5B, 0xAF, 0xAF, 0x26, 0x11, 0xE1, 0xEB, 0x02, 0x8D, 0xD3, 0x70, 0x43, 0xF8,
            0xAD, 0x70, 0x48, 0x9F, 0xD7, 0x85, 0x79, 0x92, 0x54, 0xAC, 0x5E, 0xB5, 0xCC, 0x55, 0xC0, 0x55,
            0xA8, 0xB4, 0x10, 0xDD, 0x81, 0x70, 0x0C, 0x33, 0xA0, 0x26, 0xF9, 0xAA, 0x41, 0x68, 0x74, 0x9B,
            0x24, 0x1B, 0x15, 0x0A, 0x4D, 0x0C, 0xE6, 0xC6, 0xE3, 0xF9, 0xE4, 0x6D, 0xD3, 0x0D, 0xBD, 0xAC,
            0x9E, 0xC6, 0x5B, 0x6E, 0xA3, 0x82, 0x1F, 0xB2, 0x9A, 0x35, 0x17, 0x8A, 0x5C, 0xAA, 0x90, 0xE4,
            0xCB, 0xC3, 0x4D, 0xA5, 0xAB, 0x3C, 0xD1, 0xBD, 0x22, 0x8F, 0x34, 0x1F, 0x50, 0x0C, 0x69, 0x4E,
            0xB8, 0x0C, 0x51, 0xE3, 0xF3, 0x9C, 0x92, 0xD1, 0xE7, 0xCC, 0x05, 0xE3, 0xF5, 0xDF, 0xF7, 0xEE,
            0x8B, 0x52, 0xD0, 0x1F, 0x71, 0x8B, 0x16, 0x17, 0x7C, 0x24, 0xB1, 0x02, 0xE0, 0x95, 0x84, 0x65,
            0xB0, 0x5F, 0xAB, 0x9A, 0x14, 0xD2, 0x29, 0x6C, 0xE3, 0xDA, 0xEE, 0x32, 0x8F, 0x48, 0x4C, 0x0D,
            0xC7, 0x8E, 0xFA, 0x7E, 0x7A, 0x7E, 0xA8, 0x4C, 0x75, 0x70, 0xB8, 0xF3, 0xC9, 0x52, 0xF3, 0x4E,
            0x81, 0x67, 0x5B, 0x07, 0xA3, 0xD7, 0xE9, 0xAD, 0x31, 0x7F, 0x9D, 0x8E, 0x6E, 0xEF, 0xF3, 0x57,
            0xAC, 0x75, 0x4F, 0xDF, 0xC8, 0xC6, 0x05, 0xFD, 0xE8, 0x5B, 0x8E, 0x93, 0xFD, 0xAD, 0x05, 0x7C,
            0xE8, 0x90, 0x1A, 0x5A, 0xC3, 0x79, 0x88, 0xD1, 0x9E, 0xB5, 0xE9, 0x08, 0xAE, 0xC0, 0xDD, 0xB0,
            0x7E, 0x93, 0x48, 0x7C, 0x4D, 0x7D, 0x54, 0x38, 0x25, 0x49, 0x33, 0x3A, 0x1E, 0x6A, 0x03, 0x43,
            0x18, 0x43, 0x38, 0x93, 0x84, 0xA7, 0x3A, 0xD5, 0xCD, 0x9A, 0xF2, 0xF3, 0xB6, 0x53, 0x67, 0x4E,
            0x55, 0x1F, 0xE3, 0x93, 0xB0, 0x48, 0x9A, 0x60, 0xC9, 0xA2, 0x0E, 0xA6, 0x2C, 0x72, 0x9A, 0x17,
            0xE0, 0xDB, 0x51, 0x00, 0x28, 0x19, 0xC6, 0xBC, 0xC9, 0xAD, 0x1D, 0x66, 0x57, 0xFA, 0xF6, 0xF8,
            0xF6, 0x70, 0xC3, 0x7C, 0x29, 0xA6, 0x50, 0x68, 0xC8, 0x13, 0x14, 0x2E, 0xAC, 0x13, 0x7A, 0x31
        };
    }
}