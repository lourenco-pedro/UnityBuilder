using System;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace UnityBuilder
{
    [CreateAssetMenu(menuName = "UnityBuilder/AndroidPipeline")]
    public class SO_AndroidBuildPipeline : SO_BuildPipeline
    {
        public enum AndroidApp
        {
            AAB,
            APK
        }

        [SerializeField] AndroidApp _buildApp;

        protected string GetAndroidExtension()
        {
            switch(_buildApp)
            {
                default:
                case AndroidApp.AAB: 
                    return ".aab";
                case AndroidApp.APK:
                    return ".apk";
            }
        }

        protected override string GetFilePath()
        {
            return Path.Combine(Environment.GetEnvironmentVariable(ENV_UNITY_BUILDER_ROOT), 
            PlayerSettings.bundleVersion, PlayerSettings.bundleVersion + GetAndroidExtension());
        }

        public override BuildResult Build()
        {
            try
            {
                if (!CheckEnvironments())
                    return BuildResult.INVALID_ENVIRONMENTS;

                string[] levels = _scenesInBuild.Select(AssetDatabase.GetAssetPath).ToArray();

                CreateBuildDirectory();

                string buildPath = GetFilePath();
                
                BuildPipeline.BuildPlayer(levels, buildPath, BuildTarget.Android, _options);

                return BuildResult.SUCCESS;
            }
            catch
            {
                return BuildResult.FAIL_WITH_ERROR;
            }
        }

        [CustomEditor(typeof(SO_AndroidBuildPipeline))]
        public class SO_BuildPipelineAndroid_CustomInspector : Editor
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