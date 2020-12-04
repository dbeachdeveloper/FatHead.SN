using System.Collections.Generic;

namespace FatHead.Converters
{
    public interface IModelConverter
    {
        void ConvertModelFromModel<T, U>(T modelToCopy, U modelToCopyTo)
            where T : class, new()
            where U : class, new();

        IList<U> ConvertModelListFromModelList<T, U>(IList<T> modelsToCopy)
            where T : class, new()
            where U : class, new();
    }
}
