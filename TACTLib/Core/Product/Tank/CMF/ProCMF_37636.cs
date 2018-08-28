﻿using static TACTLib.Core.Product.Tank.CMFCryptHandler;
using static TACTLib.Core.Product.Tank.ContentManifestFile;

namespace TACTLib.Core.Product.Tank.CMF {
    [CMFMetadata(AutoDetectVersion = true, Product = TACTLib.Product.Overwatch)]
    public class ProCMF_37636 : ICMFEncryptionProc {
        public byte[] Key(CMFHeader header, int length) {
            byte[] buffer = new byte[length];

            uint kidx = Keytable[header.DataCount & 511];
            uint increment = header.BuildVersion * (uint)header.DataCount % 7;
            for (int i = 0; i != length; ++i) {
                buffer[i] = Keytable[kidx % 512];
                kidx += increment;
            }

            return buffer;
        }

        public byte[] IV(CMFHeader header, byte[] digest, int length) {
            byte[] buffer = new byte[length];

            uint kidx = Keytable[(Keytable[0] * digest[7]) & 511];
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

                buffer[i] ^= digest[(kidx + header.BuildVersion) % SHA1_DIGESTSIZE];
            }

            return buffer;
        }

        private static readonly byte[] Keytable = {
            0x83, 0x91, 0xE7, 0xEC, 0xF8, 0x5B, 0x2E, 0x62, 0x75, 0xB4, 0x0F, 0x31, 0x36, 0x92, 0x86, 0xD9,
            0x1B, 0x3D, 0x54, 0x4A, 0xAA, 0x58, 0xF0, 0xDC, 0xCE, 0x5A, 0x48, 0x97, 0x2D, 0xDF, 0xB9, 0xD7,
            0xAA, 0xBE, 0x5E, 0xC6, 0x98, 0x58, 0xBA, 0x75, 0x1B, 0xFF, 0x9D, 0xED, 0x5E, 0x00, 0xBA, 0xFA,
            0x89, 0x7C, 0x9D, 0xCA, 0xE8, 0xC1, 0x76, 0x29, 0x0D, 0xC0, 0x22, 0xBA, 0x97, 0x28, 0x13, 0x83,
            0x31, 0xC4, 0x6F, 0xF5, 0xE8, 0x7A, 0xCC, 0xB1, 0x87, 0xAB, 0xB8, 0xC6, 0x74, 0x6A, 0x4E, 0xC6,
            0x95, 0xF5, 0x49, 0x7F, 0x1F, 0xA4, 0x98, 0x93, 0x15, 0x5D, 0x65, 0xAE, 0xCC, 0x19, 0xCE, 0xF3,
            0x08, 0xFE, 0x4B, 0x05, 0xCE, 0xEF, 0xF4, 0x5B, 0x88, 0x65, 0x5F, 0x28, 0xC5, 0x5A, 0xA2, 0x94,
            0xA2, 0x15, 0x80, 0xE5, 0x92, 0xBA, 0xE9, 0x12, 0x43, 0x06, 0xB5, 0x35, 0x08, 0x0E, 0x9C, 0x43,
            0x60, 0x36, 0x3D, 0xA0, 0xD1, 0x33, 0xA2, 0xCC, 0xB9, 0x7B, 0x98, 0xB2, 0x0A, 0xDE, 0xA8, 0xF3,
            0x12, 0xCB, 0x45, 0xEA, 0xAB, 0xE3, 0xB6, 0x62, 0x35, 0xEA, 0x5D, 0x7F, 0x1A, 0xFE, 0xCC, 0x0E,
            0x18, 0x91, 0x98, 0x67, 0xDB, 0x20, 0x45, 0xFD, 0x24, 0x10, 0xB8, 0x41, 0x60, 0x57, 0xA9, 0xF0,
            0x6E, 0x12, 0xE3, 0x95, 0x8C, 0xA7, 0xC5, 0xD7, 0x43, 0x23, 0x4A, 0x8C, 0x9D, 0xB6, 0x4C, 0x91,
            0xDE, 0xE1, 0x50, 0x58, 0x47, 0x29, 0x27, 0xAD, 0x05, 0x98, 0xE4, 0x7B, 0x39, 0x1F, 0xEC, 0xCC,
            0x77, 0x0F, 0x36, 0x91, 0x36, 0xBC, 0xB9, 0xBF, 0xA5, 0xBF, 0xCC, 0x0C, 0x4B, 0xA3, 0x19, 0x72,
            0x35, 0xDB, 0xB4, 0xF2, 0x2B, 0xBF, 0x14, 0x9B, 0x34, 0xD2, 0xC3, 0xDE, 0xA8, 0x0E, 0x73, 0x81,
            0xFD, 0xCD, 0xE5, 0xC2, 0x0B, 0x8A, 0xDE, 0x54, 0x4F, 0x3D, 0x3D, 0xEC, 0x02, 0xA2, 0x8E, 0x87,
            0x61, 0xED, 0xCF, 0x99, 0x19, 0xE6, 0x93, 0xDA, 0xBD, 0xE0, 0xAC, 0xA0, 0x21, 0xA1, 0x28, 0x32,
            0x0A, 0x61, 0x94, 0x15, 0x7E, 0xDE, 0xEB, 0x43, 0x4E, 0x7F, 0x9A, 0x57, 0xCC, 0xFF, 0xDB, 0xB9,
            0x10, 0x90, 0x12, 0x5B, 0xC0, 0xA7, 0xF3, 0x6D, 0x69, 0x47, 0xA8, 0x5E, 0x50, 0x36, 0x18, 0xD0,
            0xB0, 0x25, 0x7B, 0xF0, 0x66, 0x25, 0x3C, 0xD0, 0x28, 0xD1, 0x1C, 0xFA, 0x7E, 0x71, 0xB3, 0xF2,
            0x0B, 0xD0, 0x73, 0x80, 0x19, 0x5F, 0xA1, 0xBE, 0xFA, 0xBE, 0x0F, 0x94, 0x05, 0x81, 0xFC, 0x5C,
            0x77, 0x24, 0x4D, 0xCB, 0x3B, 0xFA, 0x54, 0x8E, 0x16, 0x4C, 0xCD, 0xA7, 0x27, 0x2C, 0x40, 0xEB,
            0xDA, 0xA9, 0x8A, 0xA0, 0x66, 0x73, 0x5A, 0x25, 0xC6, 0x25, 0xB8, 0xA9, 0x62, 0xA4, 0x7E, 0xE1,
            0x21, 0x14, 0x44, 0x2D, 0x9E, 0xF9, 0x0A, 0x71, 0x72, 0xBA, 0x89, 0x57, 0x91, 0x34, 0xC7, 0xC1,
            0x87, 0xC3, 0xA7, 0xB0, 0x57, 0xF9, 0x20, 0xCD, 0xE3, 0x55, 0xEF, 0x32, 0x96, 0x6D, 0x25, 0x35,
            0x10, 0x06, 0xD3, 0x97, 0xEA, 0x09, 0xBA, 0x47, 0xBF, 0x1F, 0x31, 0xD1, 0x39, 0xF5, 0xAD, 0x0B,
            0xA6, 0x89, 0xDE, 0x09, 0x6E, 0x37, 0x3F, 0x01, 0x0F, 0xC1, 0xB1, 0x98, 0x80, 0x44, 0xAC, 0x6F,
            0x45, 0xD6, 0xCD, 0x93, 0x70, 0xB7, 0xE7, 0xB7, 0x1E, 0xDF, 0x58, 0x1A, 0x34, 0x2D, 0xB0, 0xE3,
            0x62, 0x8C, 0x3E, 0x18, 0x7B, 0x95, 0x14, 0xF6, 0xE4, 0x71, 0xF5, 0x61, 0x3E, 0x18, 0x95, 0x2B,
            0x3B, 0xB1, 0x75, 0x23, 0x6C, 0x27, 0x6E, 0x7D, 0xF4, 0xAC, 0xE1, 0x3C, 0xC0, 0x44, 0x82, 0xF2,
            0x23, 0xA1, 0xF2, 0x7C, 0xD3, 0xB3, 0x3D, 0xDD, 0x27, 0x22, 0x73, 0xC0, 0x55, 0x7D, 0xE1, 0xFD,
            0xE4, 0x8A, 0x9C, 0x1B, 0x30, 0x2B, 0xE3, 0x87, 0xFC, 0x73, 0xB2, 0x68, 0xE5, 0xFD, 0x3A, 0x7D
        };
    }
}