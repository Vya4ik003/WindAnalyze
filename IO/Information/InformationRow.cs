using System.Linq;
using System.Text.RegularExpressions;

namespace IO.Information
{
    class InformationRow
    {
        private readonly Regex _regexForInformationRow;
        private readonly string[] _namesOfMatches;

        public readonly Match MatchForInformationRow;
        public string[] Output;

        /// <summary>
        /// Инициализирует новый экземпляр класса InformationRow
        /// </summary>
        /// <param name="pattern">Паттерн для RegEx</param>
        /// <param name="inputString">Входное значения string для RegEx</param>
        public InformationRow(string pattern, string inputString, params string[] names)
        {
            _namesOfMatches = names;
            pattern = InsertNames(pattern);

            _regexForInformationRow = new Regex(pattern);
            MatchForInformationRow = _regexForInformationRow.Match(inputString);

            Output = GetOutput();
        }

        private string InsertNames(string input)
        {
            string output = input;
            foreach (string name in _namesOfMatches)
            {
                int indexOfTriple = output.IndexOf("...");
                output = output.Substring(0, indexOfTriple) + output.Substring(indexOfTriple + 3);
                output = output.Insert(indexOfTriple, name);
            }
            return output;
        }

        private string[] GetOutput()
        {
            string[] output = new string[_namesOfMatches.Length];
            for (int i = 0; i < _namesOfMatches.Length; i++)
            {
                output[i] = MatchForInformationRow.Result($"${{{_namesOfMatches[i]}}}");
            }

            return output;
        }
    }
}
