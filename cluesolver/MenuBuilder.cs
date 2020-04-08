using System;
using System.Collections.Generic;

namespace cluesolver
{
    public class MenuBuilder<TMain, TSecondary>
    {
        public MenuBuilder(string title, IEnumerable<TMain> mainEntries, IDictionary<string, TSecondary> secondaryEntries = null)
        {
            CurrentTitle = title;

            MainEntries = mainEntries != null ?
                new List<TMain>(mainEntries) :
                new List<TMain>();

            SecondaryEntries = secondaryEntries != null ?
                new Dictionary<string, TSecondary>(secondaryEntries) :
                new Dictionary<string, TSecondary>();
        }

        public MenuBuilder()
        {
            MainEntries = new List<TMain>();
            SecondaryEntries = new Dictionary<string, TSecondary>();
        }

        private IList<TMain> MainEntries { get; }

        private IDictionary<string, TSecondary> SecondaryEntries { get; }

        private string CurrentTitle { get; set; } = "Menu";

        public MenuBuilder<TMain, TSecondary> AddEntry(TMain entry)
        {
            MainEntries.Add(entry);
            return this;
        }

        public MenuBuilder<TMain, TSecondary> AddSecondaryEntry(string key, TSecondary entry)
        {
            SecondaryEntries[key] = entry;
            return this;
        }

        public Menu<TMain, TSecondary> Build()
        {
            IDictionary<int, TMain> mainNumericalEntries = new Dictionary<int, TMain>();
            IDictionary<string, TMain> mainTextEntries = new Dictionary<string, TMain>();

            var number = 1;
            foreach (var item in MainEntries)
            {
                mainNumericalEntries.Add(number++, item);
                mainTextEntries.Add(item.ToString(), item);
            }

            // find the longest single line
            var maxItemLength = 0;
            foreach (var item in MainEntries)
            {
                if (item.ToString().Length > maxItemLength)
                {
                    maxItemLength = item.ToString().Length;
                }
            }
            foreach (var item in SecondaryEntries.Values)
            {
                if (item.ToString().Length > maxItemLength)
                {
                    maxItemLength = item.ToString().Length;
                }
            }

            var maxKeyLength = MainEntries.Count.ToString().Length;
            foreach (var key in SecondaryEntries.Keys)
            {
                if (key.ToString().Length > maxKeyLength)
                {
                    maxKeyLength = key.ToString().Length;
                }
            }

            if (CurrentTitle.Length - 1 > maxItemLength)
            {
                maxItemLength = CurrentTitle.Length - 1;
            }
            var maxEntryLength = maxKeyLength + 2 + maxItemLength;
            var maxLineLength = 1 + 1 + maxEntryLength + 1 + 1;


            // make the lines
            var lines = new List<string>();

            lines.Add(buildTitleTopLine(maxKeyLength, maxItemLength, CurrentTitle));
            lines.Add(buildTitleLine(maxKeyLength, maxItemLength, CurrentTitle));
            lines.Add(buildTitleBottomLine(maxKeyLength, maxItemLength, CurrentTitle));
            lines.Add(buildBlankLine(maxKeyLength, maxItemLength));
            number = 1;
            foreach (var item in MainEntries)
            {
                lines.Add(buildMainEntryLine(maxKeyLength, maxItemLength, number++.ToString(), item.ToString()));
            }
            lines.Add(buildBlankLine(maxKeyLength, maxItemLength));


            if (SecondaryEntries.Count > 0)
            {
                lines.Add(buildDivider(maxKeyLength, maxItemLength));

                foreach (var key in SecondaryEntries.Keys)
                {
                    lines.Add(buildMainEntryLine(maxKeyLength, maxItemLength, key, SecondaryEntries[key].ToString()));
                }
            }

            lines.Add(buildBottomBorder(maxKeyLength, maxItemLength));

            // todo update
            return new Menu<TMain, TSecondary>(lines, mainNumericalEntries, mainTextEntries, SecondaryEntries);
        }

        public MenuBuilder<TMain, TSecondary> Title(string title)
        {
            CurrentTitle = title;
            return this;
        }

        private string buildBlankLine(int maxKeyLength, int maxItemLength) =>
            $"║ {"".PadLeft(maxKeyLength, ' ')}  {"".PadRight(maxItemLength, ' ')} ║";

        private string buildBottomBorder(int maxKeyLength, int maxItemLength) =>
            $"╚═{"".PadLeft(maxKeyLength, '═')}══{"".PadRight(maxItemLength, '═')}═╝";

        private string buildDivider(int maxKeyLength, int maxItemLength) =>
            $"╟─{"".PadLeft(maxKeyLength, '─')}──{"".PadRight(maxItemLength, '─')}─╢";

        private string buildMainEntryLine(int maxKeyLength, int maxItemLength, string key, string item) =>
            $"║ {key.PadLeft(maxKeyLength, ' ')}) {item.PadRight(maxItemLength, ' ')} ║";

        private string buildTitleBottomLine(int maxKeyLength, int maxItemLength, string title) =>
            $"║└─{"".PadRight(title.Length, '─')}─┘{"".PadLeft(maxKeyLength + maxItemLength - title.Length, ' ')}║";

        private string buildTitleLine(int maxKeyLength, int maxItemLength, string title) =>
            $"╔╡ {title} ╞{"".PadLeft(maxKeyLength + maxItemLength - title.Length, '═')}╗";

        private string buildTitleTopLine(int maxKeyLength, int maxItemLength, string title) =>
            $" ┌─{"".PadRight(title.Length, '─')}─┐";
    }
}