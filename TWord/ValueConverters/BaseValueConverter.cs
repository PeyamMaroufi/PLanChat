using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace TWord
{
    /// <summary>
    /// a bse value converter that allows direct Xaml usage
    /// </summary>
    /// <typeparam name="T"> the type of this converter</typeparam>
    public abstract class BaseValueConverter<T> : MarkupExtension, IValueConverter
        where T : class, new()
    {
        #region private members
        private static T mConverter = null;
        #endregion

        #region Markup extension
        /// <summary>
        /// provide a static member of the converter
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public override object ProvideValue(IServiceProvider serviceProvider)
        {

            // if (mconverter == null)
            // mConverter = new T();
            // return mConverter;
            // It is same as below
            return mConverter ?? (mConverter = new T());
        }

        #endregion

        #region Value converter methods
        /// <summary>
        /// the method to convert one type to an other
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public abstract object Convert(object value, Type targetType, object parameter, CultureInfo culture);

        /// <summary>
        /// the method to convert a value back to  it's source type
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public abstract object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture);
    }
    #endregion
}
