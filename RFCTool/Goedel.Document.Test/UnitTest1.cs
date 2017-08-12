using System;
using UT=Microsoft.VisualStudio.TestTools.UnitTesting;
using Goedel.Document.Markdown;
using Goedel.Utilities;
using Goedel.FSR;
using Goedel.Document.OpenXML;

namespace Goedel.Document.Test {

    public class TestVector {
        public string Input;
        public string Result;
        public TestVector (string Input, string Result) {
            this.Input = Input;
            this.Result = Result;
            }
        }


    [UT.TestClass]
    public class UnitTest1 {
        public TestVector[] TestVectorXML = new TestVector[] {

            new TestVector("This <i>is</i> a test.", "This <i>is</i> a test."),
            new TestVector("This <a=link>is</a> a test.", "This <a=\"link\">is</a> a test."),
            new TestVector("This <a=\"link\">is</a> a test.",  "This <a=\"link\">is</a> a test."),
            new TestVector("This <a=\"http://example.com/\">is</a> a test.",  
                    "This <a=\"http://example.com/\">is</a> a test."),

            new TestVector("This SHOULD be or <i>is</i> a MUST test.",
                    "This <bcp14>SHOULD</bcp14> be or <i>is</i> a <bcp14>MUST</bcp14> test."),

            new TestVector("This is</a> a test.", "This is a test."),
            new TestVector("This <i>is</a> a test.", "This <i>is</i> a test."),
            new TestVector("This <i>is a test.", "This <i>is a test.</i>"),

            new TestVector("", "")
            };
        public TestVector[] TestVectorNormative = new TestVector[] {

            new TestVector("THIS MUST", "THIS <bcp14>MUST</bcp14>"),
            new TestVector("THIS MUST.", "THIS <bcp14>MUST</bcp14>."),
            
            // No BCP language
            new TestVector("Hello world", "Hello world"),
            new TestVector("HELLO world", "HELLO world"),

            // With BCP language
            new TestVector("REQUIRED: Do this.", "<bcp14>REQUIRED</bcp14>: Do this."),
            new TestVector("RECOMMENDED: Do this.", "<bcp14>RECOMMENDED</bcp14>: Do this."),
            new TestVector("That is OPTIONAL.", "That is <bcp14>OPTIONAL</bcp14>."),

            // Test NOT
            new TestVector("This MUST NOT be false", "This <bcp14>MUST NOT</bcp14> be false"),
            new TestVector("This SHALL NOT be true", "This <bcp14>SHALL NOT</bcp14> be true"),
            new TestVector("This SHOULD NOT be true", "This <bcp14>SHOULD NOT</bcp14> be true"),
            new TestVector("This MAY NOT be true", "This <bcp14>MAY</bcp14> NOT be true"),

            // With negatable BCP language
            new TestVector("This MUST be true", "This <bcp14>MUST</bcp14> be true"),
            new TestVector("This SHALL be true", "This <bcp14>SHALL</bcp14> be true"),
            new TestVector("This SHOULD be true", "This <bcp14>SHOULD</bcp14> be true"),
            new TestVector("This MAY be true", "This <bcp14>MAY</bcp14> be true"),

            new TestVector("THIS MUST", "THIS <bcp14>MUST</bcp14>"),
            new TestVector("THIS MUST BE", "THIS <bcp14>MUST</bcp14> BE"),
            new TestVector("This MUST.", "This <bcp14>MUST</bcp14>."),
            new TestVector("This MUST NOT", "This <bcp14>MUST NOT</bcp14>"),
            new TestVector("This MUST NOT.", "This <bcp14>MUST NOT</bcp14>."),
            // Null
            new TestVector("", "")
            };

        public void TestWordLexer (TestVector TestVector) {

            var Block = new Block();
            var Lexer = new MarkNewParagraph(Block);

            Lexer.Push(TestVector.Input);
            Lexer.PushEnd();

            // Check that the block is correct
            var Result = Block.ToHTML();
            UT.Assert.AreEqual(Result, TestVector.Result);

            }

        [UT.TestMethod]
        public void TestWordLexerNormative () {
            foreach (var TestVector in TestVectorNormative) {
                TestWordLexer(TestVector);
                }

            }

        [UT.TestMethod]
        public void TestWordLexerXML () {
            foreach (var TestVector in TestVectorXML) {
                TestWordLexer(TestVector);
                }

            }


        }
    }
