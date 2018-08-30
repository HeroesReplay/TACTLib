﻿using System.Diagnostics;
using System.IO;
using TACTLib.Config;
using TACTLib.Container;
using TACTLib.Core;
using TACTLib.Core.Product;
using TACTLib.Core.Product.Tank;
using TACTLib.Helpers;

namespace TACTLib.Client {
    public class ClientHandler {
        /// <summary>
        /// The <see cref="Product"/> that this container belongs to.
        /// </summary>
        public readonly Product Product;

        /// <summary>
        /// The installation info of the container
        /// </summary>
        /// <seealso cref="InstallInfoFileName"/>
        public readonly InstallationInfo InstallationInfo;
        
        /// <summary>Container handler</summary>
        public readonly ContainerHandler ContainerHandler;
        
        /// <summary>Encoding table handler</summary>
        public readonly EncodingHandler EncodingHandler;
        
        /// <summary>Configuration handler</summary>
        public readonly ConfigHandler ConfigHandler;

        /// <summary>Virtual File System</summary>
        public readonly VFSFileTree VFS;

        /// <summary>Product specific Root File handler</summary>
        public readonly IProductHandler ProductHandler;

        /// <summary>The base path of the container. E.g where the game executables are.</summary>
        public readonly string BasePath;

        /// <summary>Name of the installation info file</summary>
        /// <seealso cref="InstallationInfo"/>
        public const string InstallInfoFileName = ".build.info";

        public readonly ClientCreateArgs CreateArgs;

        public ClientHandler(string basePath, ClientCreateArgs createArgs) {
            BasePath = basePath;
            CreateArgs = createArgs;
            
            Product = ProductHelpers.ProductFromLocalInstall(basePath);
            
            string installationInfoPath = Path.Combine(basePath, InstallInfoFileName) + createArgs.ExtraFileEnding;
            if (!File.Exists(installationInfoPath)) {
                throw new FileNotFoundException(installationInfoPath);
            }
            using (var _ = new PerfCounter("InstallationInfo::ctor"))
                InstallationInfo = new InstallationInfo(installationInfoPath);
            
            using (var _ = new PerfCounter("ContainerHandler::ctor"))
                ContainerHandler = new ContainerHandler(this);
            using (var _ = new PerfCounter("ConfigHandler::ctor"))
                ConfigHandler = new ConfigHandler(this);
    
            using (var _ = new PerfCounter("EncodingHandler::ctor"))
                EncodingHandler = new EncodingHandler(this);

            if (ConfigHandler.BuildConfig.VFSRoot != null) {
                using (var _ = new PerfCounter("VFSFileTree::ctor"))
                    VFS = new VFSFileTree(this);
            }

            if (Product == Product.Overwatch) {
                using (var _ = new PerfCounter("ProductHandler_Tank::ctor"))
                    ProductHandler = new ProductHandler_Tank(this, OpenCKey(ConfigHandler.BuildConfig.Root.ContentKey));
            }
        }

        /// <summary>
        /// Open a file from Content Key
        /// </summary>
        /// <param name="key">Content Key of the file</param>
        /// <returns>Loaded file</returns>
        public Stream OpenCKey(CKey key) {
            if (EncodingHandler.TryGetEncodingEntry(key, out EncodingHandler.CKeyEntry entry)) {
                return OpenEKey(entry.EKey);
            }
            Debugger.Log(0, "ContainerHandler", $"Missing encoding entry for CKey {key.ToHexString()}");
            return null;
        }

        /// <summary>
        /// Open a file from Encoding Key
        /// </summary>
        /// <param name="key">The Encoding Key</param>
        /// <returns>Loaded file</returns>
        public Stream OpenEKey(EKey key) {  // ekey = value of ckey in encoding table
            var stream = ContainerHandler.OpenEKey(key);
            return stream == null ? null : new BLTEStream(this, stream);
        }
    }
}