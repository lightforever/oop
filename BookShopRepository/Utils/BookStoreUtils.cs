using System;
using BookShopRepository.Bases;

namespace BookShopRepository.Utils
{
    /// <summary>
    /// Utils class for working with BookStores and Books
    /// </summary>
    public static class BookStoreUtils
    {
        /// <summary>
        /// Get inferior objects which contains in property with parametrName
        /// </summary>
        /// <param name="propertyOwner">Container object where we will search</param>
        /// <param name="propertyName">Name of property to search in container object. Parametr must be IObjectReference[] type</param>
        /// <returns>IObjectReference[] - inferior objects of propertyOwner</returns>
        public static IObjectReference[] GetInferiorObjects(IObjectReference propertyOwner, string propertyName)
        {
            if (ReferenceEquals(propertyOwner, null))
            {
                throw new ArgumentNullException("propertyOwner");
            }

            if (string.IsNullOrWhiteSpace(propertyName))
            {
                throw new ArgumentException("Property name can't be null or empty");
            }

            //Searching property in container
            var property = propertyOwner.GetValueOfProperty(propertyName);
            if (ReferenceEquals(property, null))
            {
                return null;
            }

            //We can't get property Title of Book for example
            var result = property as IObjectReference[];
            if (ReferenceEquals(result,null))
            {
                throw new Exception("Property value must be IObjectReference[] type");
            }

            return result;
        }
    }
}