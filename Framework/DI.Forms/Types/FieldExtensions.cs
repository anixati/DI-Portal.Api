using System;
using System.Collections.Generic;
using System.Linq;

namespace DI.Forms.Types
{
    public static class FieldExtensions
    {

        public static FormField AddFieldGroup(this FormField fd, Action<FormField> Configure)
        {
            var field = new FormField { Layout = LayoutType.FieldGroup };
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

        public static FormField AddSelect<T>(this FormField fd, string key, string title = null, bool required = false,
            int width = 50) where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum)
                throw new Exception("must be a enum");

            var values = Enum.GetValues(typeof(T));
            var rCol = (from int item in values select
                new SelectFieldOption($"{item}", Enum.GetName(typeof(T), item))).ToList();
            return fd.AddSelect(key, rCol, title, required, width);
        }

        public static FormField AddSelect(this FormField fd, string key, List<SelectFieldOption> options, string title = null, bool required = false,
            int width = 50)
        {
            fd.AddInput(key, title, x =>
            {
                x.Width = width;
                x.FieldType = FormFieldType.Select;
                x.Options = options;
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
                Layout = LayoutType.Default,
                FieldType = FormFieldType.Text,
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


        #region MyRegion

        public static FormField AddPhone(this FormField fd, string key, string title = null, bool required = false,
            int width = 50)
        {
            fd.AddInput(key, title, x =>
            {
                x.Width = width;
                x.FieldType = FormFieldType.Text;
                if (required)
                    x.AddRequired($"{title} is required");
                x.AddRule(ValRule.Regex("/^((\\+[1-9]{1,4}[ \\-]*)|(\\([0-9]{2,3}\\)[ \\-]*)|([0-9]{2,4})[ \\-]*)*?[0-9]{3,4}?[ \\-]*[0-9]{3,4}?$/", $"{title} is invalid"));
            });
            return fd;
        }
        public static FormField AddEmail(this FormField fd, string key, string title = null, bool required = false,
            int width = 50)
        {
            fd.AddInput(key, title, x =>
            {
                x.Width = width;
                x.FieldType = FormFieldType.Text;
                if (required)
                    x.AddRequired($"{title} is required");
                x.AddRule(ValRule.Email($"{title} is invalid email"));
            });
            return fd;
        }


        #endregion
    }
}