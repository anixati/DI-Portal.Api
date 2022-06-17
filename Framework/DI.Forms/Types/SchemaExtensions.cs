using System;
using System.Collections.Generic;
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



        public static FormSchema AddHeaders(this FormSchema fs, Action<FormField> Configure)
        {
            var field = new FormField { Layout = LayoutType.Header };
            Configure(field);
            if (fs.Fields.All(x => x.Key != field.Key)) fs.Fields.Add(field);
            return fs;
        }

        public static FormSchema AddSubgrid(this FormSchema fs, string title,string viewId, Action<FormField> Configure)
        {
            fs.AddTab(title, f =>
            {
                var field = new FormField()
                {
                    Layout = LayoutType.SubGrid,
                    FieldType = FormFieldType.SubGrid,
                    ViewId = viewId,
                };
                Configure(field);
                if (f.Fields.All(x => x.Key != field.Key)) f.Fields.Add(field);

            });
            return fs;
        }
    }
}