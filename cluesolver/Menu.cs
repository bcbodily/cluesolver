using System;
using System.Collections.Generic;
using System.IO;

namespace cluesolver
{
    /// <summary>
    /// Represents a menu that provides a list of options for a user to select from
    /// </summary>
    /// <typeparam name="TMain">the type of the main items selectable from this menu</typeparam>
    /// <typeparam name="TSecondary">the type of the secondary items selectable from this menu</typeparam>
    public class Menu<TMain, TSecondary>
    {
        /// <summary>
        /// Creates a new instance of <see cref="Menu"/>
        /// </summary>
        /// <param name="lines">an enumeration of <see cref="string"/> lines to display</param>
        /// <param name="mainNumericalEntries">a <see cref="IDictionary"/> of items, keyed by int, that a user can select, by entering the keying number</param>
        /// <param name="mainTextEntries">a <see cref="IDictionary"/> of items, keyed by string, that a user can select by entering the keying text</param>
        /// <param name="secondaryEntries">a <see cref="IDictionary"/> of secondary items, keyed by string, that a user can select by entering the keying text</param>
        public Menu(IEnumerable<string> lines, IDictionary<int, TMain> mainNumericalEntries, IDictionary<string, TMain> mainTextEntries, IDictionary<string, TSecondary> secondaryEntries)
        {
            Lines = new List<string>(lines);
            MainNumberEntries = new Dictionary<int, TMain>(mainNumericalEntries);

            // key main entries with lowercase key
            MainTextEntries = new Dictionary<string, TMain>();
            foreach (var pair in mainTextEntries)
            {
                MainTextEntries.Add(pair.Key.ToLower(), pair.Value);
            }

            // key secondary entries with lowercase key
            SecondaryEntries = new Dictionary<string, TSecondary>();
            foreach (var pair in secondaryEntries)
            {
                SecondaryEntries.Add(pair.Key.ToLower(), pair.Value);
            }
        }

        /// <summary>
        /// Raised when the user selects an item
        /// </summary>
        public event EventHandler<ItemSelectedEventArgs<TMain>> ItemSelected;

        /// <summary>
        /// Raises the <see cref="Menu.ItemSelected"/> event
        /// </summary>
        /// <param name="eventArgs"></param>
        protected virtual void OnItemSelected(ItemSelectedEventArgs<TMain> eventArgs) => ItemSelected?.Invoke(this, eventArgs);

        /// <summary>
        /// Raised when the user selects a secondary item
        /// </summary>
        public event EventHandler<ItemSelectedEventArgs<TSecondary>> SecondaryItemSelected;

        /// <summary>
        /// Raises the <see cref="Menu.SecondaryItemSelected"/> event
        /// </summary>
        /// <param name="eventArgs"></param>
        protected virtual void OnSecondaryItemSelected(ItemSelectedEventArgs<TSecondary> eventArgs) => SecondaryItemSelected?.Invoke(this, eventArgs);

        /// <summary>
        /// the text lines that will be displayed
        /// </summary>
        /// <value></value>
        private IEnumerable<string> Lines { get; }

        /// <summary>
        /// the dictionary of main items, keyed by the numerical option number
        /// </summary>
        /// <value>the <see cref="IDictionary"/> of main items, keyed by number, that the user can select by entering the keying number</value>
        private IDictionary<int, TMain> MainNumberEntries { get; }

        /// <summary>
        /// the dictionary of main items, keyed by the text option
        /// </summary>
        /// <value>the <see cref="IDictionary"/> of main items, keyed by text, that the user can select by entering the keying text</value>
        private IDictionary<string, TMain> MainTextEntries { get; }

        /// <summary>
        /// the dictionary of secondary items, keyed by the text option
        /// </summary>
        /// <value>the <see cref="IDictionary"/> of secondary items, keyed by text, that the user can select by entering the keying text</value>
        private IDictionary<string, TSecondary> SecondaryEntries { get; }

        /// <summary>
        /// Shows the menu, using a specified <see cref="TextWriter"/> for output and a specified <see cref="TextReader"/> for input
        /// </summary>
        /// <param name="output">a <see cref="TextWriter"/> to use for output</param>
        /// <param name="input">a <see cref="TextReader"/> to use for input</param>
        public void Show(TextWriter output, TextReader input)
        {
            // argument checks
            if (output is null)
            {
                throw new ArgumentNullException(nameof(output));
            }

            if (input is null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            while (true)
            {
                // write the output
                foreach (var line in Lines)
                {
                    output.WriteLine(line);
                }

                // write the prompt
                output.Write(" # ");

                // get the input
                var userInput = input.ReadLine().ToLower();

                // check main text
                if (MainTextEntries.ContainsKey(userInput))
                {
                    OnItemSelected(new ItemSelectedEventArgs<TMain>(MainTextEntries[userInput]));
                    return;
                }

                // check secondary keys
                if (SecondaryEntries.ContainsKey(userInput))
                {
                    OnSecondaryItemSelected(new ItemSelectedEventArgs<TSecondary>(SecondaryEntries[userInput]));
                    return;
                }

                // check secondary values
                foreach (var pair in SecondaryEntries)
                {
                    if (userInput.Equals(SecondaryEntries[pair.Key].ToString().ToLower()))
                    {
                        OnSecondaryItemSelected(new ItemSelectedEventArgs<TSecondary>(SecondaryEntries[pair.Key]));
                        return;
                    }
                }

                // check numbers
                try
                {
                    var userNumber = int.Parse(userInput);
                    if (MainNumberEntries.ContainsKey(userNumber))
                    {
                        OnItemSelected(new ItemSelectedEventArgs<TMain>(MainNumberEntries[userNumber]));
                        return;
                    }
                }
                catch (FormatException)
                {
                    // just ignore
                }

                // otherwise, bad input
                output.WriteLine("Invalid input");
                output.WriteLine();
            }
        }
    }
}