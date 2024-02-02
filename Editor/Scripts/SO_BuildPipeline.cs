using System;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace UnityBuilder
{
    public abstract class SO_BuildPipeline : ScriptableObject
    {
        public const string ENV_UNITY_BUILDER_ROOT = "UNITY_BUILDER_ROOT";

        public enum BuildResult
        {
            SUCCESS,
            INVALID_ENVIRONMENTS,
            FAIL_WITH_ERROR
        };
        [SerializeField] protected BuildOptions _options;
        [SerializeField] protected SceneAsset[] _scenesInBuild;
        
        public abstract BuildResult Build();
        protected abstract string GetFilePath();

        protected bool CheckEnvironments()
        {
            string[] environments = new[]
            {
                ENV_UNITY_BUILDER_ROOT
            };

            Debug.Log(Environment.GetEnvironmentVariable(ENV_UNITY_BUILDER_ROOT));
            
            string[] invalidEnvironments = environments
                .Where(env=> string.IsNullOrEmpty(Environment.GetEnvironmentVariable(env)))
                .ToArray();

            if (invalidEnvironments.Length > 0)
            {
                Debug.Log($"Could not proceed with the build due to the following invalid Environment variables: { string.Join( ',', invalidEnvironments) }");
                return false;
            }

            return true;
        }

        protected virtual void CreateBuildDirectory()
        {
            Directory.CreateDirectory
            (
                Path.Combine(Environment.GetEnvironmentVariable(ENV_UNITY_BUILDER_ROOT), PlayerSettings.bundleVersion)
            );
        }
    }
}
