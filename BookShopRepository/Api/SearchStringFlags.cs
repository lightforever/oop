using System;

namespace BookShopRepository.Api
{
    [Flags]
    public enum SearchStringFlags
    {
        ExcactlyMode = 1,
        ContainsMode = 2,
        IgnoreCase = 4
    }
}