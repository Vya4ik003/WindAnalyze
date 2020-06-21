using Buisness.Information;
using System;

namespace Buisness
{
    public class WindsReader
    {
        private FileInformation _infoAboutFile;

        private PeriodInformation[] _infoAboutPeriods;

        private DateTime[] _windsDate;

        private string[] _windsNames;

        private string[] _informationRows;

        public WindsReader(DateTime[] dates, string[] winds, string[] infoRows)
        {
            _windsDate = dates;
            _windsNames = winds;
            _informationRows = infoRows;
        }

        public BLResult ReadWinds()
        {
            try
            {

                _infoAboutFile = new FileInformation(_windsDate, _windsNames, _informationRows);
                _infoAboutPeriods = _infoAboutFile.GetPeriodsInformation();

                BLResult result = new BLResult("Success", _infoAboutFile, _infoAboutPeriods);

                return result;
            }
            catch (Exception e)
            {
                return new BLResult(e.Message);
            }
        }
    }
}
