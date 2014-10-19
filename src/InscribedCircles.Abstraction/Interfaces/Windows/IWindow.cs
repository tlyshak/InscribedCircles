using System;
using Telerik.Windows.Controls;

namespace InscribedCircles.Abstraction.Interfaces.Windows
{
    public interface IWindow
    {
        bool? DialogResult { get; }
        event EventHandler<WindowClosedEventArgs> Closed;
        void ShowDialog();
    }
}
