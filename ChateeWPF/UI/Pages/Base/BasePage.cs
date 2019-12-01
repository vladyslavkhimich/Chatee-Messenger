using ChateeCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ChateeWPF
{
    public class BasePage<ViewModel> : Page where ViewModel : BaseViewModel, new()
    {
        ViewModel _viewModel;
        public ViewModel viewModel
        {
            get { return _viewModel; }
            set
            {
                if (_viewModel == value)
                    return;
                else _viewModel = value;
                this.DataContext = viewModel;
            }
        }
        public BasePage()
        {
            viewModel = new ViewModel();
        }
    }
}
