using ChateeCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ChateeWPF
{
    /// <summary>
    /// Interaction logic for PageHost.xaml
    /// </summary>
    public partial class PageHost : UserControl
    {
        #region Dependency Properties


        public ApplicationPages CurrentPage
        {
            get => (ApplicationPages)GetValue(CurrentPageProperty);
            set => SetValue(CurrentPageProperty, value);
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CurrentPageProperty =
            DependencyProperty.Register(nameof(CurrentPage), typeof(ApplicationPages), typeof(PageHost), new UIPropertyMetadata(default(ApplicationPages), null, CurrentPagePropertyChanged));

        

        public BaseViewModel CurrentPageViewModel
        {
            get => (BaseViewModel)GetValue(CurrentPageViewModelProperty);
            set => SetValue(CurrentPageViewModelProperty, value);
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CurrentPageViewModelProperty =
            DependencyProperty.Register(nameof(CurrentPageViewModel), typeof(BaseViewModel), typeof(PageHost), new UIPropertyMetadata());


        #endregion
        #region Constructor
        public PageHost()
        {
            InitializeComponent();
        }
        #endregion
        #region PropertyChanged Events
        private static object CurrentPagePropertyChanged(DependencyObject d, object value)
        {
            var currentPage = d.GetValue(CurrentPageProperty);
            var currentPageViewModel = d.GetValue(CurrentPageViewModelProperty);
            var newPageFrame = (d as PageHost).NewPage;
            var oldPageFrame = (d as PageHost).OldPage;
            var oldPageContent = newPageFrame.Content;
            newPageFrame.Content = null;
            oldPageFrame.Content = oldPageContent;
            if (oldPageContent is BasePage oldPage)
            {
                oldPage.Content = null;
                oldPageFrame.Content = null;
            }
            newPageFrame.Content = new ToPageConverter().Convert(currentPage, null, currentPageViewModel);
            return value;
        }
        #endregion
    }
}
