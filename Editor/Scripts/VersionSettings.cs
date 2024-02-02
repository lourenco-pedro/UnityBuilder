namespace UnityBuilder
{
    public class VersionSettings
    {
        public bool isRelease;
        public string path;
        
        void SetBuildPathInternal(string path)
        {
            this.path = path;
        }
    }
}