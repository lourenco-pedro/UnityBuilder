using System.Diagnostics;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace UnityBuilder.VersionUpdaterTypes
{
    [CreateAssetMenu(menuName = "UnityBuilder/VersionType/Two numbers and git", fileName = "TwoNumbersAndGit")]
    public class S0_VersionUpdater_TwoNumbersAndGit : SO_VersionUpdater
    {
        //Build version is defined by release.build.gitSHA
        
        [SerializeField] bool _updateAndroidVersion;
        [SerializeField] bool _updateIosVersion;
        [SerializeField] string _repositoryPath;
        
        [ContextMenu("Test bundle version")]
        public override void UpdateVersion()
        {
            VersionSettings settings = GetVersionSettings();
            
            Regex versionParser = new Regex("^\\d+\\.\\d+\\.[a-f0-9]+$");
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
                PlayerSettings.bundleVersion = "e0.0.0";
            }
            
            if (_updateAndroidVersion)
                IncreaseAndroidBundleVersion();
                
            if(_updateIosVersion)
                IncreaseIosBuildNumber();

            PlayerSettings.bundleVersion = SetGitNumber(PlayerSettings.bundleVersion);
            
            Debug.Log(PlayerSettings.bundleVersion);
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

        string SetGitNumber(string versionString)
        {
            string[] versionNumbers = versionString.Split(".");

            GitCommitUtility.RunGitCommand($"config --global --add safe.directory {_repositoryPath}", _repositoryPath);
            string result = GitCommitUtility.RetrieveCurrentCommitShorthash(_repositoryPath);
            
            if (!string.IsNullOrEmpty(result))
            {
                Debug.Log("Git SHA: " + result);
                versionNumbers[2] = result.Substring(0, 6);
                return string.Join(".", versionNumbers);
            }
            
            Debug.Log("Could not get git SHA number");
            return versionString;
        }

        [ContextMenu("Print sha")]
        void TestGit()
        {
            SetGitNumber("0.0.aaaaaa");
        }
    }
}