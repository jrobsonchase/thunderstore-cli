﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ThunderstoreCLI.Config
{
    class UserFileConfig : IConfigProvider
    {
        public void Parse() { }

        public PackageMeta GetPackageMeta()
        {
            return null;
        }

        public BuildConfig GetBuildConfig()
        {
            return null;
        }

        public PublishConfig GetPublishConfig()
        {
            return null;
        }
    }
}