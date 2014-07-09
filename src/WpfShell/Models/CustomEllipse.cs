using System.Windows.Shapes;

namespace WpfShell.Models
{
    public class CustomEllipse
    {
        public double X { get; set; }
        public double Y { get; set; }
        public Ellipse Ellipse { get; set; }

        public CustomEllipse(Ellipse ellipse)
        {
            Ellipse = ellipse;
        }
    }
}