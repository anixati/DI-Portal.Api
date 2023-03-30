using Newtonsoft.Json;
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

        public static FormSchema AddAction(this FormSchema fs, string schema, string label, string desc = "")
        {
            var action = new FormAction(schema, label, desc);
            if (fs.Actions.All(x => x.Schema != action.Schema)) fs.Actions.Add(action);
            return fs;
        }

        public static FormSchema WithAction(this FormSchema fs, string schema, Action<FormAction> Configure)
        {
            var fa = fs.Actions.FirstOrDefault(x => x.Schema == schema);
            if (fa != null)
                Configure(fa);
            return fs;
        }

        public static FormSchema AddHeaders(this FormSchema fs, string action, Action<FormField> Configure)
        {
            var field = new FormField {Layout = LayoutType.Header};
            Configure(field);
            if (fs.Fields.All(x => x.Key != field.Key)) fs.Fields.Add(field);
            return fs;
        }


        public static FormSchema AddHeaders(this FormSchema fs, Action<FormField> Configure)
        {
            var field = new FormField {Layout = LayoutType.Header};
            Configure(field);
            if (fs.Fields.All(x => x.Key != field.Key)) fs.Fields.Add(field);
            return fs;
        }

        public static FormSchema AddSubGrid(this FormSchema fs, string title, string viewId,
            Action<FormField> Configure)
        {
            fs.AddTab(title, f =>
            {
                var field = new FormField
                {
                    Layout = LayoutType.SubGrid,
                    FieldType = FormFieldType.SubGrid,
                    ViewId = viewId
                };
                var lst = new List<SelectItem>(){new SelectItem(viewId,"Active Items")};
                field.Options = JsonConvert.SerializeObject(lst);
                Configure(field);
                if (f.Fields.All(x => x.Key != field.Key)) f.Fields.Add(field);
            });
            return fs;
        }


        public static FormSchema AddSubGrid(this FormSchema fs, string title, List<SelectItem> views,
            Action<FormField> Configure)
        {
            fs.AddTab(title, f =>
            {
                var field = new FormField
                {
                    Layout = LayoutType.SubGrid,
                    FieldType = FormFieldType.SubGrid,
                };
                field.Options = JsonConvert.SerializeObject(views);
                Configure(field);
                if (f.Fields.All(x => x.Key != field.Key)) f.Fields.Add(field);
            });
            return fs;
        }


        public static FormSchema AddDocGrid(this FormSchema fs, string viewId, string title = "Documents")
        {
            fs.AddTab(title, f =>
            {
                var field = new FormField
                {
                    Layout = LayoutType.SubGrid,
                    FieldType = FormFieldType.Documents,
                    ViewId = viewId
                };
                if (f.Fields.All(x => x.Key != field.Key)) f.Fields.Add(field);
            });
            return fs;
        }
    }
}