using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace QuanLyNhanSu
{
    class Chuyenkhongdau
    {
        public static string Convert(string str)
        {
            if (string.IsNullOrWhiteSpace(str))
                return string.Empty;

            string normalizedString = str.Normalize(NormalizationForm.FormD);

            string result = Regex.Replace(normalizedString, @"\p{IsCombiningDiacriticalMarks}+", "")
                                 .Replace(" ", "")
                                 .ToLower();

            return result;
        }
    }
}
