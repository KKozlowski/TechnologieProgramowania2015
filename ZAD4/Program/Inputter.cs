using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program {
    class Inputter {
        public bool GetInt(string prompt, out int wynik) {
            Console.Write(prompt);
            string ident = Console.ReadLine();
            if (!Int32.TryParse(ident, out wynik)) {
                return false;
            } else
                return true;
        }
    }
}
