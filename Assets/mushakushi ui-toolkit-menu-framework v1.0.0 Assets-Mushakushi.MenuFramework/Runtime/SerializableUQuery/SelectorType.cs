namespace Mushakushi.MenuFramework.Runtime.SerializableUQuery
{
    /// <summary>
    /// The type of selector. 
    /// </summary>
    public enum SelectorType
    {
        /// <summary>
        /// Selects by name
        /// </summary>
        Name, 
        
        /// <summary>
        /// Selects by style class
        /// </summary>
        Class, 
        
        /// <summary>
        /// Selects by a pseudo-state (i.e. :is)
        /// </summary>
        PseudoState, 
        
        /// <summary>
        /// Selects by a negative pseudo-state (i.e. :not)
        /// </summary>
        NegativePseudoState, 
        
        /// <summary>
        /// Selects everything (i.e. *)
        /// </summary>
        Wildcard, 
    }
}