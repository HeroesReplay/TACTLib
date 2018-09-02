﻿using static TACTLib.Core.Product.Tank.CMFCryptHandler;
using static TACTLib.Core.Product.Tank.ContentManifestFile;

namespace TACTLib.Core.Product.Tank.CMF {
    [CMFMetadata(AutoDetectVersion = true, Product = TACTProduct.Overwatch)]
    public class ProCMF_39023 : ICMFEncryptionProc {
        public byte[] Key(CMFHeader header, int length) {
            byte[] buffer = new byte[length];

            uint kidx = Constrain(length * header.BuildVersion);
            for (int i = 0; i != length; ++i) {
                buffer[i] = Keytable[kidx % 512];
                switch (kidx % 3) {
                    case 0:
                        kidx += 103;
                        break;
                    case 1:
                        kidx = 4 * kidx % header.BuildVersion;
                        break;
                    case 2:
                        --kidx;
                        break;
                }
            }

            return buffer;
        }

        public byte[] IV(CMFHeader header, byte[] digest, int length) {
            byte[] buffer = new byte[length];

            uint kidx = 2u * digest[5];
            uint increment = (digest[6] & 1) > 0 ? 37 : kidx % 61;
            for (int i = 0; i != length; ++i) {
                buffer[i] = Keytable[kidx % 512];
                kidx += increment;
                buffer[i] ^= digest[(kidx - i) % SHA1_DIGESTSIZE];
            }

            return buffer;
        }

