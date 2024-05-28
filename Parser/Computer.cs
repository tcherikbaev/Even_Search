using Extreme.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace parserDecimal.Parser
{
    class Computer
    {
        Operands operands = new Operands();
        Polish polish = new Polish();
        Calculator calculator = new Calculator();
        List<string> arguments = new List<string>();
        Stack<decimal> opers = new Stack<decimal>();
        internal decimal Compute(string function, decimal value)
        {
            List<string> splitExpression = operands.returnSplitExpression(function.Replace(" ", ""));
            Queue<string> revpExpression = polish.returnPolish(splitExpression);

            decimal answer = calculator.calculate(revpExpression, value);
            arguments = calculator.getArgs();
            opers = calculator.getOpers();
            // if()
            return answer;
        }
        public bool regs()
        {
            bool flag = true;
            string val = arguments.First();
            foreach (var item in arguments)
            {
                if (item != val)
                {
                    flag = false;
                }
            }
            return flag;
        }
        public int operats(decimal value)
        {
            Decimal p = DecimalMath.Log10(value);
            if (opers.Contains(p))
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }
    }
}
