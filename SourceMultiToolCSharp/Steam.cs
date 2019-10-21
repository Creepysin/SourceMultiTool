using System.Collections.Generic;

namespace SourceMultiToolCSharp
{
    public class Steam
    {
        public string MainSteamDir { get; set; }
        public List<string> AdditionalSteamDirectories { get; set; } = new List<string>();
    }
}
