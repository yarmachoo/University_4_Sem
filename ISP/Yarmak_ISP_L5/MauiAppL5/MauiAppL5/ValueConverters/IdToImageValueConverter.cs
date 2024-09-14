using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiAppL5.ValueConverters
{
    public class IdToImageValueConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            int id = (int)value;
            string path = Path.Combine("D:\\", "Images", "Books");
            string fileName = $"{id}.png";
            string imagePath = Path.Combine(path, fileName);
            if(Path.Exists(imagePath))
            {
                return ImageSource.FromFile(imagePath);
            }
            return ImageSource.FromFile("dot_net.png");
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
