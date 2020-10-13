using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Rendering;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;

namespace LangGUI.Controls
{
    public class OffsetColorizer : DocumentColorizingTransformer
    {
        public ColorizeProps ColorizeProps { get; set; } = new ColorizeProps();

        public OffsetColorizer(ColorizeProps colorizeOffset) : base()
        {
            ColorizeProps = colorizeOffset;
        }

        protected override void ColorizeLine(DocumentLine line)
        {
            if (ColorizeProps.Start < 0 || ColorizeProps.End <= 0)
                return;

            if (line.Length == 0)
                return;

            if ((line.Offset + line.Length ) < ColorizeProps.Start || line.Offset > ColorizeProps.End)
                return;

            int start = line.Offset > ColorizeProps.Start ? line.Offset : ColorizeProps.Start;
            int end = ColorizeProps.End > line.EndOffset ? line.EndOffset : ColorizeProps.End;

            ChangeLinePart(start, end, element =>
                {
                    element.TextRunProperties.SetBackgroundBrush(new SolidColorBrush(ColorizeProps.BackColor));
                    element.TextRunProperties.SetForegroundBrush(new SolidColorBrush(ColorizeProps.ForeColor));
                });
        }
    }
}
