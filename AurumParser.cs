using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurum
{
    /// <summary>
    /// Builds an AST from a token stream.
    /// </summary>
    internal class AurumParser
    {
        private readonly AurumLexer lexer;
        public AurumParser()
        {
            lexer = new AurumLexer();
        }
        public IStmt Parse(string source)
        {
            lexer.Init(source);
            throw new NotImplementedException(); // TODO
        }
    }
}
