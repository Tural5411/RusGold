using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RusGold.Mvc.Extensions
{
    public static class UrlExtension
    {
        public static string FriendlyUrlHelperV1(this IUrlHelper helper, string url)
        {
            if (string.IsNullOrEmpty(url)) return "";
            url = url.ToLower();
            url = url.Trim();
            if (url.Length > 100)
            {
                url = url.Substring(0, 100);
            }
            url = url.Replace("Ə", "e");
            url = url.Replace("ə", "e");
            url = url.Replace("İ", "I");
            url = url.Replace("ı", "i");
            url = url.Replace("ğ", "g");
            url = url.Replace("Ğ", "G");
            url = url.Replace("ç", "c");
            url = url.Replace("Ç", "C");
            url = url.Replace("ö", "o");
            url = url.Replace("Ö", "O");
            url = url.Replace("ş", "s");
            url = url.Replace("Ş", "S");
            url = url.Replace("ü", "u");
            url = url.Replace("Ü", "U");
            url = url.Replace("'", "");
            url = url.Replace("\"", "");
            char[] replacerList = @"$%#@!*?;:~`+=()[]{}|\'<>,/^&"".".ToCharArray();
            for (int i = 0; i < replacerList.Length; i++)
            {
                string strChr = replacerList[i].ToString();
                if (url.Contains(strChr))
                {
                    url = url.Replace(strChr, string.Empty);
                }
            }
            Regex regex = new Regex("[^a-zA-Z0-9_-]");
            url = regex.Replace(url, "-");
            while (url.IndexOf("--", StringComparison.Ordinal) > -1)
                url = url.Replace("--", "-");
            return url;
        }
		public static string FriendlyUrlHelper(this IUrlHelper helper, string url)
		{
			if (string.IsNullOrEmpty(url)) return "";
			url = url.ToLower();
			url = url.Trim();
			if (url.Length > 100)
			{
				url = url.Substring(0, 100);
			}

			url = url.Replace("А", "a");
			url = url.Replace("Б", "b");
			url = url.Replace("В", "v");
			url = url.Replace("Г", "g");
			url = url.Replace("Д", "d");
			url = url.Replace("Е", "e");
			url = url.Replace("Ё", "yo");
			url = url.Replace("Ж", "zh");
			url = url.Replace("З", "z");
			url = url.Replace("И", "i");
			url = url.Replace("Й", "y");
			url = url.Replace("К", "k");
			url = url.Replace("Л", "l");
			url = url.Replace("М", "m");
			url = url.Replace("Н", "n");
			url = url.Replace("О", "o");
			url = url.Replace("П", "p");
			url = url.Replace("Р", "r");
			url = url.Replace("С", "s");
			url = url.Replace("Т", "t");
			url = url.Replace("У", "u");
			url = url.Replace("Ф", "f");
			url = url.Replace("Х", "kh");
			url = url.Replace("Ц", "ts");
			url = url.Replace("Ч", "ch");
			url = url.Replace("Ш", "sh");
			url = url.Replace("Щ", "sch");
			url = url.Replace("Ы", "y");
			url = url.Replace("Э", "e");
			url = url.Replace("Ю", "yu");
			url = url.Replace("Я", "ya");

			url = url.Replace("а", "a");
			url = url.Replace("б", "b");
			url = url.Replace("в", "v");
			url = url.Replace("г", "g");
			url = url.Replace("д", "d");
			url = url.Replace("е", "e");
			url = url.Replace("ё", "yo");
			url = url.Replace("ж", "zh");
			url = url.Replace("з", "z");
			url = url.Replace("и", "i");
			url = url.Replace("й", "y");
			url = url.Replace("к", "k");
			url = url.Replace("л", "l");
			url = url.Replace("м", "m");
			url = url.Replace("н", "n");
			url = url.Replace("о", "o");
			url = url.Replace("п", "p");
			url = url.Replace("р", "r");
			url = url.Replace("с", "s");
			url = url.Replace("т", "t");
			url = url.Replace("у", "u");
			url = url.Replace("ф", "f");
			url = url.Replace("х", "kh");
			url = url.Replace("ц", "ts");
			url = url.Replace("ч", "ch");
			url = url.Replace("ш", "sh");
			url = url.Replace("щ", "sch");
			url = url.Replace("ы", "y");
			url = url.Replace("э", "e");
			url = url.Replace("ю", "yu");
			url = url.Replace("я", "ya");

			char[] replacerList = @"$%#@!*?;:~`+=()[]{}|\'<>,/^&"".".ToCharArray();
			for (int i = 0; i < replacerList.Length; i++)
			{
				string strChr = replacerList[i].ToString();
				if (url.Contains(strChr))
				{
					url = url.Replace(strChr, string.Empty);
				}
			}

			Regex regex = new Regex("[^a-zA-Z0-9_-]");
			url = regex.Replace(url, "-");
			while (url.IndexOf("--", StringComparison.Ordinal) > -1)
				url = url.Replace("--", "-");
			return url;
		}


	}
}
