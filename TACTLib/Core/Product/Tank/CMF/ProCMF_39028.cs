﻿using static TACTLib.Core.Product.Tank.CMFCryptHandler;
using static TACTLib.Core.Product.Tank.ContentManifestFile;

namespace TACTLib.Core.Product.Tank.CMF {
    [CMFMetadata(AutoDetectVersion = true, Product = TACTProduct.Overwatch)]
    public class ProCMF_39028 : ICMFEncryptionProc {
        public byte[] Key(CMFHeader header, int length) {
            byte[] buffer = new byte[length];

            uint kidx = Constrain(length * header.BuildVersion);
            for (int i = 0; i != length; ++i) {
                buffer[i] = Keytable[kidx % 512];
                kidx = Constrain(header.BuildVersion - kidx);
            }

            return buffer;
        }

        public byte[] IV(CMFHeader header, byte[] digest, int length) {
            byte[] buffer = new byte[length];

            uint kidx = Keytable[header.BuildVersion & 511];
            for (int i = 0; i != length; ++i) {
                buffer[i] = Keytable[kidx % 512];
                kidx += 3;
                buffer[i] ^= digest[(kidx - i) % SHA1_DIGESTSIZE];
            }

            return buffer;
        }

        private static readonly byte[] Keytable = {
            0x21, 0xC3, 0x55, 0x59, 0x94, 0x08, 0xC7, 0x70, 0xFB, 0x09, 0x4D, 0xD7, 0xB0, 0x71, 0x1D, 0x48,
            0xB5, 0xC8, 0x1A, 0xB5, 0xE2, 0xC7, 0x2F, 0xA4, 0x0E, 0x59, 0x97, 0x9F, 0x94, 0xA6, 0x43, 0x26,
            0x6A, 0xB9, 0xA9, 0x18, 0xE4, 0xC8, 0x64, 0x29, 0x93, 0x8A, 0x4C, 0x8B, 0x4F, 0xA3, 0x50, 0xC7,
            0x36, 0x26, 0x34, 0xA8, 0xC4, 0x88, 0x16, 0xF2, 0x42, 0x43, 0x6D, 0x74, 0x05, 0x59, 0xC1, 0xF1,
            0x96, 0x22, 0xBA, 0xC0, 0x05, 0x07, 0x06, 0x22, 0x0F, 0xBB, 0xD3, 0xC9, 0xC0, 0xDE, 0xE7, 0x99,
            0xF7, 0xBE, 0x82, 0x32, 0x09, 0x5D, 0xEC, 0xD2, 0x7D, 0xC2, 0x73, 0x9C, 0x87, 0x1F, 0x7D, 0x75,
            0xFD, 0x4C, 0x79, 0x35, 0x98, 0xA6, 0x20, 0x37, 0x68, 0x27, 0xE1, 0x7B, 0x44, 0xBF, 0x50, 0x98,
            0xED, 0x1B, 0x15, 0x6C, 0xAD, 0x31, 0x14, 0x0E, 0x4B, 0x81, 0xF2, 0xEE, 0x8E, 0xF3, 0x3A, 0x07,
            0xF3, 0x03, 0x66, 0xC7, 0x0A, 0xCD, 0xCC, 0x7B, 0xD9, 0xB8, 0xC1, 0x4B, 0x17, 0x68, 0xD0, 0x8D,
            0xC7, 0x5F, 0x28, 0xCA, 0xC3, 0xC4, 0xAC, 0xA2, 0xFF, 0x75, 0xE8, 0xF5, 0x0F, 0xD8, 0xE6, 0x3A,
            0x40, 0xF3, 0x8C, 0x0A, 0xED, 0xBC, 0x97, 0xDE, 0x13, 0x6D, 0x04, 0x75, 0x3F, 0xFE, 0x71, 0x08,
            0xC0, 0x28, 0x87, 0x11, 0x9C, 0xC1, 0x75, 0x43, 0x8E, 0x24, 0xCB, 0xEE, 0x79, 0x45, 0x85, 0xD5,
            0x34, 0xF2, 0x6B, 0x75, 0xE7, 0x3D, 0xD8, 0x20, 0x6F, 0x86, 0x13, 0xF9, 0x4E, 0x6D, 0x66, 0xDA,
            0x40, 0x57, 0xCF, 0xA3, 0xA4, 0x4D, 0xAC, 0x9C, 0xA0, 0xA7, 0x25, 0xD6, 0xC7, 0x4F, 0xC7, 0xD6,
            0x7D, 0x23, 0x21, 0xA5, 0xA3, 0x34, 0x62, 0xD0, 0x63, 0x0B, 0xCF, 0x1A, 0xE3, 0x95, 0xED, 0x0C,
            0x12, 0xD0, 0xD4, 0x1A, 0x14, 0x8F, 0x15, 0x44, 0x49, 0xE1, 0x69, 0xF9, 0x2D, 0x0C, 0x47, 0x0E,
            0x83, 0x63, 0xA5, 0x95, 0x19, 0x07, 0x77, 0x10, 0x6A, 0x5A, 0x8C, 0x35, 0x34, 0xBC, 0x35, 0x76,
            0x0C, 0x2C, 0x0E, 0xFB, 0x42, 0x20, 0x8C, 0x93, 0xE8, 0x02, 0x25, 0x9A, 0x66, 0x90, 0x0A, 0x2F,
            0x9D, 0xB7, 0x5C, 0x40, 0x98, 0x82, 0x86, 0xF9, 0xEA, 0x45, 0xF9, 0xC1, 0x31, 0xC2, 0x39, 0x16,
            0xFD, 0xE7, 0x61, 0xE9, 0xB9, 0x88, 0xD3, 0x4D, 0x48, 0x5D, 0x33, 0x12, 0x83, 0x6C, 0xDF, 0x92,
            0x26, 0xE5, 0x17, 0x42, 0xD2, 0x4C, 0xC7, 0x83, 0xC9, 0x0E, 0x7D, 0x60, 0x9A, 0xCF, 0x77, 0x5A,
            0x12, 0xB5, 0x86, 0xB4, 0xCE, 0xA2, 0xC9, 0xBD, 0xED, 0x94, 0xA4, 0xA5, 0x84, 0x80, 0xBC, 0xCB,
            0x54, 0x9F, 0x55, 0xCA, 0xA0, 0x04, 0x18, 0x63, 0x56, 0x12, 0xC1, 0x9C, 0xF7, 0xF7, 0x57, 0x65,
            0xF9, 0x7A, 0xDE, 0x7E, 0xC9, 0xE5, 0xFD, 0x65, 0x1E, 0x03, 0x0F, 0x55, 0x8D, 0xBA, 0x16, 0xBE,
            0xF4, 0x25, 0x9F, 0xA0, 0x16, 0x10, 0xAB, 0x91, 0xFA, 0xFD, 0xB3, 0xC0, 0x70, 0x7A, 0x89, 0xD0,
            0x65, 0x52, 0x63, 0xD2, 0x33, 0xA1, 0xD2, 0xD8, 0xFD, 0x2F, 0x4E, 0xC9, 0x19, 0xDD, 0x86, 0xE9,
            0x7B, 0x9A, 0xCD, 0x82, 0x8B, 0x00, 0x33, 0xA5, 0x3F, 0x0D, 0x1C, 0xB7, 0x71, 0xAA, 0xFE, 0xF6,
            0x09, 0xE3, 0x1B, 0x71, 0x6B, 0x95, 0x35, 0xFB, 0x4D, 0x3F, 0x04, 0x00, 0x09, 0x17, 0x36, 0xD9,
            0x80, 0x46, 0x39, 0x4B, 0x32, 0x51, 0x0E, 0x52, 0xD7, 0x92, 0x46, 0x20, 0x23, 0x8A, 0x2E, 0xAC,
            0xF0, 0xED, 0xBB, 0xFE, 0x4E, 0x58, 0xD7, 0xE9, 0x50, 0xA3, 0x4C, 0x6D, 0xB4, 0xE5, 0x41, 0x58,
            0x33, 0xDD, 0xDF, 0xEE, 0xAE, 0x96, 0xFA, 0xCE, 0x7A, 0x23, 0xC3, 0x3F, 0x22, 0x85, 0x68, 0xBC,
            0x36, 0x0B, 0x9D, 0x4E, 0x04, 0x62, 0x9D, 0xCD, 0xC3, 0x5E, 0x9F, 0xDE, 0xB2, 0xDD, 0xCA, 0xF7
        };
    }
}