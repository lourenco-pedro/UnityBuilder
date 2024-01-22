using System;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace UnityBuilder
{
    public abstract class SO_VersionUpdater : ScriptableObject
    {
        public abstract void UpdateVersion();

        public VersionSettings GetVersionSettings()
        {
            string envRoot = Environment.GetEnvironmentVariable(SO_BuildPipeline.ENV_UNITY_BUILDER_ROOT);
            if (string.IsNullOrEmpty(envRoot))
            {
                return new VersionSettings();
            }

            string envSettingsFileContent = File.ReadAllText(Path.Combine(envRoot, "versionSettings.json"));
            return JsonUtility.FromJson<VersionSettings>(envSettingsFileContent);
        }

        public void IncreaseAndroidBundleVersion()
        {
            PlayerSettings.Android.bundleVersionCode++;
        }

        public void IncreaseIosBuildNumber()
        {
            if (int.TryParse(PlayerSettings.iOS.buildNumber, out int buildNumber))
            {
                buildNumber += 1;
                PlayerSettings.iOS.buildNumber = buildNumber.ToString();
            }
        }
    }
}