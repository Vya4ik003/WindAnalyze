using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Buisness.Information
{
    class InformationRow
    {
        private Regex Regex { get; }
        private IEnumerable<string> NamesOfMatches { get; }
        private Match Match { get; }

        public List<string> OutputMatches { get; private set; }

        public InformationRow(string pattern, string inputString, params string[] matchNames)
        {
            NamesOfMatches = matchNames.ToList();
            pattern = InsertNamesToPattern(pattern);

            Regex = new Regex(pattern);
            Match = Regex.Match(inputString);

            OutputMatches = GetOutput();
        }

        /// <summary>
        /// Происхолит вставка введённых имён в pattern путём замены троеточий на имена
        /// </summary>
        /// <param name="input">Входной pattern без имён</param>
        private string InsertNamesToPattern(string input)
        {
            foreach (string name in NamesOfMatches)
            {
                int indexOfTriple = input.IndexOf("...");
                input = input.Substring(0, indexOfTriple) + input.Substring(indexOfTriple + 3);
                input = input.Insert(indexOfTriple, name);
            }
            return input;
        }

        /// <summary>
        /// Метод для получения Match'ей 
        /// </summary>
        /// <returns>Список Match'ей в Regex'е</returns>
        private List<string> GetOutput()
        {
            List<string> output = new List<string>(NamesOfMatches.Count());
            for (int i = 0; i < NamesOfMatches.Count(); i++)
            {
                output.Add(Match.Result($"${{{NamesOfMatches.ElementAt(i)}}}"));
            }

            return output;
        }

    }
}
