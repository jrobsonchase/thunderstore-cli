﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ThunderstoreCLI.Config
{
    public class Config
    {
        public PackageMeta PackageMeta { get; private set; }
        public BuildConfig BuildConfig { get; private set; }
        public PublishConfig PublishConfig { get; private set; }

        private Config(PackageMeta packageMeta, BuildConfig buildConfig, PublishConfig publishConfig)
        {
            PackageMeta = packageMeta;
            BuildConfig = buildConfig;
            PublishConfig = publishConfig;
        }

        public static Config Parse(params IConfigProvider[] configProviders)
        {
            var packageMeta = new PackageMeta();
            var buildConfig = new BuildConfig();
            var publishConfig = new PublishConfig();
            foreach (var provider in configProviders)
            {
                provider.Parse();
                Merge(packageMeta, provider.GetPackageMeta(), false);
                Merge(buildConfig, provider.GetBuildConfig(), false);
                Merge(publishConfig, provider.GetPublishConfig(), false);
            }
            return new Config(packageMeta, buildConfig, publishConfig);
        }

        public static void Merge<T>(T target, T source, bool overwrite)
        {
            if (source == null)
                return;

            var t = typeof(T);
            var properties = t.GetProperties();

            foreach (var prop in properties)
            {
                var sourceVal = prop.GetValue(source, null);
                if (sourceVal != null)
                {
                    var targetVal = prop.GetValue(target, null);
                    if (targetVal == null || overwrite)
                        prop.SetValue(target, sourceVal, null);
                }
            }
        }
    }

    public class PackageMeta
    {
        public string Author { get; set; }
        public string Name { get; set; }
        public string VersionNumber { get; set; }
        public string Description { get; set; }
        public string WebsiteUrl { get; set; }
        public Dictionary<string, string> Dependencies { get; set; }
    }

    public class BuildConfig
    {
        public string IconPath { get; set; }
        public string ReadmePath { get; set; }
        public string OutDir { get; set; }
        public Dictionary<string, string> CopyPaths { get; set; }
    }

    public class PublishConfig
    {
        public string Repository { get; set; }
        public string AuthToken { get; set; }
    }
}