using System;
using Com.Dotnet.Cric.Exceptions;

namespace Com.Dotnet.Cric.Requests.Players
{
    public class MergeRequest
    {
        public long PlayerIdToMerge { get; set; }
        public long OriginalPlayerId { get; set; }
    }
}