using YG.Insides;

namespace YG
{
    public partial class InfoYG
    {
        public PlatformInfo platformInfo = new();
    }
}

namespace YG.Insides
{
    [System.Serializable]
    public partial class PlatformInfo
    {
        /// [ApplySettings] [SelectPlatform] [DeletePlatform]
    }
}