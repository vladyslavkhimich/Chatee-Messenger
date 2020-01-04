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
    /// Логика взаимодействия для MainUserInterfacePage.xaml
    /// </summary>
    public partial class ChatPage : BasePage<ChatMessageListViewModel>
    {
        public ChatPage() : base()
        {
            InitializeComponent();
        }
        
        public ChatPage(ChatMessageListViewModel specificViewModel) : base(specificViewModel)
        {
            InitializeComponent();
        }

        private void messageText_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            var textBox = sender as TextBox;
            if (e.Key == Key.Enter)
            {
                if (Keyboard.Modifiers.HasFlag(ModifierKeys.Control))
                {
                    var index = textBox.CaretIndex;
                    textBox.Text = textBox.Text.Insert(index, Environment.NewLine);
                    textBox.CaretIndex = index + Environment.NewLine.Length;
                }
                else
                    viewModel.SendMessageAsync();
            }
        }
    }
}
