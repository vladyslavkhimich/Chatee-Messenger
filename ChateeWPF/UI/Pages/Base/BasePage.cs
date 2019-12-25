using ChateeCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ChateeWPF
{
    public class BasePage : Page
    {
        public BasePage()
        {

        }
        object _viewModel;
        public object viewModelObject
        {
            get { return _viewModel; }
            set
            {
                if (_viewModel == value)
                    return;
                else _viewModel = value;
                this.DataContext = viewModelObject;
            }
        }
    }
    public class BasePage<ViewModel> : BasePage where ViewModel : BaseViewModel, new()
    {
        public ViewModel viewModel
        {
            get => (ViewModel)viewModelObject;
            set => viewModelObject = value;
        }
        
        public BasePage(ViewModel specificViewModel = null) : base()
        {
            if (specificViewModel != null)
                viewModelObject = specificViewModel;
            else
                viewModelObject = IoCContainer.Get<ViewModel>();
        }
        public BasePage() : base()
        {
            viewModelObject = IoCContainer.Get<ViewModel>();
        }
    }
}
