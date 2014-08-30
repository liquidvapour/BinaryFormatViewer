using System.Collections.Generic;

namespace BinaryFormatViewer
{
    public interface IHaveChildren
    {
        IList<Node> Values { get; }
    }
}