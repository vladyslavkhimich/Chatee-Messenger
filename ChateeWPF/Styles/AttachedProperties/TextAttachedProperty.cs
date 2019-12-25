using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace ChateeWPF
{
    public class IsFocusedProperty : BaseAttachedProperty<IsFocusedProperty, bool>
    {
        public override void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if (!(sender is Control control))
                 return;
            control.Loaded += (s, se) => control.Focus();
        }
    }
    public class FocusProperty : BaseAttachedProperty<FocusProperty, bool>
    {
        public override void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if (!(sender is TextBoxBase control))
                return;
            if((bool)e.NewValue)
            {
                control.Focus();
                control.SelectAll();
            }
                
        }
    }
    public class CtrlEnterNewLineProperty : BaseAttachedProperty<CtrlEnterNewLineProperty, bool>
    {
        public override void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if (!(sender is Control control))
                return;
            control.Loaded += (s, se) => control.Focus();
        }
    }
}
