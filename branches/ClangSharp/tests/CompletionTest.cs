﻿using System;
using System.Collections.ObjectModel;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClangSharp;

namespace tests {
    [TestClass]
    public class CompletionTest : TestBase {
        [TestMethod]
        public void TestMemberCompletion() {
            const string completionLine = "instance.";
            const string edited = "#include <iostream>\n" +
                                  "#include \"fake-class.h\"\n" +
                                  "int main() {\n" +
                                  "FakeClass instance;\n" +
                                  completionLine + "\n" +
                                  "}\n";
            var unsavedFile = new UnsavedFile(edited, MainCpp, edited.Length);
            uint line = (uint)edited.Substring(0, edited.IndexOf(completionLine)).Count(character => character == '\n') + 1;
            uint column = (uint)completionLine.Length + 1;
            var completions = Main.CodeCompleteAt(line, column, new[] { unsavedFile }, CodeCompletion.Options.IncludeBriefComments).OrderBy(completion => completion.Priority).Take(10);
            Assert.AreEqual(5, completions.Count());
        }
    }
}
