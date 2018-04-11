using Discord.Commands;
using Discord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord.WebSocket;

namespace MyDiscordBot.Modules
{
    public class Misc : ModuleBase<SocketCommandContext>
    {
        [Command("Imbed")]
        public async Task Imbed([Remainder]string message)
        {
            var embed = new EmbedBuilder();
            embed.WithTitle("Message By: " + Context.User.Username);
            embed.WithDescription(message);
            embed.WithColor(new Color(0, 255, 255));
        
            await Context.Channel.SendMessageAsync("", false, embed);
        }

        [Command("Credits")]
        public async Task Credits([Remainder]string arg = "")
        {
            var embed = new EmbedBuilder();
            embed.WithTitle("Credits go to the following people for helping make this bot a reality!");
            embed.WithDescription(Utilities.GetAlert("CREDITS"));
            embed.WithColor(new Color(0, 255, 255));

            await Context.Channel.SendMessageAsync("", false, embed);
        }


        [Command("pick")]
        public async Task PickOne([Remainder]string message)
        {
            string[] options = message.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);

            Random r = new Random();
            string selection = options[r.Next(0, options.Length)];


            var embed = new EmbedBuilder();
            embed.WithTitle("Choice for " + Context.User.Username);
            embed.WithDescription(selection);
            embed.WithColor(new Color(255, 255, 0));
            embed.WithThumbnailUrl("http://www.anothercrowd.com/sites/default/files/flipacoin.jpeg");

            await Context.Channel.SendMessageAsync("", false, embed);
        }

        [Command("pm")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task Pm([Remainder]string arg = "")
        {
            var dmChannel = await Context.User.GetOrCreateDMChannelAsync();
            await dmChannel.SendMessageAsync(Utilities.GetAlert("PM"));
        }
        

        [Command("secret")]
        public async Task RevealSecret([Remainder]string message = "")
        {
            if (!UserIsSecretOwner((SocketGuildUser)Context.User))
            {
                await Context.Channel.SendMessageAsync(":x: You do not have required permision!" + Context.User.Mention);
                return;
            }
            var dmChannel = await Context.User.GetOrCreateDMChannelAsync();
            await dmChannel.SendMessageAsync(Utilities.GetAlert("SECRET"));
        }

        private bool UserIsSecretOwner(SocketGuildUser user)
        {
            string targerRoleName = "EFTTeam";
            var result = from r in user.Guild.Roles
                         where r.Name == targerRoleName
                         select r.Id;
            ulong roleID = result.FirstOrDefault();
            if (roleID == 0) return false;
            var targetRole = user.Guild.GetRole(roleID);
            return user.Roles.Contains(targetRole);
        }














    }
}
