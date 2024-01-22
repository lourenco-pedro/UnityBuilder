using System;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace UnityBuilder
{
    [CreateAssetMenu(menuName = "UnityBuilder/BuilderFile")]
    public class SO_BuildPipeline : ScriptableObject
    {

        public const string ENV_UNITY_BUILDER_ROOT = "UNITY_BUILDER_ROOT";

        public enum BuildResult
        {
            SUCCESS,
            INVALID_ENVIRONMENTS
        };

        public BuildTarget BuildTarget => _target;
        
        [SerializeField] BuildTarget _target;
        [SerializeField] BuildOptions _options;
        [SerializeField] SceneAsset[] _scenesInBuild;
        
        public BuildResult Build()
        {
            if(!CheckEnvironments())
                return BuildResult.INVALID_ENVIRONMENTS;
                
            string[] levels = _scenesInBuild.Select(AssetDatabase.GetAssetPath).ToArray();

            Directory.CreateDirectory
            (Path.Combine(Environment.GetEnvironmentVariable(ENV_UNITY_BUILDER_ROOT), PlayerSettings.bundleVersion));
            
            string buildPath = Path.Combine(Environment.GetEnvironmentVariable(ENV_UNITY_BUILDER_ROOT), PlayerSettings.bundleVersion, PlayerSettings.bundleVersion+".exe");

            BuildReport report = BuildPipeline.BuildPlayer(levels, buildPath, _target, _options);

            return BuildResult.SUCCESS;
        }

        bool CheckEnvironments()
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
        
        [CustomEditor(typeof(SO_BuildPipeline))]
        public class SO_BuildPipeline_CustomInspector : Editor
        {
            public override void OnInspectorGUI()
            {
                base.OnInspectorGUI();

                bool build = GUILayout.Button("Build!");
                if (build)
                {
                    SO_BuildPipeline pipeLine = target as SO_BuildPipeline;
                    pipeLine.Build();
                }
            }
        }
    }
}
