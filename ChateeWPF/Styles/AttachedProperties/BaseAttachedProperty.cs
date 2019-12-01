using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ChateeWPF
{
    public abstract class BaseAttachedProperty<Parent, Property> where Parent : BaseAttachedProperty<Parent, Property>, new()
    {
        #region Public events
        public event Action<DependencyObject, DependencyPropertyChangedEventArgs> ValueChanged = (sender, e) => { };
        #endregion
        #region Public Properties
        // A singleton instance of parent class
        public static Parent Instance { get; private set; } = new Parent();
        #endregion
        #region Attached Property Definitions
        //The attached property for this class
        public static readonly DependencyProperty ValueProperty = DependencyProperty.RegisterAttached("Value", typeof(Property), typeof(BaseAttachedProperty<Parent, Property>), new PropertyMetadata(new PropertyChangedCallback(OnValuePropertyChanged)));
        //Teh callback when the ValueProperty is changed
        private static void OnValuePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Instance.OnValueChanged(d, e);
            //Call event listeners
            Instance.ValueChanged(d, e);
        }
        //Gets the attached property
        public static Property GetValue(DependencyObject d) => (Property)d.GetValue(ValueProperty);
        //Sets the attached property
        public static void SetValue(DependencyObject d, Property value) => d.SetValue(ValueProperty, value);
        #endregion
        #region Event Methods
        //The method that is called when any attached property of this type is changed
        public virtual void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
        }
        #endregion
    }
}
