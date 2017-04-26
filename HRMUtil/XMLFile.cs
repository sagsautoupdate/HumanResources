using System;
using System.Xml.XPath;

namespace HRMUtil
{
    public class XMLFile
    {
        private readonly XPathNavigator _Nav;

        public XMLFile(string path)
        {
            Path = path;
            var doc = new XPathDocument(Path);
            _Nav = doc.CreateNavigator();
        }

        public string Path { get; set; }


        public string GetInsertingFormatMessageError(string errorCode, string formatString)
        {
            return string.Format(GetFormatMessageErrors(errorCode, "/HRM/Insert/Message"), formatString);
        }

        public string GetUpdatingFormatMessageError(string errorCode, string formatString)
        {
            return string.Format(GetFormatMessageErrors(errorCode, "/HRM/Update/Message"), formatString);
        }

        public string GetDeletingFormatMessageError(string errorCode, string formatString)
        {
            return string.Format(GetFormatMessageErrors(errorCode, "/HRM/Delete/Message"), formatString);
        }

        private string GetFormatMessageErrors(string errorCode, string xPath)
        {
            XPathExpression expr;
            expr = _Nav.Compile(xPath);
            var iterator = _Nav.Select(expr);
            iterator = _Nav.Select(expr);
            var messageReturn = string.Empty;
            try
            {
                while (iterator.MoveNext())
                {
                    var s = string.Empty;
                    var nav = iterator.Current.Clone();
                    s = nav.OuterXml;
                    var arr = s.Split('"');
                    if (arr.Length > 1)
                        if (errorCode.Equals(arr[1]))
                        {
                            messageReturn = nav.Value;
                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return messageReturn;
        }
    }
}