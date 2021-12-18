using System;
using System.Collections.Generic;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using HT = Goedel.Document.RFC;

namespace Goedel.Document.OpenXML {
    public partial class MakeWord {

        private void AddStylesPartToPackage() {
            TargetStyles = Target.MainDocumentPart.StyleDefinitionsPart;
            if (TargetStyles != null) {
                return;
                }

            TargetStyles = Target.MainDocumentPart.AddNewPart<StyleDefinitionsPart>();
            Styles root = new();
            root.Save(TargetStyles);

            return;
            }

        private void AddNewStyle(string styleid, string stylename,
                bool Mono, int HPS, int Indent) {
            if (Mono) {
                AddNewStyle(styleid, stylename, "Courier New", HPS, Indent);
                }
            else {
                AddNewStyle(styleid, stylename, null, HPS, Indent);
                }
            }

        private void AddNewStyle(string styleid, string stylename,
                string Font, int HPS, int Indent) => AddNewStyle(styleid, stylename, Font, HPS,
                    null, false, false, Indent, 0, false);

        private void AddHeaderStyle(string styleid, string stylename,
                int HPS, bool italic, bool bold, int level) {
            Color color1 = new() { ThemeColor = ThemeColorValues.Accent1 };
            AddNewStyle(styleid, stylename, "Calibri Light", HPS,
                color1, false, false, 0, level, false);


            }

        private void AddNewStyle(string styleid, string stylename,
                string Font, int HPS, Color color, bool italic, bool bold,
                int Indent, int level, bool Closeup) {
            // Get access to the root element of the styles part.
            Styles styles = TargetStyles.Styles;

            // Create a new paragraph style and specify some of the properties.
            Style style = new() {
                Type = StyleValues.Paragraph,
                StyleId = styleid,
                CustomStyle = true,
                PrimaryStyle = new PrimaryStyle(),
            };
            StyleName styleName1 = new() { Val = styleid };
            BasedOn basedOn1 = new() { Val = "Normal" };
            NextParagraphStyle nextParagraphStyle1 = new() { Val = "Normal" };
            style.Append(styleName1);
            style.Append(basedOn1);
            style.Append(nextParagraphStyle1);

            // Create the StyleRunProperties object and specify some of the run properties.
            StyleRunProperties styleRunProperties1 = new();

            if (bold) {
                Bold bold1 = new();
                styleRunProperties1.Append(bold1);
                }

            if (italic) {
                Italic italic1 = new();
                styleRunProperties1.Append(italic1);
                }

            if (color != null) {
                styleRunProperties1.Append(color);
                }

            if (Closeup) {
                //styleRunProperties1.Append(color);
                }


            // Set up the font
            if (Font != null) {
                RunFonts font1 = new() { Ascii = Font };
                styleRunProperties1.Append(font1);
                }

            FontSize fontSize1 = new() { Val = HPS.ToString() };
            styleRunProperties1.Append(fontSize1);

            // Add the run properties to the style.
            style.Append(styleRunProperties1);


            var StyleParagraphProperties = new StyleParagraphProperties();
            if (Closeup) {
                var att = new ContextualSpacing() { Val = true };
                StyleParagraphProperties.Append(att);
                }

            if (level > 0) {
                StyleParagraphProperties.OutlineLevel = new OutlineLevel {
                    Val = (level - 1)
                };
                }

            style.StyleParagraphProperties = StyleParagraphProperties;


            // Add the style to the styles part.
            styles.Append(style);
            }


        private Style MakeStyle(string styleid) {
            // Get access to the root element of the styles part.
            Styles styles = TargetStyles.Styles;

            // Create a new paragraph style and specify some of the properties.
            Style style = new() {
                Type = StyleValues.Paragraph,
                StyleId = styleid,
                CustomStyle = true,
                PrimaryStyle = new PrimaryStyle(),
                StyleRunProperties = new StyleRunProperties(),
                StyleParagraphProperties = new StyleParagraphProperties(),
            };
            StyleName styleName1 = new() { Val = styleid };
            BasedOn basedOn1 = new() { Val = "Normal" };
            NextParagraphStyle nextParagraphStyle1 = new() { Val = "Normal" };
            style.Append(styleName1);
            style.Append(basedOn1);
            style.Append(nextParagraphStyle1);
            styles.Append(style);

            return style;
            }

