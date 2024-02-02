using System;
using System.IO;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

namespace UnityBuilder
{
    public class PostBuildSteps
    {
        [PostProcessBuild(1)]
        public static void PostBuildAction(BuildTarget target, string pathToBuildProject)
        {
            Debug.Log("Build proccess complete, updating VersionSettings file...");
            VersionSettings versionSettings = GetVersionSettings();
            versionSettings.path = pathToBuildProject;
            
            SaveVersionSettings(versionSettings);
        }
        
        static VersionSettings GetVersionSettings()
        {
            string envRoot = Environment.GetEnvironmentVariable(SO_BuildPipeline.ENV_UNITY_BUILDER_ROOT);
            if (string.IsNullOrEmpty(envRoot))
            {
                return new VersionSettings();
            }

            string envSettingsFileContent = File.ReadAllText(Path.Combine(envRoot, "versionSettings.json"));
            return JsonUtility.FromJson<VersionSettings>(envSettingsFileContent);
        }

        static void SaveVersionSettings(VersionSettings settings)
        {
            string envRoot = Environment.GetEnvironmentVariable(SO_BuildPipeline.ENV_UNITY_BUILDER_ROOT);
            string json = JsonUtility.ToJson(settings);

            string path = Path.Combine(envRoot, "versionSettings.json");
            
            File.WriteAllText(path, json);

            Debug.Log("Saved version Settings with new build Path: " + settings.path);
        }
    }
}