using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using DevExpress.Xpf.Editors;
using DevExpress.Mvvm.UI.Interactivity;
 

namespace KpInfohelp
{
    public class DisplayTextHelper:  Behavior<TextEdit>
    {
        TextEdit Editor
        {
            get
            {
                return AssociatedObject as TextEdit;
            }
        }
        protected override void OnAttached()
        {
            base.OnAttached();
            DependencyPropertyDescriptor descriptor = DependencyPropertyDescriptor.FromProperty(TextEdit.DisplayTextProperty, typeof(TextEdit));

                if (descriptor != null)
                    descriptor.AddValueChanged(Editor, DisplayTextChanged);
        }
        void DisplayTextChanged(object sender, EventArgs args)
        {
            TextToBind = Editor.DisplayText;
        }
        public static readonly DependencyProperty TextToBindProperty = DependencyProperty.Register("TextToBind", typeof(string), typeof(DisplayTextHelper), null);
        public string TextToBind
        {
            get { return (string)GetValue(TextToBindProperty); }
            set { SetValue(TextToBindProperty, value); }
        }
    }
    public class BindingHelper : Behavior<BaseEdit>
    {
        public static readonly DependencyProperty SourceValueProperty =
           DependencyProperty.Register("SourceValue", typeof(object), typeof(BindingHelper), new PropertyMetadata(null, OnSourceValuePropertyChanged));

        static void OnSourceValuePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((BindingHelper)d).OnSourceValueChanged(e);
        }

        public object SourceValue
        {
            get { return (object)GetValue(SourceValueProperty); }
            set { SetValue(SourceValueProperty, value); }
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.EditValueChanged += OnEditorValueChanged;
            UpdateEditValue();
        }
        protected override void OnDetaching()
        {
            AssociatedObject.EditValueChanged -= OnEditorValueChanged;
            base.OnDetaching();
        }
        protected virtual void OnEditorValueChanged(object sender, EditValueChangedEventArgs e)
        {
            UpdateSourceValue();
        }
        protected virtual void OnSourceValueChanged(DependencyPropertyChangedEventArgs e)
        {
            UpdateEditValue();
        }
        protected virtual void UpdateEditValue()
        {
            if (AssociatedObject != null)
                AssociatedObject.EditValue = SourceValue;
        }
        protected virtual void UpdateSourceValue()
        {
            SourceValue = AssociatedObject.EditValue;
        }
    }

}
