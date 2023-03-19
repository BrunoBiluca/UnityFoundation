using System.Collections.Generic;

namespace UnityFoundation.Code
{
    public interface IGridLimits<TPosition>
    {
        bool IsInside(TPosition coordinate);
        int GetIndex(TPosition coordinate);
        IEnumerable<int> GetIndexes();
    }
}
