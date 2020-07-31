using Regulus.Remote;
using Regulus.Samples.Chat1.Common;
using Regulus.Utility;
using System;

namespace Regulus.Samples.Chat1.Client
{
    internal class LoginStatus : Regulus.Utility.IStatus
    {
        readonly ILogin _Login;
        private readonly Command _Command;

        public LoginStatus(ILogin login, Command command)
        {
            _Login = login;
            _Command = command;
        }

        void IStatus.Enter()
        {
            _LoginCommand(_Login);
        }

        private void _LoginCommand(ILogin gpi)
        {
            _Command.RemotingRegister<string, bool>("login", (name) => gpi.Login(name), _LoginResult);
        }

        private void _LoginResult(bool login_result)
        {
            System.Console.WriteLine($"Login is {login_result}");
        }

        void IStatus.Leave()
        {
            _Command.Unregister("login");
        }

        void IStatus.Update()
        {
            
        }
    }
}