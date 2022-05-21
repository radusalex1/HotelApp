using System.Windows;
using System.Windows.Controls;

namespace HotelApp.Helps
{
    public class TemplateSelector:DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            FrameworkElement element = container as FrameworkElement;
            return element.FindResource("ListTemplate") as DataTemplate;
        }
    }
}
