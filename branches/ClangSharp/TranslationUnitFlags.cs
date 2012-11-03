using System;

namespace ClangSharp
{
    [Flags]
    public enum TranslationUnitFlags
    {
        None = 0,
        DetailedPreprocessingRecord = 1,
        Incomplete = 2,
        PrecompiledPreamble = 4,
        CacheCompletionResults = 8,
        ForSerialization = 16,
        CXXChainedPCH = 32,
        SkipFunctionBodies = 64,
        IncludeBriefCommentsInCodeCompletion = 128
    }
}
