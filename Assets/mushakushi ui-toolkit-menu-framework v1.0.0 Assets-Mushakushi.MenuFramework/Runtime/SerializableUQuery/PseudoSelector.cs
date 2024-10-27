namespace Mushakushi.MenuFramework.Runtime.SerializableUQuery
{

    /// <summary>
    /// Selection rules for a pseudo selector.
    /// </summary>
    /// <remarks>Source: https://docs.unity3d.com/ScriptReference/UIElements.UQueryBuilder_1.html</remarks>
    public enum PseudoSelector
    {
        /// <summary>Selects all elements that are active.</summary>
        Active,

        /// <summary>Selects all elements that are checked.</summary>
        Checked,

        /// <summary>Selects all elements that are enabled.</summary>
        Enabled,

        /// <summary>Selects all elements that are focused.</summary>
        Focused,

        /// <summary>Selects all elements that are hovered.</summary>
        Hovered,

        /// <summary>Selects all elements that are visible.</summary>
        Visible,
    }
}