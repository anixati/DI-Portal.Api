using SqlKata.Compilers;

namespace Di.Qry.Schema
{
    internal class LocalCompiler : SqlServerCompiler
    {
        public LocalCompiler()
        {
            //OpeningIdentifier = "";
            //ClosingIdentifier = "";
            //UseLegacyPagination = true;
        }

        //public override string WrapValue(string value)
        //{
        //    if (value == "*") return value;
        //    return value;
        //}
    }
}