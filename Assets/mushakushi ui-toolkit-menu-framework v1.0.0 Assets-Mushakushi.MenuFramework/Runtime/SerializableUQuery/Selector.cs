using System.Collections.Generic;

namespace Mushakushi.MenuFramework.Runtime.SerializableUQuery
{
    [System.Serializable]
    public class Selector
    {
        public SelectorType type;
        
        public PseudoSelector state;
        
        public List<string> nameOptions;
        
        public List<string> classOptions;
        
        /// <summary>
        /// The name(s) that will be selected. 
        /// </summary>
        [NameClassSelector(nameof(nameOptions))]
        public string[] names;

        /// <summary>
        /// The class(es) that will be selected.
        /// </summary>
        [NameClassSelector(nameof(classOptions))]
        public string[] classes;
        
        /// <param name="type">
        /// The type of selector.
        /// </param>
        /// <param name="state">
        /// The <see cref="PseudoSelector"/> that will be selected.
        /// </param>
        /// <param name="nameOptions">
        /// The options for name selection. 
        /// </param>
        /// <param name="classOptions">
        /// The options for class selection.
        /// </param>
        public Selector(SelectorType type, PseudoSelector state, List<string> nameOptions, List<string> classOptions)
        {
            this.type = type;
            this.state = state;
            this.nameOptions = nameOptions;
            this.classOptions = classOptions;
            names = new string[] { };
            classes = new string[] { };
        }
        
        /// <inheritdoc />
        public Selector(SelectorType type, PseudoSelector state)
            : this(type, state, new List<string>(), new List<string>()) { }

        /// <inheritdoc />
        /// <remarks>
        /// The <see cref="nameOptions"/> and <see cref="classOptions"/> must not be null,
        /// otherwise it's impossible to know if the property doesn't exist or is just set to
        /// null via reflection.
        /// </remarks>
        public Selector()
            : this(SelectorType.Wildcard, 0) { }
    }
}