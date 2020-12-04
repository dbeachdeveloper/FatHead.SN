using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FatHead.Converters
{
    public class ModelConverter : IModelConverter
    {
        /// <summary>
        /// Copies one model's properties to another if the property names are the same.
        /// Copy database models to display models that may or may not have all of the same properties
        /// </summary>
        /// <typeparam name="T">Generic Model</typeparam>
        /// <typeparam name="U">Generic Model</typeparam>
        /// <param name="modelToCopy">The generic model to copy</param>
        /// <param name="modelToCopyTo">The generic model to copy to</param>
        public void ConvertModelFromModel<T, U>(T modelToCopy, U modelToCopyTo)
            where T : class, new()
            where U : class, new()
        {
            IList<PropertyInfo> main = modelToCopy.GetType().GetProperties().ToList();
            IList<PropertyInfo> copy = modelToCopyTo.GetType().GetProperties().ToList();

            try
            {
                foreach (PropertyInfo m in main)
                {
                    foreach (PropertyInfo c in copy)
                    {
                        if (m.Name == c.Name)
                        {
                            var value = m.GetValue(modelToCopy, null);
                            c.SetValue(modelToCopyTo, value);
                            break;
                        }
                    }
                }
            }
            catch
            {
                throw new Exception();
            }
        }

        public IList<U> ConvertModelListFromModelList<T, U>(IList<T> modelsToCopy)
            where T : class, new()
            where U : class, new()
        {
            IList<U> copiedModels = new List<U>();

            foreach (T item in modelsToCopy)
            {
                U copy = new U();

                ConvertModelFromModel(item, copy);

                copiedModels.Add(copy);
            }

            return copiedModels;
        }
    }
}
