﻿using Core;
using PocketSharp;
using PocketSharp.Models;
using System;
using System.Threading.Tasks;

namespace RegisterCLI
{
    internal static class Program
    {
        private static Config _config = new ConfigBuilder(".").Build();

        private static async Task<int> Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var _client = new PocketClient(_config.PocketConsumerKey, callbackUri: _config.PocketRedirectUri);
            string requestCode = await _client.GetRequestCode();
            Console.WriteLine(_client.GenerateRegistrationUri(requestCode).ToString());
            Console.WriteLine("Press enter after authorizing app...");
            Console.ReadLine();
            PocketUser pocketUser = await _client.GetUser(requestCode);

            IUserService userService = UserService.BuildUserService(_config.StorageConnectionString);

            Console.WriteLine("Input your kindle email:");
            var kindleEmail = Console.ReadLine();

            var user = new User()
            {
                AccessCode = pocketUser.Code,
                PocketUsername = pocketUser.Username,
                KindleEmail = kindleEmail,
                LastProcessingDate = DateTime.UtcNow
            };

            await userService.AddUserAsync(user);

            Console.WriteLine("Bye World!");
            Console.ReadLine();

            return 0;
        }
    }
}