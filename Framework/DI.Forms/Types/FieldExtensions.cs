﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Newtonsoft.Json;

namespace DI.Forms.Types
{
    public static class FieldExtensions
    {
        public static FormField AddAction(this FormField fd, string key, string viewId, string title = "")
        {
            var field = new FormField(key)
            {
                Layout = LayoutType.SubGrid,
                FieldType = FormFieldType.Action,
                Title = string.IsNullOrEmpty(title) ? key : title,
                ViewId = viewId
            };
            if (fd.Fields.All(x => x.Key != field.Key)) fd.Fields.Add(field);
            return fd;
        }

        public static FormField AddYesNo(this FormField fd, string key, string title, string desc,
            bool required = false, bool disabled = false)
        {
            fd.AddInput(key, title, x =>
            {
                x.Disabled = disabled;
                x.FieldType = FormFieldType.YesNo;
                x.Description = desc;

                if (required)
                    x.AddRequired($"{title} is required");
            });
            return fd;
        }


        public static FormField AddDate(this FormField fd, string key, string title = null, bool required = false,
            bool disabled = false)
        {
            fd.AddInput(key, title, x =>
            {
                x.Disabled = disabled;
                x.FieldType = FormFieldType.Date;
                x.Options = "DD/MM/YYYY";
                if (required)
                    x.AddRequired($"{title} is required");
            });
            return fd;
        }


        public static FormField AddTextList(this FormField fd, string key, string viewId, string title = null,
            bool required = false,
            string defaultVal = "")
        {
            fd.AddInput(key, title, x =>
            {
                x.FieldType = FormFieldType.TextList;
                x.ViewId = viewId;
                x.Options = defaultVal;
                if (required)
                    x.AddRequired($"{title} is required");
            });
            return fd;
        }

        public static FormField AddPickList(this FormField fd, string key, string viewId, string title = null,
            bool required = false,
            bool disabled = false)
        {
            fd.AddInput(key, title, x =>
            {
                x.Disabled = disabled;
                x.FieldType = FormFieldType.PickList;
                x.ViewId = viewId;
                if (required)
                    x.AddRequired($"{title} is required");
            });
            return fd;
        }

        public static FormField AddLookup(this FormField fd, string key, string viewId, IClientRoute route,
            string title = null, bool required = false,
            bool disabled = false, Action<FormField> configure = null)
        {
            fd.AddInput(key, title, x =>
            {
                x.Disabled = disabled;
                x.FieldType = FormFieldType.Lookup;
                x.ViewId = viewId;
                x.Options = route.Path();
                if (required)
                    x.AddRequired($"{title} is required");
                if (configure != null)
                    configure(x);
            });
            return fd;
        }

        public static FormField AddLabel(this FormField fd, string key, string title = null)
        {
            fd.AddInput(key, title, x => { x.FieldType = FormFieldType.Label; });
            return fd;
        }

        public static FormField AddLink(this FormField fd, string key, IClientRoute route, string title = null)
        {
            fd.AddInput(key, title, x =>
            {
                x.FieldType = FormFieldType.Link;
                x.Options = route.Path();
            });
            return fd;
        }

        public static FormField AddExtLink(this FormField fd, string key, string value, string desc,
            string title = null)
        {
            fd.AddInput(key, title, x =>
            {
                x.Description = desc;
                x.FieldType = FormFieldType.ExtLink;
                x.Options = value;
            });
            return fd;
        }

        public static FormField AddInput(this FormField fd, string key, string title = null, bool required = false,
            bool disabled = false)
        {
            fd.AddInput(key, title, x =>
            {
                x.FieldType = FormFieldType.Text;
                if (required)
                    x.AddRequired($"{title} is required");
                x.Disabled = disabled;
            });
            return fd;
        }

        public static FormField AddFiller(this FormField fd)
        {
            fd.AddInput(Guid.NewGuid().ToString("N"), " ", x =>
            {
                x.FieldType = FormFieldType.Filler;
                x.Disabled = true;
            });
            return fd;
        }

        public static FormField AddDisabledInput(this FormField fd, string key, string title)
        {
            fd.AddInput(key, title, x =>
            {
                x.FieldType = FormFieldType.Text;
                x.Disabled = true;
            });
            return fd;
        }

        public static FormField AddNumeric(this FormField fd, string key, string title = null, bool required = false,
            int min = 0, int max = 9999)
        {
            fd.AddInput(key, title, x =>
            {
                x.FieldType = FormFieldType.Number;
                if (required)
                    x.AddRequired($"{title} is required");
                x.Options = JsonConvert.SerializeObject(new {min, max});
            });
            return fd;
        }

        public static FormField AddDecimal(this FormField fd, string key, string title = null, bool required = false,
            bool disabled = false)
        {
            fd.AddInput(key, title, x =>
            {
                x.Disabled = disabled;
                x.FieldType = FormFieldType.Decimal;
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

        #region Layouts

        public static FormField AddDivider(this FormField fd, string title = "")
        {
            var field = new FormField {Layout = LayoutType.Divider, Title = title};
            if (fd.Fields.All(x => x.Key != field.Key)) fd.Fields.Add(field);
            return fd;
        }

        public static FormField AddFieldGroup(this FormField fd, Action<FormField> Configure)
        {
            var field = new FormField {Layout = LayoutType.FieldGroup};
            Configure(field);
            if (fd.Fields.All(x => x.Key != field.Key)) fd.Fields.Add(field);
            return fd;
        }

        #endregion

        #region Select List

        public static FormField AddSelect<T>(this FormField fd, string key, string title = null, bool required = false,
            bool disabled = false) where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum)
                throw new Exception("must be a enum");

            var values = Enum.GetValues(typeof(T));

            var rCol = new List<SelectItem>();
            foreach (var value in values)
            {
                var desc = Enum.GetName(typeof(T), value);
                var fi = typeof(T).GetField(value.ToString());
                var attributes = (DescriptionAttribute[]) fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (attributes != null && attributes.Length > 0)
                    desc = attributes[0].Description;
                rCol.Add(new SelectItem($"{(int) value}", desc));
            }


            // var rCol = (from int item in values
            //          select new SelectItem($"{item}", Enumerations.GetEnumDescription((MyEnum)value)).ToList();
            return fd.AddSelect(key, rCol, title, required, disabled);
        }

        public static FormField AddSelect(this FormField fd, string key, List<SelectItem> options, string title = null,
            bool required = false,
            bool disabled = false)
        {
            fd.AddInput(key, title, x =>
            {
                x.Disabled = disabled;
                x.FieldType = FormFieldType.Select;
                x.Options = options;
                if (required)
                    x.AddRequired($"{title} is required");
            });
            return fd;
        }

        #endregion


        #region Rules

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

        #endregion


        #region contact details

        public static FormField AddPhone(this FormField fd, string key, string title = null, bool required = false,
            bool disabled = false)
        {
            fd.AddInput(key, title, x =>
            {
                x.Disabled = disabled;
                x.FieldType = FormFieldType.Text;
                if (required)
                    x.AddRequired($"{title} is required");
                x.AddRule(ValRule.Regex(
                    "/^((\\+[1-9]{1,4}[ \\-]*)|(\\([0-9]{2,3}\\)[ \\-]*)|([0-9]{2,4})[ \\-]*)*?[0-9]{3,4}?[ \\-]*[0-9]{3,4}?$/",
                    $"{title} is invalid"));
            });
            return fd;
        }

        public static FormField AddEmail(this FormField fd, string key, string title = null, bool required = false,
            bool disabled = false)
        {
            fd.AddInput(key, title, x =>
            {
                x.Disabled = disabled;
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