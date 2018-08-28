﻿namespace TACTLib.Core.Product.Tank {
    public interface ICMFEncryptionProc {
        byte[] Key(ContentManifestFile.CMFHeader header, int length);
        byte[] IV(ContentManifestFile.CMFHeader header, byte[] digest, int length);
    }
}