        // Creates an Numbering instance and adds its children.
        private void GeneratePartContent(NumberingDefinitionsPart part) {
            Numbering numbering1 = new() { MCAttributes = new MarkupCompatibilityAttributes() { Ignorable = "w14 w15 wp14" } };
            numbering1.AddNamespaceDeclaration("wpc", "http://schemas.microsoft.com/office/word/2010/wordprocessingCanvas");
            numbering1.AddNamespaceDeclaration("mc", "http://schemas.openxmlformats.org/markup-compatibility/2006");
            numbering1.AddNamespaceDeclaration("o", "urn:schemas-microsoft-com:office:office");
            numbering1.AddNamespaceDeclaration("r", "http://schemas.openxmlformats.org/officeDocument/2006/relationships");
            numbering1.AddNamespaceDeclaration("m", "http://schemas.openxmlformats.org/officeDocument/2006/math");
            numbering1.AddNamespaceDeclaration("v", "urn:schemas-microsoft-com:vml");
            numbering1.AddNamespaceDeclaration("wp14", "http://schemas.microsoft.com/office/word/2010/wordprocessingDrawing");
            numbering1.AddNamespaceDeclaration("wp", "http://schemas.openxmlformats.org/drawingml/2006/wordprocessingDrawing");
            numbering1.AddNamespaceDeclaration("w10", "urn:schemas-microsoft-com:office:word");
            numbering1.AddNamespaceDeclaration("w", "http://schemas.openxmlformats.org/wordprocessingml/2006/main");
            numbering1.AddNamespaceDeclaration("w14", "http://schemas.microsoft.com/office/word/2010/wordml");
            numbering1.AddNamespaceDeclaration("w15", "http://schemas.microsoft.com/office/word/2012/wordml");
            numbering1.AddNamespaceDeclaration("wpg", "http://schemas.microsoft.com/office/word/2010/wordprocessingGroup");
            numbering1.AddNamespaceDeclaration("wpi", "http://schemas.microsoft.com/office/word/2010/wordprocessingInk");
            numbering1.AddNamespaceDeclaration("wne", "http://schemas.microsoft.com/office/word/2006/wordml");
            numbering1.AddNamespaceDeclaration("wps", "http://schemas.microsoft.com/office/word/2010/wordprocessingShape");

            AbstractNum abstractNum1 = new() { AbstractNumberId = 0 };
            Nsid nsid1 = new() { Val = "1BB821F4" };
            MultiLevelType multiLevelType1 = new() { Val = MultiLevelValues.HybridMultilevel };
            TemplateCode templateCode1 = new() { Val = "9322017E" };

            Level level1 = new() { LevelIndex = 0, TemplateCode = "04090001" };
            StartNumberingValue startNumberingValue1 = new() { Val = 1 };
            NumberingFormat numberingFormat1 = new() { Val = NumberFormatValues.Bullet };
            LevelText levelText1 = new() { Val = "·" };
            LevelJustification levelJustification1 = new() { Val = LevelJustificationValues.Left };

            PreviousParagraphProperties previousParagraphProperties1 = new();
            Indentation indentation1 = new() { Start = "720", Hanging = "360" };

            previousParagraphProperties1.Append(indentation1);

            NumberingSymbolRunProperties numberingSymbolRunProperties1 = new();
            RunFonts runFonts1 = new() { Hint = FontTypeHintValues.Default, Ascii = "Symbol", HighAnsi = "Symbol" };

            numberingSymbolRunProperties1.Append(runFonts1);

            level1.Append(startNumberingValue1);
            level1.Append(numberingFormat1);
            level1.Append(levelText1);
            level1.Append(levelJustification1);
            level1.Append(previousParagraphProperties1);
            level1.Append(numberingSymbolRunProperties1);

            Level level2 = new() { LevelIndex = 1, TemplateCode = "04090003", Tentative = true };
            StartNumberingValue startNumberingValue2 = new() { Val = 1 };
            NumberingFormat numberingFormat2 = new() { Val = NumberFormatValues.Bullet };
            LevelText levelText2 = new() { Val = "o" };
            LevelJustification levelJustification2 = new() { Val = LevelJustificationValues.Left };

            PreviousParagraphProperties previousParagraphProperties2 = new();
            Indentation indentation2 = new() { Start = "1440", Hanging = "360" };

            previousParagraphProperties2.Append(indentation2);

            NumberingSymbolRunProperties numberingSymbolRunProperties2 = new();
            RunFonts runFonts2 = new() { Hint = FontTypeHintValues.Default, Ascii = "Courier New", HighAnsi = "Courier New", ComplexScript = "Courier New" };

            numberingSymbolRunProperties2.Append(runFonts2);

            level2.Append(startNumberingValue2);
            level2.Append(numberingFormat2);
            level2.Append(levelText2);
            level2.Append(levelJustification2);
            level2.Append(previousParagraphProperties2);
            level2.Append(numberingSymbolRunProperties2);

            Level level3 = new() { LevelIndex = 2, TemplateCode = "04090005", Tentative = true };
            StartNumberingValue startNumberingValue3 = new() { Val = 1 };
            NumberingFormat numberingFormat3 = new() { Val = NumberFormatValues.Bullet };
            LevelText levelText3 = new() { Val = "§" };
            LevelJustification levelJustification3 = new() { Val = LevelJustificationValues.Left };

            PreviousParagraphProperties previousParagraphProperties3 = new();
            Indentation indentation3 = new() { Start = "2160", Hanging = "360" };

            previousParagraphProperties3.Append(indentation3);

            NumberingSymbolRunProperties numberingSymbolRunProperties3 = new();
            RunFonts runFonts3 = new() { Hint = FontTypeHintValues.Default, Ascii = "Wingdings", HighAnsi = "Wingdings" };

            numberingSymbolRunProperties3.Append(runFonts3);

            level3.Append(startNumberingValue3);
            level3.Append(numberingFormat3);
            level3.Append(levelText3);
            level3.Append(levelJustification3);
            level3.Append(previousParagraphProperties3);
            level3.Append(numberingSymbolRunProperties3);

            Level level4 = new() { LevelIndex = 3, TemplateCode = "04090001", Tentative = true };
            StartNumberingValue startNumberingValue4 = new() { Val = 1 };
            NumberingFormat numberingFormat4 = new() { Val = NumberFormatValues.Bullet };
            LevelText levelText4 = new() { Val = "·" };
            LevelJustification levelJustification4 = new() { Val = LevelJustificationValues.Left };

            PreviousParagraphProperties previousParagraphProperties4 = new();
            Indentation indentation4 = new() { Start = "2880", Hanging = "360" };

            previousParagraphProperties4.Append(indentation4);

            NumberingSymbolRunProperties numberingSymbolRunProperties4 = new();
            RunFonts runFonts4 = new() { Hint = FontTypeHintValues.Default, Ascii = "Symbol", HighAnsi = "Symbol" };

            numberingSymbolRunProperties4.Append(runFonts4);

            level4.Append(startNumberingValue4);
            level4.Append(numberingFormat4);
            level4.Append(levelText4);
            level4.Append(levelJustification4);
            level4.Append(previousParagraphProperties4);
            level4.Append(numberingSymbolRunProperties4);

            Level level5 = new() { LevelIndex = 4, TemplateCode = "04090003", Tentative = true };
            StartNumberingValue startNumberingValue5 = new() { Val = 1 };
            NumberingFormat numberingFormat5 = new() { Val = NumberFormatValues.Bullet };
            LevelText levelText5 = new() { Val = "o" };
            LevelJustification levelJustification5 = new() { Val = LevelJustificationValues.Left };

            PreviousParagraphProperties previousParagraphProperties5 = new();
            Indentation indentation5 = new() { Start = "3600", Hanging = "360" };

            previousParagraphProperties5.Append(indentation5);

            NumberingSymbolRunProperties numberingSymbolRunProperties5 = new();
            RunFonts runFonts5 = new() { Hint = FontTypeHintValues.Default, Ascii = "Courier New", HighAnsi = "Courier New", ComplexScript = "Courier New" };

            numberingSymbolRunProperties5.Append(runFonts5);

            level5.Append(startNumberingValue5);
            level5.Append(numberingFormat5);
            level5.Append(levelText5);
            level5.Append(levelJustification5);
            level5.Append(previousParagraphProperties5);
            level5.Append(numberingSymbolRunProperties5);

            Level level6 = new() { LevelIndex = 5, TemplateCode = "04090005", Tentative = true };
            StartNumberingValue startNumberingValue6 = new() { Val = 1 };
            NumberingFormat numberingFormat6 = new() { Val = NumberFormatValues.Bullet };
            LevelText levelText6 = new() { Val = "§" };
            LevelJustification levelJustification6 = new() { Val = LevelJustificationValues.Left };

            PreviousParagraphProperties previousParagraphProperties6 = new();
            Indentation indentation6 = new() { Start = "4320", Hanging = "360" };

            previousParagraphProperties6.Append(indentation6);

            NumberingSymbolRunProperties numberingSymbolRunProperties6 = new();
            RunFonts runFonts6 = new() { Hint = FontTypeHintValues.Default, Ascii = "Wingdings", HighAnsi = "Wingdings" };

            numberingSymbolRunProperties6.Append(runFonts6);

            level6.Append(startNumberingValue6);
            level6.Append(numberingFormat6);
            level6.Append(levelText6);
            level6.Append(levelJustification6);
            level6.Append(previousParagraphProperties6);
            level6.Append(numberingSymbolRunProperties6);

            Level level7 = new() { LevelIndex = 6, TemplateCode = "04090001", Tentative = true };
            StartNumberingValue startNumberingValue7 = new() { Val = 1 };
            NumberingFormat numberingFormat7 = new() { Val = NumberFormatValues.Bullet };
            LevelText levelText7 = new() { Val = "·" };
            LevelJustification levelJustification7 = new() { Val = LevelJustificationValues.Left };

            PreviousParagraphProperties previousParagraphProperties7 = new();
            Indentation indentation7 = new() { Start = "5040", Hanging = "360" };

            previousParagraphProperties7.Append(indentation7);

            NumberingSymbolRunProperties numberingSymbolRunProperties7 = new();
            RunFonts runFonts7 = new() { Hint = FontTypeHintValues.Default, Ascii = "Symbol", HighAnsi = "Symbol" };

            numberingSymbolRunProperties7.Append(runFonts7);

            level7.Append(startNumberingValue7);
            level7.Append(numberingFormat7);
            level7.Append(levelText7);
            level7.Append(levelJustification7);
            level7.Append(previousParagraphProperties7);
            level7.Append(numberingSymbolRunProperties7);

            Level level8 = new() { LevelIndex = 7, TemplateCode = "04090003", Tentative = true };
            StartNumberingValue startNumberingValue8 = new() { Val = 1 };
            NumberingFormat numberingFormat8 = new() { Val = NumberFormatValues.Bullet };
            LevelText levelText8 = new() { Val = "o" };
            LevelJustification levelJustification8 = new() { Val = LevelJustificationValues.Left };

            PreviousParagraphProperties previousParagraphProperties8 = new();
            Indentation indentation8 = new() { Start = "5760", Hanging = "360" };

            previousParagraphProperties8.Append(indentation8);

            NumberingSymbolRunProperties numberingSymbolRunProperties8 = new();
            RunFonts runFonts8 = new() { Hint = FontTypeHintValues.Default, Ascii = "Courier New", HighAnsi = "Courier New", ComplexScript = "Courier New" };

            numberingSymbolRunProperties8.Append(runFonts8);

            level8.Append(startNumberingValue8);
            level8.Append(numberingFormat8);
            level8.Append(levelText8);
            level8.Append(levelJustification8);
            level8.Append(previousParagraphProperties8);
            level8.Append(numberingSymbolRunProperties8);

            Level level9 = new() { LevelIndex = 8, TemplateCode = "04090005", Tentative = true };
            StartNumberingValue startNumberingValue9 = new() { Val = 1 };
            NumberingFormat numberingFormat9 = new() { Val = NumberFormatValues.Bullet };
            LevelText levelText9 = new() { Val = "§" };
            LevelJustification levelJustification9 = new() { Val = LevelJustificationValues.Left };

            PreviousParagraphProperties previousParagraphProperties9 = new();
            Indentation indentation9 = new() { Start = "6480", Hanging = "360" };

            previousParagraphProperties9.Append(indentation9);

            NumberingSymbolRunProperties numberingSymbolRunProperties9 = new();
            RunFonts runFonts9 = new() { Hint = FontTypeHintValues.Default, Ascii = "Wingdings", HighAnsi = "Wingdings" };

            numberingSymbolRunProperties9.Append(runFonts9);

            level9.Append(startNumberingValue9);
            level9.Append(numberingFormat9);
            level9.Append(levelText9);
            level9.Append(levelJustification9);
            level9.Append(previousParagraphProperties9);
            level9.Append(numberingSymbolRunProperties9);

            abstractNum1.Append(nsid1);
            abstractNum1.Append(multiLevelType1);
            abstractNum1.Append(templateCode1);
            abstractNum1.Append(level1);
            abstractNum1.Append(level2);
            abstractNum1.Append(level3);
            abstractNum1.Append(level4);
            abstractNum1.Append(level5);
            abstractNum1.Append(level6);
            abstractNum1.Append(level7);
            abstractNum1.Append(level8);
            abstractNum1.Append(level9);

            AbstractNum abstractNum2 = new() { AbstractNumberId = 1 };
            Nsid nsid2 = new() { Val = "344D1E72" };
            MultiLevelType multiLevelType2 = new() { Val = MultiLevelValues.HybridMultilevel };
            TemplateCode templateCode2 = new() { Val = "549C79A8" };

            Level level10 = new() { LevelIndex = 0, TemplateCode = "3CC6CF18" };
            StartNumberingValue startNumberingValue10 = new() { Val = 1 };
            NumberingFormat numberingFormat10 = new() { Val = NumberFormatValues.Decimal };
            ParagraphStyleIdInLevel paragraphStyleIdInLevel1 = new() { Val = "ni" };
            LevelText levelText10 = new() { Val = "%1." };
            LevelJustification levelJustification10 = new() { Val = LevelJustificationValues.Left };

            PreviousParagraphProperties previousParagraphProperties10 = new();
            Indentation indentation10 = new() { Start = "720", Hanging = "360" };

            previousParagraphProperties10.Append(indentation10);

            level10.Append(startNumberingValue10);
            level10.Append(numberingFormat10);
            level10.Append(paragraphStyleIdInLevel1);
            level10.Append(levelText10);
            level10.Append(levelJustification10);
            level10.Append(previousParagraphProperties10);

            Level level11 = new() { LevelIndex = 1, TemplateCode = "04090019", Tentative = true };
            StartNumberingValue startNumberingValue11 = new() { Val = 1 };
            NumberingFormat numberingFormat11 = new() { Val = NumberFormatValues.LowerLetter };
            LevelText levelText11 = new() { Val = "%2." };
            LevelJustification levelJustification11 = new() { Val = LevelJustificationValues.Left };

            PreviousParagraphProperties previousParagraphProperties11 = new();
            Indentation indentation11 = new() { Start = "1440", Hanging = "360" };

            previousParagraphProperties11.Append(indentation11);

            level11.Append(startNumberingValue11);
            level11.Append(numberingFormat11);
            level11.Append(levelText11);
            level11.Append(levelJustification11);
            level11.Append(previousParagraphProperties11);

            Level level12 = new() { LevelIndex = 2, TemplateCode = "0409001B", Tentative = true };
            StartNumberingValue startNumberingValue12 = new() { Val = 1 };
            NumberingFormat numberingFormat12 = new() { Val = NumberFormatValues.LowerRoman };
            LevelText levelText12 = new() { Val = "%3." };
            LevelJustification levelJustification12 = new() { Val = LevelJustificationValues.Right };

            PreviousParagraphProperties previousParagraphProperties12 = new();
            Indentation indentation12 = new() { Start = "2160", Hanging = "180" };

            previousParagraphProperties12.Append(indentation12);

            level12.Append(startNumberingValue12);
            level12.Append(numberingFormat12);
            level12.Append(levelText12);
            level12.Append(levelJustification12);
            level12.Append(previousParagraphProperties12);

            Level level13 = new() { LevelIndex = 3, TemplateCode = "0409000F", Tentative = true };
            StartNumberingValue startNumberingValue13 = new() { Val = 1 };
            NumberingFormat numberingFormat13 = new() { Val = NumberFormatValues.Decimal };
            LevelText levelText13 = new() { Val = "%4." };
            LevelJustification levelJustification13 = new() { Val = LevelJustificationValues.Left };

            PreviousParagraphProperties previousParagraphProperties13 = new();
            Indentation indentation13 = new() { Start = "2880", Hanging = "360" };

            previousParagraphProperties13.Append(indentation13);

            level13.Append(startNumberingValue13);
            level13.Append(numberingFormat13);
            level13.Append(levelText13);
            level13.Append(levelJustification13);
            level13.Append(previousParagraphProperties13);

            Level level14 = new() { LevelIndex = 4, TemplateCode = "04090019", Tentative = true };
            StartNumberingValue startNumberingValue14 = new() { Val = 1 };
            NumberingFormat numberingFormat14 = new() { Val = NumberFormatValues.LowerLetter };
            LevelText levelText14 = new() { Val = "%5." };
            LevelJustification levelJustification14 = new() { Val = LevelJustificationValues.Left };

            PreviousParagraphProperties previousParagraphProperties14 = new();
            Indentation indentation14 = new() { Start = "3600", Hanging = "360" };

            previousParagraphProperties14.Append(indentation14);

            level14.Append(startNumberingValue14);
            level14.Append(numberingFormat14);
            level14.Append(levelText14);
            level14.Append(levelJustification14);
            level14.Append(previousParagraphProperties14);

            Level level15 = new() { LevelIndex = 5, TemplateCode = "0409001B", Tentative = true };
            StartNumberingValue startNumberingValue15 = new() { Val = 1 };
            NumberingFormat numberingFormat15 = new() { Val = NumberFormatValues.LowerRoman };
            LevelText levelText15 = new() { Val = "%6." };
            LevelJustification levelJustification15 = new() { Val = LevelJustificationValues.Right };

            PreviousParagraphProperties previousParagraphProperties15 = new();
            Indentation indentation15 = new() { Start = "4320", Hanging = "180" };

            previousParagraphProperties15.Append(indentation15);

            level15.Append(startNumberingValue15);
            level15.Append(numberingFormat15);
            level15.Append(levelText15);
            level15.Append(levelJustification15);
            level15.Append(previousParagraphProperties15);

            Level level16 = new() { LevelIndex = 6, TemplateCode = "0409000F", Tentative = true };
            StartNumberingValue startNumberingValue16 = new() { Val = 1 };
            NumberingFormat numberingFormat16 = new() { Val = NumberFormatValues.Decimal };
            LevelText levelText16 = new() { Val = "%7." };
            LevelJustification levelJustification16 = new() { Val = LevelJustificationValues.Left };

            PreviousParagraphProperties previousParagraphProperties16 = new();
            Indentation indentation16 = new() { Start = "5040", Hanging = "360" };

            previousParagraphProperties16.Append(indentation16);

            level16.Append(startNumberingValue16);
            level16.Append(numberingFormat16);
            level16.Append(levelText16);
            level16.Append(levelJustification16);
            level16.Append(previousParagraphProperties16);

            Level level17 = new() { LevelIndex = 7, TemplateCode = "04090019", Tentative = true };
            StartNumberingValue startNumberingValue17 = new() { Val = 1 };
            NumberingFormat numberingFormat17 = new() { Val = NumberFormatValues.LowerLetter };
            LevelText levelText17 = new() { Val = "%8." };
            LevelJustification levelJustification17 = new() { Val = LevelJustificationValues.Left };

            PreviousParagraphProperties previousParagraphProperties17 = new();
            Indentation indentation17 = new() { Start = "5760", Hanging = "360" };

            previousParagraphProperties17.Append(indentation17);

            level17.Append(startNumberingValue17);
            level17.Append(numberingFormat17);
            level17.Append(levelText17);
            level17.Append(levelJustification17);
            level17.Append(previousParagraphProperties17);

            Level level18 = new() { LevelIndex = 8, TemplateCode = "0409001B", Tentative = true };
            StartNumberingValue startNumberingValue18 = new() { Val = 1 };
            NumberingFormat numberingFormat18 = new() { Val = NumberFormatValues.LowerRoman };
            LevelText levelText18 = new() { Val = "%9." };
            LevelJustification levelJustification18 = new() { Val = LevelJustificationValues.Right };

            PreviousParagraphProperties previousParagraphProperties18 = new();
            Indentation indentation18 = new() { Start = "6480", Hanging = "180" };

            previousParagraphProperties18.Append(indentation18);

            level18.Append(startNumberingValue18);
            level18.Append(numberingFormat18);
            level18.Append(levelText18);
            level18.Append(levelJustification18);
            level18.Append(previousParagraphProperties18);

            abstractNum2.Append(nsid2);
            abstractNum2.Append(multiLevelType2);
            abstractNum2.Append(templateCode2);
            abstractNum2.Append(level10);
            abstractNum2.Append(level11);
            abstractNum2.Append(level12);
            abstractNum2.Append(level13);
            abstractNum2.Append(level14);
            abstractNum2.Append(level15);
            abstractNum2.Append(level16);
            abstractNum2.Append(level17);
            abstractNum2.Append(level18);

            AbstractNum abstractNum3 = new() { AbstractNumberId = 2 };
            Nsid nsid3 = new() { Val = "4879144C" };
            MultiLevelType multiLevelType3 = new() { Val = MultiLevelValues.HybridMultilevel };
            TemplateCode templateCode3 = new() { Val = "B8FC0F20" };

            Level level19 = new() { LevelIndex = 0, TemplateCode = "86445B50" };
            StartNumberingValue startNumberingValue19 = new() { Val = 1 };
            NumberingFormat numberingFormat19 = new() { Val = NumberFormatValues.Bullet };
            ParagraphStyleIdInLevel paragraphStyleIdInLevel2 = new() { Val = "li" };
            LevelText levelText19 = new() { Val = "·" };
            LevelJustification levelJustification19 = new() { Val = LevelJustificationValues.Left };

            PreviousParagraphProperties previousParagraphProperties19 = new();
            Indentation indentation19 = new() { Start = "720", Hanging = "360" };

            previousParagraphProperties19.Append(indentation19);

            NumberingSymbolRunProperties numberingSymbolRunProperties10 = new();
            RunFonts runFonts10 = new() { Hint = FontTypeHintValues.Default, Ascii = "Symbol", HighAnsi = "Symbol" };

            numberingSymbolRunProperties10.Append(runFonts10);

            level19.Append(startNumberingValue19);
            level19.Append(numberingFormat19);
            level19.Append(paragraphStyleIdInLevel2);
            level19.Append(levelText19);
            level19.Append(levelJustification19);
            level19.Append(previousParagraphProperties19);
            level19.Append(numberingSymbolRunProperties10);

            Level level20 = new() { LevelIndex = 1, TemplateCode = "04090003", Tentative = true };
            StartNumberingValue startNumberingValue20 = new() { Val = 1 };
            NumberingFormat numberingFormat20 = new() { Val = NumberFormatValues.Bullet };
            LevelText levelText20 = new() { Val = "o" };
            LevelJustification levelJustification20 = new() { Val = LevelJustificationValues.Left };

            PreviousParagraphProperties previousParagraphProperties20 = new();
            Indentation indentation20 = new() { Start = "1440", Hanging = "360" };

            previousParagraphProperties20.Append(indentation20);

            NumberingSymbolRunProperties numberingSymbolRunProperties11 = new();
            RunFonts runFonts11 = new() { Hint = FontTypeHintValues.Default, Ascii = "Courier New", HighAnsi = "Courier New", ComplexScript = "Courier New" };

            numberingSymbolRunProperties11.Append(runFonts11);

            level20.Append(startNumberingValue20);
            level20.Append(numberingFormat20);
            level20.Append(levelText20);
            level20.Append(levelJustification20);
            level20.Append(previousParagraphProperties20);
            level20.Append(numberingSymbolRunProperties11);

            Level level21 = new() { LevelIndex = 2, TemplateCode = "04090005", Tentative = true };
            StartNumberingValue startNumberingValue21 = new() { Val = 1 };
            NumberingFormat numberingFormat21 = new() { Val = NumberFormatValues.Bullet };
            LevelText levelText21 = new() { Val = "§" };
            LevelJustification levelJustification21 = new() { Val = LevelJustificationValues.Left };

            PreviousParagraphProperties previousParagraphProperties21 = new();
            Indentation indentation21 = new() { Start = "2160", Hanging = "360" };

            previousParagraphProperties21.Append(indentation21);

            NumberingSymbolRunProperties numberingSymbolRunProperties12 = new();
            RunFonts runFonts12 = new() { Hint = FontTypeHintValues.Default, Ascii = "Wingdings", HighAnsi = "Wingdings" };

            numberingSymbolRunProperties12.Append(runFonts12);

            level21.Append(startNumberingValue21);
            level21.Append(numberingFormat21);
            level21.Append(levelText21);
            level21.Append(levelJustification21);
            level21.Append(previousParagraphProperties21);
            level21.Append(numberingSymbolRunProperties12);

            Level level22 = new() { LevelIndex = 3, TemplateCode = "04090001", Tentative = true };
            StartNumberingValue startNumberingValue22 = new() { Val = 1 };
            NumberingFormat numberingFormat22 = new() { Val = NumberFormatValues.Bullet };
            LevelText levelText22 = new() { Val = "·" };
            LevelJustification levelJustification22 = new() { Val = LevelJustificationValues.Left };

            PreviousParagraphProperties previousParagraphProperties22 = new();
            Indentation indentation22 = new() { Start = "2880", Hanging = "360" };

            previousParagraphProperties22.Append(indentation22);

            NumberingSymbolRunProperties numberingSymbolRunProperties13 = new();
            RunFonts runFonts13 = new() { Hint = FontTypeHintValues.Default, Ascii = "Symbol", HighAnsi = "Symbol" };

            numberingSymbolRunProperties13.Append(runFonts13);

            level22.Append(startNumberingValue22);
            level22.Append(numberingFormat22);
            level22.Append(levelText22);
            level22.Append(levelJustification22);
            level22.Append(previousParagraphProperties22);
            level22.Append(numberingSymbolRunProperties13);

            Level level23 = new() { LevelIndex = 4, TemplateCode = "04090003", Tentative = true };
            StartNumberingValue startNumberingValue23 = new() { Val = 1 };
            NumberingFormat numberingFormat23 = new() { Val = NumberFormatValues.Bullet };
            LevelText levelText23 = new() { Val = "o" };
            LevelJustification levelJustification23 = new() { Val = LevelJustificationValues.Left };

            PreviousParagraphProperties previousParagraphProperties23 = new();
            Indentation indentation23 = new() { Start = "3600", Hanging = "360" };

            previousParagraphProperties23.Append(indentation23);

            NumberingSymbolRunProperties numberingSymbolRunProperties14 = new();
            RunFonts runFonts14 = new() { Hint = FontTypeHintValues.Default, Ascii = "Courier New", HighAnsi = "Courier New", ComplexScript = "Courier New" };

            numberingSymbolRunProperties14.Append(runFonts14);

            level23.Append(startNumberingValue23);
            level23.Append(numberingFormat23);
            level23.Append(levelText23);
            level23.Append(levelJustification23);
            level23.Append(previousParagraphProperties23);
            level23.Append(numberingSymbolRunProperties14);

            Level level24 = new() { LevelIndex = 5, TemplateCode = "04090005", Tentative = true };
            StartNumberingValue startNumberingValue24 = new() { Val = 1 };
            NumberingFormat numberingFormat24 = new() { Val = NumberFormatValues.Bullet };
            LevelText levelText24 = new() { Val = "§" };
            LevelJustification levelJustification24 = new() { Val = LevelJustificationValues.Left };

            PreviousParagraphProperties previousParagraphProperties24 = new();
            Indentation indentation24 = new() { Start = "4320", Hanging = "360" };

            previousParagraphProperties24.Append(indentation24);

            NumberingSymbolRunProperties numberingSymbolRunProperties15 = new();
            RunFonts runFonts15 = new() { Hint = FontTypeHintValues.Default, Ascii = "Wingdings", HighAnsi = "Wingdings" };

            numberingSymbolRunProperties15.Append(runFonts15);

            level24.Append(startNumberingValue24);
            level24.Append(numberingFormat24);
            level24.Append(levelText24);
            level24.Append(levelJustification24);
            level24.Append(previousParagraphProperties24);
            level24.Append(numberingSymbolRunProperties15);

            Level level25 = new() { LevelIndex = 6, TemplateCode = "04090001", Tentative = true };
            StartNumberingValue startNumberingValue25 = new() { Val = 1 };
            NumberingFormat numberingFormat25 = new() { Val = NumberFormatValues.Bullet };
            LevelText levelText25 = new() { Val = "·" };
            LevelJustification levelJustification25 = new() { Val = LevelJustificationValues.Left };

            PreviousParagraphProperties previousParagraphProperties25 = new();
            Indentation indentation25 = new() { Start = "5040", Hanging = "360" };

            previousParagraphProperties25.Append(indentation25);

            NumberingSymbolRunProperties numberingSymbolRunProperties16 = new();
            RunFonts runFonts16 = new() { Hint = FontTypeHintValues.Default, Ascii = "Symbol", HighAnsi = "Symbol" };

            numberingSymbolRunProperties16.Append(runFonts16);

            level25.Append(startNumberingValue25);
            level25.Append(numberingFormat25);
            level25.Append(levelText25);
            level25.Append(levelJustification25);
            level25.Append(previousParagraphProperties25);
            level25.Append(numberingSymbolRunProperties16);

            Level level26 = new() { LevelIndex = 7, TemplateCode = "04090003", Tentative = true };
            StartNumberingValue startNumberingValue26 = new() { Val = 1 };
            NumberingFormat numberingFormat26 = new() { Val = NumberFormatValues.Bullet };
            LevelText levelText26 = new() { Val = "o" };
            LevelJustification levelJustification26 = new() { Val = LevelJustificationValues.Left };

            PreviousParagraphProperties previousParagraphProperties26 = new();
            Indentation indentation26 = new() { Start = "5760", Hanging = "360" };

            previousParagraphProperties26.Append(indentation26);

            NumberingSymbolRunProperties numberingSymbolRunProperties17 = new();
            RunFonts runFonts17 = new() { Hint = FontTypeHintValues.Default, Ascii = "Courier New", HighAnsi = "Courier New", ComplexScript = "Courier New" };

            numberingSymbolRunProperties17.Append(runFonts17);

            level26.Append(startNumberingValue26);
            level26.Append(numberingFormat26);
            level26.Append(levelText26);
            level26.Append(levelJustification26);
            level26.Append(previousParagraphProperties26);
            level26.Append(numberingSymbolRunProperties17);

            Level level27 = new() { LevelIndex = 8, TemplateCode = "04090005", Tentative = true };
            StartNumberingValue startNumberingValue27 = new() { Val = 1 };
            NumberingFormat numberingFormat27 = new() { Val = NumberFormatValues.Bullet };
            LevelText levelText27 = new() { Val = "§" };
            LevelJustification levelJustification27 = new() { Val = LevelJustificationValues.Left };

            PreviousParagraphProperties previousParagraphProperties27 = new();
            Indentation indentation27 = new() { Start = "6480", Hanging = "360" };

            previousParagraphProperties27.Append(indentation27);

            NumberingSymbolRunProperties numberingSymbolRunProperties18 = new();
            RunFonts runFonts18 = new() { Hint = FontTypeHintValues.Default, Ascii = "Wingdings", HighAnsi = "Wingdings" };

            numberingSymbolRunProperties18.Append(runFonts18);

            level27.Append(startNumberingValue27);
            level27.Append(numberingFormat27);
            level27.Append(levelText27);
            level27.Append(levelJustification27);
            level27.Append(previousParagraphProperties27);
            level27.Append(numberingSymbolRunProperties18);

            abstractNum3.Append(nsid3);
            abstractNum3.Append(multiLevelType3);
            abstractNum3.Append(templateCode3);
            abstractNum3.Append(level19);
            abstractNum3.Append(level20);
            abstractNum3.Append(level21);
            abstractNum3.Append(level22);
            abstractNum3.Append(level23);
            abstractNum3.Append(level24);
            abstractNum3.Append(level25);
            abstractNum3.Append(level26);
            abstractNum3.Append(level27);

            NumberingInstance numberingInstance1 = new() { NumberID = 1 };
            AbstractNumId abstractNumId1 = new() { Val = 0 };

            numberingInstance1.Append(abstractNumId1);

            NumberingInstance numberingInstance2 = new() { NumberID = 2 };
            AbstractNumId abstractNumId2 = new() { Val = 2 };

            numberingInstance2.Append(abstractNumId2);

            NumberingInstance numberingInstance3 = new() { NumberID = 3 };
            AbstractNumId abstractNumId3 = new() { Val = 1 };

            numberingInstance3.Append(abstractNumId3);

            numbering1.Append(abstractNum1);
            numbering1.Append(abstractNum2);
            numbering1.Append(abstractNum3);
            numbering1.Append(numberingInstance1);
            numbering1.Append(numberingInstance2);
            numbering1.Append(numberingInstance3);

            part.Numbering = numbering1;
            }


        }
    }
