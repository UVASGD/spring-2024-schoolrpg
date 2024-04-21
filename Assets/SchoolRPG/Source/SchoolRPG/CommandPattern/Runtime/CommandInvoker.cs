namespace SchoolRPG.CommandPattern.Runtime
{
    public static class CommandInvoker
    {
        public static void Execute(ICommand command)
        {
            command.Execute();
        }
    }
}