using DI.Forms.Core;
using DI.Forms.Types;

namespace DI.Forms.Handlers
{
    public abstract class FormBuilder : IFormBuilder
    {
        private AppSettings _settings;

        protected virtual FormType FormType => FormType.Wizard;
        public abstract string FormName { get; }


        public IFormState Create(AppSettings settings)
        {
            _settings = settings;
            var fs = new FormState(FormName, FormType);
            CreateSchema(fs.Schema);
            return fs;
        }

        protected virtual void CreateSchema(FormSchema fsSchema)
        {
        }

        protected string GetConfigValue(string key)
        {
            if (_settings != null && _settings.Map.ContainsKey(key))
                return _settings.Map[key];
            return string.Empty;
        }
    }

    public abstract class DialogBuilder : FormBuilder
    {
        protected override FormType FormType => FormType.Dialog;
    }
}