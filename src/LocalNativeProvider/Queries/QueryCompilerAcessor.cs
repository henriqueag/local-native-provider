using SqlKata.Compilers;

namespace LocalNativeProvider.Queries;

public static class QueryCompilerAcessor
{
    public static readonly Compiler Compiler = new PostgresCompiler();
}