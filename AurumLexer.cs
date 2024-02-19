namespace Aurum
{
    /// <summary>
    /// Converts text into tokens that can be read by the parser.
    /// </summary>
    internal class AurumLexer
    {
        private string _text;
        private int _pos;
        private int _line;
        private int _column;
        private string _word;
        public string Word => _word; // The current word being read.
        public Token CurrentToken { get; private set; } // The current token being read.

        public void Init(string text)
        {
            _text = text;
            _pos = 0;
            _line = 1;
            _column = 1;
            _word = "";
        }

        /// <summary>
        /// Indicates the current position in the text for error messages.
        /// </summary>
        public string GetPosition()
        {
            return $"Line {_line}, Col {_column}";
        }

        /// <summary>
        /// Builds the next token from the text.
        /// </summary>
        private Token BuildToken()
        {
            while (_pos < _text.Length)
            {
                var c = _text[_pos];
                if (char.IsWhiteSpace(c))
                {
                    if (c == '\n')
                    {
                        _line++;
                        _column = 1;
                    }
                    else
                    {
                        _column++;
                    }
                    _pos++;
                    continue;
                }
                if (c == '{')
                {
                    _pos++;
                    _column++;
                    return Token.OpenCurly;
                }
                if (c == '}')
                {
                    _pos++;
                    _column++;
                    return Token.CloseCurly;
                }
                if (c == '[')
                {
                    _pos++;
                    _column++;
                    return Token.OpenSquare;
                }
                if (c == ']')
                {
                    _pos++;
                    _column++;
                    return Token.CloseSquare;
                }
                if (c == '(')
                {
                    _pos++;
                    _column++;
                    return Token.OpenParen;
                }
                if (c == ')')
                {
                    _pos++;
                    _column++;
                    return Token.CloseParen;
                }
                if (c == '<')
                {
                    _pos++;
                    _column++;
                    if (_pos < _text.Length && _text[_pos] == '=')
                    {
                        _pos++;
                        _column++;
                        return Token.LessEqual;
                    }
                    if (_pos < _text.Length && _text[_pos] == '<')
                    {
                        _pos++;
                        _column++;
                        return Token.Shl;
                    }
                    return Token.OpenAngle;
                }
                if (c == '>')
                {
                    _pos++;
                    _column++;
                    if (_pos < _text.Length && _text[_pos] == '=')
                    {
                        _pos++;
                        _column++;
                        return Token.GreaterEqual;
                    }
                    if (_pos < _text.Length && _text[_pos] == '>')
                    {
                        _pos++;
                        _column++;
                        return Token.Shr;
                    }
                    return Token.CloseAngle;
                }
                if (c == '+')
                {
                    _pos++;
                    _column++;
                    if (_pos < _text.Length && _text[_pos] == '+')
                    {
                        _pos++;
                        _column++;
                        return Token.Inc;
                    }
                    return Token.Plus;
                }
                if (c == '-')
                {
                    _pos++;
                    _column++;
                    if (_pos < _text.Length && _text[_pos] == '>')
                    {
                        _pos++;
                        _column++;
                        return Token.Arrow;
                    }
                    if (_pos < _text.Length && _text[_pos] == '-')
                    {
                        _pos++;
                        _column++;
                        return Token.Dec;
                    }
                    return Token.Minus;
                }
                if (c == '*')
                {
                    _pos++;
                    _column++;
                    return Token.Mult;
                }
                if (c == '/')
                {
                    _pos++;
                    _column++;
                    return Token.Div;
                }
                if (c == '%')
                {
                    _pos++;
                    _column++;
                    return Token.Mod;
                }
                if (c == '=')
                {
                    _pos++;
                    _column++;
                    if (_pos < _text.Length && _text[_pos] == '>')
                    {
                        _pos++;
                        _column++;
                        return Token.Lambda;
                    }
                    return Token.Equal;
                }
                if (c == '!')
                {
                    _pos++;
                    _column++;
                    if (_pos < _text.Length && _text[_pos] == '=')
                    {
                        _pos++;
                        _column++;
                        return Token.NotEqual;
                    }
                    return Token.Not;
                }
                if (c == ',')
                {
                    _pos++;
                    _column++;
                    return Token.Comma;
                }
                if (c == '.')
                {
                    _pos++;
                    _column++;
                    return Token.Dot;
                }
                if (c == ';')
                {
                    _pos++;
                    _column++;
                    return Token.Semi;
                }
                if (c == '?')
                {
                    _pos++;
                    _column++;
                    return Token.Question;
                }
                if (c == ':')
                {
                    _pos++;
                    _column++;
                    if (_pos < _text.Length && _text[_pos] == '=')
                    {
                        _pos++;
                        _column++;
                        return Token.Assign;
                    }
                    return Token.Colon;
                }
                if (c == '|')
                {
                    _pos++;
                    _column++;
                    if (_pos < _text.Length && _text[_pos] == '|')
                    {
                        _pos++;
                        _column++;
                        return Token.Or;
                    }
                    return Token.BitOr;
                }
                if (c == '&')
                {
                    _pos++;
                    _column++;
                    if (_pos < _text.Length && _text[_pos] == '&')
                    {
                        _pos++;
                        _column++;
                        return Token.And;
                    }
                    return Token.BitAnd;
                }
                if (c == '^')
                {
                    _pos++;
                    _column++;
                    return Token.BitXor;
                }
                if (c == '~')
                {
                    _pos++;
                    _column++;
                    return Token.BitNot;
                }
                if (char.IsLetter(c))
                {
                    var start = _pos;
                    while (_pos < _text.Length && char.IsLetterOrDigit(_text[_pos]))
                    {
                        _pos++;
                        _column++;
                    }
                    var id = _text.Substring(start, _pos - start);
                    if (Enum.TryParse(id, true, out Token token) && token >= Token.True)
                    {
                        return token;
                    }
                    return Token.Identifier;
                }
                if (char.IsDigit(c))
                {
                    var start = _pos;
                    while (_pos < _text.Length && char.IsDigit(_text[_pos]))
                    {
                        _pos++;
                        _column++;
                    }
                    if (_pos < _text.Length && _text[_pos] == '.')
                    {
                        _pos++;
                        _column++;
                        while (_pos < _text.Length && char.IsDigit(_text[_pos]))
                        {
                            _pos++;
                            _column++;
                        }
                        _word = _text.Substring(start, _pos - start);
                        return Token.Float;
                    }
                    _word = _text.Substring(start, _pos - start);
                    return Token.Integer;
                }
                if (c == '"')
                {
                    var start = _pos;
                    _pos++;
                    _column++;
                    while (_pos < _text.Length && _text[_pos] != '"')
                    {
                        if (_text[_pos] == '\\')
                        {
                            _pos++;
                            _column++;
                        }
                        _pos++;
                        _column++;
                    }
                    if (_pos == _text.Length)
                    {
                        throw new Exception($"Unterminated string literal");
                    }
                    _pos++;
                    _column++;
                    _word = _text.Substring(start, _pos - start);
                    return Token.String;
                }
                if (c == '\'')
                {
                    _pos++;
                    _column++;
                    if (_pos == _text.Length)
                    {
                        throw new Exception($"Unterminated character literal");
                    }
                    if (_text[_pos] == '\\')
                    {
                        _pos++;
                        _column++;
                    }
                    _pos++;
                    _column++;
                    if (_pos == _text.Length || _text[_pos] != '\'')
                    {
                        throw new Exception($"Unterminated character literal");
                    }
                    _pos++;
                    _column++;
                    _word = _text.Substring(_pos - 2, 3);
                    return Token.Char;
                }
            }
            return Token.EOF;
        }

        public void GetToken
    }
}
