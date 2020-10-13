using ICSharpCode.AvalonEdit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows;

namespace LangGUI.Controls
{
    public class BindableAvalonEditor : TextEditor, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        public new string Text
        {
            get
            {
                return (string)GetValue(TextProperty);
            }
            set
            {
                SetValue(TextProperty, value);
                RaisePropertyChanged("Text");
            }
        }

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register(
                "Text",
                typeof(string),
                typeof(BindableAvalonEditor),
                new FrameworkPropertyMetadata
                {
                    DefaultValue = default(string),
                    BindsTwoWayByDefault = true,
                    PropertyChangedCallback = OnDependencyPropertyChanged
                }
            );

        protected static void OnDependencyPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            var target = (BindableAvalonEditor)obj;

            if (target.Document != null)
            {
                var caretOffset = target.CaretOffset;
                var newValue = args.NewValue;

                if (newValue == null)
                {
                    newValue = "";
                }

                target.Document.Text = (string)newValue;
                target.CaretOffset = Math.Min(caretOffset, newValue.ToString().Length);
            }
        }

        protected override void OnTextChanged(EventArgs e)
        {
            if (this.Document != null)
            {
                Text = this.Document.Text;
            }

            base.OnTextChanged(e);
        }

        public ColorizeProps ColorizeProps
        {
            get
            {
                return (ColorizeProps)GetValue(ColorizeProperty);
            }
            set
            {
                SetValue(ColorizeProperty, value);
                RaisePropertyChanged("ColorizeOffset");
            }
        }

        public static readonly DependencyProperty ColorizeProperty =
            DependencyProperty.Register(
                "ColorizeProps",
                typeof(ColorizeProps),
                typeof(BindableAvalonEditor),
                new FrameworkPropertyMetadata
                {
                    DefaultValue = default(ColorizeProps),
                    BindsTwoWayByDefault = true,
                    PropertyChangedCallback = ColorizeDependencyPropertyChanged
                }
            );

        protected static void ColorizeDependencyPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            var target = (BindableAvalonEditor)obj;
            target.ColorizeProps = (ColorizeProps)args.NewValue;
            target.SetColorize(target.ColorizeProps);
        }


        public OffsetColorizer OffsetColorizer;

        public BindableAvalonEditor() : base()
        {
            OffsetColorizer = new OffsetColorizer(new ColorizeProps());
            TextArea.TextView.LineTransformers.Add(OffsetColorizer);
        }

        public void SetColorize(ColorizeProps colorizeOffset)
        {
            TextArea.TextView.LineTransformers.Remove(OffsetColorizer);

            OffsetColorizer = new OffsetColorizer(colorizeOffset);
            TextArea.TextView.LineTransformers.Add(OffsetColorizer);

            if (colorizeOffset.End > 0)
            {
                CaretOffset = colorizeOffset.Start;
                ScrollToVerticalOffset(CaretOffset);
            }

            UpdateLayout();
            TextArea.UpdateLayout();
        }

    }
}
