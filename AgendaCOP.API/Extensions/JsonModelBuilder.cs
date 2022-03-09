using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaCOP.API.Extensions
{
    public class JsonModelBinder : IModelBinder
    {
        private readonly IOptions<MvcJsonOptions> _jsonOptions;
        private readonly FormFileModelBinder _formFileModelBinder;

        public JsonModelBinder(IOptions<MvcJsonOptions> jsonOptions, ILoggerFactory loggerFactory)
        {
            _jsonOptions = jsonOptions;
            _formFileModelBinder = new FormFileModelBinder(loggerFactory);
        }

        public async Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null) throw new ArgumentNullException(nameof(bindingContext));

            // Check the value sent in
            var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.FieldName);
            if (valueProviderResult == ValueProviderResult.None)
            {
                //The JSON wasn't found
                var message = bindingContext.ModelMetadata.ModelBindingMessageProvider.MissingBindRequiredValueAccessor(bindingContext.FieldName);
                bindingContext.ModelState.TryAddModelError(bindingContext.ModelName, message);
                return;
            }

            var rowValue = valueProviderResult.FirstValue;

            //Deserializable o JSON
            var modeljson = JsonConvert.DeserializeObject(rowValue, bindingContext.ModelType, _jsonOptions.Value.SerializerSettings);

            foreach (var property in bindingContext.ModelMetadata.Properties)
            {
                if (property.ModelType != (typeof(IFormFile)))
                    continue;

                var fieldName = property.BinderModelName ?? property.PropertyName;
                var modelName = fieldName;
                var propertyModel = property.PropertyGetter(bindingContext.Model);
                ModelBindingResult modelBindingResult;
                using (bindingContext.EnterNestedScope(property, fieldName, modelName, propertyModel))
                {
                    await _formFileModelBinder.BindModelAsync(bindingContext);
                    modelBindingResult = bindingContext.Result;
                }

                if (modelBindingResult.IsModelSet)
                {
                    //The IFormFile was sucessfully bound, assign it to  the correspondent property on the model
                    property.PropertySetter(modeljson, modelBindingResult.Model);
                }
                else if (property.IsBindingRequired)
                {
                    var message = property.ModelBindingMessageProvider.MissingBindRequiredValueAccessor(fieldName);
                    bindingContext.ModelState.TryAddModelError(modelName, message);
                }

            }
            bindingContext.Result = ModelBindingResult.Success(modeljson);
        }
    }
}
