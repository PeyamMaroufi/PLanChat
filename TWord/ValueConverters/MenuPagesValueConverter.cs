using System;
using System.Diagnostics;
using System.Globalization;


namespace TWord
{
    /// <summary>
    /// converts the <see cref="MenuPage"/> to an actual view/page
    /// </summary>
    public class MenuPagesValueConverter:BaseValueConverter<MenuPagesValueConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // find the appropriate page
          switch((MenuPage) value)
            {
                case MenuPage.VerticalMenuLeft:
                    return null;

                default:
                    Debugger.Break();
                    return null;

            }
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
