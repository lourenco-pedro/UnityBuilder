using System;
using UnityEditor;
using UnityEngine;

namespace UnityBuilder
{
    [CreateAssetMenu(menuName = "UnityBuilder/BuildCmd", fileName = "BuildCmd")]
    public class BuildCmd : ScriptableObject
    {
        public SO_BuildPipeline pipeline;
        public SO_VersionUpdater version;
        
        public static void Build()
        {
            try
            {
                Debug.Log("Loading BuildCmd sriptableObject...");

                BuildCmd cmd = Resources.Load<BuildCmd>("BuildCmd");

                Debug.Log($"UnityBuilder will use the {cmd.pipeline.name} pipeline for this build...");
                Debug.Log("Start building...");
                
                if(null != cmd.version)
                    cmd.version.UpdateVersion();
                else
                {
                    PlayerSettings.bundleVersion = "e0.0.0";
                    Debug.Log("Could not update Player. No version updater file was selected.");
                }

                Debug.Log("build version is: " + PlayerSettings.bundleVersion);
                
                SO_BuildPipeline.BuildResult result = cmd.pipeline.Build();

                LogBuildResult(result);
                
                if (result == SO_BuildPipeline.BuildResult.SUCCESS)
                {
                    EditorApplication.Exit(0);
                }
                else
                {
                    EditorApplication.Exit(1);
                }

            }
            catch(Exception e)
            {
                Debug.Log(e.Message);
                EditorApplication.Exit(1);
            }
        }

        static void LogBuildResult(SO_BuildPipeline.BuildResult result)
        {
            switch (result)
            {
                case SO_BuildPipeline.BuildResult.SUCCESS:
                    Debug.Log("Build Success!");
                    break;
                case SO_BuildPipeline.BuildResult.INVALID_ENVIRONMENTS:
                    Debug.Log($"Build Failed due to { result }. Please certify the right environment variables " +
                              $"are created before proceeding with the build.");
                    break;
            }
        }
    }
}
