using System.ComponentModel.DataAnnotations;
using System.Reflection;
using EnumGenie.Core;
using EnumGenie.Core.Describers;
using EnumGenie.Core.Filters;
using EnumGenie.Core.Sources;
using EnumGenie.Core.Transforms;
using EnumGenie.Core.Writers;
using EnumGenie.Sample.Enums;
using EnumGenie.TypeScript;

namespace EnumGenie.Sample
{
    public static class Program
    {
        public static void Main()
        {
            var genie = new EnumGenieGenerator()
                .SourceFrom.Assembly(typeof(Program).GetTypeInfo().Assembly)
                .FilterBy.Predicate(t => t != typeof(Ignored))
                .TransformBy.RenamingEnum(def => def.Name.Replace("StripThisOut", ""))
                .DescribeUsing.Attribute<DisplayAttribute>(a => a.Description)
                .WriteTo.Console(cfg => cfg.TypeScript(ts => ts.Declaration().Description().Descriptor()))
                .WriteTo.File("c:\\temp\\enums.ts", cfg => cfg.TypeScript(ts => ts.Declaration().Description().Descriptor()));

            genie.Write();
        }
    }
}