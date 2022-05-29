using System;
using System.Linq;

namespace DI.Forms.Types
{
    public static class FormSchemaExtensions
    {
        public static FormSchema AddPage(this FormSchema fs, string title, Action<FormField> Configure)
        {
            var section = new FormField {Layout = LayoutType.Page, Title = title};
            Configure(section);
            if (fs.Fields.All(x => x.Key != section.Key)) fs.Fields.Add(section);
            return fs;
        }


        public static FormField AddFieldGroup(this FormField fd, Action<FormField> Configure)
        {
            var field = new FormField {Layout = LayoutType.FieldGroup};
            Configure(field);
            if (fd.Fields.All(x => x.Key != field.Key)) fd.Fields.Add(field);
            return fd;
        }

        public static FormField AddTab(this FormField fd, string title, Action<FormField> Configure)
        {
            var field = new FormField {Layout = LayoutType.Tab, Title = title};
            Configure(field);
            if (fd.Fields.All(x => x.Key != field.Key)) fd.Fields.Add(field);
            return fd;
        }

        public static FormField AddYesNo(this FormField fd, string key, string title, string desc,
            bool required = false, int width = 50)
        {
            fd.AddInput(key, title, x =>
            {
                x.Width = width;
                x.FieldType = FormFieldType.YesNo;
                x.Description = desc;

                if (required)
                    x.AddRequired($"{title} is required");

            });
            return fd;
        }

        public static FormField AddInput(this FormField fd, string key, string title = null, bool required = false,
            int width = 50)
        {
            fd.AddInput(key, title, x =>
            {
                x.Width = width;
                x.FieldType = FormFieldType.Text;

                if (required)
                    x.AddRequired($"{title} is required");
            });
            return fd;
        }

        public static FormField AddInput(this FormField fd, string key, string title, Action<FormField> configure)
        {
            var field = new FormField(key)
            {
                Layout = LayoutType.Default, FieldType = FormFieldType.Text,
                Title = string.IsNullOrEmpty(title) ? key : title
            };
            configure.Invoke(field);
            if (fd.Fields.All(x => x.Key != field.Key)) fd.Fields.Add(field);
            return fd;
        }

        public static FormField AddRule(this FormField fd, ValRule rule)
        {
            fd.Rules.Add(rule);
            return fd;
        }

        public static FormField AddRequired(this FormField fd, string message)
        {
            fd.AddRule(ValRule.Required(message));
            return fd;
        }
    }
}