namespace Goedel.Sitebuilder;

public partial class ParsedMultipartFrame : ParsedMultipart {

    const byte dash = (byte)'-';
    const byte dot = (byte)'.';
    const byte cr = (byte)'\r';
    const byte lf = (byte)'\n';
    const byte tab = (byte)'\t';
    const byte space = (byte)' ';


    bool complete = false;

    ParsedMultipartFrame(Stream data) : base(data) {
        }

    public static bool Bind(IBinding data, Stream stream) {

        var result = new ParsedMultipartFrame(stream);
        result.Error = !result.ParseSteam(stream, data);


        return true;
        }

    bool ParseSteam(Stream Stream,
          IBinding formData) {

        var b0 = Stream.ReadByte();
        var b1 = Stream.ReadByte();

        if (b0 != dash | b1 != dash) {
            return false;
            }
        if (!GetBoundary()) {
            return false;
            }

        while (!complete) {
            var fieldData = new FieldData();

            if (!GetHeaders(fieldData)) {
                return false;
                }
            if (fieldData.Name is null) {
                if (!GetContent(out var content)) {
                    return false;
                    }
                }
            else if (!formData._Binding.AllProperties.TryGetValue(fieldData.Name, out var property)) {
                if (!GetContent(out var content)) {
                    return false;
                    }
                }
            else {
                switch (property) {
                    case FrameString frameString: {
                        if (!GetContent(out var content)) {
                            return false;
                            }
                        var text = content.ToUTF8();

                        frameString.Set(formData, text);
                        break;
                        }
                    case FrameInteger frameInteger: {
                        if (!GetContent(out var content)) {
                            return false;
                            }
                        var text = content.ToUTF8();
                        var integer = Int32.Parse(text);

                        frameInteger.Set(formData, integer);
                        break;
                        }
                    case FrameBoolean frameBoolean: {
                        if (!GetContent(out var content)) {
                            return false;
                            }
                        var text = content.ToUTF8();
                        break;
                        }
                    case FrameImage frameImage: {
                        if (!GetContent(out var content)) {
                            return false;
                            }

                        break;
                        }

                    default: {
                        if (!GetContent(out var content)) {
                            return false;
                            }
                        break;
                        }
                    }
                }

            if (!CheckContentEnd()) {
                return false;
                }
            }

        return true;
        }


    }
