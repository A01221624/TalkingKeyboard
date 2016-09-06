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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TalkingKeyboard.Infrastructure.Controls
{
    /// <summary>
    /// Interaction logic for SelectableButton.xaml
    /// </summary>
    public partial class SelectableButton
    {
        public SelectableButton()
        {
            InitializeComponent();
            Animation = (Storyboard) Resources["ArcSelectionAnimation"];
        }

        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
            "Text", typeof(string), typeof(SelectableButton), new PropertyMetadata(default(string)));

        public string Text
        {
            get { return (string) GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public new static readonly DependencyProperty FontSizeProperty = DependencyProperty.Register(
            "FontSize", typeof(int), typeof(SelectableButton), new PropertyMetadata(default(int)));

        public new int FontSize
        {
            get { return (int) GetValue(FontSizeProperty); }
            set { SetValue(FontSizeProperty, value); }
        }
    }
}
