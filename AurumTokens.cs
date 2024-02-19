﻿namespace Aurum
{
    internal enum Token
    {
        Unknown,
        EOF,
        // brackets
        OpenCurly,
        CloseCurly,
        OpenSquare,
        CloseSquare,
        OpenParen,
        CloseParen,
        OpenAngle,
        CloseAngle,
        // operators
        Plus,
        Minus,
        Inc,
        Dec,
        Mult,
        Div,
        Mod,
        Assign,
        Equal,
        NotEqual,
        Greater,
        GreaterEqual,
        Less,
        LessEqual,
        And,
        Or,
        Not,
        BitAnd,
        BitOr,
        BitXor,
        BitNot,
        Shr,
        Shl,
        Question,
        Colon,
        Range,
        // other symbols
        Comma,
        Dot,
        Semi,
        Arrow,
        Lambda,
        // literals
        Identifier,
        String,
        Integer,
        Float,
        Char,
        True,
        False,
        // keywords
        Begin,
        End,
        If,
        Then,
        Else,
        For,
        Of,
        While,
        Do,
        Break,
        Continue,
        Defer,
        Try,
        On,
        Finally,
        Throw,
        Case,
        Default,
        // oop keywords
        Namespace,
        Import,
        Publish,
        Protocol,
        Class,
        Extends,
        Implements,
        Func,
        Proc,
        Return,
        Property,
        Read,
        Write,
        Public,
        Private,
        Protected,
        Internal,
        Static,
        Final,
        Abstract,
        Override,
        Virtual,
        New,
        This,
        Operator,
    }
}