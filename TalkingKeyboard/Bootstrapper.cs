using Microsoft.Practices.Unity;
using Prism.Unity;
using TalkingKeyboard.Views;
using System.Windows;

namespace TalkingKeyboard
{
    class Bootstrapper : UnityBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void InitializeShell()
        {
            Application.Current.MainWindow.Show();
        }
    }
}
