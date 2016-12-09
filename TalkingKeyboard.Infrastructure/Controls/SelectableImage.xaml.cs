using System.Windows.Controls;

namespace TalkingKeyboard.Infrastructure.Controls
{
    using System.Windows;
    using System.Windows.Media.Animation;

    /// <summary>
    /// Interaction logic for SelectableImage
    /// </summary>
    public partial class SelectableImage
    {
        public SelectableImage()
        {
            InitializeComponent();
            this.Animation = (Storyboard)this.Resources["ArcSelectionAnimation"];
        }

        public static readonly DependencyProperty SourceProperty = DependencyProperty.Register("Source", typeof(string), typeof(SelectableImage), new PropertyMetadata(default(string)));

        public string Source
        {
            get
            {
                return (string)GetValue(SourceProperty);
            }
            set
            {
                SetValue(SourceProperty, value);
            }
        }
    }
}
