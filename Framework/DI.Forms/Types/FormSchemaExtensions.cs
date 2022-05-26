using System;
using System.Linq;

namespace DI.Forms.Types
{
    public static class FormSchemaExtensions
    {
        public static FormSchema AddPage(this FormSchema fs, string title, Action<FormField> Configure)
        {
            var section = new FormField { Layout = LayoutType.Page, Title = title};
            Configure(section);
            if (fs.Fields.All(x => x.Key != section.Key)) fs.Fields.Add(section);
            return fs;
        }


        public static FormField AddFieldGroup(this FormField fd, Action<FormField> Configure)
        {
            var field = new FormField { Layout = LayoutType.FieldGroup};
            Configure(field);
            if (fd.Fields.All(x => x.Key != field.Key)) fd.Fields.Add(field);
            return fd;
        }
        public static FormField AddTab(this FormField fd, string title, Action<FormField> Configure)
        {
            var field = new FormField { Layout = LayoutType.Tab, Title = title };
            Configure(field);
            if (fd.Fields.All(x => x.Key != field.Key)) fd.Fields.Add(field);
            return fd;
        }

        public static FormField AddInput(this FormField fd, string key,  int? width= null,string title= "")
        {
            fd.AddInput(key, title, width);
            return fd;
        }

        public static FormField AddInput(this FormField fd, string key,string title=null,int? width=null, Action<FormField> Configure= null)
        {
            var field = new FormField(key) { Layout = LayoutType.Default, FieldType = FormFieldType.Text ,Title = string.IsNullOrEmpty(title)?key: title,Width = width};
            Configure?.Invoke(field);
            if (fd.Fields.All(x => x.Key != field.Key)) fd.Fields.Add(field);
            return fd;
        }


    }
}