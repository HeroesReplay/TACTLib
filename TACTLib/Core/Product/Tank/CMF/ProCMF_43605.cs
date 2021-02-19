﻿using static TACTLib.Core.Product.Tank.ManifestCryptoHandler;
using static TACTLib.Core.Product.Tank.ContentManifestFile;

namespace TACTLib.Core.Product.Tank.CMF {
    [ManifestCrypto(AutoDetectVersion = true, Product = TACTProduct.Overwatch)]
    public class ProCMF_43605 : ICMFEncryptionProc {
        public byte[] Key(CMFHeader header, int length) {
            byte[] buffer = new byte[length];

            uint kidx = Keytable[header.m_buildVersion & 511];
            for (int i = 0; i != length; ++i) {
                buffer[i] = Keytable[SignedMod(kidx, 512)];
                kidx -= 0x55;
            }

            return buffer;
        }

        public byte[] IV(CMFHeader header, byte[] digest, int length) {
            byte[] buffer = new byte[length];

            uint kidx = Keytable[header.m_dataCount & 511];
            uint increment = (uint)header.m_entryCount + digest[SignedMod(header.m_entryCount, SHA1_DIGESTSIZE)];
            for (int i = 0; i != length; ++i) {
                kidx += increment;
                buffer[i] ^= digest[SignedMod(kidx, SHA1_DIGESTSIZE)];
            }

            return buffer;
        }

