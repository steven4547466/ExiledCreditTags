using System.ComponentModel;
using Exiled.API.Interfaces;

namespace ExiledCreditTags
{
    public class Config : IConfig
    {
        [Description("Is the plugin enabled?")]
        public bool IsEnabled { get; set; } = true;
    }
}