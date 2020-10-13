using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;

namespace LangGUI.Controls
{
    public class ColorizeProps
    {
        public int Start { get; set; }
        public int End { get; set; }

        public Color BackColor { get; set; } = Colors.Red;
        public Color ForeColor { get; set; } = Colors.White;

        public ColorizeProps()
        {

        }

        public ColorizeProps(int start, int end)
        {
            Start = start;
            End = end;
        }

        public ColorizeProps(int start, int end, Color backColor, Color foreColor)
        {
            Start = start;
            End = end;
            BackColor = backColor;
            ForeColor = foreColor;
        }

        public override string ToString()
        {
            return $"{Start} -> {End}";
        }
    }
}