        private static readonly byte[] Keytable = {
            0x77, 0xF0, 0xDF, 0xCA, 0x53, 0x1A, 0x9E, 0x77, 0xDD, 0x1F, 0x0B, 0x5E, 0xE2, 0x41, 0xA4, 0x4F,
            0xD2, 0x93, 0x5F, 0xA9, 0x04, 0xAB, 0x4F, 0xD1, 0xD9, 0xD6, 0x4C, 0x76, 0xF1, 0xD0, 0x51, 0xBA,
            0xD2, 0x70, 0x94, 0xD1, 0x6D, 0x01, 0x2D, 0x95, 0x67, 0x70, 0xE9, 0xF7, 0x04, 0xA3, 0x87, 0x16,
            0xCC, 0xF7, 0x7A, 0x7D, 0xA6, 0x6A, 0x6F, 0x25, 0xC6, 0xEE, 0x31, 0x63, 0x07, 0xF7, 0x26, 0x3D,
            0x6D, 0xC7, 0xE7, 0x7E, 0x33, 0x68, 0xCA, 0x01, 0x09, 0x20, 0xBD, 0x9F, 0x6A, 0x6C, 0x56, 0xD0,
            0x43, 0xBA, 0xCC, 0x51, 0x35, 0xF7, 0x8A, 0x93, 0x2C, 0xA0, 0x31, 0x62, 0xC3, 0x10, 0xF1, 0x38,
            0x2A, 0x38, 0xF2, 0xB9, 0xF0, 0x15, 0x92, 0x34, 0x02, 0x7D, 0xF1, 0x4A, 0x4B, 0x7E, 0x22, 0xE9,
            0x71, 0x97, 0x84, 0xF3, 0x10, 0x4E, 0x3C, 0x1F, 0xFD, 0xD2, 0xBA, 0xC7, 0xDF, 0x31, 0xC7, 0x55,
            0x57, 0x35, 0x86, 0x38, 0x6C, 0x75, 0xAC, 0xD6, 0x6B, 0xE0, 0x0B, 0x67, 0x18, 0x0F, 0xC1, 0x5B,
            0x3A, 0x35, 0xD3, 0xC1, 0x79, 0x87, 0x33, 0xF1, 0x2F, 0x4A, 0x66, 0x7E, 0x60, 0xD5, 0x14, 0xF8,
            0x91, 0x4B, 0x05, 0x40, 0xB3, 0xB0, 0x05, 0x25, 0xE0, 0xE0, 0x33, 0x56, 0xEC, 0x1D, 0x04, 0xF7,
            0xDA, 0x01, 0xE4, 0x0E, 0x4D, 0x8D, 0x2D, 0x65, 0xA5, 0xB4, 0x33, 0x8D, 0x5D, 0xB8, 0xC8, 0x75,
            0x29, 0x85, 0x2A, 0x7E, 0x7F, 0xBE, 0xD5, 0x3E, 0xCA, 0x79, 0x77, 0x48, 0x82, 0x63, 0x59, 0x47,
            0x8B, 0xAD, 0x02, 0x62, 0xC7, 0x8C, 0xB3, 0x37, 0x2E, 0xBC, 0x55, 0x69, 0xF5, 0xA4, 0x91, 0xEB,
            0xC6, 0xAE, 0x4A, 0xC4, 0xAF, 0x0D, 0x1B, 0x70, 0x77, 0x07, 0x76, 0x44, 0x07, 0x3F, 0xCF, 0xA8,
            0xBB, 0xCB, 0x92, 0x18, 0x29, 0x58, 0xCA, 0xD1, 0x9A, 0xF0, 0x65, 0xFC, 0x4A, 0xDE, 0x73, 0xDC,
            0x80, 0xB6, 0xEE, 0xD1, 0xCA, 0x69, 0x15, 0xC7, 0xCB, 0x9C, 0xE1, 0xBB, 0xEF, 0x94, 0xC7, 0xA7,
            0xDF, 0xB4, 0x5B, 0x31, 0xCB, 0x03, 0xA0, 0xBE, 0xA1, 0x34, 0xF6, 0xCC, 0x89, 0x0A, 0xEF, 0xAC,
            0xBD, 0x4C, 0x94, 0xBE, 0x40, 0x2C, 0xCE, 0x48, 0xC6, 0x93, 0xB4, 0x60, 0xA5, 0x4A, 0x21, 0x33,
            0x93, 0x00, 0x85, 0xB9, 0xB4, 0x70, 0xFD, 0xEC, 0xC9, 0x4C, 0x74, 0x28, 0x4E, 0x43, 0xFA, 0x1A,
            0xEB, 0xB3, 0xA0, 0xEB, 0x33, 0x13, 0x73, 0x25, 0x58, 0x0F, 0x73, 0xAC, 0x47, 0x0F, 0x71, 0xD6,
            0x3E, 0x5B, 0xCA, 0xD8, 0x9C, 0x7A, 0xE4, 0x86, 0xD3, 0x0B, 0x38, 0x2A, 0xCB, 0xA5, 0xD3, 0xC5,
            0x15, 0xA9, 0x0B, 0x4F, 0x5F, 0xE9, 0x03, 0x05, 0x4C, 0xE7, 0x3A, 0xE2, 0x15, 0xD2, 0x2C, 0xF9,
            0xF5, 0x5F, 0x5F, 0xD7, 0x20, 0x6D, 0xB1, 0xFF, 0xED, 0x94, 0x14, 0x4B, 0xC0, 0x07, 0x2E, 0xC8,
            0x7C, 0xF7, 0x4C, 0x1D, 0xC1, 0x5D, 0x9A, 0x58, 0x38, 0x15, 0x71, 0x35, 0x14, 0xB3, 0xCB, 0x89,
            0xAB, 0x50, 0xF0, 0x2B, 0x1B, 0x44, 0x00, 0x95, 0x6D, 0xD6, 0x6B, 0x55, 0xDE, 0x49, 0xB4, 0xC7,
            0xBB, 0xB4, 0xAC, 0x28, 0x4B, 0xB5, 0x28, 0x37, 0xE3, 0xD5, 0xEA, 0x00, 0x82, 0x20, 0xD1, 0x49,
            0xE3, 0xDB, 0x3C, 0x41, 0x82, 0x89, 0x2B, 0x5E, 0xB7, 0xA6, 0x1F, 0x29, 0xBB, 0x7F, 0x5A, 0xF1,
            0x9A, 0xE0, 0x73, 0x7C, 0x97, 0x5F, 0x2B, 0xA6, 0xB5, 0x18, 0x67, 0xE6, 0x7E, 0x19, 0x34, 0xDB,
            0x80, 0xD6, 0xC3, 0xDD, 0x22, 0x27, 0xB7, 0xB7, 0x23, 0xEA, 0xE4, 0x45, 0x9F, 0x75, 0xC2, 0xE1,
            0x9E, 0xA3, 0xC1, 0x3B, 0x42, 0x35, 0x83, 0xF9, 0xFD, 0x63, 0x14, 0x9B, 0xFE, 0x8C, 0x1E, 0x1F,
            0xF6, 0xDC, 0x31, 0xA8, 0x53, 0x1C, 0x0B, 0xC8, 0xCE, 0xC9, 0x21, 0xA2, 0x1D, 0xE0, 0x3B, 0x08
        };
    }
}