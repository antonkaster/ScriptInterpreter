using ICSharpCode.AvalonEdit.Highlighting;
using ICSharpCode.AvalonEdit.Highlighting.Xshd;
using InterpreterLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace LangGUI.Helpers
{
    public class HighlightGenerator
    {
        private readonly ScriptBase langBase;
        public HighlightGenerator(ScriptBase langBase)
        {
            this.langBase = langBase ?? throw new ArgumentNullException("LangBase can't be null!");
        }

        public IHighlightingDefinition Make()
        {
            //if (File.Exists(codeHighlightingSchemeFile))
            //{
            //    using (Stream xshd_stream = File.OpenRead(codeHighlightingSchemeFile))
            //    using (XmlTextReader xshd_reader = new XmlTextReader(xshd_stream))
            //    {
            //        CodeHighlightingScheme = HighlightingLoader.Load(xshd_reader, HighlightingManager.Instance);
            //    }
            //}

            XNamespace xn = "http://icsharpcode.net/sharpdevelop/syntaxdefinition/2008";

            XElement complexFunctions = new XElement(xn + "Keywords", new XAttribute("color", "ComplexFunctions"));
            foreach (var func in langBase.GetComplexFunctionsList())
                complexFunctions.Add(new XElement(xn + "Word", func));

            XElement functions = new XElement(xn + "Keywords", new XAttribute("color", "Functions"));
            foreach (var func in langBase.GetFunctionsList())
                functions.Add(new XElement(xn + "Word", func));

            XDocument doc = new XDocument(
                new XElement(xn + "SyntaxDefinition",
                    new XAttribute("name", "Lang"),
                    new XElement(xn + "Color",
                            new XAttribute("name", "Comment"),
                            new XAttribute("foreground", "Green")
                        ),
                    new XElement(xn + "Color",
                            new XAttribute("name", "String"),
                            new XAttribute("foreground", "Brown")
                        ),
                    new XElement(xn + "Color",
                            new XAttribute("name", "Digits"),
                            new XAttribute("foreground", "DarkBlue")
                        ),
                    new XElement(xn + "Color",
                            new XAttribute("name", "Functions"),
                            new XAttribute("fontWeight", "bold"),
                            new XAttribute("foreground", "Purple")
                        ),
                    new XElement(xn + "Color",
                            new XAttribute("name", "ComplexFunctions"),
                            new XAttribute("fontWeight", "bold"),
                            new XAttribute("foreground", "Blue")
                        ),
                    new XElement(xn + "RuleSet",
                        new XAttribute("ignoreCase", true),
                        new XElement(xn + "Span",
                            new XAttribute("color", "Comment"),
                            new XAttribute("begin", "//")
                        ),
                        new XElement(xn + "Span",
                            new XAttribute("color", "Comment"),
                            new XAttribute("multiline", true),
                            new XAttribute("begin", @"/\*"),
                            new XAttribute("end", @"\*/")
                        ),
                        new XElement(xn + "Span",
                            new XAttribute("color", "String"),
                            new XElement(xn + "Begin", "\""),
                            new XElement(xn + "End", "\""),
                            new XElement(xn + "RuleSet",
                                new XElement(xn + "Span",
                                    new XAttribute("begin", @"\\"),
                                    new XAttribute("end", @".")
                                )
                            )
                        ),
                        new XElement(xn + "Rule",
                            new XAttribute("color", "Digits"),
                            @"\b0[xX][0-9a-fA-F]+  # hex number" + "\r\n" +
                            @"|\b" + "\r\n" +
                            @"(\d + (\.[0 - 9] +) ?   #number with optional floating point" + "\r\n" +
                            @"|\.[0 - 9] +         #or just starting with floating point" + "\r\n" +
                            @")" + "\r\n" +
                            @"([eE][+-]?[0 - 9] +) ? # optional exponent" + "\r\n"
                        ),
                        complexFunctions,
                        functions
                    )
                )
            );

            using (Stream reader = new MemoryStream(Encoding.UTF8.GetBytes(doc.ToString())))
            using (XmlTextReader xshd_reader = new XmlTextReader(reader))
            {
                return HighlightingLoader.Load(xshd_reader, HighlightingManager.Instance);
            }

        }
    }
}
