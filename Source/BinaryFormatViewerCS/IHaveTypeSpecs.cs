using System.Collections.Generic;

namespace BinaryFormatViewer
{
    public interface IHaveTypeSpecs
    {
        string Name { get; }

        Node Assembly { get; }

        IList<FieldNode> Fields { get; }
    }
}