
namespace Goedel.Document.RFC;

public interface IAnnotation {

    string User { get; set; }
    string Anchor { get; set; }
    List<string> References { get; set; }
    string Semantic { get; set; }
    string Text { get; set; }

    bool Written { get; set; }

    }