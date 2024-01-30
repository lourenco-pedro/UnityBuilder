using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

namespace UnityBuilder.VersionUpdaterTypes
{
    [CreateAssetMenu(menuName = "UnityBuilder/VersionType/Release and Build number", fileName = "ReleaseandBuildNumber")]
    public class SO_VersionUpdater_ReleaseAndBuild : SO_VersionUpdater
    {
        public override void UpdateVersion()
        {
            VersionSettings settings = GetVersionSettings();
            
            Regex versionParser = new Regex("^\\d+\\.\\d+$");
            Match match = versionParser.Match(PlayerSettings.bundleVersion);
            
            if (match.Success)
            {
                if (settings.isRelease)
                {
                    PlayerSettings.bundleVersion = IncreaseReleaseVersion(PlayerSettings.bundleVersion);
                }

                PlayerSettings.bundleVersion = IncreaseBuildNumberVersion(PlayerSettings.bundleVersion);
            }
            else
            {
                PlayerSettings.bundleVersion = "0.0";
            }
        }
        
        string IncreaseReleaseVersion(string versionString)
        {
            string[] versionNumbers = versionString.Split(".");
            if (int.TryParse(versionNumbers[0], out int buildVersion))
            {
                buildVersion += 1;
                versionNumbers[0] = buildVersion.ToString();
                versionNumbers[1] = "0";
                return string.Join('.', versionNumbers);
            }

            return versionString;
        }

        string IncreaseBuildNumberVersion(string versionString)
        {
            string[] versionNumbers = versionString.Split(".");
            if (int.TryParse(versionNumbers[1], out int buildVersion))
            {
                buildVersion += 1;
                versionNumbers[1] = buildVersion.ToString();
                return string.Join('.', versionNumbers);
            }

            return versionString;
        }
        
        [ContextMenu("Set version")]
        void TestGit()
        {
            UpdateVersion();
            Debug.Log(PlayerSettings.bundleVersion);
        }
    }
}