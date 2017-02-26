namespace BookShopRepository.Bases
{
    /// <summary>
    /// Supports Hierarchy and searching property by name  
    /// </summary>
    public interface IObjectReference
    {
        /// <summary>
        /// Will return null if there is no parent object in hierarchy
        /// </summary>
        IObjectReference Parent { get; }

        
        /// Will return null if property is not found or if property value is an inferior object(s) of type IObjectReference
        /// Will throw if null or empty
        object GetValueOfProperty(string propertyName); 
    }
}