using System;
using System.Linq;

namespace DI.Forms.Types
{
    public static class SchemaExtensions
    {
        public static FormSchema AddPage(this FormSchema fs, string title, Action<FormField> Configure)
        {
            var section = new FormField {Layout = LayoutType.Page, Title = title};
            Configure(section);
            if (fs.Fields.All(x => x.Key != section.Key)) fs.Fields.Add(section);
            return fs;
        }

        public static FormSchema AddTab(this FormSchema fs, string title, Action<FormField> Configure)
        {
            var field = new FormField {Layout = LayoutType.Tab, Title = title};
            Configure(field);
            if (fs.Fields.All(x => x.Key != field.Key)) fs.Fields.Add(field);
            return fs;
        }


        public static FormSchema AddSubgrid(this FormSchema fs, string title,string viewId)
        {
            fs.AddTab(title, f =>
            {
                var field = new FormField()
                {
                    Layout = LayoutType.Subgrid,
                    FieldType = FormFieldType.Subgrid,
                    ViewId = viewId
                
                };
                if (f.Fields.All(x => x.Key != field.Key)) f.Fields.Add(field);

            });
            return fs;
        }
    }
}