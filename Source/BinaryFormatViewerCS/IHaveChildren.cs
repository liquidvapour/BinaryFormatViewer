using System.Collections.Generic;

namespace BinaryFormatViewer
{
    public interface IHaveChildren
    {
        List<Node> Values { get; }
    }
}