        private static readonly byte[] Keytable = {
            0xB9, 0x61, 0xD0, 0x26, 0xCA, 0xC9, 0xE1, 0xA9, 0x6E, 0xA5, 0xEA, 0x15, 0x81, 0x57, 0x88, 0x21,
            0x49, 0x26, 0xB7, 0xC1, 0x91, 0x7D, 0x9C, 0xEE, 0x3F, 0xBC, 0x4C, 0x60, 0xE3, 0x56, 0x6B, 0x36,
            0x4D, 0x23, 0x34, 0x68, 0xD9, 0x37, 0x6C, 0x1D, 0x6E, 0x80, 0xB8, 0x46, 0xE3, 0xA7, 0xC3, 0x08,
            0x40, 0x74, 0x86, 0x40, 0x55, 0x57, 0xF8, 0x37, 0x54, 0x5C, 0x20, 0xE0, 0x9A, 0xD9, 0x03, 0xBC,
            0x81, 0x6F, 0x9B, 0x18, 0xD5, 0xF3, 0x01, 0x16, 0xCA, 0x37, 0xAB, 0x80, 0xA4, 0x48, 0x34, 0x44,
            0x77, 0xC0, 0xA5, 0x13, 0xC5, 0x48, 0xD5, 0xB8, 0x29, 0x84, 0x49, 0x32, 0xF2, 0x50, 0x12, 0xEB,
            0xB3, 0xFC, 0x08, 0xD4, 0xB7, 0x9C, 0x8A, 0xA5, 0x91, 0xC1, 0x2B, 0x50, 0x22, 0x42, 0xC4, 0xDF,
            0x43, 0xFB, 0x64, 0x66, 0x04, 0xE2, 0x91, 0x33, 0x5A, 0x76, 0x61, 0x1E, 0x7F, 0xB4, 0xBB, 0xFF,
            0x49, 0x6A, 0xE2, 0x7E, 0xB7, 0x49, 0xBD, 0x2F, 0x64, 0x1D, 0x19, 0x09, 0x74, 0x32, 0xF5, 0xD6,
            0x9F, 0xB9, 0x27, 0x9C, 0x96, 0x8C, 0xE7, 0x6D, 0x6E, 0x11, 0x97, 0x84, 0x97, 0x78, 0x6D, 0x94,
            0x4D, 0xBE, 0x49, 0xF7, 0x4A, 0x49, 0xA9, 0xAE, 0xD0, 0x9F, 0x82, 0xBC, 0xA7, 0xF7, 0x8C, 0x03,
            0xBD, 0x47, 0x7F, 0x0A, 0x53, 0x61, 0xE1, 0x53, 0x1F, 0xF7, 0xD4, 0x11, 0x7B, 0x69, 0xB4, 0x64,
            0x0E, 0xF7, 0x90, 0x79, 0x9C, 0x52, 0x91, 0xBB, 0x2B, 0xB2, 0xE3, 0xC2, 0x01, 0x5F, 0x32, 0x32,
            0x5D, 0xA8, 0xAA, 0xB1, 0x63, 0xCC, 0x48, 0xAE, 0xD1, 0x5A, 0xDD, 0x72, 0x36, 0x39, 0x23, 0x90,
            0x10, 0xAD, 0x8F, 0xF3, 0xE7, 0xCB, 0x90, 0x11, 0xBF, 0x12, 0x3D, 0x52, 0x1B, 0x7E, 0xE1, 0x18,
            0x2D, 0x82, 0xA2, 0x71, 0x94, 0x7D, 0xAA, 0x29, 0x94, 0xDE, 0xBF, 0xC6, 0x10, 0xBF, 0x98, 0x5A,
            0x6F, 0x27, 0xBD, 0xFA, 0xC7, 0xEE, 0xB6, 0x17, 0x66, 0x97, 0x04, 0xBE, 0xFD, 0x9A, 0x6C, 0x6C,
            0xE3, 0x57, 0xB8, 0x5E, 0x23, 0x91, 0x65, 0xFB, 0x44, 0x37, 0x68, 0xEC, 0xE9, 0x59, 0x76, 0x0D,
            0x49, 0x3C, 0x1D, 0x22, 0xEB, 0x72, 0x93, 0xB0, 0xAE, 0x82, 0x1D, 0xC5, 0x80, 0x21, 0x54, 0x87,
            0x7F, 0xE7, 0x9A, 0x35, 0x43, 0xE3, 0x01, 0x7D, 0xF0, 0x2E, 0xAD, 0xE5, 0x7C, 0xBF, 0x8C, 0x41,
            0x0C, 0xEC, 0x76, 0x72, 0xF5, 0x8E, 0x39, 0xA9, 0x23, 0x66, 0x14, 0x16, 0x69, 0x7D, 0x56, 0x66,
            0xB4, 0x39, 0x9A, 0xE4, 0x3D, 0x3D, 0x70, 0x88, 0xC4, 0xCA, 0xEA, 0xFF, 0x09, 0x67, 0xED, 0xF0,
            0x38, 0x9F, 0x9E, 0x50, 0xA0, 0x2A, 0x85, 0x0D, 0x71, 0x86, 0x53, 0xAF, 0xD2, 0xE5, 0xE0, 0x59,
            0x4F, 0xDC, 0x22, 0x46, 0xB3, 0x9D, 0xF7, 0x65, 0x59, 0x22, 0xB9, 0x44, 0x58, 0x91, 0x3E, 0xCD,
            0x3F, 0xA1, 0xD6, 0x50, 0x27, 0xD6, 0x1D, 0xE2, 0x09, 0xE6, 0xFA, 0x37, 0x40, 0x39, 0xA4, 0xAA,
            0xC9, 0x71, 0x9D, 0x21, 0x50, 0xF7, 0xA0, 0x06, 0xE3, 0x3C, 0x24, 0xAE, 0x44, 0x0A, 0x49, 0x9D,
            0xD3, 0xCD, 0x53, 0xFC, 0x06, 0x6D, 0x90, 0x76, 0x0B, 0xAE, 0xE1, 0x56, 0x42, 0xD3, 0x87, 0xEC,
            0x9C, 0x6D, 0xCC, 0xA6, 0xB9, 0x7C, 0xE0, 0xC7, 0x2B, 0x94, 0x47, 0xB1, 0x54, 0xB8, 0x4C, 0x6D,
            0x46, 0x53, 0xAE, 0x0F, 0x12, 0x55, 0x35, 0xDE, 0x6E, 0xDE, 0xAB, 0xD2, 0x79, 0xCA, 0x15, 0x05,
            0x04, 0x52, 0xDD, 0x12, 0x48, 0x34, 0x9E, 0x88, 0x20, 0x63, 0x2C, 0xC8, 0x9C, 0xD1, 0x12, 0xD1,
            0x84, 0x30, 0x04, 0x24, 0x25, 0x2F, 0x32, 0xC2, 0xA0, 0xFB, 0x20, 0xD4, 0x80, 0xCD, 0x9F, 0xBF,
            0x78, 0x3B, 0xFE, 0x1D, 0xEC, 0x49, 0x3D, 0x79, 0x6F, 0xF3, 0xBB, 0x95, 0xF5, 0x45, 0xDC, 0xF4
        };
    }
}