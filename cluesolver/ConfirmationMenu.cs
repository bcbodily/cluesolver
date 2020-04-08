using System;
using System.IO;

namespace cluesolver
{
    public class ConfirmationMenu
    {
        public ConfirmationMenu(string message, string title = "")
        {
            Message = message;
            Title = title;
        }

        public string Message { get; }

        public string Title { get; }

        public void Show(TextWriter output)
        {
            var topBorder = " ┌─";
            var topSpacer = "║└─";
            var botSpacer = "║  ";

            for (int i = 0; i < Title.Length + 1; i++)
            {
                topBorder += "─";
                topSpacer += "─";
            }
            topBorder += "┐";
            topSpacer += "┘";

            var formattedTitle = $"╔╡ {Title} ╞";
            var formattedMessage = $"║  {Message}  ║";

            while (topSpacer.Length < formattedMessage.Length - 1)
            {
                topSpacer += " ";
            }
            topSpacer += "║";

            while (botSpacer.Length < formattedMessage.Length - 1)
            {
                botSpacer += " ";
            }
            botSpacer += "║";

            var bottomBorder = "╚";
            while (formattedTitle.Length < formattedMessage.Length - 1)
            {
                formattedTitle += "═";
            }
            formattedTitle += "╗";
            while (bottomBorder.Length < formattedTitle.Length - 1)
            {
                bottomBorder += "═";
            }
            bottomBorder += "╝";

            output.WriteLine(topBorder);
            output.WriteLine(formattedTitle);
            output.WriteLine(topSpacer);
            output.WriteLine(botSpacer);
            output.WriteLine(formattedMessage);
            output.WriteLine(botSpacer);
            output.WriteLine(botSpacer);
            output.WriteLine(bottomBorder);
            output.WriteLine();

            output.Write(" [ENTER TO CONTINUE] ");

            while (Console.ReadKey(true).Key != ConsoleKey.Enter);
            output.WriteLine();
        }
    }

}