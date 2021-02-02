using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace BlogApp.Areas.Admin.Utils
{
    public static class SubsHtml
    {
        public static string SubstringHtml(string stringValue, int length)
        {
            var regexAllTags = new Regex(@"<[^>]*>");
            var regexIsTag = new Regex(@"<|>");
            var regexOpen = new Regex(@"<[^/][^>]*>");
            var regexClose = new Regex(@"</[^>]*>");
            var regexAttribute = new Regex(@"<[^ ]*");

            int necessaryCount = 0;

            if (regexAllTags.Replace(stringValue, "").Length <= length)
            {
                return stringValue;
            }

            string[] split = regexAllTags.Split(stringValue);
            string counter = "";
            foreach (string item in split)
            {
                if (counter.Length < length && counter.Length + item.Length >= length)
                {
                    necessaryCount = stringValue.IndexOf(item, counter.Length) + item.Substring(0, length - counter.Length).Length;
                    break;
                }
                counter += item;
            }

            var x = regexIsTag.Match(stringValue, necessaryCount);
            if (x.Value == ">")
            {
                necessaryCount = x.Index + 1;
            }

            string subs = stringValue.Substring(0, necessaryCount);
            var openTags = regexOpen.Matches(subs);
            var closeTags = regexClose.Matches(subs);

            List<string> OpenTags = new List<string>();
            foreach (var item in openTags)
            {
                string trans = regexAttribute.Match(item.ToString()).Value;

                if (trans.Last() == '>')
                {
                    trans = "</" + trans.Substring(1, trans.Length - 1);
                }
                else
                {
                    trans = "</" + trans.Substring(1, trans.Length - 1) + ">";
                }

                OpenTags.Add(trans);
            }

            foreach (System.Text.RegularExpressions.Match close in closeTags)
            {
                OpenTags.Remove(close.Value);
            }

            for (int i = OpenTags.Count - 1; i >= 0; i--)
            {
                subs += OpenTags[i];
            }

            return subs;
        }
    }
}

