using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankOCR.Services.Interfaces
{
    public interface IAccountNumberParsingService
    {
        public string ParseOcrInput(string input);
        public IEnumerable<string> GetPossibleAccountNumbers(string input);
    }
}
