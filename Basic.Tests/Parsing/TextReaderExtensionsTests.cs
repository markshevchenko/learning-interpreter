﻿namespace Basic.Tests.Parsing
{
    using System.IO;
    using System.Text;
    using Basic.Parsing;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class TextReaderExtensionsTests : BaseTests
    {
        [TestMethod]
        public void TryReadPunctuationMark_WithLParent_ReturnsTrue()
        {
            using (var reader = new StringReader("(a + b)"))
            {
                var builder = new StringBuilder();
                var token = Token.Unknown;

                var condition = reader.TryReadPunctuationMark(builder, out token);

                Assert.IsTrue(condition);
            }
        }

        [TestMethod]
        public void TryReadPunctuationMark_WithLParent_SetsTokenToTokenLParen()
        {
            using (var reader = new StringReader("(a + b)"))
            {
                var builder = new StringBuilder();
                var token = Token.Unknown;

                var condition = reader.TryReadPunctuationMark(builder, out token);

                Assert.AreEqual(Token.LParen, token);
            }
        }

        [TestMethod]
        public void TryReadPunctuationMark_WithLParent_AppednsToBuilderLParenCharacter()
        {
            using (var reader = new StringReader("(a + b)"))
            {
                var builder = new StringBuilder();
                var token = Token.Unknown;

                var condition = reader.TryReadPunctuationMark(builder, out token);

                Assert.AreEqual("(", builder.ToString());
            }
        }

        [TestMethod]
        public void TryReadOperator_WithLessThanOrEqual_ReturnsTrue()
        {
            using (var reader = new StringReader("<=12"))
            {
                var builder = new StringBuilder();
                var token = Token.Unknown;

                var condition = reader.TryReadOperator(builder, out token);

                Assert.IsTrue(condition);
            }
        }

        [TestMethod]
        public void TryReadOperator_WithLessThanOrEqual_SetsTokenToTokenLe()
        {
            using (var reader = new StringReader("<=12"))
            {
                var builder = new StringBuilder();
                var token = Token.Unknown;

                var condition = reader.TryReadOperator(builder, out token);

                Assert.AreEqual(Token.Le, token);
            }
        }

        [TestMethod]
        public void TryReadOperator_WithLessThanOrEqual_AppednsToBuilderLessThanAndEqualCharacters()
        {
            using (var reader = new StringReader("<=12"))
            {
                var builder = new StringBuilder();
                var token = Token.Unknown;

                var condition = reader.TryReadOperator(builder, out token);

                Assert.AreEqual("<=", builder.ToString());
            }
        }

        [TestMethod]
        public void TryReadOperator_WithNonEqual_SetsTokenToTokenNe()
        {
            using (var reader = new StringReader("<>56"))
            {
                var builder = new StringBuilder();
                var token = Token.Unknown;

                var condition = reader.TryReadOperator(builder, out token);

                Assert.AreEqual(Token.Ne, token);
            }
        }

        [TestMethod]
        public void TryReadOperator_WithLessThan_SetsTokenToTokenLt()
        {
            using (var reader = new StringReader("<4"))
            {
                var builder = new StringBuilder();
                var token = Token.Unknown;

                var condition = reader.TryReadOperator(builder, out token);

                Assert.AreEqual(Token.Lt, token);
            }
        }

        [TestMethod]
        public void TryReadOperator_WithNonPunctuationMark_ReturnsFalse()
        {
            using (var reader = new StringReader("id100"))
            {
                var builder = new StringBuilder();
                var token = Token.Unknown;

                var condition = reader.TryReadOperator(builder, out token);

                Assert.IsFalse(condition);
            }
        }

        [TestMethod]
        public void TryReadString_WithString_ReturnsTrue()
        {
            using (var reader = new StringReader("\" simple string\""))
            {
                var builder = new StringBuilder();
                var token = Token.Unknown;

                var condition = reader.TryReadString(builder, out token);

                Assert.IsTrue(condition);
            }
        }

        [TestMethod]
        public void TryReadString_WithNumber_ReturnsFalse()
        {
            using (var reader = new StringReader("314159"))
            {
                var builder = new StringBuilder();
                var token = Token.Unknown;

                var condition = reader.TryReadString(builder, out token);

                Assert.IsFalse(condition);
            }
        }

        [TestMethod]
        public void TryReadString_WithString_SetsTokenToTokenString()
        {
            using (var reader = new StringReader("\" simple string\""))
            {
                var builder = new StringBuilder();
                var token = Token.Unknown;

                var condition = reader.TryReadString(builder, out token);

                Assert.AreEqual(Token.String, token);
            }
        }

        [TestMethod]
        public void TryReadString_WithString_AppednsToBuilderStringCharactersWithoutQuotes()
        {
            using (var reader = new StringReader("\" simple string\""))
            {
                var builder = new StringBuilder();
                var token = Token.Unknown;

                var condition = reader.TryReadString(builder, out token);

                Assert.AreEqual(" simple string", builder.ToString());
            }
        }

        [TestMethod]
        public void TryReadString_WithDoubleQuotesInside_AppendsToBuilderSingleQuote()
        {
            using (var reader = new StringReader("\" simple \"\"string\"\"\""))
            {
                var builder = new StringBuilder();
                var token = Token.Unknown;

                var condition = reader.TryReadString(builder, out token);

                Assert.AreEqual(" simple \"string\"", builder.ToString());
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ParserException))]
        public void TryReadString_WithoutClosingQuote_ThrowsUnexpectedEndOfFileException()
        {
            using (var reader = new StringReader("\" a string example "))
            {
                var builder = new StringBuilder();
                var token = Token.Unknown;

                var condition = reader.TryReadString(builder, out token);
            }
        }

        [TestMethod]
        public void TryReadIntegerOrFloatNumber_WithInteger_ReturnsTrue()
        {
            using (var reader = new StringReader("117342+3.14159"))
            {
                var builder = new StringBuilder();
                var token = Token.Unknown;

                var condition = reader.TryReadIntegerOrFloatNumber(builder, out token);

                Assert.IsTrue(condition);
            }
        }

        [TestMethod]
        public void TryReadIntegerOrFloatNumber_WithInteger_SetsTokenToTokenInteger()
        {
            using (var reader = new StringReader("117342+3.14159"))
            {
                var builder = new StringBuilder();
                var token = Token.Unknown;

                var condition = reader.TryReadIntegerOrFloatNumber(builder, out token);

                Assert.AreEqual(Token.Integer, token);
            }
        }

        [TestMethod]
        public void TryReadIntegerOrFloatNumber_WithInteger_AppendsToBuilderDigits()
        {
            using (var reader = new StringReader("117342+3.14159"))
            {
                var builder = new StringBuilder();
                var token = Token.Unknown;

                var condition = reader.TryReadIntegerOrFloatNumber(builder, out token);

                Assert.AreEqual("117342", builder.ToString());
            }
        }

        [TestMethod]
        public void TryReadIntegerOrFloatNumber_WithFloat_ReturnsTrue()
        {
            using (var reader = new StringReader("3.14159+117342"))
            {
                var builder = new StringBuilder();
                var token = Token.Unknown;

                var condition = reader.TryReadIntegerOrFloatNumber(builder, out token);

                Assert.IsTrue(condition);
            }
        }

        [TestMethod]
        public void TryReadIntegerOrFloatNumber_WithFloat_SetsTokenToTokenFloat()
        {
            using (var reader = new StringReader("3.14159+117342"))
            {
                var builder = new StringBuilder();
                var token = Token.Unknown;

                var condition = reader.TryReadIntegerOrFloatNumber(builder, out token);

                Assert.AreEqual(Token.Float, token);
            }
        }

        [TestMethod]
        public void TryReadIntegerOrFloatNumber_WithFloat_ApeendsToBuilderDigitsAndPoint()
        {
            using (var reader = new StringReader("3.14159+117342"))
            {
                var builder = new StringBuilder();
                var token = Token.Unknown;

                var condition = reader.TryReadIntegerOrFloatNumber(builder, out token);

                Assert.AreEqual("3.14159", builder.ToString());
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ParserException))]
        public void TryReadIntegerOrFloatNumber_WithoutFractionalPart_ThrowsParserException()
        {
            using (var reader = new StringReader("3.abc"))
            {
                var builder = new StringBuilder();
                var token = Token.Unknown;

                var condition = reader.TryReadIntegerOrFloatNumber(builder, out token);
            }
        }

        [TestMethod]
        public void TryReadIdentifierOrKeyword_WithIdentifier_ReturnsTrue()
        {
            using (var reader = new StringReader("_Identifier_123"))
            {
                var builder = new StringBuilder();
                var token = Token.Unknown;

                var condition = reader.TryReadIdentifierOrKeyword(builder, out token);

                Assert.IsTrue(condition);
            }
        }

        [TestMethod]
        public void TryReadIdentifierOrKeyword_WithIdentifier_SetsTokenToTokenIdentifier()
        {
            using (var reader = new StringReader("_Identifier_123"))
            {
                var builder = new StringBuilder();
                var token = Token.Unknown;

                var condition = reader.TryReadIdentifierOrKeyword(builder, out token);

                Assert.AreEqual(Token.Identifier, token);
            }
        }

        [TestMethod]
        public void TryReadIdentifierOrKeyword_WithIdentifier_AppendsToBuilderUnderscoresLettersAndDigits()
        {
            using (var reader = new StringReader("_Identifier_123"))
            {
                var builder = new StringBuilder();
                var token = Token.Unknown;

                var condition = reader.TryReadIdentifierOrKeyword(builder, out token);

                Assert.AreEqual("_Identifier_123", builder.ToString());
            }
        }

        [TestMethod]
        public void TryReadIdentifierOrKeyword_WithPrintKeyword_ReturnsTrue()
        {
            using (var reader = new StringReader("pRiNt"))
            {
                var builder = new StringBuilder();
                var token = Token.Unknown;

                var condition = reader.TryReadIdentifierOrKeyword(builder, out token);

                Assert.IsTrue(condition);
            }
        }

        [TestMethod]
        public void TryReadIdentifierOrKeyword_WithPrintKeyword_SetsTokenToTokenPrint()
        {
            using (var reader = new StringReader("pRiNt"))
            {
                var builder = new StringBuilder();
                var token = Token.Unknown;

                var condition = reader.TryReadIdentifierOrKeyword(builder, out token);

                Assert.AreEqual(Token.Print, token);
            }
        }

        [TestMethod]
        public void TryReadIdentifierOrKeyword_WithPrintKeyword_AppendsToBuilderLetters()
        {
            using (var reader = new StringReader("pRiNt"))
            {
                var builder = new StringBuilder();
                var token = Token.Unknown;

                var condition = reader.TryReadIdentifierOrKeyword(builder, out token);

                Assert.AreEqual("pRiNt", builder.ToString());
            }
        }
    }
}
