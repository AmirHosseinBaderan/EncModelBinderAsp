using EncTest.Crypto;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;

namespace EncTest.Binder;

public class FteamModelBinderProvider : IModelBinderProvider
{
    public IModelBinder? GetBinder(ModelBinderProviderContext context)
    {
        return new FteamModelBinder();
    }
}

public class FteamModelBinder : IModelBinder
{
    public async Task BindModelAsync(ModelBindingContext bindingContext)
    {
        var type = bindingContext.ModelMetadata.ModelType;
        var request = bindingContext.HttpContext.Request.Body;
        using StreamReader sr = new(request);
        var content = await sr.ReadToEndAsync();
        content = content.Trim('"').Trim('\\');

        var key = "12345-12345678-2";
        var decData = JsonConvert.DeserializeObject(content.Decrypt(key), type);

        bindingContext.Result = ModelBindingResult.Success(decData);
    }
}
