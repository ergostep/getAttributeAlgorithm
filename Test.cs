namespace algorithms.TestWork
{
    /*
     Функция возвращает первое значение атрибута, которое встречается в файле
    Проблемы с текущим кодом:
    1. Название метода
    2. Некорректная работа в случае, если название атрибута совпадается с любым значением в xml.
    3. Сложность в восприятием кода.
    */
    public static class Tester
    {
        public static string Func1(string input, string elementName, string attrName)
        {
            string[] lines = System.IO.File.ReadAllLines(input);
            string result = null;

            foreach (var line in lines)
            {
                var startElEndex = line.IndexOf(elementName);

                if (startElEndex != -1)
                {
                    if (line[startElEndex - 1] == '<')
                    {
                        var endElIndex = line.IndexOf('>', startElEndex - 1);
                        var attrStartIndex = line.IndexOf(attrName, startElEndex, endElIndex - startElEndex + 1);

                        if (attrStartIndex != -1)
                        {
                        int valueStartIndex = attrStartIndex + attrName.Length + 2;

                        while (line[valueStartIndex] != '"')
                        {
                            result += line[valueStartIndex];
                            valueStartIndex++;
                        }

                        break;
                        }
                    }
                }
            }
            return result;
        }

        public static string GetAttributeValueByName(string input, string elementName, string attrName)
        {
            string[] lines;
            try
            {
                lines = System.IO.File.ReadAllLines(input);
            }
            catch (IOException e)
            {
                System.Console.WriteLine(e.ToString());
                throw;
            }
            
            string result = String.Empty;

            foreach (var line in lines)
            {
                var startElEndex = line.IndexOf("<"+elementName+" ");

                if (startElEndex != -1)
                {
                    
                        var endElIndex = line.IndexOf('>', startElEndex - 1);
                        //remove element length from string to receive a string consist only of pairs key="value"
                        var pairs = line.Substring(startElEndex + elementName.Length + 2, endElIndex-startElEndex - elementName.Length - 2).Split(" ");

                        foreach (var pair in pairs)
                        {
                            if (pair.Split("=")[0] == attrName)
                            {
                                return pair.Split("=")[1].Trim('"');
                            }
                        }           
                }
            }
            return result;
        }

    }
}